using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddItineraryAttractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[,]
                {
                    { 9, "Year round", "Lakeside gardens and an easy walking loop near central Queenstown.", 1, "assets/images/queenstown-gardens.jpg", -45.031999999999996, 168.6694, "Queenstown Gardens", 4.7m, "2 hours", 850 },
                    { 10, "Year round", "High-speed jet boat ride through the Shotover River canyons.", 1, "assets/images/shotover-jet.jpg", -44.997300000000003, 168.7072, "Shotover Jet", 4.8m, "2 hours", 1600 },
                    { 11, "Year round", "Native wildlife conservation park near town centre.", 1, "assets/images/kiwi-park.jpg", -45.028799999999997, 168.6585, "Kiwi Park Queenstown", 4.6m, "2 hours", 700 },
                    { 12, "Year round", "Historic gold-mining village with heritage streets and riverside walks.", 1, "assets/images/arrowtown.jpg", -44.939399999999999, 168.83099999999999, "Arrowtown Historic Precinct", 4.7m, "3 hours", 1100 },
                    { 13, "Year round", "Private hot pools overlooking the Shotover River canyon.", 1, "assets/images/onsen.jpg", -45.0, 168.73500000000001, "Onsen Hot Pools", 4.7m, "2 hours", 900 },
                    { 14, "Year round", "Historic riverside gardens beside Hagley Park.", 6, "assets/images/christchurch-botanic-gardens.jpg", -43.5291, 172.62, "Christchurch Botanic Gardens", 4.7m, "2 hours", 1200 },
                    { 15, "Year round", "Gondola ride with views over Lyttelton Harbour and the Canterbury Plains.", 6, "assets/images/christchurch-gondola.jpg", -43.564300000000003, 172.7226, "Christchurch Gondola", 4.6m, "2 hours", 950 },
                    { 16, "Year round", "Interactive Antarctic visitor experience beside Christchurch Airport.", 6, "assets/images/antarctic-centre.jpg", -43.489100000000001, 172.53120000000001, "International Antarctic Centre", 4.6m, "3 hours", 1000 },
                    { 17, "Year round", "Museum telling the story of the Canterbury earthquakes and recovery.", 6, "assets/images/quake-city.jpg", -43.536799999999999, 172.63759999999999, "Quake City", 4.5m, "2 hours", 600 },
                    { 18, "Sep - Apr", "Banks Peninsula harbour town, suitable as a full-day excursion from Christchurch.", 6, "assets/images/akaroa.jpg", -43.804499999999997, 172.9676, "Akaroa Harbour", 4.8m, "6 hours", 1000 },
                    { 19, "Year round", "Observation tower with panoramic views across Auckland and the Hauraki Gulf.", 3, "assets/images/sky-tower.jpg", -36.848500000000001, 174.76329999999999, "Sky Tower", 4.6m, "2 hours", 1800 },
                    { 20, "Year round", "Museum of natural history and Aotearoa New Zealand stories in the Domain.", 3, "assets/images/auckland-museum.jpg", -36.860100000000003, 174.77879999999999, "Auckland Museum", 4.7m, "3 hours", 1400 },
                    { 21, "Oct - Apr", "Hauraki Gulf island for beaches, art and vineyard visits; allow a full day.", 3, "assets/images/waiheke.jpg", -36.7806, 175.00700000000001, "Waiheke Island", 4.8m, "8 hours", 1600 },
                    { 22, "Oct - Apr", "Volcanic island day trip with a summit walk and harbour views.", 3, "assets/images/rangitoto.jpg", -36.787999999999997, 174.86000000000001, "Rangitoto Island", 4.8m, "6 hours", 1200 },
                    { 23, "Year round", "Conservation-focused zoo in Western Springs.", 3, "assets/images/auckland-zoo.jpg", -36.863799999999998, 174.71809999999999, "Auckland Zoo", 4.6m, "3 hours", 1300 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 23);
        }
    }
}
