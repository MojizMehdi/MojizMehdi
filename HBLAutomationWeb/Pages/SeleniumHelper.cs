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
using System.Data;
using OpenQA.Selenium.Interactions;

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
                string value = "";
                IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                if (locator.Contains("//input["))
                {
                    value = Control.GetAttribute("value");
                }
                else
                {
                    value = Control.Text;
                }
                value = value.Trim();
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

                            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", Button);
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
                if (locator.Equals("Login_Dashboard"))
                {
                    driver.Navigate().GoToUrl(Configuration.GetInstance().GetByKey("redirectionURL"));
                    Thread.Sleep(2000);

                    //IWebElement link = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));

                    //link.Click();
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
                    context.SetRatingCheck(a);
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
                var selecteditem = selectElement.Options;
                foreach (var item in selecteditem)
                {
                    if (item.Text.Contains(value))
                    {
                        value = item.Text;
                        break;
                    }
                }
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



        //Method For Combobox With Search Field
        public void comboboxSearch(string value, string locator1, string locator2)
        {
            try
            {
                bool check_option_value = false;
                //// Open the dropdown so the options are visible
                IWebElement Combobox = waitDriver.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator1)));
                Combobox.Click();
                // Get all of the options
                var options = (driver.FindElements(By.XPath(locator2)));
                // Loop through the options and select the one that matches
                foreach (IWebElement opt in options)
                {
                    if (opt.Text.Equals(value))
                    {
                        opt.Click();
                        check_option_value = true;
                        return;
                    }
                }
                if (check_option_value == false)
                {
                    throw new AssertFailedException(string.Format("The given value {0} is not present in the list", value));
                }
                Thread.Sleep(3000);


            }

            catch (ElementNotVisibleException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is not on screen", locator1));
            }
            catch (StaleElementReferenceException)
            {

                throw new AssertFailedException(string.Format("The element provided {0} is Stale", locator1));

            }
            catch (Exception ex)
            {
                throw new Exception("ex message: " + ex.Message);

            }


        }

        //For returning the values of keyword given
        public int SizeCountElements(string locator)
        {
            IWebElement Control = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            var list_elements = driver.FindElements(By.XPath(locator));

            return list_elements.Count;
        }

        //For decrypting One Time Password 
        public string GetOTP()
        {
            string otp = "";
            string schema = "DIGITAL_CHANNEL_SEC";
            string Key = "cf345ae2xz40yfc8";
            string IV = "abcaqwerabcaqwer";
            string query = "";

            if (context.Get_signup_check() == true)
            {
                query = "Select I.OTP from DC_OTP_HISTORY I where I.CNIC='{CNIC}' AND I.TRANSACTION_TYPE_ID = '247' ORDER BY I.GENERATED_ON DESC";
                query = query.Replace("{CNIC}", context.GetCustomerCNIC());
            }
            else
            {
                query = "Select I.OTP from DC_OTP_HISTORY I where I.CUSTOMER_INFO_ID=(Select CUSTOMER_INFO_ID from dc_customer_info i where I.CUSTOMER_NAME='{usernmae}') ORDER BY I.GENERATED_ON DESC";
                query = query.Replace("{usernmae}", context.GetUsername());
            }

            DataAccessComponent.DataAccessLink dLink2 = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable2 = dLink2.GetDataTable(query, schema);
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

        //Only for scrolling down without locator
        public void ScrollDownOnly(int count)
        {
            Keyboard.SendKeys("{DOWN}");
        }

        // For Alert Operation click
        public void AlertOperation(string option, string locator)
        {
            IWebElement link = waitDriver.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            if (option == "OK")
            {
                js.ExecuteScript("window.confirm = function(msg) { return true; }");
            }
            else
            {
                js.ExecuteScript("window.confirm = function(msg) { return false; }");
            }
            link.Click();

        }

        // For Range Slider with count and option for Left and Right arrow Key
        public void RangeSlider(string locator, int new_limit, int step, string slider_limit_loc, int orignal_edit_limit)
        {
            IWebElement slider = waitDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));

            //string abc = ReturnKeywordValue(locator);
            int count = 0;

            var dimensions = slider.Size;
            Actions actions = new Actions(driver);
            actions.MoveToElement(slider, (dimensions.Width / 2), 1).Click().Perform();

            string temp_var = ReturnKeywordValue(slider_limit_loc);
            temp_var = temp_var.Remove(temp_var.Length - 3);
            temp_var = temp_var.Replace(",", "");

            int after_click_bal = Convert.ToInt32(temp_var);

            if (after_click_bal > new_limit)
            {
                int amount = after_click_bal - new_limit;
                if (amount < orignal_edit_limit)
                {
                    count = amount / step;
                    if (new_limit == 1)
                    {
                        count = count + 1;
                    }
                    for (int i = 1; i <= count; i++)
                    {
                        slider.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
                    }
                }
            }
            else if(after_click_bal < new_limit)
            {
                int amount = new_limit - after_click_bal;
                if (amount < orignal_edit_limit)
                {
                    count = amount / step;
                    for (int i = 1; i <= count; i++)
                    {
                        slider.SendKeys(OpenQA.Selenium.Keys.ArrowRight);
                    }
                }      
            }
            //actions.MoveToElement(slider).MoveByOffset((location.Width / 2) - 2, 0).Click().Perform();
            //slider.Click();

            //if (ArrowOption == "LEFT")
            //{
            //    for (int i = 1; i <= count; i++)
            //    {
            //        slider.SendKeys(OpenQA.Selenium.Keys.ArrowLeft);
            //    }
            //}
            //else if (ArrowOption == "RIGHT")
            //{
            //    for (int i = 1; i <= count; i++)
            //    {
            //        slider.SendKeys("{RIGHT}");
            //    }
            //}
        }
    }
}
