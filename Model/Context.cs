using Food.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Food.Model
{
	public class Context : DbContext
	{

		public static ProjectConfiguration Configuration;

		public Context(DbContextOptions<Context> options, ProjectConfiguration configuration) : base(options)
		{
			if(configuration != null)
			{
				Context.Configuration = configuration;
			}
		}

		public Context()
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
				.HasOne<User>(s => s.User)
				.WithMany(g => g.Orders)
				.HasForeignKey(s => s.DriverId);
			
			modelBuilder.Entity<Order>()
				.Property(m => m.DriverId)
				.IsRequired(false);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(optionsBuilder.IsConfigured)
			{
				return;
			}
			optionsBuilder.UseSqlServer(Context.Configuration.DatabaseConfiguration.ConnectionString);
		}
	}
}
