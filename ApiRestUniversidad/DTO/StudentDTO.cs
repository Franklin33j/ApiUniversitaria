using System.Text.Json.Serialization;

namespace ApiRestUniversidad.DTO
{
    public class StudentDTO
    {
        [JsonIgnore]
        public int IdStudent { get; set; }
        public string FirstNames { get; set; } = null!;
        
        public string LastNames { get; set; } = null!;
        
        public int? IdCareer { get; set; }
        
    }
}
