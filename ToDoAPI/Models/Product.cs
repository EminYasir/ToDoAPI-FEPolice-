using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
