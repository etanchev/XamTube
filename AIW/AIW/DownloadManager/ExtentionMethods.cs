using System.IO;

namespace AIW.DownloadManager
{
    public static class ExtentionMethods
    {

        public static string GetSafeFilename(this string oldFileName)
        {
            
            return string.Join("", oldFileName.Split(Path.GetInvalidFileNameChars()));
        }

        public static string RemoveDotsFromFileNName(this string oldFileName)
        {

            return string.Join("", oldFileName.Split(new char[]  {'.'}));
        }


    }
}