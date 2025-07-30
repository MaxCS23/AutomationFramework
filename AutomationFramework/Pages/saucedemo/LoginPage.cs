using AutomationFramework.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.saucedemo
{
    internal class LoginPage : BasePage
    {
        private readonly By usernameField = By.Id("user-name");
        private readonly By passwordField = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By accessError = By.XPath("//h3[contains(@data-test, \"error\")]");

        public LoginPage(IWebDriver driver) : base(driver) {}

        public InventoryPage LoginAsValidUser(string username, string password) 
        {
            log.Debug($"Try to login using username: {username} and password: {password}");
            SendKeys(usernameField, username);
            SendKeys(passwordField, password);
            Click(loginButton);

            return new InventoryPage(Driver);
        }

        public void LoginAsInvalidUser(string username, string password)
        {
            log.Debug($"Try to login using username: {username} and password: {password}");
            SendKeys(usernameField, username);
            SendKeys(passwordField, password);
            Click(loginButton);            
        }

        public string GetErrorText() 
        {
            return GetText(accessError);        
        }

        public bool IsLoaded() 
        {
            return IsPageLoaded(loginButton);
        }
    }
}
