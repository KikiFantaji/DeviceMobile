using SQLite;

namespace MauiApp2.Models
{
    [Table("DataDevice")]
    public class DataDevice : Base
    {
        public string DeviceId { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}
