using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

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
                        driver = new ChromeDriver();
                        break;
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;
                    case "edge":
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
