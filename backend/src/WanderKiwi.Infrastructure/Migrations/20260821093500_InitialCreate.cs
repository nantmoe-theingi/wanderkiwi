using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Islands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslandId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    BestTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecommendedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                    { 6, "City" }
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
                columns: new[] { "Id", "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[,]
                {
                    { 1, "Dec - Feb", "Take in breathtaking views of Queenstown, Lake Wakatipu and the surrounding mountains.", 1, "assets/images/skyline-queenstown.jpg", -45.031199999999998, 168.6626, "Skyline Queenstown", 4.8m, "2-3 hours", 1200 },
                    { 2, "Dec - Apr", "A challenging alpine hike offering spectacular views over Queenstown and Lake Wakatipu.", 1, "assets/images/ben-lomond.jpg", -45.009700000000002, 168.61670000000001, "Ben Lomond Track", 4.7m, "6-8 hours", 713 },
                    { 3, "Dec - Feb", "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.", 1, "assets/images/tss-earnslaw.jpg", -45.030999999999999, 168.66, "TSS Earnslaw Cruise", 4.7m, "1.5-2 hours", 980 },
                    { 4, "Dec - Feb", "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.", 8, "assets/images/milford.jpg", -44.641399999999997, 167.9254, "Milford Sound", 4.9m, "4-6 hours", 1420 },
                    { 5, "Dec - Feb", "Step into the lush pastures of the Shire from The Lord of the Rings film trilogy.", 7, "assets/images/hobbiton.jpg", -37.872100000000003, 175.68260000000001, "Hobbiton Movie Set", 4.8m, "2-3 hours", 1250 },
                    { 6, "Nov - Mar", "Explore colourful geothermal pools, volcanic landscapes and geothermal activity.", 2, "assets/images/waiotapu.jpg", -38.357399999999998, 176.36680000000001, "Wai-O-Tapu Thermal Wonderland", 4.7m, "2-3 hours", 1100 },
                    { 7, "Sep - Apr", "New Zealand's highest mountain surrounded by spectacular alpine landscapes and glaciers.", 6, "assets/images/mountcook.jpg", -43.734400000000001, 170.14109999999999, "Aoraki / Mount Cook", 4.9m, "1-2 days", 1500 },
                    { 8, "Dec - Mar", "A stunning coastal national park known for golden beaches, clear water and walking trails.", 6, "assets/images/abel-tasman.jpg", -40.900599999999997, 173.07689999999999, "Abel Tasman National Park", 4.8m, "1-2 days", 900 }
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
                    { 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "AttractionCategories",
                columns: new[] { "AttractionId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 3 },
                    { 5, 3 },
                    { 5, 4 },
                    { 6, 1 },
                    { 6, 3 },
                    { 7, 1 },
                    { 7, 2 }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttractionCategories");

            migrationBuilder.DropTable(
                name: "DestinationCategories");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Islands");
        }
    }
}
