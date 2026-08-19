using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(WanderKiwiDbContext context)
    {
        // Ensure the database is created
        context.Database.EnsureCreated();

        // Look for any attractions already in the database
        if (context.Attractions.Any())
        {
            return;   // Database has been seeded
        }

        var attractions = new Attraction[]
        {
                new Attraction
                {
                    Name = "Milford Sound",
                    Description = "Famous fiord in Fiordland National Park known for towering peaks and cascading waterfalls.",
                    Region = "Southland",
                    Latitude = -44.6414,
                    Longitude = 167.9254,
                    ImageUrl = "milford.jpg"
                },
                new Attraction
                {
                    Name = "Hobbiton Movie Set",
                    Description = "Step into the lush pastures of the Shire from The Lord of the Rings film trilogy.",
                    Region = "Waikato",
                    Latitude = -37.8721,
                    Longitude = 175.6826,
                    ImageUrl = "hobbiton.jpg"
                },
                new Attraction
                {
                    Name = "Aoraki / Mount Cook",
                    Description = "New Zealand's highest mountain, towering over alpine landscapes and stunning glaciers.",
                    Region = "Canterbury",
                    Latitude = -43.7344,
                    Longitude = 170.1411,
                    ImageUrl = "mountcook.jpg"
                }
        };

        context.Attractions.AddRange(attractions);
        context.SaveChanges();
    }
}