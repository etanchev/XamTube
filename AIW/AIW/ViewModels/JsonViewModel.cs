using AIW.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace AIW.ViewModels
{
    class JsonViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ModelSearchJson.Item> listViewItems;
        ObservableCollection<ModelSearchJsonExtraction> listViewItemsExtraction;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ModelSearchJsonExtraction> ListViewItemsExtraction
        {
            set
            {
                if (listViewItemsExtraction != value)
                {
                    listViewItemsExtraction = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListViewItemsExtraction"));
                }
            }
            get
            {
                return listViewItemsExtraction;
            }
        }

        public ObservableCollection<ModelSearchJson.Item> ListViewItems
        {
            set
            {
                if (listViewItems != value)
                {
                    listViewItems = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListViewItems"));
                }
            }
            get
            {
                return listViewItems;
            }
        }

        public int translateX ; 

        public int TranslateX
        {
            get
            {
                return translateX;
            }
            set
            {
                if (translateX != value)
                {
                    translateX = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TranslateX"));
                }
            }
           
        }

        public Color backGroundColor;

        public Color BackGroundColor
        {
            get
            {
                return backGroundColor;
            }
            set
            {
                if (backGroundColor != value)
                {
                    backGroundColor = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackGroundColor"));
                }
            }

        }

    }
}
