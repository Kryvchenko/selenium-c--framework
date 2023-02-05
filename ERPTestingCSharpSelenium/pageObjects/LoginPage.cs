using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ERPTestingCSharpSelenium.pageObjects
{
	public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements(driver, this);
        }

        // Pageobject factory

        [FindsBy(How = How.Name, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
		private IWebElement submitbtn;

		[FindsBy(How = How.XPath, Using = "//span[text()='Admin']/ancestor::a")]
		private IWebElement adminPanelBtn;

		public void ValidLogin(string userN, string userPass)
		{
			username.SendKeys(userN);
			password.SendKeys(userPass);
			submitbtn.Click();

        }
		public void openAdminPanelBtn()
		{
			adminPanelBtn.Click();
		}
	}
}

