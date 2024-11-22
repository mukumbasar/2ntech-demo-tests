using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using _2NTech.Demo.Tests.Bases;


namespace _2NTech.Demo.Tests.Sites._2NHaber.Components.SearchBar
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        /// <summary>
        /// Searches for a keyword and navigates to the details of the specified news item on the search results page.
        /// </summary>
        /// <param name="newsIndex">The position of the news article to open through search bar results.</param>
        [TestCase(8)] // Example input for the 8th article
        [TestCase(3)] // Example input for the 3rd article
        public void TestSearchAndOpenNewsDetails(int newsIndex)
        {
            
        }
    }
}
