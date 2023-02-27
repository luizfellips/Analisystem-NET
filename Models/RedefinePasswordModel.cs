using System.ComponentModel.DataAnnotations;

namespace Analisystem.Models
{
    public class RedefinePasswordModel
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Type a valid email.")]
        public string Email { get; set; }
    }
}
