using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace emlakkkk.Helper
{
    public static class Helper
    {
        public static string FriendlyURLTitle(this UrlHelper urlHelper, string incomingText)
        {

            if (incomingText != null)
            {
                incomingText = incomingText.Replace("ş", "s");
                incomingText = incomingText.Replace("Ş", "s");
                incomingText = incomingText.Replace("İ", "i");
                incomingText = incomingText.Replace("I", "i");
                incomingText = incomingText.Replace("ı", "i");
                incomingText = incomingText.Replace("ö", "o");
                incomingText = incomingText.Replace("Ö", "o");
                incomingText = incomingText.Replace("ü", "u");
                incomingText = incomingText.Replace("Ü", "u");
                incomingText = incomingText.Replace("Ç", "c");
                incomingText = incomingText.Replace("ç", "c");
                incomingText = incomingText.Replace("ğ", "g");
                incomingText = incomingText.Replace("Ğ", "g");
                incomingText = incomingText.Replace(" ", "-");
                incomingText = incomingText.Replace("---", "-");
                incomingText = incomingText.Replace("?", "");
                incomingText = incomingText.Replace("/", "");
                incomingText = incomingText.Replace(".", "");
                incomingText = incomingText.Replace("'", "");
                incomingText = incomingText.Replace("#", "");
                incomingText = incomingText.Replace("%", "");
                incomingText = incomingText.Replace("&", "");
                incomingText = incomingText.Replace("*", "");
                incomingText = incomingText.Replace("!", "");
                incomingText = incomingText.Replace("@", "");
                incomingText = incomingText.Replace("+", "");
                incomingText = incomingText.ToLower();
                incomingText = incomingText.Trim();
                // tüm harfleri küçült
                string encodedUrl = (incomingText ?? "").ToLower();
                // & ile " " yer değiştirme
                encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");
                // " " karakterlerini silme
                encodedUrl = encodedUrl.Replace("'", "");
                // geçersiz karakterleri sil
                encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");
                // tekrar edenleri sil
                encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
                // karakterlerin arasına tire koy
                encodedUrl = encodedUrl.Trim('-');
                return encodedUrl;
            }
            else
            {
                return "";
            }
        }

        internal static string FriendlyURLTitle(string catURL)
        {
            throw new NotImplementedException();
        }


        public static string BrowserSorgusuManuel(string imageAdi)
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpBrowserCapabilities browser = httpRequest.Browser;

            if (browser.Type.Contains("Chrome") || browser.Type.Contains("Opera") || browser.Type.Contains("Android"))
            {
                return imageAdi+".webp";
            }

            return imageAdi+".jpg";
        }

        private static bool BrowserNeAga()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpBrowserCapabilities browser = httpRequest.Browser;

            if (browser.Type.Contains("Chrome") || browser.Type.Contains("Opera") || browser.Type.Contains("Android"))
            {
                return true;
            }

            return false;
        }

        public static MvcHtmlString DrawImage(this HtmlHelper helper, string imageUrl)
        {
            if (BrowserNeAga())
            {
                // Get the file type
                string fileType = Path.GetExtension(imageUrl);
                if (fileType != null)
                {
                    imageUrl = imageUrl.Replace(fileType, ".webp");
                }

                return new MvcHtmlString(String.Format(imageUrl));
            }

            return new MvcHtmlString(String.Format(imageUrl));
        }
    }
}