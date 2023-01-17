namespace Domain.Model.User
{
    public class Address : BaseEntity
    {
        public User user { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postCode { get; set; }
        public string country { get; set; } 
    }
}
