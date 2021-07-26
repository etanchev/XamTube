using AIW.CustomRenderer;
using AIW.Droid;
using Android.Content;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Diagnostics;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Content.Res;
using System.Collections.Generic;
using AView = Android.Views.View;
using AViewGroup = Android.Views.ViewGroup;
using System.Linq;

[assembly: ExportRenderer(typeof(MySearchBar), typeof(MySearchBarRenderer))]
namespace AIW.Droid
{
    class MySearchBarRenderer : SearchBarRenderer
    {
        public MySearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //stting background color 
                Control.SetBackgroundColor(global::Android.Graphics.Color.Pink);

                
                // setting icon lence to brown collor
                var searchView = (Control as SearchView);
                var searchIconId = searchView.Resources.GetIdentifier("android:id/search_mag_icon", null, null);

                if (searchIconId > 0)
                {
                    var searchPlateIcon = searchView.FindViewById(searchIconId);
                    (searchPlateIcon as ImageView).SetColorFilter(Android.Graphics.Color.SandyBrown, PorterDuff.Mode.SrcIn);
                 
                }

                //box edit
                var editText = Control.GetChildrenOfType<EditText>().FirstOrDefault();
                if (editText != null)
                {
                    var shape = new ShapeDrawable(new RectShape());
                    shape.Paint.Color = Android.Graphics.Color.Transparent;
                    shape.Paint.StrokeWidth = 0;
                    shape.Paint.SetStyle(Paint.Style.Stroke);
                    editText.Background = shape;
                }

                var gradient = new GradientDrawable();
                gradient.SetCornerRadius(90.0f);
                int[][] states =
                {
                new[] {Android.Resource.Attribute.StateEnabled}, // enabled
                new[] {-Android.Resource.Attribute.StateEnabled} // disabled
                };
                int[] colors =
                {
                Xamarin.Forms.Color.SandyBrown.ToAndroid(),
                Xamarin.Forms.Color.SandyBrown.ToAndroid()
                };
                var stateList = new ColorStateList(states: states, colors: colors);
                gradient.SetStroke((int)this.Context.ToPixels(1.0f), stateList);
                this.Control.SetBackground(gradient);


                //var searchView = Control.FindViewById<ImageView>(Resource.Id.search_button);
                //var searchicon = Resources.GetDrawable(Resource.Drawable.search);
                //searchView.SetImageDrawable(searchicon);

            }

            Control.Click += Control_Click;
        }
       

        private void Control_Click(object sender, EventArgs e)
        {
            Control.SetBackgroundColor(global::Android.Graphics.Color.Black);
        }
    }
    internal static class ViewGroupExtensions
    {
        internal static IEnumerable<T> GetChildrenOfType<T>(this AViewGroup self) where T : AView
        {
            for (var i = 0; i < self.ChildCount; i++)
            {
                var child = self.GetChildAt(i);
                if (child is T typedChild)
                {
                    yield return typedChild;
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + typedChild.ToString());
                }
                if (!(child is AViewGroup)) continue;
                var myChildren = (child as AViewGroup).GetChildrenOfType<T>();
                foreach (var nextChild in myChildren)
                    yield return nextChild;
            }
        }

    }
}