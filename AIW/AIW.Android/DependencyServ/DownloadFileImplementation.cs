using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AIW.Droid;
using AIW.DownloadManager;
using AIW.Repository;
using Newtonsoft.Json;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

[assembly: Xamarin.Forms.Dependency(typeof(DownloadFileImplementation))]
namespace AIW.Droid
{
    public class DownloadFileImplementation : Java.Lang.Object, IDownloader
    {
        public DownloadFileImplementation() { }

        readonly string directory = Android.OS.Environment.ExternalStorageDirectory + "/MyUTD/";

        public event EventHandler<ProgressResultEventArgs> OnReportReceived;
        public event EventHandler<DownloadErrorEventArgs> OnError;
        private Downloader dl;


        private enum TypeForm : int
        {
            audio = 1,
            video = 0,
        }

        public async void InitDownload(CompositDownloadObject compositDownloadObject, int type)
        {

           



            //video type
            if (type == (int)TypeForm.video)
            {
                MyStreamInfo<IVideoStreamInfo> myStreamInfo;

                try
                {
                    UTXHelper<IVideoStreamInfo> uTXHelper = new UTXHelper<IVideoStreamInfo>(compositDownloadObject);
                    myStreamInfo = await uTXHelper.GetVideoSreamInfo();
                    dl = new InitDownloader(
                          myStreamInfo.VideoTitle,
                          directory,
                          myStreamInfo.StreamInfo.Url,
                          compositDownloadObject.DownloadCancellationTokenSource,
                          myStreamInfo.StreamInfo.Container.Name);
                }
                catch (Exception)
                {
                    OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error: (UTDX) ", compositDownloadObject.DownloadModelProp.VideoId)); ;
                    return;
                }

            }
            //video type
            else
            {
                MyStreamInfo<IStreamInfo> myStreamInfo = new MyStreamInfo<IStreamInfo>();
                try
                {
                    UTXHelper<IStreamInfo> uTXHelper = new UTXHelper<IStreamInfo>(compositDownloadObject);

                    myStreamInfo = await uTXHelper.GetAudioSreamInfo();
                    dl = new InitDownloader(
                           myStreamInfo.VideoTitle,
                           directory,
                           myStreamInfo.StreamInfo.Url,
                           compositDownloadObject.DownloadCancellationTokenSource,
                           myStreamInfo.StreamInfo.Container.Name);
                }
                catch (Exception)
                {
                    OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error: (UTDX) ", compositDownloadObject.DownloadModelProp.VideoId)); ;
                    return;
                }
            }




            try
            {

                await dl.DownloadFile(new Progress<double>((progresss) => {
                    OnReportReceived?.Invoke(this, new ProgressResultEventArgs(progresss, compositDownloadObject.DownloadModelProp.VideoId));
                } ));
            }
            catch (System.OperationCanceledException)
            {
                OnError?.Invoke(this, new DownloadErrorEventArgs("Download canceled!", compositDownloadObject.DownloadModelProp.VideoId));
            }
            catch (WebException e)
            {
                OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error: " + e.Message, compositDownloadObject.DownloadModelProp.VideoId)); ;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error:" + ex.Message, compositDownloadObject.DownloadModelProp.VideoId)); ;
            }

        }

        public async void ResumeDownload(CompositDownloadObject compositDownloadObject, int type)
        {


          

            OnError?.Invoke(this, new DownloadErrorEventArgs("", compositDownloadObject.DownloadModelProp.VideoId)); ;

            if (type == 0)
            {
                MyStreamInfo<IVideoStreamInfo> myStreamInfo;

                try
                {
                    UTXHelper<IVideoStreamInfo> uTXHelper = new UTXHelper<IVideoStreamInfo>(compositDownloadObject);
                    myStreamInfo = await uTXHelper.GetVideoSreamInfo();

                    dl = new ResumeDownloader(
                          myStreamInfo.VideoTitle,
                          directory,
                          myStreamInfo.StreamInfo.Url,
                          compositDownloadObject.DownloadCancellationTokenSource,
                          myStreamInfo.StreamInfo.Container.Name);

                }
                catch (Exception)
                {
                    OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error: (UTDX) ", compositDownloadObject.DownloadModelProp.VideoId)); ;
                    return;
                }

            }
            else
            {
                MyStreamInfo<IStreamInfo> myStreamInfo;

                try
                {
                    UTXHelper<IStreamInfo> uTXHelper = new UTXHelper<IStreamInfo>(compositDownloadObject);
                    myStreamInfo = await uTXHelper.GetAudioSreamInfo();

                    dl = new ResumeDownloader(
                          myStreamInfo.VideoTitle,
                          directory,
                          myStreamInfo.StreamInfo.Url,
                          compositDownloadObject.DownloadCancellationTokenSource,
                          myStreamInfo.StreamInfo.Container.Name);


                }
                catch (Exception)
                {
                    OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error: (UTDX)", compositDownloadObject.DownloadModelProp.VideoId)); ;
                    return;
                }
            }



            try
            {
                //var downloadSuccess = await dl.DownloadFile(new Progress<double>(ResumeDownloadProgress));
                 await dl.DownloadFile(new Progress<double>((progresss) => {
                    OnReportReceived?.Invoke(this, new ProgressResultEventArgs(progresss, compositDownloadObject.DownloadModelProp.VideoId));
                }));
            }
            catch (System.OperationCanceledException)
            {
                OnError?.Invoke(this, new DownloadErrorEventArgs("Download canceled!", compositDownloadObject.DownloadModelProp.VideoId));
            }
            catch (WebException e)
            {
                OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error: " + e.Message, compositDownloadObject.DownloadModelProp.VideoId));
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new DownloadErrorEventArgs("Download Error:" + ex.Message, compositDownloadObject.DownloadModelProp.VideoId));
            }

        }

        //private void InitDownloadProgress(double progresss)
        //{
        //    OnReportReceived?.Invoke(this, new ProgressResultEventArgs(progresss, compositDownloadObject.DownloadModelProp.VideoId));
        //}

        //private void ResumeDownloadProgress(double progresss)
        //{
        //    OnReportReceived?.Invoke(this, new ProgressResultEventArgs(progresss, compositDownloadObject.DownloadModelProp.VideoId));
        //}
    }
}