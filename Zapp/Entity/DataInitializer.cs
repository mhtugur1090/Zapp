using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zapp.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Kategori>()
            {
                new Kategori(){ Name="Kereste" },
                new Kategori(){ Name="Diger" },
                
            };

            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }

            context.SaveChanges();

            //var Resimler = new List<Resim>()
            //{
            //    new Resim(){Name="in1.png"},
            //    new Resim(){Name="in2.png"},
            //    new Resim(){Name="out1.png"},
            //    new Resim(){Name="images.jpg"}

            //};

            //foreach (var item in Resimler)
            //{
            //    context.Resimler.Add(item);
            //}

            //context.SaveChanges();

            var Urunler = new List<Urun>()
            {
                new Urun(){Name="Kayın Kereste",Description="Kemankemankeman" ,KategoriId=1,_Image="ceviz.jpg"  },
                new Urun(){Name="İnşaatlık Kereste",Description="GitarGitarGitar" ,KategoriId=1,_Image="mese.jpg" },
                new Urun(){Name="Ladin Kereste",Description="PianoPianoPiano" ,KategoriId=1,_Image="kayin.jpg" },
                new Urun(){Name="Çam Kereste",Description="BağlamaBağlamaBağlama" ,KategoriId=1,_Image="ceviz.jpg" }
            };

            foreach (var item in Urunler)
            {
                context.Urunler.Add(item);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}