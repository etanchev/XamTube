using AIW.Repository;

using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace AIW.Droid.DownloadManager
{
    class UTXHelper<T>
    {


        private CompositDownloadObject compositDownloadObject { get; set; }
        private YoutubeClient youtubeClient;
        private MyStreamInfo<T> myStreamInfo;

        public UTXHelper(CompositDownloadObject compositDownloadObject) 
        {
            this.compositDownloadObject = compositDownloadObject;
            this.youtubeClient = new YoutubeClient();
            this.myStreamInfo = new MyStreamInfo<T>();
        }

        
        public async Task<MyStreamInfo<T>> GetAudioSreamInfo()
        {
            //network required
            
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(compositDownloadObject.DownloadModelProp.VideoId);
            //myStreamInfo.videoID = compositDownloadObject.DownloadModelProp.VideoId;
            var  videoInfo = await youtubeClient.Videos.GetAsync(compositDownloadObject.DownloadModelProp.VideoId);

            myStreamInfo.videoTitle = videoInfo.Title;
            myStreamInfo.StreamInfo = (T)streamManifest.GetAudioOnly().WithHighestBitrate();
           
            //myStreamInfo.stream = await youtubeClient.Videos.Streams.GetAsync(myStreamInfo.StreamInfo);

            return myStreamInfo;

        }

        public async Task<MyStreamInfo<T>> GetVideoSreamInfo()
        {

            //network required
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(compositDownloadObject.DownloadModelProp.VideoId);
            var videoInfo = await youtubeClient.Videos.GetAsync(compositDownloadObject.DownloadModelProp.VideoId);

            myStreamInfo.videoTitle = videoInfo.Title;
            myStreamInfo.StreamInfo = (T)streamManifest.GetMuxed().WithHighestVideoQuality();

            //myStreamInfo.stream = await youtubeClient.Videos.Streams.GetAsync(myStreamInfo.StreamInfo);

            return myStreamInfo;

        }
        
    }
 
}
