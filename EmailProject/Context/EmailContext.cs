using EmailProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmailProject.Context
{
    public class EmailContext :IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9DQS8R7\\MSSQLSERVER01; initial catalog=EmailDb; integrated security= true;TrustServerCertificate=true");
        }
        public DbSet<Message> Messages { get; set; }
    }
}
