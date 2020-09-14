using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBLAutomationWeb.Common;
using OpenQA.Selenium.IE;

namespace HBLAutomationWeb.Pages
{
    class DriverFactory
    {
        public static IWebDriver driver;

        public DriverFactory()
        {
        }

        public static IWebDriver getDriver()
        {
            if (driver == null)
            {
                try
                {
                    String remoteServerUrl;

                    if (String.Equals(Configuration.GetInstance().GetByKey("IsRemote"), "Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        remoteServerUrl = Configuration.GetInstance().GetByKey("RemoteServer");
                        String appURL = Configuration.GetInstance().GetByKey("HBLWeb");
                        initialize(DesiredCapabilities.Chrome(), remoteServerUrl, appURL);

                    }

                    if (String.Equals(Configuration.GetInstance().GetByKey("IsRemote"), "No", StringComparison.OrdinalIgnoreCase))
                    {
                        String appURL = Configuration.GetInstance().GetByKey("HBLWeb");
                        initialize(appURL);
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

        public static void initialize(DesiredCapabilities capabilities, String RemoteServerUrl, String appurl)
        {
            try
            {

                driver = new RemoteWebDriver(new Uri(RemoteServerUrl), capabilities);
                driver.Manage().Window.Maximize();
                driver.Navigate();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void initialize(String appurl)
        {
            try
            {
                string executeOnBrowser = Configuration.GetInstance().GetByKey("ExecuteOnBrowser");
                if (!String.IsNullOrEmpty(executeOnBrowser))
                {
                    switch (executeOnBrowser)
                    {
                        case "chrome":
                            Console.WriteLine("chrome");
                            ChromeOptions chromeOptions = new ChromeOptions();
                            chromeOptions.AddArgument("--start-maximized");
                            //chromeOptions.AddArguments("disable-infobars");
                            //chromeOptions.AddArgument("--disable-notifications");
                            chromeOptions.AddExcludedArgument("enable-automation");
                           // chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                            //  chromeOptions.BinaryLocation = "D:\\Projects\\IRISSuiteAutomation\\IRISSuiteAutomation\\IRISSuiteAutomation\\chromedriver.exe";
                            driver = new ChromeDriver(chromeOptions);
                            //driver = new ChromeDriver("D:\\Automation\\", chromeOptions);
                            //driver.Manage().Window.Maximize();
                            //driver.Navigate.chromeOptions;
                            break;


                        case "firefox":
                            //  FirefoxBinary firefoxbinary = new FirefoxBinary();
                            //  FirefoxProfile firefoxprofile = new FirefoxProfile();

                            //  FirefoxOptions firefoxOptions = new FirefoxOptions();
                            //  firefoxOptions.AddAdditionalCapability()
                            //  //firefoxprofile.SetPreference("xpinstall.signatures.required", false);
                            //driver = new FirefoxDriver(firefoxbinary, firefoxprofile);
                            //  ((IJavaScriptExecutor)driver).ExecuteScript(@"window.resizeTo(screen.width-100,screen.height-100);");
                            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\CMSAutomation\Demo-Automation\1LinkCertification\IRISSuiteAutomation\bin\Debug");
                            //File pathToBinary = new File("C:\user\App\Firefox\firefox.exe");
                            //FirefoxBinary ffBinary = new FirefoxBinary(pathToBinary);
                            //FirefoxProfile firefoxProfile = new FirefoxProfile();
                            //driver = new FirefoxDriver(ffBinary, firefoxProfile);
                            //System.setProperty("webdriver.gecko.driver", driverPath);
                            //Environment.SetEnvironmentVariable("webdriver.gecko.driver", "D:\\Automation\\geckodriver.exe");
                            //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                            //FirefoxOptions foptions = new FirefoxOptions();
                            //foptions.AddAdditionalCapability("acceptInsecureCerts", true);
                            //capabilities.SetCapability("acceptInsecureCerts", true);
                            driver = new FirefoxDriver();
                            driver.Manage().Window.Maximize();
                            break;

                        case "ie":

                            driver = new InternetExplorerDriver();
                            break;

                        case "safari":
                            Console.WriteLine("sa");
                            break;

                        default:
                            throw new SystemException(String.Format("Invalid browser provided as: %s", executeOnBrowser));

                    }

                }
                else
                {
                    throw new SystemException(String.Format("No browser is provided for execution: %s", executeOnBrowser));
                }
                driver.Navigate().GoToUrl(appurl);
                //driver.Manage().Window.Maximize();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

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
