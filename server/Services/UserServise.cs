using Microsoft.EntityFrameworkCore;
using server.Intarfaces; 
using server.Models;
using System.Security.Cryptography;

namespace server.Services
{
    /// <summary>
    /// UserService.
    /// </summary>
    /// <seealso cref="server.Intarfaces.IUserService" />
    public class UserServise : IUserService
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServise"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserServise(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Verifies the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<bool> VerifyAsync(string email, string password)
        {
            Models.User fuser = await _context.Users.FirstAsync(u => u.Email == email);

            UserKey fuk = await _context.UserKeys.FirstAsync(uk => uk.UserId == fuser.UserId);

            var result = CreateUserKey(password, fuk.Salt);

            if (fuk.Password.SequenceEqual(result))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Models.User user, string password)
        {
            _context.Users.Add(user);

            _context.SaveChanges();

            var result = CreateUserKey(password);

            Models.User fuser = await _context.Users.FirstAsync(u => u.Email == user.Email);

            UserKey userKey = new UserKey()
            {
                Password = result.hashBytes,
                Salt = result.salt,
                UserId = fuser.UserId,
            };

            _context.UserKeys.Add(userKey);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Creates the user key.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private (byte[] hashBytes, byte[] salt) CreateUserKey(string password)
        {
            byte[] salt = new byte[8]; // 8 байт соли
            RandomNumberGenerator.Fill(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100))
            {
                byte[] hash = pbkdf2.GetBytes(8); // 8 байт хэша

                byte[] hashBytes = new byte[16];
                Array.Copy(salt, 0, hashBytes, 0, 8);
                Array.Copy(hash, 0, hashBytes, 8, 8);

                return (hashBytes, salt);
            }
        }

        /// <summary>
        /// Creates the user key.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        private byte[] CreateUserKey(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100))
            {
                byte[] hash = pbkdf2.GetBytes(8); // 8 байт хэша

                byte[] hashBytes = new byte[16];
                Array.Copy(salt, 0, hashBytes, 0, 8);
                Array.Copy(hash, 0, hashBytes, 8, 8);

                return hashBytes;
            }
        }

        /// <summary>
        /// Users the exist in system for email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<bool> UserExistInSystemForEmail(string email)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Email == email) == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Users the exist in system for nickname.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns></returns>
        public async Task<bool> UserExistInSystemForNickname(string nickname)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Nickname == nickname) == null)
            {
                return false;
            }

            return true;
        }

    }
}
