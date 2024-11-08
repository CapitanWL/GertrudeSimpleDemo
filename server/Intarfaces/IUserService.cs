namespace server.Intarfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<bool> CreateAsync(Models.User user, string password);
        /// <summary>
        /// Verifies the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<bool> VerifyAsync(string email, string password);

        /// <summary>
        /// Users the exist in system for email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<bool> UserExistInSystemForEmail(string email);

        /// <summary>
        /// Users the exist in system for nickname.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns></returns>
        Task<bool> UserExistInSystemForNickname(string nickname);
    }
}
