using ERPTestingCSharpSelenium.pageObjects;
using ERPTestingCSharpSelenium.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ERPTestingCSharpSelenium;

public class E2EDeleteUserTest : Base
{
    // run smoke test from terminal
    // dotnet test ERPTestingCSharpSelenium.csproj --filter TestCategory=Smoke

    [Test, Category("Smoke")]
    // dynamic test data
    [TestCaseSource("AddDeleteTestDataConfig")]

    public void DeleteUser(string userName, string adminName,string adminFirstName)

    {
        // Filter urers by role and status
        UserPage userPage = new UserPage(getDriver());
        userPage.searhForAddedUser(userName, adminFirstName, adminName);
        // Delete user
        userPage.deleteUser();
        // Assertion
        Assert.That(userPage.getPopUpMessage(), Is.True);
        if (userPage.getPopUpMessage()) Console.WriteLine($"User {userName} was successfully deleted");
    }

    public static IEnumerable<TestCaseData> AddDeleteTestDataConfig()
    {
        // add multi data sets manualy or from database
        yield return new TestCaseData(getDataParser().extractData("userName"), getDataParser().extractData("adminName"), getDataParser().extractData("adminFirstName"));
    }

}
