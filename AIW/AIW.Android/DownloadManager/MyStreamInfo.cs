using System.IO;

namespace AIW.Droid.DownloadManager
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
