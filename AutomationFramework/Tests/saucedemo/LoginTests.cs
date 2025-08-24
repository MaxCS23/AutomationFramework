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
            var loginPage = new LoginPage(driver!);
            loginPage.GoToUrl(ConfigManager.Settings.BaseUrlSaucedemo);
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
            Uri expectedUri = new Uri(ConfigManager.Settings.BaseUrlSaucedemo);

            var loginPage = new LoginPage(driver!);
            loginPage.GoToUrl(ConfigManager.Settings.BaseUrlSaucedemo);
            Assert.IsTrue(loginPage.IsLoaded(), "Login Page did not load correctly");

            loginPage.LoginAsInvalidUser(username, password);

            Uri actualUri = new Uri(loginPage.GetUrl());

            Assert.Multiple(() => 
            {
                Assert.That(actualUri, Is.EqualTo(expectedUri));
                StringAssert.AreEqualIgnoringCase(expectedError, loginPage.GetErrorText());            
            });
        }

        [Test]
        public void Login_WithInvalidUser_ShouldDenyAccess()
        {
            string username = GenerateRandomUsername();
            string password = GenerateRandomPassword();
            string expectedError = "Epic sadface: Username and password do not match any user in this service";
            Uri expectedUri = new Uri(ConfigManager.Settings.BaseUrlSaucedemo);

            var loginPage = new LoginPage(driver!);
            loginPage.GoToUrl(ConfigManager.Settings.BaseUrlSaucedemo);
            Assert.IsTrue(loginPage.IsLoaded(), "Login Page did not load correctly");

            loginPage.LoginAsInvalidUser(username, password);

            Uri actualUri = new Uri(loginPage.GetUrl());

            Assert.Multiple(() =>
            {
                Assert.That(actualUri, Is.EqualTo(expectedUri));
                StringAssert.AreEqualIgnoringCase(expectedError, loginPage.GetErrorText());
            });
        }

        [Test]
        public void Login_WithInvalidPassword_ShouldDenyAccess()
        {
            string username = EnvConfig.GetValue("SAUCEDEMO_STANDARD_USER");
            string password = GenerateRandomPassword();
            string expectedError = "Epic sadface: Username and password do not match any user in this service";
            Uri expectedUri = new Uri(ConfigManager.Settings.BaseUrlSaucedemo);

            var loginPage = new LoginPage(driver!);
            loginPage.GoToUrl(ConfigManager.Settings.BaseUrlSaucedemo);
            Assert.IsTrue(loginPage.IsLoaded(), "Login Page did not load correctly");

            loginPage.LoginAsInvalidUser(username, password);

            Uri actualUri = new Uri(loginPage.GetUrl());

            Assert.Multiple(() =>
            {
                Assert.That(actualUri, Is.EqualTo(expectedUri));
                StringAssert.AreEqualIgnoringCase(expectedError, loginPage.GetErrorText());
            });
        }

        private static string GenerateRandomUsername(int length = 8)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "_user";
        }

        public static string GenerateRandomPassword(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static IEnumerable<TestCaseData> GetValidLoginData() 
        {
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_STANDARD_USER"), EnvConfig.GetValue("SAUCEDEMO_STANDARD_PASS"));
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_VISUAL_USER"), EnvConfig.GetValue("SAUCEDEMO_VISUAL_PASS"));        
        }
    }
}
