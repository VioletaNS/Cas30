using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace Cas30
{
    class SeleniumTests
    {
        IWebDriver Driver;
        WebDriverWait Wait;
        [Test]
        [Category("Create"), Category("Site : test.qa.rs")]
        public void TestCreateNewUser()
        {
            GoTo("https://test.qa.rs/");
            IWebElement LinkCreate = Wait.Until
                (EC.ElementIsVisible(By.LinkText("Kreiraj novog korisnika")));
            if(LinkCreate.Displayed && LinkCreate.Enabled)
            {
                LinkCreate.Click();
            }
            System.Threading.Thread.Sleep(5000);
        }
         
        public void GoTo(string Url)
        {
            Driver.Navigate().GoToUrl(Url);
        }

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            Wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
            Driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void TearDown()
        {

        }
    }

}
