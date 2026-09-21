using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using InventoryDesktopApp.TestData.Models;
using InventoryDesktopApp.Utils;

namespace InventoryDesktopApp.Screen
{
    public class MainScreen
    {
        private WindowsDriver Driver;

        public MainScreen(WindowsDriver Driver)
        {
            this.Driver = Driver;
        }

        // Locators
        private readonly By InventoryTab = By.XPath("//TabItem[@AutomationId='InventoryTab']");
        private readonly By CustomerTab = By.XPath("//TabItem[@AutomationId='CustomersTab']");
        private readonly By UserLabel = By.XPath("//Text[@AutomationId='loggedinuserLBL']");

        private readonly By ItemNameInputBox = By.XPath("//Pane[@AutomationId='itemnametextbox']");
        private readonly By ItemPriceInputBox = By.XPath("//Pane[@AutomationId='itempricetextbox']");
        private readonly By ItemAmoutInputBox = By.XPath("//Pane[@AutomationId='itemamounttext']");
        private readonly By AddItemButton = By.XPath("//Pane[@AutomationId='AddItemBTN']");

        private readonly By Products = By.XPath("//DataGrid[@AutomationId='InventoryTable']/Custom");
        private readonly By NegativePriceErrorMsg = By.XPath("//Text[@Name='Invalid parameters']");

        private readonly By CustomerNameInputBox = By.XPath("//Pane[@AutomationId='CustomerAddName']");
        private readonly By CustomerPhoneInputBox = By.XPath("//Pane[@AutomationId='CustomerAddPhone']");
        private readonly By CustomerEmailInputBox = By.XPath("//Pane[@AutomationId='CustomerAddEmail']");
        private readonly By AddCustomerBtn = By.XPath("//Pane[@AutomationId='CustomerAddBTN']");

        private readonly By Customers = By.XPath("//DataGrid[@AutomationId='CustomersTable']/Custom");
        private readonly By InvalidFormatError = By.XPath("//Text[@Name='Invalid parameters']");
        private readonly By PhoneNumberLengthError = By.XPath("//Text[@Name='Invalid parameters']");

        private readonly By FirstRow = By.XPath("//DataGrid[@AutomationId='InventoryTable']/Custom[@Name='Row 0']");
        private readonly By EditButton = By.XPath("//Pane[@AutomationId='EditBTN']");
        private readonly By EditProductNameInputBox = By.XPath("//Pane[@AutomationId='EditName']");
        private readonly By EditProductPriceInputBox = By.XPath("//Pane[@AutomationId='EditPrice']");
        private readonly By EditProductStockInputBox = By.XPath("//Pane[@AutomationId='EditinStock']");
        private readonly By ConfirmButton = By.XPath("//Pane[@AutomationId='ConfirmEdit']");
        private readonly By SuccessfullyUpdatedMsg = By.XPath("//Text[@Name='Successfully Updated Records!']");

        private readonly By DeleteProductBtn = By.XPath("//Pane[@AutomationId='RemoveItemBTN']");


        // Actions 
        public bool IsErrorMsgDisplayed()
        {
            ConsoleLogger.Step("Verify error message");

            return Driver.FindElement(NegativePriceErrorMsg).Displayed;
        }

        public string GetUpdateMsgConfirmation()
        {
            ConsoleLogger.Step("Get product update confirmation message");

            return Driver.FindElement(SuccessfullyUpdatedMsg).Text;
        }

        public void ClickDeleteProductButton()
        {
            ConsoleLogger.Step("Click Delete Product button");

            Driver.FindElement(DeleteProductBtn).Click();
            Thread.Sleep(1000);
        }

        public bool IsSuccessfullyUpdatedMessageDisplayed()
        {
            ConsoleLogger.Step("Verify product update success message");

            return CustomWait.WaitForElementToBeDisplayed(
                Driver,
                SuccessfullyUpdatedMsg,
                10);
        }

        public void EditProductStock(string Stock)
        {
            ConsoleLogger.Step($"Edit product stock: {Stock}");

            var Element = Driver.FindElement(EditProductStockInputBox);
            Element.Clear();
            Element.SendKeys(Stock);
        }

        public void EditProductName(string ProductName)
        {
            ConsoleLogger.Step($"Edit product name: {ProductName}");

            var Element = Driver.FindElement(EditProductNameInputBox);
            Element.Clear();
            Element.SendKeys(ProductName);
        }

        public void EditProductPrice(string Price)
        {
            ConsoleLogger.Step($"Edit product price: {Price}");

            var Element = Driver.FindElement(EditProductPriceInputBox);
            Element.Clear();
            Element.SendKeys(Price);
        }

        public MainScreen SelectFirstProduct()
        {
            ConsoleLogger.Step("Select first product");

            Driver.FindElement(FirstRow).Click();

            return this;
        }

        public void ClickEditButton()
        {
            ConsoleLogger.Step("Click Edit button");

            Driver.FindElement(EditButton).Click();
        }

        public void ClickConfirmButton()
        {
            ConsoleLogger.Step("Click Confirm button");

            Driver.FindElement(ConfirmButton).Click();
        }

        public bool IsInvalidFormatErrorDisplayed()
        {
            ConsoleLogger.Step("Verify invalid format error message");

            return Driver.FindElement(InvalidFormatError).Displayed;
        }

        public bool IsPhoneNumberLengthErrorDisplayed()
        {
            ConsoleLogger.Step("Verify phone number length error message");

            return Driver.FindElement(PhoneNumberLengthError).Displayed;
        }

        public string GetErrorMessage()
        {
            ConsoleLogger.Step("Get error message");

            return Driver.FindElement(NegativePriceErrorMsg).Text;
        }

        public void ClickAddProduct()
        {
            ConsoleLogger.Step("Click Add Product button");

            Driver.FindElement(AddItemButton).Click();
        }

        public int GetProductCount()
        {
            return Driver.FindElements(Products).Count;
        }

        public int GetCustomerCount()
        {
            ConsoleLogger.Step("Get customer count");

            return Driver.FindElements(Customers).Count;
        }

        public void EnterProductAmount(string ProductAmount)
        {
            ConsoleLogger.Step($"Enter product amount: {ProductAmount}");

            var Element = Driver.FindElement(ItemAmoutInputBox);

            Element.Clear();
            Element.SendKeys(ProductAmount);
        }

        public void EnterProductName(string ProductName)
        {
            ConsoleLogger.Step($"Enter product name: {ProductName}");

            var Element = Driver.FindElement(ItemNameInputBox);

            Element.Clear();
            Element.SendKeys(ProductName);
        }

        public void EnterProductPrice(string ProductPrice)
        {
            ConsoleLogger.Step($"Enter product price: {ProductPrice}");

            var Element = Driver.FindElement(ItemPriceInputBox);

            Element.Clear();
            Element.SendKeys(ProductPrice);
        }

        public MainScreen SelectInventoryTab()
        {
            ConsoleLogger.Step("Select Inventory tab");

            Driver.FindElement(InventoryTab).Click();

            return this;
        }

        public MainScreen SelectCustomerTab()
        {
            ConsoleLogger.Step("Select Customers tab");

            Driver.FindElement(CustomerTab).Click();

            return this;
        }

        public bool IsInventoryTabPresent()
        {
            ConsoleLogger.Step("Verify Inventory tab is displayed");

            return Driver.FindElement(InventoryTab).Displayed;
        }

        public string GetUserLabel()
        {
            ConsoleLogger.Step("Get logged-in username");

            return Driver.FindElement(UserLabel).Text;
        }

        public bool IsCustomerTabPresent()
        {
            ConsoleLogger.Step("Verify Customers tab is displayed");

            return Driver.FindElement(CustomerTab).Displayed;
        }

        public void EnterCustomerName(String CustomerName)
        {
            ConsoleLogger.Step($"Enter customer name: {CustomerName}");

            var Element = Driver.FindElement(CustomerNameInputBox);

            Element.Clear();
            Element.SendKeys(CustomerName);
        }

        public void EnterCustomerEmail(String CustomerEmail)
        {
            ConsoleLogger.Step($"Enter customer email: {CustomerEmail}");

            var Element = Driver.FindElement(CustomerEmailInputBox);

            Element.Clear();
            Element.SendKeys(CustomerEmail);
        }

        public void EnterPhoneNumber(String CustomerPhoneNumber)
        {
            ConsoleLogger.Step($"Enter customer phone number: {CustomerPhoneNumber}");

            var Element = Driver.FindElement(CustomerPhoneInputBox);

            Element.Clear();
            Element.SendKeys(CustomerPhoneNumber);
        }

        public void ClickAddCustomer()
        {
            ConsoleLogger.Step("Click Add Customer button");

            Driver.FindElement(AddCustomerBtn).Click();
        }

        // Behaviour
        public void AddProduct(Product Product)
        {

            EnterProductName(Product.Name);
            EnterProductPrice(Product.Price);
            EnterProductAmount(Product.Amount);
            ClickAddProduct();
        }

        public void AddCustomer(Customer Customer)
        {

            EnterCustomerName(Customer.Name);
            EnterCustomerEmail(Customer.Email);
            EnterPhoneNumber(Customer.PhoneNumber);
            ClickAddCustomer();
        }

        public void UpdateProductDetails(Product UpdateProduct)
        {

            ClickEditButton();
            EditProductName(UpdateProduct.Name);
            EditProductPrice(UpdateProduct.Price);
            EditProductStock(UpdateProduct.Amount);
            ClickConfirmButton();
        }
    }
}