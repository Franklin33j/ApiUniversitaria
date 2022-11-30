using System.ComponentModel.DataAnnotations;

namespace ApiRestUniversidad.DTO
{
    public class InscriptionStudentDTO
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int IdRecord { get; set; }
        public int IdSubject { get; set; }
        public int IdStudent { get; set; }
    }
}
