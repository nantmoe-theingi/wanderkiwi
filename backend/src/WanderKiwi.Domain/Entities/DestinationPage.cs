using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Models;

public class DestinationPageData
{
    public IEnumerable<Destination> PopularDestinations { get; set; }
        = new List<Destination>();

    public IEnumerable<Region> Regions { get; set; }
        = new List<Region>();

    public IEnumerable<Attraction> FeaturedAttractions { get; set; }
        = new List<Attraction>();
}