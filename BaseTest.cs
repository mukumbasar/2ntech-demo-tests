using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace _2NTech.Demo.Tests
{
    public class BaseTest
    {
        protected IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Navigate().GoToUrl("https://2nhaber.com/");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
