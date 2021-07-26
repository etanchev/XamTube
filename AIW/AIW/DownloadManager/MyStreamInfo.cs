using System.IO;

namespace AIW.DownloadManager
{
    public class MyStreamInfo<T>
    {

        public string VideoID { get; set; }
        public string VideoTitle { get; set; }
        public T StreamInfo { get; set; }
        public Stream Stream { get; set; }

    }
}
