using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace NETUiHentaiXamarin.ehentai
{
    internal class img_list
    {
        private List<string> pass_extraction(string URL)
        {
            List<string> extractionL_list = new List<string>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //string URL = "https://e-hentai.org/g/2447353/e2e5797e67";
            doc = web.Load(URL);

            var img_list_html = doc.DocumentNode.SelectNodes("//td");

            foreach (HtmlNode pass in img_list_html)
            {
                if (pass.InnerHtml.Contains("pages"))
                {
                    string ReplaceResult = pass.InnerHtml.Replace("pages", "");
                    ReplaceResult = ReplaceResult.Replace(" ", "");
                    int total = Convert.ToInt32(ReplaceResult);
                    int page = 0;
                    while (true)
                    {
                        page++;
                        total = total - 40;
                        if (total <= -1)
                        {
                            break;
                        }
                    }

                    for (int i = 0; i < page; i++)
                    {
                        //Console.WriteLine($"{URL}?p={i}");
                        extractionL_list.Add($"{URL}?p={i}");
                    }
                }
            }
            return extractionL_list;
        }

        private List<string> pass_url(string passURL) 
        {
            List<string> passURL_list = new List<string>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(passURL);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];

                if (att.Value.Contains("a"))
                {
                    string urls = att.Value;
                    if (urls.Contains("e-hentai.org/s/"))
                    {
                        //Console.WriteLine(urls);
                        passURL_list.Add(urls);
                    }
                }
            }

            return passURL_list;
        }

        public List<string> absorptions(string URL) 
        {
            List<string> absorptions_list = new List<string>();
            foreach (var value1 in pass_extraction(URL))
            {
                //Console.WriteLine(value1);
                foreach (var value2 in pass_url(value1))
                {
                    //Console.WriteLine(value2);
                    absorptions_list.Add(value2);
                }

            }
            return absorptions_list;
        }

    }
}
