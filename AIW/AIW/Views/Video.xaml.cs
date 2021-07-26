using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AIW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Video : ContentPage
    {
        public Video(string file)
        {
            InitializeComponent();

            BackgroundColor = Color.Black;

            this.Animate("MyAnim", new Animation(), 16, 250, Easing.SpringOut, null, null);
            NavigationPage.SetHasNavigationBar(this, false);
          

            if (!string.IsNullOrWhiteSpace(file))
            {
                videoPlayer.Source = new FileVideoSource
                {
                    File = file,

                };
            }
         
        }
    }
}