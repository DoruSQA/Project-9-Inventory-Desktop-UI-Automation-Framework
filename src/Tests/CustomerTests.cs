using InventoryDesktopApp.Base;
using InventoryDesktopApp.TestData;
using InventoryDesktopApp.TestData.ExpectedValues;
using InventoryDesktopApp.TestData.Models;
using InventoryDesktopApp.Utils;

namespace InventoryDesktopApp.Tests
{
    public class CustomerTests : BaseTest
    {
        [SetUp]
        public void Login()
        {
            string FilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "JsonFiles", "ValidUser.json");
            LoginUser UserWithValidCredentials = JsonReader.ReadJson<LoginUser>(FilePath);

            LoginScreen.Authentication(UserWithValidCredentials);
            SwitchToWindow(Screens.INTERFACE_SCREEN);
            HomeScreen.SelectCustomerTab();
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidCustomer))]
        [Category("Smoke")]
        [Category("Regression")]
        [Category("Customer")]
        [TestName("TC-CUSTOMER-001: Add valid customer")]
        public void AddValidCustomer(Customer ValidCustomer)
        {
            // Get the current number of customers
            int initialCustomerCount = HomeScreen.GetCustomerCount();

            // Add a customer with valid details
            HomeScreen.AddCustomer(ValidCustomer);

            // Verify that the customer was added successfully
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetCustomerCount(), Is.EqualTo(initialCustomerCount + 1));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.CustomerWithInvalidEmailFormat))]
        [Category("Regression")]
        [Category("Customer")]
        [TestName("TC-CUSTOMER-002: Add customer with invalid email format")]
        public void AddCustomerWithInvalidEmailFormat(Customer CustomerWithInvalidEmailFormat)
        {
            // Get the current number of customers
            int initialCustomerCount = HomeScreen.GetCustomerCount();

            // Add a customer with an invalid email format
            HomeScreen.AddCustomer(CustomerWithInvalidEmailFormat);

            // Verify that the customer was not added and the email validation is displayed
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetCustomerCount(), Is.EqualTo(initialCustomerCount));
                Assert.That(HomeScreen.IsInvalidFormatErrorDisplayed(), Is.True);
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.CustomerWithPhoneExceedingMaxLength))]
        [Category("Regression")]
        [Category("Customer")]
        [TestName("TC-CUSTOMER-003: Add customer with phone number exceeding max length")]
        public void AddCustomerWithPhoneNumberExceedingMaxLength(Customer CustomerWithPhoneNumberExceedingMaxLength)
        {
            // Get the current number of customers
            int initialCustomerCount = HomeScreen.GetCustomerCount();

            // Add a customer with a phone number exceeding the maximum length
            HomeScreen.AddCustomer(CustomerWithPhoneNumberExceedingMaxLength);

            // Verify that the customer was not added and the phone number validation is displayed
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetCustomerCount(), Is.EqualTo(initialCustomerCount));
                Assert.That(HomeScreen.IsPhoneNumberLengthErrorDisplayed(), Is.True);
            });
        }

        /* ... */
    }
}