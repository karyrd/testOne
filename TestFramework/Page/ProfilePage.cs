using OpenQA.Selenium;
using System.Threading;
using TestFramework.Constants;
using TestFramework.Test.Base;

namespace TestFramework.Page
{
    public class ProfilePage : PageBase
    {
        private const string _surnameField = "//*[@id='vue-root']/div[3]/div/div[2]/div/div/div/form[1]/div[1]/dl[1]/dd/div[1]/input";
        private const string _nameField = "//*[@id='vue-root']/div[3]/div/div[2]/div/div/div/form[1]/div[1]/dl[1]/dd/div[2]/input";
        private const string _submitButton = "//*[@id='vue-root']/div[3]/div/div[2]/div/div/div/form[1]/div[3]/button";

        public ProfilePage(IWebDriver driver)
            : base(driver)
        {
            Url = PageUrl.Profile;
        }

        public string GetLoggedUser()
        {
            var nameField = _driver.FindElement(By.XPath(_nameField));
            var name = nameField.GetAttribute("value");
            return name;
        }

        public ProfilePage ChangeName(string name)
        {
            var nameField = _driver.FindElement(By.XPath(_nameField));
            nameField.Clear();
            nameField.SendKeys(name);

            var submitButton = _driver.FindElement(By.XPath(_submitButton));
            submitButton.Click();
            Thread.Sleep(1000);

            return this;
        }
    }
}
