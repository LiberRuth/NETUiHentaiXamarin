using NETUiHentaiXamarin.ehentai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NETUiHentaiXamarin.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        detail Detail = new detail();

        string img_url;

        public DetailPage(string nextURL)
        {
            InitializeComponent();

            webbring(nextURL);
            img_url = nextURL;

        }


        private async void webbring(string nextURL)
        {
            await Task.Run(async () =>
            {
                if (Detail.TypeDetection(nextURL))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        var Detail_all = Detail.detail_txt(nextURL);
                        Title_Label.Text = $"{Detail_all["title"]}";
                        DetailIMG.Source = $"{Detail_all["thumbnail"]}";
                        type_Label.Text = $"{Detail_all["type"]}";
                        artist_Label.Text = $"{Detail_all["artist"]}";
                        language_Label.Text = $"{Detail_all["language"]}";
                        date_Label.Text = $"{Detail_all["date"]}";
                        datasizes_Label.Text = $"{Detail_all["sizes"]}";
                        number_Label.Text = $"{Detail_all["number"]}";
                        pages_Label.Text = $"{Detail_all["pages"]}";
                        genre_Label.Text = $"{Detail_all["genre"]}";

                    });
                    await Task.Delay(500);
                }
                else
                {

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        var Deatail_point = Detail.essential_detail(nextURL);
                        Title_Label.Text = $"{Deatail_point["title"]}";
                        DetailIMG.Source = $"{Deatail_point["thumbnail"]}";
                        type_Label.Text = $"{Deatail_point["type"]}";
                        //artist_Label.Text = $"N/A";
                        language_Label.Text = $"{Deatail_point["language"]}";
                        date_Label.Text = $"{Deatail_point["date"]}";
                        datasizes_Label.Text = $"{Deatail_point["sizes"]}";
                        number_Label.Text = $"{Deatail_point["number"]}";
                        pages_Label.Text = $"{Deatail_point["pages"]}";
                    });
                    await Task.Delay(1000);
                }

            });
        }

        private void Download_Btn_text_Clicked(object sender, EventArgs e)
        {

        }

        private async void View_Btn_text_Clicked(object sender, EventArgs e)
        {
            //string nextURL = $"{listViews.SelectedItem}";
            await Navigation.PushAsync(new ImageView(img_url));
        }
    }
}