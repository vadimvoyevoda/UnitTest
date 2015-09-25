using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeTest
{
    public class FacebookAuthorizeHelper
    {
        const string _goTo = "https://www.facebook.com/";
        const string _emailFieldId = "email";
        const string _passFieldId = "pass";
        const string _loginbtnFieldId = "loginbutton";
        IWebDriver _driver;

        public FacebookAuthorizeHelper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void RedirectToAuthorizePage()
        {
            _driver.Navigate().GoToUrl(_goTo);
        }

        public void FacebookAuthorize(string login, string pass)
        {
            _driver.FindElement(By.Id(_emailFieldId)).SendKeys(login);
            _driver.FindElement(By.Id(_passFieldId)).SendKeys(pass);
            _driver.FindElement(By.Id(_loginbtnFieldId)).Click();
        }


    }
}
