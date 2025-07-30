using AutomationFramework.Core;
using AutomationFramework.Pages.saucedemo;


namespace AutomationFramework.Tests.saucedemo
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [TestCaseSource(nameof(GetValidLoginData))]
        public void Login_WithValidCredentials_ShouldSucceed(string username, string password) 
        {
            var loginPage = new LoginPage(driver);
            loginPage.GoToUrl(ConfigManager.Settings.baseUrlSaucedemo);
            InventoryPage inventoryPage = loginPage.LoginAsValidUser(username, password);

            var url = inventoryPage.GetUrl();

            StringAssert.Contains("/inventory.html", url);
            Assert.IsTrue(inventoryPage.IsLoaded());
        }

        [Test]
        public void Login_WithLockedUser_ShouldDenyAccess()
        {
            string username = EnvConfig.LockedUser;
            string password = EnvConfig.LockedUserPassword;

            var loginPage = new LoginPage(driver);
            loginPage.GoToUrl(ConfigManager.Settings.baseUrlSaucedemo);

            loginPage.LoginAsInvalidUser(username, password);

            var url = loginPage.GetUrl();

            StringAssert.AreEqualIgnoringCase(ConfigManager.Settings.baseUrlSaucedemo, url);
            Assert.IsTrue(loginPage.IsLoaded());
        }

        private static IEnumerable<TestCaseData> GetValidLoginData() 
        {
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_STANDARD_USER"), EnvConfig.GetValue("SAUCEDEMO_STANDARD_PASS"));
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_VISUAL_USER"), EnvConfig.GetValue("SAUCEDEMO_VISUAL_PASS"));        
        }
    }
}
