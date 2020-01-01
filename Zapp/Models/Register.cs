using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Newtonsoft.Json;

namespace Zapp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Adınız gereklidir.")]
        [DisplayName("Adınız")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadınız gereklidir.")]
        [DisplayName("Soyadınız")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı gereklidir.")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]

        
        [DisplayName("Eposta Adresiniz")]
        [EmailAddress(ErrorMessage = "Eposta adresinizi düzgün giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Alanı gereklidir.")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        
        [Required(ErrorMessage = "Şifre Alanı gereklidir.")]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password",ErrorMessage = "Şifreler Uyuşmuyor.")]
        public string RePassword { get; set; }

    }
}