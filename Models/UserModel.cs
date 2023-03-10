using Analisystem.Enums;
using Analisystem.Helper;
using System.ComponentModel.DataAnnotations;

namespace Analisystem.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please type a name for the user.")]
		public string Name { get; set; }
        [Required(ErrorMessage = "Please type an e-mail.")]
        [EmailAddress(ErrorMessage = "The given email isn't valid!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please type a login.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Please type a strong password.")]
        public string Password { get; set; }
		public string? Address { get; set; }
		public string? Phone { get; set; }
		public string? CPF { get; set; }
		public AccessLevel ProfileLevel { get; set; }
		public DateTime RegisterDate { get; set; }
		public DateTime? LastUpdated { get; set; }

		public virtual List<ProductModel>? Products { get; set; }

		//methods

		public void SetHash()
		{
			Password = Password.GenerateHash();
		}

		public bool ValidatePassword(string value)
		{
			return Password == value.GenerateHash();
		}

		public string GenerateNewPassword()
		{
			string newPassword = Guid.NewGuid().ToString().Substring(0,8);
			Password = newPassword.GenerateHash();
			return newPassword;
		}

		public void SetPassword(string value)
		{
			Password = value.GenerateHash();
		}
	}
}
