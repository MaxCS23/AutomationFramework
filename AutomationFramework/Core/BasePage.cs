using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Core
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public BasePage(IWebDriver driver) 
        {  
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));            
        }

        protected void Click(By locator) 
        {
            var element = WaitForElement(locator);
            element.Click();
        }

        protected void SendKeys(By locator, string text)
        {
            var element = WaitForElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        { 
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
