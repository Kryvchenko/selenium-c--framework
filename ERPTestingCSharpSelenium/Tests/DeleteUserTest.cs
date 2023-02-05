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
    // add test data
    [TestCase("Mark Zenn", "Joe Root", "Joe", "@Barry-Hill1", "Strongest")]

    public void DeleteUser(string userName, string adminName,string adminFirstName, string userPassword, string passwordStrengths)

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

}
