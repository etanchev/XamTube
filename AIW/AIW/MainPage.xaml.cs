using AIW.Models;
using AIW.ViewModels;
using AIW.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AIW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        // Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments).AbsolutePath;
       // Color myBlue = Color.FromHex("#254c66");
        SearchBar searchBar;
        ListView lv;
        Button download;

        JsonViewModel jsonViewModel = new JsonViewModel();




        public MainPage()
        {

            InitializeComponent();

            // Switch On/Off Navigation bar
            //  NavigationPage.SetHasNavigationBar(this, false);

            Title = "SEARCH";

            BackgroundColor = Color.Black;

            lv = new ListView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //SeparatorColor = myBlue,
                SelectionMode = ListViewSelectionMode.Single,
                //BackgroundColor = Color.Black,
                Margin = 0,
                RowHeight = 80,
                HasUnevenRows = false,


            };

            //bind listview items values to the ModelSearchJson Properties
            lv.SetBinding(ListView.ItemsSourceProperty, new Binding { Mode = BindingMode.OneWay, Path = "ListViewItemsExtraction", Source = jsonViewModel });
            //lv.SetBinding(ListView.ItemsSourceProperty, new Binding { Mode = BindingMode.OneWay, Path = "ListViewItems", Source = jsonViewModel });
            //set ul Tamplate view based on VideoSearchCell class
            lv.ItemTemplate = new DataTemplate(typeof(JsonViewCell));






            lv.ItemSelected +=  (sender, e) =>
            {

                try
                {
                    var item = (ModelSearchJsonExtraction)e.SelectedItem;
                    var senderObject = (ListView)sender;

                    //foreach (var itm in jsonViewModel.ListViewItemsExtraction)
                    //{
                    //    if (itm.Title == item.Title)
                    //    {

                    //        // VideoSearchCell.ViewCellTranslated = 10;
                    //        jsonViewModel.ListViewItemsExtraction.Remove(itm);
                    //        // int itemIndex = listItems.IndexOf(itm);
                    //        break;
                    //    }
                    //}
                }
                catch (Exception expe)
                {
                    Console.WriteLine(expe.ToString());
                }
                //var item = (ModelSearchJson.Item)e.SelectedItem;
                //var itemPage = new NewItemPage(item.snippet.title);
                //await DisplayAlert("Item click alert", item.snippet.title, "OK");
                //await Navigation.PushAsync(itemPage);
            };


            searchBar = new SearchBar
            {
                Placeholder = "Search For Videos",
                //PlaceholderColor = myBlue,
                Margin = 0,
                //SearchCommand = new Command(async () =>
                //{
                // 
                //}),
                FontSize = 15,
                //BackgroundColor = Color.DarkGray,
            };

            searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;



            download = new Button
            {
                Text = "Download",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //HeightRequest = 40,
                FontSize = 15,
                CornerRadius = 0,
                //BackgroundColor = Color.Black,
                //TextColor = myBlue
                //TextColor = Color.FromRgb(255.0, 255.0, 255.0),

            };

            download.Clicked +=  (sender, e) =>
            {
                // Navi.PushAsync(new Downloads());
                App.Current.MainPage = new Downloads();
            };


            Button cancel = new Button
            {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 40,
                FontSize = 15,
                CornerRadius = 0,
                //BackgroundColor = Color.Black,
                //TextColor = myBlue

                //VerticalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.End,
            };

            StackLayout buttonContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                //BackgroundColor = Color.Black,
                // HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {

                    download,
                    cancel
                },
            };


            ToolbarItem directory = new ToolbarItem
            {
                Text = "Directory",
            };
            ToolbarItem saved = new ToolbarItem
            {
                Text = "Saved",
            };
            ToolbarItems.Add(directory);
            ToolbarItems.Add(saved);

            directory.Order = ToolbarItemOrder.Secondary;
            saved.Order = ToolbarItemOrder.Secondary;

            directory.Clicked += Directory_Clicked;
            saved.Clicked += Saved_Clicked;



            Content = new StackLayout
            {
                //VerticalOptions = LayoutOptions.Start,
                Children = {
                    searchBar,
                    lv,
                    buttonContainer,
                    //new ScrollView
                    //{
                    //    //Content = resultsLabel,
                    //    VerticalOptions = LayoutOptions.FillAndExpand,
                    //}
                },

                // Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5)
            };

        }

        private void Saved_Clicked(object sender, EventArgs e)
        {

           //Navigation.PushAsync(new AboutPage());

        }

        private void Directory_Clicked(object sender, EventArgs e)
        {
            //Intent intent = new Intent(Intent.ActionSend);
            //intent.SetType("video/*");
            //intent.PutExtra("directory", Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            //Forms.Context.StartActivity(Intent.CreateChooser(intent, "Start App"));
        }


        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string youtubeAPIkey = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=30&type=video&key=AIzaSyDTCxULvKCL8sTlpE1gsL9bJfxB9Yj1Zks&q=";

            ObservableCollection<ModelSearchJsonExtraction> JsonExtractVideoList = new ObservableCollection<ModelSearchJsonExtraction>() { };

            try
            {
                WebClient jsonRequest = new WebClient();
                //async awaitable not blocking main tread while the download is running

                string jsonString = await jsonRequest.DownloadStringTaskAsync(new Uri(youtubeAPIkey + searchBar.Text));
                // continue after await return the result  


                ModelSearchJson.RootObject JsonRetrevedVideoList = JsonConvert.DeserializeObject<ModelSearchJson.RootObject>(jsonString);


                foreach (var item in JsonRetrevedVideoList.items)
                {
                    JsonExtractVideoList.Add(new ModelSearchJsonExtraction()
                    {
                        ChanelId = item.snippet.channelId,
                        VideoId = item.id.videoId,
                        ImageURL = item.snippet.thumbnails.@default.url,
                        Title = item.snippet.title,

                    });

                }

                foreach (var item in JsonExtractVideoList)
                {
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>" + item.VideoId + item.ImageURL);
                }

                //jsonViewModel.ListViewItems = new ObservableCollection<ModelSearchJson.Item>(/*mocklist */JsonRetrevedVideoList.items);
                jsonViewModel.ListViewItemsExtraction = new ObservableCollection<ModelSearchJsonExtraction>(JsonExtractVideoList);

                //in order to manipulate list view items u need a list of type observable collection assigne to ItemSource.
                //jsonViewModel.ListViewItems = jsonViewModel.ListViewItems;


                lv.ItemsSource = jsonViewModel.ListViewItemsExtraction;
                //lv.ItemsSource = jsonViewModel.ListViewItems ;
            }
            catch (Exception excep)
            {
                Console.WriteLine(excep.ToString());
            }


        }

        ObservableCollection<ModelSearchJson.Item> mocklist = new ObservableCollection<ModelSearchJson.Item>()
        {
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID1"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 1",
                    title = "VIdeo title 1",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID2"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 2",
                    title = "VIdeo title 2",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID3"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 3",
                    title = "VIdeo title 3",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID4"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 4",
                    title = "VIdeo title 4",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID5"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 5",
                    title = "VIdeo title 5",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID6"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 6",
                    title = "VIdeo title 6",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID7"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 7",
                    title = "VIdeo title 7",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID8"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 8",
                    title = "VIdeo title 8",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID9"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 9",
                    title = "VIdeo title 9",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID10"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 10",
                    title = "VIdeo title 10",
                }

            },
            new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID10"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 10",
                    title = "VIdeo title 10",
                }

            },
             new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID11"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 11",
                    title = "VIdeo title 11",
                }

            },
              new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID12"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 12",
                    title = "VIdeo title 12",
                }

            },
               new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID13"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 13",
                    title = "VIdeo title 13",
                }

            },
                new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID14"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 14",
                    title = "VIdeo title 14",
                }

            },
                 new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID15"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 15",
                    title = "VIdeo title 15",
                }

            },
                  new ModelSearchJson.Item(  ){  id = new ModelSearchJson.Id(){ videoId = "VideoID16"  },  snippet = new ModelSearchJson.Snippet()          {
                    channelId = "Channnell ID 16",
                    title = "VIdeo title 16",
                }

                  },




        };





    }
}
