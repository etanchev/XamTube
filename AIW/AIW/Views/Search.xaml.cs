using AIW.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using AIW.ViewModels;
using System.Collections.ObjectModel;
using System.Net;
using System;
using Newtonsoft.Json;
using AIW.DependencyServices;
using SQLite;

namespace AIW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {

        SearchViewModel searchViewModel = new SearchViewModel();
        public string testvar = "";

        public Search()
        {
            InitializeComponent();

            //set binding viewmodel in the code 
            BindingContext = searchViewModel;

            //embeded image icon
            Image magIconEmbeded = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromResource("AIW.Images.mag_white_60x60.png", typeof(AIW.Search).GetTypeInfo().Assembly)
            };
            IconImageSource = magIconEmbeded.Source;

            // adding the image localy from AIW.UWP project
            //IconImageSource = "mag_white_60x60.png";

            
            listSearchResults.ItemTapped += async (sender, arg) =>
            {


                var result = await DisplayActionSheet("Action", "Cancel", null, "Download Audio", "Download Video", "Play Video");

                if (result == "Download Audio")
                {
                    var modelDownloads = CreateMesageObject(sender, arg, (int)StreamType.Audio);

                    MessagingCenter.Send(this, "Audio", modelDownloads);
                }
                if (result == "Download Video")
                {
                    var modelDownloads =  CreateMesageObject(sender, arg, (int)StreamType.Video);
                    MessagingCenter.Send(this, "Video", modelDownloads);
                }
                //if (result == "Play Video")
                //{
                //    //MessagingCenter.Send(this, "Play", modelDownloads);
                //}
            };

          
           

            //receing msg from android shared intent between apps

           MessagingCenter.Subscribe<string>(this, "FromAndroid", (msg) =>
           {
               searchBar.Text = msg;
               searchViewModel.SearchVideos();
           });

        }


       
        private  ModelDownloads CreateMesageObject(object sender,ItemTappedEventArgs arg, int streamType)
        {
            var item = (ModelSearchExtracted)arg.Item;
           

            return  new ModelDownloads()
            {
                ChanelId = item.ChanelId,
                ImageURL = item.ImageURL,
                Title = item.Title,
                VideoId = item.VideoId,
                StreamType = streamType,
            };

            
        }

       

        public enum StreamType { Video = 0, Audio = 1 }


       

    }
      
    
}
