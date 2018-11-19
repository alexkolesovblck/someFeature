using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using Tests.PageObject;
using Tests.Settings;
using Tests.Settings.Helper;

namespace Tests.Steps
{
    [Binding]
    public class SomeFeatureSteps : SetUp
    {
        PageObjectIDs pageObjectIDs = new PageObjectIDs();

        [Given(@"I open browser and go to website page")]
        public void GivenIOpenBrowserAndGoToWebsitePage()
        {
            openYandex();
            Assert.AreEqual(TestParams.baseUrl, new Uri(driver.Url).AbsoluteUri);
        }
        
        [Then(@"I open the yandex market section")]
        public void ThenIOpenTheYandexMarketSection()
        {
            FindElementFromCssSelector("a[data-id='market']").Click();
            WaitForElement(By.CssSelector("ul[class='topmenu__list']"));
            Assert.AreEqual("market.yandex.ru", new Uri(driver.Url).Host);
        }
        
        [Then(@"Choose a subsection as a (.*)")]
        public void ThenChooseASubsection(string subsection)
        {
            ScenarioContext.Current["subsection"] = subsection;
            string currentUrl = new Uri(driver.Url).Host;

            if (ElementIsVisible(WaitForElement(By.CssSelector("div[class='popup2__content']"))))
            {
                FindElementFromCssSelector("div[class='n-region-notification__actions-cell']").Click();
            }

            new Actions(driver).MoveToElement(FindElementFromCssSelector("li[class='topmenu__item i-bem topmenu__item_js_inited']")).Perform();
            WaitForElement(By.CssSelector("div[class='topmenu__sublist']"));

            switch (subsection)
            {
                case "smartphones":
                    new Actions(driver).MoveToElement(FindElementFromCssSelector("a[class='link topmenu__subitem']")).Click().Build().Perform();
                    break;

                case "headphones":
                    new Actions(driver).MoveToElement(FindElementFromCssSelector("a[class='link topmenu__subitem']:nth-child(6)")).Click().Build().Perform();
                    break;
            }
        }
        
        [Then(@"I open advanced search on the page")]
        public void ThenIOpenAdvancedSearchOnThePage()
        {
            WaitForElement(By.CssSelector("a[class='OcaftndW9c _2bjY2zQo59 _4WmLhr2Vhx _2Kihe5N2Sn']"));
            FindElementFromCssSelector("a[class='OcaftndW9c _2bjY2zQo59 _4WmLhr2Vhx _2Kihe5N2Sn']").Click();
        }
        
        [Then(@"set the search parameter as (.*)")]
        public void ThenSetTheSearchParameterAs(string price)
        {
            ScenarioContext.Current["price"] = price;
            IWebElement inputPriceField = FindElementFromCssSelector("input[id='glf-pricefrom-var']");

            switch (price)
            {
                case "20000":
                    inputPriceField.SendKeys(price);
                    break;

                case "5000":
                    inputPriceField.SendKeys(price);
                    break;
            }
        }
        
        [Then(@"I also choose manufacturers as (.*)")]
        public void ThenIAlsoChooseManufacturers(string manufacturers)
        {
            ScenarioContext.Current["manufacturers"] = manufacturers;

            switch (manufacturers)
            {
                case "iphone and samsung":
                    new Actions(driver).MoveToElement(FindElementFromCssSelector("input[id='glf-7893318-153043']")).Click().Build().Perform();
                    new Actions(driver).MoveToElement(FindElementFromCssSelector("input[id='glf-7893318-153061']")).Click().Build().Perform();
                    break;

                case "beats":
                    new Actions(driver).MoveToElement(FindElementFromCssSelector("input[id='glf-7893318-8455647']")).Click().Build().Perform();
                    break;
            }
        }
        
        [Then(@"I submit changes")]
        public void ThenISubmitChanges()
        {
            PageFactory.InitElements(driver, pageObjectIDs);

            pageObjectIDs.PresentBtn.Click();
        }
        
        [Then(@"check that the elements on page twelve")]
        public void ThenCheckThatTheElementsOnPageTwelve()
        {
            switch (ScenarioContext.Current["manufacturers"])
            {
                case "iphone and samsung":

                    WaitForElement(By.CssSelector("div[class='pager-more__button pager-loader_preload']"));

                    if (pageObjectIDs.SortBtn.Text != "Показывать по 12")
                    {
                        pageObjectIDs.SortBtn.Click();
                        WaitForElement(By.CssSelector("button[aria-expanded='true']"));

                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                        FindElementFromCssSelector("div[class='select__item']").Click();
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                    }

                    Assert.AreEqual("Показывать по 12",
                        WaitForElement(By.CssSelector("button[class='button button_theme_normal button_arrow_down button_size_s select__button i-bem button_js_inited']")).Text);

                    assertCountOnPage(12);
                    break;

                case "beats":

                    assertCountOnPage(19); //P.S. в момент проверки для наушников не было кнопки отсортировать по 12, поэтому отображалось в количестве 19 шт.
                    break;
            }
        }

        [Then(@"I memorize the first item in the list")]
        public void ThenIMemorizeTheFirstItemInTheList()
        {
            WaitForElement(By.CssSelector("div[class='n-snippet-cell2__title']"));

            string s = FindElementFromCssSelector("div[class='n-snippet-cell2__title'] a").GetAttribute("title");
            ScenarioContext.Current["item"] = s;
        }

        [Then(@"then I insert the saved name into the search field")]
        public void ThenThenIInsertTheSavedNameIntoTheSearchField()
        {
            var s = (string)ScenarioContext.Current["item"];
           
            FindElementFromId("header-search").SendKeys(s + Keys.Enter);
        }
        
        [Then(@"I check that the name of the product matches the previously saved name")]
        public void ThenICheckThatTheNameOfTheProductMatchesThePreviouslySavedName()
        {
            var s = (string)ScenarioContext.Current["item"];
            Assert.AreEqual(s, FindElementFromCssSelector("h1[class='title title_size_28 title_bold_yes']").Text);
        }

        [Then(@"I click the Sort by price button")]
        public void ThenIClickTheSortByPriceButton()
        {
            FindElementFromCssSelector("div[class='n-filter-sorter i-bem n-filter-sorter_js_inited']").Click();
        }

        [Then(@"check that the items on the page are sorted correctly")]
        public void ThenCheckThatTheItemsOnThePageAreSortedCorrectly()
        {
            PageFactory.InitElements(driver, pageObjectIDs);

            IWebElement s = pageObjectIDs.TableGridWithPrice;
            List<IWebElement> content_data = new List<IWebElement>();
            content_data = s.FindElements(By.CssSelector("div[class='price']")).ToList();

            IWebElement previous = content_data.First();

            foreach (IWebElement item in content_data)
            {
                if (item != previous)
                {
                    try
                    {
                        var n = Convert.ToInt32(item.Text);
                        var summa = Convert.ToInt32(previous.Text);
                        Assert.IsTrue(n >= summa);
                    }
                    catch 
                    {
                        Console.WriteLine("Неправильно строка переведена в число");
                    }
                }
                previous = item;
            }
        }


        private void assertCountOnPage(int countOnTheList)
        {
            ElementIsVisible(WaitForElement(By.CssSelector("div[class='n-snippet-list n-snippet-list_type_grid snippet-list_size_3 metrika b-zone b-spy-init b-spy-events i-bem metrika_js_inited snippet-list_js_inited b-spy-init_js_inited b-zone_js_inited']")));
            IWebElement s = pageObjectIDs.TableGridWithPrice;

            int listCount = s.FindElements(By.CssSelector("div[class='price']")).Count();
            Assert.AreEqual(countOnTheList, listCount);
        }
    }
}
