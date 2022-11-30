using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("students")]
    [Index("IdCareer", Name = "Students_fk0")]
    public partial class Student
    {
        public Student()
        {
            Ratings = new HashSet<Rating>();
            Recordinscriptionstudents = new HashSet<Recordinscriptionstudent>();
        }
        [JsonIgnore]
        [Key]
        [Column("idStudent", TypeName = "int(11)")]
        public int IdStudent { get; set; }
        [Column("firstNames")]
        [StringLength(255)]
        public string FirstNames { get; set; } = null!;
        [Column("lastNames")]
        [StringLength(255)]
        public string LastNames { get; set; } = null!;
        [Column("idCareer", TypeName = "int(11)")]
        public int? IdCareer { get; set; }
        [Column("cum")]
        [Precision(10, 0)]
        public decimal Cum { get; set; }
        [JsonIgnore]
        [ForeignKey("IdCareer")]
        [InverseProperty("Students")]
        public virtual Career? IdCareerNavigation { get; set; }
        [InverseProperty("IdStudentNavigation")]
        [JsonIgnore]
        public virtual ICollection<Rating> Ratings { get; set; }
        [InverseProperty("IdStudentNavigation")]
        [JsonIgnore]
        public virtual ICollection<Recordinscriptionstudent> Recordinscriptionstudents { get; set; }
    }
}
