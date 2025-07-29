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
            loginPage.GoToUrl(ConfigManager.Settings.baseUrlSaucedemo.ToLower());
            loginPage.Login(username, password);

            StringAssert.Contains("/inventaory.html", driver.Url);
        }

        private static IEnumerable<TestCaseData> GetValidLoginData() 
        {
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_STANDARD_USER"), EnvConfig.GetValue("SAUCEDEMO_STANDARD_PASS"));
            yield return new TestCaseData(EnvConfig.GetValue("SAUCEDEMO_VISUAL_USER"), EnvConfig.GetValue("SAUCEDEMO_VISUAL_PASS"));
        
        }
    }
}
