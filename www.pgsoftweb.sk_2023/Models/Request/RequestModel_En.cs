using System.ComponentModel.DataAnnotations;

namespace www.pgsoftweb.sk_2023.Models.Request
{
    public class RequestSendModel_En
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = ModelUtil.requiredErrMessage_En)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = ModelUtil.requiredErrMessage_En)]
        [Email(ErrorMessage = ModelUtil.invalidEmailErrMessage_En)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [Required(ErrorMessage = ModelUtil.requiredErrMessage_En)]
        [Display(Name = "Write your message here")]
        public string Text { get; set; }
        /// <summary>
        /// Captcha
        /// </summary>
        [Display(Name = "Captcha")]
        public string Captcha { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// Confirm password
        /// </summary>
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}