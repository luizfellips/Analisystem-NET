using Analisystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Analisystem.Data
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}

		public DbSet<UserModel> Users { get; set; }
		public DbSet<ProductModel> Products { get; set; }
	}
}
