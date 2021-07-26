
using AIW.Droid;
using Android.Content;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Diagnostics;
using AIW.CustomRenderer;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace AIW.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                Control.Click += Control_Click;
            }



            
        }

        private void Control_Click(object sender, EventArgs e)
        {
            Control.SetBackgroundColor(global::Android.Graphics.Color.Azure);
        }
    }
}