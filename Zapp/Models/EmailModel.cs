using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zapp.Models
{
    public class EmailModel
    {
        [Required(ErrorMessage ="İsim alanını giriniz."),Display(Name ="İsminiz"),StringLength(20,ErrorMessage ="En fazla 20 karakter girebilirsiniz.")]
        public string FromName { get; set; }
        [Required(ErrorMessage ="Geçerli bir eposta adresi giriniz."),Display(Name ="Mail Adresiniz"),EmailAddress(ErrorMessage ="Geçerli bir eposta adresi giriniz.")]
        public string FromMail { get; set; }
        [Required(ErrorMessage ="Boş mesaj gönderemezsiniz."),Display(Name ="Mesajınız")]
        public string FromMessage { get; set; }
    }
}