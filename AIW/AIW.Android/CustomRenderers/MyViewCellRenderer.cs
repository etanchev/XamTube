using AIW.CustomRenderer;
using AIW.UWP.CustomRenderers;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Views;
using System.ComponentModel;
using System.Runtime.Remoting.Contexts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using DroidListView = Android.Widget.ListView;

[assembly: ExportRenderer(typeof(MyViewCell), typeof(MyViewCellRenderer))]
namespace AIW.UWP.CustomRenderers
{
    class MyViewCellRenderer : ViewCellRenderer
    {

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Android.Content.Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);

            DroidListView listview = parent as DroidListView;

            if(listview != null)
            {
                //set viewcell selection collor


                listview.SetSelector(Android.Resource.Color.HoloOrangeLight);
               

            }



            return cell;
        }

        
      
    }
}
