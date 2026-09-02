using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ContentJson = table.Column<string>(type: "jsonb", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    AuthorName = table.Column<string>(type: "text", nullable: false),
                    AuthorAvatar = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    ReadTime = table.Column<string>(type: "text", nullable: false),
                    ViewsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Islands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BudgetRange = table.Column<string>(type: "text", nullable: false),
                    TripStyle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IslandId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Islands_IslandId",
                        column: x => x.IslandId,
                        principalTable: "Islands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripDays_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    ReviewCount = table.Column<int>(type: "integer", nullable: false),
                    IsPopular = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    DestinationId = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    ReviewCount = table.Column<int>(type: "integer", nullable: false),
                    BestTime = table.Column<string>(type: "text", nullable: false),
                    ActivityLevel = table.Column<string>(type: "text", nullable: false),
                    AvailabilityNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RecommendedDuration = table.Column<string>(type: "text", nullable: false),
                    OpeningHoursNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    BookingNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SourceUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attractions_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DestinationCategories",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationCategories", x => new { x.DestinationId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_DestinationCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DestinationCategories_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttractionCategories",
                columns: table => new
                {
                    AttractionId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionCategories", x => new { x.AttractionId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_AttractionCategories_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttractionCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TripDayId = table.Column<int>(type: "integer", nullable: false),
                    AttractionId = table.Column<int>(type: "integer", nullable: true),
                    CustomName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PlannedDurationMinutes = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripStops_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripStops_TripDays_TripDayId",
                        column: x => x.TripDayId,
                        principalTable: "TripDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "AuthorAvatar", "AuthorName", "Category", "ContentJson", "Date", "Description", "ImageUrl", "ReadTime", "Title", "ViewsCount" },
                values: new object[,]
                {
                    { 1, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Destinations", "{\n                \"intro\": \"New Zealand is a land of breathtaking landscapes, rich culture, and unique wildlife. From the majestic fjords of the South Island to the geothermal wonders of the North Island, there's something for every traveler. Here are 6 must-visit places that will make your trip unforgettable.\",\n                \"sections\": [\n                    {\n                        \"title\": \"1. Milford Sound\",\n                        \"icon\": \"🌊\",\n                        \"introText\": \"Often referred to as the 'eighth wonder of the world', Milford Sound is a fjord in the southwest of New Zealand's South Island.\", \n                        \"items\": [\n                            \"Take a scenic cruise to witness towering cliffs and waterfalls.\",\n                            \"Kayak through the calm waters for a more intimate experience.\",\n                            \"Hike the Milford Track for stunning views of the surrounding mountains.\"\n                        ]\n                    },\n                    {\n                        \"title\": \"2. Rotorua\",\n                        \"icon\": \"🌋\",\n                        \"introText\": \"Known for its geothermal activity and Maori culture, Rotorua offers a unique experience.\",\n                        \"items\": [\n                            \"Visit the Wai-O-Tapu Thermal Wonderland to see colorful hot springs.\",\n                            \"Experience a traditional Maori hangi feast and cultural performance.\",\n                            \"Relax in the natural hot springs at Polynesian Spa.\"\n                        ]\n                    },\n                    {\n                        \"title\": \"3. Queenstown\",\n                        \"icon\": \"🏔️\",\n                        \"introText\": \"The adventure capital of New Zealand, Queenstown is set against the stunning Southern Alps.\",\n                        \"items\": [\n                            \"Try bungee jumping or skydiving for an adrenaline rush.\",\n                            \"Take a scenic gondola ride for panoramic views of Lake Wakatipu.\",\n                            \"Explore nearby vineyards and enjoy wine tasting tours.\"\n                        ]\n                    },\n                    {\n                        \"title\": \"4. Bay of Islands\",\n                        \"icon\": \"🏝️\",\n                        \"introText\": \"A subtropical region known for its beautiful beaches and historic sites.\",\n                        \"items\": [\n                            \"Take a boat tour to see the famous Hole in the Rock.\",\n                            \"Visit the Waitangi Treaty Grounds to learn about New Zealand's history.\",\n                            \"Enjoy water activities like sailing, fishing, and dolphin watching.\"\n                        ]\n                    },\n                    {\n                        \"title\": \"5. Franz Josef Glacier\",\n                        \"icon\": \"🧊\",\n                        \"introText\": \"One of the most accessible glaciers in the world, located on the West Coast of the South Island.\",\n                        \"items\": [\n                            \"Take a guided glacier hike or ice climbing tour.\",\n                            \"Helicopter tours offer breathtaking aerial views of the glacier.\",\n                            \"Relax in the nearby hot pools after your glacier adventure.\"\n                        ]\n                    },\n                    {\n                        \"title\": \"6. Hobbiton Movie Set\",\n                        \"icon\": \"🏡\",\n                        \"introText\": \"Step into the world of Middle-earth at the Hobbiton Movie Set in Matamata.\",\n                        \"items\": [\n                            \"Take a guided tour of the iconic movie set.\",\n                            \"Enjoy a drink at the Green Dragon Inn.\",\n                            \"Learn about the making of the Lord of the Rings and The Hobbit films.\"\n                        ]\n                    \n                    }\n                ],\n                \"finalTip\": \"Plan your itinerary across both islands carefully to experience a mix of vibrant city life, alpine adventures, and peaceful coastal landscapes!\"\n                }", "Sep 1, 2026", "From stunning fjords to geothermal wonders, discover the best places that should be on every traveller's list.", "assets/images/articles/nz-places.jpg", "6 min read", "6 Must-Visit Places in New Zealand", 0 },
                    { 2, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Travel Tips", "{\n              \"intro\": \"New Zealand's stunning landscapes and diverse climate mean packing smart is key to having an incredible trip. Whether you're hiking mountains, relaxing on beaches, or exploring vibrant cities, here's your ultimate packing guide.\",\n              \"sections\": [\n                {\n                  \"title\": \"1. Clothing Essentials\",\n                  \"icon\": \"👕\",\n                  \"introText\": \"New Zealand's weather can be unpredictable, so layering is your best friend.\",\n                  \"items\": [\n                    \"Base layers: Moisture-wicking tops (merino wool is ideal)\",\n                    \"Mid-layers: Fleece or down jacket for warmth\",\n                    \"Outer layer: Waterproof and windproof jacket\",\n                    \"Pants: Comfortable hiking pants and casual wear\",\n                    \"Extras: Hat, gloves, scarf, and sunglasses\"\n                  ]\n                },\n                {\n                  \"title\": \"2. Footwear\",\n                  \"icon\": \"🥾\",\n                  \"introText\": \"From hiking trails to city streets, the right footwear makes all the difference.\",\n                  \"items\": [\n                    \"Hiking boots or trail shoes\",\n                    \"Comfortable sneakers or casual shoes\",\n                    \"Sandals or flip-flops (for beaches and hostels)\"\n                  ]\n                },\n                {\n                    \"title\": \"3. Travel Accessories\",\n                    \"icon\": \"🎒\",\n                    \"introText\": \"Make your journey smoother with these handy items.\",\n                    \"items\": [\n                        \"Daypack for daily excursions\",\n                        \"Reusable water bottle\",\n                        \"Travel adapter and chargers\",\n                        \"Camera or smartphone for capturing memories\",\n                        \"Travel documents: Passport, visa (if required), and travel insurance\"\n                    ]\n                    },\n                    {\n                    \"title\": \"4. Health & Safety\",\n                    \"icon\": \"💊\",\n                    \"introText\": \"Stay healthy and safe during your adventures.\",\n                    \"items\": [\n                        \"Basic first aid kit\",\n                        \"Sunscreen and insect repellent\",\n                        \"Prescription medications (if any)\"\n                    ]\n                }\n              ],\n              \"finalTip\": \"Pack light, stay flexible, and be ready for anything!\"\n            }", "Sep 01, 2026", "What to pack for every season and adventure in New Zealand.", "assets/images/articles/packing.jpg", "4 min read", "Packing List for New Zealand", 0 },
                    { 3, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Road Trips", "{\n                \"intro\": \"The South Island of New Zealand is widely considered one of the best road trip destinations on earth. With dramatic mountain ranges, mirror-like lakes, and winding coastal highways, every mile brings a new breathtaking view. Here is your ultimate 5-day itinerary.\",\n                \"sections\": [\n                    {\n                        \"title\": \"Day 1-2: Christchurch to Lake Tekapo & Mount Cook\",\n                        \"icon\": \"🚗\",\n                        \"introText\": \"Begin your journey heading inland across the Canterbury Plains towards alpine lakes.\",\n                        \"items\": [\n                            \"Admire the striking turquoise waters of Lake Tekapo and visit the Church of the Good Shepherd\",\n                            \"Stargaze in the Aoraki Mackenzie International Dark Sky Reserve\",\n                            \"Take a short hike to view the majestic peaks of Mount Cook (Aoraki)\"\n                        ]\n                    },\n                    {\n                        \"title\": \"Day 3: Queenstown & Alpine Passes\",\n                        \"icon\": \"🏔️\",\n                        \"introText\": \"Drive through rugged mountain passes down into the global adventure capital\",\n                        \"items\": [\n                            \"Journey past the dramatic Kawarau Gorge and the historic Kawarau Bridge, home of the first commercial bungee jump\",\n                            \"Stop for photos at the historic Cromwell heritage precinct\",\n                            \"Settle into Queenstown for an evening by Lake Wakatipu\"\n                        ]\n                    },\n                    {\n                        \"title\": \"Day 4-5: Milford Sound & Coastal Return\",\n                        \"icon\": \"🌊\",\n                        \"introText\": \"Experience the deep blue fjords and lush rainforests of Fiordland National Park.\",\n                        \"items\": [\n                            \"Drive the Milford Road—one of the most scenic alpine drives in the world\",\n                            \"Take a midday cruise through Milford Sound to see majestic waterfalls and wildlife\",\n                            \"Complete your loop back north with a stop at coastal viewpoints\"\n                        ]\n                    }\n                ],\n                \"finalTip\": \"Ensure you book your vehicle rental and activities well in advance, especially if you are traveling during peak summer season!\"\n                }", "Sep 2, 2026", "A 3-day itinerary covering glaciers, lakes, and coastal drives you'll never forget.", "assets/images/articles/road-trip.jpg", "8 min read", "The Ultimate South Island Road Trip", 0 },
                    { 4, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Adventure", "{\n                    \"intro\": \"New Zealand is renowned for its adventure tourism, offering a wide range of activities for thrill-seekers. Here are the top 2 adventure activities you shouldn't miss during your visit.\",\n                    \"sections\": [\n                        {\n                            \"title\": \"1. Bungee Jumping\",\n                            \"icon\": \"🪂\",\n                            \"introText\": \"Experience the ultimate adrenaline rush by leaping off iconic bridges and platforms.\",\n                            \"items\": [\n                                \"Kawarau Bridge in Queenstown: The world's first commercial bungee jump.\",\n                                \"Nevis Bungy: One of the highest jumps in New Zealand at 134 meters.\"\n                            ]\n                        },\n                        {\n                            \"title\": \"2. Skydiving\",\n                            \"icon\": \"🪂\",\n                            \"introText\": \"Soar through the skies and enjoy breathtaking aerial views of New Zealand's landscapes.\",\n                            \"items\": [\n                                \"Queenstown: Jump over lakes and mountains for an unforgettable experience.\",\n                                \"Taupo: Skydive over Lake Taupo and the surrounding volcanic terrain.\"\n                            ]\n                        }\n                    ],\n                    \"finalTip\": \"Always ensure you choose reputable operators with certified safety standards for all adventure activities.\"\n                }", "Sep 2, 2026", "From bungee jumping to skydiving, discover the adrenaline-pumping experiences that make New Zealand a thrill-seeker's paradise.", "assets/images/articles/adventure.jpg", "7 min read", "Top 2 Adventure Activities in New Zealand", 0 },
                    { 5, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Off the Beaten Path", "{\n    \"intro\": \"While major cities and famous hot spots get most of the attention, the North Island hides incredible secret locations that few tourists ever manage to find. Escape the crowds and discover these pristine hidden gems.\",\n    \"sections\": [\n        {\n            \"title\": \"1. Cathedral Cove Alternatives: Secret Beaches of the Coromandel\",\n            \"icon\": \"🏖️\",\n            \"introText\": \"Skip the crowded paths and explore secluded coastal coves along the Pacific Coast Highway.\",\n            \"items\": [\n                \"Discover hidden swimming holes accessible only at low tide\",\n                \"Explore untouched golden sand beaches surrounded by native pohutukawa trees\",\n                \"Pack a picnic to enjoy uninterrupted ocean horizons\"\n            ]\n        },\n        {\n            \"title\": \"2. The Forgotten World Highway (SH43)\",\n            \"icon\": \"🚗\",\n            \"introText\": \"A historic, winding road that takes you deep into New Zealand's rugged rural history.\",\n            \"items\": [\n                \"Drive through the eerie, hand-carved Moki Tunnel\",\n                \"Explore the self-proclaimed Republic of Whangamomona\",\n                \"Take in sweeping valley views from remote mountain saddles\"\n            ]\n        },\n        {\n            \"title\": \"3. Taranaki's Secret Waterfall Tracks\",\n            \"icon\": \"🌿\",\n            \"introText\": \"Step into an ancient, moss-draped goblin forest beneath Mount Taranaki.\",\n            \"items\": [\n                \"Walk the enchanting delayed-exposure photography tracks\",\n                \"Listen to native bird song in untouched ecological sanctuaries\",\n                \"Capture stunning reflections of volcanic peaks in quiet alpine tarns\"\n            ]\n        }\n    ],\n    \"finalTip\": \"Always check local weather and tide charts before exploring secluded coastal tracks on the North Island!\"\n}", "Sep 1, 2026", "Discover the North Island's lesser-known landscapes, from secluded Coromandel beaches and the historic Forgotten World Highway to enchanting forests and waterfalls around Taranaki.", "assets/images/articles/north-gem.jpg", "5 min read", "Hidden Gems of the North Island", 0 },
                    { 6, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Food & Wine", "{\n    \"intro\": \"New Zealand's culinary scene is a delightful fusion of indigenous Maori flavors, European influences, and fresh local ingredients. From world-class wines to farm-to-table dining experiences, here's a guide to the best food and wine experiences across the country.\",\n    \"sections\": [\n        {\n            \"title\": \"1. Traditional Māori Hāngi\",\n            \"icon\": \"🔥\",\n            \"introText\": \"An ancient cooking method where food is slow-cooked underground on hot stones.\",\n            \"items\": [\n                \"Savor tender chicken, pork, and root vegetables infused with a rich, earthy flavor\",\n                \"Learn about the cultural significance of sharing food in community gatherings\",\n                \"Best experienced through guided cultural tours in Rotorua\"\n            ]\n        },\n        {\n            \"title\": \"2. World-Class Seafood & Bluff Oysters\",\n            \"icon\": \"🦪\",\n            \"introText\": \"Surrounded by ocean, New Zealand offers some of the freshest seafood on the planet.\",\n            \"items\": [\n                \"Taste famous wild Bluff oysters during their seasonal harvest\",\n                \"Try classic New Zealand green-lipped mussels steamed in white wine\",\n                \"Indulge in fresh crayfish (rock lobster) along the Kaikoura coast\"\n            ]\n        },\n        {\n            \"title\": \"3. Wine Regions to Explore\",\n            \"icon\": \"🍷\",\n            \"introText\": \"New Zealand is renowned for its vineyards, producing some of the world's best Sauvignon Blanc and Pinot Noir.\",\n            \"items\": [\n                \"Marlborough: Famous for crisp Sauvignon Blancs and scenic vineyard tours.\",\n                \"Central Otago: Known for its award-winning Pinot Noir and stunning alpine landscapes.\",\n                \"Hawke's Bay: Offers a diverse range of wines, including Merlot and Syrah, along with gourmet food experiences.\"\n            ]\n        },\n        {\n            \"title\": \"4. Must-Try Local Dishes\",\n            \"icon\": \"🍽️\",\n            \"introText\": \"Experience the unique flavors of New Zealand through its traditional and contemporary dishes.\",\n            \"items\": [\n                \"Hāngi: A traditional Maori method of cooking food in an earth oven, resulting in tender and flavorful meats and vegetables.\",\n                \"Pavlova: A meringue-based dessert topped with fresh fruits, named after the Russian ballerina Anna Pavlova.\",\n                \"Green-lipped Mussels: A local seafood delicacy, often served steamed or in a creamy sauce.\"\n            ]\n        },\n        {\n            \"title\": \"5. Iconic Kiwi Sweets & Coffee Culture\",\n            \"icon\": \"☕\",\n            \"introText\": \"Fuel your road trips with exceptional flat whites and legendary local treats.\",\n            \"items\": [\n                \"Order a classic 'flat white' at any local artisanal café\",\n                \"Try iconic Hokey Pokey ice cream (vanilla with crunchy honeycomb chunks)\",\n                \"Snack on classic Anzac biscuits baked fresh daily\"\n            ]\n        }\n        ],\n        \"finalTip\": \"Pair your evening meals with a glass of world-renowned Marlborough Sauvignon Blanc or Central Otago Pinot Noir!\"\n    }", "Jul 31, 2026", "Taste your way through New Zealand with traditional Māori hāngi, fresh seafood, world-class wines, iconic Kiwi treats and a thriving café culture.", "assets/images/articles/food-wine.jpg", "6 min read", "Culinary Delights of New Zealand", 0 },
                    { 7, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Destinations", "{\n        \"intro\": \"New Zealand is famous for beautiful scenery, but some parts of the country look almost otherworldly. Volcanic craters, limestone caves, unusual coastal formations and remote islands have created landscapes that feel completely different from one another. Here are six places where the scenery itself becomes the main attraction.\",\n        \"sections\": [\n            {\n                \"title\": \"1. Tongariro National Park\",\n                \"icon\": \"🌋\",\n                \"introText\": \"An extraordinary volcanic landscape filled with alpine terrain, volcanic craters, emerald-coloured lakes and dramatic mountains.\",\n                \"items\": [\n                    \"Walk the Tongariro Alpine Crossing when weather and track conditions are suitable.\",\n                    \"See the striking Emerald Lakes and volcanic landscape around the crossing.\",\n                    \"Consider the Tama Lakes Track for another way to experience the volcanic scenery.\"\n                ]\n            },\n            {\n                \"title\": \"2. Waitomo Cave Country\",\n                \"icon\": \"🪨\",\n                \"introText\": \"Beneath the green farmland of Waikato lies a remarkable underground world of limestone caves and glowworms.\",\n                \"items\": [\n                    \"Take a guided tour through the famous Waitomo cave systems.\",\n                    \"See glowworms illuminating the darkness above the underground waterways.\",\n                    \"Choose from relaxed cave tours or more adventurous underground experiences.\"\n                ]\n            },\n            {\n                \"title\": \"3. Putangirua Pinnacles\",\n                \"icon\": \"🏜️\",\n                \"introText\": \"Thousands of years of erosion have created dramatic columns of rock in the Aorangi Range near the southern Wairarapa coast.\",\n                \"items\": [\n                    \"Walk the Putangirua Pinnacles Track through the rugged landscape.\",\n                    \"Follow the riverbed toward the towering formations.\",\n                    \"Recognise the landscape featured in The Lord of the Rings films.\"\n                ]\n            },\n            {\n                \"title\": \"4. Punakaiki Pancake Rocks\",\n                \"icon\": \"🌊\",\n                \"introText\": \"Along the West Coast, layers of limestone have been shaped by the sea into unusual formations resembling enormous stacks of pancakes.\",\n                \"items\": [\n                    \"Walk the Pancake Rocks and Blowholes Track.\",\n                    \"Visit around high tide when wave action can make the blowholes especially dramatic.\",\n                    \"Combine the visit with the surrounding rainforest and rugged West Coast scenery.\"\n                ]\n            },\n            {\n                \"title\": \"5. Castlepoint\",\n                \"icon\": \"🏖️\",\n                \"introText\": \"A distinctive Wairarapa coastline where cliffs, limestone formations, beaches and the Pacific Ocean meet.\",\n                \"items\": [\n                    \"Walk toward the historic Castlepoint Lighthouse.\",\n                    \"Explore the unusual coastal rock formations around Castlepoint.\",\n                    \"Climb toward Castle Rock for panoramic views of the coastline.\"\n                ]\n            },\n            {\n                \"title\": \"6. Rakiura / Stewart Island\",\n                \"icon\": \"🌌\",\n                \"introText\": \"New Zealand's third-largest island offers remote forests, dramatic coastline, native wildlife and exceptionally dark night skies.\",\n                \"items\": [\n                    \"Explore parts of Rakiura National Park on foot.\",\n                    \"Look for native birds and wildlife in the island's natural environment.\",\n                    \"Experience the island after dark beneath its famous southern night sky.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"New Zealand's most unusual landscapes are often best experienced slowly. Leave time for short walks, viewpoints and unexpected stops instead of rushing from one attraction to another.\"\n    }", "Sep 2, 2026", "From volcanic craters to underground glowworm caves, discover six extraordinary landscapes that show just how diverse New Zealand can be.", "assets/images/articles/otherworldly-nz.jpg", "7 min read", "6 New Zealand Landscapes That Feel Like Another Planet", 0 },
                    { 8, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Travel Tips", "{\n        \"intro\": \"New Zealand can look easy to travel around on a map, but first-time visitors can quickly discover that the reality is different. Long driving days, changing weather, remote hiking areas and popular attractions that require advance planning can all affect your trip. Here are ten common mistakes worth avoiding.\",\n        \"sections\": [\n            {\n                \"title\": \"1. Underestimating Driving Times\",\n                \"icon\": \"🚗\",\n                \"introText\": \"Road distances can look surprisingly short on a map, but New Zealand roads often wind through hills, mountains and coastal terrain.\",\n                \"items\": [\n                    \"Allow more time than the map distance alone suggests.\",\n                    \"Remember that scenic roads often include many worthwhile stopping points.\",\n                    \"Take regular breaks during long drives rather than trying to reach the next destination as quickly as possible.\"\n                ]\n            },\n            {\n                \"title\": \"2. Trying to See Everything\",\n                \"icon\": \"🗺️\",\n                \"introText\": \"New Zealand may look compact compared with some countries, but trying to cover too much can turn a holiday into a long series of drives.\",\n                \"items\": [\n                    \"Choose a few priority destinations rather than trying to see every famous attraction.\",\n                    \"Allow enough time to explore each region.\",\n                    \"Consider focusing on one island if your trip is short.\"\n                ]\n            },\n            {\n                \"title\": \"3. Trusting the Weather Too Much\",\n                \"icon\": \"🌦️\",\n                \"introText\": \"New Zealand's weather can change quickly, particularly in alpine and coastal environments.\",\n                \"items\": [\n                    \"Check the forecast before outdoor activities.\",\n                    \"Carry a waterproof and windproof layer even when the morning looks sunny.\",\n                    \"Be prepared to change plans when conditions become unsafe.\"\n                ]\n            },\n            {\n                \"title\": \"4. Forgetting Sun Protection\",\n                \"icon\": \"☀️\",\n                \"introText\": \"Outdoor adventures can mean spending many hours exposed to the sun, even when the temperature does not feel extremely hot.\",\n                \"items\": [\n                    \"Carry sunscreen and reapply it during long outdoor activities.\",\n                    \"Wear sunglasses and a hat when appropriate.\",\n                    \"Take extra care during long hikes, beach days and water activities.\"\n                ]\n            },\n            {\n                \"title\": \"5. Treating Every Great Walk as an Easy Walk\",\n                \"icon\": \"🥾\",\n                \"introText\": \"New Zealand's Great Walks are famous, but they are still multi-day outdoor adventures that require preparation.\",\n                \"items\": [\n                    \"Check the official track information before starting.\",\n                    \"Understand the distance, terrain and expected conditions.\",\n                    \"Book huts or campsites where required and carry suitable equipment.\"\n                ]\n            },\n            {\n                \"title\": \"6. Leaving Popular Bookings Until the Last Minute\",\n                \"icon\": \"📅\",\n                \"introText\": \"Popular walks, accommodation and activities can become difficult to book during busy travel periods.\",\n                \"items\": [\n                    \"Book popular Great Walk huts and campsites ahead of time.\",\n                    \"Reserve rental vehicles and accommodation before arriving during busy periods.\",\n                    \"Check activity availability before building your itinerary around it.\"\n                ]\n            },\n            {\n                \"title\": \"7. Packing Only for the Season\",\n                \"icon\": \"🧥\",\n                \"introText\": \"New Zealand's seasons provide a useful guide, but local conditions can vary significantly between regions.\",\n                \"items\": [\n                    \"Pack layers rather than relying on one heavy item.\",\n                    \"Carry a light waterproof layer for changeable conditions.\",\n                    \"Remember that alpine areas can be much colder than nearby towns.\"\n                ]\n            },\n            {\n                \"title\": \"8. Depending Completely on Mobile Coverage\",\n                \"icon\": \"📱\",\n                \"introText\": \"Remote roads and hiking areas may have limited or no mobile reception.\",\n                \"items\": [\n                    \"Download maps before leaving towns and cities.\",\n                    \"Tell someone about your plans when heading into remote areas.\",\n                    \"Do not rely entirely on your phone for navigation or emergency information.\"\n                ]\n            },\n            {\n                \"title\": \"9. Ignoring Māori Culture and Place Names\",\n                \"icon\": \"🌿\",\n                \"introText\": \"Travelling through Aotearoa is also an opportunity to learn about Māori history, culture and the meaning behind many place names.\",\n                \"items\": [\n                    \"Learn the pronunciation of Māori place names where possible.\",\n                    \"Read about the cultural significance of places you visit.\",\n                    \"Respect local cultural sites and follow visitor guidance.\"\n                ]\n            },\n            {\n                \"title\": \"10. Rushing the Journey\",\n                \"icon\": \"⏳\",\n                \"introText\": \"Some of New Zealand's best travel moments happen between the major attractions.\",\n                \"items\": [\n                    \"Leave space in your itinerary for unexpected stops.\",\n                    \"Take short walks and scenic detours when time allows.\",\n                    \"Spend time in smaller towns rather than treating them only as overnight stops.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"Build flexibility into your itinerary. A good New Zealand trip is not just about reaching every destination—it is about having enough time to enjoy the journey between them.\"\n    }", "Sep 1, 2026", "Avoid common travel mistakes with practical lessons about driving, weather, hiking, bookings, safety and travelling around Aotearoa.", "assets/images/articles/nz-travel-mistakes.jpg", "7 min read", "10 Things First-Time Visitors Get Wrong About New Zealand", 0 },
                    { 9, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Road Trips", "{\n        \"intro\": \"The best New Zealand road trips are not always about reaching the next city as quickly as possible. Instead, build your journey around the landscapes. This scenery-first route connects volcanic country, alpine lakes, mountains, forests and dramatic waterways into one memorable Kiwi adventure.\",\n        \"sections\": [\n            {\n                \"title\": \"1. Auckland to Rotorua\",\n                \"icon\": \"🚐\",\n                \"introText\": \"Begin in Auckland and head south toward Rotorua, where the landscape changes from urban life to geothermal country.\",\n                \"items\": [\n                    \"Explore Rotorua's geothermal areas and steaming landscapes.\",\n                    \"Experience Māori culture through reputable local cultural experiences.\",\n                    \"Explore the lakes and forests surrounding Rotorua.\"\n                ]\n            },\n            {\n                \"title\": \"2. Rotorua to Lake Taupō\",\n                \"icon\": \"🌋\",\n                \"introText\": \"Continue south toward Lake Taupō and discover one of the largest volcanic landscapes in the North Island.\",\n                \"items\": [\n                    \"Stop to see the powerful Huka Falls.\",\n                    \"Enjoy views around Lake Taupō.\",\n                    \"Take time to explore the surrounding volcanic region.\"\n                ]\n            },\n            {\n                \"title\": \"3. Lake Taupō to Tongariro\",\n                \"icon\": \"🥾\",\n                \"introText\": \"Head toward Tongariro National Park for a dramatic change from lake country to alpine volcanic terrain.\",\n                \"items\": [\n                    \"Walk the Tongariro Alpine Crossing when conditions and experience are suitable.\",\n                    \"Consider the Tama Lakes Track for another volcanic landscape experience.\",\n                    \"Spend extra time around Whakapapa Village and Tongariro National Park.\"\n                ]\n            },\n            {\n                \"title\": \"4. Across to the South Island\",\n                \"icon\": \"⛴️\",\n                \"introText\": \"For a longer journey, continue south and cross to the South Island, where the landscapes become increasingly alpine.\",\n                \"items\": [\n                    \"Allow enough time for the journey between the North and South Islands.\",\n                    \"Plan accommodation around your major stops rather than trying to drive continuously.\",\n                    \"Keep the itinerary flexible around weather and transport schedules.\"\n                ]\n            },\n            {\n                \"title\": \"5. Aoraki / Mount Cook & Mackenzie Country\",\n                \"icon\": \"🏔️\",\n                \"introText\": \"Travel through the open landscapes of Mackenzie Country toward Aoraki / Mount Cook.\",\n                \"items\": [\n                    \"Enjoy the striking colours of the alpine lakes.\",\n                    \"Take a short walk in Aoraki / Mount Cook National Park.\",\n                    \"Spend time under the dark skies of the Mackenzie region when conditions allow.\"\n                ]\n            },\n            {\n                \"title\": \"6. Queenstown to Fiordland\",\n                \"icon\": \"🌊\",\n                \"introText\": \"Continue through Central Otago toward Queenstown before heading into the dramatic landscapes of Fiordland.\",\n                \"items\": [\n                    \"Drive through the mountain scenery around Queenstown and Central Otago.\",\n                    \"Allow plenty of time for stops along the Milford Road.\",\n                    \"Take a cruise on Milford Sound / Piopiotahi to experience the fiord from the water.\"\n                ]\n            },\n            {\n                \"title\": \"7. Return Through Central Otago\",\n                \"icon\": \"🍂\",\n                \"introText\": \"Complete the journey through the dry inland landscapes and historic towns of Central Otago.\",\n                \"items\": [\n                    \"Explore historic gold-mining communities.\",\n                    \"Visit local vineyards and cellar doors where available.\",\n                    \"Slow down for roadside viewpoints and small-town discoveries.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"Plan the major overnight stops in advance, but do not fill every hour of every day. The scenery, small detours and unexpected stops are what make a New Zealand road trip memorable.\"\n    }", "Jul 30, 2026", "Follow a scenery-first New Zealand road trip through volcanic country, alpine lakes, dramatic mountains, forests and remote coastlines.", "assets/images/articles/great-kiwi-loop.jpg", "8 min read", "The Great Kiwi Loop: A Road Trip Built Around Landscapes", 0 },
                    { 10, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Adventure", "{\n        \"intro\": \"New Zealand's adventure reputation goes far beyond bungee jumping and skydiving. Rivers, caves, forests, mountains and coastlines create opportunities to experience the country's landscapes from completely different perspectives. Here are seven adventures worth adding to your Kiwi itinerary.\",\n        \"sections\": [\n            {\n                \"title\": \"1. White-Water Rafting\",\n                \"icon\": \"🌊\",\n                \"introText\": \"Get into New Zealand's rivers and experience the landscape from the water.\",\n                \"items\": [\n                    \"Choose a guided rafting trip that matches your experience level.\",\n                    \"Experience rapids while travelling through river valleys and native landscapes.\",\n                    \"Follow the operator's safety instructions and equipment requirements.\"\n                ]\n            },\n            {\n                \"title\": \"2. Jet Boating\",\n                \"icon\": \"🚤\",\n                \"introText\": \"Jet boats turn New Zealand's rivers into high-speed adventure routes through narrow valleys and dramatic scenery.\",\n                \"items\": [\n                    \"Experience fast turns and rapid acceleration on specially designed river routes.\",\n                    \"Enjoy views of mountains, cliffs and native forest from the water.\",\n                    \"Choose an established operator with appropriate safety procedures.\"\n                ]\n            },\n            {\n                \"title\": \"3. Black-Water Rafting\",\n                \"icon\": \"🕳️\",\n                \"introText\": \"In the Waitomo region, underground adventure combines caves, waterways and glowworms.\",\n                \"items\": [\n                    \"Travel through underground cave passages with a specialist guide.\",\n                    \"Float through sections of underground river in complete darkness.\",\n                    \"Look up at glowworms while travelling through the cave system.\"\n                ]\n            },\n            {\n                \"title\": \"4. Canyoning\",\n                \"icon\": \"🧗\",\n                \"introText\": \"Canyoning combines walking, climbing, swimming and abseiling in New Zealand's natural waterways.\",\n                \"items\": [\n                    \"Move through narrow canyons and natural rock formations.\",\n                    \"Abseil alongside waterfalls on suitable guided trips.\",\n                    \"Swim through natural pools and explore areas inaccessible by normal walking tracks.\"\n                ]\n            },\n            {\n                \"title\": \"5. Mountain Biking\",\n                \"icon\": \"🚵\",\n                \"introText\": \"New Zealand offers cycling experiences ranging from relaxed rail trails to challenging mountain-bike terrain.\",\n                \"items\": [\n                    \"Explore purpose-built mountain-bike parks.\",\n                    \"Ride scenic rail trails through countryside and historic regions.\",\n                    \"Combine cycling with local cafés, towns and food stops.\"\n                ]\n            },\n            {\n                \"title\": \"6. Kayaking & Sea Adventures\",\n                \"icon\": \"🛶\",\n                \"introText\": \"Kayaking provides a quieter form of adventure and a completely different view of New Zealand's coastline.\",\n                \"items\": [\n                    \"Paddle beside coastal cliffs, beaches and islands.\",\n                    \"Explore sheltered waterways and marine environments with a local guide.\",\n                    \"Combine kayaking with walking in coastal areas such as Abel Tasman National Park.\"\n                ]\n            },\n            {\n                \"title\": \"7. Alpine Adventures\",\n                \"icon\": \"🏔️\",\n                \"introText\": \"New Zealand's alpine environments offer some of the country's most challenging outdoor experiences.\",\n                \"items\": [\n                    \"Try guided alpine experiences suitable for your skill level.\",\n                    \"Explore mountain landscapes through experienced local operators.\",\n                    \"Check weather and alpine conditions carefully before entering exposed terrain.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"Choose an adventure that matches your experience rather than simply choosing the activity with the biggest adrenaline factor. The best adventure is one where you feel challenged, excited and prepared.\"\n    }", "Sep 2, 2026", "Go beyond the famous jumps with rafting, jet boating, caving, kayaking, mountain biking and other unforgettable Kiwi adventures.", "assets/images/articles/beyond-bungee.jpg", "8 min read", "Beyond Bungee: 7 Adventures That Show New Zealand Differently", 0 },
                    { 11, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Off the Beaten Path", "{\n        \"intro\": \"Going off the beaten path in New Zealand does not necessarily mean discovering a completely unknown place. Sometimes it simply means taking the slower road, staying longer in a small community or choosing a region that receives less attention than the country's biggest tourist destinations. These six places reward travellers who are willing to slow down.\",\n        \"sections\": [\n            {\n                \"title\": \"1. Ōpōtiki & the Eastern Bay of Plenty\",\n                \"icon\": \"🌊\",\n                \"introText\": \"The Eastern Bay of Plenty combines Pacific coastline, forests, rivers and strong Māori cultural connections.\",\n                \"items\": [\n                    \"Explore the coastline around Ōpōtiki.\",\n                    \"Discover local walking and cycling opportunities.\",\n                    \"Use the town as a base for exploring the surrounding Eastern Bay of Plenty.\"\n                ]\n            },\n            {\n                \"title\": \"2. Whanganui River Country\",\n                \"icon\": \"🛶\",\n                \"introText\": \"The Whanganui River passes through remote hills and bush-clad valleys, creating one of New Zealand's most distinctive slow-travel experiences.\",\n                \"items\": [\n                    \"Experience the Whanganui Journey by canoe or kayak with suitable preparation.\",\n                    \"Travel through Whanganui National Park and its remote river landscapes.\",\n                    \"Stay overnight along the river during a multi-day journey.\"\n                ]\n            },\n            {\n                \"title\": \"3. Golden Bay\",\n                \"icon\": \"🌿\",\n                \"introText\": \"At the northern end of the South Island, Golden Bay offers beaches, forests, limestone landscapes and a noticeably slower pace.\",\n                \"items\": [\n                    \"Explore the coastline and beaches around the region.\",\n                    \"Discover limestone landscapes and nearby natural attractions.\",\n                    \"Use the area as a gateway to parts of Kahurangi National Park.\"\n                ]\n            },\n            {\n                \"title\": \"4. Westport & the Buller Coast\",\n                \"icon\": \"🌊\",\n                \"introText\": \"The Buller Coast combines rugged beaches, rivers, native forest and a strong connection to the West Coast's mining history.\",\n                \"items\": [\n                    \"Explore the coastline around Westport.\",\n                    \"Discover local history and former mining communities.\",\n                    \"Use Westport as a base for exploring the wider Buller region.\"\n                ]\n            },\n            {\n                \"title\": \"5. The Catlins\",\n                \"icon\": \"🌳\",\n                \"introText\": \"Located in the far south, the Catlins combines native forest, waterfalls, rugged beaches and dramatic coastal scenery.\",\n                \"items\": [\n                    \"Stop at waterfalls and short walking tracks along the coast.\",\n                    \"Explore the region's native forest and coastal landscapes.\",\n                    \"Allow enough time to stop rather than treating the Catlins as a simple drive-through route.\"\n                ]\n            },\n            {\n                \"title\": \"6. Rakiura / Stewart Island\",\n                \"icon\": \"🌌\",\n                \"introText\": \"Far south of the mainland, Stewart Island offers remote forests, coastal walks, native wildlife and a slower rhythm of travel.\",\n                \"items\": [\n                    \"Explore Rakiura National Park on foot.\",\n                    \"Look for native birds in their natural environment.\",\n                    \"Experience the island's exceptionally dark night skies.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"Going off the beaten path is not about keeping places secret. Travel responsibly, support local communities and give yourself enough time to appreciate places that reward patience.\"\n    }", "Sep 3, 2026", "Escape the busiest tourist routes and discover quieter coastlines, river valleys, forests and small communities across New Zealand.", "assets/images/articles/quiet-corners.jpg", "7 min read", "6 Quiet Corners of New Zealand Worth Taking the Long Way To", 0 },
                    { 12, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Food & Wine", "{\n        \"intro\": \"New Zealand's food culture is closely connected to its seasons and regions. Instead of asking only what food you should try, ask what is fresh where you are travelling. From summer seafood and grape harvests to autumn produce and seasonal oysters, food can become part of the journey itself.\",\n        \"sections\": [\n            {\n                \"title\": \"1. Summer Seafood\",\n                \"icon\": \"🦪\",\n                \"introText\": \"New Zealand's summer months bring warm weather and plenty of opportunities to enjoy fresh coastal seafood.\",\n                \"items\": [\n                    \"Look for seasonal seafood at local restaurants and fish markets.\",\n                    \"Try kina, a native sea urchin, where it is locally available and legally sourced.\",\n                    \"Enjoy seafood outdoors when visiting coastal regions during summer.\"\n                ]\n            },\n            {\n                \"title\": \"2. New Zealand Wine Harvest\",\n                \"icon\": \"🍇\",\n                \"introText\": \"Late summer and early autumn coincide with grape harvesting across many of New Zealand's wine regions.\",\n                \"items\": [\n                    \"Explore vineyard regions such as Marlborough, Hawke's Bay and Central Otago.\",\n                    \"Visit cellar doors and learn how regional conditions influence the wines.\",\n                    \"Look for local wine and food events during the harvest period.\"\n                ]\n            },\n            {\n                \"title\": \"3. Bluff Oysters\",\n                \"icon\": \"🦪\",\n                \"introText\": \"Bluff oysters are one of New Zealand's best-known seasonal seafood specialties and are strongly associated with Southland.\",\n                \"items\": [\n                    \"Try Bluff oysters during their seasonal availability.\",\n                    \"Look for them at restaurants and seafood venues in Southland.\",\n                    \"Pair the experience with a journey through the southern regions of New Zealand.\"\n                ]\n            },\n            {\n                \"title\": \"4. Autumn Harvests\",\n                \"icon\": \"🍎\",\n                \"introText\": \"Autumn brings fresh harvests of apples, kūmara and other produce to markets and kitchens around the country.\",\n                \"items\": [\n                    \"Visit farmers' markets to discover seasonal local produce.\",\n                    \"Look for apples and other autumn fruit when travelling through growing regions.\",\n                    \"Try seasonal dishes featuring freshly harvested ingredients.\"\n                ]\n            },\n            {\n                \"title\": \"5. Farmers' Markets\",\n                \"icon\": \"🧺\",\n                \"introText\": \"Farmers' markets are one of the easiest ways to discover what a region produces locally.\",\n                \"items\": [\n                    \"Look for locally grown fruit and vegetables.\",\n                    \"Try locally produced bread, cheese, honey and preserves.\",\n                    \"Ask vendors about where the ingredients were grown or produced.\"\n                ]\n            },\n            {\n                \"title\": \"6. Wine Regions With Local Food\",\n                \"icon\": \"🍷\",\n                \"introText\": \"New Zealand's wine regions offer more than tastings, with many combining vineyards, local produce, restaurants and scenic landscapes.\",\n                \"items\": [\n                    \"Visit Marlborough for Sauvignon Blanc and vineyard experiences.\",\n                    \"Explore Central Otago for Pinot Noir and dramatic inland scenery.\",\n                    \"Discover Hawke's Bay for wine, vineyard dining and regional produce.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"The best food experience is often the one connected to where you are. Follow the season, buy locally, ask questions and let the region influence what ends up on your plate.\"\n    }", "Sep 3, 2026", "Taste New Zealand through its seasons with fresh seafood, local produce, wine harvests, farmers' markets and regional specialties.", "assets/images/articles/seasonal-nz-food.jpg", "7 min read", "Aotearoa by Season: 6 Food Experiences Worth Planning Around", 0 },
                    { 13, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Travel Tips", "{\n        \"intro\": \"Renting a car is one of the most popular ways to explore New Zealand, but it is not the only option. Travellers can combine buses, scenic trains, ferries, local public transport and guided tours to experience Aotearoa without spending their entire holiday behind the wheel. For some visitors, travelling without a car can make the journey slower, easier and more relaxing.\",\n        \"sections\": [\n            {\n                \"title\": \"1. Use Scenic Trains for the Big Journeys\",\n                \"icon\": \"🚆\",\n                \"introText\": \"New Zealand's scenic rail journeys turn the trip itself into part of the experience.\",\n                \"items\": [\n                    \"Take the Northern Explorer between Auckland and Wellington through the North Island's volcanic and rural landscapes.\",\n                    \"Travel the Coastal Pacific between Christchurch and Picton along the Kaikōura coastline and through Marlborough.\",\n                    \"Cross the Southern Alps on the TranzAlpine between Christchurch and Greymouth.\"\n                ]\n            },\n            {\n                \"title\": \"2. Connect the Islands by Ferry\",\n                \"icon\": \"⛴️\",\n                \"introText\": \"You can travel between the North and South Islands without putting a rental car on the ferry.\",\n                \"items\": [\n                    \"Take a Cook Strait ferry between Wellington and Picton.\",\n                    \"Spend the crossing enjoying views of the Marlborough Sounds and Cook Strait.\",\n                    \"Combine ferry travel with trains or coaches on either side of the crossing.\"\n                ]\n            },\n            {\n                \"title\": \"3. Let Intercity Buses Do the Driving\",\n                \"icon\": \"🚌\",\n                \"introText\": \"Long-distance coach services can connect major towns and cities while you relax and enjoy the scenery.\",\n                \"items\": [\n                    \"Use buses for regional connections that are not covered by scenic rail.\",\n                    \"Plan accommodation around your arrival points rather than trying to cover too much in one day.\",\n                    \"Use the travel time to rest, read or enjoy the scenery instead of concentrating on the road.\"\n                ]\n            },\n            {\n                \"title\": \"4. Explore Cities With Local Transport\",\n                \"icon\": \"🚏\",\n                \"introText\": \"A car is often unnecessary once you arrive in New Zealand's larger cities.\",\n                \"items\": [\n                    \"Use local buses and other public transport to explore urban areas.\",\n                    \"Walk between attractions where practical.\",\n                    \"Look for accommodation close to central transport connections.\"\n                ]\n            },\n            {\n                \"title\": \"5. Use Guided Day Trips for Hard-to-Reach Places\",\n                \"icon\": \"🗺️\",\n                \"introText\": \"Some of New Zealand's most famous experiences are easier to visit as part of an organised day trip.\",\n                \"items\": [\n                    \"Choose guided excursions to destinations that are difficult to reach independently without a vehicle.\",\n                    \"Use local operators for activities where local knowledge adds value.\",\n                    \"Treat the journey as part of the experience rather than simply trying to reach the destination.\"\n                ]\n            },\n            {\n                \"title\": \"6. Build Your Trip Around Transport Hubs\",\n                \"icon\": \"📍\",\n                \"introText\": \"The easiest car-free itineraries are designed around places with strong transport connections.\",\n                \"items\": [\n                    \"Choose major towns as bases for regional exploration.\",\n                    \"Check train, bus and ferry schedules before booking accommodation.\",\n                    \"Allow extra time between connections instead of creating tight same-day transfers.\"\n                ]\n            },\n            {\n                \"title\": \"7. Turn Slow Travel Into the Experience\",\n                \"icon\": \"🌿\",\n                \"introText\": \"Travelling without a car can change the pace of a New Zealand holiday and make the journey itself more memorable.\",\n                \"items\": [\n                    \"Watch mountains, coastlines and farmland pass by instead of focusing on navigation.\",\n                    \"Use travel days as opportunities to rest rather than treating them as wasted time.\",\n                    \"Spend longer in fewer destinations instead of constantly moving to the next stop.\"\n                ]\n            }\n        ],\n        \"finalTip\": \"You do not need to drive every kilometre to experience New Zealand. Combine trains, buses, ferries, local transport and guided experiences to build a slower journey that lets you enjoy more of the scenery and less of the stress.\"\n    }", "Sep 3, 2026", "Discover a smarter way to explore Aotearoa using scenic trains, buses, ferries, local transport and guided trips instead of driving everywhere.", "assets/images/articles/nz-without-car.jpg", "7 min read", "How to Travel Around New Zealand Without Renting a Car", 0 },
                    { 14, "assets/images/wanderkiwi-logo.png", "WanderKiwi AI", "Travel Tips", "{\n              \"intro\": \"New Zealand's stunning landscapes and diverse climate mean packing smart is key to having an incredible trip. Whether you're hiking mountains, relaxing on beaches, or exploring vibrant cities, here's your ultimate packing guide.\",\n              \"sections\": [\n                {\n                  \"title\": \"1. Clothing Essentials\",\n                  \"icon\": \"👕\",\n                  \"introText\": \"New Zealand's weather can be unpredictable, so layering is your best friend.\",\n                  \"items\": [\n                    \"Base layers: Moisture-wicking tops (merino wool is ideal)\",\n                    \"Mid-layers: Fleece or down jacket for warmth\",\n                    \"Outer layer: Waterproof and windproof jacket\",\n                    \"Pants: Comfortable hiking pants and casual wear\",\n                    \"Extras: Hat, gloves, scarf, and sunglasses\"\n                  ]\n                },\n                {\n                  \"title\": \"2. Footwear\",\n                  \"icon\": \"🥾\",\n                  \"introText\": \"From hiking trails to city streets, the right footwear makes all the difference.\",\n                  \"items\": [\n                    \"Hiking boots or trail shoes\",\n                    \"Comfortable sneakers or casual shoes\",\n                    \"Sandals or flip-flops (for beaches and hostels)\"\n                  ]\n                },\n                {\n                    \"title\": \"3. Travel Accessories\",\n                    \"icon\": \"🎒\",\n                    \"introText\": \"Make your journey smoother with these handy items.\",\n                    \"items\": [\n                        \"Daypack for daily excursions\",\n                        \"Reusable water bottle\",\n                        \"Travel adapter and chargers\",\n                        \"Camera or smartphone for capturing memories\",\n                        \"Travel documents: Passport, visa (if required), and travel insurance\"\n                    ]\n                    },\n                    {\n                    \"title\": \"4. Health & Safety\",\n                    \"icon\": \"💊\",\n                    \"introText\": \"Stay healthy and safe during your adventures.\",\n                    \"items\": [\n                        \"Basic first aid kit\",\n                        \"Sunscreen and insect repellent\",\n                        \"Prescription medications (if any)\"\n                    ]\n                }\n              ],\n              \"finalTip\": \"Pack light, stay flexible, and be ready for anything!\"\n            }", "July 30, 2026", "What to pack for every season and adventure in New Zealand.", "assets/images/articles/packing.jpg", "4 min read", "Packing List for New Zealand", 0 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nature" },
                    { 2, "Adventure" },
                    { 3, "Sightseeing" },
                    { 4, "Culture" },
                    { 5, "Food & Wine" },
                    { 6, "City" },
                    { 7, "Relaxation" },
                    { 8, "Wildlife" }
                });

            migrationBuilder.InsertData(
                table: "Islands",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Aotearoa's North Island, known for culture, beaches, geothermal landscapes and vibrant cities.", "assets/images/north-island.jpg", "North Island" },
                    { 2, "New Zealand's South Island, famous for mountains, lakes, fiords and outdoor adventures.", "assets/images/south-island.jpg", "South Island" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Description", "IslandId", "Name" },
                values: new object[,]
                {
                    { 1, "", 1, "Northland" },
                    { 2, "", 1, "Auckland" },
                    { 3, "", 1, "Waikato" },
                    { 4, "", 1, "Bay of Plenty" },
                    { 5, "", 1, "Gisborne" },
                    { 6, "", 1, "Taranaki" },
                    { 7, "", 1, "Manawatū-Whanganui" },
                    { 8, "", 1, "Hawke's Bay" },
                    { 9, "", 1, "Wellington" },
                    { 10, "", 2, "Tasman" },
                    { 11, "", 2, "Nelson" },
                    { 12, "", 2, "Marlborough" },
                    { 13, "", 2, "West Coast" },
                    { 14, "", 2, "Canterbury" },
                    { 15, "", 2, "Otago" },
                    { 16, "", 2, "Southland" }
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Description", "ImageUrl", "IsPopular", "Name", "Rating", "RegionId", "ReviewCount" },
                values: new object[,]
                {
                    { 1, "New Zealand's adventure capital, surrounded by mountains and Lake Wakatipu.", "assets/images/queenstown.png", true, "Queenstown", 4.9m, 15, 980 },
                    { 2, "A geothermal wonderland known for Māori culture, hot springs and outdoor adventures.", "assets/images/rotorua.jpg", true, "Rotorua", 4.8m, 4, 980 },
                    { 3, "New Zealand's largest city, surrounded by beautiful harbours, islands and beaches.", "assets/images/auckland.jpg", true, "Auckland", 4.7m, 2, 1200 },
                    { 4, "A relaxed lakeside town surrounded by mountains and outdoor adventures.", "assets/images/wanaka.jpg", true, "Wanaka", 4.8m, 15, 850 },
                    { 5, "New Zealand's creative capital, known for culture, food and waterfront views.", "assets/images/wellington.jpg", true, "Wellington", 4.7m, 9, 920 },
                    { 6, "A vibrant South Island city surrounded by mountains, gardens and natural landscapes.", "assets/images/christchurch.jpg", true, "Christchurch", 4.7m, 14, 760 },
                    { 7, "A charming Waikato town and gateway to the famous Hobbiton Movie Set.", "assets/images/matamata.jpg", false, "Matamata", 4.7m, 3, 600 },
                    { 8, "A scenic lakeside town and gateway to Fiordland National Park and Milford Sound.", "assets/images/te-anau.jpg", true, "Te Anau", 4.8m, 16, 720 }
                });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "ActivityLevel", "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[,]
                {
                    { 1, "Easy", "Open year round; alpine weather can affect gondola operations.", "Year round", "Pre-book gondola and activities in peak periods; weather may affect operations.", "Take in breathtaking views of Queenstown, Lake Wakatipu and the surrounding mountains.", 1, "assets/images/skyline-queenstown.jpg", -45.028700000000001, 168.6558, "Skyline Queenstown", "Check Skyline’s current operating hours before visit.", 4.7m, "3 hours", 3447, "https://www.skyline.co.nz/en/queenstown/" },
                    { 2, "Easy", "Seasonal timetable; services can be affected by lake and weather conditions.", "Nov - Mar", "Advance booking recommended; arrive at the wharf early and check weather cancellations.", "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.", 1, "assets/images/tss-earnslaw-cruise.jpg", -45.032600000000002, 168.6575, "TSS Earnslaw Cruise", "Check RealNZ’s current sailing timetable before visit.", 4.4m, "3 hours", 80, "https://www.realnz.com/en/experiences/cruises/tss-earnslawe/" },
                    { 3, "Moderate", "Operates year round, subject to river and weather conditions.", "Year round", "Advance booking recommended; trips can be delayed or cancelled for weather or river conditions.", "High-speed jet boat ride through the Shotover River canyons.", 1, "assets/images/shotover-jet.jpg", -44.982900000000001, 168.67019999999999, "Shotover Jet", "Check Shotover Jet’s current departure times before visit.", 4.3m, "2 hours", 269, "https://www.shotoverjet.com/" },
                    { 4, "Easy", "Year round; check current seasonal operating times.", "Year round", "Book online or check the official site before visiting; wildlife encounters and conservation shows run daily.", "Native wildlife conservation park near town centre.", 1, "assets/images/kiwi-park-queenstown.jpg", -45.029600000000002, 168.6557, "Kiwi Park Queenstown", "Daily. The official site lists 9:30am–6:30pm with last entry 5:45pm, and a shorter 9:30am–5pm schedule with last entry 4:15pm; confirm the applicable season.", 4.6m, "2 hours", 355, "https://kiwibird.co.nz/" },
                    { 5, "Easy", "Open year round; autumn colour is a seasonal highlight.", "Sep - Apr", "No booking normally required; use daylight hours and allow for weather.", "Lakeside gardens and an easy walking loop near central Queenstown.", 1, "assets/images/queenstown-gardens.jpg", -45.0336, 168.66309999999999, "Queenstown Gardens", "Public gardens; check Queenstown Lakes District Council information for facility updates.", 4.4m, "2 hours", 1024, "https://www.queenstownnz.co.nz/listing/queenstown-gardens/120/" },
                    { 6, "Easy", "Open year round; autumn is especially popular.", "Year round", "No booking for the precinct; allow extra time for parking during autumn and events.", "Historic gold-mining village with heritage streets and riverside walks.", 1, "assets/images/arrowtown-historic-precinct.jpg", -44.9392, 168.8313, "Arrowtown Historic Precinct", "Public streets are accessible daily; check individual shops and museums for their hours.", 4.3m, "3 hours", 864, "https://www.arrowtown.com/" },
                    { 7, "Easy", "Open year round; vineyard and cellar-door experiences vary seasonally.", "Year round", "Book tastings, tours and dining in advance; appoint a sober driver or use a tour.", "Explore the region's oldest vineyards and New Zealand's largest wine cave.", 1, "assets/images/gibbston-valley-winery.jpg", -45.011600000000001, 168.86869999999999, "Gibbston Valley Winery", "Check Gibbston Valley’s current cellar-door and restaurant hours before visit.", 4.3m, "4 hours", 861, "https://www.gibbstonvalley.com/" },
                    { 8, "Easy", "Open year round; popular in winter and evenings.", "Year round", "Advance booking is essential; outdoor sessions may be weather affected.", "Private hot pools overlooking the Shotover River canyon.", 1, "assets/images/onsen-hot-pools.jpg", -44.984000000000002, 168.6687, "Onsen Hot Pools", "Check Onsen Hot Pools’ current session times before visit.", 4.5m, "2 hours", 17, "https://www.onsen.co.nz/" },
                    { 9, "Challenging", "Open year round, subject to wind and weather limits.", "Year round", "Advance booking recommended; weather can delay or cancel jumps.", "The world's first commercial bungy jump site, located at the historic Kawarau Bridge.", 1, "assets/images/kawarau-bungy-centre.jpg", -45.013399999999997, 168.89060000000001, "Kawarau Bungy Centre", "Check AJ Hackett’s current operating hours before visit.", 4.4m, "3 hours", 141, "https://www.bungy.co.nz/queenstown/kawarau-bungy-centre/" },
                    { 10, "Challenging", "Skiing is seasonal; sightseeing and summer operations vary.", "Year round", "Book rentals or lessons in advance; alpine road and lift access are weather dependent.", "A premier ski resort offering spectacular winter sports and summer sightseeing.", 1, "assets/images/coronet-peak.jpg", -44.928699999999999, 168.73599999999999, "Coronet Peak", "Check NZSki’s current lift, road and operating status before visit.", 4.5m, "5 hours", 2400, "https://www.coronetpeak.co.nz/" },
                    { 11, "Moderate", "Best in dry conditions; snow, ice and strong wind can affect winter access.", "Year round", "No booking; take water, layers and suitable footwear.", "A rewarding hike through pine forest to panoramic views of the Wakatipu basin.", 1, "assets/images/queenstown-hill-time-walk.jpg", -45.029499999999999, 168.6661, "Queenstown Hill Time Walk", "Public walking track; start in daylight and check DOC/Queenstown weather advice.", 4.8m, "3 hours", 36, "https://www.queenstownnz.co.nz/listing/queenstown-hill-time-walk/146/" },
                    { 12, "Easy", "Open year round; winter snow/ice and storm conditions may affect roads.", "Year round", "No booking; fuel up, allow extra driving time, and do not rely on the route during road closures.", "A stunning coastal road trip tracing the edge of Lake Wakatipu to the gateway of Mount Aspiring National Park.", 1, "assets/images/glenorchy-scenic-drive.jpg", -44.846800000000002, 168.38460000000001, "Glenorchy Scenic Drive", "Public road; check NZTA and weather conditions before departure.", 4.6m, "6 hours", 1187, "https://www.queenstownnz.co.nz/things-to-do/scenic-drives/glenorchy-road/" },
                    { 13, "Easy", "Year round; road, avalanche and severe-weather disruptions are possible.", "Nov - Mar", "Advance booking strongly recommended; carry food/water and expect weather-related changes.", "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.", 8, "assets/images/milford.png", -44.671500000000002, 167.9255, "Milford Sound day trip", "Check operator timetable and NZTA road conditions before visit.", 4.5m, "10 hours", 415, "https://www.realnz.com/en/experiences/cruises/milford-sound-cruises/" },
                    { 14, "Easy", "Open year round; best enjoyed in settled weather and daylight.", "Year round", "No booking; check weather and water-safety advice before lake activities.", "A vibrant promenade perfect for a scenic stroll, lakeside dining, or watching the sunset.", 1, "assets/images/lake-wakatipu-waterfront.jpg", -45.033200000000001, 168.65989999999999, "Lake Wakatipu waterfront", "Public waterfront; no set hours.", 4.6m, "2 hours", 1469, "https://www.queenstownnz.co.nz/listing/queenstown-bay/605/" },
                    { 15, "Easy", "Open year round; track conditions can be muddy, icy or affected by storms.", "Dec - Feb", "No booking; use the car park trailhead and carry weather-appropriate gear.", "An easy, picturesque walk through native bush to a secluded cove on Lake Wakatipu.", 1, "assets/images/bobs-cove-track.jpg", -45.068199999999997, 168.53980000000001, "Bobs Cove Track", "Public walking track; check DOC conditions before visit.", 4.9m, "3 hours", 682, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/otago/places/queenstown-area/things-to-do/tracks/bobs-cove-track/" },
                    { 16, "Easy", "Open year round; spring and summer are especially colourful.", "Sep - Apr", "No booking for gardens; weather and events may affect some areas.", "Historic riverside gardens beside Hagley Park.", 6, "assets/images/christchurch-botanic-gardens.jpg", -43.5306, 172.62620000000001, "Christchurch Botanic Gardens", "Check Christchurch City Council’s current garden and visitor-centre hours before visit.", 4.8m, "2 hours", 957, "https://ccc.govt.nz/parks-and-gardens/christchurch-botanic-gardens" },
                    { 17, "Easy", "Open year round; indoor attraction.", "Year round", "Advance booking recommended in peak periods; allow time for timed experiences.", "Interactive Antarctic visitor experience beside Christchurch Airport.", 6, "assets/images/international-antarctic-centre.jpg", -43.486199999999997, 172.5488, "International Antarctic Centre", "Check the International Antarctic Centre’s current daily hours before visit.", 4.5m, "3 hours", 176, "https://www.iceberg.co.nz/" },
                    { 18, "Easy", "Open year round, subject to wind and weather.", "Year round", "Book ahead in peak periods; gondola operations can be affected by high winds.", "Gondola ride with views over Lyttelton Harbour and the Canterbury Plains.", 6, "assets/images/christchurch-gondola.jpg", -43.582799999999999, 172.71190000000001, "Christchurch Gondola", "Check Christchurch Gondola’s current operating hours before visit.", 4.4m, "2 hours", 1075, "https://www.christchurchgondola.co.nz/" },
                    { 19, "Easy", "Open year round; indoor attraction.", "Year round", "Booking recommended for groups; allow time for nearby central-city parking.", "Museum telling the story of the Canterbury earthquakes and recovery.", 6, "assets/images/quake-city.jpg", -43.528399999999998, 172.63220000000001, "Quake City", "Check Quake City’s current hours before visit.", 4.6m, "2 hours", 1438, "https://www.quakecity.co.nz/" },
                    { 20, "Easy", "Open year round; indoor/outdoor exhibits.", "Year round", "General admission is usually free; book guided tours or special activities if required.", "Discover the history of New Zealand military aviation through engaging exhibits and historic aircraft.", 6, "assets/images/air-force-museum-of-new-zealand.jpg", -43.548299999999998, 172.54599999999999, "Air Force Museum of New Zealand", "Check the museum’s current opening hours before visit.", 4.3m, "3 hours", 630, "https://www.airforcemuseum.co.nz/" },
                    { 21, "Moderate", "Open year round; outdoor animal experiences vary with weather and animal welfare needs.", "Year round", "Advance booking recommended in school holidays; check encounter times and weather advice.", "New Zealand's only open-range zoo, offering unique up-close animal encounters.", 6, "assets/images/orana-wildlife-park.jpg", -43.468200000000003, 172.46360000000001, "Orana Wildlife Park", "Check Orana’s current daily hours before visit.", 4.2m, "5 hours", 314, "https://www.oranawildlifepark.co.nz/" },
                    { 22, "Easy", "Open year round; night tours and animal encounters may be seasonal.", "Year round", "Book kiwi/night tours and encounters in advance.", "A wildlife park dedicated to New Zealand's native species and Māori cultural experiences.", 6, "assets/images/willowbank-wildlife-reserve.jpg", -43.467799999999997, 172.59370000000001, "Willowbank Wildlife Reserve", "Check Willowbank’s current visitor hours before visit.", 4.5m, "3 hours", 513, "https://www.willowbank.co.nz/" },
                    { 23, "Easy", "Year round; harbour cruises and wildlife trips are weather dependent.", "Sep - Apr", "Book harbour cruises in advance; allow for the drive and possible weather cancellations.", "Banks Peninsula harbour town, suitable as a full-day excursion from Christchurch.", 6, "assets/images/akaroa-harbour-day-trip.jpg", -43.805799999999998, 172.9675, "Akaroa Harbour day trip", "Check the chosen operator’s timetable before visit.", 4.6m, "8 hours", 1144, "https://www.christchurchnz.com/explore/akaroa" },
                    { 24, "Easy", "Open year round; market and ferry activity varies by day.", "Year round", "No booking for the waterfront; check parking and cruise-ship/event impacts.", "A historic port town set in a collapsed volcanic crater, featuring quirky shops and stunning views.", 6, "assets/images/lyttelton-harbour.jpg", -43.601500000000001, 172.72120000000001, "Lyttelton Harbour", "Public harbour area; check individual businesses and event schedules.", 4.9m, "3 hours", 519, "https://www.christchurchnz.com/explore/lyttelton" },
                    { 25, "Easy", "Open year round; best in settled conditions.", "Dec - Feb", "No booking; check surf, tide and weather warnings before swimming or rock access.", "A popular coastal suburb known for its relaxed surf culture and iconic volcanic rock formations.", 6, "assets/images/sumner-beach-and-cave-rock.jpg", -43.567, 172.75839999999999, "Sumner Beach and Cave Rock", "Public beach; no set hours.", 4.4m, "3 hours", 1377, "https://ccc.govt.nz/parks-and-gardens/explore-parks/coastal-parks/sumner-beach" },
                    { 26, "Easy", "Operates seasonally and may be weather dependent.", "Year round", "Advance booking recommended; rain, wind or river conditions may affect service.", "A tranquil and iconic Christchurch experience gliding along the Avon River in a flat-bottomed boat.", 6, "assets/images/punting-on-the-avon.jpg", -43.533200000000001, 172.6277, "Punting on the Avon", "Check Punting on the Avon’s current departure times before visit.", 4.6m, "2 hours", 497, "https://www.puntingontheavon.co.nz/" },
                    { 27, "Easy", "Open year round; trading hours vary by stall and day.", "Year round", "No booking for market browsing; book restaurants separately if required.", "A bustling indoor market offering diverse street food, fresh local produce, and boutique stalls.", 6, "assets/images/riverside-market.jpg", -43.532299999999999, 172.63239999999999, "Riverside Market", "Check Riverside Market’s current opening hours before visit.", 4.6m, "2 hours", 890, "https://riverside.nz/" },
                    { 28, "Challenging", "Open year round; exposed tracks are best in dry, low-wind conditions.", "Year round", "No booking; carry water, sun protection and layers; avoid exposed routes in severe weather.", "A rugged volcanic range offering extensive walking and biking trails with panoramic city and harbour views.", 6, "assets/images/port-hills.jpg", -43.633800000000001, 172.6223, "Port Hills", "Public tracks; check Christchurch City Council and weather/fire restrictions before visit.", 4.5m, "4 hours", 949, "https://ccc.govt.nz/parks-and-gardens/explore-parks/port-hills" },
                    { 29, "Easy", "Confirm reopening and temporary exhibition arrangements before planning.", "Year round", "No booking assumption; verify venue location, ticketing and opening information first.", "A cultural heritage museum showcasing the rich natural and human history of the Canterbury region.", 6, "assets/images/canterbury-museum.jpg", -43.531199999999998, 172.6268, "Canterbury Museum", "Check the Canterbury Museum website before visit; redevelopment may affect access.", 4.5m, "2 hours", 305, "https://canterburymuseum.com/" },
                    { 30, "Easy", "Open year round; galleries, shops and events have separate schedules.", "Year round", "No booking to explore public areas; book performances, tours or workshops separately.", "A vibrant hub for arts, culture, and education set within stunning restored Gothic Revival buildings.", 6, "assets/images/the-arts-centre.jpg", -43.531300000000002, 172.6284, "The Arts Centre", "Check The Arts Centre’s current building and venue hours before visit.", 4.7m, "2 hours", 744, "https://artscentre.org.nz/" },
                    { 31, "Easy", "Open year round; outdoor SkyWalk/SkyJump is weather dependent.", "Year round", "Pre-book SkyWalk/SkyJump and peak observation visits; outdoor activities can be weather cancelled.", "Observation tower with panoramic views across Auckland and the Hauraki Gulf.", 3, "assets/images/sky-tower.jpg", -36.848500000000001, 174.76220000000001, "Sky Tower", "Check SkyCity’s current attraction hours before visit.", 4.5m, "2 hours", 535, "https://skycityauckland.co.nz/sky-tower/" },
                    { 32, "Easy", "Open year round; indoor museum and outdoor Domain.", "Year round", "Book paid exhibitions or events in advance; allow time for parking or public transport.", "Museum of natural history and Aotearoa New Zealand stories in the Domain.", 3, "assets/images/auckland-museum.jpg", -36.860599999999998, 174.77780000000001, "Auckland Museum", "Check Auckland Museum’s current opening hours before visit.", 4.5m, "3 hours", 1112, "https://www.aucklandmuseum.com/" },
                    { 33, "Moderate", "Open year round; outdoor areas and encounters are weather dependent.", "Year round", "Advance booking recommended in peak periods; check animal encounter requirements.", "Conservation-focused zoo in Western Springs.", 3, "assets/images/auckland-zoo.jpg", -36.863100000000003, 174.7176, "Auckland Zoo", "Check Auckland Zoo’s current daily hours before visit.", 4.5m, "4 hours", 981, "https://www.aucklandzoo.co.nz/" },
                    { 34, "Easy", "Open year round; ferry sailings and outdoor activities depend on weather.", "Nov - Mar", "Book ferries, tours and popular wineries in advance; allow for weather or sea-condition disruptions.", "Hauraki Gulf island for beaches, art and vineyard visits; allow a full day.", 3, "assets/images/waiheke-island-day-trip.jpg", -36.843000000000004, 174.767, "Waiheke Island day trip", "Check Fullers360 ferry timetable and chosen winery/attraction hours before visit.", 4.2m, "8 hours", 757, "https://www.fullers.co.nz/destinations-and-experiences/waiheke-island/" },
                    { 35, "Moderate", "Open year round; ferry service and summit track conditions are weather dependent.", "Nov - Mar", "Pre-book ferry; take food, water and sun protection—there are no shops on Rangitoto.", "Volcanic island day trip with a summit walk and harbour views.", 3, "assets/images/rangitoto-island-day-trip.jpg", -36.843000000000004, 174.767, "Rangitoto Island day trip", "Check Fullers360 timetable and DOC island advice before visit.", 4.5m, "7 hours", 1154, "https://www.aucklandnz.com/explore/rangitoto-island" },
                    { 36, "Easy", "Open year round; indoor attraction.", "Year round", "Advance booking recommended in weekends and school holidays.", "An iconic underwater attraction featuring penguin colonies, shark tunnels, and marine rescue exhibits.", 3, "assets/images/sea-life-kelly-tarltons-aquarium.jpg", -36.847499999999997, 174.81829999999999, "SEA LIFE Kelly Tarlton’s Aquarium", "Check SEA LIFE Kelly Tarlton’s current hours before visit.", 4.3m, "3 hours", 425, "https://www.visitsealife.com/auckland/" },
                    { 37, "Easy", "Open year round; indoor/outdoor exhibits.", "Year round", "Book special events and school-holiday activities in advance where offered.", "An interactive museum exploring the history and future of New Zealand's transport and technology.", 3, "assets/images/museum-of-transport-and-technology.jpg", -36.866500000000002, 174.71789999999999, "Museum of Transport and Technology", "Check MOTAT’s current opening hours before visit.", 4.6m, "3 hours", 1277, "https://www.motat.nz/" },
                    { 38, "Easy", "Open year round; harbour sailing experiences are weather dependent.", "Year round", "Book heritage sailings in advance; sailings can be weather affected.", "Discover the stories of the people and ships that shaped New Zealand's seafaring history.", 3, "assets/images/new-zealand-maritime-museum.jpg", -36.841900000000003, 174.76339999999999, "New Zealand Maritime Museum", "Check the Maritime Museum’s current hours before visit.", 4.8m, "2 hours", 1357, "https://www.maritimemuseum.co.nz/" },
                    { 39, "Easy", "Open year round; gallery programme and special exhibitions vary.", "Year round", "Book ticketed exhibitions or events in advance when required.", "New Zealand's largest visual arts institution, housing an extensive collection of national and international art.", 3, "assets/images/auckland-art-gallery-toi-o-tamaki.jpg", -36.850200000000001, 174.76609999999999, "Auckland Art Gallery Toi o Tāmaki", "Check Auckland Art Gallery’s current opening hours before visit.", 4.4m, "2 hours", 989, "https://www.aucklandartgallery.com/" },
                    { 40, "Moderate", "Open year round; exposed summit is best in settled weather.", "Year round", "No booking; use daylight hours and allow for a walk from parking.", "A significant volcanic peak and historic park offering 360-degree views of Auckland.", 3, "assets/images/maungakiekie-one-tree-hill.jpg", -36.896700000000003, 174.7765, "Maungakiekie / One Tree Hill", "Public park; check Cornwall Park and local weather information before visit.", 4.3m, "3 hours", 1426, "https://cornwallpark.co.nz/" },
                    { 41, "Moderate", "Open year round; ferry and outdoor walk conditions are weather dependent.", "Year round", "No booking for North Head; ferry services can be weather affected and tunnels may have access limits.", "A charming historic village paired with a coastal reserve known for its military tunnels and harbour views.", 3, "assets/images/devonport-waterfront-and-north-head.jpg", -36.832900000000002, 174.7961, "Devonport waterfront and North Head", "Check Fullers360 timetable and DOC North Head information before visit.", 4.5m, "4 hours", 1480, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/auckland/places/north-head-historic-reserve/" },
                    { 42, "Moderate", "Open year round; scheduled ferry access and outdoor walking are weather dependent.", "Year round", "Book ferry well ahead; take food, water and walking gear—check weather cancellations.", "A renowned open sanctuary for native birdlife and conservation, accessible by a scenic ferry ride.", 3, "assets/images/tiritiri-matangi-island-day-trip.jpg", -36.843000000000004, 174.767, "Tiritiri Matangi Island day trip", "Check Explore Group ferry timetable and DOC visitor information before visit.", 4.6m, "8 hours", 248, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/auckland/places/tiritiri-matangi-open-sanctuary/" },
                    { 43, "Easy", "Open year round; best in settled weather and daylight.", "Year round", "No booking; check swim, weather and traffic conditions before visit.", "A picturesque coastal route leading to a vibrant seaside suburb with a beautiful sandy beach and eateries.", 3, "assets/images/mission-bay-and-tamaki-drive.jpg", -36.847999999999999, 174.83150000000001, "Mission Bay and Tāmaki Drive", "Public waterfront; no set hours.", 4.7m, "3 hours", 441, "https://www.aucklandnz.com/explore/mission-bay" },
                    { 44, "Easy", "Open year round; events may limit vehicle access or parking.", "Sep - Apr", "No booking; use daylight hours and combine with Auckland Museum if suitable.", "Auckland's oldest park, featuring expansive green spaces, walking tracks, and the historic Wintergardens.", 3, "assets/images/auckland-domain.jpg", -36.8596, 174.7758, "Auckland Domain", "Public park; check Auckland Council information for event impacts.", 4.5m, "2 hours", 437, "https://www.aucklandcouncil.govt.nz/parks-recreation/get-outdoors/find-a-park/Pages/park-details.aspx?parkID=1" },
                    { 45, "Easy", "Open year round; indoor attraction.", "Year round", "Advance booking recommended; arrive before your timed session.", "An immersive and wildly imaginative experience exploring the worlds of horror, sci-fi, and fantasy film-making.", 3, "assets/images/weta-workshop-unleashed.jpg", -36.8489, 174.7621, "Wētā Workshop Unleashed", "Check Wētā Workshop Unleashed’s current session times before visit.", 4.6m, "2 hours", 1343, "https://tours.wetaworkshop.com/auckland/" },
                    { 46, "Easy", "Open year round; daily boat departures across Lake Te Anau.", "Year round", "Advance booking recommended; check-in 30 minutes prior to departure; requires bending/walking in caves.", "A magical underground experience starting with a scenic lake cruise to a hidden limestone cave illuminated by thousands of glowworms.", 8, "assets/images/te-anau-glowworm-caves.jpg", -45.416499999999999, 167.71180000000001, "Te Anau Glowworm Caves", "Open 7 days, daily departures. Check official website before visit.", 4.5m, "2.25 hours", 850, "https://www.realnz.com/en/experiences/glowworm-caves/te-anau-glowworm-caves/" },
                    { 47, "Challenging", "Great Walks season runs late October to April; day walks accessible year round in good weather.", "Sep - Apr", "No booking required for day walks; check DOC weather and track alerts before setting out.", "An accessible section of the famous Kepler Great Walk, leading through ancient beech forests along the lake shore.", 8, "assets/images/kepler-track-day-walk.jpg", -45.439799999999998, 167.68299999999999, "Kepler Track Day Walk", "Public walking track; accessible during daylight hours.", 4.8m, "3 hours", 620, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/tracks/kepler-track/" },
                    { 48, "Easy", "Operates year round; full-day excursion departing from Manapouri.", "Nov - Mar", "Advance booking essential; departures leave from Pearl Harbour in Manapouri.", "A tranquil and remote wilderness cruise through a deep, pristine fiord known for its serene waters and native wildlife.", 8, "assets/images/doubtful-sound-wilderness-cruise.jpg", -45.563600000000001, 167.6163, "Doubtful Sound Wilderness Cruise", "Check official website before visit for seasonal departure times.", 4.7m, "7 hours", 540, "https://www.realnz.com/en/experiences/cruises/doubtful-sound-wilderness-cruises/" },
                    { 49, "Easy", "Open year round from dawn to dusk.", "Year round", "Free entry (gold coin donation appreciated); guided tour feeds can be booked.", "A lakeside conservation haven providing a rare chance to see endangered native birds like the Takahē up close.", 8, "assets/images/te-anau-bird-sanctuary.jpg", -45.426200000000001, 167.70509999999999, "Te Anau Bird Sanctuary", "Open daily from dawn to dusk.", 4.6m, "1 hours", 310, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/te-anau-bird-sanctuary/" },
                    { 50, "Easy", "Open year round; an excellent indoor activity.", "Year round", "Advance booking recommended for popular evening screenings.", "A boutique cinema showcasing the custom-shot documentary 'Ata Whenua - Shadowland', capturing Fiordland's wild landscapes.", 8, "assets/images/fiordland-cinema.jpg", -45.414999999999999, 167.71350000000001, "Fiordland Cinema", "Check official website for current screening showtimes.", 4.8m, "1 hours", 420, "https://www.fiordlandcinema.co.nz/" }
                });

            migrationBuilder.InsertData(
                table: "DestinationCategories",
                columns: new[] { "CategoryId", "DestinationId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 3, 3 },
                    { 6, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 4, 5 },
                    { 6, 5 },
                    { 1, 8 },
                    { 3, 8 },
                    { 8, 8 }
                });

            migrationBuilder.InsertData(
                table: "AttractionCategories",
                columns: new[] { "AttractionId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 2, 7 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 8 },
                    { 5, 1 },
                    { 5, 7 },
                    { 6, 3 },
                    { 6, 4 },
                    { 7, 5 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 7 },
                    { 9, 2 },
                    { 10, 1 },
                    { 10, 2 },
                    { 11, 1 },
                    { 11, 2 },
                    { 12, 1 },
                    { 12, 3 },
                    { 13, 1 },
                    { 13, 3 },
                    { 14, 3 },
                    { 14, 7 },
                    { 15, 1 },
                    { 15, 7 },
                    { 16, 1 },
                    { 16, 7 },
                    { 17, 3 },
                    { 17, 4 },
                    { 18, 3 },
                    { 19, 3 },
                    { 19, 4 },
                    { 20, 4 },
                    { 21, 1 },
                    { 21, 8 },
                    { 22, 4 },
                    { 22, 8 },
                    { 23, 3 },
                    { 23, 8 },
                    { 24, 3 },
                    { 24, 6 },
                    { 25, 1 },
                    { 25, 7 },
                    { 26, 3 },
                    { 26, 7 },
                    { 27, 5 },
                    { 27, 6 },
                    { 28, 1 },
                    { 28, 2 },
                    { 29, 4 },
                    { 30, 4 },
                    { 30, 6 },
                    { 31, 3 },
                    { 31, 6 },
                    { 32, 4 },
                    { 33, 8 },
                    { 34, 1 },
                    { 34, 5 },
                    { 35, 1 },
                    { 35, 2 },
                    { 36, 3 },
                    { 36, 8 },
                    { 37, 4 },
                    { 38, 3 },
                    { 38, 4 },
                    { 39, 4 },
                    { 39, 6 },
                    { 40, 1 },
                    { 40, 4 },
                    { 41, 3 },
                    { 41, 4 },
                    { 42, 1 },
                    { 42, 8 },
                    { 43, 6 },
                    { 43, 7 },
                    { 44, 1 },
                    { 44, 6 },
                    { 45, 3 },
                    { 45, 4 },
                    { 46, 1 },
                    { 46, 2 },
                    { 46, 3 },
                    { 47, 1 },
                    { 47, 2 },
                    { 48, 1 },
                    { 48, 3 },
                    { 48, 8 },
                    { 49, 1 },
                    { 49, 8 },
                    { 50, 4 },
                    { 50, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttractionCategories_CategoryId",
                table: "AttractionCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_DestinationId",
                table: "Attractions",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationCategories_CategoryId",
                table: "DestinationCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_RegionId",
                table: "Destinations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_IslandId",
                table: "Regions",
                column: "IslandId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDays_TripId_DayNumber",
                table: "TripDays",
                columns: new[] { "TripId", "DayNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_OwnerId_StartDate",
                table: "Trips",
                columns: new[] { "OwnerId", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_AttractionId",
                table: "TripStops",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_TripDayId",
                table: "TripStops",
                column: "TripDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AttractionCategories");

            migrationBuilder.DropTable(
                name: "DestinationCategories");

            migrationBuilder.DropTable(
                name: "TripStops");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "TripDays");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Islands");
        }
    }
}
