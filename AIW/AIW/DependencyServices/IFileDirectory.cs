using AIW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AIW.DependencyServices
{
    public interface IFileDirectory
    {
      
        string GetDirectory();

        Task<ObservableCollection<ModelComplatedDownloads>> EnumerateFilesAsync();
        void  DeleteFile(string fileName);
        //event EventHandler<ObservableCollection<ModelComplatedDownloads>> OnEnumerate;
       

    }
}
