using AIW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AIW.ViewModels
{
    class DownloadsViewModel : INotifyPropertyChanged
    {
       

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
