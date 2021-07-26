using AIW.CustomRenderer;
using AIW.UWP.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;
using AIW;
using Windows.UI;
using UWPm = Windows.UI.Xaml.Media;

[assembly: ExportRenderer(typeof(TabbedPageStart), typeof(MyTabbedPageRenderer))]
namespace AIW.UWP.CustomRenderers
{
    class MyTabbedPageRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
             
            // var elem = e.NewElement;

            // Control.Background = new UWPm.SolidColorBrush(Colors.DarkSeaGreen);
            //elem.BackgroundColor = Color.White;
            //var elemN = e.OldElement;

        }
    }
}
