using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AIW.MyCustomControls
{
    class MyAlertDialog : View
    {


        

        public static readonly BindableProperty AreTransportControlsEnabledProperty =
          BindableProperty.Create(nameof(AreTransportControlsEnabled), typeof(bool), typeof(MyAlertDialog), true);

        public bool AreTransportControlsEnabled
        {
            set { SetValue(AreTransportControlsEnabledProperty, value); }
            get { return (bool)GetValue(AreTransportControlsEnabledProperty); }
        }
    }
}
