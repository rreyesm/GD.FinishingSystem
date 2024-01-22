namespace GD.Finishing.WebAPI.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string KeyPassword { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        string _connectionString;
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = $"Data source={DataSource};Initial Catalog={InitialCatalog};User Id={UserId};Password={Password};Persist Security Info=True;";
                }
                return _connectionString;
            }
        }
    }
}
