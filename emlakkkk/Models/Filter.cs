using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emlakkkk.Models
{
    public static class Filter
    {
        public static List<products> Filter1(products yeni)
        {
            emlksisEntities veritabani = new emlksisEntities();
            var v2 = veritabani.products.Where(w => String.IsNullOrEmpty(yeni.productName) || w.productName.Contains(yeni.productName)).ToList();
            List<products> v3 = veritabani.products.Where(w => String.IsNullOrEmpty(yeni.productName) || w.productName.Contains(yeni.productName)).ToList();
            return v2;
        }

        public static List<products> Filter2(products yeni, string Kategori, string provinceName, int? MinFiyat, int? MaxFiyat)
        {
            emlksisEntities veritabani = new emlksisEntities();
            string sCategory = Kategori.Trim();
            string sProvince = provinceName.Trim();

            if (MinFiyat != null && MaxFiyat != null)
            {
                var vSorgu = veritabani.products.Where(w =>
                w.categories.categoryName == sCategory &&
                w.counties.provinces.provinceName == sProvince &&
                w.type == yeni.type &&
                w.price >= MinFiyat && w.price <= MaxFiyat).ToList();
                return vSorgu;
            }
            else if (MinFiyat != null && MaxFiyat == null)
            {
                var vSorgu = veritabani.products.Where(w =>
                w.categories.categoryName == sCategory &&
                w.counties.provinces.provinceName == sProvince &&
                w.type == yeni.type &&
                w.price >= MinFiyat).ToList();
                return vSorgu;
            }
            else if (MinFiyat == null && MaxFiyat != null)
            {
                var vSorgu = veritabani.products.Where(w =>
                w.categories.categoryName == sCategory &&
                w.counties.provinces.provinceName == sProvince &&
                w.type == yeni.type &&
                w.price <= MaxFiyat).ToList();
                return vSorgu;
            }
            else if (MinFiyat == null && MaxFiyat == null)
            {
                var vSorgu = veritabani.products.Where(w =>
                w.categories.categoryName == sCategory &&
                w.counties.provinces.provinceName == sProvince &&
                w.type == yeni.type
                ).ToList();
                return vSorgu;
            }
            var vSorgu01 = veritabani.products.Where(w =>
            w.categories.categoryName == sCategory &&
            w.counties.provinces.provinceName == sProvince &&
            w.type == yeni.type &&
            w.price >= MinFiyat && w.price <= MaxFiyat).ToList();
            return vSorgu01;
        }

        public static List<products> Filter3(int? ilId)
        {
            emlksisEntities veritabani = new emlksisEntities();
            var v3 = veritabani.products.Where(w => w.counties.provinces.provinceId == ilId).ToList();
            return v3;
        }
    }
}