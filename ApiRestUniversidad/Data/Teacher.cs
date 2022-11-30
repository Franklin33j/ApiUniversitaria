using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("teachers")]
    public partial class Teacher
    {
        public Teacher()
        {
            Recordinscriptionteachers = new HashSet<Recordinscriptionteacher>();
        }
        
        [Key]
        [Column(TypeName = "int(255)")]
        public int IdTeacher { get; set; }
        [Column("firstNames")]
        [StringLength(255)]
        public string FirstNames { get; set; } = null!;
        [Column("lastNames")]
        [StringLength(255)]
        public string LastNames { get; set; } = null!;

        [InverseProperty("IdTeacherNavigation")]
        public virtual ICollection<Recordinscriptionteacher> Recordinscriptionteachers { get; set; }
    }
}
