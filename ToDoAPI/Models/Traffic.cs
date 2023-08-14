using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Traffic: Policys
    {
        
        public string? PlakaIlKodu { get; set; }
        public string? PlakaKodu { get; set; }
    }
}
