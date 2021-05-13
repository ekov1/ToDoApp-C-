using Microsoft.Extensions.Configuration;

namespace ToDoApp
{
    public class ConfigInitializer
    {
        public static IConfigurationRoot InitConfig()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
            return builder.Build();
        }
    }
}
