using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    /// <summary>
    /// Register Dto class.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        [Required(ErrorMessage = "Никнейм является обязательным для заполнения.")]
        [MinLength(6, ErrorMessage = "Никнейм должен содержать минимум 6 английских символов.")]
        [StringLength(100, ErrorMessage = "Никнейм не может превышать 100 символов.")]
        public string Nickname { get; set; }

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

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [Compare("Password", ErrorMessage = "Пароли не совпадают для заполнения..")]
        [Required(ErrorMessage = "Подтверждение пароля является обязательным.")]
        public string ConfirmPassword { get; set; }
    }

}
