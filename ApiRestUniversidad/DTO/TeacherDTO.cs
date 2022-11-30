using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiRestUniversidad.DTO
{
    public class TeacherDTO
    {
        [StringLength(255)]
        public string FirstNames { get; set; } = "";
        [StringLength(255)]
        public string LastNames { get; set; } ="";
    }
}
