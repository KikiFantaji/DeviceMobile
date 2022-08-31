using MySqlConnector;

namespace MauiApp2
{
    public static class Data
    {
        // Строка подключения к Mysql 
        public static string mysqlConString = "Server=92.255.229.102;Port=4000;" +
                                          "Database=Offlink;" +
                                          "Uid=Tester;" +
                                          "Pwd=Init-123;" +
                                          "Default command timeout=120;";

        // Строка подключения к тестовому Mysql 
        public static string MysqlConTest = "Server=37.140.192.136;" +
                                        "Database=u1604744_Offlink;" +
                                        "Uid=u1604744_OfflinkUser;" +
                                        "Pwd=OfflinkUser;" +
                                        "Default command timeout=120;";

        public static MySqlConnection MysqlCon = new MySqlConnection(MysqlConTest);
    }
}
