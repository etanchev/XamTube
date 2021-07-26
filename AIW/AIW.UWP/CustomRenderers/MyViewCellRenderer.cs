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

[assembly: ExportRenderer(typeof(MyViewCell), typeof(MyViewCellRenderer))]

namespace AIW.UWP.CustomRenderers
{
    class MyViewCellRenderer : ViewCellRenderer
    {

    }
}
