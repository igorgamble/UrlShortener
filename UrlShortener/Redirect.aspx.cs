using NLog;
using System;
using System.Web;
using System.Web.UI;
using UrlShortener.Models;

namespace UrlShortener
{
    public partial class Redirect : Page
    {
        private static readonly Logger _logger = LogManager.GetLogger("UrlShortenerRule");

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
                    html += $@"<div class='alert alert-success'><a href='{shortLink}' target='blanc'>{shortLink}</div>";
                }

                _logger.Info("List of shortUrls is opened");
            }
            else if (shortstrings.Contains(urlEnd))
            {
                var originalUrl = Functions.GetOriginalUrl(urlEnd);
                _logger.Info($"Opened {originalUrl} using {domainName}/{urlEnd}");
                Response.Redirect(originalUrl);
            }
            else
            {
                html = @"<div class='alert alert-danger'>Page not Found</div>";
                _logger.Error($"Page not Found for {domainName}/{ urlEnd}");
            }

            result_area.InnerHtml = html;
        }
    }
}