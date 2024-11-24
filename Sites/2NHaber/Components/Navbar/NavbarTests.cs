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
        public void TestNavbar()
        {
            // Obtain an WebDriveWait instance with a 20 secs of operational span
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            // Wait for, obtain and check link elements
            var linkElements = wait.Until(driver => driver.FindElements(By.XPath("//*[@id=\"cmsmasters-scroll-top\"]/div/div/div/section/div/div[2]/div/div/div/nav//a")));
            Assert.IsTrue(linkElements.Count > 0, "The link does not exist.");

            foreach (var linkElement in linkElements)
            {
                // Check for href
                wait.Until(driver => linkElement.Displayed && linkElement.Enabled);
                string hrefValue = linkElement.GetAttribute("href");
                Assert.AreEqual(false, string.IsNullOrEmpty(hrefValue));

                // Click the link and wait for the page to load and make sure of the parity
                linkElement.Click();
                wait.Until(driver => driver.Url.Contains(hrefValue) && driver.Url == hrefValue);
                Assert.AreEqual(hrefValue, wait.Until(driver => driver.Url), "The navigation did not reach the expected URL.");

                // Navigate back and wait for it to be finalized
                _driver.Navigate().Back();
                wait.Until(driver => driver.Url != hrefValue);
                Assert.AreNotEqual(hrefValue, wait.Until(driver => driver.Url), "The navigation reached an unexpected URL.");
            }
        }
    }
}
