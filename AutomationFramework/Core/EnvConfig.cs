
namespace AutomationFramework.Core
{
    public static class EnvConfig
    {
        static EnvConfig() => DotNetEnv.Env.Load();

        public static string GetValue(string key) 
        {
            return Environment.GetEnvironmentVariable(key) ?? throw new Exception($"Missing env var: {key}");
        }

        public static string LockedUser => GetValue("SAUCEDEMO_LOCKED_USER");
        public static string LockedUserPassword => GetValue("SAUCEDEMO_LOCKED_PASS");
    }
}
