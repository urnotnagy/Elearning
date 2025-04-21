namespace ELearning.Infrastructure.Data
{
    public class DatabaseSettings
    {
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(Host) || string.IsNullOrEmpty(Port) || 
                string.IsNullOrEmpty(Database) || string.IsNullOrEmpty(Username) || 
                string.IsNullOrEmpty(Password))
            {
                throw new InvalidOperationException("Database settings are not properly configured.");
            }

            return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
        }
    }
} 