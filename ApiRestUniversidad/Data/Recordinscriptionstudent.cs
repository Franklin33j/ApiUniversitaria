using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("recordinscriptionstudents")]
    [Index("IdStudent", Name = "recordInscriptionStudents_fk0")]
    [Index("IdSubject", Name = "recordInscriptionStudents_fk1")]
    public partial class Recordinscriptionstudent
    {
        [JsonIgnore]
        [Key]
        [Column("idRecord", TypeName = "int(11)")]
        public int IdRecord { get; set; }
        [Column("idStudent", TypeName = "int(11)")]
        public int IdStudent { get; set; }
        [Column("idSubject", TypeName = "int(11)")]
        public int IdSubject { get; set; }
        [Column("date", TypeName = "datetime")]
        [JsonIgnore]
        public DateTime Date { get; set; }= DateTime.Now;
        [JsonIgnore]
        [ForeignKey("IdStudent")]
        [InverseProperty("Recordinscriptionstudents")]
        public virtual Student? IdStudentNavigation { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("IdSubject")]
        [InverseProperty("Recordinscriptionstudents")]
        public virtual Subject? IdSubjectNavigation { get; set; } = null!;
    }
}
