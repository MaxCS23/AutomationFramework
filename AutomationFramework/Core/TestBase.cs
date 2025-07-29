using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Serilog;


namespace AutomationFramework.Core
{
    
    public class TestBase
    {
        protected IWebDriver driver;
        protected static ExtentReports extent;
        protected ExtentTest test;
        protected ILogger log;

        [OneTimeSetUp]
        public void GlobalSetup() 
        {
            Logger.Init();
            log = Logger.GetLogger();
            log.Information("Logger initialized");

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            var reportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(reportsDir);
            var reportPath = Path.Combine(reportsDir, $"TestReport_{timestamp}.html");

            var htmlReporter = new ExtentSparkReporter(reportPath);


            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            log.Information("ExtentReports initialized");
        }

        [SetUp]
        public void Setup() 
        {
            driver = DriverFactory.GetDriver();
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            log.Information($"************************************************************************************");
            log.Information($"Test Started: {TestContext.CurrentContext.Test.Name}");
        
        }

        [TearDown]
        public void TearDown() 
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Fail(errorMessage);
                log.Error($"Test Failed: {errorMessage}");
            }
            else
            {
                test.Pass("Test Passed");
                log.Information($"Test Passed");
            }
            DriverFactory.QuitDriver();
            log.Information($"************************************************************************************");

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
                log.Information("WebDriver disposed explicitly in TearDown.");
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            log.Information("ExtentReports flushed.");
            extent.Flush();
        }

    }
}
