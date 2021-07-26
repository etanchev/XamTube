using Xamarin.Forms;
using AIW.CustomRenderer;
using AIW.Droid.CutosmRenderers;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(MyListView), typeof(MyListViewRenderer))]
namespace AIW.Droid.CutosmRenderers
{
    public class MyListViewRenderer : ListViewRenderer
    {
        public MyListViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {

            base.OnElementChanged(e);

            Control.SetSelector(Android.Resource.Color.Transparent);

            //Control.ItemSelected += Control_ItemSelected;
            Element.ItemSelected += Element_ItemSelected;         

            //Control.NothingSelected += Control_ItemSelectionCleared;

          
        }

        private void Element_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Control.SetSelector(Android.Resource.Color.Transparent);
            //Control.SetBackground(GetRect());
            //Control.GetChildAt(e.SelectedItemIndex).Background = GetRect();
        }

        private Drawable GetRect()
        {
            //frame for list view with round corners 
            RectF rectF = new RectF(10, 10, 10, 10);
            float[] outer = new float[] { 90, 90, 90, 90, 90, 90, 90, 90 };
            float[] inner = new float[] { 90, 90, 90, 90, 90, 90, 90, 90 };
            ShapeDrawable roundRect = new ShapeDrawable(new RoundRectShape(outer, rectF, inner)); ;
            roundRect.Paint.Color = Color.Brown;
            roundRect.Paint.StrokeWidth = 5;
            roundRect.Paint.SetStyle(Paint.Style.FillAndStroke);

            return roundRect;
        }

        private void Control_ItemSelectionCleared(object sender, System.EventArgs e)
        {
            Control.SetSelector(Android.Resource.Color.HoloPurple);
        }

        private void Control_ItemSelected(object sender, Android.Widget.AdapterView.ItemSelectedEventArgs e)
        {
            //Control.SetSelector(Android.Resource.Color.Transparent);
            ////Control.SetBackground(GetRect());
            //Control.GetChildAt(e.Position).Background = GetRect();
        }

    }
    
}