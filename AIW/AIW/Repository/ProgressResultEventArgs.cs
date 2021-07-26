using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AIW
{
    public class ProgressResultEventArgs : EventArgs
    {
        public double Progress;
        public string ID;
        

        public ProgressResultEventArgs(double progress,string id)
        {
           
            Progress = progress;
            ID = id;
           
        }
    }
}
