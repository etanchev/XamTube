using System.IO;
namespace AIW.Droid.DownloadManager
{
    public static class ExtentionMethods
    {

        public static string GetSafeFilename(this string oldFileName)
        {
            
            return string.Join("", oldFileName.Split(Path.GetInvalidFileNameChars()));
        }
       

       
    }
}