using AutomationFramework.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.saucedemo
{
    internal class InventoryPage : BasePage
    {
        private readonly By cartIcon = By.ClassName("shopping_cart_link");

        public InventoryPage(IWebDriver driver) : base(driver) {}


        public bool IsLoaded()
        {
            return IsPageLoaded(cartIcon);
        }

    }
}
