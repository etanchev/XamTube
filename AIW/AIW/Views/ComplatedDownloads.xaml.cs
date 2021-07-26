using AIW.CustomRenderer;
using AIW.DependencyServices;
using AIW.Models;
using AIW.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using System.Threading.Tasks;
using FormsVideoLibrary;
using AIW.Views;
using System.Linq;


namespace AIW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComplatedDownloads : ContentPage
    {

        //ObservableCollection<ModelComplatedDownloads> complatedDownloads = new ObservableCollection<ModelComplatedDownloads>() { };

         
        public ComplatedDownloads()
        {
            InitializeComponent();

            //instantiate the view model for the page
            ComplatedViewModel complatedViewModel = new ComplatedViewModel();

            BackgroundColor = Color.Black;

            //global binding for the whole contents in the page
            BindingContext = complatedViewModel;

            
           // complatedViewModel.FrameColor = Color.Pink;

            var tabIconEmbeded = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromResource("AIW.Images.iconComplated_60x60.png", typeof(AIW.Search).GetTypeInfo().Assembly)
            };
            IconImageSource = tabIconEmbeded.Source;

            listComplatedDownloads.IsPullToRefreshEnabled = true;
            listComplatedDownloads.Refreshing +=  ListComplatedDownloads_Refreshing;
            listComplatedDownloads.RefreshControlColor = Color.DodgerBlue;

            listComplatedDownloads.ItemTapped += async (sender, arg) =>
            {
                var itemModel = (ModelComplatedDownloads)arg.Item;
                var result = await DisplayActionSheet("Action", "Cancel", null, "Play", "Delete");

                if (result == "Play")
                {
                    // MessagingCenter.Send<string>(itemModel.FileNameAndExt, "Share");
                    string dir = DependencyService.Get<IFileDirectory>().GetDirectory();
                    string filename = "";

                    if (Device.Android == Device.RuntimePlatform)
                    {
                        filename = dir + itemModel.FileNameAndExt;
                    }
                    if (Device.UWP == Device.RuntimePlatform)
                    {
                        filename = dir + "\\" + itemModel.FileNameAndExt;
                    }

                    if (!string.IsNullOrWhiteSpace(filename))
                    {
                        
                        await Navigation.PushAsync(new Video(filename));
                      
                    }
                }
                if (result == "Delete")
                {
                    var res = await DisplayAlert("Alert", "Are you sure", "Ok", "Cancel");
                    if (res)
                    {
                        DependencyService.Get<IFileDirectory>().DeleteFile(itemModel.FileNameAndExt);
                        listComplatedDownloads.ItemsSource = await DependencyService.Get<IFileDirectory>().EnumerateFilesAsync();
                    }
                   
                }

            };


           
            
        }

       

        private async void ListComplatedDownloads_Refreshing(object sender, EventArgs e)
        {
            listComplatedDownloads.ItemsSource = await DependencyService.Get<IFileDirectory>().EnumerateFilesAsync();
           
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
           
            var list = await DependencyService.Get<IFileDirectory>().EnumerateFilesAsync();
          
            listComplatedDownloads.ItemsSource = list;
        }

      

        private void FrameTapped(object sender, EventArgs e)
        {

            var item = (Frame)sender;

            if (item != null) 
            {

                item.BackgroundColor = Color.Brown;

            }
        }
    }
}


   
