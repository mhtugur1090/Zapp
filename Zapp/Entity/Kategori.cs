using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zapp.Entity
{
    public class Kategori
    {
        public int Id { get; set; }
        [Required,DisplayName("Kategori ismi"),StringLength(20,ErrorMessage ="Maksimum 20 karakter")]
        public string Name { get; set; }
        
    }
}