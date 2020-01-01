using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Zapp.Controllers
{
    public class SlaytController : Controller
    {
        

        // GET: Slayt
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IEnumerable<string> images = Directory.EnumerateFiles(Server.MapPath("~/Image/Slayt/"))
                .Select(x=> "~/Image/Slayt/" + Path.GetFileName(x));
            var _objFile= new DirectoryInfo(Server.MapPath("~/Image/Slayt/"));
            var _file = _objFile.GetFiles("*.*");

            return View(images);
        }


        [HttpPost]
        public  ActionResult SaveImageToFolder(HttpPostedFileBase file) 
        {

            if (file!=null)
            {

                //Dosya ismini aldık.
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Image/Slayt"), filename);
                file.SaveAs(path);
                return RedirectToAction("Index", "Slayt");
            }
            else
            {
                return HttpNotFound();
            }

        
        }
        [HttpPost]
        public void DeleteImageFromFolder(string path)
        {
            string fullPath = Request.MapPath("~" + path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                
            }
            
        }


    }
}