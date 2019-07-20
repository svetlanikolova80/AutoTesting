using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Diagnostics;
using System.Reflection;


namespace AutoTests6Examples
{
    [TestClass]
    public class RefactorMe
    {

        IWebDriver driver = new FirefoxDriver();
        
        string url = "http://www.abv.bg";
        string userNameHTMLDOMID = "username";
        string passwordHTMLDOMID = "password";
        string submitButtonHTMLDOMID = "loginBut";
        string userName = "studentqacourse";
        string password = "studentqacourse";
        string expectedURL = "https://nm20.abv.bg/Mail.html";
        string userFullNameHTMLDOMClass = "userName";
        string fullName = "studentqacourse studentqacourse";
        string composeEmailButtonHTMLDOMClass = "abv-button";
        string composeEmailButtonInnerText = "Напиши";
        string sendEmailButtonHTMLDOMClass = "abv-button";
        string sendEmailButtonInnerText = "Изпрати";
        string email = "studentqacourse@abv.bg";
        string toHTMLDOMClass = "fl";
        string testDummyData = "test dummy data";
        string subjectHTMLDOMClass = "gwt-TextBox";
        string classNameAttributeOfIFrameValue = "gwt-RichTextArea";
        string mailBoxInnerText = "Кутия";

        static Random random = new Random();
        int randomNumber = random.Next(0, 100);

        string wrongExpectedURL = "https://nm50.abv.bg/Mail.html";
        
        string issueTrackerURL = "https://github.com/studentqacourse/IssueTracker";
        string gitHubSignInButtonInnerText = "Sign in";
        string gitHubLoginFieldHTMLDOMID = "login_field";
        string gitHubPasswordFieldHTMLDOMID = "password";
        string issues = "Issues";
        string newIssueButtonHTMLDOMClass = "btn btn-primary right";
        string issueTitleHTMLDOMID = "issue_title";
        string issueBodyHTMLDOMID = "issue_body";
        string submitNewIssueHTMLDOMClass = "btn btn-primary";
        string issueTitleHTMLDOMClass = "js-issue-title";

        [TestMethod]
        public void TestSendEmail()
        {
            try
            {
                this.NavigateToURL(this.url);
                this.BrowserMaximize();
                this.TypeTextInAField(userName, "//input[@id='" + userNameHTMLDOMID + "']");
                this.WaitForElementPresentByXPath("//input[@id='" + passwordHTMLDOMID + "']", 10);
                this.TypeTextInAField(password, "//input[@id='" + passwordHTMLDOMID + "']");
                this.WaitForElementPresentByXPath("//input[@id='" + submitButtonHTMLDOMID + "']", 10);
                this.ClickOnAnElement("//input[@id='" + submitButtonHTMLDOMID + "']");
                this.AssertCorrectNavigation(expectedURL);
                this.AssertInnerTextOfAnElement(fullName, "//div[@class='" + userFullNameHTMLDOMClass + "']");
                this.ClickOnAnElement("//div[@class='" + composeEmailButtonHTMLDOMClass + "']");
                string emailCounterBeforeNewEmail = GetInnerTextOfElement("//div[.='" + mailBoxInnerText + "']/..//em");

                this.ClickOnASpecificElement("//div[@class='" + composeEmailButtonHTMLDOMClass + "']", "class", composeEmailButtonInnerText);
                this.WaitForElementPresentByClassName(sendEmailButtonHTMLDOMClass, 10);
                this.TypeTextInAField(email, "//input[@class='" + toHTMLDOMClass + "']");
                this.TypeTextInAField(testDummyData + " " + randomNumber, "//input[@class='" + subjectHTMLDOMClass + "']");
                this.WaitSeconds(10);
                this.TypeInIFrame(classNameAttributeOfIFrameValue, testDummyData);
                this.ClickOnASpecificElement("//div[@class='" + sendEmailButtonHTMLDOMClass + "']", "class", sendEmailButtonInnerText);

                Thread.Sleep(1234);

                this.driver.Navigate().Refresh();
                string emailCounterAfterNewEmail = GetInnerTextOfElement("//div[.='" + mailBoxInnerText + "']/..//em");
                Assert.AreEqual((Int32.Parse(emailCounterBeforeNewEmail) + 1), Int32.Parse(emailCounterAfterNewEmail));

                this.ClickOnAnElement("//div[.='" + mailBoxInnerText + "']");
                bool isElementPresent = this.driver.FindElement(By.XPath("//div[.='" + testDummyData + " " + randomNumber + "']")).Displayed;
                Assert.AreEqual(true, isElementPresent);
            }
            catch (Exception e)
            {
                this.TrackAnIssue(e);
            }
        }

        [TestMethod]
        public void TestRedirectAfterLoginShouldFail()
        {
            try
            {
                this.NavigateToURL(this.url);
                this.BrowserMaximize();
                this.TypeTextInAField(userName, "//input[@id='" + userNameHTMLDOMID + "']");
                this.WaitForElementPresentByXPath("//input[@id='" + passwordHTMLDOMID + "']", 10);
                this.TypeTextInAField(password, "//input[@id='" + passwordHTMLDOMID + "']");
                this.WaitForElementPresentByXPath("//input[@id='" + submitButtonHTMLDOMID + "']", 10);
                this.ClickOnAnElement("//input[@id='" + submitButtonHTMLDOMID + "']");
                Thread.Sleep(1234);
                this.AssertCorrectNavigation(wrongExpectedURL);

            }
            catch (Exception e)
            {
                this.TrackAnIssue(e);
            }
        }

        [TestMethod]
        public void TrackAnIssue(Exception e)
        {
            string exceptionMessage = e.Message;

            this.NavigateToURL(this.issueTrackerURL);
            this.BrowserMaximize();
            this.ClickOnAnElement("//a[.='" + gitHubSignInButtonInnerText + "']");
            this.WaitForElementPresentByXPath("//h1[.='" + gitHubSignInButtonInnerText + "']", 10);
            this.TypeTextInAField(userName, "//input[@id='" + gitHubLoginFieldHTMLDOMID + "']");
            this.WaitForElementPresentByXPath("//input[@id='" + gitHubPasswordFieldHTMLDOMID + "']", 10);
            this.TypeTextInAField((password + "1"), "//input[@id='" + gitHubPasswordFieldHTMLDOMID + "']");
            this.WaitForElementPresentByXPath("//input[@value='" + gitHubSignInButtonInnerText + "']", 10);
            this.ClickOnAnElement("//input[@value='" + gitHubSignInButtonInnerText + "']");
            this.WaitForElementPresentByXPath("//span[.='" + issues + "']", 10);
            this.ClickOnAnElement("//span[.='" + issues + "']");
            this.WaitForElementPresentByXPath("//a[@class='" + newIssueButtonHTMLDOMClass + "']", 123);
            this.ClickOnAnElement("//a[@class='" + newIssueButtonHTMLDOMClass + "']");
            this.WaitSeconds(10);

            System.Diagnostics.StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            MethodBase currentMethodName = sf.GetMethod();
            string exception = "";

            this.TypeTextInAField(currentMethodName.ToString().Substring(5) + " failed", "//input[@id='" + issueTitleHTMLDOMID + "']");
            this.TypeTextInAField("with \n" + exceptionMessage, "//textarea[@id='" + issueBodyHTMLDOMID + "']");
            this.ClickOnAnElement("//button[@class='" + submitNewIssueHTMLDOMClass + "']");
            this.WaitForElementPresentByXPath("//span[@class='" + issueTitleHTMLDOMClass + "']", 123);
            Assert.AreEqual(currentMethodName.ToString().Substring(5) + " failed", this.GetInnerTextOfElement("//span[@class='" + issueTitleHTMLDOMClass + "']"));

            throw new Exception(String.Format("The exception is: ", exception), e);
        }
                               
        public void NavigateToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            
        }

        public void BrowserMaximize()
        {
            driver.Manage().Window.Maximize();
        }

        public void TypeTextInAField(string text, string fieldXPath)
        {
            IWebElement field = driver.FindElement(By.XPath(fieldXPath));
            field.SendKeys(text);
        }

        public void ClickOnAnElement(string elementXPath)
        {
            IWebElement element = driver.FindElement(By.XPath(elementXPath));
            element.Click();
        }

        public void WaitSeconds(int seconds)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
        }
                
        public void AssertCorrectNavigation(string expectedURL)
        {
            string actualURL = driver.Url;
            Assert.AreEqual(expectedURL, actualURL);
        }

        public void AssertInnerTextOfAnElement(string expectedInnerText, string elementXPath)
        {
            IWebElement element = driver.FindElement(By.XPath(elementXPath));
            string actualInnerText = element.Text;
            Assert.AreEqual(expectedInnerText, actualInnerText);
        }

        public void ClickOnASpecificElement(string elementXPath, string tagAttribute, string expectexInnerText)
        {
            IWebElement element = driver.FindElement(By.XPath(elementXPath));

            if (expectexInnerText == element.Text && element.GetAttribute(tagAttribute) == composeEmailButtonHTMLDOMClass)
            {
                element.Click();
            }

        }

        public void WaitForElementPresentByClassName(string className, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.ClassName(className));
            });
        }

        public void WaitForElementPresentByXPath(string xPath, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.XPath(xPath));
            });
        }

        public void TypeInIFrame(string classNameAttributeOfIFrameValue, string textToType) 
        {
            IWebElement iFrame = driver.FindElement(By.XPath("//iframe[@class='" + classNameAttributeOfIFrameValue + "']"));
            driver.SwitchTo().Frame(iFrame);

            IWebElement bodyInIFrame = driver.FindElement(By.TagName("body"));
            bodyInIFrame.Click();
            bodyInIFrame.SendKeys(textToType);
            driver.SwitchTo().DefaultContent();
        }

        public string GetInnerTextOfElement(string elementXPath) 
        {
            IWebElement element = driver.FindElement(By.XPath(elementXPath));
            string resultInnerText = element.Text;
            return resultInnerText;
        }


    }
}
