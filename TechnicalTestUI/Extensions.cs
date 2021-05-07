using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TechnicalTestUI
{
    public static class Extensions
    {
        public static void ClickButton(this IWebElement element, IWebDriver driver, WebDriverWait wait)
        {
            wait.Until(drv => element);
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            element.Click();
        }

    }
}
