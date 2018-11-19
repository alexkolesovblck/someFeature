using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace Tests.Settings.Helper
{
    public class SetUp
    {
        protected IWebDriver driver
        {
            [BeforeScenario]
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    ScenarioContext.Current["browser"] = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                }
                return (IWebDriver)ScenarioContext.Current["browser"];
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
                driver.Quit();
        }

        public void openYandex()
        {
            driver.Navigate().GoToUrl(TestParams.baseUrl);
        }

        public IWebElement WaitForElement(By locator)
        {
            WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return waitForElement.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement FindElementFromCssSelector(string path)
        {
            return driver.FindElement(By.CssSelector(path));
        }

        public IWebElement FindElementFromTagName(string path)
        {
            return driver.FindElement(By.TagName(path));
        }

        public IWebElement FindElementFromXpath(string path)
        {
            return driver.FindElement(By.XPath(path));
        }

        public IWebElement FindElementFromClassName(string path)
        {
            return driver.FindElement(By.ClassName(path));
        }

        public IWebElement FindElementFromId(string path)
        {
            return driver.FindElement(By.Id(path));
        }

        public IWebElement FindElementFromLinkText(string path)
        {
            return driver.FindElement(By.LinkText(path));
        }

        public bool ElementIsVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }
}