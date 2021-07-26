using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AIW.Models;
using SQLite;
using SQLitePCL;

namespace AIW.Data
{
   public  class DownloadsDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public DownloadsDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ModelDatabaseDownloads>().Wait();
        }

        public Task<List<ModelDatabaseDownloads>> GetDownloadsDatabaseAsync()
        {
            return _database.Table<ModelDatabaseDownloads>().ToListAsync();
        }

        public Task<ModelDatabaseDownloads> GetDownloadsDatabaseAsync(int id)
        {
            return _database.Table<ModelDatabaseDownloads>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        
        
        public Task<int> CreatOrUpdateRecord(ModelDatabaseDownloads downloadItem)
        {
            var recordExist = _database.Table<ModelDatabaseDownloads>().Where(w => w.VideoId == downloadItem.VideoId).FirstOrDefaultAsync().Result;

            if (recordExist != null)
            {
                recordExist.DownloadProgress = downloadItem.DownloadProgress;
                recordExist.ProgressDownloadPercentage = downloadItem.ProgressDownloadPercentage;
                return _database.UpdateAsync(recordExist);
            }
            else
            {
                return _database.InsertAsync(downloadItem);
            }
           
            


        }

      



        //delate single entry per video ID
        public Task<int> DeleteDownloadsDatabaseAsync(ModelDatabaseDownloads downloadItem)
        {

            return  _database.Table<ModelDatabaseDownloads>().Where(w => w.VideoId == downloadItem.VideoId).DeleteAsync(); //delate per video ID

            
           // return _database.DeleteAsync(downloadItem); //DELATE by primary key 
        }


        //delate all the records
        public Task<int> DelateAllDatabaseRecords()
        {
            
            return _database.DeleteAllAsync<ModelDatabaseDownloads>();
        }

      

    }
}
