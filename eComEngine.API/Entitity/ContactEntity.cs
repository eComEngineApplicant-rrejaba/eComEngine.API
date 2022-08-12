namespace eComEngine.API.Entitity
{
    public class NameEntity
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }

    public class AddressEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
    }

    public class PhoneEntity
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class ContactEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public NameEntity Name { get; set; }
        public AddressEntity Address { get; set; }
        public List<PhoneEntity> Phone { get; set; }
    }
}
