using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[,]
                {
                    { 1, "Open year round; alpine weather can affect gondola operations.", "Year round", "Pre-book gondola and activities in peak periods; weather may affect operations.", "Take in breathtaking views of Queenstown, Lake Wakatipu and the surrounding mountains.", 1, "assets/images/skyline-queenstown.jpg", -45.028700000000001, 168.6558, "Skyline Queenstown", "Check Skyline’s current operating hours before visit.", 4.7m, "3 hours", 3447, "https://www.skyline.co.nz/en/queenstown/" },
                    { 2, "Seasonal timetable; services can be affected by lake and weather conditions.", "Nov - Mar", "Advance booking recommended; arrive at the wharf early and check weather cancellations.", "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.", 1, "assets/images/tss-earnslaw-cruise.jpg", -45.032600000000002, 168.6575, "TSS Earnslaw Cruise", "Check RealNZ’s current sailing timetable before visit.", 4.4m, "3 hours", 80, "https://www.realnz.com/en/experiences/cruises/tss-earnslawe/" },
                    { 3, "Operates year round, subject to river and weather conditions.", "Year round", "Advance booking recommended; trips can be delayed or cancelled for weather or river conditions.", "High-speed jet boat ride through the Shotover River canyons.", 1, "assets/images/shotover-jet.jpg", -44.982900000000001, 168.67019999999999, "Shotover Jet", "Check Shotover Jet’s current departure times before visit.", 4.3m, "2 hours", 269, "https://www.shotoverjet.com/" },
                    { 4, "Year round; check current seasonal operating times.", "Year round", "Book online or check the official site before visiting; wildlife encounters and conservation shows run daily.", "Native wildlife conservation park near town centre.", 1, "assets/images/kiwi-park-queenstown.jpg", -45.029600000000002, 168.6557, "Kiwi Park Queenstown", "Daily. The official site lists 9:30am–6:30pm with last entry 5:45pm, and a shorter 9:30am–5pm schedule with last entry 4:15pm; confirm the applicable season.", 4.6m, "2 hours", 355, "https://kiwibird.co.nz/" },
                    { 5, "Open year round; autumn colour is a seasonal highlight.", "Sep - Apr", "No booking normally required; use daylight hours and allow for weather.", "Lakeside gardens and an easy walking loop near central Queenstown.", 1, "assets/images/queenstown-gardens.jpg", -45.0336, 168.66309999999999, "Queenstown Gardens", "Public gardens; check Queenstown Lakes District Council information for facility updates.", 4.4m, "2 hours", 1024, "https://www.queenstownnz.co.nz/listing/queenstown-gardens/120/" },
                    { 6, "Open year round; autumn is especially popular.", "Year round", "No booking for the precinct; allow extra time for parking during autumn and events.", "Historic gold-mining village with heritage streets and riverside walks.", 1, "assets/images/arrowtown-historic-precinct.jpg", -44.9392, 168.8313, "Arrowtown Historic Precinct", "Public streets are accessible daily; check individual shops and museums for their hours.", 4.3m, "3 hours", 864, "https://www.arrowtown.com/" },
                    { 7, "Open year round; vineyard and cellar-door experiences vary seasonally.", "Year round", "Book tastings, tours and dining in advance; appoint a sober driver or use a tour.", "Explore the region's oldest vineyards and New Zealand's largest wine cave.", 1, "assets/images/gibbston-valley-winery.jpg", -45.011600000000001, 168.86869999999999, "Gibbston Valley Winery", "Check Gibbston Valley’s current cellar-door and restaurant hours before visit.", 4.3m, "4 hours", 861, "https://www.gibbstonvalley.com/" },
                    { 8, "Open year round; popular in winter and evenings.", "Year round", "Advance booking is essential; outdoor sessions may be weather affected.", "Private hot pools overlooking the Shotover River canyon.", 1, "assets/images/onsen-hot-pools.jpg", -44.984000000000002, 168.6687, "Onsen Hot Pools", "Check Onsen Hot Pools’ current session times before visit.", 4.5m, "2 hours", 17, "https://www.onsen.co.nz/" },
                    { 9, "Open year round, subject to wind and weather limits.", "Year round", "Advance booking recommended; weather can delay or cancel jumps.", "The world's first commercial bungy jump site, located at the historic Kawarau Bridge.", 1, "assets/images/kawarau-bungy-centre.jpg", -45.013399999999997, 168.89060000000001, "Kawarau Bungy Centre", "Check AJ Hackett’s current operating hours before visit.", 4.4m, "3 hours", 141, "https://www.bungy.co.nz/queenstown/kawarau-bungy-centre/" },
                    { 10, "Skiing is seasonal; sightseeing and summer operations vary.", "Year round", "Book rentals or lessons in advance; alpine road and lift access are weather dependent.", "A premier ski resort offering spectacular winter sports and summer sightseeing.", 1, "assets/images/coronet-peak.jpg", -44.928699999999999, 168.73599999999999, "Coronet Peak", "Check NZSki’s current lift, road and operating status before visit.", 4.5m, "5 hours", 2400, "https://www.coronetpeak.co.nz/" },
                    { 11, "Best in dry conditions; snow, ice and strong wind can affect winter access.", "Year round", "No booking; take water, layers and suitable footwear.", "A rewarding hike through pine forest to panoramic views of the Wakatipu basin.", 1, "assets/images/queenstown-hill-time-walk.jpg", -45.029499999999999, 168.6661, "Queenstown Hill Time Walk", "Public walking track; start in daylight and check DOC/Queenstown weather advice.", 4.8m, "3 hours", 36, "https://www.queenstownnz.co.nz/listing/queenstown-hill-time-walk/146/" },
                    { 12, "Open year round; winter snow/ice and storm conditions may affect roads.", "Year round", "No booking; fuel up, allow extra driving time, and do not rely on the route during road closures.", "A stunning coastal road trip tracing the edge of Lake Wakatipu to the gateway of Mount Aspiring National Park.", 1, "assets/images/glenorchy-scenic-drive.jpg", -44.846800000000002, 168.38460000000001, "Glenorchy Scenic Drive", "Public road; check NZTA and weather conditions before departure.", 4.6m, "6 hours", 1187, "https://www.queenstownnz.co.nz/things-to-do/scenic-drives/glenorchy-road/" },
                    { 13, "Year round; road, avalanche and severe-weather disruptions are possible.", "Nov - Mar", "Advance booking strongly recommended; carry food/water and expect weather-related changes.", "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.", 8, "assets/images/milford-sound-day-trip.jpg", -44.671500000000002, 167.9255, "Milford Sound day trip", "Check operator timetable and NZTA road conditions before visit.", 4.5m, "10 hours", 415, "https://www.realnz.com/en/experiences/cruises/milford-sound-cruises/" },
                    { 14, "Open year round; best enjoyed in settled weather and daylight.", "Year round", "No booking; check weather and water-safety advice before lake activities.", "A vibrant promenade perfect for a scenic stroll, lakeside dining, or watching the sunset.", 1, "assets/images/lake-wakatipu-waterfront.jpg", -45.033200000000001, 168.65989999999999, "Lake Wakatipu waterfront", "Public waterfront; no set hours.", 4.6m, "2 hours", 1469, "https://www.queenstownnz.co.nz/listing/queenstown-bay/605/" },
                    { 15, "Open year round; track conditions can be muddy, icy or affected by storms.", "Dec - Feb", "No booking; use the car park trailhead and carry weather-appropriate gear.", "An easy, picturesque walk through native bush to a secluded cove on Lake Wakatipu.", 1, "assets/images/bobs-cove-track.jpg", -45.068199999999997, 168.53980000000001, "Bobs Cove Track", "Public walking track; check DOC conditions before visit.", 4.9m, "3 hours", 682, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/otago/places/queenstown-area/things-to-do/tracks/bobs-cove-track/" },
                    { 16, "Open year round; spring and summer are especially colourful.", "Sep - Apr", "No booking for gardens; weather and events may affect some areas.", "Historic riverside gardens beside Hagley Park.", 6, "assets/images/christchurch-botanic-gardens.jpg", -43.5306, 172.62620000000001, "Christchurch Botanic Gardens", "Check Christchurch City Council’s current garden and visitor-centre hours before visit.", 4.8m, "2 hours", 957, "https://ccc.govt.nz/parks-and-gardens/christchurch-botanic-gardens" },
                    { 17, "Open year round; indoor attraction.", "Year round", "Advance booking recommended in peak periods; allow time for timed experiences.", "Interactive Antarctic visitor experience beside Christchurch Airport.", 6, "assets/images/international-antarctic-centre.jpg", -43.486199999999997, 172.5488, "International Antarctic Centre", "Check the International Antarctic Centre’s current daily hours before visit.", 4.5m, "3 hours", 176, "https://www.iceberg.co.nz/" },
                    { 18, "Open year round, subject to wind and weather.", "Year round", "Book ahead in peak periods; gondola operations can be affected by high winds.", "Gondola ride with views over Lyttelton Harbour and the Canterbury Plains.", 6, "assets/images/christchurch-gondola.jpg", -43.582799999999999, 172.71190000000001, "Christchurch Gondola", "Check Christchurch Gondola’s current operating hours before visit.", 4.4m, "2 hours", 1075, "https://www.christchurchgondola.co.nz/" },
                    { 19, "Open year round; indoor attraction.", "Year round", "Booking recommended for groups; allow time for nearby central-city parking.", "Museum telling the story of the Canterbury earthquakes and recovery.", 6, "assets/images/quake-city.jpg", -43.528399999999998, 172.63220000000001, "Quake City", "Check Quake City’s current hours before visit.", 4.6m, "2 hours", 1438, "https://www.quakecity.co.nz/" },
                    { 20, "Open year round; indoor/outdoor exhibits.", "Year round", "General admission is usually free; book guided tours or special activities if required.", "Discover the history of New Zealand military aviation through engaging exhibits and historic aircraft.", 6, "assets/images/air-force-museum-of-new-zealand.jpg", -43.548299999999998, 172.54599999999999, "Air Force Museum of New Zealand", "Check the museum’s current opening hours before visit.", 4.3m, "3 hours", 630, "https://www.airforcemuseum.co.nz/" },
                    { 21, "Open year round; outdoor animal experiences vary with weather and animal welfare needs.", "Year round", "Advance booking recommended in school holidays; check encounter times and weather advice.", "New Zealand's only open-range zoo, offering unique up-close animal encounters.", 6, "assets/images/orana-wildlife-park.jpg", -43.468200000000003, 172.46360000000001, "Orana Wildlife Park", "Check Orana’s current daily hours before visit.", 4.2m, "5 hours", 314, "https://www.oranawildlifepark.co.nz/" },
                    { 22, "Open year round; night tours and animal encounters may be seasonal.", "Year round", "Book kiwi/night tours and encounters in advance.", "A wildlife park dedicated to New Zealand's native species and Māori cultural experiences.", 6, "assets/images/willowbank-wildlife-reserve.jpg", -43.467799999999997, 172.59370000000001, "Willowbank Wildlife Reserve", "Check Willowbank’s current visitor hours before visit.", 4.5m, "3 hours", 513, "https://www.willowbank.co.nz/" },
                    { 23, "Year round; harbour cruises and wildlife trips are weather dependent.", "Sep - Apr", "Book harbour cruises in advance; allow for the drive and possible weather cancellations.", "Banks Peninsula harbour town, suitable as a full-day excursion from Christchurch.", 6, "assets/images/akaroa-harbour-day-trip.jpg", -43.805799999999998, 172.9675, "Akaroa Harbour day trip", "Check the chosen operator’s timetable before visit.", 4.6m, "8 hours", 1144, "https://www.christchurchnz.com/explore/akaroa" },
                    { 24, "Open year round; market and ferry activity varies by day.", "Year round", "No booking for the waterfront; check parking and cruise-ship/event impacts.", "A historic port town set in a collapsed volcanic crater, featuring quirky shops and stunning views.", 6, "assets/images/lyttelton-harbour.jpg", -43.601500000000001, 172.72120000000001, "Lyttelton Harbour", "Public harbour area; check individual businesses and event schedules.", 4.9m, "3 hours", 519, "https://www.christchurchnz.com/explore/lyttelton" },
                    { 25, "Open year round; best in settled conditions.", "Dec - Feb", "No booking; check surf, tide and weather warnings before swimming or rock access.", "A popular coastal suburb known for its relaxed surf culture and iconic volcanic rock formations.", 6, "assets/images/sumner-beach-and-cave-rock.jpg", -43.567, 172.75839999999999, "Sumner Beach and Cave Rock", "Public beach; no set hours.", 4.4m, "3 hours", 1377, "https://ccc.govt.nz/parks-and-gardens/explore-parks/coastal-parks/sumner-beach" },
                    { 26, "Operates seasonally and may be weather dependent.", "Year round", "Advance booking recommended; rain, wind or river conditions may affect service.", "A tranquil and iconic Christchurch experience gliding along the Avon River in a flat-bottomed boat.", 6, "assets/images/punting-on-the-avon.jpg", -43.533200000000001, 172.6277, "Punting on the Avon", "Check Punting on the Avon’s current departure times before visit.", 4.6m, "2 hours", 497, "https://www.puntingontheavon.co.nz/" },
                    { 27, "Open year round; trading hours vary by stall and day.", "Year round", "No booking for market browsing; book restaurants separately if required.", "A bustling indoor market offering diverse street food, fresh local produce, and boutique stalls.", 6, "assets/images/riverside-market.jpg", -43.532299999999999, 172.63239999999999, "Riverside Market", "Check Riverside Market’s current opening hours before visit.", 4.6m, "2 hours", 890, "https://riverside.nz/" },
                    { 28, "Open year round; exposed tracks are best in dry, low-wind conditions.", "Year round", "No booking; carry water, sun protection and layers; avoid exposed routes in severe weather.", "A rugged volcanic range offering extensive walking and biking trails with panoramic city and harbour views.", 6, "assets/images/port-hills.jpg", -43.633800000000001, 172.6223, "Port Hills", "Public tracks; check Christchurch City Council and weather/fire restrictions before visit.", 4.5m, "4 hours", 949, "https://ccc.govt.nz/parks-and-gardens/explore-parks/port-hills" },
                    { 29, "Confirm reopening and temporary exhibition arrangements before planning.", "Year round", "No booking assumption; verify venue location, ticketing and opening information first.", "A cultural heritage museum showcasing the rich natural and human history of the Canterbury region.", 6, "assets/images/canterbury-museum.jpg", -43.531199999999998, 172.6268, "Canterbury Museum", "Check the Canterbury Museum website before visit; redevelopment may affect access.", 4.5m, "2 hours", 305, "https://canterburymuseum.com/" },
                    { 30, "Open year round; galleries, shops and events have separate schedules.", "Year round", "No booking to explore public areas; book performances, tours or workshops separately.", "A vibrant hub for arts, culture, and education set within stunning restored Gothic Revival buildings.", 6, "assets/images/the-arts-centre.jpg", -43.531300000000002, 172.6284, "The Arts Centre", "Check The Arts Centre’s current building and venue hours before visit.", 4.7m, "2 hours", 744, "https://artscentre.org.nz/" },
                    { 31, "Open year round; outdoor SkyWalk/SkyJump is weather dependent.", "Year round", "Pre-book SkyWalk/SkyJump and peak observation visits; outdoor activities can be weather cancelled.", "Observation tower with panoramic views across Auckland and the Hauraki Gulf.", 3, "assets/images/sky-tower.jpg", -36.848500000000001, 174.76220000000001, "Sky Tower", "Check SkyCity’s current attraction hours before visit.", 4.5m, "2 hours", 535, "https://skycityauckland.co.nz/sky-tower/" },
                    { 32, "Open year round; indoor museum and outdoor Domain.", "Year round", "Book paid exhibitions or events in advance; allow time for parking or public transport.", "Museum of natural history and Aotearoa New Zealand stories in the Domain.", 3, "assets/images/auckland-museum.jpg", -36.860599999999998, 174.77780000000001, "Auckland Museum", "Check Auckland Museum’s current opening hours before visit.", 4.5m, "3 hours", 1112, "https://www.aucklandmuseum.com/" },
                    { 33, "Open year round; outdoor areas and encounters are weather dependent.", "Year round", "Advance booking recommended in peak periods; check animal encounter requirements.", "Conservation-focused zoo in Western Springs.", 3, "assets/images/auckland-zoo.jpg", -36.863100000000003, 174.7176, "Auckland Zoo", "Check Auckland Zoo’s current daily hours before visit.", 4.5m, "4 hours", 981, "https://www.aucklandzoo.co.nz/" },
                    { 34, "Open year round; ferry sailings and outdoor activities depend on weather.", "Nov - Mar", "Book ferries, tours and popular wineries in advance; allow for weather or sea-condition disruptions.", "Hauraki Gulf island for beaches, art and vineyard visits; allow a full day.", 3, "assets/images/waiheke-island-day-trip.jpg", -36.843000000000004, 174.767, "Waiheke Island day trip", "Check Fullers360 ferry timetable and chosen winery/attraction hours before visit.", 4.2m, "8 hours", 757, "https://www.fullers.co.nz/destinations-and-experiences/waiheke-island/" },
                    { 35, "Open year round; ferry service and summit track conditions are weather dependent.", "Nov - Mar", "Pre-book ferry; take food, water and sun protection—there are no shops on Rangitoto.", "Volcanic island day trip with a summit walk and harbour views.", 3, "assets/images/rangitoto-island-day-trip.jpg", -36.843000000000004, 174.767, "Rangitoto Island day trip", "Check Fullers360 timetable and DOC island advice before visit.", 4.5m, "7 hours", 1154, "https://www.aucklandnz.com/explore/rangitoto-island" },
                    { 36, "Open year round; indoor attraction.", "Year round", "Advance booking recommended in weekends and school holidays.", "An iconic underwater attraction featuring penguin colonies, shark tunnels, and marine rescue exhibits.", 3, "assets/images/sea-life-kelly-tarltons-aquarium.jpg", -36.847499999999997, 174.81829999999999, "SEA LIFE Kelly Tarlton’s Aquarium", "Check SEA LIFE Kelly Tarlton’s current hours before visit.", 4.3m, "3 hours", 425, "https://www.visitsealife.com/auckland/" },
                    { 37, "Open year round; indoor/outdoor exhibits.", "Year round", "Book special events and school-holiday activities in advance where offered.", "An interactive museum exploring the history and future of New Zealand's transport and technology.", 3, "assets/images/museum-of-transport-and-technology.jpg", -36.866500000000002, 174.71789999999999, "Museum of Transport and Technology", "Check MOTAT’s current opening hours before visit.", 4.6m, "3 hours", 1277, "https://www.motat.nz/" },
                    { 38, "Open year round; harbour sailing experiences are weather dependent.", "Year round", "Book heritage sailings in advance; sailings can be weather affected.", "Discover the stories of the people and ships that shaped New Zealand's seafaring history.", 3, "assets/images/new-zealand-maritime-museum.jpg", -36.841900000000003, 174.76339999999999, "New Zealand Maritime Museum", "Check the Maritime Museum’s current hours before visit.", 4.8m, "2 hours", 1357, "https://www.maritimemuseum.co.nz/" },
                    { 39, "Open year round; gallery programme and special exhibitions vary.", "Year round", "Book ticketed exhibitions or events in advance when required.", "New Zealand's largest visual arts institution, housing an extensive collection of national and international art.", 3, "assets/images/auckland-art-gallery-toi-o-tamaki.jpg", -36.850200000000001, 174.76609999999999, "Auckland Art Gallery Toi o Tāmaki", "Check Auckland Art Gallery’s current opening hours before visit.", 4.4m, "2 hours", 989, "https://www.aucklandartgallery.com/" },
                    { 40, "Open year round; exposed summit is best in settled weather.", "Year round", "No booking; use daylight hours and allow for a walk from parking.", "A significant volcanic peak and historic park offering 360-degree views of Auckland.", 3, "assets/images/maungakiekie-one-tree-hill.jpg", -36.896700000000003, 174.7765, "Maungakiekie / One Tree Hill", "Public park; check Cornwall Park and local weather information before visit.", 4.3m, "3 hours", 1426, "https://cornwallpark.co.nz/" },
                    { 41, "Open year round; ferry and outdoor walk conditions are weather dependent.", "Year round", "No booking for North Head; ferry services can be weather affected and tunnels may have access limits.", "A charming historic village paired with a coastal reserve known for its military tunnels and harbour views.", 3, "assets/images/devonport-waterfront-and-north-head.jpg", -36.832900000000002, 174.7961, "Devonport waterfront and North Head", "Check Fullers360 timetable and DOC North Head information before visit.", 4.5m, "4 hours", 1480, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/auckland/places/north-head-historic-reserve/" },
                    { 42, "Open year round; scheduled ferry access and outdoor walking are weather dependent.", "Year round", "Book ferry well ahead; take food, water and walking gear—check weather cancellations.", "A renowned open sanctuary for native birdlife and conservation, accessible by a scenic ferry ride.", 3, "assets/images/tiritiri-matangi-island-day-trip.jpg", -36.843000000000004, 174.767, "Tiritiri Matangi Island day trip", "Check Explore Group ferry timetable and DOC visitor information before visit.", 4.6m, "8 hours", 248, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/auckland/places/tiritiri-matangi-open-sanctuary/" },
                    { 43, "Open year round; best in settled weather and daylight.", "Year round", "No booking; check swim, weather and traffic conditions before visit.", "A picturesque coastal route leading to a vibrant seaside suburb with a beautiful sandy beach and eateries.", 3, "assets/images/mission-bay-and-tamaki-drive.jpg", -36.847999999999999, 174.83150000000001, "Mission Bay and Tāmaki Drive", "Public waterfront; no set hours.", 4.7m, "3 hours", 441, "https://www.aucklandnz.com/explore/mission-bay" },
                    { 44, "Open year round; events may limit vehicle access or parking.", "Sep - Apr", "No booking; use daylight hours and combine with Auckland Museum if suitable.", "Auckland's oldest park, featuring expansive green spaces, walking tracks, and the historic Wintergardens.", 3, "assets/images/auckland-domain.jpg", -36.8596, 174.7758, "Auckland Domain", "Public park; check Auckland Council information for event impacts.", 4.5m, "2 hours", 437, "https://www.aucklandcouncil.govt.nz/parks-recreation/get-outdoors/find-a-park/Pages/park-details.aspx?parkID=1" },
                    { 45, "Open year round; indoor attraction.", "Year round", "Advance booking recommended; arrive before your timed session.", "An immersive and wildly imaginative experience exploring the worlds of horror, sci-fi, and fantasy film-making.", 3, "assets/images/weta-workshop-unleashed.jpg", -36.8489, 174.7621, "Wētā Workshop Unleashed", "Check Wētā Workshop Unleashed’s current session times before visit.", 4.6m, "2 hours", 1343, "https://tours.wetaworkshop.com/auckland/" },
                    { 46, "Open year round; daily boat departures across Lake Te Anau.", "Year round", "Advance booking recommended; check-in 30 minutes prior to departure; requires bending/walking in caves.", "A magical underground experience starting with a scenic lake cruise to a hidden limestone cave illuminated by thousands of glowworms.", 8, "assets/images/te-anau-glowworm-caves.jpg", -45.416499999999999, 167.71180000000001, "Te Anau Glowworm Caves", "Open 7 days, daily departures. Check official website before visit.", 4.5m, "2.25 hours", 850, "https://www.realnz.com/en/experiences/glowworm-caves/te-anau-glowworm-caves/" },
                    { 47, "Great Walks season runs late October to April; day walks accessible year round in good weather.", "Sep - Apr", "No booking required for day walks; check DOC weather and track alerts before setting out.", "An accessible section of the famous Kepler Great Walk, leading through ancient beech forests along the lake shore.", 8, "assets/images/kepler-track-day-walk.jpg", -45.439799999999998, 167.68299999999999, "Kepler Track Day Walk", "Public walking track; accessible during daylight hours.", 4.8m, "3 hours", 620, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/tracks/kepler-track/" },
                    { 48, "Operates year round; full-day excursion departing from Manapouri.", "Nov - Mar", "Advance booking essential; departures leave from Pearl Harbour in Manapouri.", "A tranquil and remote wilderness cruise through a deep, pristine fiord known for its serene waters and native wildlife.", 8, "assets/images/doubtful-sound-wilderness-cruise.jpg", -45.563600000000001, 167.6163, "Doubtful Sound Wilderness Cruise", "Check official website before visit for seasonal departure times.", 4.7m, "7 hours", 540, "https://www.realnz.com/en/experiences/cruises/doubtful-sound-wilderness-cruises/" },
                    { 49, "Open year round from dawn to dusk.", "Year round", "Free entry (gold coin donation appreciated); guided tour feeds can be booked.", "A lakeside conservation haven providing a rare chance to see endangered native birds like the Takahē up close.", 8, "assets/images/te-anau-bird-sanctuary.jpg", -45.426200000000001, 167.70509999999999, "Te Anau Bird Sanctuary", "Open daily from dawn to dusk.", 4.6m, "1 hours", 310, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/te-anau-bird-sanctuary/" },
                    { 50, "Open year round; an excellent indoor activity.", "Year round", "Advance booking recommended for popular evening screenings.", "A boutique cinema showcasing the custom-shot documentary 'Ata Whenua - Shadowland', capturing Fiordland's wild landscapes.", 8, "assets/images/fiordland-cinema.jpg", -45.414999999999999, 167.71350000000001, "Fiordland Cinema", "Check official website for current screening showtimes.", 4.8m, "1 hours", 420, "https://www.fiordlandcinema.co.nz/" }
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
