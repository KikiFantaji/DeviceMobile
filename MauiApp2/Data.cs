using MauiApp2.Models;
using MySqlConnector;

namespace MauiApp2
{
    public static class Data
    {
        public static DataServer serverData = App.DataServer.GetItems().First();

        // Строка подключения к Mysql 
        public static string mysqlConString = $"Server={serverData.Server};Port={serverData.Port};" +
                                          $"Database={serverData.DataBase};" +
                                          $"Uid={serverData.User};" +
                                          $"Pwd={serverData.Password};" +
                                          "Default command timeout=120;";

        // Строка подключения к тестовому Mysql 
        public static string MysqlConTest = "Server=37.140.192.136;" +
                                        "Database=u1604744_Offlink;" +
                                        "Uid=u1604744_OfflinkUser;" +
                                        "Pwd=OfflinkUser;" +
                                        "Default command timeout=120;";

        public static MySqlConnection MysqlCon = new MySqlConnection(mysqlConString);
    }
}
