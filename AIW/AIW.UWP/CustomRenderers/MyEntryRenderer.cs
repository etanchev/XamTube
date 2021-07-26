using AIW.CustomRenderer;
using AIW.UWP;
using Windows.UI;
using UWPm = Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Color = Windows.UI.Color;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace AIW.UWP
{
    public class MyEntryRenderer : EntryRenderer 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new UWPm.SolidColorBrush(Colors.DarkSeaGreen);
                //Control.BackgroundFocusBrush = new SolidColorBrush(Colors.White);
                Control.BorderBrush = new UWPm.SolidColorBrush(Colors.DarkOliveGreen);
                Control.BackgroundFocusBrush = new UWPm.SolidColorBrush(Colors.Brown);
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(4);
                Control.CharacterSpacing = 4;
                Control.FlowDirection = Windows.UI.Xaml.FlowDirection.RightToLeft;
                Control.Foreground = new UWPm.SolidColorBrush(Colors.White);
                Control.Text = "UWP custom renderer";
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
               
               
               
                
                

            }

                       
            Control.Tapped += Control_Tapped;
            Control.TextChanged += Control_TextChanged;
        }

        private void Control_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {
            Control.Background = new UWPm.SolidColorBrush(Colors.DarkBlue);
            Control.BackgroundFocusBrush = new UWPm.SolidColorBrush(Colors.White);
            Control.BorderThickness = new Windows.UI.Xaml.Thickness(0.5);
        }

        private void Control_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Control.Background = new UWPm.SolidColorBrush(Colors.Brown);
            
            Control.BackgroundFocusBrush = new UWPm.SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            
        }
    }
}
