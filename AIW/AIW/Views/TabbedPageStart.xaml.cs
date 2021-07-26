using AIW.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AIW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageStart : TabbedPage
    {
        public TabbedPageStart()
        {
            InitializeComponent();

            Title = "Xam Tube";
            BackgroundColor = Color.Black;
            BarBackgroundColor = Color.FromRgba(255,255,255,0.1);
            BarTextColor = Color.SaddleBrown;
            //SelectedTabColor = Color.Brown;
            BackgroundColor = Color.Black;
            //Padding = new Thickness(10,0.5,0.5,0.5);



            ToolbarItems.Add(new ToolbarItem("DownloadById", "*", Route, ToolbarItemOrder.Secondary));
            
            







            Page searchPage, downloadPage, /*bindingTest ,*/ complatedDownloads;

            searchPage = new Search();
            downloadPage = new Downloads();
            //bindingTest = new BindingTest();
            complatedDownloads = new ComplatedDownloads();

            Children.Add(searchPage);
            Children.Add(downloadPage);

            Children.Add(complatedDownloads);
            //Children.Add(bindingTest);




        }

       
        private void Route()
        {
           // throw new NotImplementedException();
        }

       
    }
}