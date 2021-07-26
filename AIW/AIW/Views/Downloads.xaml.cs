using AIW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;

using System.Threading.Tasks;
using AIW.Repository;
using AIW.ViewModels;
using Xamarin.Essentials;
using System.Reflection;
using System.Linq;

public delegate void InputDelegate(string directory);
public delegate void DownloadFileDelegte(int type, string itemClickedVideoID);

namespace AIW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Downloads : ContentPage
    {

        
        ObservableCollection<ModelDownloads> listModelDownloads = new ObservableCollection<ModelDownloads>();
        Dictionary<string, CompositDownloadObject> keyValuePairs = new Dictionary<string, CompositDownloadObject>();
        List<ModelDatabaseDownloads> listDatabaseDownloads = new List<ModelDatabaseDownloads>();
       
       

        public string VideoId { get; set; }
        

        public Downloads()
        {
            InitializeComponent();
            Title = "DOWNLOAD";
            BackgroundColor = Color.Black;
           


            var magIconEmbeded = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromResource("AIW.Images.iconDownload60x60.png", typeof(AIW.Downloads).GetTypeInfo().Assembly)
            };
            IconImageSource = magIconEmbeded.Source;

            //myImageButton.Source = magIconEmbeded.Source;

            LoadDataBaseVideos();
           
            listDownloads.ItemsSource = listModelDownloads;
           


            MessagingCenter.Subscribe<Search, ModelDownloads>(this, "Audio", (sender, audioItem) => {

                SubscribeDownload(audioItem);
            });

            MessagingCenter.Subscribe<Search, ModelDownloads>(this, "Video", (sender, videoItem) => {

                SubscribeDownload(videoItem);
            });

            MessagingCenter.Subscribe<Search, ModelDownloads>(this, "Play", (sender, arg) => {
               // listModelDownloads.Add(arg);
                //Debug.WriteLine("record inserted");
            });


           
           
            listDownloads.ItemTapped += ListDownloads_ItemTapped;
            
            eraseButton.Clicked +=  async (sender, arg) =>
            {
                //var result = await DisplayActionSheet("Action", "Cancel", null, "Download Audio", "Download Video", "Play Video");

               var res = await  DisplayAlert("Alert", "Are you sure","Ok", "Cancel");
                if(res == true) {
                    await App.Database.DelateAllDatabaseRecords();
                    listModelDownloads.Clear();
                    keyValuePairs.Clear();
                }
                else
                {

                }
              
            };
        }

        public async void SubscribeDownload(ModelDownloads modelDownload)
        {
            CompositDownloadObject compositDownloadObject = new CompositDownloadObject()
            {
                DownloadModelProp = modelDownload,
                DownloadCancellationTokenSource = new CancellationTokenSource(),
            };


            if (keyValuePairs.ContainsKey(compositDownloadObject.DownloadModelProp.VideoId))
            {
                await DisplayAlert("", "Item already added", "Close");

            }
            else
            {
                //add new download to the top of the list
                listModelDownloads.Insert(0, compositDownloadObject.DownloadModelProp);
                keyValuePairs.Add(compositDownloadObject.DownloadModelProp.VideoId, compositDownloadObject);
            }

            DependencyService.Get<IDownloader>().InitDownload(compositDownloadObject, compositDownloadObject.DownloadModelProp.StreamType);
            DependencyService.Get<IDownloader>().OnReportReceived += Downloads_OnReportReceived;
            DependencyService.Get<IDownloader>().OnError += Downloads_OnError;
        }

        private async void ListDownloads_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var dowwloadItem = (ModelDownloads)e.Item;
            var result = await DisplayActionSheet("Action", "Cancel", null,"Copy URL", "Resume Download","Cancel download", "Clear record");


            if (result == "Resume Download")
            {
                foreach (var downloadItem in keyValuePairs)
                {
                    if (downloadItem.Key == dowwloadItem.VideoId)
                    {
                        DependencyService.Get<IDownloader>().ResumeDownload(new CompositDownloadObject() { DownloadModelProp = downloadItem.Value.DownloadModelProp, DownloadCancellationTokenSource = downloadItem.Value.DownloadCancellationTokenSource }, downloadItem.Value.DownloadModelProp.StreamType);
                        DependencyService.Get<IDownloader>().OnReportReceived += Downloads_OnReportReceived;
                        DependencyService.Get<IDownloader>().OnError += Downloads_OnError;
                        
                    }
                }
              
            }
            if (result == "Cancel download")
            {

                CancelDownload(dowwloadItem.VideoId);
               
            }
            if (result == "Clear record")
            {
                foreach (var itemToRemove in listModelDownloads)
                {
                    if (itemToRemove.VideoId == dowwloadItem.VideoId)
                    {

                        //cancel the download
                        CancelDownload(dowwloadItem.VideoId);
                        //remmove from dictionary
                        keyValuePairs.Remove(itemToRemove.VideoId);
                        //eraze from the listview
                        listModelDownloads.Remove(itemToRemove);
                       
                        await App.Database.DeleteDownloadsDatabaseAsync(new ModelDatabaseDownloads()
                        {
                            ID = itemToRemove.ID,
                            ChannelID = itemToRemove.ChanelId,
                            Title = itemToRemove.Title,
                            ImageURL = itemToRemove.ImageURL,
                            VideoId = itemToRemove.VideoId,
                            StreamType = itemToRemove.StreamType,
                        });
                        break;
                    }
                }
            }
            if (result == "Copy URL")
            {
                //var cacheDir = FileSystem.CacheDirectory;
                //var mainDir = FileSystem.AppDataDirectory;
                //Debug.WriteLine(cacheDir);
                //Debug.WriteLine(mainDir);
                //var streamfinfo = await GetStreamEndPointURL(VideoInfo, 0);
                //await Clipboard.SetTextAsync(streamfinfo.Url);
            }
            else { }
        }

        public  void CancelDownload(string downloadVideoId)
        {
            foreach (var downloadItem in keyValuePairs)
            {
                if (downloadItem.Key == downloadVideoId)
                {

                    downloadItem.Value.DownloadCancellationTokenSource.Cancel();
                    downloadItem.Value.DownloadCancellationTokenSource = new CancellationTokenSource();
                   
                    break;
                }
            }
        }

        private async void Downloads_OnError(object sender, DownloadErrorEventArgs e)
        {
            foreach (var downloadItem in keyValuePairs)
            {
                if (downloadItem.Key == e.ID)
                {

                    //update the database with the progress of the download
                    await App.Database.CreatOrUpdateRecord(new ModelDatabaseDownloads()
                    {
                        //ID = downloadItem.Value.DownloadModelProp.ID,
                        ChannelID = downloadItem.Value.DownloadModelProp.ChanelId,
                        ImageURL = downloadItem.Value.DownloadModelProp.ImageURL,
                        Title = downloadItem.Value.DownloadModelProp.Title,
                        VideoId = downloadItem.Value.DownloadModelProp.VideoId,
                        StreamType = downloadItem.Value.DownloadModelProp.StreamType,
                        DownloadProgress = downloadItem.Value.DownloadModelProp.DownloadProgress,
                        ProgressDownloadPercentage = Math.Round(downloadItem.Value.DownloadModelProp.DownloadProgress * 100, 0),

                    });

 
                     downloadItem.Value.DownloadModelProp.Error = e.Error;
                    break;

                }
            }
        }

        private async void Downloads_OnReportReceived(object sender, ProgressResultEventArgs e)
        {
            foreach(var downloadItem in keyValuePairs)
            {
                if(downloadItem.Key == e.ID)
                {
                    downloadItem.Value.DownloadModelProp.DownloadProgress = e.Progress; //progress bar 
                    downloadItem.Value.DownloadModelProp.ProgressDownloadPercentage = Math.Round(e.Progress * 100,0); //progress in percentage
                   
                    if (Math.Round(e.Progress * 100, 0) == 100)
                    {
                        //await App.Database.SaveDownloadsDatabaseAsync(new ModelDatabaseDownloads()
                        //{
                        //    ID = downloadItem.Value.DownloadModelProp.ID,
                        //    ChannelID = downloadItem.Value.DownloadModelProp.ChanelId,
                        //    ImageURL = downloadItem.Value.DownloadModelProp.ImageURL,
                        //    Title = downloadItem.Value.DownloadModelProp.Title,
                        //    VideoId = downloadItem.Value.DownloadModelProp.VideoId,
                        //    StreamType = downloadItem.Value.DownloadModelProp.StreamType,
                        //    DownloadProgress = downloadItem.Value.DownloadModelProp.DownloadProgress,
                        //    ProgressDownloadPercentage = 100,

                        //});

                        //remove from dictionary
                        keyValuePairs.Remove(downloadItem.Key);
                        //eraze from the listview
                        listModelDownloads.Remove(downloadItem.Value.DownloadModelProp);
                        //deate from database 
                        await App.Database.DeleteDownloadsDatabaseAsync(new ModelDatabaseDownloads()
                        {
                            ID = downloadItem.Value.DownloadModelProp.ID,
                            ChannelID = downloadItem.Value.DownloadModelProp.ChanelId,
                            ImageURL = downloadItem.Value.DownloadModelProp.ImageURL,
                            Title = downloadItem.Value.DownloadModelProp.Title,
                            VideoId = downloadItem.Value.DownloadModelProp.VideoId,
                            StreamType = downloadItem.Value.DownloadModelProp.StreamType,

                        });

                       // await DisplayAlert("", "Video download", "Close");
                        break;
                        
                    }
                }
            }
        }

        protected override  void OnAppearing()
        {

           // LoadDataBaseVideos();

        }

        public async void LoadDataBaseVideos()
        {
           

            listDatabaseDownloads = await App.Database.GetDownloadsDatabaseAsync();

            ObservableCollection<CompositDownloadObject> compositDownloadObjectsDatabase = new ObservableCollection<CompositDownloadObject>() { };

            
            //add all db enties into composite downloa object
            foreach (var databaseItem in listDatabaseDownloads)
            {

                compositDownloadObjectsDatabase.Add(new CompositDownloadObject()
                {
                    DownloadModelProp = new ModelDownloads()
                    {
                        ID = databaseItem.ID,
                        ChanelId = databaseItem.ChannelID,
                        ImageURL = databaseItem.ImageURL,
                        Title = databaseItem.Title,
                        VideoId = databaseItem.VideoId,
                        StreamType = databaseItem.StreamType,
                        DownloadProgress = databaseItem.DownloadProgress,
                        ProgressDownloadPercentage = databaseItem.ProgressDownloadPercentage,
                        

                    },
                    DownloadCancellationTokenSource = new CancellationTokenSource(),
                });
            }


            //add all entries in ModelDatabase object and in the dictionary
            foreach (var item in compositDownloadObjectsDatabase)
            {

                if (!keyValuePairs.ContainsKey(item.DownloadModelProp.VideoId)) {
                    listModelDownloads.Add(item.DownloadModelProp); //instantly updated becasue of listview type Observable collection
                    keyValuePairs.Add(item.DownloadModelProp.VideoId, item);
                }
               
            }

            //listDownloads.ItemsSource = listModelDownloads;
        }

       

       
        public enum StreamType { Video = 0, Audio = 1 }
    }
}