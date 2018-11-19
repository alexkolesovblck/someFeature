using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Tests.PageObject
{
    class PageObjectIDs
    {
        /// <summary>Кнопка "Показать подходящие"</summary>
        [FindsBy(How = How.CssSelector, Using = "a[class='button button_size_l button_theme_pseudo i-bem button_action_show-filtered n-filter-panel-extend__controll-button_size_big button_js_inited']")]
        public IWebElement PresentBtn { get; set; }

        /// <summary>Кнопка "Сортировать по ..."</summary>
        [FindsBy(How = How.CssSelector, Using = "button[class='button button_theme_normal button_arrow_down button_size_s select__button i-bem button_js_inited']")]
        public IWebElement SortBtn { get; set; }

        /// <summary>Кнопка "Поиск"</summary>
        [FindsBy(How = How.CssSelector, Using = "button[class='button2 button2_size_ml button2_type_submit button2_pin_brick-round i-bem suggest2-form__button button2_theme_gray button2_js_inited']")]
        public IWebElement SearchBtn { get; set; }

        /// <summary>Локатор таблицы с ценником</summary>
        [FindsBy(How = How.CssSelector, Using = "div[class='n-snippet-list n-snippet-list_type_grid snippet-list_size_3 metrika b-zone b-spy-init b-spy-events i-bem metrika_js_inited snippet-list_js_inited b-spy-init_js_inited b-zone_js_inited']")]
        public IWebElement TableGridWithPrice { get; set; }
    }
}

