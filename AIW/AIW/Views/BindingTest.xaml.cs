using AIW.ViewModels;
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
	public partial class BindingTest : ContentPage
	{
        public BindingTest()
        {
            InitializeComponent();

            BindingTest bindTest = new BindingTest();

            ComplatedViewModel complatedView = new ComplatedViewModel();

           // stackLayout.BindingContext = complatedView;

           

            //binding the Slider property called "Value"  to the TextProperty of the lable
            lable.SetBinding(Label.TextProperty, new Binding("Value"));
            lable.BindingContext = slider;

         
            frame.BindingContext = complatedView;



            //entry.SetBinding(Entry.TextProperty, new Binding("Value", BindingMode.OneWay));
            //entry.BindingContext = slider;

            //entry.SetBinding(Entry.TextProperty, new Binding("DownloadProgress", BindingMode.OneWay));
            //entry.BindingContext = downloadsViewModel;




            button.Clicked += (sender, arg) =>
            {
                complatedView.FrameColor = Color.FromRgb(new Random().Next(0,255), new Random().Next(0, 255),new Random().Next(0, 255));
            };






        }
	}
}