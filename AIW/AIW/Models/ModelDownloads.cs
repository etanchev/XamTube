using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AIW.Models
{
    public class ModelDownloads : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string VideoId { get; set; }
        public string ChanelId { get; set; }
        public string ImageURL { get; set; }
        public int StreamType { get; set; }
       

        private double downloadProgress = 0;
        private double progressDownloadPercentage = 0;
        private string error = "";
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error
        {
            get => error;
            set
            {
                if (value != error)
                {
                    error = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double DownloadProgress
        {

            get => downloadProgress;
            set
            {
                if (value != downloadProgress)
                {
                    downloadProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double ProgressDownloadPercentage
        {
            get { return progressDownloadPercentage; }
            set
            {
                if (value != progressDownloadPercentage)
                {
                    progressDownloadPercentage = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
