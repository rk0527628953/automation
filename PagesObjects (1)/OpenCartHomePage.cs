using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.PagesObjects
{
    public class OpenCartHomePage
    {
        private IWebDriver driver;
        private By searchField = By.Name("search");
        private By searchButton = By.CssSelector("button[class='btn btn-default btn-lg']");

        public OpenCartHomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Search(string query)
        {
            driver.FindElement(searchField).SendKeys(query);
            driver.FindElement(searchButton).Click();
        }
    }
}
