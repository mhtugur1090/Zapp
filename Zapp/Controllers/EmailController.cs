using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Zapp.Models;
using System.Configuration;

namespace Zapp.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> MailSending(EmailModel model)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var body = "<p>Email From: {0} ({1})</p><p>Mesaj:</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["From"]));  // Alıcı 
                    message.From = new MailAddress(ConfigurationManager.AppSettings["To"]);  // Gonderici
                    message.Subject = model.FromMessage;
                    message.Body = string.Format(body, model.FromName, model.FromMail, model.FromMessage);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {



                        var credential = new NetworkCredential
                        {
                            
                            UserName = ConfigurationManager.AppSettings["From"],/*"securmail.901@gmail.com"*/  // replace with valid value
                            Password = ConfigurationManager.AppSettings["Password"]/*"123456a" */ // replace with valid value
                        };
                        smtp.Credentials = credential;
                        smtp.Host = ConfigurationManager.AppSettings["Host"]/*"smtp.gmail.com"*/;
                        smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["Port"])/*587*/;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                        return View();

                    }
                }
                else { return View("Error"); }
            }
            catch (Exception)
            {
               
                return View("Error");
            }
            

            
            return View();
        }


        public ActionResult Error()
        {


            return View();

        }

    }
}