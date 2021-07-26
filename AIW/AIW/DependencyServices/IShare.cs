using System;
using System.Collections.Generic;
using System.Text;

namespace AIW.DependencyServices
{
    public interface IShare
    {
        void Share();
        event EventHandler<string> OnShareReceived;
    }
}
