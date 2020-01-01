using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zapp.Entity
{
    public class DataContext:DbContext
    {

        public DataContext():base("DataConnection")
        {
            
        }

        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        

    }
}