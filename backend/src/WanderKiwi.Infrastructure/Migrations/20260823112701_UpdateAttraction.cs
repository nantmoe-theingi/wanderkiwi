using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvailabilityNote",
                table: "Attractions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookingNote",
                table: "Attractions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OpeningHoursNote",
                table: "Attractions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Attractions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Latitude", "Longitude", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; alpine weather can affect gondola operations.", "Year round", "Pre-book gondola and activities in peak periods; weather may affect operations.", -45.028700000000001, 168.6558, "Check Skyline’s current operating hours before visit.", 4.7m, "3 hours", 3447, "https://www.skyline.co.nz/en/queenstown/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Seasonal timetable; services can be affected by lake and weather conditions.", "Nov - Mar", "Advance booking recommended; arrive at the wharf early and check weather cancellations.", "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.", "assets/images/tss-earnslaw-cruise.jpg", -45.032600000000002, 168.6575, "TSS Earnslaw Cruise", "Check RealNZ’s current sailing timetable before visit.", 4.4m, "3 hours", 80, "https://www.realnz.com/en/experiences/cruises/tss-earnslawe/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Operates year round, subject to river and weather conditions.", "Year round", "Advance booking recommended; trips can be delayed or cancelled for weather or river conditions.", "High-speed jet boat ride through the Shotover River canyons.", "assets/images/shotover-jet.jpg", -44.982900000000001, 168.67019999999999, "Shotover Jet", "Check Shotover Jet’s current departure times before visit.", 4.3m, "2 hours", 269, "https://www.shotoverjet.com/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Year round; check current seasonal operating times.", "Year round", "Book online or check the official site before visiting; wildlife encounters and conservation shows run daily.", "Native wildlife conservation park near town centre.", 1, "assets/images/kiwi-park-queenstown.jpg", -45.029600000000002, 168.6557, "Kiwi Park Queenstown", "Daily. The official site lists 9:30am–6:30pm with last entry 5:45pm, and a shorter 9:30am–5pm schedule with last entry 4:15pm; confirm the applicable season.", 4.6m, "2 hours", 355, "https://kiwibird.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; autumn colour is a seasonal highlight.", "Sep - Apr", "No booking normally required; use daylight hours and allow for weather.", "Lakeside gardens and an easy walking loop near central Queenstown.", 1, "assets/images/queenstown-gardens.jpg", -45.0336, 168.66309999999999, "Queenstown Gardens", "Public gardens; check Queenstown Lakes District Council information for facility updates.", 4.4m, "2 hours", 1024, "https://www.queenstownnz.co.nz/listing/queenstown-gardens/120/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; autumn is especially popular.", "Year round", "No booking for the precinct; allow extra time for parking during autumn and events.", "Historic gold-mining village with heritage streets and riverside walks.", 1, "assets/images/arrowtown-historic-precinct.jpg", -44.9392, 168.8313, "Arrowtown Historic Precinct", "Public streets are accessible daily; check individual shops and museums for their hours.", 4.3m, "3 hours", 864, "https://www.arrowtown.com/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; vineyard and cellar-door experiences vary seasonally.", "Year round", "Book tastings, tours and dining in advance; appoint a sober driver or use a tour.", "Explore the region's oldest vineyards and New Zealand's largest wine cave.", 1, "assets/images/gibbston-valley-winery.jpg", -45.011600000000001, 168.86869999999999, "Gibbston Valley Winery", "Check Gibbston Valley’s current cellar-door and restaurant hours before visit.", 4.3m, "4 hours", 861, "https://www.gibbstonvalley.com/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; popular in winter and evenings.", "Year round", "Advance booking is essential; outdoor sessions may be weather affected.", "Private hot pools overlooking the Shotover River canyon.", 1, "assets/images/onsen-hot-pools.jpg", -44.984000000000002, 168.6687, "Onsen Hot Pools", "Check Onsen Hot Pools’ current session times before visit.", 4.5m, "2 hours", 17, "https://www.onsen.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round, subject to wind and weather limits.", "Advance booking recommended; weather can delay or cancel jumps.", "The world's first commercial bungy jump site, located at the historic Kawarau Bridge.", "assets/images/kawarau-bungy-centre.jpg", -45.013399999999997, 168.89060000000001, "Kawarau Bungy Centre", "Check AJ Hackett’s current operating hours before visit.", 4.4m, "3 hours", 141, "https://www.bungy.co.nz/queenstown/kawarau-bungy-centre/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Skiing is seasonal; sightseeing and summer operations vary.", "Book rentals or lessons in advance; alpine road and lift access are weather dependent.", "A premier ski resort offering spectacular winter sports and summer sightseeing.", "assets/images/coronet-peak.jpg", -44.928699999999999, 168.73599999999999, "Coronet Peak", "Check NZSki’s current lift, road and operating status before visit.", 4.5m, "5 hours", 2400, "https://www.coronetpeak.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Best in dry conditions; snow, ice and strong wind can affect winter access.", "No booking; take water, layers and suitable footwear.", "A rewarding hike through pine forest to panoramic views of the Wakatipu basin.", "assets/images/queenstown-hill-time-walk.jpg", -45.029499999999999, 168.6661, "Queenstown Hill Time Walk", "Public walking track; start in daylight and check DOC/Queenstown weather advice.", 4.8m, "3 hours", 36, "https://www.queenstownnz.co.nz/listing/queenstown-hill-time-walk/146/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; winter snow/ice and storm conditions may affect roads.", "No booking; fuel up, allow extra driving time, and do not rely on the route during road closures.", "A stunning coastal road trip tracing the edge of Lake Wakatipu to the gateway of Mount Aspiring National Park.", "assets/images/glenorchy-scenic-drive.jpg", -44.846800000000002, 168.38460000000001, "Glenorchy Scenic Drive", "Public road; check NZTA and weather conditions before departure.", 4.6m, "6 hours", 1187, "https://www.queenstownnz.co.nz/things-to-do/scenic-drives/glenorchy-road/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Year round; road, avalanche and severe-weather disruptions are possible.", "Nov - Mar", "Advance booking strongly recommended; carry food/water and expect weather-related changes.", "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.", "assets/images/milford-sound-day-trip.jpg", -44.671500000000002, 167.9255, "Milford Sound day trip", "Check operator timetable and NZTA road conditions before visit.", 4.5m, "10 hours", 415, "https://www.realnz.com/en/experiences/cruises/milford-sound-cruises/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; best enjoyed in settled weather and daylight.", "No booking; check weather and water-safety advice before lake activities.", "A vibrant promenade perfect for a scenic stroll, lakeside dining, or watching the sunset.", 1, "assets/images/lake-wakatipu-waterfront.jpg", -45.033200000000001, 168.65989999999999, "Lake Wakatipu waterfront", "Public waterfront; no set hours.", 4.6m, 1469, "https://www.queenstownnz.co.nz/listing/queenstown-bay/605/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; track conditions can be muddy, icy or affected by storms.", "Dec - Feb", "No booking; use the car park trailhead and carry weather-appropriate gear.", "An easy, picturesque walk through native bush to a secluded cove on Lake Wakatipu.", 1, "assets/images/bobs-cove-track.jpg", -45.068199999999997, 168.53980000000001, "Bobs Cove Track", "Public walking track; check DOC conditions before visit.", 4.9m, "3 hours", 682, "https://www.doc.govt.nz/parks-and-recreation/places-to-go/otago/places/queenstown-area/things-to-do/tracks/bobs-cove-track/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; spring and summer are especially colourful.", "Sep - Apr", "No booking for gardens; weather and events may affect some areas.", "Historic riverside gardens beside Hagley Park.", "assets/images/christchurch-botanic-gardens.jpg", -43.5306, 172.62620000000001, "Christchurch Botanic Gardens", "Check Christchurch City Council’s current garden and visitor-centre hours before visit.", 4.8m, "2 hours", 957, "https://ccc.govt.nz/parks-and-gardens/christchurch-botanic-gardens" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; indoor attraction.", "Advance booking recommended in peak periods; allow time for timed experiences.", "Interactive Antarctic visitor experience beside Christchurch Airport.", "assets/images/international-antarctic-centre.jpg", -43.486199999999997, 172.5488, "International Antarctic Centre", "Check the International Antarctic Centre’s current daily hours before visit.", "3 hours", 176, "https://www.iceberg.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round, subject to wind and weather.", "Year round", "Book ahead in peak periods; gondola operations can be affected by high winds.", "Gondola ride with views over Lyttelton Harbour and the Canterbury Plains.", "assets/images/christchurch-gondola.jpg", -43.582799999999999, 172.71190000000001, "Christchurch Gondola", "Check Christchurch Gondola’s current operating hours before visit.", 4.4m, "2 hours", 1075, "https://www.christchurchgondola.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; indoor attraction.", "Booking recommended for groups; allow time for nearby central-city parking.", "Museum telling the story of the Canterbury earthquakes and recovery.", 6, "assets/images/quake-city.jpg", -43.528399999999998, 172.63220000000001, "Quake City", "Check Quake City’s current hours before visit.", 1438, "https://www.quakecity.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AvailabilityNote", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; indoor/outdoor exhibits.", "General admission is usually free; book guided tours or special activities if required.", "Discover the history of New Zealand military aviation through engaging exhibits and historic aircraft.", 6, "assets/images/air-force-museum-of-new-zealand.jpg", -43.548299999999998, 172.54599999999999, "Air Force Museum of New Zealand", "Check the museum’s current opening hours before visit.", 4.3m, 630, "https://www.airforcemuseum.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; outdoor animal experiences vary with weather and animal welfare needs.", "Year round", "Advance booking recommended in school holidays; check encounter times and weather advice.", "New Zealand's only open-range zoo, offering unique up-close animal encounters.", 6, "assets/images/orana-wildlife-park.jpg", -43.468200000000003, 172.46360000000001, "Orana Wildlife Park", "Check Orana’s current daily hours before visit.", 4.2m, "5 hours", 314, "https://www.oranawildlifepark.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Open year round; night tours and animal encounters may be seasonal.", "Year round", "Book kiwi/night tours and encounters in advance.", "A wildlife park dedicated to New Zealand's native species and Māori cultural experiences.", 6, "assets/images/willowbank-wildlife-reserve.jpg", -43.467799999999997, 172.59370000000001, "Willowbank Wildlife Reserve", "Check Willowbank’s current visitor hours before visit.", 4.5m, "3 hours", 513, "https://www.willowbank.co.nz/" });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[] { "Year round; harbour cruises and wildlife trips are weather dependent.", "Sep - Apr", "Book harbour cruises in advance; allow for the drive and possible weather cancellations.", "Banks Peninsula harbour town, suitable as a full-day excursion from Christchurch.", 6, "assets/images/akaroa-harbour-day-trip.jpg", -43.805799999999998, 172.9675, "Akaroa Harbour day trip", "Check the chosen operator’s timetable before visit.", "8 hours", 1144, "https://www.christchurchnz.com/explore/akaroa" });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "AvailabilityNote", "BestTime", "BookingNote", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "OpeningHoursNote", "Rating", "RecommendedDuration", "ReviewCount", "SourceUrl" },
                values: new object[,]
                {
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
                    { 45, "Open year round; indoor attraction.", "Year round", "Advance booking recommended; arrive before your timed session.", "An immersive and wildly imaginative experience exploring the worlds of horror, sci-fi, and fantasy film-making.", 3, "assets/images/weta-workshop-unleashed.jpg", -36.8489, 174.7621, "Wētā Workshop Unleashed", "Check Wētā Workshop Unleashed’s current session times before visit.", 4.6m, "2 hours", 1343, "https://tours.wetaworkshop.com/auckland/" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "Relaxation" },
                    { 8, "Wildlife" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "AvailabilityNote",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "BookingNote",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "OpeningHoursNote",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Attractions");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestTime", "Latitude", "Longitude", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Dec - Feb", -45.031199999999998, 168.6626, 4.8m, "2-3 hours", 1200 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestTime", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Dec - Apr", "A challenging alpine hike offering spectacular views over Queenstown and Lake Wakatipu.", "assets/images/ben-lomond.jpg", -45.009700000000002, 168.61670000000001, "Ben Lomond Track", 4.7m, "6-8 hours", 713 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BestTime", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Dec - Feb", "Enjoy a classic cruise across Lake Wakatipu aboard a historic steamship.", "assets/images/tss-earnslaw.jpg", -45.030999999999999, 168.66, "TSS Earnslaw Cruise", 4.7m, "1.5-2 hours", 980 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Dec - Feb", "A spectacular fiord surrounded by towering peaks, waterfalls and native rainforest.", 8, "assets/images/milford.jpg", -44.641399999999997, 167.9254, "Milford Sound", 4.9m, "4-6 hours", 1420 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Dec - Feb", "Step into the lush pastures of the Shire from The Lord of the Rings film trilogy.", 7, "assets/images/hobbiton.jpg", -37.872100000000003, 175.68260000000001, "Hobbiton Movie Set", 4.8m, "2-3 hours", 1250 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Nov - Mar", "Explore colourful geothermal pools, volcanic landscapes and geothermal activity.", 2, "assets/images/waiotapu.jpg", -38.357399999999998, 176.36680000000001, "Wai-O-Tapu Thermal Wonderland", 4.7m, "2-3 hours", 1100 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Sep - Apr", "New Zealand's highest mountain surrounded by spectacular alpine landscapes and glaciers.", 6, "assets/images/mountcook.jpg", -43.734400000000001, 170.14109999999999, "Aoraki / Mount Cook", 4.9m, "1-2 days", 1500 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Dec - Mar", "A stunning coastal national park known for golden beaches, clear water and walking trails.", 6, "assets/images/abel-tasman.jpg", -40.900599999999997, 173.07689999999999, "Abel Tasman National Park", 4.8m, "1-2 days", 900 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Lakeside gardens and an easy walking loop near central Queenstown.", "assets/images/queenstown-gardens.jpg", -45.031999999999996, 168.6694, "Queenstown Gardens", 4.7m, "2 hours", 850 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "High-speed jet boat ride through the Shotover River canyons.", "assets/images/shotover-jet.jpg", -44.997300000000003, 168.7072, "Shotover Jet", 4.8m, "2 hours", 1600 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Native wildlife conservation park near town centre.", "assets/images/kiwi-park.jpg", -45.028799999999997, 168.6585, "Kiwi Park Queenstown", 4.6m, "2 hours", 700 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Historic gold-mining village with heritage streets and riverside walks.", "assets/images/arrowtown.jpg", -44.939399999999999, 168.83099999999999, "Arrowtown Historic Precinct", 4.7m, "3 hours", 1100 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BestTime", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Year round", "Private hot pools overlooking the Shotover River canyon.", "assets/images/onsen.jpg", -45.0, 168.73500000000001, "Onsen Hot Pools", 4.7m, "2 hours", 900 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "ReviewCount" },
                values: new object[] { "Historic riverside gardens beside Hagley Park.", 6, "assets/images/christchurch-botanic-gardens.jpg", -43.5291, 172.62, "Christchurch Botanic Gardens", 4.7m, 1200 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Year round", "Gondola ride with views over Lyttelton Harbour and the Canterbury Plains.", 6, "assets/images/christchurch-gondola.jpg", -43.564300000000003, 172.7226, "Christchurch Gondola", 4.6m, "2 hours", 950 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BestTime", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Year round", "Interactive Antarctic visitor experience beside Christchurch Airport.", "assets/images/antarctic-centre.jpg", -43.489100000000001, 172.53120000000001, "International Antarctic Centre", 4.6m, "3 hours", 1000 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Latitude", "Longitude", "Name", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Museum telling the story of the Canterbury earthquakes and recovery.", "assets/images/quake-city.jpg", -43.536799999999999, 172.63759999999999, "Quake City", "2 hours", 600 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BestTime", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Sep - Apr", "Banks Peninsula harbour town, suitable as a full-day excursion from Christchurch.", "assets/images/akaroa.jpg", -43.804499999999997, 172.9676, "Akaroa Harbour", 4.8m, "6 hours", 1000 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "ReviewCount" },
                values: new object[] { "Observation tower with panoramic views across Auckland and the Hauraki Gulf.", 3, "assets/images/sky-tower.jpg", -36.848500000000001, 174.76329999999999, "Sky Tower", 1800 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "ReviewCount" },
                values: new object[] { "Museum of natural history and Aotearoa New Zealand stories in the Domain.", 3, "assets/images/auckland-museum.jpg", -36.860100000000003, 174.77879999999999, "Auckland Museum", 4.7m, 1400 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Oct - Apr", "Hauraki Gulf island for beaches, art and vineyard visits; allow a full day.", 3, "assets/images/waiheke.jpg", -36.7806, 175.00700000000001, "Waiheke Island", 4.8m, "8 hours", 1600 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "Rating", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Oct - Apr", "Volcanic island day trip with a summit walk and harbour views.", 3, "assets/images/rangitoto.jpg", -36.787999999999997, 174.86000000000001, "Rangitoto Island", 4.8m, "6 hours", 1200 });

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "BestTime", "Description", "DestinationId", "ImageUrl", "Latitude", "Longitude", "Name", "RecommendedDuration", "ReviewCount" },
                values: new object[] { "Year round", "Conservation-focused zoo in Western Springs.", 3, "assets/images/auckland-zoo.jpg", -36.863799999999998, 174.71809999999999, "Auckland Zoo", "3 hours", 1300 });
        }
    }
}
