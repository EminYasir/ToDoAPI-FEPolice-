using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string? KimlikNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string? Adi { get; set; }
        public string? Soyadi { get; set; }
        public string? Password { get; set; }
        //public List<Casco>? Cascos { get; set; }
        //public List<Health>? Healths { get; set; }
        //public List<Traffic>? Traffics { get; set; }
        //public List<Dask>? Dasks { get; set; }
    }
}
