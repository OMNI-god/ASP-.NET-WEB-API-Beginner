using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext:IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var roles=new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "e7a8b3e3-60a4-4d6d-b0be-30ef43fbf805",
                    ConcurrencyStamp = "e7a8b3e3-60a4-4d6d-b0be-30ef43fbf805",
                    Name = "reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = "baa69e2e-11bf-4524-96f7-b37a96a892a0",
                    ConcurrencyStamp = "baa69e2e-11bf-4524-96f7-b37a96a892a0",
                    Name = "writer",
                    NormalizedName = "WRITER"
                },
                new IdentityRole
                {
                    Id = "61a22f6f-76da-4a7d-b2b4-89d7b1d17d30",
                    ConcurrencyStamp = "61a22f6f-76da-4a7d-b2b4-89d7b1d17d30",
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
