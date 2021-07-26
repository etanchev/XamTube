using System;
using System.Collections.Generic;
using System.Text;

namespace AIW.Repository
{
    public class DownloadErrorEventArgs
    {
        public string Error="";
        public string ID = "";

        public DownloadErrorEventArgs(string error, string id) 
        {
            Error = error;
            ID = id;
        }
    }
}
