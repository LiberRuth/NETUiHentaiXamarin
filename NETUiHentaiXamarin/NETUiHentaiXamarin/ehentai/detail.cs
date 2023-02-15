using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;


namespace NETUiHentaiXamarin.ehentai
{
    internal class detail
    {

        //private string language_text;


        public IDictionary<string, string> detail_txt(string URL)
        {
            Dictionary<string, string> detail_txt = new Dictionary<string, string>();
            List<string> artist = new List<string>();
            List<string> parody = new List<string>();
            List<string> group = new List<string>();
            List<string> genre = new List<string>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            // string URL = "https://e-hentai.org/g/2447353/e2e5797e67"; 
            //string URL = "https://e-hentai.org/g/1837120/47f9e7217a/";
            //string URL = "https://e-hentai.org/g/1921126/866e9dde2b"; 
            doc = web.Load(URL);

            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("html");

            foreach (HtmlNode node in htmlNodes)
            {

                //썸네일
                var thumbnail = node.SelectSingleNode("//div[@class='gm']/div[@id='gleft']/div[@id]");
                string url_magic = thumbnail.InnerHtml;
                url_magic = url_magic.Substring(url_magic.IndexOf("(") + 1).Trim();
                url_magic = url_magic.Substring(0, url_magic.LastIndexOf(")"));
                detail_txt.Add("thumbnail", url_magic);
                // Console.WriteLine(url_magic);

                //제목
                var title = node.SelectSingleNode("//title");
                //title_name = title.InnerText;
                detail_txt.Add("title", title.InnerText);

                //유형
                var type = node.SelectSingleNode("//div[@id='gmid']/div[@id]/div[@id='gdc']");
                //type_name = type.InnerText;
                detail_txt.Add("type", type.InnerText);

                //날짜
                var date = node.SelectSingleNode("//td[@class='gdt2']");
                //date_text = date.InnerText;
                detail_txt.Add("date", date.InnerText);

                //고유번호
                var number = node.SelectNodes("//td[@class='gdt2']")[1];
                //unique_number = number.InnerText;
                detail_txt.Add("number", number.InnerText);

                //언어
                var language_original = node.SelectNodes("//td[@class='gdt2']")[3];
                string language_text = language_original.InnerText;
                language_text = language_text.Substring(0, language_text.LastIndexOf("&"));
                detail_txt.Add("language", language_text);

                //용량
                var sizes = node.SelectNodes("//td[@class='gdt2']")[4];
                //data_size = sizes.InnerText;
                detail_txt.Add("sizes", sizes.InnerText);

                //총 페이지수
                var pages_original = node.SelectNodes("//td[@class='gdt2']")[5];
                string all_pages = pages_original.InnerText;
                all_pages = all_pages.Replace("pages", "");
                all_pages = all_pages.Replace(" ", "");
                detail_txt.Add("pages", all_pages);


                //태그 
                //var all_htm = node.SelectNodes("//div[@id='taglist']/div");
                HtmlNodeCollection all_htm = doc.DocumentNode.SelectNodes("//div[@id]/div[@id='taglist']/table");

                foreach (HtmlNode text in all_htm)
                {

                    foreach (var item in text.SelectNodes("//tr/td/div[@class]"))
                    {
                        //작가
                        if (item.OuterHtml.Contains("artist:")) artist.Add(item.InnerText);

                        //원작
                        if (item.OuterHtml.Contains("parody:")) parody.Add(item.InnerText);

                        //그룹
                        if (item.OuterHtml.Contains("group:")) group.Add(item.InnerText);

                        //태그관련
                        //남성
                        if (item.OuterHtml.Contains("male:")) genre.Add($"♂:{item.InnerText}");

                        //여성
                        if (item.OuterHtml.Contains("female:")) genre.Add($"♀:{item.InnerText}");

                        if (item.OuterHtml.Contains("other:")) genre.Add($"?:{item.InnerText}"); //기타

                        if (item.OuterHtml.Contains("mixed:")) genre.Add($"?:{item.InnerText} "); //그룹(단체)


                    }

                }

                detail_txt.Add("artist", String.Join(", ", artist.ToArray())); //작가
                detail_txt.Add("parody", String.Join(", ", parody.ToArray())); //원작
                detail_txt.Add("group", String.Join(", ", group.ToArray())); //그룹
                detail_txt.Add("genre", String.Join(", ", genre.ToArray())); //태그//

            }
            return detail_txt;
        }

        public IDictionary<string, string> essential_detail(string URL) 
        {
            Dictionary<string, string> essential_txt = new Dictionary<string, string>();
            List<string> artist = new List<string>();
            List<string> parody = new List<string>();
            List<string> group = new List<string>();
            List<string> genre = new List<string>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(URL);

            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("html");

            foreach (HtmlNode node in htmlNodes)
            {

                //썸네일
                var thumbnail = node.SelectSingleNode("//div[@class='gm']/div[@id='gleft']/div[@id]");
                string url_magic = thumbnail.InnerHtml;
                url_magic = url_magic.Substring(url_magic.IndexOf("(") + 1).Trim();
                url_magic = url_magic.Substring(0, url_magic.LastIndexOf(")"));
                essential_txt.Add("thumbnail", url_magic);
                // Console.WriteLine(url_magic);

                //제목
                var title = node.SelectSingleNode("//title");
                //title_name = title.InnerText;
                essential_txt.Add("title", title.InnerText);

                //유형
                var type = node.SelectSingleNode("//div[@id='gmid']/div[@id]/div[@id='gdc']");
                //type_name = type.InnerText;
                essential_txt.Add("type", type.InnerText);

                //날짜
                var date = node.SelectSingleNode("//td[@class='gdt2']");
                //date_text = date.InnerText;
                essential_txt.Add("date", date.InnerText);

                //고유번호
                var number = node.SelectNodes("//td[@class='gdt2']")[1];
                //unique_number = number.InnerText;
                essential_txt.Add("number", number.InnerText);

                //언어
                var language_original = node.SelectNodes("//td[@class='gdt2']")[3];
                string language_text = language_original.InnerText;
                language_text = language_text.Substring(0, language_text.LastIndexOf("&"));
                essential_txt.Add("language", language_text);

                //용량
                var sizes = node.SelectNodes("//td[@class='gdt2']")[4];
                //data_size = sizes.InnerText;
                essential_txt.Add("sizes", sizes.InnerText);

                //총 페이지수
                var pages_original = node.SelectNodes("//td[@class='gdt2']")[5];
                string all_pages = pages_original.InnerText;
                all_pages = all_pages.Replace("pages", "");
                all_pages = all_pages.Replace(" ", "");
                essential_txt.Add("pages", all_pages);

            }
            return essential_txt;
        }


        public bool TypeDetection(string URL)
        {
            bool tys = true;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(URL);

            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("html");

            foreach (HtmlNode ty in htmlNodes)
            {
                var tyText = ty.SelectSingleNode("//div[@id='taglist']");
                if (tyText.OuterHtml.Contains("No tags"))
                {
                    tys = false; 
                }
                else 
                {
                    tys = true;
                }
            }

            return tys;
        }
    }

}
