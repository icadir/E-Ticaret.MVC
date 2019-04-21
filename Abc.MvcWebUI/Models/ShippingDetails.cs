using System.ComponentModel.DataAnnotations;

namespace Abc.MvcWebUI.Models
{
    public class ShippingDetails
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Adres tanımını Giriniz")]
        public string AdresBasligi { get; set; }

        [Required(ErrorMessage = "Lütfen bir adres Giriniz")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen şehir Giriniz")]
        public string Sehir { get; set; }
        [Required(ErrorMessage = "Lütfen Semt Giriniz")]
        public string Semt { get; set; }
        [Required(ErrorMessage = "Lütfen Mahalle Giriniz")]
        public string Mahalle { get; set; }
        [Required(ErrorMessage = "Lütfen PostaKodu Giriniz")]
        public string PostaKodu { get; set; }

    }
}