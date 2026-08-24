namespace WanderKiwi.Application.DTOs;

public class DestinationPageDto
{
    public List<DestinationDto> PopularDestinations { get; set; } = new();

    public List<RegionDto> Regions { get; set; } = new();

    public List<AttractionDto> FeaturedAttractions { get; set; } = new();
}