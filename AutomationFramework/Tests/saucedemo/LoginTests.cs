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
            Assert.IsTrue(loginPage.IsLoaded(), "Login Page did not load correctly");

            InventoryPage inventoryPage = loginPage.LoginAsValidUser(username, password);

            var url = inventoryPage.GetUrl();

            Assert.Multiple(() => 
            {
                StringAssert.Contains("/inventory.html", url);
                Assert.IsTrue(inventoryPage.IsLoaded());            
            });
        }

        [Test]
        public void Login_WithLockedUser_ShouldDenyAccess()
        {
            string username = EnvConfig.LockedUser;
            string password = EnvConfig.LockedUserPassword;
            string expectedError = "Epic sadface: Sorry, this user has been locked out.";
            Uri expectedUri = new Uri(ConfigManager.Settings.baseUrlSaucedemo);

            var loginPage = new LoginPage(driver);
            loginPage.GoToUrl(ConfigManager.Settings.baseUrlSaucedemo);
            Assert.IsTrue(loginPage.IsLoaded(), "Login Page did not load correctly");

            loginPage.LoginAsInvalidUser(username, password);

            Uri actualUri = new Uri(loginPage.GetUrl());

            Assert.Multiple(() => 
            {
                Assert.That(actualUri, Is.EqualTo(expectedUri));
                StringAssert.AreEqualIgnoringCase(expectedError, loginPage.GetErrorText());            
            });
        }

        private static IEnumerable<TestCaseData> GetValidLoginData() 
        {
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_STANDARD_USER"), EnvConfig.GetValue("SAUCEDEMO_STANDARD_PASS"));
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_VISUAL_USER"), EnvConfig.GetValue("SAUCEDEMO_VISUAL_PASS"));        
        }
    }
}
