using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Data
{
    [Table("ratings")]
    [Index("IdStudent", Name = "Ratings_fk0")]
    [Index("IdSubject", Name = "Ratings_fk1")]
    public partial class Rating
    {
        public Rating()
        {
            
        }
        [JsonIgnore]
        [Key]
        [Column(TypeName = "int(11)")]
        public int IdRating { get; set; }
        [Column("note1")]
        [Precision(10, 0)]
        public decimal Note1 { get; set; }
        [Column("note2")]
        [Precision(10, 0)]
        public decimal Note2 { get; set; }
        [Column("note3")]
        [Precision(10, 0)]
        public decimal Note3 { get; set; }
        [Column("note4")]
        [Precision(10, 0)]
        public decimal Note4 { get; set; }
        [Column("idStudent", TypeName = "int(11)")]
        public int IdStudent { get; set; }
        [Column("idSubject", TypeName = "int(11)")]
        public int IdSubject { get; set; }
        [JsonIgnore]
        [ForeignKey("IdStudent")]
        [InverseProperty("Ratings")]
        public virtual Student IdStudentNavigation { get; set; } = null!;
        [ForeignKey("IdSubject")]
        [InverseProperty("Ratings")]
        [JsonIgnore]
        public virtual Subject IdSubjectNavigation { get; set; } = null!;
        
        
    }
}
