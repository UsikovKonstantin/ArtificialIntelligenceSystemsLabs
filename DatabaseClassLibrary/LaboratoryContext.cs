using Microsoft.EntityFrameworkCore;

namespace DatabaseClassLibrary;

public class LaboratoryContext : DbContext
{
	public DbSet<Employee> Employees { get; set; }
	public DbSet<User> Users { get; set; } 

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=laboratory.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Employee>().HasKey(e => e.Id); 
		modelBuilder.Entity<User>().HasKey(u => u.Id); 
	}
}
