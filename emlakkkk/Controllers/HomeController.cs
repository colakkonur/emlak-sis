using emlakkkk.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlakkkk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ilan = Process.homeProducts();
            var gorsel = Process.allImages();
            var il = Process.allProvinces();
            var vPostlar = Process.allPostsForHome();
            return View(Tuple.Create(ilan,gorsel,il, vPostlar));
        }

        public static List<products> aramaSonucu = new List<products>();

        [Route("ilanlar")]
        public ActionResult Listing(int? filtreTetikleyici, int? filterDelete, products yeni, string Kategori, string provinceName, int? ilId , int? MinFiyat, int? MaxFiyat, int page = 1, int pageSize = 10)
        {
            if (filterDelete != null)
            {
                aramaSonucu.Clear();
            }
            if (filtreTetikleyici != null)
            {
                if (filtreTetikleyici == 1)
                {
                    var vSearch = emlakkkk.Models.Filter.Filter1(yeni);
                    aramaSonucu.Clear();
                    aramaSonucu.AddRange(vSearch);
                    var vImages = Process.allImages();
                    var vProvinces = Process.allProvinces();
                    var vCounties = Process.allCounties();
                    var vCategories = Process.allCategories();
                    ViewBag.kategoriler = vCategories.Select(w => w.categoryName.ToLower().ToString());
                    return View(Tuple.Create(aramaSonucu.ToPagedList(page, pageSize), vImages, vProvinces, vCounties));
                }
                else if (filtreTetikleyici == 2)
                {
                    var vFilter = emlakkkk.Models.Filter.Filter2(yeni, Kategori, provinceName, MinFiyat, MaxFiyat);
                    aramaSonucu.Clear();
                    aramaSonucu.AddRange(vFilter);
                    var vImages = Process.allImages();
                    var vProvinces = Process.allProvinces();
                    var vCounties = Process.allCounties();
                    var vCategories = Process.allCategories();
                    ViewBag.kategoriler = vCategories.Select(w => w.categoryName.ToLower().ToString());
                    return View(Tuple.Create(aramaSonucu.ToPagedList(page, pageSize), vImages, vProvinces, vCounties));
                }
                else if (filtreTetikleyici == 3)
                {
                    var vFilter = emlakkkk.Models.Filter.Filter3(ilId);
                    aramaSonucu.Clear();
                    aramaSonucu.AddRange(vFilter);
                    var vImages = Process.allImages();
                    var vProvinces = Process.allProvinces();
                    var vCounties = Process.allCounties();
                    var vCategories = Process.allCategories();
                    ViewBag.kategoriler = vCategories.Select(w => w.categoryName.ToLower().ToString());
                    return View(Tuple.Create(aramaSonucu.ToPagedList(page, pageSize), vImages, vProvinces, vCounties));
                }
            }
            if (aramaSonucu.Count() != 0)
            {
                var vImages = Process.allImages();
                var vProvinces = Process.allProvinces();
                var vCounties = Process.allCounties();
                var vCategories = Process.allCategories();
                ViewBag.kategoriler = vCategories.Select(w => w.categoryName.ToString());
                return View(Tuple.Create(aramaSonucu.ToPagedList(page, pageSize), vImages, vProvinces, vCounties));
            }
            var ilan = Process.listingProducts();
            var gorsel = Process.allImages();
            var il = Process.allProvinces();
            var ilce = Process.allCounties();
            var kat = Process.allCategories();
            ViewBag.kategoriler = kat.Select(w => w.categoryName.ToLower().ToString());
            return View(Tuple.Create(ilan.ToPagedList(page, pageSize), gorsel, il, ilce));
        }

        [Route("{category}/{baslik}-{id:int}")]
        public ActionResult ListingSingle(int id)
        {
            emlksisEntities db = new emlksisEntities();
            var ilanDetay = Process.productDetail(id);
            var gorsel = Process.productDetailImages(id);
            var detay = Process.detail(id);
            var ozellik = Process.feature(id);
            var eklenmismi = Process.favoriEklenmismi(id);
            if (eklenmismi != null)
            {
                if (eklenmismi.Count() == 1)
                {
                    ViewBag.FavoriSorgusu = "hidden";
                }
                else
                {
                    ViewBag.FavoriSorgusu = "";
                }
            }
            else
            {
                ViewBag.FavoriSorgusu = "";
            }
            ViewBag.provinces = db.provinces.Where(c => c.provinceId == ilanDetay.counties.provinces.provinceId).FirstOrDefault();
            ViewBag.counties = db.counties.Where(c => c.countyId == ilanDetay.countyId).FirstOrDefault();

            return View(Tuple.Create(ilanDetay,gorsel,detay,ozellik));
        }

        [Route("{ad}-{soyad}-{id:int}")]
        public ActionResult MemberSingle(int id)
        {
            var userDetay = Process.userDetail(id);
            var userIlanlar = Process.memberProducts(id);
            var vImages = Process.memberProductsImages(id);
            var vSonIlanlar = Process.allProductsForSingle();
            var gorsel = Process.allImages();
            ViewBag.gorseller = gorsel;
            return View(Tuple.Create(userDetay,userIlanlar, vImages,vSonIlanlar));
        }

        [Route("blog")]
        public ActionResult Blog(int page = 1, int pageSize = 5)
        {
            aramaSonucu.Clear();
            var vPosts = Process.allPosts();
            var vSonIlanlar = Process.allProductsForSingle();
            var gorsel = Process.allImages();
            return View(Tuple.Create(vPosts.ToPagedList(page,pageSize), vSonIlanlar, gorsel));

        }

        [Route("blog/{baslik}-{id:int}")]
        public ActionResult BlogSingle(int id)
        {
            var vPost = Process.postDetail(id);
            var vSonPostlar = Process.allPostsForSingle();
            var vSonIlanlar = Process.allProductsForSingle();
            var gorsel = Process.allImages();
            return View(Tuple.Create(vPost, vSonPostlar, vSonIlanlar, gorsel));

        }

        [Route("sikca-sorulan-sorular")]
        public ActionResult Sss()
        {
            aramaSonucu.Clear();
            var vSss = Process.allSss();
            return View(vSss);
        }

        [Route("hakkimizda")]
        public ActionResult About()
        {
            aramaSonucu.Clear();
            return View();
        }

        [Route("iletisim")]
        public ActionResult Contact()
        {
            aramaSonucu.Clear();
            return View();
        }


    }
}