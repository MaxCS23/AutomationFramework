using AutomationFramework.Core;
using AutomationFramework.Pages.saucedemo;


namespace AutomationFramework.Tests.saucedemo
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [TestCase("standard_user", "secret_sauce")]
        [TestCase("visual_user", "secret_sauce")]
        public void Login_WithValidCredentials_ShouldSucceed(string username, string password) 
        {
            var loginPage = new LoginPage(driver);
            loginPage.GoToUrl(ConfigManager.Settings.baseUrlSaucedemo.ToLower());
            loginPage.Login(username, password);

            StringAssert.Contains("/inventory.html", driver.Url);
        }
    }
}
