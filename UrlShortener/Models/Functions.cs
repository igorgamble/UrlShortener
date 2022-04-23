using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortener.Helper;

namespace UrlShortener.Models
{
    public class Functions
    {
        public static bool IsValidUrl(string url)
             => Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        public static string GetOriginalUrl(string shortUrl)
        {
            var values = SqlHelper.GetColumnValues($"Select * From Links Where ShortUrl='{shortUrl}'", "OriginalUrl");
            return values.Count > 0 ? values.First() : null;
        }

        public static string AddNewLink(string url)
        {
            var shortUrl = GenerateShortUrl();
            SqlHelper.ExecuteQuery($"Insert into Links (ShortUrl, OriginalUrl) Values ('{shortUrl}', '{url}')");
            return shortUrl;
        }

        public static List<string> GetAllShortenlUrls() => SqlHelper.GetColumnValues("Select * From Links", "ShortUrl");

        private static string GenerateShortUrl()
        {
            var shorturl = RandomUrl(5);
            while (GetOriginalUrl(shorturl) != null)
            {
                shorturl = RandomUrl(5);
            }
            return shorturl;
        }

        private static string RandomUrl(int length)
        {
            var rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}