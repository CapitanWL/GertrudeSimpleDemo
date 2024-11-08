using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    /// <summary>
    /// Aurization Dto class.
    /// </summary>
    public class AuthDto
    {

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Email является обязательным для заполнения..")]
        [StringLength(100, ErrorMessage = "Неверный формат почты. Проверьте и попробуйте снова.")]
        [EmailAddress(ErrorMessage = "Неверный формат почты. Проверьте и попробуйте снова.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "Пароль является обязательным для заполнения..")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        public string Password { get; set; }
    }
}
