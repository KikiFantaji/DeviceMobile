using MauiApp2.Models;
using SQLite;

namespace MauiApp2.Controllers
{
    public class DataServerService
    {
        SQLiteConnection database;

        public DataServerService(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            //database.DropTable<DataServer>();
            database.CreateTable<DataServer>();
        }

        /// <summary>
        /// Получение списка 
        /// </summary>
        /// <returns>Список приборов</returns>
        public IEnumerable<DataServer> GetItems()
        {
            return database.Table<DataServer>().ToList();
        }

        /// <summary>
        /// Удаление всех записей из таблицы
        /// </summary>
        /// <returns>1 - если запись удалена, 0 - если нет</returns>
        public int DeleteItems()
        {
            return database.DeleteAll<DataServer>();
        }

        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <param name="item">Информация о сервере</param>
        /// <returns>1 - если добавляется новая запись</returns>
        public int SaveItem(DataServer item)
        {
            return database.Insert(item);
        }
    }
}