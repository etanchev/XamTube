using System;
using System.Collections.Generic;
using System.Text;

namespace AIW
{
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
       
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
           

        }




    }
}
