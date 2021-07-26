using AIW.Repository;
using System;
using System.Threading;

namespace AIW
{
    public interface IDownloader
    {
      

        
        void InitDownload(CompositDownloadObject compositDownloadObject, int type);
        void ResumeDownload(CompositDownloadObject compositDownloadObject, int type);

        event EventHandler<ProgressResultEventArgs> OnReportReceived;
        event EventHandler<DownloadErrorEventArgs> OnError;


    }
}
