using NETUiHentaiXamarin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using NETUiHentaiXamarin.view;
using NETUiHentaiXamarin.ehentai;

namespace NETUiHentaiXamarin
{
    public partial class MainPage : ContentPage
    {
        List<group_list> group_Lists = new List<group_list>();
        main_list main_List = new main_list();
        string present_Page = "https://e-hentai.org/"; 

        public MainPage()
        {
            InitializeComponent();


            var list_text = main_List.manga_information("https://e-hentai.org/");

            Debug.WriteLine($"{list_text["thumbnai"].Count}");


            for (int i = 0; i < list_text["thumbnai"].Count; i++)
            {
                group_Lists.Add(new group_list()
                {
                    Title = $"{list_text["title"][i]}",
                    Types = $"{list_text["type"][i]}",
                    Date = $"{list_text["date"][i]}",
                    Imageurl = $"{list_text["thumbnai"][i]}",
                    maURL = $"{list_text["url"][i]}",
                });
                //listViews.ItemsSource = group_Lists;
                Debug.WriteLine($"{list_text["thumbnai"][i]}-{i}");

            }


            /*
            group_Lists.Add(new group_list() { Title = "제목1", Types = "타입", Imageurl = "https://cdn.pixabay.com/photo/2023/01/30/23/09/bird-7756768_960_720.jpg" } );
            group_Lists.Add(new group_list() { Title = "제목2", Types = "타입", Imageurl = "https://cdn.pixabay.com/photo/2023/01/30/23/09/bird-7756768_960_720.jpg" } );
            group_Lists.Add(new group_list() { Title = "제목3", Types = "타입", Imageurl = "https://cdn.pixabay.com/photo/2023/01/30/23/09/bird-7756768_960_720.jpg" } );
            */
            listViews.ItemsSource = group_Lists;
        }


        public class group_list
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Types { get; set; }
            public string Types_color { get; set; }
            public string Details { get; set; }
            public string Imageurl { get; set; }
            public string Date { get; set; }
            public string maURL { get; set; }

            public override string ToString()
            {
                return maURL;
            }
        }

        private async void listViews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //new NavigationPage(new DetailPage());
            string DetailURL = $"{listViews.SelectedItem}";
            await Navigation.PushAsync(new DetailPage(DetailURL));   
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {

            var nextlink = main_List.main_next(present_Page);
            Debug.WriteLine($"{present_Page} > {nextlink}");

            var list_text = main_List.manga_information(nextlink);

            Debug.WriteLine($"{list_text["thumbnai"].Count}");


            for (int i = 0; i < list_text["thumbnai"].Count; i++)
            {
                group_Lists.Add(new group_list()
                {
                    Title = $"{list_text["title"][i]}",
                    Types = $"{list_text["type"][i]}",
                    Date = $"{list_text["date"][i]}",
                    Imageurl = $"{list_text["thumbnai"][i]}",
                    maURL = $"{list_text["url"][i]}",
                });
                //listViews.ItemsSource = group_Lists;
                Debug.WriteLine($"{list_text["thumbnai"][i]}-{i}");

            }

            listViews.ItemsSource = null;
            listViews.ItemsSource = group_Lists;

           
            present_Page = nextlink;

        }
    }
}
