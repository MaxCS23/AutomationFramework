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
            var element = WaitForVisibleAndEnabledElement(locator);
            log.Debug($"Click on: {locator}");
            element.Click();
        }

        protected void SendKeys(By locator, string text)
        {
            var element = WaitForVisibleAndEnabledElement(locator);
            element.Clear();
            log.Debug($"SendKeys on: {locator}");
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            log.Debug($"Getting text from: {locator}");
            var element = WaitForVisibleAndEnabledElement(locator);
            return element.Text;
        }

        public string GetUrl() 
        {
            if (Driver == null) 
            {
                log.Error("Driver is null, cannot get the URL");
                throw new InvalidOperationException("Driver is null.");
            
            }
            try
            {
                return Driver.Url;
            }
            catch (WebDriverException ex)
            {
                log.Error($"Error getting URL: {ex.Message}");
                throw;
            }
        }

        protected bool IsPageLoaded(By locator) 
        {
           return WaitForVisibleAndEnabledElement(locator) != null;
        }

        private IWebElement WaitForVisibleAndEnabledElement(By locator)
        {
            try
            {
                return Wait.Until(Driver =>
                {
                    try
                    {
                        var element = Driver.FindElement(locator);
                        return (element.Displayed && element.Enabled) ? element : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                log.Warning($"Element not found or not interactable within timeout: {locator}");
                return null;
            }
        }
    }
}
