using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumExr
{
	public class OrangeHrm
	{
        IWebDriver driver;
        String userName = "Alan Var";
        String adminName = "Joe Root";

        [SetUp]

        public void Dashboard()
        {
            // Setup chromedriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Minimize();
            driver.Url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
            // Login to the page and open admin panel on the dashboard
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.XPath("//span[text()='Admin']/ancestor::a")).Click();
         }

        [Test]

        public void AddNewUser()
            
        {
            // Add new user
            driver.FindElement(By.CssSelector(".orangehrm-header-container button")).Click();
            driver.FindElement(By.XPath("//label[text()='User Role']/../following-sibling::div/div")).Click();
            driver.FindElement(By.XPath("//div[@role='option']/span[text()='Admin']")).Click();
            driver.FindElement(By.XPath("//div[@class='oxd-autocomplete-wrapper']/div/input")).SendKeys("Jo");
            int count = 0;
            IReadOnlyList<IWebElement> options = driver.FindElements(By.XPath("//div[@role='option']/span"));
            foreach (IWebElement e in options)
            {
                if (e.Text == adminName)
                {
                    e.Click();
                    count++;
                    break;
                }
            }
            if(count != 0) Console.WriteLine($"User {adminName} successfully selected");
            else Console.WriteLine($"No sush user name {adminName}");
            driver.FindElement(By.XPath("//label[text()='Status']/../following-sibling::div/div")).Click();
            driver.FindElement(By.XPath("//div[@role='option']/span[text()='Enabled']")).Click();
            driver.FindElement(By.XPath("//label[text()='Username']/../following-sibling::div/input")).SendKeys($"{userName}");
            driver.FindElement(By.XPath("//label[text()='Password']/../following-sibling::div/input")).SendKeys("@Barry-Hill1");
            driver.FindElement(By.XPath("//label[text()='Confirm Password']/../following-sibling::div/input")).SendKeys("@Barry-Hill1");
            bool strong = driver.FindElement(By.XPath("//span[text()='Strongest ']")).Displayed;
            Console.WriteLine($"You have entered strongest password - {strong}");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[@class='oxd-form-actions']/button[@type='submit']")).Click();
            String currentUserName = driver.FindElement(By.XPath($"//div[contains(text(),'{userName}')]")).Text;
            Assert.That(currentUserName, Is.EqualTo($"{userName}"), "Names are not matching");
            //driver.Quit();
        }

        [Test]

        public void FilterByUserAndRole()
        {
            // Filter urers by role and status
            driver.FindElement(By.XPath("//label[text()='Username']/../following-sibling::div/input")).SendKeys($"{userName}");
            driver.FindElement(By.XPath("//label[text()='User Role']/../following-sibling::div/div")).Click();
            driver.FindElement(By.XPath("//div[@role='option']/span[text()='Admin']")).Click();
            driver.FindElement(By.XPath("//div[@class='oxd-autocomplete-wrapper']/div/input")).SendKeys("Jo");
            int count = 0;
            IReadOnlyList<IWebElement> options = driver.FindElements(By.XPath("//div[@role='option']/span"));
            foreach (IWebElement e in options)
            {
                if (e.Text == adminName)
                {
                    e.Click();
                    count++;
                    break;
                }
            }
            if (count != 0) Console.WriteLine($"User {adminName} successfully selected");
            else Console.WriteLine($"No sush user name {adminName}");
            driver.FindElement(By.XPath("//label[text()='Status']/../following-sibling::div/div")).Click();
            driver.FindElement(By.XPath("//div[@role='option']/span[text()='Enabled']")).Click();
            driver.FindElement(By.CssSelector(".oxd-form-actions button[type='submit']")).Click();
            driver.FindElement(By.XPath("//*[@class='oxd-icon bi-trash']/ancestor::button")).Click();
            driver.FindElement(By.XPath("//button[normalize-space()='Yes, Delete']")).Click();
            //Explicit wait for particular element
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            String successPopUpXpath = "//*[@class='oxd-toast-content oxd-toast-content--success']";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(successPopUpXpath)));
            bool success = driver.FindElement(By.XPath(successPopUpXpath)).Displayed;
            Assert.That(success, Is.True);
            Console.WriteLine($"User {userName} was successfully deleted - {success}");
            //driver.Quit();
        }
    }
}

