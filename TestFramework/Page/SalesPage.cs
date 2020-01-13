using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TestFramework.Constants;
using TestFramework.Page.Base;

namespace TestFramework.Page
{
    public class SalesPage : SearchBarPageBase<ItemsPage>
    {
        private const string _itemsOnSale = "products-catalog__list";

        public SalesPage(IWebDriver driver)
            : base(driver)
        {
            Url = PageUrl.Sales;
        }

        public bool AreAllItemsOnSale()
        {
            var itemsOnSale = _driver.FindElements(By.ClassName(_itemsOnSale));
            int trueAmountOnSaleElements = 0;
            foreach(var item in itemsOnSale)
            {
                if (item.FindElements(By.ClassName("price__old")).Count > 0)
                    trueAmountOnSaleElements++;
            }

            return (itemsOnSale.Count == trueAmountOnSaleElements ? true : false);
        }
    }
}
