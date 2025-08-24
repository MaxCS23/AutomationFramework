using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationFramework.Core
{
    public static class DriverFactory
    {
        private static IWebDriver? driver;

        public static IWebDriver GetDriver() 
        {
            if (driver == null) 
            {
                string browser = ConfigManager.Settings.Browser.ToLower();
                bool headless = ConfigManager.Settings.Headless;

                switch (browser)
                {
                    case "chrome":
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        if (headless)
                        {
                            var chromeOptions = new ChromeOptions();
                            chromeOptions.AddArgument("--headless"); // modo headless
                            chromeOptions.AddArgument("--window-size=1920,1080");
                            driver = new ChromeDriver(chromeOptions);
                            break;
                        }
                        driver = new ChromeDriver();
                        break;
                    case "firefox":
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                        if (headless)
                        {
                            var firefoxOptions = new FirefoxOptions();
                            firefoxOptions.AddArgument("--headless");
                            firefoxOptions.AddArgument("--width=1920");
                            firefoxOptions.AddArgument("--height=1080");
                            driver = new FirefoxDriver(firefoxOptions);
                            break;
                        }
                        driver = new FirefoxDriver();
                        break;
                    case "edge":
                        new DriverManager().SetUpDriver(new EdgeConfig());
                        if (headless)
                        {
                            var edgeOptions = new EdgeOptions();
                            edgeOptions.AddArgument("headless");
                            edgeOptions.AddArgument("window-size=1920,1000");
                            driver = new EdgeDriver(edgeOptions);
                            break;

                        }
                        driver = new EdgeDriver();
                        break;
                    default:
                        throw new ArgumentException($"Browser '{browser}' is not supported.");
                } 
                
                driver.Manage().Window.Maximize();
            }
            return driver;
        }

        public static void QuitDriver() 
        {
            driver?.Quit();
            driver?.Dispose();
            driver = null;
        }
    }
}
