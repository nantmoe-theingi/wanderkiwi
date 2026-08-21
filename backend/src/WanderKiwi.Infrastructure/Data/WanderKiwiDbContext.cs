using WanderKiwi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WanderKiwi.Infrastructure.Data;

public class WanderKiwiDbContext : DbContext
{
    public WanderKiwiDbContext(DbContextOptions<WanderKiwiDbContext> options)
        : base(options)
    {
    }

    public DbSet<Island> Islands { get; set; } = null!;

    public DbSet<Region> Regions { get; set; } = null!;

    public DbSet<Destination> Destinations { get; set; } = null!;

    public DbSet<Attraction> Attractions { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<DestinationCategory> DestinationCategories { get; set; } = null!;

    public DbSet<AttractionCategory> AttractionCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureRelationships(modelBuilder);

        SeedIslands(modelBuilder);
        SeedRegions(modelBuilder);
        SeedCategories(modelBuilder);
        SeedDestinations(modelBuilder);
        SeedAttractions(modelBuilder);
        SeedDestinationCategories(modelBuilder);
        SeedAttractionCategories(modelBuilder);
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>()
            .HasOne(r => r.Island)
            .WithMany(i => i.Regions)
            .HasForeignKey(r => r.IslandId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Destination>()
            .HasOne(d => d.Region)
            .WithMany(r => r.Destinations)
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attraction>()
            .HasOne(a => a.Destination)
            .WithMany(d => d.Attractions)
            .HasForeignKey(a => a.DestinationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DestinationCategory>()
            .HasKey(dc => new
            {
                dc.DestinationId,
                dc.CategoryId
            });

        modelBuilder.Entity<DestinationCategory>()
            .HasOne(dc => dc.Destination)
            .WithMany(d => d.DestinationCategories)
            .HasForeignKey(dc => dc.DestinationId);

        modelBuilder.Entity<DestinationCategory>()
            .HasOne(dc => dc.Category)
            .WithMany(c => c.DestinationCategories)
            .HasForeignKey(dc => dc.CategoryId);

        modelBuilder.Entity<AttractionCategory>()
            .HasKey(ac => new
            {
                ac.AttractionId,
                ac.CategoryId
            });

        modelBuilder.Entity<AttractionCategory>()
            .HasOne(ac => ac.Attraction)
            .WithMany(a => a.AttractionCategories)
            .HasForeignKey(ac => ac.AttractionId);

        modelBuilder.Entity<AttractionCategory>()
            .HasOne(ac => ac.Category)
            .WithMany(c => c.AttractionCategories)
            .HasForeignKey(ac => ac.CategoryId);
    }

    private static void SeedIslands(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Island>().HasData(
            new Island
            {
                Id = 1,
                Name = "North Island",
                Description = "Aotearoa's North Island, known for culture, beaches, geothermal landscapes and vibrant cities.",
                ImageUrl = "assets/images/north-island.jpg"
            },
            new Island
            {
                Id = 2,
                Name = "South Island",
                Description = "New Zealand's South Island, famous for mountains, lakes, fiords and outdoor adventures.",
                ImageUrl = "assets/images/south-island.jpg"
            }
        );
    }

    private static void SeedRegions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>().HasData(

            // North Island
            new Region { Id = 1, Name = "Northland", IslandId = 1 },
            new Region { Id = 2, Name = "Auckland", IslandId = 1 },
            new Region { Id = 3, Name = "Waikato", IslandId = 1 },
            new Region { Id = 4, Name = "Bay of Plenty", IslandId = 1 },
            new Region { Id = 5, Name = "Gisborne", IslandId = 1 },
            new Region { Id = 6, Name = "Taranaki", IslandId = 1 },
            new Region { Id = 7, Name = "Manawatū-Whanganui", IslandId = 1 },
            new Region { Id = 8, Name = "Hawke's Bay", IslandId = 1 },
            new Region { Id = 9, Name = "Wellington", IslandId = 1 },

            // South Island
            new Region { Id = 10, Name = "Tasman", IslandId = 2 },
            new Region { Id = 11, Name = "Nelson", IslandId = 2 },
            new Region { Id = 12, Name = "Marlborough", IslandId = 2 },
            new Region { Id = 13, Name = "West Coast", IslandId = 2 },
            new Region { Id = 14, Name = "Canterbury", IslandId = 2 },
            new Region { Id = 15, Name = "Otago", IslandId = 2 },
            new Region { Id = 16, Name = "Southland", IslandId = 2 }
        );
    }

    private static void SeedCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Nature" },
            new Category { Id = 2, Name = "Adventure" },
            new Category { Id = 3, Name = "Sightseeing" },
            new Category { Id = 4, Name = "Culture" },
            new Category { Id = 5, Name = "Food & Wine" },
            new Category { Id = 6, Name = "City" }
        );
    }

    private static void SeedDestinations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>().HasData(

            new Destination
            {
                Id = 1,
                Name = "Queenstown",
                RegionId = 15,
                Description = "New Zealand's adventure capital, surrounded by mountains and Lake Wakatipu.",
                ImageUrl = "assets/images/queenstown.png",
                Rating = 4.9m,
                ReviewCount = 980,
                IsPopular = true
            },

            new Destination
            {
                Id = 2,
                Name = "Rotorua",
                RegionId = 4,
                Description = "A geothermal wonderland known for Māori culture, hot springs and outdoor adventures.",
                ImageUrl = "assets/images/rotorua.jpg",
                Rating = 4.8m,
                ReviewCount = 980,
                IsPopular = true
            },

            new Destination
            {
                Id = 3,
                Name = "Auckland",
                RegionId = 2,
                Description = "New Zealand's largest city, surrounded by beautiful harbours, islands and beaches.",
                ImageUrl = "assets/images/auckland.jpg",
                Rating = 4.7m,
                ReviewCount = 1200,
                IsPopular = true
            },

            new Destination
            {
                Id = 4,
                Name = "Wanaka",
                RegionId = 15,
                Description = "A relaxed lakeside town surrounded by mountains and outdoor adventures.",
                ImageUrl = "assets/images/wanaka.jpg",
                Rating = 4.8m,
                ReviewCount = 850,
                IsPopular = true
            },

            new Destination
            {
                Id = 5,
                Name = "Wellington",
                RegionId = 9,
                Description = "New Zealand's creative capital, known for culture, food and waterfront views.",
                ImageUrl = "assets/images/wellington.jpg",
                Rating = 4.7m,
                ReviewCount = 920,
                IsPopular = true
            },

            new Destination
            {
                Id = 6,
                Name = "Christchurch",
                RegionId = 14,
                Description = "A vibrant South Island city surrounded by mountains, gardens and natural landscapes.",
                ImageUrl = "assets/images/christchurch.jpg",
                Rating = 4.7m,
                ReviewCount = 760,
                IsPopular = true
            },

            new Destination
            {
                Id = 7,
                Name = "Matamata",
                RegionId = 3,
                Description = "A charming Waikato town and gateway to the famous Hobbiton Movie Set.",
                ImageUrl = "assets/images/matamata.jpg",
                Rating = 4.7m,
                ReviewCount = 600,
                IsPopular = false
            },

            new Destination
            {
                Id = 8,
                Name = "Te Anau",
                RegionId = 16,
                Description = "A scenic lakeside town and gateway to Fiordland National Park and Milford Sound.",
                ImageUrl = "assets/images/te-anau.jpg",
                Rating = 4.8m,
                ReviewCount = 720,
                IsPopular = true
            }
        );
    }

    private static void SeedAttractions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attraction>().HasData(

            new Attraction
            {
                Id = 1,
                Name = "Skyline Queenstown",
                DestinationId = 1,
                Description = "Take in breathtaking views of Queenstown, Lake Wakatipu and the surrounding mountains.",
                ImageUrl = "assets/images/skyline-queenstown.jpg",
                Latitude = -45.0312,
                Longitude = 168.6626,
                Rating = 4.8m,
                ReviewCount = 1200,
                BestTime = "Dec - Feb",
                RecommendedDuration = "2-3 hours"
            },

            new Attraction
            {
                Id = 2,
                Name = "Ben Lomond Track",
                DestinationId = 1,
                Description = "A challenging alpine hike offering spectacular views over Queenstown and Lake Wakatipu.",
                ImageUrl = "assets/images/ben-lomond.jpg",
                Latitude = -45.0097,
                Longitude = 168.6167,
                Rating = 4.7m,
                ReviewCount = 713,
                BestTime = "Dec - Apr",
                RecommendedDuration = "6-8 hours"
            },

            new Attraction
            {
                Id = 3,
                Name = "TSS Earnslaw Cruise",
                DestinationId = 1,
                Description = "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.",
                ImageUrl = "assets/images/tss-earnslaw.jpg",
                Latitude = -45.0310,
                Longitude = 168.6600,
                Rating = 4.7m,
                ReviewCount = 980,
                BestTime = "Dec - Feb",
                RecommendedDuration = "1.5-2 hours"
            },

            new Attraction
            {
                Id = 4,
                Name = "Milford Sound",
                DestinationId = 8,
                Description = "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.",
                ImageUrl = "assets/images/milford.jpg",
                Latitude = -44.6414,
                Longitude = 167.9254,
                Rating = 4.9m,
                ReviewCount = 1420,
                BestTime = "Dec - Feb",
                RecommendedDuration = "4-6 hours"
            },

            new Attraction
            {
                Id = 5,
                Name = "Hobbiton Movie Set",
                DestinationId = 7,
                Description = "Step into the lush pastures of the Shire from The Lord of the Rings film trilogy.",
                ImageUrl = "assets/images/hobbiton.jpg",
                Latitude = -37.8721,
                Longitude = 175.6826,
                Rating = 4.8m,
                ReviewCount = 1250,
                BestTime = "Dec - Feb",
                RecommendedDuration = "2-3 hours"
            },

            new Attraction
            {
                Id = 6,
                Name = "Wai-O-Tapu Thermal Wonderland",
                DestinationId = 2,
                Description = "Explore colourful geothermal pools, volcanic landscapes and geothermal activity.",
                ImageUrl = "assets/images/waiotapu.jpg",
                Latitude = -38.3574,
                Longitude = 176.3668,
                Rating = 4.7m,
                ReviewCount = 1100,
                BestTime = "Nov - Mar",
                RecommendedDuration = "2-3 hours"
            },

            new Attraction
            {
                Id = 7,
                Name = "Aoraki / Mount Cook",
                DestinationId = 6,
                Description = "New Zealand's highest mountain surrounded by spectacular alpine landscapes and glaciers.",
                ImageUrl = "assets/images/mountcook.jpg",
                Latitude = -43.7344,
                Longitude = 170.1411,
                Rating = 4.9m,
                ReviewCount = 1500,
                BestTime = "Sep - Apr",
                RecommendedDuration = "1-2 days"
            },

            new Attraction
            {
                Id = 8,
                Name = "Abel Tasman National Park",
                DestinationId = 6,
                Description = "A stunning coastal national park known for golden beaches, clear water and walking trails.",
                ImageUrl = "assets/images/abel-tasman.jpg",
                Latitude = -40.9006,
                Longitude = 173.0769,
                Rating = 4.8m,
                ReviewCount = 900,
                BestTime = "Dec - Mar",
                RecommendedDuration = "1-2 days"
            }
        );
    }

    private static void SeedDestinationCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DestinationCategory>().HasData(

            // Queenstown
            new DestinationCategory
            {
                DestinationId = 1,
                CategoryId = 1
            },
            new DestinationCategory
            {
                DestinationId = 1,
                CategoryId = 2
            },
            new DestinationCategory
            {
                DestinationId = 1,
                CategoryId = 3
            },

            // Rotorua
            new DestinationCategory
            {
                DestinationId = 2,
                CategoryId = 1
            },
            new DestinationCategory
            {
                DestinationId = 2,
                CategoryId = 3
            },
            new DestinationCategory
            {
                DestinationId = 2,
                CategoryId = 4
            },

            // Auckland
            new DestinationCategory
            {
                DestinationId = 3,
                CategoryId = 3
            },
            new DestinationCategory
            {
                DestinationId = 3,
                CategoryId = 6
            },

            // Wanaka
            new DestinationCategory
            {
                DestinationId = 4,
                CategoryId = 1
            },
            new DestinationCategory
            {
                DestinationId = 4,
                CategoryId = 2
            },

            // Wellington
            new DestinationCategory
            {
                DestinationId = 5,
                CategoryId = 4
            },
            new DestinationCategory
            {
                DestinationId = 5,
                CategoryId = 6
            }
        );
    }

    private static void SeedAttractionCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttractionCategory>().HasData(

            // Skyline Queenstown
            new AttractionCategory
            {
                AttractionId = 1,
                CategoryId = 2
            },
            new AttractionCategory
            {
                AttractionId = 1,
                CategoryId = 3
            },

            // Ben Lomond
            new AttractionCategory
            {
                AttractionId = 2,
                CategoryId = 1
            },
            new AttractionCategory
            {
                AttractionId = 2,
                CategoryId = 2
            },

            // TSS Earnslaw
            new AttractionCategory
            {
                AttractionId = 3,
                CategoryId = 3
            },

            // Milford Sound
            new AttractionCategory
            {
                AttractionId = 4,
                CategoryId = 1
            },
            new AttractionCategory
            {
                AttractionId = 4,
                CategoryId = 3
            },

            // Hobbiton
            new AttractionCategory
            {
                AttractionId = 5,
                CategoryId = 4
            },
            new AttractionCategory
            {
                AttractionId = 5,
                CategoryId = 3
            },

            // Wai-O-Tapu
            new AttractionCategory
            {
                AttractionId = 6,
                CategoryId = 1
            },
            new AttractionCategory
            {
                AttractionId = 6,
                CategoryId = 3
            },

            // Mount Cook
            new AttractionCategory
            {
                AttractionId = 7,
                CategoryId = 1
            },
            new AttractionCategory
            {
                AttractionId = 7,
                CategoryId = 2
            }
        );
    }


}