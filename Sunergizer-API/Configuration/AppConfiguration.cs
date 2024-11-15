namespace Sunergizer_API.Configuration
{
    public class AppConfiguration
    {
        public ConnectionString ConnectionStrings { get; set; }
        public SwaggerDoc Swagger { get; set; }

        public class ConnectionString
        {
            public string OracleSunergizer { get; set; }
        }
        public class SwaggerDoc
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}
