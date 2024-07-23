using NUnit.Framework.Interfaces;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumProject.PagesObjects;

namespace SeleniumProject
{
  //  [TestFixture(typeof(ChromeDriver))]
   // [TestFixture(typeof(FirefoxDriver))]
    [TestFixture]
    public class GoogleSearchTest
    {
        private IWebDriver driver;
        private GoogleHomePage googleHomePage;
        private GoogleResultsPage googleResultsPage;
     //   public static IEnumerable<TestData> TestCases => XmlDataReader.ReadTestData("C:\\Users\\USER\\source\\repos\\SeleniumProject\\SeleniumProject\\TestData.xml");


        [OneTimeSetUp]
        public void SetUp()
        {
            string path = "T:\\����� �����\\��� �\\����� �\\���� ��������\\�����5\\SeleniumProject\\SeleniumProject";

 �����������//Creates the ChomeDriver object, Executes tests on Google Chrome

������������driver = new ChromeDriver(path + @"\drivers\");
            googleHomePage = new GoogleHomePage(driver);
            googleResultsPage = new GoogleResultsPage(driver);
        }

     //1
        [Test]
        public void TestHandleAlert()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/alerts");

            // ����� ������ ����� alert ���� 5 �����
            var alertButton = driver.FindElements(By.Id("promtButton"))[0];
         //   var alertButton = driver.FindElements(By.Id("timerAlertButton"))[0];
          //  ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", alertButton);

            // ����� �� ������
            alertButton.Click();

            // ����� �-alert ������� ��
            IAlert alert = WaitForAlert(driver, TimeSpan.FromSeconds(10));
            ClassicAssert.IsNotNull(alert, "Alert was not displayed.");

            alert.Accept();
        }

        [Test]
        public void TestSwitchBetweenWindowsAndTabs()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");

            // ����� ������� ��������
         //   HideAds();

            // ����� �� ����� ������ ���� ���
            var windowButton = driver.FindElement(By.Id("windowButton"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", windowButton);

            // ����� ���� ����� ������
            string originalWindow = driver.CurrentWindowHandle;

            // ����� ����� ���
            WaitForNewWindow(driver, 2);

            // ���� ����� ����
            foreach (string windowHandle in driver.WindowHandles)
            {
                if (windowHandle != originalWindow)
                {
                    driver.SwitchTo().Window(windowHandle);
                    break;
                }
            }

            // ����� ������ �� ����� ����
            var newTabHeading = driver.FindElement(By.Id("sampleHeading"));
            ClassicAssert.AreEqual("This is a sample page", newTabHeading.Text);

            // ����� ����� ���� ����� ����� ������
            driver.Close();
            driver.SwitchTo().Window(originalWindow);
        }

        private IAlert WaitForAlert(IWebDriver driver, TimeSpan timeout)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                return wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        private void WaitForNewWindow(IWebDriver driver, int expectedWindowCount)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.WindowHandles.Count == expectedWindowCount);
        }

        private void HideAds()
        {
            try
            {
                // ����� �� iframes
                var iframes = driver.FindElements(By.TagName("iframe"));
                foreach (var iframe in iframes)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", iframe);
                }
            }
            catch (NoSuchElementException)
            {
                // �� �� ���� �� iframe, ������� �����
            }
        }
        [Test]
        public void TestChart()
        {
            // ����� ������ ��������
            driver.Navigate().GoToUrl("https://demo.opencart.com");
            string mainWindow = driver.CurrentWindowHandle;
            driver.ExecuteJavaScript("window.open();");
            var tabs = new List<string>(driver.WindowHandles);
            driver.SwitchTo().Window(tabs[1]);
            driver.Navigate().GoToUrl("https://demo.opencart.com/index.php?route=product/category&path=20");
            driver.Navigate().Back();
            // ����� Firefox
            //    firefoxDriver.Navigate().GoToUrl("https://demo.opencart.com");
            // firefoxDriver.ExecuteScript("window.open();");
            // var firefoxTabs = new List<string>(firefoxDriver.WindowHandles);
            // firefoxDriver.SwitchTo().Window(firefoxTabs[1]);
            // firefoxDriver.Navigate().GoToUrl("https://demo.opencart.com/index.php?route=product/category&path=20");

            // ������ ��������
            //OpenCartHomePage homePage = new OpenCartHomePage(driver);
            //  homePage.Search("iPhone");

            //   driver.SwitchTo().Window(mainWindow);
            // homePage.Search("MacBook");

            //  OpenCartHomePage firefoxHomePage = new OpenCartHomePage(firefoxDriver);
            // firefoxHomePage.Search("iPhone");

            // firefoxDriver.SwitchTo().Window(firefoxTabs[0]);
            //firefoxHomePage.Search("MacBook");
        }



        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
