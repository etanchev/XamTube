using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AIW.iOS;
using Foundation;
using UIKit;


[assembly: Xamarin.Forms.Dependency(typeof(DownloadFileImplementation))]
namespace AIW.iOS
{
    class DownloadFileImplementation : IDownloader
    {
        public string videoID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        public event EventHandler<ProgressResultEventArgs> OnReportReceived;

        public void DownloadFile(string url, string folder)
        {
            throw new NotImplementedException();
        }

        public void DownloadFile(string url, int type)
        {
            throw new NotImplementedException();
        }
    }
}