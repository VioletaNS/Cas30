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
            GoTo("http://test.qa.rs/");
            IWebElement LinkCreate = Wait.Until
                (EC.ElementIsVisible(By.LinkText("Kreiraj novog korisnika")));
            if(LinkCreate.Displayed && LinkCreate.Enabled)
            {
                LinkCreate.Click();
            }
            IWebElement InputName = Wait.Until(EC.ElementIsVisible(
                By.Name("ime")));
            if (InputName.Displayed && InputName.Enabled)
            {
                InputName.SendKeys(Functions.RandomAplpha(15 //Functions.Random(10 , 20)
                    ));
            }
            IWebElement InputLastName = FindElement(By.Name("prezime"));
            if(InputLastName != null)
            { 
                InputLastName.SendKeys("Pilipovic");
            }
            IWebElement InputUserName = FindElement(By.Name("korisnicko"));
            InputUserName?.SendKeys(Functions.RandomAplphaNumeric(Functions.Random(10, 20)));

            IWebElement InputEmail = FindElement(By.Name("email"));
            InputEmail?.SendKeys(Functions.RandomEmail());

            IWebElement InputPhone = FindElement(By.Name("telefon"));
            InputPhone.SendKeys(Functions.RandomTelephone());

            IWebElement InputCountry = FindElement(By.Name("zemlja"));
            if(InputCountry != null)
            {
                SelectElement SelectCountry = new SelectElement(InputCountry);
                SelectCountry.SelectByText("Sweden");
                SelectCountry.SelectByValue("fin");
                SelectCountry.SelectByIndex(6);
            }
            IWebElement InputCity = Wait.Until(EC.ElementIsVisible(By.Name("grad")));

            if(InputCity.Displayed && InputCity.Enabled)
            {
                SelectElement SelectCity = new SelectElement(InputCity);
                int NumberOfOptions = SelectCity.Options.Count;
                Random Rnd = new Random ();
                SelectCity.SelectByIndex(Rnd.Next(1, NumberOfOptions -1));
            } 
            IWebElement InputStreet = FindElement (By.XPath("//div[@id='address']//input"));
            InputStreet.SendKeys(Functions.RandomAplpha(Functions.Random(10, 20)) + "" +
                Functions.Random(1 , 100));

            IWebElement InputGender = FindElement(By.Id("pol_m"));
            InputGender.Click();

            IWebElement ButonRegister = FindElement(By.Name("register"));
            ButonRegister.Click();
            System.Threading.Thread.Sleep(5000);
        }
        public IWebElement FindElement(By Selector)
        {
            IWebElement ReturnElement = null;
            try
            {
                ReturnElement = Driver.FindElement(Selector);
            }
            catch (NoSuchElementException)
            {

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return ReturnElement;
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
            Driver.Close();
        }
    }

}
