using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Casco: Policys
    {
        
        public string? CarPlateNumber { get; set; }
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
    }
}
