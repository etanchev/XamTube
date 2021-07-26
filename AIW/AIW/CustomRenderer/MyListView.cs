using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AIW.CustomRenderer
{
    public class MyListView : ListView
    {
        public MyListView()
        {
            //prevent native item selection from firing
            ItemSelected += delegate { SelectedItem = null; };
           
        }
    }
}
