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

        public LoginPage(IWebDriver driver) : base(driver) {}

        public void Login(string username, string password) 
        {
            SendKeys(usernameField, username);
            SendKeys(passwordField, password);
            Click(loginButton);        
        }
    }
}
