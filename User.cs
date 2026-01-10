namespace DataWarehouseApi
{
    public class User
    {
        public Guid id { get; set; }
        public string username { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;
    }
}
