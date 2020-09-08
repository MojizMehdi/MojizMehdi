using HBLAutomationAndroid.Common;
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
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenQA.Selenium.Appium.MultiTouch;
using System.Data;
using OpenQA.Selenium.Appium.Windows.Enums;

namespace HBLAutomationAndroid.Pages
{
    class AppiumHelper
    {
        AppiumDriver<AndroidElement> driver = ContextPage.Driver;
        //IWebDriver driver = ContextPage.Driver;
        private WebDriverWait waitDriver;
        string savelocation;
        string FilterSpecialChar;
        private ContextPage context = ContextPage.GetInstance();
        public AppiumHelper()
        {
            if (driver != null)
            {
                waitDriver = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            }

        }
        //For Scrolling down directly to the button
        //public void PressEnter(string locator)
        //{
        //    waitDriver.Until(ExpectedConditions.ElementExists(By.Id(locator)));
        //    {
        //        IWebElement elementbutton = waitDriver.Until(ExpectedConditions.ElementExists(By.Id(locator)));
        //        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //        js.ExecuteScript("arguments[0].scrollIntoView();", elementbutton);
        //        // var elmnt = document.getElementById("content");
        //        //  elmnt.scrollIntoView();
        //        IWebElement button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
        //        {
        //            //button.SendKeys(OpenQA.Selenium.Keys.Enter);
        //            button.Click();
        //        }
        //        // js.executeScript("arguments[0].scrollIntoView();", locator);

        //    }
        //}

        //public void ScrollToElement(string locator)
        //{
        //    waitDriver.Until(ExpectedConditions.ElementExists(By.Id(locator)));
        //    {
        //        IWebElement elementbutton = waitDriver.Until(ExpectedConditions.ElementExists(By.Id(locator)));
        //        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //        js.ExecuteScript("arguments[0].scrollIntoView();", elementbutton);
        //        // var elmnt = document.getElementById("content");
        //        //  elmnt.scrollIntoView();
        //        //IWebElement button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
        //        //{
        //        //    //button.SendKeys(OpenQA.Selenium.Keys.Enter);
        //        //    button.Click();
        //        //}
        //        // js.executeScript("arguments[0].scrollIntoView();", locator);

        //    }
        //}

        //public void verification(string message, string locator)
        //{
        //    try
        //    {
        //        IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
        //        Assert.AreEqual(message, Control.Text);
        //    }
        //    catch (ElementNotVisibleException ex)
        //    {
        //        throw new AssertFailedException(string.Format("The element provided {0} is invalid", locator));

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        //For returning the value from the web page of the keyword given using xpath
        public void longpress(string locator, string locator_type)
        {
            IWebElement Control = null;
            if (locator_type == "id")
            {
                Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
            }
            else if (locator_type == "xpath")
            {
                Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            }
            ITouchAction tc = new TouchAction(driver);
            tc.LongPress(Control).Perform();

        }
        //For Counting Elements
        public int SizeCountElements(string locator,string locator_type)
        {
            var size_count = 0;
            if (locator_type == "id")
            {
                AndroidElement Control = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                var list_elements = driver.FindElements(By.Id(locator));
                size_count = list_elements.Count;
            }
            else if (locator_type == "xpath")
            {
                AndroidElement Control = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                var list_elements = driver.FindElements(By.XPath(locator));
                size_count = list_elements.Count;
            }
            return size_count;
        }

        //For Returning Elements
        public List<string> Return_Keyword_Elements_List(string locator, string locator_type)
        {
            List<string> lst = new List<string>();
            if (locator_type == "id")
            {
                AndroidElement Control = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                var list_elements = driver.FindElements(By.Id(locator));
                foreach (var item in list_elements)
                {
                    lst.Add(item.Text);
                }
                //return lst;
            }
            else if (locator_type == "xpath")
            {
                var list_elements = driver.FindElements(By.Id(locator));
                foreach (var item in list_elements)
                {
                    lst.Add(item.Text);
                }
                //return lst;
            }
            return lst;
        }
        ////For returning the value inside of a text field of a keyword given
        //public string ReturnTextBoxValue(string locator)
        //{
        //    IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
        //    return Control.GetAttribute("value");

        //}

        ////Method For Scrolling
        //public void Scroll(string locator)
        //{
        //    waitDriver.Until(ExpectedConditions.ElementExists(By.Id(locator)));
        //    {
        //        IWebElement elementbutton = waitDriver.Until(ExpectedConditions.ElementExists(By.Id(locator)));
        //        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //        js.ExecuteScript("arguments[0].scrollIntoView();", elementbutton);
        //        // var elmnt = document.getElementById("content");
        //        //  elmnt.scrollIntoView();
        //        IWebElement button = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));

        //        // js.executeScript("arguments[0].scrollIntoView();", locator);

        //    }

        //}


        ////For returning the background color from the web page of the keyword given
        //public string ReturnBackgroundColorKeywordValue(string locator)
        //{
        //    IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
        //    string color = Control.GetCssValue("background-color");
        //    String[] hexValue = color.Replace("rgba(", "").Replace(")", "").Split(',');
        //    int hexValue1 = Convert.ToInt32(hexValue[0]);
        //    hexValue[1] = hexValue[1].Trim();
        //    int hexValue2 = Convert.ToInt32(hexValue[1]);
        //    hexValue[2] = hexValue[2].Trim();
        //    int hexValue3 = Convert.ToInt32(hexValue[2]);
        //    //string color_actual = ColorTranslator.FromHtml(color);
        //    Color myColor = Color.FromArgb(hexValue1, hexValue2, hexValue3);
        //    string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        //    return hex;
        //}


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
        public void SetTextBoxValue(string textboxvalue, string locator,string locator_type)
        {
            try
            {
                if (locator_type == "id")
                {
                    

                    waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                    {
                        AndroidElement Value = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                        Thread.Sleep(100);
                        Value.Click();
                        Value.Clear();
                        Value.SendKeys(textboxvalue);
                        //if (locator == "com.hbl.android.hblmobilebanking:id/s_hbpsBillCompanies")
                        //{
                        //    Value = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id("com.hbl.android.hblmobilebanking:id/withoutMargin_Lay")));
                        //    Size size = Value.Size;
                        //    var location = Value.Location;
                        //    //Size dimension = driver.Manage().Window.Size;
                        //    int x = location.X / 2;
                        //    //int start_y = (int)(dimension.Height * height_start_dimension);
                        //    //int end_y = (int)(dimension.Height * height_end_dimension);
                        //    ITouchAction tc = new TouchAction(driver);
                        //    Thread.Sleep(2000);
                        //    tc.Press(x + 100, location.Y).Wait(1000).Release().Perform();

                        //}
                        driver.HideKeyboard();

                    }
                }
                else if (locator_type == "xpath")
                {
                    waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                    {
                        AndroidElement Value = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                        Thread.Sleep(100);
                        Value.Click();
                        Value.Clear();
                        Value.SendKeys(textboxvalue);
                        driver.HideKeyboard();
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


        public void set_attribute(string locator)
        {
            IWebElement Control = null;
            //IJavaScriptExecutor js = IJavaScriptExecutor(driver)
            Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
            //Control.Enabled = true;
        }

        //Method For Message Verification
        public void verification(string message, string locator,string locator_type)
        {
            try
            {
                IWebElement Control = null;
                string value = "";
                if (locator_type == "id")
                {
                    Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                }
                else if (locator_type == "xpath")
                {
                    Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                }
                if (locator.Contains("//input["))
                {
                    value = Control.GetAttribute("value");
                }
                else
                {
                    value = Control.Text;
                }

                Assert.AreEqual(message, value);

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


        //Method For Button
        public void Button(string locator,string locator_type)
        {
            if (locator != "")
            {
                try
                {
                    Thread.Sleep(5000);
                    if (locator_type == "id")
                    {
                        waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                            {
                                AndroidElement Button = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));

                                // var js = ((IJavaScriptExecutor)driver);
                                // js.ExecuteScript("arguments[0].scrollIntoView(true);", Button);
                                Button.Click();
                                
                            }
                        }
                    }
                    else if (locator_type == "xpath")
                    {
                        waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                            {
                                AndroidElement Button = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                                // var js = ((IJavaScriptExecutor)driver);
                                // js.ExecuteScript("arguments[0].scrollIntoView(true);", Button);
                                Button.Click();
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

        }

        //Method For Link Visibility
        public void links_visibility(string locator, string locator_type)
        {
            try
            {
                    Thread.Sleep(3000);

                    if (locator_type == "id")
                    {

                        AndroidElement link = driver.FindElementById(locator);
                        //link.Click();
                    }
                    else if (locator_type == "xpath")
                    {
                         AndroidElement link = driver.FindElementByXPath(locator);
                        // link.Click();
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



        //Method For Link
        public void links(string locator,string locator_type)
        {
            try
            {


                //Home Page Locator
                if (locator.Equals("com.hbl.android.hblmobilebanking:id/homeClick"))
                {
                    try
                    {
                        AndroidElement link = (AndroidElement)driver.FindElementById(locator);
                        link.Click();
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                {

                    Thread.Sleep(3000);

                    if (locator_type == "id")
                    {
                        waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                            {
                                waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                                {
                                    AndroidElement link = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                                    //AndroidElement link = driver.FindElementByXPath(locator);
                                    link.Click();
                                }
                            }
                        }

                    }
                    else if (locator_type == "xpath")
                    {
                        waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                            {
                                waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                                {
                                    AndroidElement link = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                                    //AndroidElement link = driver.FindElementByXPath(locator);
                                    link.Click();
                                }
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

        //For returning the value from the mobile of the keyword given
        public string ReturnKeywordValue(string locator,string locator_type)
        {
            IWebElement Control = null;
            if (locator_type == "id")
            {
                Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
            }
            if (locator_type == "xpath")
            {
                Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            }
            return Control.Text;
        }
        ////Method For Rating
        public void rating(string locator)
        {
            try
            {
                    Thread.Sleep(3000);
                    Boolean a = driver.FindElements(By.Id(locator)).Count != 0;
                    if (a == true)
                    {
                        waitDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
                        {
                            waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                            {
                                IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));

                                link.Click();
                            }
                        }
                    }
                    else
                    {
                        //do nothing
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

        //public void checkPageIsReady()
        //{
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    string ali = js.ExecuteScript("return document.readyState").ToString();
        //    if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
        //    {
        //        Thread.Sleep(200);
        //        return;
        //    }

        //}

        //For Scroll to Element using Text
        public void scroll_to_element_text(string text)
        {

            Thread.Sleep(3000);
            var temp = driver.FindElements(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector().textContains(\"" + text + "\"));"));
            if(temp.Count == 0)
            {
                throw new Exception(string.Format("Element with the given text not found on screen"));
            }          
            //var elements = driver.FindElements(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector()).scrollIntoView("+ "new UiSelector().text(\"" + text + "\"));"));
        }


        //Method For Scroll Down
        public void scroll_down(double height_start_dimension, double height_end_dimension)
        {
            try
            {
                Size dimension = driver.Manage().Window.Size;
                int x = dimension.Width / 2;
                int start_y = (int)(dimension.Height * height_start_dimension);
                int end_y = (int)(dimension.Height * height_end_dimension);
                ITouchAction tc = new TouchAction(driver);
                Thread.Sleep(2000);
                tc.Press(x, start_y).Wait(1000).MoveTo(x, end_y).Release().Perform();
            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);

            }
        }

        //Method For Scroll Right
        public void scroll_right(double height_dimension, double width_start_dimension, double width_end_dimension)
        {
            //string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
            try
            {
                Size dimension = driver.Manage().Window.Size;
                int y = Convert.ToInt32((dimension.Height * height_dimension));
                int start_x = (int)(dimension.Width * width_start_dimension);
                int end_x = (int)(dimension.Width * width_end_dimension);
                ITouchAction tc = new TouchAction(driver);
                tc.Press(start_x, y).Wait(1000).MoveTo(end_x, y).Release().Perform();
            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);

            }
        }



        //Method For Returning Combobox Values
        public List<string> return_combobox_values(string locator,string list_locator, string locator_type)
        {
            try
            {
                List<string> elements = new List<string>();
                if (locator_type == "id")
                {
                    AndroidElement Combobox = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                    Thread.Sleep(1000);
                    Combobox.Click();
                    Thread.Sleep(2000);
                    var temp = driver.FindElementsById(list_locator);
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp[i].Text.ToString().ToLower().Contains("select"))
                        {
                            continue;
                        }
                        else
                        {
                            elements.Add(temp[i].Text.ToString());
                        }
                    }
                }
                else if (locator_type == "xpath")
                {
                    AndroidElement Combobox = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                    Thread.Sleep(1000);
                    Combobox.Click();
                    Thread.Sleep(2000);
                    var temp = driver.FindElementsById(list_locator);
                    for (int i = 0; i < elements.Count; i++)
                    {
                        elements.Add(temp.ToString());
                    }
                }
                driver.FindElementByXPath("//*").Click();
                return elements;
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


        //Method For Combobox
        public void combobox(string value, string locator,string locator_type)
        {
            try
            {
                if (locator_type == "id")
                {
                    AndroidElement Combobox = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                    Thread.Sleep(1000);
                    Combobox.Click();
                    Thread.Sleep(2000);
                    AndroidElement ComboboxValue = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@text,'" + value + "')]")));
                    ComboboxValue.Click();
                    Thread.Sleep(3000);
                }
                else if(locator_type == "xpath")
                {
                    AndroidElement Combobox = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.Id(locator)));
                    Thread.Sleep(1000);
                    Combobox.Click();
                    Thread.Sleep(2000);
                    AndroidElement ComboboxValue = (AndroidElement)waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@text,'" + value + "')]")));
                    ComboboxValue.Click();
                    Thread.Sleep(3000);
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

        //For decrypting One Time Password 
        public string GetOTP()
        {
            string otp = "";
            string schema = "DIGITAL_CHANNEL_SEC";
            string Key = "cf345ae2xz40yfc8";
            string IV = "abcaqwerabcaqwer";
            string query2 = "";
            if (context.Get_signup_check() == true)
            {
                query2 = "Select I.OTP from DC_OTP_HISTORY I where I.CNIC='{CNIC}' AND I.TRANSACTION_TYPE_ID = '247' ORDER BY I.GENERATED_ON DESC";
                query2 = query2.Replace("{CNIC}", context.GetCustomerCNIC());
            }
            else
            {
                string query3 = "Select CUSTOMER_INFO_ID from dc_customer_info i where I.CUSTOMER_NAME='{usernmae}'";
                query3 = query3.Replace("{usernmae}", context.GetUsername());

                DataAccessComponent.DataAccessLink dLink3 = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable3 = dLink3.GetDataTable(query3, schema);
                string customer_info_id = SourceDataTable3.Rows[0][0].ToString();

                query2 = "Select I.OTP from DC_OTP_HISTORY I where I.CUSTOMER_INFO_ID='{customer_info_id}' ORDER BY I.GENERATED_ON DESC";
                query2 = query2.Replace("{customer_info_id}", customer_info_id);
            }
            DataAccessComponent.DataAccessLink dLink2 = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable2 = dLink2.GetDataTable(query2, schema);
            otp = SourceDataTable2.Rows[0][0].ToString();


            string chk_encrypt_query = "Select PARAMTER_VALUE  from DC_APPLICATION_PARAM_DETAIL i where I.PARAMETER_NAME='OTP_HISTORY_ENCRYPTED'";
            DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dLink.GetDataTable(chk_encrypt_query, schema);
            string otp_flag = SourceDataTable.Rows[0][0].ToString();
            if (otp_flag == "1")
            {
                string decryptedstring = AESEncryptorDecryptor.Decrypt(otp, Key, IV);
                otp = decryptedstring;
            }
            return otp;
        }
    }
}
