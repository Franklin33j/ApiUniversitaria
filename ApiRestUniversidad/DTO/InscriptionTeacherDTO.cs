using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestUniversidad.DTO
{
    public class InscriptionTeacherDTO
    {
        [Required(ErrorMessage ="El campo es obligatorio")]
        public int IdRecord { get; set; }
        public int IdSubject { get; set; }
        public int IdTeacher { get; set; }
    }
}
