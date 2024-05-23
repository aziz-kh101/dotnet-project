using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace project;

public class ApplciationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Category> Categories { get; set; }


    public ApplciationDbContext(DbContextOptions<ApplciationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var connectionString = "server=localhost;user=admin;password=sql2949;database=dotnetProject";
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                PasswordHash = PasswordHasher.HashPassword("admin123456"),
            }
        );
    }
}