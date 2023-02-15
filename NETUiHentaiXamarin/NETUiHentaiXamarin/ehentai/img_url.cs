using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace NETUiHentaiXamarin.ehentai
{
    internal class img_url
    {
        public string ExtractHref_img(string URL)
        {
            string filesURL = "";
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(URL);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='sni']//img[@id]"))
            {
                HtmlAttribute att = link.Attributes["src"];
                filesURL = att.Value;
            }
            return filesURL;
        }
    }
}
