using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using _2NTech.Demo.Tests.Bases;

namespace _2NTech.Demo.Tests.Sites._2NHaber.Components.Navbar
{
    [TestFixture]
    public class NavbarTests : BaseTest
    {
        /// <summary>
        /// Tests all the navbar links by clicking each one and verifying the page URL.
        /// </summary>
        [Test]
        public void TestNavbarLinks()
        {
            // Obtain an WebDriveWait instance with a 20 secs of operational span
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            // Wait for and obtain the container and link elements
            var container = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"cmsmasters-scroll-top\"]/div/div/div/section/div/div[2]/div/div/div/nav")));
            var linkElements = container.FindElements(By.TagName("a"));

            foreach (var linkElement in linkElements)
            {
                // Wait for link to be enabled and check for href validity
                wait.Until(driver => linkElement.Enabled);
                string hrefValue = linkElement.GetAttribute("href");
                Assert.AreEqual(false, string.IsNullOrEmpty(hrefValue));

                // Click the link and wait for the page to load and make sure of the parity
                linkElement.Click();
                wait.Until(driver => driver.Url.Contains(hrefValue) && driver.Url == hrefValue);

                // Navigate back and wait for it to be finalized
                _driver.Navigate().Back();
                wait.Until(driver => driver.Url != hrefValue);
            }
        }
    }
}
