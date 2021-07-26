using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using AIW.Models;
using System.Drawing;

namespace AIW.ViewModels
{
    class ComplatedViewModel : INotifyPropertyChanged
    {
        public ComplatedViewModel()
        {
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       

        private ObservableCollection<ModelComplatedDownloads> lVComplatedDownloads;

        public ObservableCollection<ModelComplatedDownloads> LVComplatedDownloads
        {
            set
            {
                if (lVComplatedDownloads != value)
                {
                    lVComplatedDownloads = value;
                    NotifyPropertyChanged();

                }
            }
            get
            {
                return lVComplatedDownloads;
            }
        }



        private Color frameColor = Color.Brown;

        public Color FrameColor
        {
            get { return frameColor; }
            set
            {
                if (value != frameColor)
                {
                    frameColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
