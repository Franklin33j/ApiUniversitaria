using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("recordinscriptionteachers")]
    [Index("IdSubject", Name = "recordInscriptionTeachers_fk0")]
    [Index("IdTeacher", Name = "recordInscriptionTeachers_fk1")]
    public partial class Recordinscriptionteacher
    {
        [JsonIgnore]
        [Key]
        [Column("idRecord", TypeName = "int(11)")]
        public int IdRecord { get; set; }
        [Column("idSubject", TypeName = "int(11)")]
        public int IdSubject { get; set; }
        [Column("idTeacher", TypeName = "int(11)")]
        public int IdTeacher { get; set; }
        [Column("date", TypeName = "datetime")]
        [JsonIgnore]
        public DateTime Date { get; set; } = DateTime.Now;
        [JsonIgnore]
        [ForeignKey("IdSubject")]
        [InverseProperty("Recordinscriptionteachers")]

        public virtual Subject? IdSubjectNavigation { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("IdTeacher")]
        [InverseProperty("Recordinscriptionteachers")]
        public virtual Teacher? IdTeacherNavigation { get; set; } = null!;
    }
}
