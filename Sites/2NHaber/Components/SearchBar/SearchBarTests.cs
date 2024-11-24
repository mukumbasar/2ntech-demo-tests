using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using _2NTech.Demo.Tests.Bases;
using System.ComponentModel;


namespace _2NTech.Demo.Tests.Sites._2NHaber.Components.SearchBar
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        /// <summary>
        /// Searches for a keyword and navigates to the details of the selected article.
        /// </summary>
        /// <param name="newsIndex">The position of the news article to open through search bar results.</param>
        [TestCase(8)] // Test for the 8th article
        public void TestSearchBarAndArticleNavigation(int articleIndex)
        {
            // Initialize WebDriverWait with a timeout
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            // Wait and check for the search button and click it
            var searchButton = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"cmsmasters-scroll-top\"]/div/div/div/section/div/div[3]/div/div[2]/div/div/div[2]/div")));
            Assert.IsNotNull(searchButton, "The search button does not exist.");
            searchButton.Click();

            // Wait and check for the search bar button and submit "İstanbul" with it
            var searchBar = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"cmsmasters-scroll-top\"]/div/div/div/section/div/div[3]/div/div[2]/div/div/div[1]/div/form/div/input")));
            Assert.IsNotNull(searchBar, "The search bar does not exist.");
            searchBar.SendKeys("İstanbul");
            searchBar.Submit();

            //// Wait for the search results container to load and obtain articles
            var articles = wait.Until(driver => driver.FindElements(By.XPath("//*[@id=\"cmsmasters_body\"]/div[2]/div/div/section[2]/div/div[1]/div/div/div/div/div[2]/div/div//article")));
            Assert.IsTrue(articles.Count > 0, "No articles were found.");

            // Obtain and check article anchor and href value from selectedArticle using articleIndex
            Assert.IsTrue(articleIndex > 0 && articleIndex <= articles.Count, "The selected article does not exist.");
            var selectedArticle = articles[articleIndex - 1];
            var articleAnchor = selectedArticle.FindElement(By.TagName("a"));
            string hrefValue = articleAnchor.GetAttribute("href");
            Assert.IsNotNull(hrefValue, "The hrefValue does not exist.");

            // Instance a jsExecutor, scroll into view of the anchor and click to start navigation
            var jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", articleAnchor);
            wait.Until(driver => articleAnchor.Displayed && articleAnchor.Enabled);
            jsExecutor.ExecuteScript("arguments[0].click();", articleAnchor);

            // Check if the driver URL matches the href value
            Assert.AreEqual(hrefValue, wait.Until(driver => driver.Url), "The navigation did not reach the expected URL.");
        }
    }
}
