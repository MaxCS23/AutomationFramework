using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System.Security.Policy;

namespace AutomationFramework.Core
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected readonly ILogger log;

        public BasePage(IWebDriver driver) 
        {  
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            log = Logger.GetLogger();
        }

        public void GoToUrl(string url) 
        {
            
            if (string.IsNullOrEmpty(url)) 
            {
                throw new ArgumentException("The URL cannot be null");            
            } 

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) 
            {
                throw new UriFormatException($"Invalid URL format: {url}");                            
            }

            log.Information($"Navigating to url: {url}");
            Driver.Navigate().GoToUrl(url);        
        }

        protected void Click(By locator) 
        {
            var element = WaitForElement(locator);
            log.Debug($"Click on: {locator}");
            element.Click();
        }

        protected void SendKeys(By locator, string text)
        {
            var element = WaitForElement(locator);
            element.Clear();
            log.Debug($"SendKeys on: {locator}");
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            log.Debug($"Getting text from: {locator}");
            var element = WaitForElement(locator);
            return element.Text;
        }

        private IWebElement WaitForElement(By locator)
        {
            IWebElement element = Wait.Until(Driver =>
            {
                try
                {
                    element = Driver.FindElement(locator);
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (NoSuchElementException) 
                {
                    return null;
                }
            });
            return element;
        }
    }
}
