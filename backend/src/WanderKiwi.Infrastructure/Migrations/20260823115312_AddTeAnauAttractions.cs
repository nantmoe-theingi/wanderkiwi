using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeAnauAttractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 13,
                column: "DestinationId",
                value: 8);

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[,]
                {
                    { 46, "Open year round; daily boat departures across Lake Te Anau.", "Year round", "Advance booking recommended; check-in 30 minutes prior to departure; requires bending/walking in caves.", "A magical underground experience starting with a scenic lake cruise to a hidden limestone cave illuminated by thousands of glowworms.", 8, "assets/images/te-anau-glowworm-caves.jpg", -45.416499999999999, 167.71180000000001, "Te Anau Glowworm Caves", "Open 7 days, daily departures. Check official website before visit.", 4.5m, "2.25 hours", 850, "https://www.realnz.com/en/experiences/glowworm-caves/te-anau-glowworm-caves/" },
                    { 47, "Great Walks season runs late October to April; day walks accessible year round in good weather.", "Sep - Apr", "No booking required for day walks; check DOC weather and track alerts before setting out.", "An accessible section of the famous Kepler Great Walk, leading through ancient beech forests along the lake shore.", 8, "assets/images/kepler-track-day-walk.jpg", -45.439799999999998, 167.68299999999999, "Kepler Track Day Walk", "Public walking track; accessible during daylight hours.", 4.8m, "3 hours", 620, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/tracks/kepler-track/" },
                    { 48, "Operates year round; full-day excursion departing from Manapouri.", "Nov - Mar", "Advance booking essential; departures leave from Pearl Harbour in Manapouri.", "A tranquil and remote wilderness cruise through a deep, pristine fiord known for its serene waters and native wildlife.", 8, "assets/images/doubtful-sound-wilderness-cruise.jpg", -45.563600000000001, 167.6163, "Doubtful Sound Wilderness Cruise", "Check official website before visit for seasonal departure times.", 4.7m, "7 hours", 540, "https://www.realnz.com/en/experiences/cruises/doubtful-sound-wilderness-cruises/" },
                    { 49, "Open year round from dawn to dusk.", "Year round", "Free entry (gold coin donation appreciated); guided tour feeds can be booked.", "A lakeside conservation haven providing a rare chance to see endangered native birds like the Takahē up close.", 8, "assets/images/te-anau-bird-sanctuary.jpg", -45.426200000000001, 167.70509999999999, "Te Anau Bird Sanctuary", "Open daily from dawn to dusk.", 4.6m, "1 hours", 310, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/fiordland/places/fiordland-national-park/things-to-do/te-anau-bird-sanctuary/" },
                    { 50, "Open year round; an excellent indoor activity.", "Year round", "Advance booking recommended for popular evening screenings.", "A boutique cinema showcasing the custom-shot documentary 'Ata Whenua - Shadowland', capturing Fiordland's wild landscapes.", 8, "assets/images/fiordland-cinema.jpg", -45.414999999999999, 167.71350000000001, "Fiordland Cinema", "Check official website for current screening showtimes.", 4.8m, "1 hours", 420, "https://www.fiordlandcinema.co.nz/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 13,
                column: "DestinationId",
                value: 1);
        }
    }
}
