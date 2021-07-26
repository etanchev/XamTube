using AIW.CustomRenderer;
using AIW.UWP;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using Color = Windows.UI.Color;

[assembly: ExportRenderer(typeof(MyButton), typeof(MyEntryRenderer))]
namespace AIW.UWP.CustomRenderers
{
    class MyButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            //Control.Background = new SolidColorBrush(Colors.Red);
            //Control.BackgroundColor = new SolidColorBrush(Colors.Red);
            //Control.BorderBrush = new SolidColorBrush(Colors.Yellow);
            //Control.

        }
    }
}
