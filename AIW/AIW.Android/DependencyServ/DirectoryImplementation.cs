using AIW.DependencyServices;
using AIW.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using AIW.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(DirectoryImplementation))]

namespace AIW.Droid
{
    class DirectoryImplementation : Java.Lang.Object, IFileDirectory
    {

        readonly string directory = Android.OS.Environment.ExternalStorageDirectory + "/MyUTD/";
        readonly public string[] audioFile = { ".webm",".mp3," };
        readonly public string[] videoFile = { ".mp4",".avi" };
        readonly public string[] noExt = {" "};
        readonly public string[] dbFile = { ".db3" };




        public async void DeleteFile(string fileName)
        {
            if (File.Exists(directory + fileName))
            {
                File.Delete(directory + fileName);
                _ = await EnumerateFilesAsync();
            }
        }

        public string GetDirectory()
        {
            return directory;
        }


        private string GetFileType(string file)
        {
            var ext = new FileInfo(file).Extension;

            if (videoFile.Contains(ext))
            {
                return "Video";
            }
             if  (audioFile.Contains(ext))
            {
                return "Audio";
            }
             if (dbFile.Contains(ext))
            {
                return "DataBase";

            }
            if (dbFile.Contains(ext))
            {
                return "Partial";

            }

            return "unkown";

           

        }

        private string GetFileNameAndExt(string file)
        {
            FileInfo fileInfo = new FileInfo(file);

            return fileInfo.Name;
        }

        private double GetFileSize(string file)
        {
            FileInfo fileInfo = new FileInfo(file);

            var bytes = fileInfo.Length / 1024.00;



            return Math.Round(bytes / 1024, 2);
        }



        public async Task<ObservableCollection<ModelComplatedDownloads>> EnumerateFilesAsync()
        {

            IEnumerable<string> fileList = Directory.EnumerateFiles(directory);
            ObservableCollection<ModelComplatedDownloads> collectionToPass = new ObservableCollection<ModelComplatedDownloads>();

            foreach (var file in fileList)
            {
               
                    collectionToPass.Add(new ModelComplatedDownloads()
                    {
                        FileSizeMB = GetFileSize(file),
                        FileNameAndExt = GetFileNameAndExt(file),
                        FileType = GetFileType(file),



                    });
                
            }

            return await  Task.FromResult(collectionToPass);
        }
    }
}