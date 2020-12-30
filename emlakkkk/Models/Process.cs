using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emlakkkk.Models
{
    public static class Process
    {
        public static List<products> homeProducts()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.products.OrderByDescending(w=>w.productId).Take(12).ToList();
        }

        public static List<products> listingProducts()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.products.OrderByDescending(w => w.productId).ToList();
        }

        public static products productDetail(int id)
        {
            emlksisEntities db = new emlksisEntities();
            var ılanDetay = db.products.Where(x => x.productId == id).FirstOrDefault();
            return ılanDetay;
        }

        public static List<images> productDetailImages(int id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.images.Where(w=>w.productId == id).ToList();
        }

        public static details detail(int id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.details.Where(w => w.productId == id).FirstOrDefault();
        }

        public static features feature(int id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.features.Where(w => w.productId == id).FirstOrDefault();
        }

        public static List<products> userProducts()
        {
            emlksisEntities veritabani = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(HttpContext.Current.Session["userid"]);
            return veritabani.products.Where(x => x.userId == sessiondakiDeğer).OrderByDescending(x => x.productId).ToList();
        }

        public static List<products> memberProducts(int? id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.products.Where(x => x.userId == id).OrderByDescending(x => x.productId).ToList();

        }

        public static List<images> memberProductsImages(int id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.images.Where(w => w.products.users.userId == id).ToList();
        }

        public static List<favorites> userFavorites()
        {
            emlksisEntities veritabani = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(HttpContext.Current.Session["userid"]);
            return veritabani.favorites.Where(w => w.userId == sessiondakiDeğer).ToList();
        }

        public static List<favorites> favoriEklenmismi(int? id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(HttpContext.Current.Session["userid"]);
            return veritabani.favorites.Where(w => w.productId == id && w.userId == sessiondakiDeğer).ToList();
        }

        public static List<images> allImages()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.images.ToList();
        }

        public static List<categories> allCategories()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.categories.OrderBy(w => w.categoryName).ToList();
        }

        public static List<posts> allPosts()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.posts.OrderByDescending(w => w.postId).ToList();
        }

        public static List<posts> allPostsForSingle()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.posts.OrderByDescending(w => w.postId).Take(2).ToList();
        }

        public static List<posts> allPostsForHome()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.posts.OrderByDescending(w => w.postId).Take(3).ToList();
        }

        public static List<products> allProductsForSingle()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.products.OrderByDescending(w => w.productId).Take(3).ToList();
        }

        public static posts postDetail(int id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.posts.Where(w=>w.postId == id).FirstOrDefault();
        }

        public static List<sss> allSss()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.sss.OrderByDescending(w => w.sssId).ToList();
        }

        public static List<provinces> allProvinces()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.provinces.OrderBy(w => w.provinceName).ToList();
        }

        public static List<counties> allCounties()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.counties.OrderBy(w => w.countyName).ToList();
        }

        public static List<users> allUser()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.users.OrderBy(w => w.name).ToList();
        }

        public static List<provinces> allProvincesForAdvertise()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.provinces.OrderBy(w => w.provinceName).ToList();
        }

        public static List<counties> allCountiesForAdvertise()
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.counties.OrderBy(w => w.countyName).ToList();
        }


        public static List<counties> CountiesForAdvertise(int prvCode)
        {
            emlksisEntities veritabani = new emlksisEntities();
            var vSonuc = veritabani.counties.Where(w=>w.provinces.provinceCode == prvCode).OrderBy(w => w.countyName).ToList();
            return vSonuc;
        }

        public static users userDetail(int? id)
        {
            emlksisEntities veritabani = new emlksisEntities();
            return veritabani.users.Where(w=>w.userId == id).FirstOrDefault();
        }

        public static void addFavorite(favorites yeni1, int? id, int? sessiondakiDeğer)
        {
            emlksisEntities db = new emlksisEntities();
            yeni1.userId = sessiondakiDeğer;
            yeni1.productId = id;
            db.favorites.Add(yeni1);
            db.SaveChanges();
        }

        public static string BilgiDegistir(users gelen)
        {
            emlksisEntities db = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(HttpContext.Current.Session["userid"]);
            if (gelen != null)
            {
                var vSorgu = db.users.Where(x => x.userId == sessiondakiDeğer).FirstOrDefault();
                if (gelen.avatarSource != null)
                {
                    try
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Content/uploads/avatar/" + vSorgu.avatarSource.ToString()));
                    }
                    catch (Exception)
                    {
                    }
                    vSorgu.avatarSource = gelen.avatarSource;
                }
                vSorgu.mail = gelen.mail;
                vSorgu.name = gelen.name;
                vSorgu.surname = gelen.surname;
                vSorgu.phone = gelen.phone;
                vSorgu.about = gelen.about;
                db.SaveChanges();
                return "Bilgileriniz başarıyla Güncellendi.";

            }
            else
            {
                return "HATA! Bilgileriniz güncellenemedi.";
            }

        }

        public static string SosyalDegistir(users gelen)
        {
            emlksisEntities db = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(HttpContext.Current.Session["userid"]);
            if (gelen != null)
            {
                var vSorgu = db.users.Where(x => x.userId == sessiondakiDeğer).FirstOrDefault();
                vSorgu.socialFacebook = gelen.socialFacebook;
                vSorgu.socialInstagram = gelen.socialInstagram;
                vSorgu.socialLinkedin = gelen.socialLinkedin;
                vSorgu.socialPinterest = gelen.socialPinterest;
                vSorgu.socialSkype = gelen.socialSkype;
                vSorgu.socialTwitter = gelen.socialTwitter;
                vSorgu.socialWebsite = gelen.socialWebsite;
                vSorgu.socialYoutube = gelen.socialYoutube;
                db.SaveChanges();
                return "Sosyal medya bilgileriniz başarıyla Güncellendi.";
            }
            else
            {
                return "HATA! Bilgileriniz güncellenemedi.";
            }

        }

        public static string SifreDegistir(users gelen, string NewUserPassword, string NewPasswordConfirm)
        {
            emlksisEntities db = new emlksisEntities();
            int sessiondakiDeğer = Convert.ToInt32(HttpContext.Current.Session["userid"]);
            var vSorgu = db.users.Where(x => x.userId == sessiondakiDeğer).FirstOrDefault();
            if (gelen.password == vSorgu.password)
            {
                if (NewUserPassword == NewPasswordConfirm)
                {
                    vSorgu.password = NewUserPassword;
                    db.SaveChanges();
                    return "Şifreniz başarıyla değiştirildi.";
                }
                else
                {
                    return "Yeni şifreler uyuşmuyor. Lütfen yeni şifrelerin aynı olmasına dikkat edin!";
                }
            }
            else
            {
                return "Eski şifreniz uyuşmuyor ya da yanlış!";
            }
        }
    }
}