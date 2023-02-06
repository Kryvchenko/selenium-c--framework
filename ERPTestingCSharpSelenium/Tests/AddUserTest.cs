using ERPTestingCSharpSelenium.pageObjects;
using ERPTestingCSharpSelenium.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace ERPTestingCSharpSelenium;

public class E2EAddUserTest : Base
{

    // run critical-path test from terminal
    // dotnet test ERPTestingCSharpSelenium.csproj --filter TestCategory=Smoke
    
    [Test, Category("Critical-Path")]
    // dynamic test data
    [TestCaseSource("AddTestDataConfig")]

    public void AddNewUser(string userName, string adminName, string adminFirstName, string userPassword, string passwordStrengths)

    {
        // Add new user
        UserPage userPage = new UserPage(getDriver());
        userPage.addNewUser(adminFirstName, adminName, userName, userPassword);
  
        // Assertion
        String strong = userPage.getPasswordStrength();
        StringAssert.Contains(passwordStrengths, strong);
        Console.WriteLine($"You have entered {strong} password");
        Assert.That(userPage.getPopUpMessage(), Is.True);
        if(userPage.getPopUpMessage()) Console.WriteLine($"User {userName} was successfully added");
    }

    public static IEnumerable<TestCaseData> AddTestDataConfig()
    {
        // multi data sets from database
        yield return new TestCaseData(getDataParser().extractData("userName"), getDataParser().extractData("adminName"), getDataParser().extractData("adminFirstName"), getDataParser().extractData("userPassword"), getDataParser().extractData("passwordStrengths"));
        //yield return new TestCaseData(getDataParser().extractData("userName"), getDataParser().extractData("adminName"), getDataParser().extractData("adminFirstName"), getDataParser().extractData("userPassword"), getDataParser().extractData("passwordStrengths"));   
    }

}
