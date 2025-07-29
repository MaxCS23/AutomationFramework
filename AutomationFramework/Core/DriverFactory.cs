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
        private static IWebDriver driver;

        public static IWebDriver GetDriver() 
        {
            if (driver == null) 
            {
                string browser = ConfigManager.Settings.browser.ToLower();

                switch (browser)
                {
                    case "chrome":
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        driver = new ChromeDriver();
                        break;
                    case "firefox":
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                        driver = new FirefoxDriver();
                        break;
                    case "edge":
                        new DriverManager().SetUpDriver(new EdgeConfig());
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
