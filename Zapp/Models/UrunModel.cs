using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zapp.Entity;

namespace Zapp.Models
{
    public class UrunModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string _Image { get; set; }
        public int KategoriId { get; set; }//foreign key
        public Kategori kategori { get; set; }
    }
}