using AIW.CustomRenderer;
using AIW.UWP.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyListView),typeof(MyListViewRenderer))]
namespace AIW.UWP.CustomRenderers
{
    class MyListViewRenderer : ListViewRenderer
    {
       

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
               
            }
        }

    }
}
