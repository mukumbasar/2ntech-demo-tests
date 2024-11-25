using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using _2NTech.Demo.Tests.Bases;
using System.IO;

namespace _2NTech.Demo.Tests.Sites._2NHaber.Components.Form
{
    [TestFixture]
    public class CandidateFormTests : BaseTest
    {
        // Override BaseUrl with the current preference
        protected override string BaseUrl => "https://www.2ntech.com.tr/hr";

        [Test]
        public void TestFormSubmission()
        {
            // Obtain an WebDriveWait instance with a 20 secs of operational span
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            // Wait and check for the form
            var form = wait.Until(driver => driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div[2]/form")));
            Assert.IsTrue(form.Enabled, "The form is not enabled.");

            // Wait, check and provide data for the name-surname input
            var nameSurnameInput = form.FindElement(By.Id("name"));
            Assert.IsTrue(nameSurnameInput.Enabled, "The name-surname input is not enabled.");
            nameSurnameInput.SendKeys("Örnek Ad Soyad");

            // Wait, check and provide formatted data for the birth date input
            var birthDateInput = form.FindElement(By.Id("birth"));
            Assert.IsTrue(birthDateInput.Enabled, "The birth date field is not enabled.");
            DateTime birthDateValue = new DateTime(1991, 01, 11);
            string formattedBirthDate = birthDateValue.ToString("MM/dd/yyyy");
            birthDateInput.SendKeys(formattedBirthDate);

            // Wait, check and provide data for the id input
            var idInput  = form.FindElement(By.Id("tcKimlik"));
            Assert.IsTrue(idInput.Enabled, "The id input is not enabled.");
            idInput.SendKeys("55555555555");

            // Wait, check and provide data for the phone num input
            var phoneNumInput = form.FindElement(By.Id("phone"));
            Assert.IsTrue(phoneNumInput.Enabled, "The phone num input is not enabled.");
            phoneNumInput.SendKeys("55555555555");

            // Wait, check and provide data for the email input
            var emailInput = form.FindElement(By.Id("email"));
            Assert.IsTrue(emailInput.Enabled, "The email input is not enabled.");
            emailInput.SendKeys("5555555555555@55555555.com");

            // Wait, check and provide data for the cv input
            var fileInput = form.FindElement(By.Id("cv_field"));
            Assert.IsTrue(idInput.Enabled, "The file input is not enabled.");
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Assets", "placeholder-resume.pdf");
            filePath = Path.GetFullPath(filePath);
            fileInput.SendKeys(filePath);

            // Instance a jsExecutor, scroll into views of the buttons to click the said buttons - undergraduate, next, test engineer and submit buttons
            var jsExecutor = (IJavaScriptExecutor)_driver;
            var undergraduateButton = wait.Until(driver => driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div[2]/form/div[5]/div/button[2]")));
            Assert.IsTrue(undergraduateButton.Enabled, "The undergraduate button is not enabled.");
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", undergraduateButton);
            jsExecutor.ExecuteScript("arguments[0].click();", undergraduateButton);

            var nextButton = wait.Until(driver => driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div[2]/form/div[7]/button")));
            Assert.IsTrue(nextButton.Enabled, "The next button is not enabled.");
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", nextButton);
            jsExecutor.ExecuteScript("arguments[0].click();", nextButton);

            var testEngineerButton = wait.Until(driver => driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div[2]/div[3]/div[1]/div/div[2]")));
            Assert.IsTrue(testEngineerButton.Enabled, "The test engineer button is not enabled.");
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", testEngineerButton);
            jsExecutor.ExecuteScript("arguments[0].click();", testEngineerButton);

            var submitButton = wait.Until(driver => driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div[2]/div[3]/div[2]/div[2]")));
            Assert.IsTrue(submitButton.Enabled, "The submit button is not enabled.");
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            jsExecutor.ExecuteScript("arguments[0].click();", submitButton);

            // Once the submit button is clicked, check for the expected navigation and then for the touch notification to compare the text
            // with the expected status
            string targetUrl = "https://www.2ntech.com.tr/success";
            string currentUrl = wait.Until(driver => driver.Url);

            if (currentUrl != targetUrl)
            {
                var notification = wait.Until(d => d.FindElement(By.XPath("//span[@role='status' and @aria-live='assertive']")));
                Assert.IsTrue(notification.Displayed, "Touch notification not found.");

                string notificationMessage = notification.Text;
                Assert.AreEqual("Notification Bir hata oluştu.Daha Önce Bu Programa Başvurunuz Bulunmaktadır.", notificationMessage, "The notification message is not as expected.");
            }
            else
            {
                Assert.AreEqual("https://www.2ntech.com.tr/success", currentUrl, "The navigation did not reach the expected URL.");
            }
        }
    }
}
