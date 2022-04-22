using System;
using System.Web;
using System.Web.UI;
using UrlShortener.Models;

namespace UrlShortener
{
    public partial class Default : Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Functions.IsValidUrl(url.Value))
            {
                var shortstring = Functions.AddNewLink(url.Value);
                var domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                var shortLink = $"{domainName}/{shortstring}";
                var originalUrl = Functions.GetOriginalUrl(shortstring);
                data_place.InnerHtml = $@"<div class='alert alert-success'><a href='{originalUrl}' target='blanc'>{shortLink}</div>";
            }
            else
            {
                data_place.InnerHtml = @"<div class='alert alert-danger'>Invalid Url</div>";
            }
        }

        protected void Button2_Click(object sender, EventArgs e) => Response.RedirectPermanent("Redirect.aspx");
    }
}