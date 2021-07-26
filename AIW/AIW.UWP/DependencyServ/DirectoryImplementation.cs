using AIW.DependencyServices;
using AIW.Models;
using AIW.Repository;
using AIW.UWP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;     

[assembly: Xamarin.Forms.Dependency(typeof(DirectoryImplementation))]
namespace AIW.UWP
{
    class DirectoryImplementation : IFileDirectory
    {
        readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //readonly public string extentions = ".mp4,.webm,.mp3,.avi, ";
        readonly public string[] extentions = { " ", ".mp4", ".webm", ".mp3", ".avi" };

        public void DeleteFile(string fileName)
        {
            if (File.Exists(directory + "\\" + fileName))
            {
                File.Delete(directory + "\\" + fileName);
            
            }
        }

      

        public string GetDirectory()
        {
            return directory;
        }

        private string GetFileType(string file)
        {
            
            FileInfo fileInfo = new FileInfo(file);
           
            if (fileInfo.Extension == ".mp4")
            {
                return "Video";
            }
            if (fileInfo.Extension == "")
            {
                return "Partial";
            }
            if (fileInfo.Extension == ".db3")
            {
                return "Partial";
            }
            if (fileInfo.Extension == ".webm")
            {
                return "Audio";
            }
            //else { return "Audio"; }

            return "Unknown";
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
                string ext = new FileInfo(file).Extension;

                if (extentions.Contains(ext))
                {
                    collectionToPass.Add(new ModelComplatedDownloads()
                    {
                        FileSizeMB = GetFileSize(file),
                        FileNameAndExt = GetFileNameAndExt(file),
                        FileType = GetFileType(file),



                    });
                }
              
                else
                {
                    collectionToPass.Add(new ModelComplatedDownloads()
                    {
                        FileSizeMB = GetFileSize(file),
                        FileNameAndExt = GetFileNameAndExt(file),
                        FileType = GetFileType(file),

                    });
                }
            }

            return await Task.FromResult(collectionToPass);
        }
    }
}