using System;
using System.IO;
using System.Configuration;
using AventStack.ExtentReports.Reporter;
using ERPTestingCSharpSelenium.pageObjects;
using ERPTestingCSharpSelenium.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace ERPTestingCSharpSelenium.utilities
{
    public class Base
    {

        public IWebDriver driver;

        public ExtentReports extent;
        public ExtentTest test;
        // report file
        [OneTimeSetUp]

        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlteporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlteporter);
            extent.AddSystemInfo("Host Name", "localhost");
        }

        [SetUp]

        public void StartBrowser()
        {
            // Configuration
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            String browserName = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browserName);

            // Setup implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Open base url and maximize window
            driver.Manage().Window.Minimize();
            driver.Url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";

            //Login to the page and open admin panel on the dashboard
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.ValidLogin("Admin", "admin123");
            loginPage.openAdminPanelBtn();
        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]

        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if(status == TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenShot(getDriver(), fileName));
                test.Log(Status.Fail, "Tets failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                test.Log(Status.Pass, "Tets passed with logtrace" + stackTrace);
            }
            extent.Flush();
            driver.Close();
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}

