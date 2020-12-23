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
                        string saucelab_check = Configuration.GetInstance().GetByKey("Saucelab_Execution");
                        //ContextPage.platform_version = platformVersion;
                        AppiumOptions options = new AppiumOptions();
                        //options.AddAdditionalCapability("pCloudy_Username", "ali.abbas@hbl.com");
                        //options.AddAdditionalCapability("pCloudy_ApiKey", "x8hd3nfqthmbktx7tt9c95j8");
                        //options.AddAdditionalCapability("pCloudy_DurationInMinutes", 10);
                        //options.AddAdditionalCapability("newCommandTimeout", 600);
                        //options.AddAdditionalCapability("launchTimeout", 90000);
                        //options.AddAdditionalCapability("pCloudy_DeviceFullName", "SAMSUNG_GalaxyJ52016_Android_7.1.1_09c99");
                        //options.AddAdditionalCapability("platformVersion", "7.1.1");
                        //options.AddAdditionalCapability("platformName", "Android");
                        //options.AddAdditionalCapability("automationName", "uiautomator2");
                        //options.AddAdditionalCapability("pCloudy_ApplicationName", "AUG20internet_new.apk");
                        //options.AddAdditionalCapability("appPackage", "com.hbl.android.hblmobilebanking");
                        //options.AddAdditionalCapability("appActivity", "com.hbl.android.hblmobilebanking.activity.SplashActivity");
                        //options.AddAdditionalCapability("pCloudy_WildNet", "false");
                        //options.AddAdditionalCapability("pCloudy_EnableVideo", "true");
                        //options.AddAdditionalCapability("pCloudy_EnablePerformanceData", "false");
                        //options.AddAdditionalCapability("pCloudy_EnableDeviceLogs", "false");
                        //AndroidDriver<WebElement> driver = new AndroidDriver<WebElement>(new URL("https://us.pcloudy.com/appiumcloud/wd/hub"), capabilities);
                        //options.PlatformName = "Android";
                        options.AddAdditionalCapability("appPackage", appPackage);
                        options.AddAdditionalCapability("appActivity", appActivity);
                        options.AddAdditionalCapability("platformName", platformName);
                        options.AddAdditionalCapability("deviceName", deviceName);
                        options.AddAdditionalCapability("platformVersion", platformVersion);
                        options.AddAdditionalCapability("noReset", "false");
                        options.AddAdditionalCapability("fullReset", "false");
                        options.AddAdditionalCapability("autoGrantPermissions", false);
                        options.AddAdditionalCapability("newCommandTimeout", 180);
                        if(saucelab_check.ToLower() == "yes")
                        {
                            string sauce_username = Configuration.GetInstance().GetByKey("Saucelab_Username");
                            string sauce_password = Configuration.GetInstance().GetByKey("Suacelab_Accesskey");
                            options.AddAdditionalCapability("username", sauce_username);
                            options.AddAdditionalCapability("accessKey", sauce_password);
                            options.AddAdditionalCapability("app", "storage:filename=AUG20internet_new.apk");
                            options.AddAdditionalCapability("appiumVersion", "1.17.1");
                            serverURL = Configuration.GetInstance().GetByKey("Saucelab_URL");
                            serverURL = serverURL.Replace("{UserName}", sauce_username).Replace("{Accesskey}", sauce_password);
                        }
                        initialize(options, serverURL);
                    }

                    return driver;
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message.ToString());
                }
            }

            return driver;
        }

        public static void initialize(AppiumOptions options, string serverURL)
        {
            try
            {

                //driver = new AndroidDriver<AndroidElement>(new Uri(serverURL), options);
                //driver.
                driver = new AndroidDriver<AndroidElement>(new Uri(serverURL), options,TimeSpan.FromSeconds(300));
                //driver = new WpDriver(new Uri(serverURL),options);
                //driver = new WpDriver((new SauceLabsEndpoint().SauceHubUri), options);
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
