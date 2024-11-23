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
        /// Searches for a keyword and navigates to the details of the specified news item on the search results page.
        /// </summary>
        /// <param name="newsIndex">The position of the news article to open through search bar results.</param>
        [TestCase(6)] // Example input for the 8th article
        //[TestCase(3)] // Example input for the 3rd article
        public void TestSearchBarAndArticleNavigation(int articleIndex)
        {
            // Initialize WebDriverWait with a timeout
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            // Wait for the search button and click it
            var searchButton = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"cmsmasters-scroll-top\"]/div/div/div/section/div/div[3]/div/div[2]/div/div/div[2]/div"))); // Replace with the actual class or locator for the search button
            searchButton.Click();

            var searchBar = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"cmsmasters-scroll-top\"]/div/div/div/section/div/div[3]/div/div[2]/div/div/div[1]/div/form/div/input")));
            searchBar.SendKeys("İstanbul");
            searchBar.Submit();

            //// Wait for the search results container to load and obtain articles
            var resultsContainer = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"cmsmasters_body\"]/div[2]/div/div/section[2]/div/div[1]/div/div/div/div/div[2]/div/div")));

            var articles = resultsContainer.FindElements(By.TagName("article"));

            // Obtain selectedArticle using articleIndex and start and check for navigation
            var selectedArticle = articles[articleIndex - 1];
            var articleLink = selectedArticle.FindElement(By.TagName("a"));

            var jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", articleLink);
            wait.Until(driver => articleLink.Displayed && articleLink.Enabled);

            string hrefValue = articleLink.GetAttribute("href");
            jsExecutor.ExecuteScript("arguments[0].click();", articleLink);

            // Check if the driver URL matches the href value
            wait.Until(driver => driver.Url == hrefValue);
        }
    }
}
