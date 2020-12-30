using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emlakkkk.Models;
using emlakkkk.Models.Giris;

namespace emlakkkk.Controllers
{
    [ControlLoginAdmin]
    public class AdminController : Controller
    {

        public ActionResult Ilanlar()
        {
            var v = Process.listingProducts();
            return View(Tuple.Create(v));
        }

        public ActionResult Kategoriler()
        {
            var v = Process.allCategories();
            return View(v);
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Posts()
        {
            var v = Process.allPosts();
            return View(Tuple.Create(v));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Posts(posts yeni, IEnumerable<HttpPostedFileBase> postImage1)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                if (yeni.postContent.Count() <= 9999)
                {
                    HttpPostedFileBase Gorsel01 = Request.Files[0];
                    FileInfo dosyaBil = new FileInfo(Gorsel01.FileName);
                    string yeniIsım = Guid.NewGuid().ToString("N") + dosyaBil.Extension;
                    Gorsel01.SaveAs(Server.MapPath("~/Content/Uploads/images/") + yeniIsım);
                    yeni.postImage = yeniIsım;
                    yeni.userId = Convert.ToInt32(Session["userid"]);
                    yeni.releaseDate = DateTime.Now;
                    db.posts.Add(yeni);
                    db.SaveChanges();
                    return RedirectToAction("Posts");
                }
                else if (yeni.postContent.Count() >= 9999)
                {
                    TempData["MessageRed"] = "Lütfen 10.000 karakteri aşmayınız.";
                    TempData["postBaslik"] = yeni.postTitle;
                    TempData["postIcerik"] = yeni.postContent;
                    return RedirectToAction("Posts");
                }
            }
            catch (Exception)
            {
                TempData["MessageRed"] = "Post İçeriği Boş Olamaz!.";
                TempData["postBaslik"] = yeni.postTitle;
                TempData["postIcerik"] = yeni.postContent;
                return RedirectToAction("Posts");
            }

            return RedirectToAction("Posts");
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Sss()
        {
            var v = Process.allSss();
            return View(Tuple.Create(v));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sss(sss yeni)
        {
            try
            {
                emlksisEntities db = new emlksisEntities();
                if (yeni.sssContent.Count() <= 9999)
                {
                    db.sss.Add(yeni);
                    db.SaveChanges();
                    return RedirectToAction("Sss");
                }
                else if (yeni.sssContent.Count() >= 9999)
                {
                    TempData["MessageRed"] = "Lütfen 10.000 karakteri aşmayınız.";
                    TempData["postBaslik"] = yeni.sssTitle;
                    TempData["postIcerik"] = yeni.sssContent;
                    return RedirectToAction("Sss");
                }
            }
            catch (Exception)
            {
                TempData["MessageRed"] = "Post İçeriği Boş Olamaz!.";
                TempData["postBaslik"] = yeni.sssTitle;
                TempData["postIcerik"] = yeni.sssContent;
                return RedirectToAction("Sss");
            }

            return RedirectToAction("Sss");
        }

        public ActionResult Kullanicilar()
        {
            var v = Process.allUser();
            return View(v);
        }

        [ValidateInput(false)]
        public ActionResult Hakkimizda()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ValidateInput(false)]
        public ActionResult Iletisim()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Sil(products ilan, details detay, features ozellik, images gorsel, favorites favori,users kullanici, posts post, sss ssSorular, categories GelenKategori)
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
                    return RedirectToAction("Ilanlar");
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
                if (kullanici.userId.ToString() != "0")
                {
                    try
                    {
                        var vSorgu = db.users.Where(w => w.userId == kullanici.userId).FirstOrDefault();
                        db.users.Remove(vSorgu);
                        db.SaveChanges();
                        TempData["DonenMesaj"] = "Başarılı! Kullanıcı kaldırıldı.";
                        return RedirectToAction("Kullanicilar");
                    }
                    catch (Exception)
                    {
                        TempData["MessageRed"] = "Bu kullanıcı altında öğe(ler) mevcut olduğundan kullanıcı silinemiyor.";
                        return RedirectToAction("Kullanicilar");
                    }
                }
                if (post.postId.ToString() != "0")
                {
                    try
                    {
                        var vSorgu = db.posts.Where(w => w.postId == post.postId).FirstOrDefault();
                        try
                        {
                            System.IO.File.Delete(HttpContext.Server.MapPath("~/Content/uploads/images/" + vSorgu.postImage.ToString()));
                        }
                        catch (Exception)
                        {
                        }
                        db.posts.Remove(vSorgu);
                        db.SaveChanges();
                        TempData["DonenMesaj"] = "Başarılı! post kaldırıldı.";
                        return RedirectToAction("Posts");
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Posts");
                    }
                }
                if (ssSorular.sssId.ToString() != "0")
                {
                    try
                    {
                        var vSorgu = db.sss.Where(w => w.sssId == ssSorular.sssId).FirstOrDefault();
                        db.sss.Remove(vSorgu);
                        db.SaveChanges();
                        TempData["DonenMesaj"] = "Başarılı! sss kaldırıldı.";
                        return RedirectToAction("Sss");
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Sss");
                    }
                }
                if (GelenKategori.categoryId.ToString() != "0")
                {
                    try
                    {
                        var vSorgu = db.categories.Where(w => w.categoryId == GelenKategori.categoryId).FirstOrDefault();
                        db.categories.Remove(vSorgu);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        TempData["MessageRed"] = "Bu kategori altında öğe(ler) mevcut olduğundan kategori silinemiyor.";
                    }
                    return RedirectToAction("Kategoriler");
                }
            }
            catch (Exception)
            {
                TempData["DonenHataMesaji"] = "Bir hata oluştu! Daha sonra tekrar deneyin.";
                return RedirectToAction("MyAds");
            }

            return View();
        }
    }
}