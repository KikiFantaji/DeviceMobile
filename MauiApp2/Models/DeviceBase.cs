using SQLite;

namespace MauiApp2.Models
{
    [Table("DeviceBase")]
    public class DeviceBase : Base
    {
        public string Name { get; set; }
    }
}
