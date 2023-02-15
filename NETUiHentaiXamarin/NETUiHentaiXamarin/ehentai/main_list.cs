using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace NETUiHentaiXamarin.ehentai
{
    internal class main_list
    {
        
        public IDictionary<string, List<string>> manga_information(string URL)
        {

            Dictionary<string, List<string>> manga_information = new Dictionary<string, List<string>>();
            List<string> type_list = new List<string>();
            List<string> title_list = new List<string>();
            List<string> date_list = new List<string>();
            List<string> list_url = new List<string>();

            List<string> list_thumbnail_one = new List<string>();
            List<string> list_thumbnail = new List<string>();

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(URL);
;

            foreach (HtmlNode mainHTML in doc.DocumentNode.SelectNodes("html"))
            {
                //유형
                //var type_text = mainHTML.SelectNodes("//td[@class='gl1c glcat']");
                foreach (HtmlNode type in mainHTML.SelectNodes("//td[@class='gl1c glcat']"))
                {
                    //Console.WriteLine(type.InnerText);
                    type_list.Add(type.InnerText);
                }
                manga_information.Add("type", type_list);

                //제목
                foreach (HtmlNode title in mainHTML.SelectNodes("//div[@class='glink']"))
                {
                    //Console.WriteLine(title.InnerText);
                    title_list.Add(title.InnerText);              
                }
                manga_information.Add("title", title_list);

                //날짜
                foreach (HtmlNode iddate in mainHTML.SelectNodes("//div[@onclick]"))
                {
                    var htmldate = iddate.OuterHtml;
                    if (htmldate.Contains("popUp")) 
                    {
                        //Console.WriteLine(iddate.InnerText);
                        date_list.Add(iddate.InnerText);
                    }        
                }
                manga_information.Add("date", date_list);

                //만화 URL             
                foreach (HtmlNode manga_url in mainHTML.SelectNodes("//tr/td[@class='gl3c glname']/a[@href]"))
                {
                    //Console.WriteLine(manga_url.Attributes["href"].Value);
                    list_url.Add(manga_url.Attributes["href"].Value);
                }
                manga_information.Add("url", list_url);

                //썸네일
                var oneHTML = mainHTML.SelectSingleNode("//tr//td[@class='gl2c']/div[@class='glthumb']/div/img[@src]");
                list_thumbnail.Add(oneHTML.Attributes["src"].Value);

                foreach (HtmlNode thumbnail_img in mainHTML.SelectNodes("//tr//td[@class='gl2c']/div[@class='glthumb']/div/img[@data-src]"))
                //foreach (HtmlNode thumbnail_img in mainHTML.SelectNodes("//div/img[@data-src]"))
                {
                    //Console.WriteLine(thumbnail_img.Attributes["data-src"].Value);
                    list_thumbnail.Add(thumbnail_img.Attributes["data-src"].Value);
                }
                manga_information.Add("thumbnai", list_thumbnail);

                /*
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div/img[@src]"))
                {
                    //Console.WriteLine(link.Attributes["src"].Value);
                    list_thumbnail_one.Add(link.Attributes["src"].Value);
                }
                manga_information.Add("thumbnai_one", list_thumbnail_one);
                */
            }

            return manga_information;
        }
        

        public string main_next(string URL)
        {
            string nextpage = "";

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(URL);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                //다음페이지
                if (link.OuterHtml.Contains("dnext"))
                {
                    nextpage = link.Attributes["href"].Value;
                    //Console.WriteLine(nextpage.Value); 
                }
            }

            return nextpage;
        }

    }
}
