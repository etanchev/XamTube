using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AIW.Models
{
    public class ModelDatabaseDownloads
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string ChannelID { get; set; }
        public string ImageURL { get; set; }
        public int StreamType { get; set; }
        public double DownloadProgress { get; set; }
        public double ProgressDownloadPercentage { get; set; }
    }
}
