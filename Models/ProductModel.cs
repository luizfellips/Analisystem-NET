using System.ComponentModel.DataAnnotations;

namespace Analisystem.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Name is required.")]
		public string Name { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }
		public string? Purpose { get; set; }
		public string? Provider { get; set; }
		public string? ProviderNumber { get; set; }

		public DateTime RegisteredDate { get; set; }
		public DateTime? LastUpdated { get; set; }
	}
}
