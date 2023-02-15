using NETUiHentaiXamarin.ehentai;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static NETUiHentaiXamarin.MainPage;
using static NETUiHentaiXamarin.view.ImageView;
using static NETUiHentaiXamarin.view.SearchPage;

namespace NETUiHentaiXamarin.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        List<search_group_list> Search_group_List = new List<search_group_list>();
        main_list main_List = new main_list();
        string Search_present_Page;

        public SearchPage ()
		{
			InitializeComponent ();
		}

        public class search_group_list
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
        

        private void entText_Clicked(object sender, EventArgs e)
        {

            var list_text = main_List.manga_information($"https://e-hentai.org/?f_search={entry.Text}");

            Debug.WriteLine($"{list_text["thumbnai"].Count}");

            Search_group_List.Clear();
            for (int i = 0; i < list_text["thumbnai"].Count; i++)
            {
                Search_group_List.Add(new search_group_list()
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
            se_listView.ItemsSource = null;
            se_listView.ItemsSource = Search_group_List;
            Search_present_Page = $"https://e-hentai.org/?f_search={entry.Text}";
            /*
            Debug.WriteLine($"Sarch > {entry.Text}");
            Search_group_List.Clear();
            await Task.Run(async () =>
            {
                var sarch_list_text = main_List.manga_information($"https://e-hentai.org/?f_search={entry.Text}");
                Debug.WriteLine($"{sarch_list_text["thumbnai"].Count}");
                present_Page = $"https://e-hentai.org/?f_search={entry.Text}";
                for (int i = 0; i < sarch_list_text["thumbnai"].Count; i++)  
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Search_group_List.Add(new search_group_list()
                        {
                            Title = $"{sarch_list_text["title"][i]}",
                            Types = $"{sarch_list_text["type"][i]}",
                            //Date = $"{sarch_list_text["date"][i]}",
                            Imageurl = $"{sarch_list_text["thumbnai"][i]}",
                            maURL = $"{sarch_list_text["url"][i]}",
                        });
                    });
                    //await Task.Delay(1000);
                }
            });
            
            se_listView.ItemsSource = null;
            se_listView.ItemsSource = Search_group_List;
            */
        }

        private async void se_listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string DetailURL = $"{se_listView.SelectedItem}";
            await Navigation.PushAsync(new DetailPage(DetailURL));
        }


        private void ADDBtnList_Clicked(object sender, EventArgs e)
        {
            var nextlink = main_List.main_next(Search_present_Page);
            string replaceSTR = nextlink.Replace("amp;", "");
            Search_present_Page = replaceSTR;
            Debug.WriteLine(Search_present_Page);


            var list_text = main_List.manga_information(Search_present_Page);

            Debug.WriteLine($"{list_text["thumbnai"].Count}");
            for (int i = 0; i < list_text["thumbnai"].Count; i++)
            {
                Search_group_List.Add(new search_group_list()
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
            se_listView.ItemsSource = null;
            se_listView.ItemsSource = Search_group_List;

        }
    }
}