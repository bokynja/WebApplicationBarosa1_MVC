using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public List<Dog> GetDogsWithCategory(int categoryId)
        {
            return Dogs
                .Include(d => d.Category)  // Uključuje Category u rezultat
                .Where(d => d.CategoryId == categoryId)
                .ToList();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 9, TypeOfBreed = "Hunting Dogs" },
                new Category { CategoryId = 10, TypeOfBreed = "Companion Dogs" },
                new Category { CategoryId = 11 , TypeOfBreed = "Kid-Friendly Dogs" }
            );

            modelBuilder.Entity<Dog>().HasData(
                new Dog
                {
                    Id = 1,
                    Breed = "Golden Retriever",
                    Description = "Friendly and intelligent, the Golden Retriever is a popular family dog known for its gentle temperament and loyalty.",
                    SKU = "GR9999001",
                    ListPrice = 1200,
                    Gender = Dog.Sex.Male,
                    CategoryId = 11, // Kid-Friendly Dogs
                    ImageUrl=""
                
                },
                new Dog
                {
                    Id = 2,
                    Breed = "German Shepherd",
                    Description = "Highly intelligent and versatile, the German Shepherd is an excellent working dog and family companion.",
                    SKU = "GS777777701",
                    ListPrice = 1400,
                    Gender = Dog.Sex.Female,
                    CategoryId = 9, // Hunting Dogs
                    ImageUrl = ""
                },
                new Dog
                {
                    Id = 3,
                    Breed = "Bulldog",
                    Description = "With its distinctive wrinkled face and muscular build, the Bulldog is known for its friendly and courageous nature.",
                    SKU = "BD5555501",
                    ListPrice = 1600,
                    Gender = Dog.Sex.Male,
                    CategoryId = 10, // Companion Dogs
                    ImageUrl = ""
                },
                new Dog
                {
                    Id = 4,
                    Breed = "Poodle",
                    Description = "Elegant and intelligent, the Poodle comes in various sizes and is known for its hypoallergenic coat.",
                    SKU = "PD3333333301",
                    ListPrice = 1300,
                    Gender = Dog.Sex.Female,
                    CategoryId = 10, // Companion Dogs
                    ImageUrl = ""
                },
                new Dog
                {
                    Id = 5,
                    Breed = "Beagle",
                    Description = "The Beagle is known for its keen sense of smell and tracking instinct, making it an excellent hunting dog.",
                    SKU = "BG1111111101",
                    ListPrice = 950,
                    Gender =Dog.Sex.Male,
                    CategoryId = 9, // Hunting Dogs
                    ImageUrl = ""
                },
                new Dog
                {
                    Id = 6,
                    Breed = "Siberian Husky",
                    Description = "Known for its striking appearance and endurance, the Siberian Husky is a strong, energetic, and playful breed.",
                    SKU = "SH000000001",
                    ListPrice = 1500,
                    Gender = Dog.Sex.Female,
                    CategoryId = 11, // Kid-Friendly Dogs
                    ImageUrl = ""
                }
            );
            

        }
    }
}
