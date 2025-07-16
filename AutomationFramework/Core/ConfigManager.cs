using Newtonsoft.Json;

namespace AutomationFramework.Core
{
    public class Config()
    {
        public string browser {  get; set; }
        public string loglevel { get; set; }

    }
    public static class ConfigManager
    {
        public static Config Settings { get; set; }

        static ConfigManager() 
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "config.json");
            var json = File.ReadAllText(configPath);
            Settings = JsonConvert.DeserializeObject<Config>(json);        
        }
    }
}
