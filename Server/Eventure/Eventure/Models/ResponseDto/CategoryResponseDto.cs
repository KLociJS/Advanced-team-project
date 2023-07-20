using Eventure.Models.Entities;

namespace Eventure.Models.ResponseDto;

public class CategoryResponseDto
{
    public List<Category> Categories { get; set; } = new List<Category>();
}