using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebDriverTest_Example
{
    [TestFixture]
    public class WebTests
    {
        const string login = "vadimvoyevoda";
        const string pass = "16101991";

        [Test]
        public void should_open_chrome()
        {
            //arrange
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.google.com.ua");
           
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(x => x.FindElement(By.Id("lst-ib")).Displayed);

            var searchId = driver.FindElement(By.Id("lst-ib"));
            searchId.SendKeys("WebDriver");
            searchId.SendKeys(Keys.Enter);

            wait.Until(x => x.FindElement(By.ClassName("srg")).Displayed);
            var src = driver.FindElement(By.ClassName("srg"));

            //act
            var res = src.FindElements(By.TagName("a"));

            //assert
            Assert.AreEqual("Selenium WebDriver", res[0].Text);
        }

        [Test]
        public void should_open_gmail_and_send_message()
        {
            //arrange
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.google.com.ua");

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            
            //act && assert
            GetElementOnXPath(driver, wait, "//*[@id=\"gbwa\"]/div[1]/a").Click();
            GetElementOnXPath(driver, wait, "//*[@id=\"gb23\"]").Click();
            GetElementOnXPath(driver, wait, "//*[@id=\"gmail-sign-in\"]").Click();
            
            GetElementOnXPath(driver, wait, "//*[@id=\"Email\"]").SendKeys(login);
            GetElementOnXPath(driver, wait, "//*[@id=\"next\"]").Click();
            GetElementOnXPath(driver, wait, "//*[@id=\"Passwd\"]").SendKeys(pass);
            GetElementOnXPath(driver, wait, "//*[@id=\"signIn\"]").Click();

            GetElementOnXPath(driver, wait, "//*[@id=\":j2\"]/div/div").Click();
            GetElementOnXPath(driver, wait, "//*[@id=\":oi\"]").SendKeys("vadimvoyevoda@gmail.com");
            GetElementOnXPath(driver, wait, "//*[@id=\":o3\"]").SendKeys("testMsg");
            GetElementOnXPath(driver, wait, "//*[@id=\":p5\"]").SendKeys("Bu-ga-ga!!");
            GetElementOnXPath(driver, wait, "//*[@id=\":nt\"]").Click();

            string expectedText = "Повідомлення надіслано. Переглянути повідомлення";
            string actualText = GetElementOnXPath(driver, wait, "/html/body/div[7]/div[3]/div/div[1]/div[5]/div[1]/div[2]/div[3]/div/div/div[2]").Text;

            //Assert
            Assert.AreEqual(expectedText, actualText);
        }

        public IWebElement GetElementOnXPath(ChromeDriver driver, WebDriverWait wait, string xpath)
        {
            wait.Until(x => x.FindElement(By.XPath(xpath)).Displayed);
            return driver.FindElement(By.XPath(xpath));
        }
    }
}
