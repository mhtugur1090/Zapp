using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Zapp.Entity;
using Zapp.Identity;
using Zapp.Models;


namespace Zapp.Controllers
{
    
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore =new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager =new UserManager<ApplicationUser>(userStore);

            var roleStore =new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            //var username = User.Identity.Name; //sistemdeki o anki kullanıcı

            //var urunler = db.Urunler.SortBy("KategoriId").ToList();
            
            return View();

        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            
            if (ModelState.IsValid)//Gelen modelin kontrolünü yapalım
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.Email = model.Email;
                user.UserName = model.UserName;

                IdentityResult result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    // Kayıt oluştu ve kullanıcıyı bir role atayabiliriz.

                    if (RoleManager.RoleExists("user")) // Atanacak rol var mı onun kontrolü yapılır.
                    {
                        UserManager.AddToRole(user.Id, "user");

                    }
                    else
                    {
                        ModelState.AddModelError("RegisterUserError","Kullanıcı Oluşturma Hatası");
    
                    }

                    return RedirectToAction("Login", "Account");
                }


            }

            return View(model);
        }




        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Login model/*,string ReturnUrl*/) // ReturnUrl [Authorize] (Controller için) login olduktan sonra otomatik istenen sayfaya yönlendirmek için kullanılır.
        {
            
            //-------DENEME------------------------------
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Email/Error");// Önceden oturum açıldıysa yapılacak işlemler.
            }
            //-----------------------------------


            if (ModelState.IsValid)//Gelen modelin kontrolünü yapalım
            {
               // Login işlemleri
            var user = UserManager.Find(model.UserName, model.Password);

            if (user != null)
            {
                // varolan kullanıcıyı sisteme dahil etcez.
                //Application cookie oluşturup sisteme bırak.
                var authManager = HttpContext.GetOwinContext().Authentication;
                // Oluşturulan user cookie içine atıyoruz.
                var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                // Cookie nin kalıcı olup olmadığını belirlemek için;
                var IsPersist = new AuthenticationProperties();
                    IsPersist.IsPersistent = model.RememberMe;

                authManager.SignIn(IsPersist,identityclaims);
                // Kullanıcı sisteme dahil edildi.

                //if (!String.IsNullOrEmpty(ReturnUrl))
                //{
                //   return Redirect(ReturnUrl); // Redirect ile "ReturnUrl" kullanılır(Login olmadan önceki sayfa)
                //}

               // return RedirectToAction("Index", "Account");
                  return Redirect("/Account/Index");
            }
            else
            {
                ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı bulunamadı.");
                }
                

            }

            return View(model);
        }


        // GET: Account 
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index","Home");
        }

        //GET: Change Password
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ChangePass()
        {

            return View();
        }

        //POST: Change Password
        [HttpPost]
        public async Task<ActionResult> ChangePass(string pOld , string pNew)
        {

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(),pOld,pNew);

            if (result.Succeeded)
            {
                TempData["result"] = true;
                return RedirectToAction("resultPage", "Account");
            }
            else
            {
                TempData["result"] = false;
                return RedirectToAction("resultPage", "Account");
            }
            
        }

        
        public ActionResult resultPage()
        {
            if (TempData["result"]!=null)
            {
                bool result = (bool)TempData["result"];
                return View(result);
            }
            if (TempData["result"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Geçersiz Yineleme");
            }

            return View(false);
        }


        //GET: Change Mail Address
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ChangeMail()
        {
            
            return View(new EmailModel());
        }

        //POST: Change Mail Address
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<string> ChangeMail(string FromMail)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user.Email!= FromMail)
            {
                user.Email = FromMail;
                IdentityResult result = await UserManager.UpdateAsync(user);
            }

            return user.Email.ToString()+"<==";
        }

        public ActionResult ChangeAboutText() 
        {


            return null;

        }

    }
}