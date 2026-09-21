using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using InventoryDesktopApp.TestData.Models;
using InventoryDesktopApp.Utils;

namespace InventoryDesktopApp.Screen
{

    public class UserAuthenticationScreen
    {
        private WindowsDriver Driver;
        public UserAuthenticationScreen(WindowsDriver Driver)
        {
            this.Driver = Driver;
        }

        // Locators
        private readonly By UsernameInput =By.XPath("//Edit[@AutomationId='usernameTXT']");
        private readonly By PasswordInput =By.XPath("//Edit[@AutomationId='passwordTXT']");
        private readonly By LoginButton =By.XPath("//Button[@AutomationId='loginBTN']");
        private readonly By InvalidCredentialsMsg = By.XPath("//Text[@Name='Invalid Credentials!']");
        private readonly By PasswordErrorMsg =By.XPath("//Text[@AutomationId='passwordcheckLBL']");
        private readonly By UsernameErrorMsg = By.XPath("//Text[@AutomationId='usernamecheckLBL']");

        // Actions
        public void EnterUsername(string Username)
        {
            ConsoleLogger.Step($"Enter username: {Username}");
            var Element = Driver.FindElement(UsernameInput);

            Element.Clear();
            Element.SendKeys(Username);
        }
        public bool IsUsernameErrorDisplayed()
        {
            ConsoleLogger.Step("Verify username validation message");
            return Driver.FindElement(UsernameErrorMsg).Displayed;
        }

        public string GetUsernameErrorMsg()
        {
            ConsoleLogger.Step("Get username validation message");
            return Driver.FindElement(UsernameErrorMsg).Text;
        }

        public bool IsPasswordErrorDisplayed()
        {
            ConsoleLogger.Step("Verify password validation message");
            return Driver.FindElement(PasswordErrorMsg).Displayed;
        }

        public string GetPasswordErrorMsg()
        {
            ConsoleLogger.Step("Get password validation message");
            return Driver.FindElement(PasswordErrorMsg).Text;
        }

        public bool IsInvalidCedentialsMsgDisplayed()
        {
            ConsoleLogger.Step("Verify invalid credentials message");
            return Driver.FindElement(InvalidCredentialsMsg).Displayed;
        }

        public string GetInvalidCredentialsMsg()
        {
            ConsoleLogger.Step("Get invalid credentials message");
            return Driver.FindElement(InvalidCredentialsMsg).Text;
        }

        public void EnterPassword(string Password)
        {
            var Element = Driver.FindElement(PasswordInput);

            Element.Clear();
            Element.SendKeys(Password);
        }

        public void ClickLogin()
        {
            Driver.FindElement(LoginButton).Click();
        }

        // Behaviour
        public void Authentication(LoginUser standardUser)
        {
            ConsoleLogger.Step("Authenticate user");
            EnterUsername(standardUser.Username);
            EnterPassword(standardUser.Password);
            ClickLogin();
            //some wait
        }
    }
}
