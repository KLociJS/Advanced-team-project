using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Eventure.Models.Entities;

public class Location
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
    public List<Event> Events { get; set; } = new List<Event>();
    
    public static List<Location> LoadLocationsFromCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var locations = csv.GetRecords<Location>().ToList();
        return locations;
    }
}