using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emlakkkk.Models;
using emlakkkk.Models.Giris;

namespace Demtus.Controllers
{
    public class GirisController : Controller
    {
        // GET: Admin
        [HttpGet, Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, Route("login")]  
        public ActionResult Login(users gelen)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                if (new LoginState().IsLoginSucces(gelen.mail, gelen.password))
                {
                    HttpContext.Session.Add("Kullanici", gelen.mail.ToString());
                    return RedirectToAction("Index", "User");
                }
                TempData["messageRed1"] = "E Posta veya Şifre yanlış!";
                return RedirectToAction("Login", "Giris");
            }
            catch (Exception)
            {
                TempData["messageRed1"] = "Bir Hata Oluştu!";
                return RedirectToAction("Login", "Giris");
            }
        }

        [HttpGet, Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, Route("register")]
        public ActionResult Register(users gelen)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                if (gelen != null)
                {
                    var vSorgu = db.users.Where(w => w.mail.Trim().ToString().Equals(gelen.mail)).FirstOrDefault();
                    if (vSorgu == null)
                    {
                        gelen.type = "Bireysel";
                        db.users.Add(gelen);
                        db.SaveChanges();
                        TempData["messageOnay"] = "Kayıt başvurunuz başarıyla alınmıştır onay süreci minimum 1 iş günü maximum 3 iş günü içerisinde tamamlanacaktır!";
                        return RedirectPermanent("/login");
                    }
                    else
                    {
                        TempData["messageRed"] = "Kayıt başvurunuz reddedildi! (Bu Mail Adresi Kullanılıyor)";
                        return RedirectPermanent("/register");
                        //return RedirectToAction("Giris#signup", "Giris");
                    }
                }
                return RedirectToAction("Register", "User");
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "User");
            }
        }

        [HttpGet, Route("admin")]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost, Route("admin")]
        public ActionResult LoginAdmin(users gelen)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                if (new LoginStateAdmin().IsLoginSuccesAdmin(gelen.mail, gelen.password))
                {
                    HttpContext.Session.Add("Kullanici", gelen.mail.ToString());
                    return RedirectToAction("Ilanlar", "Admin");
                }
                TempData["messageRed1"] = "E Posta veya Şifre yanlış!";
                return RedirectToAction("LoginAdmin", "Giris");
            }
            catch (Exception)
            {
                TempData["messageRed1"] = "Bir Hata Oluştu!";
                return RedirectToAction("Login", "Giris");
            }
        }

        public ActionResult Cikis()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}