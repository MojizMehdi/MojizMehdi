using HBLAutomationWeb.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using System.Windows.Forms;
using System.Threading;
using Tamir.SharpSsh;

namespace HBLAutomationWeb.Pages
{
    class SeleniumHelper
    {
        IWebDriver driver = ContextPage.Driver;
        private WebDriverWait waitDriver;
        string savelocation;
        string FilterSpecialChar;
        private ContextPage context = ContextPage.GetInstance();
        public SeleniumHelper()
        {
            if (driver != null)
            {
                waitDriver = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            }

        }


        //For Scrolling down directly to the button
        public void PressEnter(string locator)
        {
            waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
            {
                IWebElement elementbutton = waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", elementbutton);
                // var elmnt = document.getElementById("content");
                //  elmnt.scrollIntoView();
                IWebElement button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                {
                    //button.SendKeys(OpenQA.Selenium.Keys.Enter);
                    button.Click();
                }
                // js.executeScript("arguments[0].scrollIntoView();", locator);

            }
        }

        public void ScrollToElement(string locator)
        {
            waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
            {
                IWebElement elementbutton = waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", elementbutton);
                // var elmnt = document.getElementById("content");
                //  elmnt.scrollIntoView();
                //IWebElement button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                //{
                //    //button.SendKeys(OpenQA.Selenium.Keys.Enter);
                //    button.Click();
                //}
                // js.executeScript("arguments[0].scrollIntoView();", locator);

            }
        }

        public void verification(string message, string locator)
        {
            try
            {
                IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                Assert.AreEqual(message, Control.Text);
            }
            catch (ElementNotVisibleException ex)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is invalid", locator));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //For returning the value from the web page of the keyword given
        public string ReturnKeywordValue(string locator)
        {
            IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            return Control.Text;

        }

        //For returning the value inside of a text field of a keyword given
        public string ReturnTextBoxValue(string locator)
        {
            IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            return Control.GetAttribute("value");

        }

        //Method For Scrolling
        public void Scroll(string locator)
        {
            waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
            {
                IWebElement elementbutton = waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", elementbutton);
                // var elmnt = document.getElementById("content");
                //  elmnt.scrollIntoView();
                IWebElement button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                // js.executeScript("arguments[0].scrollIntoView();", locator);

            }

        }


        //For returning the background color from the web page of the keyword given
        public string ReturnBackgroundColorKeywordValue(string locator)
        {
            IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            string color = Control.GetCssValue("background-color");
            String[] hexValue = color.Replace("rgba(", "").Replace(")", "").Split(',');
            int hexValue1 = Convert.ToInt32(hexValue[0]);
            hexValue[1] = hexValue[1].Trim();
            int hexValue2 = Convert.ToInt32(hexValue[1]);
            hexValue[2] = hexValue[2].Trim();
            int hexValue3 = Convert.ToInt32(hexValue[2]);
            //string color_actual = ColorTranslator.FromHtml(color);
            Color myColor = Color.FromArgb(hexValue1, hexValue2, hexValue3);
            string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
            return hex;
        }


        ////For Taking Screenshot
        public static void TakeScreenshot()
        {
            try
            {
                string FeatureName = ContextPage.GetInstance().GetExcelRecord().FeatureName;
                string savelocation = Configuration.GetInstance().GetByKey("ScreenshotFolderPath") + FeatureName + DateTime.Now.ToString("yyyyMMdd") + "/";
                if (!Directory.Exists(savelocation))
                {
                    Directory.CreateDirectory(savelocation);
                }
                //ITakesScreenshot ssdriver = ContextPage.Driver as ITakesScreenshot;
                //Screenshot screenshot = ssdriver.GetScreenshot();
                //string fileName = ContextPage.GetInstance().GetExcelRecord().ScenarioName + ".png";
                //screenshot.SaveAsFile(savelocation + fileName, ScreenshotImageFormat.Png);
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }
                    string fileName = ContextPage.GetInstance().GetExcelRecord().ScenarioName + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
                    bitmap.Save(savelocation + fileName, ImageFormat.Png);
                }
            }
            catch (Exception exception)
            {
                throw new AssertFailedException(exception.Message);
            }
        }


        // Method For TextBox
        public void SetTextBoxValue(string textboxvalue, string locator)
        {
            try
            {

                waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                {
                    IWebElement Value = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                    Thread.Sleep(100);
                    Value.Click();
                    Value.Clear();
                    Value.SendKeys(textboxvalue);
                }
            }
            catch (ElementNotVisibleException)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator));
            }
            catch (StaleElementReferenceException)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator));
            }
            catch (InvalidElementStateException)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is not in desired state", locator));
            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);
                throw new AssertFailedException(string.Format("The element provided {0} is invalid", locator));
            }

        }

        //Method For Scroll Down

        public void ScrollDown(int count, string locator)
        {
            try
            {
                Keyboard.SendKeys("{DOWN}");
                
            }
            catch (ElementNotVisibleException)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator));
            }
            catch (StaleElementReferenceException)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator));
            }
            catch (InvalidElementStateException)
            {
                throw new AssertFailedException(string.Format("The element provided {0} is not in desired state", locator));
            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);
                throw new AssertFailedException(string.Format("The element provided {0} is invalid", locator));
            }

        }


        //Method For Button
        public void Button(string locator)
        {
            if (locator != "")
            {
                try
                {
                    Thread.Sleep(5000);
                    waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                    {
                        waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                        {
                            IWebElement Button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", Button);
                            Button.Click();
                        }
                    }
                }
                catch (ElementNotVisibleException)
                {

                    throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator));
                }
                catch (StaleElementReferenceException)
                {

                    throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator));
                }
                catch (Exception ex)
                {
                    throw new Exception("ex message: " + ex.Message);

                }
            }

        }


        //Method For Link
        public void links(string locator)
        {
            try
            {


                //Home Page Locator
                if (locator.Equals("Home Page Locator"))
                {
                    driver.Navigate().GoToUrl(Configuration.GetInstance().GetByKey("redirectionURL"));
                    Thread.Sleep(2000);

                    IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                    link.Click();
                }
                else
                {

                    Thread.Sleep(3000);

                    waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                    {
                        waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                            {
                                IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                                link.Click();
                            }
                        }
                    }
                }
            }
            catch (ElementNotVisibleException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator));
            }
            catch (StaleElementReferenceException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator));
            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);

            }

        }



        //Method For Rating
        public void rating(string locator)
        {
            try
            {


                //Home Page Locator
                if (locator.Equals("//img[@class='desk-logo']"))
                {
                    driver.Navigate().GoToUrl(Configuration.GetInstance().GetByKey("redirectionURL"));
                    Thread.Sleep(2000);

                    IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                    link.Click();
                }
                else
                {

                    Thread.Sleep(3000);
                    Boolean a = driver.FindElements(By.XPath(locator)).Count != 0;
                    //bool a = driver.FindElement(By.XPath(locator)).Displayed;
                    //IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                    if (a == true)
                    {
                        waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                            {
                                IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                                link.Click();
                            }
                        }
                    }
                    //IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                    // if(locator.)
                    else
                    {
                        //do nothing
                    }
                }
            }
            catch (ElementNotVisibleException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator));
            }
            catch (StaleElementReferenceException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator));
            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);

            }

        }

        public void checkPageIsReady()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
            {
                Thread.Sleep(200);
                return;
            }

        }

        //Method For Combobox
        public void combobox(string value, string locator)
        {
            try
            {
                IWebElement Combobox = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                Thread.Sleep(1000);
                Combobox.Click();
                Thread.Sleep(2000);
                var selectElement = new SelectElement(Combobox);
                selectElement.SelectByText(value);
                //List<IWebElement> comboValues = Combobox.FindElements(By.XPath(listlocator)).ToList();
                //{
                //    string check = String.Empty;
                //    foreach (IWebElement element in comboValues)
                //    {

                //        if (value.Equals(element.Text))
                //        {
                //            check = element.Text;
                //            Thread.Sleep(200);
                //            element.Click();
                //            break;
                //        }


                //    }
                //    if (string.IsNullOrEmpty(check))
                //    {
                //        throw new Exception("No value found in Dropdown");
                //    }

            //}
                Thread.Sleep(3000);


            }

            catch (ElementNotVisibleException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator));
            }
            catch (StaleElementReferenceException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator));

            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);

            }


        }
    }
}
