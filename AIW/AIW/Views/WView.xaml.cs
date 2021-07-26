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
    public partial class WView : ContentPage
    {
        public WView()
        {
            InitializeComponent();

            
            webview.Source = new Uri("http://localhost/home/chat");
            butn.Clicked += (s, e) => { webview.Reload(); };
           
        }
    }
}