using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiRestUniversidad.DTO
{
    public class RatingDTO
    {
        
        public int IdRating { get; set; }
        
        public decimal Note1 { get; set; }
        
        public decimal Note2 { get; set; }
        
        public decimal Note3 { get; set; }
        
        public decimal Note4 { get; set; }
        
        public int IdStudent { get; set; }
        
        public int IdSubject { get; set; }

        
    }
}
