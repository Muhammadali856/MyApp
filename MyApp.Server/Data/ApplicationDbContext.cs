using Microsoft.EntityFrameworkCore;
using MyApp.Server.Models;

namespace MyApp.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Dacha> Dachas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dacha>().HasData(
                    new Dacha()
                    {
                        Id = 1,
                        Name = "Sunset Villa",
                        Details = "A beautiful villa with a sunset view.",
                        Sqft = 2000,
                        Rate = 7.5,
                        ImageURL = "",
                        IsAvailable = "No",
                        Amenity = "Wi-Fi, Pool, Sauna",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new Dacha()
                    {
                        Id = 2,
                        Name = "Mountain Retreat",
                        Details = "Cozy retreat in the mountains.",
                        Sqft = 1800,
                        Rate = 6.2,
                        ImageURL = "",
                        IsAvailable = "Yes",
                        Amenity = "Wi-Fi, Fireplace, Hiking Trails",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new Dacha()
                    {
                        Id = 3,
                        Name = "Lake House",
                        Details = "Relaxing house by the lake.",
                        Sqft = 2200,
                        Rate = 8.0,
                        ImageURL = "",
                        IsAvailable = "Yes",
                        Amenity = "Wi-Fi, Boat Dock, BBQ",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new Dacha()
                    {
                        Id = 4,
                        Name = "Forest Cabin",
                        Details = "Secluded cabin in the forest.",
                        Sqft = 1200,
                        Rate = 4.5,
                        ImageURL = "",
                        IsAvailable = "No",
                        Amenity = "Fireplace, Hiking Trails, Pet Friendly",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new Dacha()
                    {
                        Id = 5,
                        Name = "City Loft",
                        Details = "Modern loft in the city center.",
                        Sqft = 950,
                        Rate = 9.0,
                        ImageURL = "",
                        IsAvailable = "Yes",
                        Amenity = "Wi-Fi, Rooftop Access, Gym",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
               );
        }
    }
}
