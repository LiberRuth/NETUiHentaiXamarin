using NETUiHentaiXamarin.ehentai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static NETUiHentaiXamarin.MainPage;

namespace NETUiHentaiXamarin.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageView : ContentPage
    {
        img_list img_List = new img_list();
        img_url img_Url = new img_url();
        List<urlText> imgURL_Lists = new List<urlText>();

        public ImageView(string URL)
        {
            InitializeComponent();
 
            imgWebSpider(URL);

        }

 

        private async void imgWebSpider(string urls) 
        {

            actlc.IsRunning = true;
            //var ListImage_URL = img_List.absorptions(urls);

            await Task.Run(async () => 
            {
                var ListImage_URL = img_List.absorptions(urls);
                foreach (var item in ListImage_URL) 
                {
                    MainThread.BeginInvokeOnMainThread(() => 
                    {
                        imgURL_Lists.Add(new urlText() { Imageurl = img_Url.ExtractHref_img(item) });
                    });
                    await Task.Delay(1000);
                }     
            });
            listimgViews.ItemsSource = imgURL_Lists;
            actlc.IsRunning = false;
        } 

        public class urlText { public string Imageurl { get; set; } }

    }
}