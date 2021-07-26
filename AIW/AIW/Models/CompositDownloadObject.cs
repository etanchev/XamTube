using AIW.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AIW.Repository
{
    public class CompositDownloadObject 
    {
        public ModelDownloads DownloadModelProp { get; set; }
        public CancellationTokenSource DownloadCancellationTokenSource { get; set; }

       
    }
}
