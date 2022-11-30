using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("careers")]
    [Index("IdFaculty", Name = "Careers_fk0")]
    public partial class Career
    {
        public Career()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [Column("idCareer", TypeName = "int(11)")]
        public int IdCareer { get; set; }
        [Column("nameCareer")]
        [StringLength(255)]
        public string NameCareer { get; set; } = null!;
        [Column("idFaculty", TypeName = "int(11)")]
        public int IdFaculty { get; set; }

        [ForeignKey("IdFaculty")]
        [InverseProperty("Careers")]
        [JsonIgnore]
        public virtual Faculty IdFacultyNavigation { get; set; } = null!;
        [InverseProperty("IdCareerNavigation")]
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }
}
