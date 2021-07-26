
using AIW.Repository;

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Printing.Workflow;

namespace AIW.UWP.DownloadManager
{
    public abstract class Downloader /*:IDisposable*/
    {


        public string FileName { get; set; }
        public string Directory { get; set; }
        // private string fileName;
        public string FileExtention { get; set; }

        public Downloader()
        {

        }
        public Downloader(string fileName, string directory, string extention)
        {
            FileName = fileName.GetSafeFilename();
            Directory = directory;
            FileExtention = extention;
            // this.fileName = fileName;
        }

        //public static string GetSafeFilename(this string filename) => string.Join("", filename.Split(Path.GetInvalidFileNameChars()));


        public bool FileExists()
        {
            return File.Exists(Path.Combine(Directory, FileName + "." + FileExtention));
        }

        public string CreateGUIDFileName()
        {
            return FileName + "-" + Guid.NewGuid() + "." + FileExtention;
        }

        public void RenameFile()
        {
            File.Move(Path.Combine(Directory, FileName), Path.Combine(Directory, FileName + "." + FileExtention));
        }

        public int GetFileBytesOnDisk()
        {

            var directoryFiles = new DirectoryInfo(Directory).EnumerateFiles();
            int range = 0;
            foreach (var file in directoryFiles)
            {

                if (FileName == file.Name)
                {

                    byte[] fileBytes = File.ReadAllBytes(file.FullName);
                    range = fileBytes.Length;
                    break;
                }
            }

            return range;

        }

        public abstract Task<bool> DownloadFile(IProgress<double> progress);

    }

    public class InitDownloader : Downloader
    {


        private string URL { get; set; }
        private CancellationTokenSource cancellationToken;


        public InitDownloader(string filename, string directory, string URL, CancellationTokenSource cancellationToken, string extention) : base(filename, directory, extention)
        {
            this.URL = URL;
            this.cancellationToken = cancellationToken;
        }

        public override async Task<bool> DownloadFile(IProgress<double> progress)
        {

            if (FileExists())
            {
                FileName = CreateGUIDFileName();
            }

            Stream remoteStream = null;
            FileStream localStream = null;
            WebResponse response = null;
            HttpWebRequest request;
            int contentsLenght;

            try
            {

                request = (HttpWebRequest)WebRequest.Create(URL);
                response = await request.GetResponseAsync();
                contentsLenght = Convert.ToInt32(response.Headers.Get("Content-Length"));
                remoteStream = response.GetResponseStream();

                localStream = new FileStream(Path.Combine(Directory, FileName), FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 4096, true);

                //set dynamic buffer size
                int b = 4096;
                byte[] buffer;
                int bytesRead;
                int currentProgress = 0;


                do
                {

                    buffer = new byte[b];
                    bytesRead = await remoteStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken.Token);

                    if (bytesRead == b) { b = b + 4096; } //dynamic buffer 
                    else { }
                    await localStream.WriteAsync(buffer, 0, bytesRead, cancellationToken.Token);

                    currentProgress += bytesRead;

                    progress.Report((double)currentProgress / (double)(contentsLenght));


                } while (bytesRead > 0);
                //localStream.Close();
                localStream.Dispose();


            }

            finally
            {
                if (localStream != null) localStream.Close();
                if (remoteStream != null) remoteStream.Close();
                if (response != null) response.Close();

            }

            RenameFile();
            return true;

        }


    }

    public class ResumeDownloader : Downloader
    {


        private string URL { get; set; }
        private CancellationTokenSource cancellationToken;

        public ResumeDownloader(string filename, string directory, string URL, CancellationTokenSource cancellationToken, string extention) : base(filename, directory, extention)
        {
            this.URL = URL;
           
            this.cancellationToken = cancellationToken;
        }


        public override async Task<bool> DownloadFile(IProgress<double> progress)
        {

            Stream remoteStream = null;
            FileStream localStream = null;
            WebResponse response = null;
            HttpWebRequest request;
            int contentsLenght;

            try
            {
                int range = GetFileBytesOnDisk();

                request = (HttpWebRequest)WebRequest.Create(URL);
                request.AddRange(range);
                response = await request.GetResponseAsync();
                contentsLenght = Convert.ToInt32(response.Headers.Get("Content-Length"));
                remoteStream = response.GetResponseStream();

                localStream = new FileStream(Path.Combine(Directory, FileName), FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);

                //set dynamic buffer size
                int b = 4096;
                byte[] buffer;
                int bytesRead;
                
                int currentProgress = range;

                do
                {
                    buffer = new byte[b];
                    bytesRead = await remoteStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken.Token);

                    if (bytesRead == b) { b = b + 4096; } //dynamic buffer 
                    else { }
                    await localStream.WriteAsync(buffer, 0, bytesRead, cancellationToken.Token);

                    currentProgress += bytesRead;

                    progress.Report((double)currentProgress / (double)(contentsLenght + range));

                } while (bytesRead > 0);
                //localStream.Close();
                localStream.Dispose();
            }
            finally
            {
                if (localStream != null) localStream.Close();
                if (remoteStream != null) remoteStream.Close();
                if (response != null) response.Close();

            }

            RenameFile();
            return true;

        }


       

    }
}










