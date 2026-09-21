using NUnit.Framework;
using InventoryDesktopApp.Base;
using InventoryDesktopApp.TestData;
using InventoryDesktopApp.Utils;
using InventoryDesktopApp.TestData.Models;

namespace InventoryDesktopApp.Tests
{
    public class LoginTests : BaseTest
    {
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidUser))]
        [Category("Smoke")]
        [Category("Regression")]
        [Category("Login")]
        [TestName("TC-LOGIN-001: Login with valid credentials")]
        public void LoginWithValidCredentials(LoginUser UserWithValidDetails)
        {
            // Enter valid login credentials
            LoginScreen.Authentication(UserWithValidDetails);

            // Switch to the main interface
            SwitchToWindow(Screens.INTERFACE_SCREEN);

            // Verify the logged-in user and available inventory sections
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetUserLabel(), Is.EqualTo(UserWithValidDetails.Username));
                Assert.That(HomeScreen.IsInventoryTabPresent(), Is.True);
                Assert.That(HomeScreen.IsCustomerTabPresent(), Is.True);
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidUsernameAndWrongPassword))]
        [Category("Regression")]
        [Category("Login")]
        [TestName("TC-LOGIN-002: Login with valid username and wrong password")]
        public void LoginWithValidUsernameAndWrongPassword(LoginUser UserWithWrongPassword)
        {
            // Enter a valid username with an incorrect password
            LoginScreen.Authentication(UserWithWrongPassword);

            // Verify the invalid credentials validation
            Assert.Multiple(() =>
            {
                Assert.That(LoginScreen.IsInvalidCedentialsMsgDisplayed(), Is.True);
                Assert.That(LoginScreen.GetInvalidCredentialsMsg, Is.EqualTo(LoginExpected.FAILED_LOGIN_WRONG_PASSWORD));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidPasswordAndWrongUsername))]
        [Category("Regression")]
        [Category("Login")]
        [TestName("TC-LOGIN-003: Login with valid password and wrong username")]
        public void LoginWithWrongUsernameAndValidPassword(LoginUser UserWithWrongUsername)
        {
            // Enter an incorrect username with a valid password
            LoginScreen.Authentication(UserWithWrongUsername);

            // Verify the invalid credentials validation
            Assert.Multiple(() =>
            {
                Assert.That(LoginScreen.IsInvalidCedentialsMsgDisplayed(), Is.True);
                Assert.That(LoginScreen.GetInvalidCredentialsMsg, Is.EqualTo(LoginExpected.FAILED_LOGIN_WRONG_USERNAME));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidUsernameAndEmptyPassword))]
        [Category("Regression")]
        [Category("Login")]
        [TestName("TC-LOGIN-004: Login with empty password and valid username")]
        public void LoginWithValidUsernameAndEmptyPassword(LoginUser UserWithEmptyPassword)
        {
            // Enter a valid username with an empty password
            LoginScreen.Authentication(UserWithEmptyPassword);

            // Verify the password validation
            Assert.Multiple(() =>
            {
                Assert.That(LoginScreen.IsPasswordErrorDisplayed(), Is.True);
                Assert.That(LoginScreen.GetPasswordErrorMsg, Is.EqualTo(LoginExpected.FAILED_LOGIN_WITH_EMPTY_PASSWORD));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidPasswordAndEmptyUsername))]
        [Category("Regression")]
        [Category("Login")]
        [TestName("TC-LOGIN-005: Login with empty username and valid password")]
        public void LoginWithValidPasswordAndEmptyUsername(LoginUser UserWithEmptyUsername)
        {
            // Enter an empty username with a valid password
            LoginScreen.Authentication(UserWithEmptyUsername);

            // Verify the username validation
            Assert.Multiple(() =>
            {
                Assert.That(LoginScreen.IsUsernameErrorDisplayed(), Is.True);
                Assert.That(LoginScreen.GetUsernameErrorMsg, Is.EqualTo(LoginExpected.FAILED_LOGIN_WITH_EMPTY_USERNAME));
            });
        }

        /* ... */
    }
}