using Cf.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cf.Data
{
    public class CfDb
    {
        readonly SQLiteAsyncConnection database;

        public CfDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UsuarioModel>().Wait();
        }

        public Task<List<UsuarioModel>> GetUsuarioAsync()
        {
            return database.Table<UsuarioModel>().ToListAsync();
        }

        public Task<List<UsuarioModel>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<UsuarioModel>("SELECT * FROM [UsuarioModel] WHERE [Done] = 0");
        }

        public Task<UsuarioModel> GetItemAsync(int id)
        {
            var usuario = database.Table<UsuarioModel>().Where(i => i.ID == id).FirstOrDefaultAsync();

            return usuario;
        }

        public Task<int> SaveItemAsync(UsuarioModel item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(UsuarioModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}
