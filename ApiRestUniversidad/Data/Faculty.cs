using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("faculties")]
    [Index("NameFaculty", Name = "nameFaculty", IsUnique = true)]
    public partial class Faculty
    {
        public Faculty()
        {
            Careers = new HashSet<Career>();
        }

        [Key]
        [Column("idFaculty", TypeName = "int(11)")]
        public int IdFaculty { get; set; }
        [Column("nameFaculty")]
        public string NameFaculty { get; set; } = null!;

        [InverseProperty("IdFacultyNavigation")]
        [JsonIgnore]
        public virtual ICollection<Career> Careers { get; set; }
    }
}
