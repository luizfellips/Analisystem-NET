using System.ComponentModel.DataAnnotations;

namespace Analisystem.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Type the current password for your user.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Type a new password for your user.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm the new password for your user.")]
        [Compare("NewPassword",ErrorMessage = "The passwords do not match.")]
        public string ConfirmNewPassword { get; set; }
        
    }
}
