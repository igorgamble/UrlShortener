using System;
using System.Web;
using System.Web.UI;
using UrlShortener.Models;

namespace UrlShortener
{
    public partial class Redirect : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var shortstrings = Functions.GetAllShortenlUrls();
            var domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var urlEnd = HttpContext.Current.Request.Url.ToString().Replace($"{domainName}/", string.Empty);

            var html = string.Empty;
            if (urlEnd.Equals("Redirect.aspx"))
            {
                foreach (var shortstring in shortstrings)
                {
                    var shortLink = $"{domainName}/{shortstring}";
                    var originalUrl = Functions.GetOriginalUrl(shortstring);
                    html += $@"<div class='alert alert-success'><a href='{originalUrl}' target='blanc'>{shortLink}</div>";
                }
            }
            else if (shortstrings.Contains(urlEnd))
            {
                var originalUrl = Functions.GetOriginalUrl(urlEnd);
                Response.RedirectPermanent(originalUrl);
            }
            else
            {
                html = @"<div class='alert alert-danger'>Page not Found</div>";
            }

            result_area.InnerHtml = html;
        }
    }
}