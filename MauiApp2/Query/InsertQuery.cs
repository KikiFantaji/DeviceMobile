using MauiApp2.Models;

namespace MauiApp2.Query
{
    public static class InsertQuery
    {
        /// <summary>
        /// Добавление в таблицу Mysql счётчиков
        /// </summary>
        /// <param name="Device">Объект счётчика</param>
        /// <returns>Строка Mysql запроса</returns>
        public static string InsertDevice(DeviceBase Device)
        {
            return $"INSERT INTO `device`(`Id`) " +
                   $"VALUES ('{Device.Id}')";
        }

        /// <summary>
        /// Добавление в таблицу Mysql данных со счётчиков
        /// </summary>
        /// <param name="dataDevice">Объект данных</param>
        /// <returns>Строка Mysql запроса</returns>
        public static string InsertDeviceData(DataDevice dataDevice)
        {
            return $"INSERT INTO `device_data`(`DeviceId`, `Date`, `Value`) " +
                   $"VALUES (" +
                        $"'{dataDevice.DeviceId}', " +
                        $"'{dataDevice.Date.Year}-{dataDevice.Date.Month}-{dataDevice.Date.Day}', " +
                        $"{dataDevice.Value})";
        }
    }
}
