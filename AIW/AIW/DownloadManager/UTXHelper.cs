using AIW.Repository;

using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace AIW.DownloadManager
{
    public class UTXHelper<T>
    {


        private CompositDownloadObject compositDownloadObject { get; set; }
        private readonly YoutubeClient youtubeClient;
        private readonly MyStreamInfo<T> myStreamInfo;

        public UTXHelper(CompositDownloadObject compositDownloadObject) 
        {
            this.compositDownloadObject = compositDownloadObject;
            youtubeClient = new YoutubeClient();
            myStreamInfo = new MyStreamInfo<T>();
        }

        
        public async Task<MyStreamInfo<T>> GetAudioSreamInfo()
        {
            
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(compositDownloadObject.DownloadModelProp.VideoId);
            var  videoInfo = await youtubeClient.Videos.GetAsync(compositDownloadObject.DownloadModelProp.VideoId);

            myStreamInfo.VideoTitle = videoInfo.Title;
            myStreamInfo.StreamInfo = (T)streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            return myStreamInfo;

        }

        public async Task<MyStreamInfo<T>> GetVideoSreamInfo()
        {

           
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(compositDownloadObject.DownloadModelProp.VideoId);
            var videoInfo = await youtubeClient.Videos.GetAsync(compositDownloadObject.DownloadModelProp.VideoId);

            myStreamInfo.VideoTitle = videoInfo.Title;
            myStreamInfo.StreamInfo = (T)streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

            return myStreamInfo;

        }
        
    }
 
}
