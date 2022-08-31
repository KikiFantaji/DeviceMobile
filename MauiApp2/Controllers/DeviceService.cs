using MauiApp2.Models;
using SQLite;

namespace MauiApp2.Controllers
{
    public class DeviceService
    {
        SQLiteConnection database;

        public DeviceService(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            //database.DropTable<DeviceBase>();
            database.CreateTable<DeviceBase>();
        }

        /// <summary>
        /// Получение списка приборов
        /// </summary>
        /// <returns>Список приборов</returns>
        public IEnumerable<DeviceBase> GetItems()
        {
            return database.Table<DeviceBase>().ToList();
        }

        /// <summary>
        /// Получение прибора
        /// </summary>
        /// <param name="id">Первичный ключ в БД</param>
        /// <returns>Прибор</returns>
        public DeviceBase GetItem(int id)
        {
            return database.Get<DeviceBase>(id);
        }

        /// <summary>
        /// Удаление записи прибора
        /// </summary>
        /// <param name="id">Первичный ключ в БД</param>
        /// <returns>1 - если запись удалена, 0 - если нет</returns>
        public int DeleteItem(int id)
        {
            return database.Delete<DeviceBase>(id);
        }

        /// <summary>
        /// Удаление всех записей из таблицы
        /// </summary>
        /// <returns>1 - если запись удалена, 0 - если нет</returns>
        public int DeleteItems()
        {
            return database.DeleteAll<DeviceBase>();
        }

        /// <summary>
        /// Изменение записи оборудования
        /// </summary>
        /// <param name="item">Оборудование</param>
        /// <returns>Id - если идёт изменение записи, 1 - если добавляется новая запись</returns>
        public int SaveItem(DeviceBase item)
        {
            #region Нерабочее изменение записи
            //if (item.Id != 0)
            //{
            //    database.Update(item);
            //    return item.Id;
            //}
            //else
            //{
            //    return database.Insert(item);
            //}
            #endregion

            return database.Insert(item);
        }
    }
}