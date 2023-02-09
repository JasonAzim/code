
namespace PharmacyAdmin.Model
{
    public class NotificationModel
    {
        public string NotificationID { get; set; }
        public string Severity { get; set; }
        public string Code { get; set; }
        public int CodeInt
        {
            get => int.Parse(Code);
        }
        public string Message { get; set; }
        public string Source { get; set; }


        public NotificationModel()
        {
        }
    }
}

