using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("subjects")]
    public partial class Subject
    {
        public Subject()
        {
            Ratings = new HashSet<Rating>();
            Recordinscriptionstudents = new HashSet<Recordinscriptionstudent>();
            Recordinscriptionteachers = new HashSet<Recordinscriptionteacher>();
        }

        [Key]
        [Column("idSubject", TypeName = "int(11)")]
        public int IdSubject { get; set; }
        [Column("nameSubject")]
        [StringLength(255)]
        public string NameSubject { get; set; } = null!;
        [JsonIgnore]
        [InverseProperty("IdSubjectNavigation")]
        public virtual ICollection<Rating> Ratings { get; set; }
        [InverseProperty("IdSubjectNavigation")]
        [JsonIgnore]
        public virtual ICollection<Recordinscriptionstudent> Recordinscriptionstudents { get; set; }
        [InverseProperty("IdSubjectNavigation")]
        [JsonIgnore]
        public virtual ICollection<Recordinscriptionteacher> Recordinscriptionteachers { get; set; }
    }
}
