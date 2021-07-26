using AIW.CustomRenderer;
using AIW.UWP;
using AIW.UWP.CustomRenderers;
using Windows.UI;
using UWPm = Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Color = Windows.UI.Color;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

[assembly: ExportRenderer(typeof(SearchBar), typeof(MySearchBarRenderer))]

namespace AIW.UWP.CustomRenderers
{
    public class MySearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                //e.NewElement.TextColor = new Xamarin.Forms.Color(); 
               
                Control.Background = new UWPm.SolidColorBrush(Colors.DarkSeaGreen);
                Control.FocusVisualPrimaryBrush = new UWPm.SolidColorBrush(Colors.Black);
                Control.FocusVisualPrimaryThickness = new Windows.UI.Xaml.Thickness(0);
                Control.FocusVisualSecondaryBrush = new UWPm.SolidColorBrush(Colors.Black);
                Control.BorderBrush = new UWPm.SolidColorBrush(Colors.DarkOliveGreen);

                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                Control.CharacterSpacing = 4;
                Control.FlowDirection = Windows.UI.Xaml.FlowDirection.RightToLeft;
                Control.Foreground = new UWPm.SolidColorBrush(Colors.Black);
                Control.PlaceholderText = "Type to search";

                Control.Opacity = 1;
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);


            }

        }
	//	private FormsTextBox _queryTextBox;

	//	protected override void UpdateBackgroundColor()
	//	{
	//		base.UpdateBackgroundColor();

	//		// Handle focus color
	//		T GetFirstDescendant<T>(DependencyObject element) where T : FrameworkElement
	//		{
	//			var count = VisualTreeHelper.GetChildrenCount(element);
	//			for (var i = 0; i < count; i++)
	//			{
	//				var child = VisualTreeHelper.GetChild(element, i);

	//				var target = child as T ?? GetFirstDescendant<T>(child);
	//				if (target != null)
	//					return target;
	//			}

	//			return null;
	//		}

	//		if (_queryTextBox == null)
	//			_queryTextBox = GetFirstDescendant<FormsTextBox>(Control);

	//		if (_queryTextBox == null)
	//			return;

	//		var backgroundColor = Element.BackgroundColor;

	//		if (!backgroundColor.IsDefault)
	//			_queryTextBox.BackgroundFocusBrush = _queryTextBox.Background;
	//		else
	//			_queryTextBox.ClearValue(FormsTextBox.BackgroundFocusBrushProperty);
	//	}
	}
}