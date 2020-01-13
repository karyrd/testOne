using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.IO;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestFramework.Driver
{
    public class DriverController
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver(string browser = "")
        { 
            if (_driver == null)
            {
                FirefoxDriverService firefoxService;
                ChromeDriverService chromeService;
                switch (browser)
                {
                    case "firefox":
                        //firefoxService = FirefoxDriverService.CreateDefaultService("D:\\4kyrs\\Tests\\selenium_drivers");
                        firefoxService = FirefoxDriverService
                            .CreateDefaultService($"{Directory.GetCurrentDirectory()}\\Driver\\selenium_drivers");
                        _driver = new FirefoxDriver(firefoxService);
                        break;

                    case "chrome":
                        //chromeService = ChromeDriverService.CreateDefaultService("D:\\4kyrs\\Tests\\selenium_drivers");
                        chromeService = ChromeDriverService
                            .CreateDefaultService($"{Directory.GetCurrentDirectory()}\\Driver\\selenium_drivers");
                        _driver = new ChromeDriver(chromeService);
                        break;

                    default:
                        // new DriverManager().SetUpDriver(new ChromeConfig());
                        //chromeService = ChromeDriverService.CreateDefaultService("D:\\4kyrs\\Tests\\selenium_drivers");
                        chromeService = ChromeDriverService
                            .CreateDefaultService($"{Directory.GetCurrentDirectory()}\\Driver\\selenium_drivers");
                        _driver = new ChromeDriver(chromeService);
                        break;
                }
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            return _driver;
        }

        public static void CloseBrowser()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
