using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Health: Policys
    {
        
        public string? SigortaNumarasi { get; set; }
        public string? HastaneAdi { get; set; }
    }
}
