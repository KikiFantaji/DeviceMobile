using MauiApp2.Models;
using SQLite;

namespace MauiApp2.Controllers
{
    public class DeviceDataService
    {
        SQLiteConnection database;

        public DeviceDataService(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            //database.DropTable<DataDevice>();
            database.CreateTable<DataDevice>();
        }

        /// <summary>
        /// Получение списка приборов
        /// </summary>
        /// <returns>Список приборов</returns>
        public IEnumerable<DataDevice> GetItems()
        {
            return database.Table<DataDevice>().ToList();
        }

        /// <summary>
        /// Получение прибора
        /// </summary>
        /// <param name="id">Первичный ключ в БД</param>
        /// <returns>Прибор</returns>
        public DataDevice GetItem(int id)
        {
            return database.Get<DataDevice>(id);
        }

        /// <summary>
        /// Удаление записи прибора
        /// </summary>
        /// <param name="id">Первичный ключ в БД</param>
        /// <returns>1 - если запись удалена, 0 - если нет</returns>
        public int DeleteItem(int id)
        {
            return database.Delete<DataDevice>(id);
        }

        /// <summary>
        /// Удаление всех записей из таблицы
        /// </summary>
        /// <returns>1 - если запись удалена, 0 - если нет</returns>
        public int DeleteItems()
        {
            return database.DeleteAll<DataDevice>();
        }

        /// <summary>
        /// Изменение записи оборудования
        /// </summary>
        /// <param name="item">Оборудование</param>
        /// <returns>Id - если идёт изменение записи, 1 - если добавляется новая запись</returns>
        public int SaveItem(DataDevice item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}