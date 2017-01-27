using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Helpers
{
    public class SQLiteDatabase
    {
        public static SQLiteAsyncConnection GetConnection(string path, ISQLitePlatform sqlitePlatform)
        {
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(sqlitePlatform, new SQLiteConnectionString(path, storeDateTimeAsTicks: false)));
            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}
