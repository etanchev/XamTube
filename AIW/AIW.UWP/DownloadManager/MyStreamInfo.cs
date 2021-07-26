using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Gaming.Input;
using YoutubeExplode.Videos.Streams;

namespace AIW.UWP.DownloadManager
{
    public class MyStreamInfo<T>
    {

        public string videoID { get; set; }
        public string videoTitle { get; set; }
        //public IVideoStreamInfo VideoStreamInfo { get; set; }
        public T StreamInfo { get; set; }
        //public IStreamInfo AusioStreamInfo { get; set; }
        //public IStreamInfo AusioStreamInfo { get; set; }
        public Stream stream { get; set; }

    }
}
