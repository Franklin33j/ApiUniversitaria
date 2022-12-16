namespace ApiRestUniversidad.ViewModels
{
    public class RatingVM
    {
        public int Id { get; set; }
        public decimal Note1 { get; set; } 
        public decimal Note2 { get; set; } 
        public decimal Note3 { get; set; } 
        public decimal Note4 { get; set; } 
        public string NameStudent { get; set; } = "";
        public string Subject { get; set; } = "";
    }
}
