using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TechnicalTestUI
{
    public class Tests
    {
        IWebDriver driver;

        IWebElement ShoppingCart => driver.FindElement(By.CssSelector("[class='shopping_cart'] > a"));

        IWebElement SummaryProducts => driver.FindElement(By.Id("summary_products_quantity"));
        WebDriverWait Wait;
        string BaseUrl = "http://automationpractice.com/index.php";


        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            driver.Url = BaseUrl;
            driver.Manage().Window.Maximize();
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            LogHelper.CreateLogFile();
            LogHelper.Write("Browser started");
        }

        [Test]
        public void test()
        {

            // Add elements into the shopping cart
            
            int i = 0;
            do
            {
                Wait.Until(x => x.FindElement(By.CssSelector("[class='product-image-container'] > [class='product_img_link']")));
                IList<IWebElement> buttonsElements = driver.FindElements(By.CssSelector("[class='product-image-container'] > [class='product_img_link']"));

                buttonsElements[i%4].ClickButton(driver, Wait);
                LogHelper.Write("Click 'image "+i+"' Button");

                driver.FindElement(By.CssSelector("[class='exclusive']")).ClickButton(driver, Wait);
                LogHelper.Write("Click 'Add cart' Button");

                driver.FindElement(By.CssSelector("[class*='continue']")).ClickButton(driver, Wait);
                LogHelper.Write("Click 'Continue' Button");

                driver.Url = BaseUrl;
                LogHelper.Write("Go to 'Home Page'");

                i++;
            } while (i < 10);

            // Navigate to shopping cart and validate the total items added are 10.

            ShoppingCart.ClickButton(driver, Wait);

            string textSummary = SummaryProducts.Text;

            LogHelper.Write("Validating Assert of products");
            Assert.AreEqual(textSummary, "10 Products");
            

            // Remove the elements
            IList<IWebElement> removeElements = driver.FindElements(By.ClassName("icon-trash"));

            int total = removeElements.Count;
            for (i = 0;i< total; i++)
            {
                Wait.Until(x => x.FindElements(By.ClassName("icon-trash")).Count == total - (i));
                
                removeElements[i].ClickButton(driver, Wait);
                LogHelper.Write("Click 'delete "+ i +"' product");
            }

        }

        [TearDown]
        public void closeBrowser()
        {
            if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                LogHelper.Write("Failed execution runtime: " + TestContext.CurrentContext.Result.Message);
            }
            else
            {
                LogHelper.Write("Success execution runtime");

            }

            LogHelper.CloseLogFile();
            driver.Close();
            
        }
    }
}