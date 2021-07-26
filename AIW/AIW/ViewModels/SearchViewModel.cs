using AIW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AIW.ViewModels
{
    class SearchViewModel : INotifyPropertyChanged
    {
        public SearchViewModel()
        {
            SearchCommand = new Command(SearchVideos);
           
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ItemTapped { get; }
        public ICommand SearchCommand { get; }

        private ObservableCollection<ModelSearchExtracted> listViewSearchModel;

        public ObservableCollection<ModelSearchExtracted> ListViewSearchModel
        {
            set
            {
                if (listViewSearchModel != value)
                {
                    listViewSearchModel = value;
                    NotifyPropertyChanged();
                    
                }
            }
            get
            {
                return listViewSearchModel;
            }
        }

        private string searchText = "";
        public string SearchText
        {

            get => searchText;
            set
            {
                if (value != searchText)
                {
                    searchText = value;
                    NotifyPropertyChanged();
                }
            }
        }

       

        public async void SearchVideos()
        {

           

            string youtubeAPIkey = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=30&type=video&key=yourapikey&q=";

            ObservableCollection<ModelSearchExtracted> modelSearchJsonSimplifieds = new ObservableCollection<ModelSearchExtracted>() { };
            // SearchViewModel searchViewModel = new SearchViewModel();
            try
            {
                WebClient jsonRequest = new WebClient();
                

                string jsonString = await jsonRequest.DownloadStringTaskAsync(new Uri(youtubeAPIkey + SearchText));
                // continue after await return the result  
                ModelSearchJson.Root modelSearchJson = JsonConvert.DeserializeObject<ModelSearchJson.Root>(jsonString);

                foreach (var item in modelSearchJson.Items)
                {
                    modelSearchJsonSimplifieds.Add(new ModelSearchExtracted()
                    {
                        ChanelId = item.Snippet.ChannelId,
                        VideoId = item.Id.VideoId,
                        ImageURL = item.Snippet.Thumbnails.@Default.Url,
                        Title = item.Snippet.Title,

                    });

                }

                ListViewSearchModel = modelSearchJsonSimplifieds;

            }
            catch (Exception excep)
            {
                Console.WriteLine(excep.ToString());
                
            }
        }


    }
}
