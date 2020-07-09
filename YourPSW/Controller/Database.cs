using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using YourPSW.Model;

namespace YourPSW
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<StopwatchDB>().Wait();
            _database.CreateTableAsync<TimewatchDB>().Wait();
        }

        public Task<List<StopwatchDB>> GetStopwatches()
        {
            return _database.Table<StopwatchDB>().ToListAsync();
        }

        public Task<List<StopwatchDB>> GetLastStopwatch()
        {
            var lastStopwatch = _database.QueryAsync<StopwatchDB>("select * from StopwatchDB where id_stopwatch = (select MAX(id_stopwatch) from StopwatchDB)");
            return lastStopwatch;
        }

        public void DeleteStopwatch(string id_stopwatch)
        {
            _database.QueryAsync<StopwatchDB>("delete from StopwatchDB where id_stopwatch=?",id_stopwatch);
        }


        public Task<int> SaveStopwatchAsync(StopwatchDB stopwatchDB)
        {
            return _database.InsertAsync(stopwatchDB);
        }

        public Task<List<TimewatchDB>> GetTimewatches(string id_stopwatch)
        {
            if (id_stopwatch is null)
            {
                throw new System.ArgumentNullException(nameof(id_stopwatch));
            }

            var list = _database.QueryAsync<TimewatchDB>("select * from TimewatchDB where id_stopwatch=?", id_stopwatch);
            return list;
        }

        public void DeleteTimewatch(string id_stopwatch)
        {
            _database.QueryAsync<StopwatchDB>("delete from TimewatchDB where id_stopwatch=?", id_stopwatch);
        }


        public Task<int> SaveTimewatchAsync(TimewatchDB timewatch)
        {
            return _database.InsertAsync(timewatch);
        }
    }
}
