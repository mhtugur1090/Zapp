using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zapp.Entity
{
    public class Urun
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ürün ismi boş bırakılamaz."),StringLength(20,ErrorMessage ="Maksimum 20 karakter.")]
        [DisplayName("İsim")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        public string _Image { get; set; }

        [Required(ErrorMessage ="Kategorisini seçiniz.")]
        [DisplayName("Kategori")]
        public int KategoriId { get; set; }//foreign key
        public Kategori kategori { get; set; }
    }
}