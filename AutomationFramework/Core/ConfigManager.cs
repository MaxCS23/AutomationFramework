using Newtonsoft.Json;

namespace AutomationFramework.Core
{
    public class Config
    {
        [JsonProperty("browser")]
        public required string Browser {  get; set; }
        [JsonProperty("loglevel")]
        public required string Loglevel { get; set; }
        [JsonProperty("baseUrlSaucedemo")]
        public required string BaseUrlSaucedemo { get; set; }
        [JsonProperty("headless")]
        public required bool Headless { get; set; }


    }
    public static class ConfigManager
    {
        public static Config Settings { get; set; } = null!;

        static ConfigManager() 
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "config.json");
            var json = File.ReadAllText(configPath);
            var config = JsonConvert.DeserializeObject<Config>(json);
            if (config == null) 
            {
                throw new InvalidOperationException($"Failed to load configuration from {configPath}");
            }
            Settings = config; 
        }
    }
}
