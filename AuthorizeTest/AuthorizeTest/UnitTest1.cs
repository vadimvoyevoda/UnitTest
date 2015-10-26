using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Credentials;

namespace AuthorizeTest
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver _driver;
        FacebookAuthorizeHelper _helper;
        const string errorMsg = "Неверный электронный адрес";

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TestClean()
        
        {
            _driver = null;
        }
        
        [TestMethod]
        public void IncorrectAuthorize()
        {
            //Arrange
            _helper = new FacebookAuthorizeHelper(_driver);
            _helper.RedirectToAuthorizePage();
            
            //Act
            _helper.FacebookAuthorize("lalalalal@la.la", "1111111111");
                        
            //Assert
            var errorText = _driver.FindElement(By.XPath("//*[@id=\"login_form\"]/div[1]/div[1]")).Text;

            Assert.AreEqual(errorMsg, errorText);
        }

        [TestMethod]
        public void CorrectAuthorize()
        {
            //Arrange
            _helper = new FacebookAuthorizeHelper(_driver);
            _helper.RedirectToAuthorizePage();

            //Act
            _helper.FacebookAuthorize(CredentialClass.Login, CredentialClass.Pass);

            IWebElement res = null;
            res = _driver.FindElement(By.XPath("//*[@id=\"blueBarNAXAnchor\"]/div[1]/div/div/div[2]/ul/li[1]/a/span"));
            
            //Assert
            Assert.AreNotEqual(null, res);                        
        }
    }
}
