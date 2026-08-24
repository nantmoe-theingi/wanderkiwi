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

    public DbSet<Trip> Trips { get; set; } = null!;

    public DbSet<TripDay> TripDays { get; set; } = null!;

    public DbSet<TripStop> TripStops { get; set; } = null!;

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

        modelBuilder.Entity<Attraction>()
            .Property(a => a.OpeningHoursNote)
            .HasMaxLength(500);

        modelBuilder.Entity<Attraction>()
            .Property(a => a.BookingNote)
            .HasMaxLength(500);

        modelBuilder.Entity<Attraction>()
            .Property(a => a.SourceUrl)
            .HasMaxLength(500);

        modelBuilder.Entity<Attraction>()
        .Property(a => a.AvailabilityNote)
        .HasMaxLength(500);

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

        modelBuilder.Entity<Trip>()
            .Property(t => t.Name)
            .HasMaxLength(120);

        modelBuilder.Entity<Trip>()
            .Property(t => t.OwnerId)
            .HasMaxLength(64);

        modelBuilder.Entity<Trip>()
            .HasIndex(t => new { t.OwnerId, t.StartDate });

        modelBuilder.Entity<Trip>()
            .HasMany(t => t.Days)
            .WithOne(d => d.Trip)
            .HasForeignKey(d => d.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TripDay>()
            .HasIndex(d => new { d.TripId, d.DayNumber })
            .IsUnique();

        modelBuilder.Entity<TripDay>()
            .HasMany(d => d.Stops)
            .WithOne(s => s.TripDay)
            .HasForeignKey(s => s.TripDayId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TripStop>()
            .Property(s => s.CustomName)
            .HasMaxLength(120);

        modelBuilder.Entity<TripStop>()
            .Property(s => s.Notes)
            .HasMaxLength(1000);

        modelBuilder.Entity<TripStop>()
            .HasOne(s => s.Attraction)
            .WithMany()
            .HasForeignKey(s => s.AttractionId)
            .OnDelete(DeleteBehavior.Restrict);
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
            new Category { Id = 6, Name = "City" },
            new Category { Id = 7, Name = "Relaxation" },
            new Category { Id = 8, Name = "Wildlife" }
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
                Latitude = -45.0287,
                Longitude = 168.6558,
                Rating = 4.7m,
                ReviewCount = 3447,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; alpine weather can affect gondola operations.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check Skyline’s current operating hours before visit.",
                BookingNote = "Pre-book gondola and activities in peak periods; weather may affect operations.",
                SourceUrl = "https://www.skyline.co.nz/en/queenstown/"
            },
            new Attraction
            {
                Id = 2,
                Name = "TSS Earnslaw Cruise",
                DestinationId = 1,
                Description = "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.",
                ImageUrl = "assets/images/tss-earnslaw-cruise.jpg",
                Latitude = -45.0326,
                Longitude = 168.6575,
                Rating = 4.4m,
                ReviewCount = 80,
                BestTime = "Nov - Mar",
                AvailabilityNote = "Seasonal timetable; services can be affected by lake and weather conditions.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check RealNZ’s current sailing timetable before visit.",
                BookingNote = "Advance booking recommended; arrive at the wharf early and check weather cancellations.",
                SourceUrl = "https://www.realnz.com/en/experiences/cruises/tss-earnslawe/"
            },
            new Attraction
            {
                Id = 3,
                Name = "Shotover Jet",
                DestinationId = 1,
                Description = "High-speed jet boat ride through the Shotover River canyons.",
                ImageUrl = "assets/images/shotover-jet.jpg",
                Latitude = -44.9829,
                Longitude = 168.6702,
                Rating = 4.3m,
                ReviewCount = 269,
                BestTime = "Year round",
                AvailabilityNote = "Operates year round, subject to river and weather conditions.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Shotover Jet’s current departure times before visit.",
                BookingNote = "Advance booking recommended; trips can be delayed or cancelled for weather or river conditions.",
                SourceUrl = "https://www.shotoverjet.com/"
            },
            new Attraction
            {
                Id = 4,
                Name = "Kiwi Park Queenstown",
                DestinationId = 1,
                Description = "Native wildlife conservation park near town centre.",
                ImageUrl = "assets/images/kiwi-park-queenstown.jpg",
                Latitude = -45.0296,
                Longitude = 168.6557,
                Rating = 4.6m,
                ReviewCount = 355,
                BestTime = "Year round",
                AvailabilityNote = "Year round; check current seasonal operating times.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Daily. The official site lists 9:30am–6:30pm with last entry 5:45pm, and a shorter 9:30am–5pm schedule with last entry 4:15pm; confirm the applicable season.",
                BookingNote = "Book online or check the official site before visiting; wildlife encounters and conservation shows run daily.",
                SourceUrl = "https://kiwibird.co.nz/"
            },
            new Attraction
            {
                Id = 5,
                Name = "Queenstown Gardens",
                DestinationId = 1,
                Description = "Lakeside gardens and an easy walking loop near central Queenstown.",
                ImageUrl = "assets/images/queenstown-gardens.jpg",
                Latitude = -45.0336,
                Longitude = 168.6631,
                Rating = 4.4m,
                ReviewCount = 1024,
                BestTime = "Sep - Apr",
                AvailabilityNote = "Open year round; autumn colour is a seasonal highlight.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Public gardens; check Queenstown Lakes District Council information for facility updates.",
                BookingNote = "No booking normally required; use daylight hours and allow for weather.",
                SourceUrl = "https://www.queenstownnz.co.nz/listing/queenstown-gardens/120/"
            },
            new Attraction
            {
                Id = 6,
                Name = "Arrowtown Historic Precinct",
                DestinationId = 1,
                Description = "Historic gold-mining village with heritage streets and riverside walks.",
                ImageUrl = "assets/images/arrowtown-historic-precinct.jpg",
                Latitude = -44.9392,
                Longitude = 168.8313,
                Rating = 4.3m,
                ReviewCount = 864,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; autumn is especially popular.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public streets are accessible daily; check individual shops and museums for their hours.",
                BookingNote = "No booking for the precinct; allow extra time for parking during autumn and events.",
                SourceUrl = "https://www.arrowtown.com/"
            },
            new Attraction
            {
                Id = 7,
                Name = "Gibbston Valley Winery",
                DestinationId = 1,
                Description = "Explore the region's oldest vineyards and New Zealand's largest wine cave.",
                ImageUrl = "assets/images/gibbston-valley-winery.jpg",
                Latitude = -45.0116,
                Longitude = 168.8687,
                Rating = 4.3m,
                ReviewCount = 861,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; vineyard and cellar-door experiences vary seasonally.",
                RecommendedDuration = "4 hours",
                OpeningHoursNote = "Check Gibbston Valley’s current cellar-door and restaurant hours before visit.",
                BookingNote = "Book tastings, tours and dining in advance; appoint a sober driver or use a tour.",
                SourceUrl = "https://www.gibbstonvalley.com/"
            },
            new Attraction
            {
                Id = 8,
                Name = "Onsen Hot Pools",
                DestinationId = 1,
                Description = "Private hot pools overlooking the Shotover River canyon.",
                ImageUrl = "assets/images/onsen-hot-pools.jpg",
                Latitude = -44.984,
                Longitude = 168.6687,
                Rating = 4.5m,
                ReviewCount = 17,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; popular in winter and evenings.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Onsen Hot Pools’ current session times before visit.",
                BookingNote = "Advance booking is essential; outdoor sessions may be weather affected.",
                SourceUrl = "https://www.onsen.co.nz/"
            },
            new Attraction
            {
                Id = 9,
                Name = "Kawarau Bungy Centre",
                DestinationId = 1,
                Description = "The world's first commercial bungy jump site, located at the historic Kawarau Bridge.",
                ImageUrl = "assets/images/kawarau-bungy-centre.jpg",
                Latitude = -45.0134,
                Longitude = 168.8906,
                Rating = 4.4m,
                ReviewCount = 141,
                BestTime = "Year round",
                AvailabilityNote = "Open year round, subject to wind and weather limits.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check AJ Hackett’s current operating hours before visit.",
                BookingNote = "Advance booking recommended; weather can delay or cancel jumps.",
                SourceUrl = "https://www.bungy.co.nz/queenstown/kawarau-bungy-centre/"
            },
            new Attraction
            {
                Id = 10,
                Name = "Coronet Peak",
                DestinationId = 1,
                Description = "A premier ski resort offering spectacular winter sports and summer sightseeing.",
                ImageUrl = "assets/images/coronet-peak.jpg",
                Latitude = -44.9287,
                Longitude = 168.736,
                Rating = 4.5m,
                ReviewCount = 2400,
                BestTime = "Year round",
                AvailabilityNote = "Skiing is seasonal; sightseeing and summer operations vary.",
                RecommendedDuration = "5 hours",
                OpeningHoursNote = "Check NZSki’s current lift, road and operating status before visit.",
                BookingNote = "Book rentals or lessons in advance; alpine road and lift access are weather dependent.",
                SourceUrl = "https://www.coronetpeak.co.nz/"
            },
            new Attraction
            {
                Id = 11,
                Name = "Queenstown Hill Time Walk",
                DestinationId = 1,
                Description = "A rewarding hike through pine forest to panoramic views of the Wakatipu basin.",
                ImageUrl = "assets/images/queenstown-hill-time-walk.jpg",
                Latitude = -45.0295,
                Longitude = 168.6661,
                Rating = 4.8m,
                ReviewCount = 36,
                BestTime = "Year round",
                AvailabilityNote = "Best in dry conditions; snow, ice and strong wind can affect winter access.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public walking track; start in daylight and check DOC/Queenstown weather advice.",
                BookingNote = "No booking; take water, layers and suitable footwear.",
                SourceUrl = "https://www.queenstownnz.co.nz/listing/queenstown-hill-time-walk/146/"
            },
            new Attraction
            {
                Id = 12,
                Name = "Glenorchy Scenic Drive",
                DestinationId = 1,
                Description = "A stunning coastal road trip tracing the edge of Lake Wakatipu to the gateway of Mount Aspiring National Park.",
                ImageUrl = "assets/images/glenorchy-scenic-drive.jpg",
                Latitude = -44.8468,
                Longitude = 168.3846,
                Rating = 4.6m,
                ReviewCount = 1187,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; winter snow/ice and storm conditions may affect roads.",
                RecommendedDuration = "6 hours",
                OpeningHoursNote = "Public road; check NZTA and weather conditions before departure.",
                BookingNote = "No booking; fuel up, allow extra driving time, and do not rely on the route during road closures.",
                SourceUrl = "https://www.queenstownnz.co.nz/things-to-do/scenic-drives/glenorchy-road/"
            },
            new Attraction
            {
                Id = 13,
                Name = "Milford Sound day trip",
                DestinationId = 8,
                Description = "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.",
                ImageUrl = "assets/images/milford-sound-day-trip.jpg",
                Latitude = -44.6715,
                Longitude = 167.9255,
                Rating = 4.5m,
                ReviewCount = 415,
                BestTime = "Nov - Mar",
                AvailabilityNote = "Year round; road, avalanche and severe-weather disruptions are possible.",
                RecommendedDuration = "10 hours",
                OpeningHoursNote = "Check operator timetable and NZTA road conditions before visit.",
                BookingNote = "Advance booking strongly recommended; carry food/water and expect weather-related changes.",
                SourceUrl = "https://www.realnz.com/en/experiences/cruises/milford-sound-cruises/"
            },
            new Attraction
            {
                Id = 14,
                Name = "Lake Wakatipu waterfront",
                DestinationId = 1,
                Description = "A vibrant promenade perfect for a scenic stroll, lakeside dining, or watching the sunset.",
                ImageUrl = "assets/images/lake-wakatipu-waterfront.jpg",
                Latitude = -45.0332,
                Longitude = 168.6599,
                Rating = 4.6m,
                ReviewCount = 1469,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; best enjoyed in settled weather and daylight.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Public waterfront; no set hours.",
                BookingNote = "No booking; check weather and water-safety advice before lake activities.",
                SourceUrl = "https://www.queenstownnz.co.nz/listing/queenstown-bay/605/"
            },
            new Attraction
            {
                Id = 15,
                Name = "Bobs Cove Track",
                DestinationId = 1,
                Description = "An easy, picturesque walk through native bush to a secluded cove on Lake Wakatipu.",
                ImageUrl = "assets/images/bobs-cove-track.jpg",
                Latitude = -45.0682,
                Longitude = 168.5398,
                Rating = 4.9m,
                ReviewCount = 682,
                BestTime = "Dec - Feb",
                AvailabilityNote = "Open year round; track conditions can be muddy, icy or affected by storms.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public walking track; check DOC conditions before visit.",
                BookingNote = "No booking; use the car park trailhead and carry weather-appropriate gear.",
                SourceUrl = "https://www.doc.govt.nz/parks-and-recreation/places-to-go/otago/places/queenstown-area/things-to-do/tracks/bobs-cove-track/"
            },
            new Attraction
            {
                Id = 16,
                Name = "Christchurch Botanic Gardens",
                DestinationId = 6,
                Description = "Historic riverside gardens beside Hagley Park.",
                ImageUrl = "assets/images/christchurch-botanic-gardens.jpg",
                Latitude = -43.5306,
                Longitude = 172.6262,
                Rating = 4.8m,
                ReviewCount = 957,
                BestTime = "Sep - Apr",
                AvailabilityNote = "Open year round; spring and summer are especially colourful.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Christchurch City Council’s current garden and visitor-centre hours before visit.",
                BookingNote = "No booking for gardens; weather and events may affect some areas.",
                SourceUrl = "https://ccc.govt.nz/parks-and-gardens/christchurch-botanic-gardens"
            },
            new Attraction
            {
                Id = 17,
                Name = "International Antarctic Centre",
                DestinationId = 6,
                Description = "Interactive Antarctic visitor experience beside Christchurch Airport.",
                ImageUrl = "assets/images/international-antarctic-centre.jpg",
                Latitude = -43.4862,
                Longitude = 172.5488,
                Rating = 4.5m,
                ReviewCount = 176,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor attraction.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check the International Antarctic Centre’s current daily hours before visit.",
                BookingNote = "Advance booking recommended in peak periods; allow time for timed experiences.",
                SourceUrl = "https://www.iceberg.co.nz/"
            },
            new Attraction
            {
                Id = 18,
                Name = "Christchurch Gondola",
                DestinationId = 6,
                Description = "Gondola ride with views over Lyttelton Harbour and the Canterbury Plains.",
                ImageUrl = "assets/images/christchurch-gondola.jpg",
                Latitude = -43.5828,
                Longitude = 172.7119,
                Rating = 4.4m,
                ReviewCount = 1075,
                BestTime = "Year round",
                AvailabilityNote = "Open year round, subject to wind and weather.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Christchurch Gondola’s current operating hours before visit.",
                BookingNote = "Book ahead in peak periods; gondola operations can be affected by high winds.",
                SourceUrl = "https://www.christchurchgondola.co.nz/"
            },
            new Attraction
            {
                Id = 19,
                Name = "Quake City",
                DestinationId = 6,
                Description = "Museum telling the story of the Canterbury earthquakes and recovery.",
                ImageUrl = "assets/images/quake-city.jpg",
                Latitude = -43.5284,
                Longitude = 172.6322,
                Rating = 4.6m,
                ReviewCount = 1438,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor attraction.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Quake City’s current hours before visit.",
                BookingNote = "Booking recommended for groups; allow time for nearby central-city parking.",
                SourceUrl = "https://www.quakecity.co.nz/"
            },
            new Attraction
            {
                Id = 20,
                Name = "Air Force Museum of New Zealand",
                DestinationId = 6,
                Description = "Discover the history of New Zealand military aviation through engaging exhibits and historic aircraft.",
                ImageUrl = "assets/images/air-force-museum-of-new-zealand.jpg",
                Latitude = -43.5483,
                Longitude = 172.546,
                Rating = 4.3m,
                ReviewCount = 630,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor/outdoor exhibits.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check the museum’s current opening hours before visit.",
                BookingNote = "General admission is usually free; book guided tours or special activities if required.",
                SourceUrl = "https://www.airforcemuseum.co.nz/"
            },
            new Attraction
            {
                Id = 21,
                Name = "Orana Wildlife Park",
                DestinationId = 6,
                Description = "New Zealand's only open-range zoo, offering unique up-close animal encounters.",
                ImageUrl = "assets/images/orana-wildlife-park.jpg",
                Latitude = -43.4682,
                Longitude = 172.4636,
                Rating = 4.2m,
                ReviewCount = 314,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; outdoor animal experiences vary with weather and animal welfare needs.",
                RecommendedDuration = "5 hours",
                OpeningHoursNote = "Check Orana’s current daily hours before visit.",
                BookingNote = "Advance booking recommended in school holidays; check encounter times and weather advice.",
                SourceUrl = "https://www.oranawildlifepark.co.nz/"
            },
            new Attraction
            {
                Id = 22,
                Name = "Willowbank Wildlife Reserve",
                DestinationId = 6,
                Description = "A wildlife park dedicated to New Zealand's native species and Māori cultural experiences.",
                ImageUrl = "assets/images/willowbank-wildlife-reserve.jpg",
                Latitude = -43.4678,
                Longitude = 172.5937,
                Rating = 4.5m,
                ReviewCount = 513,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; night tours and animal encounters may be seasonal.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check Willowbank’s current visitor hours before visit.",
                BookingNote = "Book kiwi/night tours and encounters in advance.",
                SourceUrl = "https://www.willowbank.co.nz/"
            },
            new Attraction
            {
                Id = 23,
                Name = "Akaroa Harbour day trip",
                DestinationId = 6,
                Description = "Banks Peninsula harbour town, suitable as a full-day excursion from Christchurch.",
                ImageUrl = "assets/images/akaroa-harbour-day-trip.jpg",
                Latitude = -43.8058,
                Longitude = 172.9675,
                Rating = 4.6m,
                ReviewCount = 1144,
                BestTime = "Sep - Apr",
                AvailabilityNote = "Year round; harbour cruises and wildlife trips are weather dependent.",
                RecommendedDuration = "8 hours",
                OpeningHoursNote = "Check the chosen operator’s timetable before visit.",
                BookingNote = "Book harbour cruises in advance; allow for the drive and possible weather cancellations.",
                SourceUrl = "https://www.christchurchnz.com/explore/akaroa"
            },
            new Attraction
            {
                Id = 24,
                Name = "Lyttelton Harbour",
                DestinationId = 6,
                Description = "A historic port town set in a collapsed volcanic crater, featuring quirky shops and stunning views.",
                ImageUrl = "assets/images/lyttelton-harbour.jpg",
                Latitude = -43.6015,
                Longitude = 172.7212,
                Rating = 4.9m,
                ReviewCount = 519,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; market and ferry activity varies by day.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public harbour area; check individual businesses and event schedules.",
                BookingNote = "No booking for the waterfront; check parking and cruise-ship/event impacts.",
                SourceUrl = "https://www.christchurchnz.com/explore/lyttelton"
            },
            new Attraction
            {
                Id = 25,
                Name = "Sumner Beach and Cave Rock",
                DestinationId = 6,
                Description = "A popular coastal suburb known for its relaxed surf culture and iconic volcanic rock formations.",
                ImageUrl = "assets/images/sumner-beach-and-cave-rock.jpg",
                Latitude = -43.567,
                Longitude = 172.7584,
                Rating = 4.4m,
                ReviewCount = 1377,
                BestTime = "Dec - Feb",
                AvailabilityNote = "Open year round; best in settled conditions.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public beach; no set hours.",
                BookingNote = "No booking; check surf, tide and weather warnings before swimming or rock access.",
                SourceUrl = "https://ccc.govt.nz/parks-and-gardens/explore-parks/coastal-parks/sumner-beach"
            },
            new Attraction
            {
                Id = 26,
                Name = "Punting on the Avon",
                DestinationId = 6,
                Description = "A tranquil and iconic Christchurch experience gliding along the Avon River in a flat-bottomed boat.",
                ImageUrl = "assets/images/punting-on-the-avon.jpg",
                Latitude = -43.5332,
                Longitude = 172.6277,
                Rating = 4.6m,
                ReviewCount = 497,
                BestTime = "Year round",
                AvailabilityNote = "Operates seasonally and may be weather dependent.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Punting on the Avon’s current departure times before visit.",
                BookingNote = "Advance booking recommended; rain, wind or river conditions may affect service.",
                SourceUrl = "https://www.puntingontheavon.co.nz/"
            },
            new Attraction
            {
                Id = 27,
                Name = "Riverside Market",
                DestinationId = 6,
                Description = "A bustling indoor market offering diverse street food, fresh local produce, and boutique stalls.",
                ImageUrl = "assets/images/riverside-market.jpg",
                Latitude = -43.5323,
                Longitude = 172.6324,
                Rating = 4.6m,
                ReviewCount = 890,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; trading hours vary by stall and day.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Riverside Market’s current opening hours before visit.",
                BookingNote = "No booking for market browsing; book restaurants separately if required.",
                SourceUrl = "https://riverside.nz/"
            },
            new Attraction
            {
                Id = 28,
                Name = "Port Hills",
                DestinationId = 6,
                Description = "A rugged volcanic range offering extensive walking and biking trails with panoramic city and harbour views.",
                ImageUrl = "assets/images/port-hills.jpg",
                Latitude = -43.6338,
                Longitude = 172.6223,
                Rating = 4.5m,
                ReviewCount = 949,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; exposed tracks are best in dry, low-wind conditions.",
                RecommendedDuration = "4 hours",
                OpeningHoursNote = "Public tracks; check Christchurch City Council and weather/fire restrictions before visit.",
                BookingNote = "No booking; carry water, sun protection and layers; avoid exposed routes in severe weather.",
                SourceUrl = "https://ccc.govt.nz/parks-and-gardens/explore-parks/port-hills"
            },
            new Attraction
            {
                Id = 29,
                Name = "Canterbury Museum",
                DestinationId = 6,
                Description = "A cultural heritage museum showcasing the rich natural and human history of the Canterbury region.",
                ImageUrl = "assets/images/canterbury-museum.jpg",
                Latitude = -43.5312,
                Longitude = 172.6268,
                Rating = 4.5m,
                ReviewCount = 305,
                BestTime = "Year round",
                AvailabilityNote = "Confirm reopening and temporary exhibition arrangements before planning.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check the Canterbury Museum website before visit; redevelopment may affect access.",
                BookingNote = "No booking assumption; verify venue location, ticketing and opening information first.",
                SourceUrl = "https://canterburymuseum.com/"
            },
            new Attraction
            {
                Id = 30,
                Name = "The Arts Centre",
                DestinationId = 6,
                Description = "A vibrant hub for arts, culture, and education set within stunning restored Gothic Revival buildings.",
                ImageUrl = "assets/images/the-arts-centre.jpg",
                Latitude = -43.5313,
                Longitude = 172.6284,
                Rating = 4.7m,
                ReviewCount = 744,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; galleries, shops and events have separate schedules.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check The Arts Centre’s current building and venue hours before visit.",
                BookingNote = "No booking to explore public areas; book performances, tours or workshops separately.",
                SourceUrl = "https://artscentre.org.nz/"
            },
            new Attraction
            {
                Id = 31,
                Name = "Sky Tower",
                DestinationId = 3,
                Description = "Observation tower with panoramic views across Auckland and the Hauraki Gulf.",
                ImageUrl = "assets/images/sky-tower.jpg",
                Latitude = -36.8485,
                Longitude = 174.7622,
                Rating = 4.5m,
                ReviewCount = 535,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; outdoor SkyWalk/SkyJump is weather dependent.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check SkyCity’s current attraction hours before visit.",
                BookingNote = "Pre-book SkyWalk/SkyJump and peak observation visits; outdoor activities can be weather cancelled.",
                SourceUrl = "https://skycityauckland.co.nz/sky-tower/"
            },
            new Attraction
            {
                Id = 32,
                Name = "Auckland Museum",
                DestinationId = 3,
                Description = "Museum of natural history and Aotearoa New Zealand stories in the Domain.",
                ImageUrl = "assets/images/auckland-museum.jpg",
                Latitude = -36.8606,
                Longitude = 174.7778,
                Rating = 4.5m,
                ReviewCount = 1112,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor museum and outdoor Domain.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check Auckland Museum’s current opening hours before visit.",
                BookingNote = "Book paid exhibitions or events in advance; allow time for parking or public transport.",
                SourceUrl = "https://www.aucklandmuseum.com/"
            },
            new Attraction
            {
                Id = 33,
                Name = "Auckland Zoo",
                DestinationId = 3,
                Description = "Conservation-focused zoo in Western Springs.",
                ImageUrl = "assets/images/auckland-zoo.jpg",
                Latitude = -36.8631,
                Longitude = 174.7176,
                Rating = 4.5m,
                ReviewCount = 981,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; outdoor areas and encounters are weather dependent.",
                RecommendedDuration = "4 hours",
                OpeningHoursNote = "Check Auckland Zoo’s current daily hours before visit.",
                BookingNote = "Advance booking recommended in peak periods; check animal encounter requirements.",
                SourceUrl = "https://www.aucklandzoo.co.nz/"
            },
            new Attraction
            {
                Id = 34,
                Name = "Waiheke Island day trip",
                DestinationId = 3,
                Description = "Hauraki Gulf island for beaches, art and vineyard visits; allow a full day.",
                ImageUrl = "assets/images/waiheke-island-day-trip.jpg",
                Latitude = -36.843,
                Longitude = 174.767,
                Rating = 4.2m,
                ReviewCount = 757,
                BestTime = "Nov - Mar",
                AvailabilityNote = "Open year round; ferry sailings and outdoor activities depend on weather.",
                RecommendedDuration = "8 hours",
                OpeningHoursNote = "Check Fullers360 ferry timetable and chosen winery/attraction hours before visit.",
                BookingNote = "Book ferries, tours and popular wineries in advance; allow for weather or sea-condition disruptions.",
                SourceUrl = "https://www.fullers.co.nz/destinations-and-experiences/waiheke-island/"
            },
            new Attraction
            {
                Id = 35,
                Name = "Rangitoto Island day trip",
                DestinationId = 3,
                Description = "Volcanic island day trip with a summit walk and harbour views.",
                ImageUrl = "assets/images/rangitoto-island-day-trip.jpg",
                Latitude = -36.843,
                Longitude = 174.767,
                Rating = 4.5m,
                ReviewCount = 1154,
                BestTime = "Nov - Mar",
                AvailabilityNote = "Open year round; ferry service and summit track conditions are weather dependent.",
                RecommendedDuration = "7 hours",
                OpeningHoursNote = "Check Fullers360 timetable and DOC island advice before visit.",
                BookingNote = "Pre-book ferry; take food, water and sun protection—there are no shops on Rangitoto.",
                SourceUrl = "https://www.aucklandnz.com/explore/rangitoto-island"
            },
            new Attraction
            {
                Id = 36,
                Name = "SEA LIFE Kelly Tarlton’s Aquarium",
                DestinationId = 3,
                Description = "An iconic underwater attraction featuring penguin colonies, shark tunnels, and marine rescue exhibits.",
                ImageUrl = "assets/images/sea-life-kelly-tarltons-aquarium.jpg",
                Latitude = -36.8475,
                Longitude = 174.8183,
                Rating = 4.3m,
                ReviewCount = 425,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor attraction.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check SEA LIFE Kelly Tarlton’s current hours before visit.",
                BookingNote = "Advance booking recommended in weekends and school holidays.",
                SourceUrl = "https://www.visitsealife.com/auckland/"
            },
            new Attraction
            {
                Id = 37,
                Name = "Museum of Transport and Technology",
                DestinationId = 3,
                Description = "An interactive museum exploring the history and future of New Zealand's transport and technology.",
                ImageUrl = "assets/images/museum-of-transport-and-technology.jpg",
                Latitude = -36.8665,
                Longitude = 174.7179,
                Rating = 4.6m,
                ReviewCount = 1277,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor/outdoor exhibits.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Check MOTAT’s current opening hours before visit.",
                BookingNote = "Book special events and school-holiday activities in advance where offered.",
                SourceUrl = "https://www.motat.nz/"
            },
            new Attraction
            {
                Id = 38,
                Name = "New Zealand Maritime Museum",
                DestinationId = 3,
                Description = "Discover the stories of the people and ships that shaped New Zealand's seafaring history.",
                ImageUrl = "assets/images/new-zealand-maritime-museum.jpg",
                Latitude = -36.8419,
                Longitude = 174.7634,
                Rating = 4.8m,
                ReviewCount = 1357,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; harbour sailing experiences are weather dependent.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check the Maritime Museum’s current hours before visit.",
                BookingNote = "Book heritage sailings in advance; sailings can be weather affected.",
                SourceUrl = "https://www.maritimemuseum.co.nz/"
            },
            new Attraction
            {
                Id = 39,
                Name = "Auckland Art Gallery Toi o Tāmaki",
                DestinationId = 3,
                Description = "New Zealand's largest visual arts institution, housing an extensive collection of national and international art.",
                ImageUrl = "assets/images/auckland-art-gallery-toi-o-tamaki.jpg",
                Latitude = -36.8502,
                Longitude = 174.7661,
                Rating = 4.4m,
                ReviewCount = 989,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; gallery programme and special exhibitions vary.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Auckland Art Gallery’s current opening hours before visit.",
                BookingNote = "Book ticketed exhibitions or events in advance when required.",
                SourceUrl = "https://www.aucklandartgallery.com/"
            },
            new Attraction
            {
                Id = 40,
                Name = "Maungakiekie / One Tree Hill",
                DestinationId = 3,
                Description = "A significant volcanic peak and historic park offering 360-degree views of Auckland.",
                ImageUrl = "assets/images/maungakiekie-one-tree-hill.jpg",
                Latitude = -36.8967,
                Longitude = 174.7765,
                Rating = 4.3m,
                ReviewCount = 1426,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; exposed summit is best in settled weather.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public park; check Cornwall Park and local weather information before visit.",
                BookingNote = "No booking; use daylight hours and allow for a walk from parking.",
                SourceUrl = "https://cornwallpark.co.nz/"
            },
            new Attraction
            {
                Id = 41,
                Name = "Devonport waterfront and North Head",
                DestinationId = 3,
                Description = "A charming historic village paired with a coastal reserve known for its military tunnels and harbour views.",
                ImageUrl = "assets/images/devonport-waterfront-and-north-head.jpg",
                Latitude = -36.8329,
                Longitude = 174.7961,
                Rating = 4.5m,
                ReviewCount = 1480,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; ferry and outdoor walk conditions are weather dependent.",
                RecommendedDuration = "4 hours",
                OpeningHoursNote = "Check Fullers360 timetable and DOC North Head information before visit.",
                BookingNote = "No booking for North Head; ferry services can be weather affected and tunnels may have access limits.",
                SourceUrl = "https://www.doc.govt.nz/parks-and-recreation/places-to-go/auckland/places/north-head-historic-reserve/"
            },
            new Attraction
            {
                Id = 42,
                Name = "Tiritiri Matangi Island day trip",
                DestinationId = 3,
                Description = "A renowned open sanctuary for native birdlife and conservation, accessible by a scenic ferry ride.",
                ImageUrl = "assets/images/tiritiri-matangi-island-day-trip.jpg",
                Latitude = -36.843,
                Longitude = 174.767,
                Rating = 4.6m,
                ReviewCount = 248,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; scheduled ferry access and outdoor walking are weather dependent.",
                RecommendedDuration = "8 hours",
                OpeningHoursNote = "Check Explore Group ferry timetable and DOC visitor information before visit.",
                BookingNote = "Book ferry well ahead; take food, water and walking gear—check weather cancellations.",
                SourceUrl = "https://www.doc.govt.nz/parks-and-recreation/places-to-go/auckland/places/tiritiri-matangi-open-sanctuary/"
            },
            new Attraction
            {
                Id = 43,
                Name = "Mission Bay and Tāmaki Drive",
                DestinationId = 3,
                Description = "A picturesque coastal route leading to a vibrant seaside suburb with a beautiful sandy beach and eateries.",
                ImageUrl = "assets/images/mission-bay-and-tamaki-drive.jpg",
                Latitude = -36.848,
                Longitude = 174.8315,
                Rating = 4.7m,
                ReviewCount = 441,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; best in settled weather and daylight.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public waterfront; no set hours.",
                BookingNote = "No booking; check swim, weather and traffic conditions before visit.",
                SourceUrl = "https://www.aucklandnz.com/explore/mission-bay"
            },
            new Attraction
            {
                Id = 44,
                Name = "Auckland Domain",
                DestinationId = 3,
                Description = "Auckland's oldest park, featuring expansive green spaces, walking tracks, and the historic Wintergardens.",
                ImageUrl = "assets/images/auckland-domain.jpg",
                Latitude = -36.8596,
                Longitude = 174.7758,
                Rating = 4.5m,
                ReviewCount = 437,
                BestTime = "Sep - Apr",
                AvailabilityNote = "Open year round; events may limit vehicle access or parking.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Public park; check Auckland Council information for event impacts.",
                BookingNote = "No booking; use daylight hours and combine with Auckland Museum if suitable.",
                SourceUrl = "https://www.aucklandcouncil.govt.nz/parks-recreation/get-outdoors/find-a-park/Pages/park-details.aspx?parkID=1"
            },
            new Attraction
            {
                Id = 45,
                Name = "Wētā Workshop Unleashed",
                DestinationId = 3,
                Description = "An immersive and wildly imaginative experience exploring the worlds of horror, sci-fi, and fantasy film-making.",
                ImageUrl = "assets/images/weta-workshop-unleashed.jpg",
                Latitude = -36.8489,
                Longitude = 174.7621,
                Rating = 4.6m,
                ReviewCount = 1343,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; indoor attraction.",
                RecommendedDuration = "2 hours",
                OpeningHoursNote = "Check Wētā Workshop Unleashed’s current session times before visit.",
                BookingNote = "Advance booking recommended; arrive before your timed session.",
                SourceUrl = "https://tours.wetaworkshop.com/auckland/"
            },
            new Attraction
            {
                Id = 46,
                Name = "Te Anau Glowworm Caves",
                DestinationId = 8,
                Description = "A magical underground experience starting with a scenic lake cruise to a hidden limestone cave illuminated by thousands of glowworms.",
                ImageUrl = "assets/images/te-anau-glowworm-caves.jpg",
                Latitude = -45.4165,
                Longitude = 167.7118,
                Rating = 4.5m,
                ReviewCount = 850,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; daily boat departures across Lake Te Anau.",
                RecommendedDuration = "2.25 hours",
                OpeningHoursNote = "Open 7 days, daily departures. Check official website before visit.",
                BookingNote = "Advance booking recommended; check-in 30 minutes prior to departure; requires bending/walking in caves.",
                SourceUrl = "https://www.realnz.com/en/experiences/glowworm-caves/te-anau-glowworm-caves/"
            },
            new Attraction
            {
                Id = 47,
                Name = "Kepler Track Day Walk",
                DestinationId = 8,
                Description = "An accessible section of the famous Kepler Great Walk, leading through ancient beech forests along the lake shore.",
                ImageUrl = "assets/images/kepler-track-day-walk.jpg",
                Latitude = -45.4398,
                Longitude = 167.6830,
                Rating = 4.8m,
                ReviewCount = 620,
                BestTime = "Sep - Apr",
                AvailabilityNote = "Great Walks season runs late October to April; day walks accessible year round in good weather.",
                RecommendedDuration = "3 hours",
                OpeningHoursNote = "Public walking track; accessible during daylight hours.",
                BookingNote = "No booking required for day walks; check DOC weather and track alerts before setting out.",
                SourceUrl = "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/tracks/kepler-track/"
            },
            new Attraction
            {
                Id = 48,
                Name = "Doubtful Sound Wilderness Cruise",
                DestinationId = 8,
                Description = "A tranquil and remote wilderness cruise through a deep, pristine fiord known for its serene waters and native wildlife.",
                ImageUrl = "assets/images/doubtful-sound-wilderness-cruise.jpg",
                Latitude = -45.5636,
                Longitude = 167.6163,
                Rating = 4.7m,
                ReviewCount = 540,
                BestTime = "Nov - Mar",
                AvailabilityNote = "Operates year round; full-day excursion departing from Manapouri.",
                RecommendedDuration = "7 hours",
                OpeningHoursNote = "Check official website before visit for seasonal departure times.",
                BookingNote = "Advance booking essential; departures leave from Pearl Harbour in Manapouri.",
                SourceUrl = "https://www.realnz.com/en/experiences/cruises/doubtful-sound-wilderness-cruises/"
            },
            new Attraction
            {
                Id = 49,
                Name = "Te Anau Bird Sanctuary",
                DestinationId = 8,
                Description = "A lakeside conservation haven providing a rare chance to see endangered native birds like the Takahē up close.",
                ImageUrl = "assets/images/te-anau-bird-sanctuary.jpg",
                Latitude = -45.4262,
                Longitude = 167.7051,
                Rating = 4.6m,
                ReviewCount = 310,
                BestTime = "Year round",
                AvailabilityNote = "Open year round from dawn to dusk.",
                RecommendedDuration = "1 hours",
                OpeningHoursNote = "Open daily from dawn to dusk.",
                BookingNote = "Free entry (gold coin donation appreciated); guided tour feeds can be booked.",
                SourceUrl = "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/te-anau-bird-sanctuary/"
            },
            new Attraction
            {
                Id = 50,
                Name = "Fiordland Cinema",
                DestinationId = 8,
                Description = "A boutique cinema showcasing the custom-shot documentary 'Ata Whenua - Shadowland', capturing Fiordland's wild landscapes.",
                ImageUrl = "assets/images/fiordland-cinema.jpg",
                Latitude = -45.4150,
                Longitude = 167.7135,
                Rating = 4.8m,
                ReviewCount = 420,
                BestTime = "Year round",
                AvailabilityNote = "Open year round; an excellent indoor activity.",
                RecommendedDuration = "1 hours",
                OpeningHoursNote = "Check official website for current screening showtimes.",
                BookingNote = "Advance booking recommended for popular evening screenings.",
                SourceUrl = "https://www.fiordlandcinema.co.nz/"
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
            },
            // Te Anau (DestinationId = 8)
            new DestinationCategory
            {
                DestinationId = 8,
                CategoryId = 1 // Nature
            },
            new DestinationCategory
            {
                DestinationId = 8,
                CategoryId = 3 // Sightseeing
            },
            new DestinationCategory
            {
                DestinationId = 8,
                CategoryId = 8 // Wildlife
            }
        );
    }

    private static void SeedAttractionCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttractionCategory>().HasData(
            // Skyline Queenstown (Id 1)
            new AttractionCategory { AttractionId = 1, CategoryId = 2 }, // Adventure
            new AttractionCategory { AttractionId = 1, CategoryId = 3 }, // Sightseeing

            // TSS Earnslaw Cruise (Id 2)
            new AttractionCategory { AttractionId = 2, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 2, CategoryId = 7 }, // Relaxation

            // Shotover Jet (Id 3)
            new AttractionCategory { AttractionId = 3, CategoryId = 2 }, // Adventure
            new AttractionCategory { AttractionId = 3, CategoryId = 3 }, // Sightseeing

            // Kiwi Park Queenstown (Id 4)
            new AttractionCategory { AttractionId = 4, CategoryId = 8 }, // Wildlife
            new AttractionCategory { AttractionId = 4, CategoryId = 1 }, // Nature

            // Queenstown Gardens (Id 5)
            new AttractionCategory { AttractionId = 5, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 5, CategoryId = 7 }, // Relaxation

            // Arrowtown Historic Precinct (Id 6)
            new AttractionCategory { AttractionId = 6, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 6, CategoryId = 3 }, // Sightseeing

            // Gibbston Valley Winery (Id 7)
            new AttractionCategory { AttractionId = 7, CategoryId = 5 }, // Food & Wine
            new AttractionCategory { AttractionId = 7, CategoryId = 7 }, // Relaxation

            // Onsen Hot Pools (Id 8)
            new AttractionCategory { AttractionId = 8, CategoryId = 7 }, // Relaxation
            new AttractionCategory { AttractionId = 8, CategoryId = 1 }, // Nature

            // Kawarau Bungy Centre (Id 9)
            new AttractionCategory { AttractionId = 9, CategoryId = 2 }, // Adventure

            // Coronet Peak (Id 10)
            new AttractionCategory { AttractionId = 10, CategoryId = 2 }, // Adventure
            new AttractionCategory { AttractionId = 10, CategoryId = 1 }, // Nature

            // Queenstown Hill Time Walk (Id 11)
            new AttractionCategory { AttractionId = 11, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 11, CategoryId = 2 }, // Adventure

            // Glenorchy Scenic Drive (Id 12)
            new AttractionCategory { AttractionId = 12, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 12, CategoryId = 1 }, // Nature

            // Milford Sound day trip (Id 13)
            new AttractionCategory { AttractionId = 13, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 13, CategoryId = 3 }, // Sightseeing

            // Lake Wakatipu waterfront (Id 14)
            new AttractionCategory { AttractionId = 14, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 14, CategoryId = 7 }, // Relaxation

            // Bobs Cove Track (Id 15)
            new AttractionCategory { AttractionId = 15, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 15, CategoryId = 7 }, // Relaxation

            // Christchurch Botanic Gardens (Id 16)
            new AttractionCategory { AttractionId = 16, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 16, CategoryId = 7 }, // Relaxation

            // International Antarctic Centre (Id 17)
            new AttractionCategory { AttractionId = 17, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 17, CategoryId = 4 }, // Culture

            // Christchurch Gondola (Id 18)
            new AttractionCategory { AttractionId = 18, CategoryId = 3 }, // Sightseeing

            // Quake City (Id 19)
            new AttractionCategory { AttractionId = 19, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 19, CategoryId = 3 }, // Sightseeing

            // Air Force Museum of New Zealand (Id 20)
            new AttractionCategory { AttractionId = 20, CategoryId = 4 }, // Culture

            // Orana Wildlife Park (Id 21)
            new AttractionCategory { AttractionId = 21, CategoryId = 8 }, // Wildlife
            new AttractionCategory { AttractionId = 21, CategoryId = 1 }, // Nature

            // Willowbank Wildlife Reserve (Id 22)
            new AttractionCategory { AttractionId = 22, CategoryId = 8 }, // Wildlife
            new AttractionCategory { AttractionId = 22, CategoryId = 4 }, // Culture

            // Akaroa Harbour day trip (Id 23)
            new AttractionCategory { AttractionId = 23, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 23, CategoryId = 8 }, // Wildlife

            // Lyttelton Harbour (Id 24)
            new AttractionCategory { AttractionId = 24, CategoryId = 6 }, // City
            new AttractionCategory { AttractionId = 24, CategoryId = 3 }, // Sightseeing

            // Sumner Beach and Cave Rock (Id 25)
            new AttractionCategory { AttractionId = 25, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 25, CategoryId = 7 }, // Relaxation

            // Punting on the Avon (Id 26)
            new AttractionCategory { AttractionId = 26, CategoryId = 7 }, // Relaxation
            new AttractionCategory { AttractionId = 26, CategoryId = 3 }, // Sightseeing

            // Riverside Market (Id 27)
            new AttractionCategory { AttractionId = 27, CategoryId = 5 }, // Food & Wine
            new AttractionCategory { AttractionId = 27, CategoryId = 6 }, // City

            // Port Hills (Id 28)
            new AttractionCategory { AttractionId = 28, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 28, CategoryId = 2 }, // Adventure

            // Canterbury Museum (Id 29)
            new AttractionCategory { AttractionId = 29, CategoryId = 4 }, // Culture

            // The Arts Centre (Id 30)
            new AttractionCategory { AttractionId = 30, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 30, CategoryId = 6 }, // City

            // Sky Tower (Id 31)
            new AttractionCategory { AttractionId = 31, CategoryId = 6 }, // City
            new AttractionCategory { AttractionId = 31, CategoryId = 3 }, // Sightseeing

            // Auckland Museum (Id 32)
            new AttractionCategory { AttractionId = 32, CategoryId = 4 }, // Culture

            // Auckland Zoo (Id 33)
            new AttractionCategory { AttractionId = 33, CategoryId = 8 }, // Wildlife

            // Waiheke Island day trip (Id 34)
            new AttractionCategory { AttractionId = 34, CategoryId = 5 }, // Food & Wine
            new AttractionCategory { AttractionId = 34, CategoryId = 1 }, // Nature

            // Rangitoto Island day trip (Id 35)
            new AttractionCategory { AttractionId = 35, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 35, CategoryId = 2 }, // Adventure

            // SEA LIFE Kelly Tarlton’s Aquarium (Id 36)
            new AttractionCategory { AttractionId = 36, CategoryId = 8 }, // Wildlife
            new AttractionCategory { AttractionId = 36, CategoryId = 3 }, // Sightseeing

            // Museum of Transport and Technology (Id 37)
            new AttractionCategory { AttractionId = 37, CategoryId = 4 }, // Culture

            // New Zealand Maritime Museum (Id 38)
            new AttractionCategory { AttractionId = 38, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 38, CategoryId = 3 }, // Sightseeing

            // Auckland Art Gallery Toi o Tāmaki (Id 39)
            new AttractionCategory { AttractionId = 39, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 39, CategoryId = 6 }, // City

            // Maungakiekie / One Tree Hill (Id 40)
            new AttractionCategory { AttractionId = 40, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 40, CategoryId = 4 }, // Culture

            // Devonport waterfront and North Head (Id 41)
            new AttractionCategory { AttractionId = 41, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 41, CategoryId = 3 }, // Sightseeing

            // Tiritiri Matangi Island day trip (Id 42)
            new AttractionCategory { AttractionId = 42, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 42, CategoryId = 8 }, // Wildlife

            // Mission Bay and Tāmaki Drive (Id 43)
            new AttractionCategory { AttractionId = 43, CategoryId = 7 }, // Relaxation
            new AttractionCategory { AttractionId = 43, CategoryId = 6 }, // City

            // Auckland Domain (Id 44)
            new AttractionCategory { AttractionId = 44, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 44, CategoryId = 6 }, // City

            // Wētā Workshop Unleashed (Id 45)
            new AttractionCategory { AttractionId = 45, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 45, CategoryId = 3 }, // Sightseeing

            // Te Anau Glowworm Caves (Id 46)
            new AttractionCategory { AttractionId = 46, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 46, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 46, CategoryId = 2 }, // Adventure

            // Kepler Track Day Walk (Id 47)
            new AttractionCategory { AttractionId = 47, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 47, CategoryId = 2 }, // Adventure

            // Doubtful Sound Wilderness Cruise (Id 48)
            new AttractionCategory { AttractionId = 48, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 48, CategoryId = 3 }, // Sightseeing
            new AttractionCategory { AttractionId = 48, CategoryId = 8 }, // Wildlife

            // Te Anau Bird Sanctuary (Id 49)
            new AttractionCategory { AttractionId = 49, CategoryId = 1 }, // Nature
            new AttractionCategory { AttractionId = 49, CategoryId = 8 }, // Wildlife

            // Fiordland Cinema (Id 50)
            new AttractionCategory { AttractionId = 50, CategoryId = 4 }, // Culture
            new AttractionCategory { AttractionId = 50, CategoryId = 7 }  // Relaxation
        );
    }
}
