using Microsoft.EntityFrameworkCore;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Hunting Dogs", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Companion Dogs", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Kid-Friendly Dogs", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Dog>().HasData(
                new Dog
                {
                    Id = 1,
                    Name = "Goldie",
                    Breed = "Golden Retriever",
                    Description = "Friendly and intelligent, the Golden Retriever is a popular family dog known for its gentle temperament and loyalty.",
                    SKU = "GR9999001",
                    ListPrice = 1200,
                    Gender = Sex.Male,
                    CategoryId = 3 // Kid-Friendly Dogs
                },
                new Dog
                {
                    Id = 2,
                    Name = "Coco",
                    Breed = "German Shepherd",
                    Description = "Highly intelligent and versatile, the German Shepherd is an excellent working dog and family companion.",
                    SKU = "GS777777701",
                    ListPrice = 1400,
                    Gender = Sex.Female,
                    CategoryId = 1 // Hunting Dogs
                },
                new Dog
                {
                    Id = 3,
                    Name = "Bully",
                    Breed = "Bulldog",
                    Description = "With its distinctive wrinkled face and muscular build, the Bulldog is known for its friendly and courageous nature.",
                    SKU = "BD5555501",
                    ListPrice = 1600,
                    Gender = Sex.Male,
                    CategoryId = 2 // Companion Dogs
                },
                new Dog
                {
                    Id = 4,
                    Name = "Pookie",
                    Breed = "Poodle",
                    Description = "Elegant and intelligent, the Poodle comes in various sizes and is known for its hypoallergenic coat.",
                    SKU = "PD3333333301",
                    ListPrice = 1300,
                    Gender = Sex.Female,
                    CategoryId = 2 // Companion Dogs
                },
                new Dog
                {
                    Id = 5,
                    Name = "Beebee",
                    Breed = "Beagle",
                    Description = "The Beagle is known for its keen sense of smell and tracking instinct, making it an excellent hunting dog.",
                    SKU = "BG1111111101",
                    ListPrice = 950,
                    Gender = Sex.Male,
                    CategoryId = 1 // Hunting Dogs
                },
                new Dog
                {
                    Id = 6,
                    Name = "Sibery",
                    Breed = "Siberian Husky",
                    Description = "Known for its striking appearance and endurance, the Siberian Husky is a strong, energetic, and playful breed.",
                    SKU = "SH000000001",
                    ListPrice = 1500,
                    Gender = Sex.Female,
                    CategoryId = 3 // Kid-Friendly Dogs
                }
            );
        }
    }
}
