using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ToDoAPI.Models;

namespace ToDoAPI
{
    public class AddPolicy
    {

        
        public int PolicyNumber { get; set; }
        public int PersonId { get; set; }
        public int ProductId { get; set; }
        public DateTime TanzimTarihi { get; set; } // CreatedDate (Kodu yazdığınız yerde burayı DateTime.Now olarak set edebilirsiniz.)
        public DateTime VadeBaslangic { get; set; } // CreatedDate (Kodu yazdığınız yerde burayı DateTime.Now olarak set edebilirsiniz.) 
        public DateTime VadeBitis { get; set; } // VadeBaslangic.AddYear(1)
        public double Prim { get; set; } // new Random().Next(1000, 10000);
        public string? CarModel { get; set; }
        public string? CarPlateNumber { get; set; }
        public string? CarBrand { get; set; }
        public string? PlakaIlKodu { get; set; }
        public string? PlakaKodu { get; set; }
        public string? SigortaNumarasi { get; set; }
        public string? HastaneAdi { get; set; }
        public string? Adress { get; set; }
        public string? Ilce { get; set; }
        public string? Il { get; set; }
        public string? Discriminator { get; set; }

    }
}
