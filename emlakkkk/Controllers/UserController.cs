using emlakkkk.Models.Giris;
using emlakkkk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace emlakkkk.Controllers
{
    [ControlLogin]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var vSorgu = Process.userProducts().Count();
            var vSorgu2 = Process.userFavorites().Count();
            TempData["ilanSayisi"] = vSorgu;
            TempData["favoriSayisi"] = vSorgu2;
            return View();
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Advertise()
        {
            var vSorgu = Process.allCategories();
            var vSorgu2 = Process.allProvincesForAdvertise();
            var vSorgu3 = Process.allCountiesForAdvertise();
            return View(Tuple.Create(vSorgu, vSorgu2, vSorgu3));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Advertise(products yeni,details detaylar,features ozellikler, images ilangorsel, HttpPostedFileBase[] Gorsel, HttpPostedFileBase[] Ekler)
        {
            try
            {
                
                emlksisEntities db = new emlksisEntities();
                int sessiondakiDeğer = Convert.ToInt32(Session["userid"]);
                yeni.userId = sessiondakiDeğer;
                string yeniLatLong = yeni.latLong.Replace(" ", "");
                string yeniLatLong2 = yeniLatLong.Replace("(", "");
                string yeniLatLong3 = yeniLatLong2.Replace(")", "");
                yeni.latLong = yeniLatLong3;
                yeni.releaseDate = DateTime.Now;
                db.products.Add(yeni);
                if (ModelState.IsValid)
                {   //iterating through multiple file collection   
                    foreach (HttpPostedFileBase file in Gorsel)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            string yeniIsım = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/uploads/images/") + yeniIsım);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //Save file to db  
                            ilangorsel.imageSource = yeniIsım;
                            ilangorsel.productId = yeni.productId;
                            db.images.Add(ilangorsel);
                            db.SaveChanges();
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = Gorsel.Count().ToString() + " files uploaded successfully.";
                        }

                    }
                }

                var vUserType = db.users.Where(w => w.userId == sessiondakiDeğer).Select(e => e.type).FirstOrDefault();
                detaylar.productId = yeni.productId;
                detaylar.fromWho = vUserType.ToString();
                if (ModelState.IsValid)
                {   //iterating through multiple file collection   
                    foreach (HttpPostedFileBase file in Ekler)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            string yeniIsım = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/uploads/attachments/") + yeniIsım);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //Save file to db  
                            detaylar.attachments = yeniIsım;
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = Ekler.Count().ToString() + " files uploaded successfully.";
                        }

                    }
                }
                db.details.Add(detaylar);
                ozellikler.productId = yeni.productId;
                db.features.Add(ozellikler);
                db.SaveChanges();
                TempData["DonenMesaj"] = "Başarılı! ilan incelemede.";
                return RedirectToAction("MyAds");
            }
            catch (Exception)
            {
                TempData["DonenHataMesaji"] = "Bir hata oluştu. Daha sonra tekrar deneyin!";
                return RedirectToAction("Advertise");
            }
        }

        public ActionResult Messages()
        {
            emlksisEntities db = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(Session["userid"]);
            var v = db.messages.Where(w => w.toUser == sessiondakiDeğer).ToList();
            return View(Tuple.Create(v));
        }

        [HttpGet]
        public ActionResult MessageDetail(int? id)
        {
            emlksisEntities db = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(Session["userid"]);
            var v = db.messages.Where(w => w.fromUser == id && w.toUser == sessiondakiDeğer || w.fromUser == sessiondakiDeğer && w.toUser == id).ToList();
            var v3 = db.messages.Where(w => w.fromUser == sessiondakiDeğer && w.toUser == id).ToList();

            var v2 = db.users.Where(w => w.userId == id).FirstOrDefault();
            TempData["mesajGondereninAdi"] = v2.name + " " + v2.surname;
            TempData["mesajGondereninAvatari"] = v2.avatarSource;
            TempData["mesajalaninId"] = Convert.ToInt32(id);
            return View(Tuple.Create(v, v3));
        }

        [HttpPost]
        public ActionResult MessageDetail(messages yaz, int? id,int? kime)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                int sessiondakiDeğer = Convert.ToInt32(Session["userid"]);
                var v = db.messages.Where(w => w.fromUser == id && w.toUser == sessiondakiDeğer || w.fromUser == sessiondakiDeğer && w.toUser == id).ToList();
                var v3 = db.messages.Where(w => w.fromUser == sessiondakiDeğer && w.toUser == id).ToList();

                yaz.fromUser = sessiondakiDeğer;
                if (id != null)
                {
                    yaz.toUser = id;
                    yaz.date = DateTime.Now;
                    db.messages.Add(yaz);
                    db.SaveChanges();
                    return RedirectToAction("MessageDetail");
                }
                else
                {
                    id = kime;
                    yaz.toUser = id;
                    yaz.date = DateTime.Now;
                    db.messages.Add(yaz);
                    db.SaveChanges();
                    return RedirectToAction("MessageDetail/"+id);
                }
            }
            catch (Exception)
            {

                emlksisEntities db = new emlksisEntities();
                int sessiondakiDeğer = Convert.ToInt32(Session["userid"]);
                var v = db.messages.Where(w => w.fromUser == id && w.toUser == sessiondakiDeğer || w.fromUser == sessiondakiDeğer && w.toUser == id).ToList();
                var v3 = db.messages.Where(w => w.fromUser == sessiondakiDeğer && w.toUser == id).ToList();
                return RedirectToAction("MessageDetail");
            }
        }

        public ActionResult MyAds()
        {
            var vSorgu = Process.allImages();
            var vSorgu2 = Process.userProducts();
            return View(Tuple.Create(vSorgu, vSorgu2));
        }

        public ActionResult MyFavorites()
        {
            var vSorgu = Process.userFavorites();
            var vSorgu2 = Process.allImages();
            return View(Tuple.Create(vSorgu, vSorgu2));
        }

        public ActionResult Membership()
        {
            emlksisEntities db = new emlksisEntities();
            int sessiondakiDeger = Convert.ToInt32(Session["userid"]);
            var vSorgu = db.users.Where(x => x.userId == sessiondakiDeger).FirstOrDefault();
            if (sessiondakiDeger != null)
            {
                try
                {
                    ViewBag.avatarr = vSorgu.avatarSource;
                    ViewBag.ad = vSorgu.name;
                    ViewBag.soyad = vSorgu.surname;
                    ViewBag.mail = vSorgu.mail;
                    ViewBag.telefon = vSorgu.phone;
                    ViewBag.hakkimda = vSorgu.about;
                    ViewBag.facebook = vSorgu.socialFacebook;
                    ViewBag.instagram = vSorgu.socialInstagram;
                    ViewBag.linkedin = vSorgu.socialLinkedin;
                    ViewBag.pinterest = vSorgu.socialPinterest;
                    ViewBag.skype = vSorgu.socialSkype;
                    ViewBag.twitter = vSorgu.socialTwitter;
                    ViewBag.website = vSorgu.socialWebsite;
                    ViewBag.youtube = vSorgu.socialYoutube;
                }
                catch (Exception)
                {
                }
            }
            return View();
        }

        public ActionResult FavEkle(favorites yeni1, int? id)
        {
            int sessiondakiDeğer = Convert.ToInt32(Session["userid"]);

            if (sessiondakiDeğer != null && sessiondakiDeğer != 0)
            {
                Process.addFavorite(yeni1, id, sessiondakiDeğer);
                TempData["DonenMesaj"] = "Başarılı! ilan favorilerine eklendi.";
                return RedirectToAction("MyFavorites");
            }
            else
            {
                return RedirectToAction("MyFavorites");
            }
            
        }

        public ActionResult BilgiDegistir(users gelen, HttpPostedFileBase[] avatar)
        {
            if (gelen != null)
            {
                if (ModelState.IsValid)
                {   //iterating through multiple file collection   
                    foreach (HttpPostedFileBase file in avatar)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            string yeniIsım = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/uploads/avatar/") + yeniIsım);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //Save file to db  
                            gelen.avatarSource = yeniIsım;
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = avatar.Count().ToString() + " files uploaded successfully.";
                        }

                    }
                }
                var geriBildirim = Process.BilgiDegistir(gelen);
                TempData["message"] = geriBildirim.ToString();
            }
            return RedirectToAction("MemberShip");
        }

        public ActionResult SocialDegistir(users gelen)
        {
            if (gelen != null)
            {
                var geriBildirim = Process.SosyalDegistir(gelen);
                TempData["message"] = geriBildirim.ToString();
            }
            return RedirectToAction("MemberShip");
        }

        public ActionResult SifreDegistir(users gelen, string NewUserPassword, string NewPasswordConfirm)
        {
            if (gelen != null)
            {
                var geriBildirim = Process.SifreDegistir(gelen, NewUserPassword, NewPasswordConfirm);
                TempData["message"] = geriBildirim.ToString();
            }
            return RedirectToAction("MemberShip");
        }

        [HttpPost]
        public ActionResult Sil(products ilan, details detay, features ozellik, images gorsel, favorites favori)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                if (ilan.productId.ToString() != "0")
                {
                    try
                    {
                        var vSorgu2 = db.details.Where(w => w.productId == ilan.productId).FirstOrDefault();
                        try
                        {
                            System.IO.File.Delete(HttpContext.Server.MapPath("~/Content/uploads/attachments/" + vSorgu2.attachments.ToString()));
                        }
                        catch (Exception)
                        {
                        }
                        db.details.Remove(vSorgu2);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        var vSorgu4 = db.features.Where(w => w.productId == ilan.productId).FirstOrDefault();
                        db.features.Remove(vSorgu4);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        var vSorgu5 = db.images.Where(w => w.productId == ilan.productId).ToList();
                        try
                        {
                            foreach (var item in vSorgu5)
                            {
                                System.IO.File.Delete(HttpContext.Server.MapPath("~/Content/uploads/images/" + item.imageSource.ToString()));
                                db.images.Remove(item);
                                db.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                    }

                    var vSorgu = db.products.Where(w => w.productId == ilan.productId).FirstOrDefault();
                    try
                    {
                        var vSorgu22 = db.favorites.Where(w => w.productId == vSorgu.productId).FirstOrDefault();
                        db.favorites.Remove(vSorgu22);
                    }
                    catch (Exception)
                    {
                    }
                    db.products.Remove(vSorgu);
                    db.SaveChanges();
                    TempData["DonenMesaj"] = "Başarılı! ilan kaldırıldı.";
                    return RedirectToAction("MyAds");
                }
                if (favori.favoriteId.ToString() != "0")
                {
                    try
                    {
                        var vSorgu = db.favorites.Where(w => w.favoriteId == favori.favoriteId).FirstOrDefault();
                        db.favorites.Remove(vSorgu);
                        db.SaveChanges();
                        TempData["DonenMesaj"] = "Başarılı! ilan favorilerinden kaldırıldı.";
                        return RedirectToAction("MyFavorites");
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("MyFavorites");
                    }
                }
            }
            catch (Exception)
            {
                TempData["DonenHataMesaji"] = "Bir hata oluştu! Daha sonra tekrar deneyin.";
                return RedirectToAction("MyAds");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult GetDistricts(int prvCode)
        {
            var model = Process.CountiesForAdvertise(prvCode);
            return Json(model);
        }

    }
}