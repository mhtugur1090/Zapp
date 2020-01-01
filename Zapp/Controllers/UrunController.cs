using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Zapp.Entity;

namespace Zapp.Controllers
{
    public class UrunController : Controller
    {

        private DataContext db = new DataContext();

        // GET: Urun
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {

            var urunler = db.Urunler.SortBy("KategoriId").ToList();

            return View(urunler);
        }

        //GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urunler.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }
        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urunler.Find(id);
            db.Urunler.Remove(urun);
            db.SaveChanges();
            return Redirect("/Account/Index");
        }

        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urunler.Find(id);
            if (urun == null)
            {
                return HttpNotFound();

            }
            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,_Image,KategoriId")] Urun urun, HttpPostedFileBase file)
        {

            if (file != null)
            {
                //Dosya ismini aldık.
                var filename = Path.GetFileName(file.FileName);
                urun._Image = filename;
                var path = Path.Combine(Server.MapPath("~/Image"), filename);
                file.SaveAs(path);
            }

            if (ModelState.IsValid)
            {
                db.Entry(urun).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Account/Index");
            }
            return View(urun);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,KategoriId,_Image")] Urun urun,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (/*file.ContentLength>0 &&*/ file!=null)
                {
                    //Dosya ismini aldık.
                    var filename = Path.GetFileName(file.FileName);
                    urun._Image = filename;
                    var path = Path.Combine(Server.MapPath("~/Image"),filename);
                    file.SaveAs(path);
                }

                db.Urunler.Add(urun);
                db.SaveChanges();
                return Redirect("/Account/Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "Name");
            return View(urun);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var urun = db.Urunler.Find(id);

            if (urun==null)
            {
                return HttpNotFound();
            }

            return View(urun);
        }

    }
}