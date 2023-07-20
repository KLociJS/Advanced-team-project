using Eventure.Models.Entities;

namespace Eventure.Models.ResponseDto;

public class LocationResponseDto
{
    public List<Location> Locations { get; set; } = new List<Location>();
    
}