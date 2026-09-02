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

    public DbSet<Article> Articles { get; set; } = null!;

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
        SeedArticles(modelBuilder);
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>()
            .HasOne(r => r.Island)
            .WithMany(i => i.Regions)
            .HasForeignKey(r => r.IslandId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Article>()
            .Property(a => a.ContentJson)
            .HasColumnType("jsonb");

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

    // Create the seeding method with AI articles:
    private static void SeedArticles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                Id = 1,
                Title = "6 Must-Visit Places in New Zealand",
                Description = "From stunning fjords to geothermal wonders, discover the best places that should be on every traveller's list.",
                Category = "Destinations",
                ImageUrl = "assets/images/articles/nz-places.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "Sep 1, 2026",
                ReadTime = "6 min read",
                ViewsCount = 0,
                ContentJson = @"{
                ""intro"": ""New Zealand is a land of breathtaking landscapes, rich culture, and unique wildlife. From the majestic fjords of the South Island to the geothermal wonders of the North Island, there's something for every traveler. Here are 6 must-visit places that will make your trip unforgettable."",
                ""sections"": [
                    {
                        ""title"": ""1. Milford Sound"",
                        ""icon"": ""🌊"",
                        ""introText"": ""Often referred to as the 'eighth wonder of the world', Milford Sound is a fjord in the southwest of New Zealand's South Island."", 
                        ""items"": [
                            ""Take a scenic cruise to witness towering cliffs and waterfalls."",
                            ""Kayak through the calm waters for a more intimate experience."",
                            ""Hike the Milford Track for stunning views of the surrounding mountains.""
                        ]
                    },
                    {
                        ""title"": ""2. Rotorua"",
                        ""icon"": ""🌋"",
                        ""introText"": ""Known for its geothermal activity and Maori culture, Rotorua offers a unique experience."",
                        ""items"": [
                            ""Visit the Wai-O-Tapu Thermal Wonderland to see colorful hot springs."",
                            ""Experience a traditional Maori hangi feast and cultural performance."",
                            ""Relax in the natural hot springs at Polynesian Spa.""
                        ]
                    },
                    {
                        ""title"": ""3. Queenstown"",
                        ""icon"": ""🏔️"",
                        ""introText"": ""The adventure capital of New Zealand, Queenstown is set against the stunning Southern Alps."",
                        ""items"": [
                            ""Try bungee jumping or skydiving for an adrenaline rush."",
                            ""Take a scenic gondola ride for panoramic views of Lake Wakatipu."",
                            ""Explore nearby vineyards and enjoy wine tasting tours.""
                        ]
                    },
                    {
                        ""title"": ""4. Bay of Islands"",
                        ""icon"": ""🏝️"",
                        ""introText"": ""A subtropical region known for its beautiful beaches and historic sites."",
                        ""items"": [
                            ""Take a boat tour to see the famous Hole in the Rock."",
                            ""Visit the Waitangi Treaty Grounds to learn about New Zealand's history."",
                            ""Enjoy water activities like sailing, fishing, and dolphin watching.""
                        ]
                    },
                    {
                        ""title"": ""5. Franz Josef Glacier"",
                        ""icon"": ""🧊"",
                        ""introText"": ""One of the most accessible glaciers in the world, located on the West Coast of the South Island."",
                        ""items"": [
                            ""Take a guided glacier hike or ice climbing tour."",
                            ""Helicopter tours offer breathtaking aerial views of the glacier."",
                            ""Relax in the nearby hot pools after your glacier adventure.""
                        ]
                    },
                    {
                        ""title"": ""6. Hobbiton Movie Set"",
                        ""icon"": ""🏡"",
                        ""introText"": ""Step into the world of Middle-earth at the Hobbiton Movie Set in Matamata."",
                        ""items"": [
                            ""Take a guided tour of the iconic movie set."",
                            ""Enjoy a drink at the Green Dragon Inn."",
                            ""Learn about the making of the Lord of the Rings and The Hobbit films.""
                        ]
                    
                    }
                ],
                ""finalTip"": ""Plan your itinerary across both islands carefully to experience a mix of vibrant city life, alpine adventures, and peaceful coastal landscapes!""
                }"
            },
            new Article
            {
                Id = 2,
                Title = "Packing List for New Zealand",
                Description = "What to pack for every season and adventure in New Zealand.",
                Category = "Travel Tips",
                ImageUrl = "assets/images/articles/packing.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "Sep 01, 2026",
                ReadTime = "4 min read",
                ViewsCount = 0,
                ContentJson = @"{
              ""intro"": ""New Zealand's stunning landscapes and diverse climate mean packing smart is key to having an incredible trip. Whether you're hiking mountains, relaxing on beaches, or exploring vibrant cities, here's your ultimate packing guide."",
              ""sections"": [
                {
                  ""title"": ""1. Clothing Essentials"",
                  ""icon"": ""👕"",
                  ""introText"": ""New Zealand's weather can be unpredictable, so layering is your best friend."",
                  ""items"": [
                    ""Base layers: Moisture-wicking tops (merino wool is ideal)"",
                    ""Mid-layers: Fleece or down jacket for warmth"",
                    ""Outer layer: Waterproof and windproof jacket"",
                    ""Pants: Comfortable hiking pants and casual wear"",
                    ""Extras: Hat, gloves, scarf, and sunglasses""
                  ]
                },
                {
                  ""title"": ""2. Footwear"",
                  ""icon"": ""🥾"",
                  ""introText"": ""From hiking trails to city streets, the right footwear makes all the difference."",
                  ""items"": [
                    ""Hiking boots or trail shoes"",
                    ""Comfortable sneakers or casual shoes"",
                    ""Sandals or flip-flops (for beaches and hostels)""
                  ]
                },
                {
                    ""title"": ""3. Travel Accessories"",
                    ""icon"": ""🎒"",
                    ""introText"": ""Make your journey smoother with these handy items."",
                    ""items"": [
                        ""Daypack for daily excursions"",
                        ""Reusable water bottle"",
                        ""Travel adapter and chargers"",
                        ""Camera or smartphone for capturing memories"",
                        ""Travel documents: Passport, visa (if required), and travel insurance""
                    ]
                    },
                    {
                    ""title"": ""4. Health & Safety"",
                    ""icon"": ""💊"",
                    ""introText"": ""Stay healthy and safe during your adventures."",
                    ""items"": [
                        ""Basic first aid kit"",
                        ""Sunscreen and insect repellent"",
                        ""Prescription medications (if any)""
                    ]
                }
              ],
              ""finalTip"": ""Pack light, stay flexible, and be ready for anything!""
            }"
            },
            new Article
            {
                Id = 3,
                Title = "The Ultimate South Island Road Trip",
                Description = "A 3-day itinerary covering glaciers, lakes, and coastal drives you'll never forget.",
                Category = "Road Trips",
                ImageUrl = "assets/images/articles/road-trip.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "Sep 2, 2026",
                ReadTime = "8 min read",
                ViewsCount = 0,
                ContentJson = @"{
                ""intro"": ""The South Island of New Zealand is widely considered one of the best road trip destinations on earth. With dramatic mountain ranges, mirror-like lakes, and winding coastal highways, every mile brings a new breathtaking view. Here is your ultimate 5-day itinerary."",
                ""sections"": [
                    {
                        ""title"": ""Day 1-2: Christchurch to Lake Tekapo & Mount Cook"",
                        ""icon"": ""🚗"",
                        ""introText"": ""Begin your journey heading inland across the Canterbury Plains towards alpine lakes."",
                        ""items"": [
                            ""Admire the striking turquoise waters of Lake Tekapo and visit the Church of the Good Shepherd"",
                            ""Stargaze in the Aoraki Mackenzie International Dark Sky Reserve"",
                            ""Take a short hike to view the majestic peaks of Mount Cook (Aoraki)""
                        ]
                    },
                    {
                        ""title"": ""Day 3: Queenstown & Alpine Passes"",
                        ""icon"": ""🏔️"",
                        ""introText"": ""Drive through rugged mountain passes down into the global adventure capital"",
                        ""items"": [
                            ""Journey past the dramatic Kawarau Gorge and the historic Kawarau Bridge, home of the first commercial bungee jump"",
                            ""Stop for photos at the historic Cromwell heritage precinct"",
                            ""Settle into Queenstown for an evening by Lake Wakatipu""
                        ]
                    },
                    {
                        ""title"": ""Day 4-5: Milford Sound & Coastal Return"",
                        ""icon"": ""🌊"",
                        ""introText"": ""Experience the deep blue fjords and lush rainforests of Fiordland National Park."",
                        ""items"": [
                            ""Drive the Milford Road—one of the most scenic alpine drives in the world"",
                            ""Take a midday cruise through Milford Sound to see majestic waterfalls and wildlife"",
                            ""Complete your loop back north with a stop at coastal viewpoints""
                        ]
                    }
                ],
                ""finalTip"": ""Ensure you book your vehicle rental and activities well in advance, especially if you are traveling during peak summer season!""
                }"
            },
            new Article
            {
                Id = 4,
                Title = "Top 2 Adventure Activities in New Zealand",
                Description = "From bungee jumping to skydiving, discover the adrenaline-pumping experiences that make New Zealand a thrill-seeker's paradise.",
                Category = "Adventure",
                ImageUrl = "assets/images/articles/adventure.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "Sep 2, 2026",
                ReadTime = "7 min read",
                ViewsCount = 0,
                ContentJson = @"{
                    ""intro"": ""New Zealand is renowned for its adventure tourism, offering a wide range of activities for thrill-seekers. Here are the top 2 adventure activities you shouldn't miss during your visit."",
                    ""sections"": [
                        {
                            ""title"": ""1. Bungee Jumping"",
                            ""icon"": ""🪂"",
                            ""introText"": ""Experience the ultimate adrenaline rush by leaping off iconic bridges and platforms."",
                            ""items"": [
                                ""Kawarau Bridge in Queenstown: The world's first commercial bungee jump."",
                                ""Nevis Bungy: One of the highest jumps in New Zealand at 134 meters.""
                            ]
                        },
                        {
                            ""title"": ""2. Skydiving"",
                            ""icon"": ""🪂"",
                            ""introText"": ""Soar through the skies and enjoy breathtaking aerial views of New Zealand's landscapes."",
                            ""items"": [
                                ""Queenstown: Jump over lakes and mountains for an unforgettable experience."",
                                ""Taupo: Skydive over Lake Taupo and the surrounding volcanic terrain.""
                            ]
                        }
                    ],
                    ""finalTip"": ""Always ensure you choose reputable operators with certified safety standards for all adventure activities.""
                }"
            },
            new Article
            {
                Id = 5,
                Title = "Hidden Gems of the North Island",
                Description = "Discover the North Island's lesser-known landscapes, from secluded Coromandel beaches and the historic Forgotten World Highway to enchanting forests and waterfalls around Taranaki.",
                Category = "Off the Beaten Path",
                ImageUrl = "assets/images/articles/north-gem.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "Sep 1, 2026",
                ReadTime = "5 min read",
                ViewsCount = 0,
                ContentJson = @"{
    ""intro"": ""While major cities and famous hot spots get most of the attention, the North Island hides incredible secret locations that few tourists ever manage to find. Escape the crowds and discover these pristine hidden gems."",
    ""sections"": [
        {
            ""title"": ""1. Cathedral Cove Alternatives: Secret Beaches of the Coromandel"",
            ""icon"": ""🏖️"",
            ""introText"": ""Skip the crowded paths and explore secluded coastal coves along the Pacific Coast Highway."",
            ""items"": [
                ""Discover hidden swimming holes accessible only at low tide"",
                ""Explore untouched golden sand beaches surrounded by native pohutukawa trees"",
                ""Pack a picnic to enjoy uninterrupted ocean horizons""
            ]
        },
        {
            ""title"": ""2. The Forgotten World Highway (SH43)"",
            ""icon"": ""🚗"",
            ""introText"": ""A historic, winding road that takes you deep into New Zealand's rugged rural history."",
            ""items"": [
                ""Drive through the eerie, hand-carved Moki Tunnel"",
                ""Explore the self-proclaimed Republic of Whangamomona"",
                ""Take in sweeping valley views from remote mountain saddles""
            ]
        },
        {
            ""title"": ""3. Taranaki's Secret Waterfall Tracks"",
            ""icon"": ""🌿"",
            ""introText"": ""Step into an ancient, moss-draped goblin forest beneath Mount Taranaki."",
            ""items"": [
                ""Walk the enchanting delayed-exposure photography tracks"",
                ""Listen to native bird song in untouched ecological sanctuaries"",
                ""Capture stunning reflections of volcanic peaks in quiet alpine tarns""
            ]
        }
    ],
    ""finalTip"": ""Always check local weather and tide charts before exploring secluded coastal tracks on the North Island!""
}"
            },
            new Article
            {
                Id = 6,
                Title = "Culinary Delights of New Zealand",
                Description = "Taste your way through New Zealand with traditional Māori hāngi, fresh seafood, world-class wines, iconic Kiwi treats and a thriving café culture.",
                Category = "Food & Wine",
                ImageUrl = "assets/images/articles/food-wine.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "Jul 31, 2026",
                ReadTime = "6 min read",
                ViewsCount = 0,
                ContentJson = @"{
    ""intro"": ""New Zealand's culinary scene is a delightful fusion of indigenous Maori flavors, European influences, and fresh local ingredients. From world-class wines to farm-to-table dining experiences, here's a guide to the best food and wine experiences across the country."",
    ""sections"": [
        {
            ""title"": ""1. Traditional Māori Hāngi"",
            ""icon"": ""🔥"",
            ""introText"": ""An ancient cooking method where food is slow-cooked underground on hot stones."",
            ""items"": [
                ""Savor tender chicken, pork, and root vegetables infused with a rich, earthy flavor"",
                ""Learn about the cultural significance of sharing food in community gatherings"",
                ""Best experienced through guided cultural tours in Rotorua""
            ]
        },
        {
            ""title"": ""2. World-Class Seafood & Bluff Oysters"",
            ""icon"": ""🦪"",
            ""introText"": ""Surrounded by ocean, New Zealand offers some of the freshest seafood on the planet."",
            ""items"": [
                ""Taste famous wild Bluff oysters during their seasonal harvest"",
                ""Try classic New Zealand green-lipped mussels steamed in white wine"",
                ""Indulge in fresh crayfish (rock lobster) along the Kaikoura coast""
            ]
        },
        {
            ""title"": ""3. Wine Regions to Explore"",
            ""icon"": ""🍷"",
            ""introText"": ""New Zealand is renowned for its vineyards, producing some of the world's best Sauvignon Blanc and Pinot Noir."",
            ""items"": [
                ""Marlborough: Famous for crisp Sauvignon Blancs and scenic vineyard tours."",
                ""Central Otago: Known for its award-winning Pinot Noir and stunning alpine landscapes."",
                ""Hawke's Bay: Offers a diverse range of wines, including Merlot and Syrah, along with gourmet food experiences.""
            ]
        },
        {
            ""title"": ""4. Must-Try Local Dishes"",
            ""icon"": ""🍽️"",
            ""introText"": ""Experience the unique flavors of New Zealand through its traditional and contemporary dishes."",
            ""items"": [
                ""Hāngi: A traditional Maori method of cooking food in an earth oven, resulting in tender and flavorful meats and vegetables."",
                ""Pavlova: A meringue-based dessert topped with fresh fruits, named after the Russian ballerina Anna Pavlova."",
                ""Green-lipped Mussels: A local seafood delicacy, often served steamed or in a creamy sauce.""
            ]
        },
        {
            ""title"": ""5. Iconic Kiwi Sweets & Coffee Culture"",
            ""icon"": ""☕"",
            ""introText"": ""Fuel your road trips with exceptional flat whites and legendary local treats."",
            ""items"": [
                ""Order a classic 'flat white' at any local artisanal café"",
                ""Try iconic Hokey Pokey ice cream (vanilla with crunchy honeycomb chunks)"",
                ""Snack on classic Anzac biscuits baked fresh daily""
            ]
        }
        ],
        ""finalTip"": ""Pair your evening meals with a glass of world-renowned Marlborough Sauvignon Blanc or Central Otago Pinot Noir!""
    }"
            },
            new Article
{
    Id = 7,
    Title = "6 New Zealand Landscapes That Feel Like Another Planet",
    Description = "From volcanic craters to underground glowworm caves, discover six extraordinary landscapes that show just how diverse New Zealand can be.",
    Category = "Destinations",
    ImageUrl = "assets/images/articles/otherworldly-nz.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Sep 2, 2026",
    ReadTime = "7 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""New Zealand is famous for beautiful scenery, but some parts of the country look almost otherworldly. Volcanic craters, limestone caves, unusual coastal formations and remote islands have created landscapes that feel completely different from one another. Here are six places where the scenery itself becomes the main attraction."",
        ""sections"": [
            {
                ""title"": ""1. Tongariro National Park"",
                ""icon"": ""🌋"",
                ""introText"": ""An extraordinary volcanic landscape filled with alpine terrain, volcanic craters, emerald-coloured lakes and dramatic mountains."",
                ""items"": [
                    ""Walk the Tongariro Alpine Crossing when weather and track conditions are suitable."",
                    ""See the striking Emerald Lakes and volcanic landscape around the crossing."",
                    ""Consider the Tama Lakes Track for another way to experience the volcanic scenery.""
                ]
            },
            {
                ""title"": ""2. Waitomo Cave Country"",
                ""icon"": ""🪨"",
                ""introText"": ""Beneath the green farmland of Waikato lies a remarkable underground world of limestone caves and glowworms."",
                ""items"": [
                    ""Take a guided tour through the famous Waitomo cave systems."",
                    ""See glowworms illuminating the darkness above the underground waterways."",
                    ""Choose from relaxed cave tours or more adventurous underground experiences.""
                ]
            },
            {
                ""title"": ""3. Putangirua Pinnacles"",
                ""icon"": ""🏜️"",
                ""introText"": ""Thousands of years of erosion have created dramatic columns of rock in the Aorangi Range near the southern Wairarapa coast."",
                ""items"": [
                    ""Walk the Putangirua Pinnacles Track through the rugged landscape."",
                    ""Follow the riverbed toward the towering formations."",
                    ""Recognise the landscape featured in The Lord of the Rings films.""
                ]
            },
            {
                ""title"": ""4. Punakaiki Pancake Rocks"",
                ""icon"": ""🌊"",
                ""introText"": ""Along the West Coast, layers of limestone have been shaped by the sea into unusual formations resembling enormous stacks of pancakes."",
                ""items"": [
                    ""Walk the Pancake Rocks and Blowholes Track."",
                    ""Visit around high tide when wave action can make the blowholes especially dramatic."",
                    ""Combine the visit with the surrounding rainforest and rugged West Coast scenery.""
                ]
            },
            {
                ""title"": ""5. Castlepoint"",
                ""icon"": ""🏖️"",
                ""introText"": ""A distinctive Wairarapa coastline where cliffs, limestone formations, beaches and the Pacific Ocean meet."",
                ""items"": [
                    ""Walk toward the historic Castlepoint Lighthouse."",
                    ""Explore the unusual coastal rock formations around Castlepoint."",
                    ""Climb toward Castle Rock for panoramic views of the coastline.""
                ]
            },
            {
                ""title"": ""6. Rakiura / Stewart Island"",
                ""icon"": ""🌌"",
                ""introText"": ""New Zealand's third-largest island offers remote forests, dramatic coastline, native wildlife and exceptionally dark night skies."",
                ""items"": [
                    ""Explore parts of Rakiura National Park on foot."",
                    ""Look for native birds and wildlife in the island's natural environment."",
                    ""Experience the island after dark beneath its famous southern night sky.""
                ]
            }
        ],
        ""finalTip"": ""New Zealand's most unusual landscapes are often best experienced slowly. Leave time for short walks, viewpoints and unexpected stops instead of rushing from one attraction to another.""
    }"
},

new Article
{
    Id = 8,
    Title = "10 Things First-Time Visitors Get Wrong About New Zealand",
    Description = "Avoid common travel mistakes with practical lessons about driving, weather, hiking, bookings, safety and travelling around Aotearoa.",
    Category = "Travel Tips",
    ImageUrl = "assets/images/articles/nz-travel-mistakes.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Sep 1, 2026",
    ReadTime = "7 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""New Zealand can look easy to travel around on a map, but first-time visitors can quickly discover that the reality is different. Long driving days, changing weather, remote hiking areas and popular attractions that require advance planning can all affect your trip. Here are ten common mistakes worth avoiding."",
        ""sections"": [
            {
                ""title"": ""1. Underestimating Driving Times"",
                ""icon"": ""🚗"",
                ""introText"": ""Road distances can look surprisingly short on a map, but New Zealand roads often wind through hills, mountains and coastal terrain."",
                ""items"": [
                    ""Allow more time than the map distance alone suggests."",
                    ""Remember that scenic roads often include many worthwhile stopping points."",
                    ""Take regular breaks during long drives rather than trying to reach the next destination as quickly as possible.""
                ]
            },
            {
                ""title"": ""2. Trying to See Everything"",
                ""icon"": ""🗺️"",
                ""introText"": ""New Zealand may look compact compared with some countries, but trying to cover too much can turn a holiday into a long series of drives."",
                ""items"": [
                    ""Choose a few priority destinations rather than trying to see every famous attraction."",
                    ""Allow enough time to explore each region."",
                    ""Consider focusing on one island if your trip is short.""
                ]
            },
            {
                ""title"": ""3. Trusting the Weather Too Much"",
                ""icon"": ""🌦️"",
                ""introText"": ""New Zealand's weather can change quickly, particularly in alpine and coastal environments."",
                ""items"": [
                    ""Check the forecast before outdoor activities."",
                    ""Carry a waterproof and windproof layer even when the morning looks sunny."",
                    ""Be prepared to change plans when conditions become unsafe.""
                ]
            },
            {
                ""title"": ""4. Forgetting Sun Protection"",
                ""icon"": ""☀️"",
                ""introText"": ""Outdoor adventures can mean spending many hours exposed to the sun, even when the temperature does not feel extremely hot."",
                ""items"": [
                    ""Carry sunscreen and reapply it during long outdoor activities."",
                    ""Wear sunglasses and a hat when appropriate."",
                    ""Take extra care during long hikes, beach days and water activities.""
                ]
            },
            {
                ""title"": ""5. Treating Every Great Walk as an Easy Walk"",
                ""icon"": ""🥾"",
                ""introText"": ""New Zealand's Great Walks are famous, but they are still multi-day outdoor adventures that require preparation."",
                ""items"": [
                    ""Check the official track information before starting."",
                    ""Understand the distance, terrain and expected conditions."",
                    ""Book huts or campsites where required and carry suitable equipment.""
                ]
            },
            {
                ""title"": ""6. Leaving Popular Bookings Until the Last Minute"",
                ""icon"": ""📅"",
                ""introText"": ""Popular walks, accommodation and activities can become difficult to book during busy travel periods."",
                ""items"": [
                    ""Book popular Great Walk huts and campsites ahead of time."",
                    ""Reserve rental vehicles and accommodation before arriving during busy periods."",
                    ""Check activity availability before building your itinerary around it.""
                ]
            },
            {
                ""title"": ""7. Packing Only for the Season"",
                ""icon"": ""🧥"",
                ""introText"": ""New Zealand's seasons provide a useful guide, but local conditions can vary significantly between regions."",
                ""items"": [
                    ""Pack layers rather than relying on one heavy item."",
                    ""Carry a light waterproof layer for changeable conditions."",
                    ""Remember that alpine areas can be much colder than nearby towns.""
                ]
            },
            {
                ""title"": ""8. Depending Completely on Mobile Coverage"",
                ""icon"": ""📱"",
                ""introText"": ""Remote roads and hiking areas may have limited or no mobile reception."",
                ""items"": [
                    ""Download maps before leaving towns and cities."",
                    ""Tell someone about your plans when heading into remote areas."",
                    ""Do not rely entirely on your phone for navigation or emergency information.""
                ]
            },
            {
                ""title"": ""9. Ignoring Māori Culture and Place Names"",
                ""icon"": ""🌿"",
                ""introText"": ""Travelling through Aotearoa is also an opportunity to learn about Māori history, culture and the meaning behind many place names."",
                ""items"": [
                    ""Learn the pronunciation of Māori place names where possible."",
                    ""Read about the cultural significance of places you visit."",
                    ""Respect local cultural sites and follow visitor guidance.""
                ]
            },
            {
                ""title"": ""10. Rushing the Journey"",
                ""icon"": ""⏳"",
                ""introText"": ""Some of New Zealand's best travel moments happen between the major attractions."",
                ""items"": [
                    ""Leave space in your itinerary for unexpected stops."",
                    ""Take short walks and scenic detours when time allows."",
                    ""Spend time in smaller towns rather than treating them only as overnight stops.""
                ]
            }
        ],
        ""finalTip"": ""Build flexibility into your itinerary. A good New Zealand trip is not just about reaching every destination—it is about having enough time to enjoy the journey between them.""
    }"
},

new Article
{
    Id = 9,
    Title = "The Great Kiwi Loop: A Road Trip Built Around Landscapes",
    Description = "Follow a scenery-first New Zealand road trip through volcanic country, alpine lakes, dramatic mountains, forests and remote coastlines.",
    Category = "Road Trips",
    ImageUrl = "assets/images/articles/great-kiwi-loop.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Jul 30, 2026",
    ReadTime = "8 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""The best New Zealand road trips are not always about reaching the next city as quickly as possible. Instead, build your journey around the landscapes. This scenery-first route connects volcanic country, alpine lakes, mountains, forests and dramatic waterways into one memorable Kiwi adventure."",
        ""sections"": [
            {
                ""title"": ""1. Auckland to Rotorua"",
                ""icon"": ""🚐"",
                ""introText"": ""Begin in Auckland and head south toward Rotorua, where the landscape changes from urban life to geothermal country."",
                ""items"": [
                    ""Explore Rotorua's geothermal areas and steaming landscapes."",
                    ""Experience Māori culture through reputable local cultural experiences."",
                    ""Explore the lakes and forests surrounding Rotorua.""
                ]
            },
            {
                ""title"": ""2. Rotorua to Lake Taupō"",
                ""icon"": ""🌋"",
                ""introText"": ""Continue south toward Lake Taupō and discover one of the largest volcanic landscapes in the North Island."",
                ""items"": [
                    ""Stop to see the powerful Huka Falls."",
                    ""Enjoy views around Lake Taupō."",
                    ""Take time to explore the surrounding volcanic region.""
                ]
            },
            {
                ""title"": ""3. Lake Taupō to Tongariro"",
                ""icon"": ""🥾"",
                ""introText"": ""Head toward Tongariro National Park for a dramatic change from lake country to alpine volcanic terrain."",
                ""items"": [
                    ""Walk the Tongariro Alpine Crossing when conditions and experience are suitable."",
                    ""Consider the Tama Lakes Track for another volcanic landscape experience."",
                    ""Spend extra time around Whakapapa Village and Tongariro National Park.""
                ]
            },
            {
                ""title"": ""4. Across to the South Island"",
                ""icon"": ""⛴️"",
                ""introText"": ""For a longer journey, continue south and cross to the South Island, where the landscapes become increasingly alpine."",
                ""items"": [
                    ""Allow enough time for the journey between the North and South Islands."",
                    ""Plan accommodation around your major stops rather than trying to drive continuously."",
                    ""Keep the itinerary flexible around weather and transport schedules.""
                ]
            },
            {
                ""title"": ""5. Aoraki / Mount Cook & Mackenzie Country"",
                ""icon"": ""🏔️"",
                ""introText"": ""Travel through the open landscapes of Mackenzie Country toward Aoraki / Mount Cook."",
                ""items"": [
                    ""Enjoy the striking colours of the alpine lakes."",
                    ""Take a short walk in Aoraki / Mount Cook National Park."",
                    ""Spend time under the dark skies of the Mackenzie region when conditions allow.""
                ]
            },
            {
                ""title"": ""6. Queenstown to Fiordland"",
                ""icon"": ""🌊"",
                ""introText"": ""Continue through Central Otago toward Queenstown before heading into the dramatic landscapes of Fiordland."",
                ""items"": [
                    ""Drive through the mountain scenery around Queenstown and Central Otago."",
                    ""Allow plenty of time for stops along the Milford Road."",
                    ""Take a cruise on Milford Sound / Piopiotahi to experience the fiord from the water.""
                ]
            },
            {
                ""title"": ""7. Return Through Central Otago"",
                ""icon"": ""🍂"",
                ""introText"": ""Complete the journey through the dry inland landscapes and historic towns of Central Otago."",
                ""items"": [
                    ""Explore historic gold-mining communities."",
                    ""Visit local vineyards and cellar doors where available."",
                    ""Slow down for roadside viewpoints and small-town discoveries.""
                ]
            }
        ],
        ""finalTip"": ""Plan the major overnight stops in advance, but do not fill every hour of every day. The scenery, small detours and unexpected stops are what make a New Zealand road trip memorable.""
    }"
},

new Article
{
    Id = 10,
    Title = "Beyond Bungee: 7 Adventures That Show New Zealand Differently",
    Description = "Go beyond the famous jumps with rafting, jet boating, caving, kayaking, mountain biking and other unforgettable Kiwi adventures.",
    Category = "Adventure",
    ImageUrl = "assets/images/articles/beyond-bungee.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Sep 2, 2026",
    ReadTime = "8 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""New Zealand's adventure reputation goes far beyond bungee jumping and skydiving. Rivers, caves, forests, mountains and coastlines create opportunities to experience the country's landscapes from completely different perspectives. Here are seven adventures worth adding to your Kiwi itinerary."",
        ""sections"": [
            {
                ""title"": ""1. White-Water Rafting"",
                ""icon"": ""🌊"",
                ""introText"": ""Get into New Zealand's rivers and experience the landscape from the water."",
                ""items"": [
                    ""Choose a guided rafting trip that matches your experience level."",
                    ""Experience rapids while travelling through river valleys and native landscapes."",
                    ""Follow the operator's safety instructions and equipment requirements.""
                ]
            },
            {
                ""title"": ""2. Jet Boating"",
                ""icon"": ""🚤"",
                ""introText"": ""Jet boats turn New Zealand's rivers into high-speed adventure routes through narrow valleys and dramatic scenery."",
                ""items"": [
                    ""Experience fast turns and rapid acceleration on specially designed river routes."",
                    ""Enjoy views of mountains, cliffs and native forest from the water."",
                    ""Choose an established operator with appropriate safety procedures.""
                ]
            },
            {
                ""title"": ""3. Black-Water Rafting"",
                ""icon"": ""🕳️"",
                ""introText"": ""In the Waitomo region, underground adventure combines caves, waterways and glowworms."",
                ""items"": [
                    ""Travel through underground cave passages with a specialist guide."",
                    ""Float through sections of underground river in complete darkness."",
                    ""Look up at glowworms while travelling through the cave system.""
                ]
            },
            {
                ""title"": ""4. Canyoning"",
                ""icon"": ""🧗"",
                ""introText"": ""Canyoning combines walking, climbing, swimming and abseiling in New Zealand's natural waterways."",
                ""items"": [
                    ""Move through narrow canyons and natural rock formations."",
                    ""Abseil alongside waterfalls on suitable guided trips."",
                    ""Swim through natural pools and explore areas inaccessible by normal walking tracks.""
                ]
            },
            {
                ""title"": ""5. Mountain Biking"",
                ""icon"": ""🚵"",
                ""introText"": ""New Zealand offers cycling experiences ranging from relaxed rail trails to challenging mountain-bike terrain."",
                ""items"": [
                    ""Explore purpose-built mountain-bike parks."",
                    ""Ride scenic rail trails through countryside and historic regions."",
                    ""Combine cycling with local cafés, towns and food stops.""
                ]
            },
            {
                ""title"": ""6. Kayaking & Sea Adventures"",
                ""icon"": ""🛶"",
                ""introText"": ""Kayaking provides a quieter form of adventure and a completely different view of New Zealand's coastline."",
                ""items"": [
                    ""Paddle beside coastal cliffs, beaches and islands."",
                    ""Explore sheltered waterways and marine environments with a local guide."",
                    ""Combine kayaking with walking in coastal areas such as Abel Tasman National Park.""
                ]
            },
            {
                ""title"": ""7. Alpine Adventures"",
                ""icon"": ""🏔️"",
                ""introText"": ""New Zealand's alpine environments offer some of the country's most challenging outdoor experiences."",
                ""items"": [
                    ""Try guided alpine experiences suitable for your skill level."",
                    ""Explore mountain landscapes through experienced local operators."",
                    ""Check weather and alpine conditions carefully before entering exposed terrain.""
                ]
            }
        ],
        ""finalTip"": ""Choose an adventure that matches your experience rather than simply choosing the activity with the biggest adrenaline factor. The best adventure is one where you feel challenged, excited and prepared.""
    }"
},

new Article
{
    Id = 11,
    Title = "6 Quiet Corners of New Zealand Worth Taking the Long Way To",
    Description = "Escape the busiest tourist routes and discover quieter coastlines, river valleys, forests and small communities across New Zealand.",
    Category = "Off the Beaten Path",
    ImageUrl = "assets/images/articles/quiet-corners.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Sep 3, 2026",
    ReadTime = "7 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""Going off the beaten path in New Zealand does not necessarily mean discovering a completely unknown place. Sometimes it simply means taking the slower road, staying longer in a small community or choosing a region that receives less attention than the country's biggest tourist destinations. These six places reward travellers who are willing to slow down."",
        ""sections"": [
            {
                ""title"": ""1. Ōpōtiki & the Eastern Bay of Plenty"",
                ""icon"": ""🌊"",
                ""introText"": ""The Eastern Bay of Plenty combines Pacific coastline, forests, rivers and strong Māori cultural connections."",
                ""items"": [
                    ""Explore the coastline around Ōpōtiki."",
                    ""Discover local walking and cycling opportunities."",
                    ""Use the town as a base for exploring the surrounding Eastern Bay of Plenty.""
                ]
            },
            {
                ""title"": ""2. Whanganui River Country"",
                ""icon"": ""🛶"",
                ""introText"": ""The Whanganui River passes through remote hills and bush-clad valleys, creating one of New Zealand's most distinctive slow-travel experiences."",
                ""items"": [
                    ""Experience the Whanganui Journey by canoe or kayak with suitable preparation."",
                    ""Travel through Whanganui National Park and its remote river landscapes."",
                    ""Stay overnight along the river during a multi-day journey.""
                ]
            },
            {
                ""title"": ""3. Golden Bay"",
                ""icon"": ""🌿"",
                ""introText"": ""At the northern end of the South Island, Golden Bay offers beaches, forests, limestone landscapes and a noticeably slower pace."",
                ""items"": [
                    ""Explore the coastline and beaches around the region."",
                    ""Discover limestone landscapes and nearby natural attractions."",
                    ""Use the area as a gateway to parts of Kahurangi National Park.""
                ]
            },
            {
                ""title"": ""4. Westport & the Buller Coast"",
                ""icon"": ""🌊"",
                ""introText"": ""The Buller Coast combines rugged beaches, rivers, native forest and a strong connection to the West Coast's mining history."",
                ""items"": [
                    ""Explore the coastline around Westport."",
                    ""Discover local history and former mining communities."",
                    ""Use Westport as a base for exploring the wider Buller region.""
                ]
            },
            {
                ""title"": ""5. The Catlins"",
                ""icon"": ""🌳"",
                ""introText"": ""Located in the far south, the Catlins combines native forest, waterfalls, rugged beaches and dramatic coastal scenery."",
                ""items"": [
                    ""Stop at waterfalls and short walking tracks along the coast."",
                    ""Explore the region's native forest and coastal landscapes."",
                    ""Allow enough time to stop rather than treating the Catlins as a simple drive-through route.""
                ]
            },
            {
                ""title"": ""6. Rakiura / Stewart Island"",
                ""icon"": ""🌌"",
                ""introText"": ""Far south of the mainland, Stewart Island offers remote forests, coastal walks, native wildlife and a slower rhythm of travel."",
                ""items"": [
                    ""Explore Rakiura National Park on foot."",
                    ""Look for native birds in their natural environment."",
                    ""Experience the island's exceptionally dark night skies.""
                ]
            }
        ],
        ""finalTip"": ""Going off the beaten path is not about keeping places secret. Travel responsibly, support local communities and give yourself enough time to appreciate places that reward patience.""
    }"
},

new Article
{
    Id = 12,
    Title = "Aotearoa by Season: 6 Food Experiences Worth Planning Around",
    Description = "Taste New Zealand through its seasons with fresh seafood, local produce, wine harvests, farmers' markets and regional specialties.",
    Category = "Food & Wine",
    ImageUrl = "assets/images/articles/seasonal-nz-food.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Sep 3, 2026",
    ReadTime = "7 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""New Zealand's food culture is closely connected to its seasons and regions. Instead of asking only what food you should try, ask what is fresh where you are travelling. From summer seafood and grape harvests to autumn produce and seasonal oysters, food can become part of the journey itself."",
        ""sections"": [
            {
                ""title"": ""1. Summer Seafood"",
                ""icon"": ""🦪"",
                ""introText"": ""New Zealand's summer months bring warm weather and plenty of opportunities to enjoy fresh coastal seafood."",
                ""items"": [
                    ""Look for seasonal seafood at local restaurants and fish markets."",
                    ""Try kina, a native sea urchin, where it is locally available and legally sourced."",
                    ""Enjoy seafood outdoors when visiting coastal regions during summer.""
                ]
            },
            {
                ""title"": ""2. New Zealand Wine Harvest"",
                ""icon"": ""🍇"",
                ""introText"": ""Late summer and early autumn coincide with grape harvesting across many of New Zealand's wine regions."",
                ""items"": [
                    ""Explore vineyard regions such as Marlborough, Hawke's Bay and Central Otago."",
                    ""Visit cellar doors and learn how regional conditions influence the wines."",
                    ""Look for local wine and food events during the harvest period.""
                ]
            },
            {
                ""title"": ""3. Bluff Oysters"",
                ""icon"": ""🦪"",
                ""introText"": ""Bluff oysters are one of New Zealand's best-known seasonal seafood specialties and are strongly associated with Southland."",
                ""items"": [
                    ""Try Bluff oysters during their seasonal availability."",
                    ""Look for them at restaurants and seafood venues in Southland."",
                    ""Pair the experience with a journey through the southern regions of New Zealand.""
                ]
            },
            {
                ""title"": ""4. Autumn Harvests"",
                ""icon"": ""🍎"",
                ""introText"": ""Autumn brings fresh harvests of apples, kūmara and other produce to markets and kitchens around the country."",
                ""items"": [
                    ""Visit farmers' markets to discover seasonal local produce."",
                    ""Look for apples and other autumn fruit when travelling through growing regions."",
                    ""Try seasonal dishes featuring freshly harvested ingredients.""
                ]
            },
            {
                ""title"": ""5. Farmers' Markets"",
                ""icon"": ""🧺"",
                ""introText"": ""Farmers' markets are one of the easiest ways to discover what a region produces locally."",
                ""items"": [
                    ""Look for locally grown fruit and vegetables."",
                    ""Try locally produced bread, cheese, honey and preserves."",
                    ""Ask vendors about where the ingredients were grown or produced.""
                ]
            },
            {
                ""title"": ""6. Wine Regions With Local Food"",
                ""icon"": ""🍷"",
                ""introText"": ""New Zealand's wine regions offer more than tastings, with many combining vineyards, local produce, restaurants and scenic landscapes."",
                ""items"": [
                    ""Visit Marlborough for Sauvignon Blanc and vineyard experiences."",
                    ""Explore Central Otago for Pinot Noir and dramatic inland scenery."",
                    ""Discover Hawke's Bay for wine, vineyard dining and regional produce.""
                ]
            }
        ],
        ""finalTip"": ""The best food experience is often the one connected to where you are. Follow the season, buy locally, ask questions and let the region influence what ends up on your plate.""
    }"
},
            new Article
{
    Id = 13,
    Title = "How to Travel Around New Zealand Without Renting a Car",
    Description = "Discover a smarter way to explore Aotearoa using scenic trains, buses, ferries, local transport and guided trips instead of driving everywhere.",
    Category = "Travel Tips",
    ImageUrl = "assets/images/articles/nz-without-car.jpg",
    AuthorName = "WanderKiwi AI",
    AuthorAvatar = "assets/images/wanderkiwi-logo.png",
    Date = "Sep 3, 2026",
    ReadTime = "7 min read",
    ViewsCount = 0,
    ContentJson = @"{
        ""intro"": ""Renting a car is one of the most popular ways to explore New Zealand, but it is not the only option. Travellers can combine buses, scenic trains, ferries, local public transport and guided tours to experience Aotearoa without spending their entire holiday behind the wheel. For some visitors, travelling without a car can make the journey slower, easier and more relaxing."",
        ""sections"": [
            {
                ""title"": ""1. Use Scenic Trains for the Big Journeys"",
                ""icon"": ""🚆"",
                ""introText"": ""New Zealand's scenic rail journeys turn the trip itself into part of the experience."",
                ""items"": [
                    ""Take the Northern Explorer between Auckland and Wellington through the North Island's volcanic and rural landscapes."",
                    ""Travel the Coastal Pacific between Christchurch and Picton along the Kaikōura coastline and through Marlborough."",
                    ""Cross the Southern Alps on the TranzAlpine between Christchurch and Greymouth.""
                ]
            },
            {
                ""title"": ""2. Connect the Islands by Ferry"",
                ""icon"": ""⛴️"",
                ""introText"": ""You can travel between the North and South Islands without putting a rental car on the ferry."",
                ""items"": [
                    ""Take a Cook Strait ferry between Wellington and Picton."",
                    ""Spend the crossing enjoying views of the Marlborough Sounds and Cook Strait."",
                    ""Combine ferry travel with trains or coaches on either side of the crossing.""
                ]
            },
            {
                ""title"": ""3. Let Intercity Buses Do the Driving"",
                ""icon"": ""🚌"",
                ""introText"": ""Long-distance coach services can connect major towns and cities while you relax and enjoy the scenery."",
                ""items"": [
                    ""Use buses for regional connections that are not covered by scenic rail."",
                    ""Plan accommodation around your arrival points rather than trying to cover too much in one day."",
                    ""Use the travel time to rest, read or enjoy the scenery instead of concentrating on the road.""
                ]
            },
            {
                ""title"": ""4. Explore Cities With Local Transport"",
                ""icon"": ""🚏"",
                ""introText"": ""A car is often unnecessary once you arrive in New Zealand's larger cities."",
                ""items"": [
                    ""Use local buses and other public transport to explore urban areas."",
                    ""Walk between attractions where practical."",
                    ""Look for accommodation close to central transport connections.""
                ]
            },
            {
                ""title"": ""5. Use Guided Day Trips for Hard-to-Reach Places"",
                ""icon"": ""🗺️"",
                ""introText"": ""Some of New Zealand's most famous experiences are easier to visit as part of an organised day trip."",
                ""items"": [
                    ""Choose guided excursions to destinations that are difficult to reach independently without a vehicle."",
                    ""Use local operators for activities where local knowledge adds value."",
                    ""Treat the journey as part of the experience rather than simply trying to reach the destination.""
                ]
            },
            {
                ""title"": ""6. Build Your Trip Around Transport Hubs"",
                ""icon"": ""📍"",
                ""introText"": ""The easiest car-free itineraries are designed around places with strong transport connections."",
                ""items"": [
                    ""Choose major towns as bases for regional exploration."",
                    ""Check train, bus and ferry schedules before booking accommodation."",
                    ""Allow extra time between connections instead of creating tight same-day transfers.""
                ]
            },
            {
                ""title"": ""7. Turn Slow Travel Into the Experience"",
                ""icon"": ""🌿"",
                ""introText"": ""Travelling without a car can change the pace of a New Zealand holiday and make the journey itself more memorable."",
                ""items"": [
                    ""Watch mountains, coastlines and farmland pass by instead of focusing on navigation."",
                    ""Use travel days as opportunities to rest rather than treating them as wasted time."",
                    ""Spend longer in fewer destinations instead of constantly moving to the next stop.""
                ]
            }
        ],
        ""finalTip"": ""You do not need to drive every kilometre to experience New Zealand. Combine trains, buses, ferries, local transport and guided experiences to build a slower journey that lets you enjoy more of the scenery and less of the stress.""
    }"
},
            new Article
            {
                Id = 14,
                Title = "Packing List for New Zealand",
                Description = "What to pack for every season and adventure in New Zealand.",
                Category = "Travel Tips",
                ImageUrl = "assets/images/articles/packing.jpg",
                AuthorName = "WanderKiwi AI",
                AuthorAvatar = "assets/images/wanderkiwi-logo.png",
                Date = "July 30, 2026",
                ReadTime = "4 min read",
                ViewsCount = 0,
                ContentJson = @"{
              ""intro"": ""New Zealand's stunning landscapes and diverse climate mean packing smart is key to having an incredible trip. Whether you're hiking mountains, relaxing on beaches, or exploring vibrant cities, here's your ultimate packing guide."",
              ""sections"": [
                {
                  ""title"": ""1. Clothing Essentials"",
                  ""icon"": ""👕"",
                  ""introText"": ""New Zealand's weather can be unpredictable, so layering is your best friend."",
                  ""items"": [
                    ""Base layers: Moisture-wicking tops (merino wool is ideal)"",
                    ""Mid-layers: Fleece or down jacket for warmth"",
                    ""Outer layer: Waterproof and windproof jacket"",
                    ""Pants: Comfortable hiking pants and casual wear"",
                    ""Extras: Hat, gloves, scarf, and sunglasses""
                  ]
                },
                {
                  ""title"": ""2. Footwear"",
                  ""icon"": ""🥾"",
                  ""introText"": ""From hiking trails to city streets, the right footwear makes all the difference."",
                  ""items"": [
                    ""Hiking boots or trail shoes"",
                    ""Comfortable sneakers or casual shoes"",
                    ""Sandals or flip-flops (for beaches and hostels)""
                  ]
                },
                {
                    ""title"": ""3. Travel Accessories"",
                    ""icon"": ""🎒"",
                    ""introText"": ""Make your journey smoother with these handy items."",
                    ""items"": [
                        ""Daypack for daily excursions"",
                        ""Reusable water bottle"",
                        ""Travel adapter and chargers"",
                        ""Camera or smartphone for capturing memories"",
                        ""Travel documents: Passport, visa (if required), and travel insurance""
                    ]
                    },
                    {
                    ""title"": ""4. Health & Safety"",
                    ""icon"": ""💊"",
                    ""introText"": ""Stay healthy and safe during your adventures."",
                    ""items"": [
                        ""Basic first aid kit"",
                        ""Sunscreen and insect repellent"",
                        ""Prescription medications (if any)""
                    ]
                }
              ],
              ""finalTip"": ""Pack light, stay flexible, and be ready for anything!""
            }"
            }
            
        
        
        );
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Challenging",
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
                ActivityLevel = "Challenging",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Easy",
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
                ImageUrl = "assets/images/milford.png",
                Latitude = -44.6715,
                Longitude = 167.9255,
                Rating = 4.5m,
                ReviewCount = 415,
                BestTime = "Nov - Mar",
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Challenging",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Moderate",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Challenging",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
                ActivityLevel = "Easy",
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
