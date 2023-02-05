using System;
using ERPTestingCSharpSelenium.pageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace ERPTestingCSharpSelenium.pageObjects
{
    public class UserPage
    {
        private IWebDriver driver;
        By successPopUpXpath = By.XPath("//*[@class='oxd-toast-content oxd-toast-content--success']");

        public UserPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Pageobject factory

        [FindsBy(How = How.CssSelector, Using = ".orangehrm-header-container button")]
        private IWebElement addUserBtn;

        [FindsBy(How = How.XPath, Using = "//label[text()='User Role']/../following-sibling::div/div")]
        private IWebElement userRoleClick;

        [FindsBy(How = How.XPath, Using = "//div[@role='option']/span[text()='Admin']")]
        private IWebElement userRoleSelect;

        [FindsBy(How = How.XPath, Using = "//div[@class='oxd-autocomplete-wrapper']/div/input")]
        private IWebElement employeeInput;

        [FindsBy(How = How.XPath, Using = "//div[@role='option']/span")]
        private IList<IWebElement> employeeAutosugestList;

        [FindsBy(How = How.XPath, Using = "//label[text()='Status']/../following-sibling::div/div")]
        private IWebElement userStatus;

        [FindsBy(How = How.XPath, Using = "//div[@role='option']/span[text()='Enabled']")]
        private IWebElement selectUserStatus;

        [FindsBy(How = How.XPath, Using = "//label[text()='Username']/../following-sibling::div/input")]
        private IWebElement userNameInput;

        [FindsBy(How = How.XPath, Using = "//label[text()='Password']/../following-sibling::div/input")]
        private IWebElement userPassword;

        [FindsBy(How = How.XPath, Using = "//label[text()='Confirm Password']/../following-sibling::div/input")]
        private IWebElement confirmUserPassword;

        [FindsBy(How = How.XPath, Using = "//span[text()='Strongest ']")]
        private IWebElement passwordStrength;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement saveBtn;

        [FindsBy(How = How.XPath, Using = "//*[@class='oxd-toast-content oxd-toast-content--success']")]
        private IWebElement successPopUpXpathFullPath;

        [FindsBy(How = How.XPath, Using = "//div[@class='oxd-autocomplete-wrapper']/div/input")]
        private IWebElement employeeName;

        [FindsBy(How = How.XPath, Using = "//*[@class='oxd-icon bi-trash']/ancestor::button")]
        private IWebElement trashIcon;

        [FindsBy(How = How.XPath, Using = "//button[normalize-space()='Yes, Delete']")]
        private IWebElement confirmDelete;

        public void selectUserFromDropDown(string adminName)
        {
            int count = 0;
            foreach (IWebElement e in employeeAutosugestList)
            {
                if (e.Text == adminName)
                {
                    e.Click();
                    count++;
                    break;
                }
            }
            if (count != 0) Console.WriteLine($"Employee {adminName} successfully selected");
            else Console.WriteLine($"No sush employee name {adminName}");
        }


        public void addNewUser(string adminFirstName, string adminName, string userName, string password)
        {
            addUserBtn.Click();
            userRoleClick.Click();
            userRoleSelect.Click();
            employeeInput.SendKeys(adminFirstName);
            selectUserFromDropDown(adminName);
            userStatus.Click();
            selectUserStatus.Click();
            userNameInput.SendKeys(userName);
            userPassword.SendKeys(password);
            confirmUserPassword.SendKeys(password);
            Thread.Sleep(1000);
            saveBtn.Click();           
        }

        public string getPasswordStrength()
        {
            return passwordStrength.Text;
        }

        public bool getPopUpMessage()
        {
            //Explicit wait for particular element
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(successPopUpXpath));
            return successPopUpXpathFullPath.Displayed;
        }

        public void searhForAddedUser(string userName, string adminFirstName, string adminName)
        {
            userNameInput.SendKeys(userName);
            userRoleClick.Click();
            userRoleSelect.Click();
            employeeName.SendKeys(adminFirstName);
            selectUserFromDropDown(adminName);
            userStatus.Click();
            selectUserStatus.Click();
            saveBtn.Click();
        }

        public void deleteUser()
        {
            trashIcon.Click();
            confirmDelete.Click();
        }

    }
}

