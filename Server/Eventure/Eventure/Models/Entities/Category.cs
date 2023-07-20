using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CsvHelper;
using Eventure.Models.Enums;

namespace Eventure.Models.Entities;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public List<Event> Events = new List<Event>();
    
    public static List<Category> LoadCategoriesFromCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var categories = csv.GetRecords<Category>().ToList();
        return categories;
    }
    
}