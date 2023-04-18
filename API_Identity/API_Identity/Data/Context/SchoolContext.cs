using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Identity.Data
{
    public class SchoolContext : IdentityDbContext<Student>    /// this must inheret from IdentityUser
    {
        public SchoolContext(DbContextOptions<SchoolContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)  /// to configure the name of tables
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().ToTable("Students");
            builder.Entity<IdentityUserClaim<string>>().ToTable("StudentClaims");   

        }

    }
}
