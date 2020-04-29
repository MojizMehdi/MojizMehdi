using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBLAutomationAndroid.Common;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace HBLAutomationAndroid.Pages
{
    class DriverFactory
    {
        //public static AppiumDriver<AndroidElement> driver;
        public static AppiumDriver<AndroidElement> driver;

        public DriverFactory()
        {
        }

        //driverchange
       // public static IWebDriver getDriver()
       // public static AppiumDriver<AndroidElement> getDriver()
        public static AppiumDriver<AndroidElement> getDriver()
        {
            if (driver == null)
            {
                try
                {
                    String remoteServerUrl;

                    if (String.Equals(Configuration.GetInstance().GetByKey("IsRemote"), "Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        remoteServerUrl = Configuration.GetInstance().GetByKey("RemoteServer");
                        //string appPackage = Configuration.GetInstance().GetByKey("appPackage");
                        //string appActivity = Configuration.GetInstance().GetByKey("appActivity");
                        //string platformName = Configuration.GetInstance().GetByKey("platformName");
                        //string deviceName = Configuration.GetInstance().GetByKey("deviceName");
                        //string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
                        //initialize(DesiredCapabilities.Chrome(), remoteServerUrl, appURL);

                    }

                    if (String.Equals(Configuration.GetInstance().GetByKey("IsRemote"), "No", StringComparison.OrdinalIgnoreCase))
                    {
                        string serverURL = Configuration.GetInstance().GetByKey("AppiumServerURL");
                        string appPackage = Configuration.GetInstance().GetByKey("appPackage");
                        string appActivity = Configuration.GetInstance().GetByKey("appActivity");
                        string platformName = Configuration.GetInstance().GetByKey("platformName");
                        string deviceName = Configuration.GetInstance().GetByKey("deviceName");
                        string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
                        AppiumOptions options = new AppiumOptions();
                        options.PlatformName = "Android";
                        options.AddAdditionalCapability("appPackage", appPackage);
                        options.AddAdditionalCapability("appActivity", appActivity);
                        options.AddAdditionalCapability("platformName", platformName);
                        options.AddAdditionalCapability("deviceName", deviceName);
                        options.AddAdditionalCapability("platformVersion", platformVersion);
                        options.AddAdditionalCapability("noReset", "true");
                        options.AddAdditionalCapability("fullReset", "false");
                        //options.AddAdditionalCapability("newCommandTimeout", 60);
                        initialize(options,serverURL);
                    }

                    return driver;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return driver;
        }

        public static void initialize(AppiumOptions options, string serverURL)
        {
            try
            {

                //driver = new AndroidDriver<AndroidElement>(new Uri(serverURL), options);
                driver = new WpDriver(new Uri(serverURL), options);
                //driver = (AndroidDriver<AndroidElement>)rd;
                //WpDriver rd = driver;
                //driver.Manage().Window.Maximize();
                //driver.Navigate();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        //public static void initialize(String appurl)
        //{
        //    try
        //    {
        //        string executeOnBrowser = Configuration.GetInstance().GetByKey("ExecuteOnBrowser");
        //        if (!String.IsNullOrEmpty(executeOnBrowser))
        //        {
        //            switch (executeOnBrowser)
        //            {
        //                case "chrome":
        //                    Console.WriteLine("chrome");
        //                    ChromeOptions chromeOptions = new ChromeOptions();
        //                    chromeOptions.AddArgument("--start-maximized");
        //                    //chromeOptions.AddArguments("disable-infobars");
        //                    //chromeOptions.AddArgument("--disable-notifications");
        //                    chromeOptions.AddExcludedArgument("enable-automation");
        //                   // chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
        //                    //  chromeOptions.BinaryLocation = "D:\\Projects\\IRISSuiteAutomation\\IRISSuiteAutomation\\IRISSuiteAutomation\\chromedriver.exe";
        //                    driver = new ChromeDriver("D:\\Automation\\",chromeOptions);
        //                    //driver = new ChromeDriver("D:\\Automation\\", chromeOptions);
        //                    //driver.Manage().Window.Maximize();
        //                    //driver.Navigate.chromeOptions;
        //                    break;


        //                case "firefox":
        //                    //  FirefoxBinary firefoxbinary = new FirefoxBinary();
        //                    //  FirefoxProfile firefoxprofile = new FirefoxProfile();

        //                    //  FirefoxOptions firefoxOptions = new FirefoxOptions();
        //                    //  firefoxOptions.AddAdditionalCapability()
        //                    //  //firefoxprofile.SetPreference("xpinstall.signatures.required", false);
        //                    //driver = new FirefoxDriver(firefoxbinary, firefoxprofile);
        //                    //  ((IJavaScriptExecutor)driver).ExecuteScript(@"window.resizeTo(screen.width-100,screen.height-100);");
        //                    //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\CMSAutomation\Demo-Automation\1LinkCertification\IRISSuiteAutomation\bin\Debug");
        //                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\Automation");
        //                    // service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";                           
        //                    driver = new FirefoxDriver(service);
        //                    driver.Manage().Window.Maximize();
        //                    break;

        //                case "ie":

        //                    driver = new InternetExplorerDriver();
        //                    break;

        //                case "safari":
        //                    Console.WriteLine("sa");
        //                    break;

        //                default:
        //                    throw new SystemException(String.Format("Invalid browser provided as: %s", executeOnBrowser));

        //            }

        //        }
        //        else
        //        {
        //            throw new SystemException(String.Format("No browser is provided for execution: %s", executeOnBrowser));
        //        }
        //        driver.Navigate().GoToUrl(appurl);
        //        //driver.Manage().Window.Maximize();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        public string getTitleOfCurrentPage()
        {
            return driver.Url;
        }

        public static void driverQuit()
        {
            driver.Quit();
        }
    }
}
