using InventoryDesktopApp.Base;
using InventoryDesktopApp.TestData;
using InventoryDesktopApp.TestData.ExpectedValues;
using InventoryDesktopApp.TestData.Models;
using InventoryDesktopApp.Utils;

namespace InventoryDesktopApp.Tests
{
    internal class InventoryTests : BaseTest
    {
        [SetUp]
        public void Login()
        {
            string FilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "JsonFiles", "ValidUser.json");
            LoginUser UserWithValidCredentials = JsonReader.ReadJson<LoginUser>(FilePath);

            LoginScreen.Authentication(UserWithValidCredentials);
            SwitchToWindow(Screens.INTERFACE_SCREEN);
            HomeScreen.SelectInventoryTab();
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ValidProduct))]
        [Category("Smoke")]
        [Category("Regression")]
        [Category("Inventory")]
        [TestName("TC-INVENTORY-001: Add valid product")]
        public void AddValidProduct(Product ProductWithValidDetails)
        {
            // Get the current number of products
            int InitialProductCount = HomeScreen.GetProductCount();

            // Add a product with valid details
            HomeScreen.AddProduct(ProductWithValidDetails);

            // Verify that the product was added successfully
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetProductCount(), Is.EqualTo(InitialProductCount + 1));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ProductWithNegativePrice))]
        [Category("Regression")]
        [Category("Inventory")]
        [TestName("TC-INVENTORY-002: Add product with negative price")]
        public void AddProductWithNegativePrice(Product ProductWithNegativePrice)
        {
            // Get the current number of products
            int InitialProductCount = HomeScreen.GetProductCount();

            // Add a product with a negative price
            HomeScreen.AddProduct(ProductWithNegativePrice);

            // Verify that the product was not added and the validation message is displayed
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetProductCount(), Is.EqualTo(InitialProductCount));
                Assert.That(HomeScreen.IsErrorMsgDisplayed(), Is.True);
                Assert.That(HomeScreen.GetErrorMessage(), Is.EqualTo(InterfaceExpected.ERROR_MSG));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.ProductWithEmptyName))]
        [Category("Regression")]
        [Category("Inventory")]
        [TestName("TC-INVENTORY-003: Add product with empty name")]
        public void AddProductWithEmptyName(Product ProductWithEmptyName)
        {
            // Get the current number of products
            int InitialProductCount = HomeScreen.GetProductCount();

            // Add a product with an empty name
            HomeScreen.AddProduct(ProductWithEmptyName);

            // Verify that the product was not added and the validation message is displayed
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetProductCount(), Is.EqualTo(InitialProductCount));
                Assert.That(HomeScreen.IsErrorMsgDisplayed(), Is.True);
                Assert.That(HomeScreen.GetErrorMessage(), Is.EqualTo(InterfaceExpected.ERROR_MSG));
            });
        }

        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.UpdateProductWIthValidDetails))]
        [Category("Smoke")]
        [Category("Regression")]
        [Category("Inventory")]
        [TestName("TC-INVENTORY-004: Update product with valid details")]
        public void UpdateProductWithValidDetails(Product UpdatedProduct)
        {
            // Select the first product and update it with valid details
            HomeScreen.SelectFirstProduct().UpdateProductDetails(UpdatedProduct);

            // Verify that the product was updated successfully
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.IsSuccessfullyUpdatedMessageDisplayed(), Is.True);
                Assert.That(HomeScreen.GetUpdateMsgConfirmation(), Is.EqualTo(InterfaceExpected.VALID_UPDATE_CONFIRMATION_MSG));
            });
        }

        [Test]
        [Category("Smoke")]
        [Category("Regression")]
        [Category("Inventory")]
        [TestName("TC-INVENTORY-005: Delete product from Inventory")]
        public void DeleteProductFromInventory()
        {
            // Get the current number of products
            int InitialProductCount = HomeScreen.GetProductCount();

            // Select the first product and delete it
            HomeScreen.SelectFirstProduct().ClickDeleteProductButton();

            // Verify that the product was deleted successfully
            Assert.Multiple(() =>
            {
                Assert.That(HomeScreen.GetProductCount(), Is.EqualTo(InitialProductCount - 1));
            });
        }

        /* ... */
    }
}