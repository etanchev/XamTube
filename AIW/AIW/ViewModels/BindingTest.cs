using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIW.ViewModels
{
    public class BindingTest : INotifyPropertyChanged
    {

        public BindingTest()
            {
            }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private int sliderPosition;

        public int SliderPosition
        {
            get { return sliderPosition; }
            set
            {
                if (value != sliderPosition)
                {
                    sliderPosition = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private Color frameColor;

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
