using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zapp.Entity;
using Zapp.Models;

namespace Zapp.Controllers
{
    public class HomeController : Controller
    {
        DataContext context = new DataContext();

        // GET: Home
        public ActionResult Index()
        {
            var urunler = context.Urunler.Select(x => new UrunModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                _Image = x._Image
            }).AsQueryable();

            return View(urunler.ToList());
            //return RedirectToAction("OutOfOrder");
        }
        
        

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }




        public ActionResult OutOfOrder()
        {

            return View();
        }

    }
}