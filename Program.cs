using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace LegoGuideLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please choose an option: 1 for Franchise , 2 for Campaign");
            var platform = Console.ReadLine();
            switch (platform)
            {
                case "1": Console.WriteLine("franchise");
                    break;
                case "2": Console.WriteLine("campaign");
                    break;
                default: Console.WriteLine("invalid input");
                    break;
            }
            Console.WriteLine("Enter Url : ");
            var urlInput = Console.ReadLine();
            //var input = "http://www.lego.com/dimensions";
            //var input = "http://ninjagocompetition.lego.com";

            //IWebDriver driver = new ChromeDriver(@"C:\test1\LegoGuideLine\bin\Debug");
            IWebDriver driver = new FirefoxDriver();

            driver.Navigate().GoToUrl(urlInput);
            driver.Navigate().Refresh();
            driver.Manage().Window.Maximize();

            //var pageSrc = Driver.PageSource;
            //const string expectedHeader = "lego-global-header-outer-wrap";
            //const string expectedFooter = "lego-global-footer";
            //const string expectedStickyFooter = "GFSticky";
            //const string siteTrackingEnabledforCampaignWithSecure = "https://cache.lego.com/r/www/r/Analytics/Modules/TrackManApi";
            const string siteTrackingEnabledforCampaignWithoutSecure = "http://cache.lego.com/r/www/r/Analytics/Modules/TrackManApi";
            const string siteTrackingEnabledforFranchise = "https://a248.e.akamai.net/cache.lego.com/r/www/r/analytics/Modules/TrackManApi";

            Boolean actualSiteTrackingEnabled = false;
            // Assert.IsTrue(pageSrc.Contains(expectedHeader));
            Boolean actualHeader = driver.FindElement(By.CssSelector(".lego-global-header-outer-wrap")).Displayed;


            //Assert.IsTrue(pageSrc.Contains(expectedFooter));
            Boolean actualFooter = driver.FindElement(By.CssSelector(".lego-global-footer")).Displayed;

            // Assert.IsTrue(pageSrc.Contains(expectedStickyFooter));
            Boolean actualStrickyFooter = driver.FindElement(By.CssSelector("#GFSticky")).Displayed;

            switch (platform)
            {
                case "1":

                    actualSiteTrackingEnabled = driver.FindElement(By.CssSelector("script[data-initial-page]"))
                        .GetAttribute("src")
                        .Equals(siteTrackingEnabledforFranchise);
                    break;
                case "2": actualSiteTrackingEnabled = driver.FindElement(By.CssSelector("script[data-initial-page]"))
                        .GetAttribute("src")
                        .Equals(siteTrackingEnabledforCampaignWithoutSecure);
                    break;
            }

            //Assert.IsTrue(Driver.FindElement(By.CssSelector("script[data-initial-page]")).GetAttribute("src").Equals(siteTrackingEnabledforFranchise));

            driver.Quit();
            
            var queryString = actualHeader + "|" + actualFooter + "|" + actualStrickyFooter + "|" + actualSiteTrackingEnabled + "|" + "URL : " + urlInput;

            Console.WriteLine("Please Enter Lego User Name : ");
            var userName = Console.ReadLine();
            driver = new FirefoxDriver();
            //driver.Navigate().GoToUrl("http://localhost:21003/api/email?emailid=" + userName + "%40lego.com&key=455htk09sdny345n&emailContent=" + queryString);
            driver.Navigate().GoToUrl("http://localhost:21003//api/email?emailid=" + userName + "%40lego.com&key=455htk09sdny345n&emailContent=" + queryString);

            driver.Quit();

            Console.WriteLine("Test Results :");
            Console.WriteLine("Site :" + urlInput);
            Console.WriteLine("Test Result for Global Header : " + actualHeader);
            Console.WriteLine("Test Result for Global Footer : " + actualFooter);
            Console.WriteLine("Test Result for Sticky Footer : " + actualStrickyFooter);
            Console.WriteLine("Test Result for Site Tracking : " + actualSiteTrackingEnabled);
        }
    }
}
