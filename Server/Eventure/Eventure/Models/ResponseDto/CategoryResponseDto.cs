using Eventure.Models.Entities;

namespace Eventure.Models.ResponseDto;

public class CategoryResponseDto
{
    public List<Category> Data { get; set; } = new List<Category>();
}