using HBLAutomationAndroid.Beans;
using HBLAutomationAndroid.Common;
using HBLAutomationAndroid.Pages;
using HBLAutomationAndroid.XML.ElementFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using Dynamitey.DynamicObjects;
using System.Text;
using Newtonsoft.Json;

namespace HBLAutomationAndroid.Core
{
    [Binding]
    public class LoginSteps
    {
        private ContextPage context = ContextPage.GetInstance();
        [Given(@"the test case title is ""(.*)""")]
        public void GivenTheTestCaseTitleIs(string p0)
        {
            ExcelRecord record = new ExcelRecord();
            record.FeatureName = FeatureContext.Current.FeatureInfo.Title;
            record.ScenarioName = ScenarioContext.Current.ScenarioInfo.Title + p0;
            ContextPage.GetInstance().SetExcelRecord(record);
        }

        [Given(@"the test case expected result is ""(.*)""")]
        public void GivenTheTestCaseExpectedResultIs(string expected_result)
        {
            ExcelRecord rec = ContextPage.GetInstance().GetExcelRecord();
            rec.ExpectedResult = expected_result;
            if(expected_result.ToLower() == "pass")
            {
                if(Configuration.GetInstance().GetByKey("Record_Video").ToLower() == "yes")
                {
                    AppiumHelper.Start_Video();
                }
            }
        }


        [Given(@"the user is arrive to Mobile Banking home page")]
        [When(@"the user is arrive to Mobile Banking home page")]
        [Then(@"the user is arrive to Mobile Banking home page")]
        public void GivenTheUserIsArriveToMobileBankingHomePage()
        {
            try
            {
                ContextPage.driver = DriverFactory.getDriver();
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I change screen attribute value on ""(.*)""")]
        public void WhenIChangeScreenAttributeValueOn(string Keyword)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            //apmhelper.checkPageIsReady();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            apmhelper.set_attribute(keyword.Locator);
        }

        [When(@"I verify bill payment inquiry for mobile")]
        public void WhenIVerifyBillPaymentInquiryForMobile()
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                string consumer_template_query = "SELECT CH.CONSUMER_NAME_TEMPLATE,CH.COMPANY_CODE FROM BPS_COMPANY_CHANNEL CH WHERE CH.CHANNEL_CODE = 'MB' AND CH.COMPANY_CODE = '" + context.GetCompany_Code() + "'";
                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable(consumer_template_query, "QAT_BPS");
                string consumer_template = SourceDataTable.Rows[0][0].ToString();
                char[] char_spearator = new char[2];
                char_spearator[0] = '|';
                char_spearator[1] = ';';
                string[] consumer_template_arr = consumer_template.Split(char_spearator);
                Element keyword = ContextPage.GetInstance().GetElement("BillPayment_Bill_Inquiry_Fields");
                string temp = "";
                string ui_value = "";
                string db_value = "";
                string[] date_fromat_arr;
                //string date_format;
                for (int i = 0; i < consumer_template_arr.Length; i++)
                {

                    if (i % 2 == 0)
                    {
                        temp = keyword.Locator.Replace("{Field_Name}", consumer_template_arr[i]);
                        ui_value = apmhelper.ReturnKeywordValue(temp, "xpath");
                    }
                    if (i % 2 != 0)
                    {
                        consumer_template_arr[i] = consumer_template_arr[i].Replace("<FS_01:", string.Empty);
                        date_fromat_arr = consumer_template_arr[i].Split('>');
                        consumer_template_arr[i] = date_fromat_arr[0];
                        //if (consumer_template_arr[i - 1] == "Bill Status")
                        //{
                        //    consumer_template_arr[i] = "BILL_STATUS_ID";
                        //}
                        string query = "SELECT * FROM (SELECT BI." + consumer_template_arr[i] + " FROM BPS_BILL_INFO BI WHERE BI.CREATED_ON >= trunc(sysdate) AND BI.CONSUMER_NO = '" + context.GetConsumer_No() + "' AND TO_CHAR(BI.BILLING_MONTH,'MM/YYYY') = '" + context.GetBilling_Month() + "'order by BI.BILL_INFO_ID desc) WHERE ROWNUM = 1";
                        //if (query.Contains("COMPANY_NAME"))
                        //{
                        //    query = "SELECT CO.COMPANY_NAME FROM BPS_COMPANY CO WHERE CO.COMPANY_CODE = '" + context.GetCompany_Code() + "'";
                        //}

                        //if (query.Contains("ATTRIBUTE"))
                        //{
                        //    query = query.Replace("ATTRIBUTE", "ATTRIBUTE_");
                        //}
                        SourceDataTable = null;
                        SourceDataTable = dlink.GetDataTable(query, "QAT_BPS");
                        db_value = SourceDataTable.Rows[0][0].ToString();

                        if(consumer_template_arr[i - 1] == "Consumer Name")
                        {
                            db_value = db_value.Replace(" ", string.Empty);
                            ui_value = ui_value.Replace(" ", string.Empty);
                        }
                        //if (consumer_template_arr[i] == "BILL_STATUS_ID")
                        //{
                        //    if (db_value == "1")
                        //    {
                        //        db_value = "UNPAID";
                        //    }
                        //    else if (db_value == "2")
                        //    {
                        //        db_value = "PAID";
                        //    }
                        //    else if (db_value == "3")
                        //    {
                        //        db_value = "You cannot pay this bill because the grace period after due date has passed";
                        //    }
                        //}
                        //if(query.Contains("SELECT LP.BILLING_MONTH"))
                        //{
                        //    DateTime dt = Convert.ToDateTime(db_value);
                        //    db_value = dt.ToString("MMM-yyyy");
                        //}
                        //if (query.Contains("CONSUMER_NAME"))
                        //{
                        //    ui_value = ui_value.Replace(" ", string.Empty);
                        //    db_value = db_value.Replace(" ", string.Empty);
                        //}
                        if (date_fromat_arr[1] != "")
                        {
                            date_fromat_arr[1] = date_fromat_arr[1].Replace("^", string.Empty);
                            DateTime dt = Convert.ToDateTime(db_value);
                            db_value = dt.ToString(date_fromat_arr[1]);
                        }
                        if (ui_value != db_value)
                        {
                            throw new Exception(string.Format("The UI value is {0} and the databse value is {1}", ui_value, db_value));
                        }
                    }
                }


            }
            catch(Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new Exception(exception.Message);
            }
        }


        [Given(@"I am resetting app")]
        [When(@"I am resetting app")]
        public void WhenIAmResettingApp()
        {
            AppiumHelper apmhelper = new AppiumHelper();
            apmhelper.reset_app();
        }

        [Given(@"I am switiching activity as package ""(.*)"" as activity ""(.*)""")]
        [When(@"I am switiching activity as package ""(.*)"" as activity ""(.*)""")]
        [Then(@"I am switiching activity as package ""(.*)"" as activity ""(.*)""")]
        public void GivenIAmSwitichingActivityAsPackageAsActivity(string app_package, string app_activity)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            apmhelper.swtich_activity(app_package, app_activity);
        }

        //[When(@"I deselect consumers for multi bill payment as ""(.*)"" on ""(.*)""")]
        //public void WhenIDeselectConsumersForMultiBillPaymentAsOn(string p0, string p1)
        //{
        //    ScenarioContext.Current.Pending();
        //}


        [When(@"I have given ""(.*)"" on ""(.*)""")]
        [Then(@"I have given ""(.*)"" on ""(.*)""")]
        public void WhenIHaveGivenOn(string textboxvalue, string Keyword)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            string locator_type = "id";
            if (Keyword.Contains("Expiry_Date") && textboxvalue != "")
            {
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                apmhelper.links(keyword.Locator, "id");
                string year = textboxvalue.Substring(0, 4);
                string month = textboxvalue.Substring(4, 2);
                apmhelper.date_wheel(year, month);
                apmhelper.links("android:id/button1", "id");
                return;
            }
            if (Keyword.Contains("Login_OTP_field"))
            {
                //AppiumHelper apmhelper = new AppiumHelper();
                apmhelper.scroll_to_element_text("One Time Password (OTP)");
                textboxvalue = apmhelper.GetOTP();
            }
            if (Keyword.Contains("SendMoney_TranPass"))
            {
                apmhelper.scroll_to_element_text("Transaction Password");
            }
            //if (Keyword.Equals("Forget_PasswordCardPin") && context.GetCustomerType() != "D")
            //{
            //    return;
            //}
            //if (Keyword.Equals("Forget_PasswordEmail") && context.GetCustomerType() != "C")
            //{
            //    return;
            //}
            if ((Keyword.Equals("Forget_Login_Id_email") || Keyword.Equals("Forget_Login_Id_creditcardnumber") || Keyword.Equals("Forget_PasswordEmail")) && context.GetCustomerType() != "C")
            {
                return;
            }
            if ((Keyword.Equals("Forget_Login_Id_debitcardpin") || Keyword.Equals("Forget_Login_Id_debitcardnumber") || Keyword.Equals("Forget_PasswordCardPin")) && context.GetCustomerType() != "D")
            {
                return;
            }
            if (Keyword.Contains("SendMoney_Amount"))
            {
                context.Set_tran_amount(Convert.ToDouble(textboxvalue));
            }
            if (Keyword.Contains("SendMoney_SearchBeneField") || Keyword.Contains("BillPayment_SearchBeneField"))
            {
                context.SetCategory_value(textboxvalue);
            }
            if (Keyword.Equals("BillPayment_Transaction_Unpaid_Amount_Field"))
            {
                if (context.Get_Is_Partial_Payment_Allowed() != "1")
                {
                    return;
                }
            }

            if (String.IsNullOrEmpty(textboxvalue))
            {
                return;
            }
            try
            {
                //AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                //keyword.Locator used instead od locator
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                apmhelper.SetTextBoxValue(textboxvalue, keyword.Locator, locator_type);

            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }



        [When(@"verify the message ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        [Given(@"verify the message ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the message ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheMessageThroughDatabaseOnOn(string value, string query, string schema)
        {
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            //DataTable SourceDataTable2;
            if (query.Contains("{account_number}"))
            {
                query = query.Replace("{account_number}", context.GetBeneAccountNo());
            }
            if (query.Contains("{customer_name}"))
            {
                query = query.Replace("{customer_name}", context.GetUsername());
            }
            if (query.Contains("{customer_cnic}"))
            {
                query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
            }
            if (query.Contains("TT.FOLIO_NO"))
            {
                query = query.Replace("{GUID}", context.Get_HostReferenceNo());
            }
            if (query.Contains("TT.TRAN_AMOUNT"))
            {
                query = query.Replace("{GUID}", context.Get_HostReferenceNo());
            }
            if (query.Contains("{ConsumerNo}"))
            {
                query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
            }
            if (query.Contains("{Billing_Month}"))
            {
                query = query.Replace("{Billing_Month}", context.GetBilling_Month());
            }
            try
            {
                if (query.Contains("LP.BILL_STATUS_ID"))
                {
                    if (context.Get_Is_Paid_Marking_Req() != "1")
                    {
                        return;
                    }
                }
                if (query.Contains("K.IS_ACTIVE"))
                {
                    dlink = null;
                    dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);

                    List<string> account_list = new List<string>();
                    account_list = context.GetAccNumbers();
                    int count = account_list.Count;

                    List<string> db_account_list = new List<string>();

                    for (int i = 0; i < count; i++)
                    {
                        string message = SourceDataTable.Rows[i][0].ToString();
                        db_account_list.Add(message);
                    }
                    if (db_account_list.Count != account_list.Count)
                    {
                        throw new AssertFailedException(string.Format("Account Numbers during Sign up:{0} are not the same as in Database:{1}", account_list, db_account_list));
                    }
                }

                else if (query.Contains("K.IS_ACCOUNT_LINK='1'"))
                {
                    dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);

                    List<string> db_account_list = new List<string>();

                    List<string> account_list = new List<string>();
                    account_list = context.GetAccountForTag();
                    int count = account_list.Count;

                    for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                    {
                        string message = SourceDataTable.Rows[i][0].ToString();
                        db_account_list.Add(message);
                    }
                    if (db_account_list.Count != account_list.Count)
                    {
                        throw new AssertFailedException(string.Format("Account Numbers which were tagged during Sign up:{0} are not the same as in Database:{1}", account_list, db_account_list));
                    }
                }
                else if (query.Contains("K.IS_ACCOUNT_LINK='0'"))
                {
                    dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);

                    List<string> db_account_list = new List<string>();

                    List<string> account_list = new List<string>();
                    account_list = context.GetIsnotLinkedAccNumbers();
                    int count = account_list.Count;

                    for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                    {
                        string message = SourceDataTable.Rows[i][0].ToString();
                        db_account_list.Add(message);
                    }
                    if (db_account_list.Count != account_list.Count)
                    {
                        throw new AssertFailedException(string.Format("Account Numbers which were not tagged during Sign up:{0} are not the same as in Database:{1}", account_list, db_account_list));
                    }
                }
                else
                {

                    dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                    string message = SourceDataTable.Rows[0][0].ToString();

                    Assert.AreEqual(message, value);
                }

            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"verify the message using element ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the message using element ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheMessageUsingElementThroughDatabaseOnOnSchema(string Keyword, string query, string schema)
        {
            if (query != "")
            {
                try
                {
                    bool kmobile_msg_check = false;
                    if (query.Contains("{Company_Code}"))
                    {
                        query = query.Replace("{Company_Code}", context.GetCompany_Code());
                    }
                    if (query.Contains("{KMobileNo}"))
                    {
                        query = query.Replace("{KMobileNo}", context.Get_mobile_no());
                        kmobile_msg_check = true;
                    }
                    if (query.Contains("{customer_name}"))
                    {
                        query = query.Replace("{customer_name}", context.GetUsername());
                    }
                    string locator_type = "id";
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                    string message = SourceDataTable.Rows[0][0].ToString();
                    if(Keyword == "BillPayment_Fcy_Disclaimer")
                    {
                        message = context.GetPlainTextFromHtml(message);
                    }
                    decimal foreign_rate = 0;
                    if (Keyword.Equals("SendMoney_Buy_Rate") || Keyword.Equals("SendMoney_Converted_Amount"))
                    {
                        if (context.Get_FCY_Tran_Check() == true)
                        {
                            message = message.Split('{')[2];
                            message = '{' + message;
                            message = message.Remove(message.Length - 2, 2);
                            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                            foreign_rate = Convert.ToDecimal(values["conversionRate"]);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (Keyword.Equals("BillPayment_Conversion_Rate"))
                    {
                        if (context.Get_FCY_Tran_Check() == true)
                        {
                            message = message.Split('{')[2];
                            message = '{' + message;
                            message = message.Remove(message.Length - 2, 2);
                            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                            foreign_rate = Convert.ToDecimal(values["conversionRate"]);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (message.Contains("<br>"))
                    {
                        message = message.Replace("<br>", string.Empty);
                    }
                    if (kmobile_msg_check == true)
                    {
                        if (message == "")
                        {
                            query = "SELECT BB.FIRST_NAME FROM BB_CUSTOMER  BB WHERE BB.CONTACT_NUMBER = '" + context.Get_mobile_no() + "'";
                            dlink = new DataAccessComponent.DataAccessLink();
                            SourceDataTable = dlink.GetDataTable(query, "QAT_BB_SYSTEM");
                            message = SourceDataTable.Rows[0][0].ToString();
                            message = message.Remove(message.IndexOf(' '));
                        }
                        message = "HI, " + message + "!";
                    }

                    AppiumHelper apmhelper = new AppiumHelper();
                    //apmhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    //keyword.Locator used instead od locator
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string value = "";
                    if (Keyword.Equals("BillPayment_Fcy_Disclaimer"))
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            string temp = keyword.Locator.Replace("[i]", "[" + i.ToString() + "]");
                            value += apmhelper.ReturnKeywordValue(temp, "xpath");
                        }
                        value = value.Replace(" ", string.Empty);
                    }
                    else
                    {
                        value = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    }
                    if (Keyword.Equals("BillPayment_Conversion_Rate"))
                    {
                        if (context.Get_FCY_Tran_Check() == true)
                        {
                            string Foreign_Currency = SourceDataTable.Rows[0][1].ToString();
                            string PKR_Currency = SourceDataTable.Rows[0][2].ToString();
                            decimal currency_rate = Convert.ToDecimal(SourceDataTable.Rows[0][3].ToString());
                            
                            string [] amount_values = value.Split('=');
                            if (currency_rate != foreign_rate)
                            {
                                throw new Exception(string.Format("The currency rate in packet {0} is not equals to currency rate in Lead_Field3 {1}", foreign_rate, currency_rate));
                            }
                            if (PKR_Currency != "PKR")
                            {
                                throw new Exception(string.Format("The currency name in Lead Field2 is {0} which is not equal to PKR", PKR_Currency));
                            }
                            string foreign_currency_ui = amount_values[1].Substring(amount_values[1].Length - 3, 3);
                            if (Foreign_Currency != foreign_currency_ui)
                            {
                                throw new Exception(string.Format("The currency name in Lead Field1 is {0} which is not equal to {1}", Foreign_Currency, foreign_currency_ui));
                            }
                            decimal pkr_amount = Convert.ToDecimal(amount_values[0].Replace("PKR", string.Empty).Trim());
                            pkr_amount = pkr_amount / foreign_rate;
                            pkr_amount = Math.Round(pkr_amount,3);
                            int index = value.IndexOf('=');
                            message = value.Remove(index + 1, value.Length - 4 - index);
                            message = message.Replace("=", "= " + pkr_amount.ToString() + " ");
                        }
                    }
                    if (Keyword.Equals("SendMoney_Buy_Rate"))
                    {
                        if (context.Get_FCY_Tran_Check() == true)
                        {
                            string[] amount_values = value.Split('=');
                            //decimal pkr_amount = Convert.ToDecimal(amount_values[0].Replace("PKR", string.Empty).Trim());
                            foreign_rate = Math.Round(foreign_rate, 1);
                            //context.Set_conversion_rate(foreign_rate);
                            //amount_values[0] = amount_values[0].Remove(0, amount_values[0].IndexOf("USD"));
                            amount_values[1] = amount_values[1].Remove(0, amount_values[1].IndexOf("PKR"));
                            message = amount_values[0] + "= " + foreign_rate.ToString() + " " + amount_values[1];
                            //pkr_amount = pkr_amount / foreign_rate;
                            //pkr_amount = Math.Round(pkr_amount, 15);
                            //int index = value.IndexOf('=');
                            //message = value.Remove(index + 1, value.Length - 4 - index);
                            //message = message.Replace("=", "= " + pkr_amount.ToString() + " ");
                        }
                    }
                    if (Keyword.Equals("SendMoney_Converted_Amount"))
                    {
                        if (context.Get_FCY_Tran_Check() == true)
                        {
                            //string[] amount_values = value.Split('=');
                            foreign_rate = Math.Round(foreign_rate, 2);
                            //double tran_amount = 
                            foreign_rate = Convert.ToDecimal(context.Get_tran_amount()) * foreign_rate;
                            message = foreign_rate.ToString();
                        }
                    }

                    if (value.Contains("\r") || value.Contains("\n"))
                    {
                        value = value.Replace("\r", string.Empty).Replace("\n", string.Empty);
                    }
                    Assert.AreEqual(message, value);
                }
                catch (Exception exception)
                {
                    AppiumHelper.TakeScreenshot();
                    throw new AssertFailedException(exception.Message);
                }
            }
        }

        //[Given(@"verify through ""(.*)"" on ""(.*)""")]
        //[Then(@"verify through ""(.*)"" on ""(.*)""")]
        //[When(@"verify through ""(.*)"" on ""(.*)""")]
        //public void ThenVerifyThroughOn(string message, string Keyword)
        //{
        //    try
        //    {
        //        string locator_type = "id";
        //        AppiumHelper apmhelper = new AppiumHelper();
        //        //selhelper.checkPageIsReady();
        //        Thread.Sleep(3000);
        //        Element keyword = ContextPage.GetInstance().GetElement(Keyword);
        //        if (keyword.Locator.StartsWith("/"))
        //        {
        //            locator_type = "xpath";
        //        }
        //        //selhelper.Scroll(keyword.Locator);
        //        if (message == "ConsumerNoContextVal")
        //        {
        //            message = context.GetConsumer_No();
        //        }
        //        //if (message == "Signup_PassPolicy")
        //        //{
        //        //    for (int i = 1; i <= 3; i++)
        //        //    {
        //        //        string temp = "";
        //        //        if (i == 1)
        //        //        {
        //        //            temp = context.GetPassPolicy1();
        //        //        }
        //        //        else if (i == 2)
        //        //        {
        //        //            temp = context.GetPassPolicy2();
        //        //        }
        //        //        else if (i == 3)
        //        //        {
        //        //            temp = context.GetPassPolicy3();
        //        //        }
        //        //        string loc = keyword.Locator.Replace("[i]", "[" + (i) + "]");
        //        //        selhelper.verification(temp, keyword.Locator);
        //        //    }
        //        //    return;
        //        //}
        //        //if (message == "MyAccount_PassPolicy")
        //        //{
        //        //    selhelper.verification(context.GetPassPolicy1(), keyword.Locator);
        //        //    for (int i = 2; i <= 3; i++)
        //        //    {
        //        //        string temp = "";
        //        //        if (i == 2)
        //        //        {
        //        //            temp = context.GetPassPolicy2();
        //        //        }
        //        //        else if (i == 3)
        //        //        {
        //        //            temp = context.GetPassPolicy3();
        //        //        }
        //        //        if (Keyword.Equals("MyAccount_Forgot_UserPassPolicy1"))
        //        //        {
        //        //            Element keyword2 = ContextPage.GetInstance().GetElement("MyAccount_Forgot_UserPassPolicy2");
        //        //            string temp_loc = keyword2.Locator.Replace("[i]", "[" + (i) + "]");
        //        //            selhelper.verification(temp, temp_loc);
        //        //            temp_loc = null;
        //        //        }
        //        //        else if (Keyword.Equals("MyAccount_Forgot_TranPassPolicy1"))
        //        //        {
        //        //            Element keyword2 = ContextPage.GetInstance().GetElement("MyAccount_Forgot_TranPassPolicy2");
        //        //            string temp_loc = keyword2.Locator.Replace("[i]", "[" + (i) + "]");
        //        //            selhelper.verification(temp, temp_loc);
        //        //            temp_loc = null;
        //        //        }

        //        //    }
        //        //}
        //        //else
        //        //{
        //        apmhelper.verification(message, keyword.Locator, locator_type);
        //        //}
        //        //if (Keyword.Contains("Pay_Transaction_Success") || Keyword.Contains("SendMoney_TranSuccessMessage") || Keyword.Contains("MyAccount_Forgot_Status") || Keyword.Contains("MyAccount_PayOrder_Success") || Keyword.Contains("BeneManage_TranCongrats") || Keyword.Contains("MyAccount_CheqBook_TranMsg"))
        //        //{
        //        //    keyword = null;
        //        //    string tranid_keyword = "Pay_Transaction_ID";
        //        //    keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
        //        //    string tran_id = selhelper.ReturnKeywordValue(keyword.Locator);
        //        //    context.SetTransaction_Id(tran_id);
        //        //}

        //    }
        //    catch (Exception exception)
        //    {
        //        AppiumHelper.TakeScreenshot();
        //        throw new AssertFailedException(exception.Message);
        //    }
        //}
        [When(@"Set parameter in context class ""(.*)""")]
        public void WhenSetParameterInContextClass(string Keyword)
        {
            try
            {
                string locator_type = "id";
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                AppiumHelper apmhelper = new AppiumHelper();
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                string temp = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                if (Keyword.Equals("BillPayment_Bill_Status"))
                {
                    context.SetBill_Status(temp);
                }
                if (Keyword.Equals("BillPayment_Inquiry_BillingMonth"))
                {
                    DateTime temp_var = Convert.ToDateTime(temp);
                    temp = temp_var.ToString("MM/yyyy");
                    context.SetBilling_Month(temp);
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I want value from textview ""(.*)"" on database ""(.*)"" as ""(.*)""")]
        public void WhenIWantValueFromTextviewOnDatabaseAs(string Keyword, string db_value, string query)
        {
            try
            {
                string locator_type = "id";
                string value = "";
                string value2 = "";
                AppiumHelper apmhelper = new AppiumHelper();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (query != "")
                {
                    if (query.Contains("{ConsumerNo}"))
                    {
                        query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                        //query = a;
                    }
                    Thread.Sleep(2000);
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
                    value = SourceDataTable.Rows[0][0].ToString();
                    if (Keyword.Equals("Accounts_NoOfDays"))
                    {
                        string query_temp = query.Replace("'ACC_STATEMENT_MAX_DAY'", "'No of Days'");
                        SourceDataTable = null;
                        SourceDataTable = dLink.GetDataTable(query_temp, db_value);
                        value2 = SourceDataTable.Rows[0][0].ToString();
                        context.SetAccStatementDays(value2);
                    }
                    if (Keyword.Equals("BillPayment_Transaction_Unpaid_Amount") || Keyword.Equals("BillPayment_Transaction_Unpaid_Amount_Bene"))
                    {
                        if (context.Get_Is_Partial_Payment_Allowed() != "0")
                        {
                            return;
                        }
                        context.SetCompany_Code(SourceDataTable.Rows[0][1].ToString());
                        string temp_query = query.Replace("LB.BILL_AMOUNT", "LB.DUE_DATE");
                        SourceDataTable = null;
                        SourceDataTable = dLink.GetDataTable(temp_query, db_value);
                        value2 = SourceDataTable.Rows[0][2].ToString();
                        if (Convert.ToDateTime(value2) < DateTime.Today)
                        {
                            query = "SELECT L.CONSUMER_NAME_TEMPLATE FROM BPS_COMPANY_CHANNEL L WHERE L.CHANNEL_CODE='MB'  AND L.COMPANY_CODE = '" + context.GetCompany_Code() + "'";
                            SourceDataTable = dLink.GetDataTable(query, db_value);
                            value = SourceDataTable.Rows[0][0].ToString();
                            string code = "Payable After Due Date|<FS_01:";
                            //int b = value.IndexOf(code);
                            //int a = value.IndexOf(code) + code.Length;
                            code = value.Substring(value.IndexOf(code) + code.Length);
                            code = code.Split(new string[] { ">;" }, 2, StringSplitOptions.None)[0];
                            //query = temp_query.Replace("LB.DUE_DATE", "LB.ATTRIBUTE_" + code);
                            query = "SELECT * FROM (SELECT LB." + code + " FROM BPS_BILL_INFO LB WHERE LB.CONSUMER_NO='" + context.GetConsumer_No() + "' ORDER BY LB.CREATED_ON DESC) WHERE ROWNUM = 1";
                            //query = temp_query.Replace("LB.DUE_DATE", "LB." + code);
                            SourceDataTable = dLink.GetDataTable(query, db_value);
                            value = SourceDataTable.Rows[0][0].ToString();
                        }
                        //value = Convert.ToDecimal(value).ToString("0.00");
                    }


                }
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                string keyword_value = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                if (context.GetBill_Status() != "UNPAID")
                {
                    value = "";
                    keyword_value = "";
                }
                if (value != keyword_value)
                {
                    throw new AssertFailedException(string.Format("The Value against keyword is: {0} and value against db is:", keyword_value, value));
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I am performing on ""(.*)""")]
        public void WhenIAmPerformingOn(string Keyword)
        {
            string locator_type = "id";
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                apmhelper.Button(keyword.Locator, locator_type);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I am verifying OTP and Transaction pass check on company code ""(.*)""")]
        public void WhenIAmVerifyingOTPAndTransactionPassCheckOnCompanyCode(string company_code_value)
        {
            try
            {
                //string[] arr = company_code_value.Split(',');
                string query = "SELECT CC.IS_PWD_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = '" + company_code_value + "' AND CC.CHANNEL_CODE = 'MB'";
                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable(query, "QAT_BPS");
                string is_tran_req = SourceDataTable.Rows[0][0].ToString();
                context.Set_is_tranpass_req(is_tran_req);

                string query2 = "SELECT CC.IS_OTP_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = '" + company_code_value + "' AND CC.CHANNEL_CODE = 'MB'";
                DataAccessComponent.DataAccessLink dlink2 = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable2 = dlink2.GetDataTable(query2, "QAT_BPS");
                string is_otp_req = SourceDataTable2.Rows[0][0].ToString();
                context.Set_is_otp_req(is_otp_req);
                if (context.GetTranTypeBene() == true)
                {
                    context.Set_is_otp_req("0");
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I have otp check and given (.*) on ""(.*)""")]
        public void WhenIHaveOtpCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword)
        {
            try
            {
                if (context.Get_is_otp_req() == "1")
                {
                    string locator_type = "id";
                    AppiumHelper apmhelper = new AppiumHelper();
                    otp_value = apmhelper.GetOTP();
                    //selhelper.checkPageIsReady();
                    Thread.Sleep(3000);
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    apmhelper.scroll_to_element_text("One Time Password (OTP)");
                    apmhelper.SetTextBoxValue(otp_value, keyword.Locator, locator_type);
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }



        [When(@"I have transaction pass check and given (.*) on ""(.*)""")]
        public void WhenIHaveTransactionPassCheckAndGivenOnOnCompanyCode(string tran_pass_value, string Keyword)
        {
            try
            {
                if (context.Get_is_tranpass_req() == "1")
                {
                    string locator_type = "id";
                    AppiumHelper apmhelper = new AppiumHelper();
                    Thread.Sleep(3000);
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    apmhelper.scroll_to_element_text("Transaction Password");
                    apmhelper.SetTextBoxValue(tran_pass_value, keyword.Locator, locator_type);
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [Given(@"I am clicking on ""(.*)""")]
        [When(@"I am clicking on ""(.*)""")]
        [Then(@"I am clicking on ""(.*)""")]
        public void WhenIAmClickingOn(string Keyword)
        {

            string locator_type = "id";
            AppiumHelper apmhelper = new AppiumHelper();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            try
            {
                if (Keyword.Contains("BillPayment_Fcy_Toggle"))
                {
                    if (context.Get_FCY_Tran_Check() != true)
                    {
                        return;
                    }
                }
                if (Keyword.Equals("BillPayment_NextBtn_Fcy"))
                {
                    if(context.Get_FCY_Tran_Check() == true)
                    {
                        return;
                    }
                }

                //if (Keyword.Equals("Login_permission_allow_btn") || Keyword.Equals("Login_permission_allow_btn2"))
                //{
                //    apmhelper.rating(keyword.Locator);
                //    return;
                //}
                if (Keyword == "BillPayment_Rating" || Keyword == "BillPayment_RatingOkBtn" || Keyword == "BillPayment_Rating_Feedback_OkBtn" || Keyword == "BillPayment_Rating" || Keyword == "SendMoney_SkipBtn" || Keyword == "Login_permission_allow_btn" || Keyword == "Login_permission_allow_btn2")
                {

                    if (Keyword.Contains("Login_permission_allow_btn") || Keyword.Contains("Login_permission_allow_btn2"))
                    {
                        string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
                        platformVersion = platformVersion.Substring(0, platformVersion.Length - 2);
                        double pv = Convert.ToDouble(platformVersion);
                        if (pv > 10.0 && Keyword.Equals("Login_permission_allow_btn"))
                        {
                            keyword = ContextPage.GetInstance().GetElement("Login_permission_allow_btn_11");
                        }
                        if (pv > 10.0 && Keyword.Equals("Login_permission_allow_btn2"))
                        {
                            keyword = ContextPage.GetInstance().GetElement("Login_permission_allow_btn2");
                        }
                        if (Configuration.GetInstance().GetByKey("Manage_Calls_Permission").ToLower() == "no" && Keyword.Contains("Login_permission_allow_btn"))
                        {
                            keyword = ContextPage.GetInstance().GetElement("Login_permission_deny_btn");
                        }
                        if (Configuration.GetInstance().GetByKey("Location_Permission").ToLower() == "no" && Keyword.Contains("Login_permission_allow_btn2"))
                        {
                            keyword = ContextPage.GetInstance().GetElement("Login_permission_deny_btn");
                        }
                        

                    }
                    //Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    apmhelper.rating(keyword.Locator);
                    return;
                }
                if (Keyword.Equals("MutualFund_PopupBtn") && context.Get_MutualFundDisclaimerPopup() == "")
                {
                    return;
                }
                else if (Keyword == "Registration_AccountNo_Marking")
                {
                    if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
                    {
                        locator_type = "xpath";
                    }
                    List<string> AccList = new List<string>();
                    AccList = context.GetAccountForTag();
                    foreach (var acc_no in AccList)
                    {
                        string locator = keyword.Locator.Replace("{AccountNo}", acc_no);
                        apmhelper.links(locator, locator_type);
                    }
                }
                else if (!String.IsNullOrEmpty(Keyword))
                {
                    if (Keyword.Contains("BillPayment_AddNewBtn"))
                    {
                        string query = "SELECT COUNT(*) FROM DC_BILL_PAYMENT_BENEFICIARY BB WHERE BB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME ='" + context.GetUsername() + "') AND BB.COMPANY_SUB_CATEGORY LIKE '%" + context.GetCategory_value() + "%' AND BB.IS_ACTIVE = 1";
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                        int a = Convert.ToInt32(SourceDataTable.Rows[0][0]);
                        if (Convert.ToInt32(SourceDataTable.Rows[0][0]) == 0)
                        {
                            return;
                        };
                    }
                    if (Keyword.Equals("BillPayment_MultiPayment_Schedule_Toggle"))
                    {
                        string query = "SELECT CH.IS_SI_ALLOWED FROM BPS_COMPANY_CHANNEL CH WHERE CH.COMPANY_CODE = '" + context.GetCompany_Code() + "' AND CH.CHANNEL_CODE = 'MB'";
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, "QAT_BPS");
                        string is_si_allowed = SourceDataTable.Rows[0][0].ToString();
                        if (is_si_allowed != "1")
                        {
                            throw new Exception(string.Format("Schedule Payment is not allowed on company code: " + context.GetCompany_Code()));
                        }
                    }
                    if (Keyword.Contains("BillPayment_CheckNextBtn"))
                    {
                        if ((context.Get_is_otp_req() == "0" && context.Get_is_tranpass_req() == "0") || (context.Get_is_otp_req() == "1" && context.Get_is_tranpass_req() == "0") || (context.Get_is_otp_req() == "0" && context.Get_is_tranpass_req() == "1"))
                        {
                            return;
                        }
                    }
                    if (Keyword.Contains("SendMoney_AddNewBtn_interbranch"))
                    {
                        if ((context.Get_no_of_accounts() + context.Get_bene_count_inter_branch()) <= 1)
                        {
                            return;
                        }
                    }
                    if (Keyword.Contains("SendMoney_AddNewBtn_interbank"))
                    {
                        if (context.Get_bene_count_inter_bank() <= 1)
                        {
                            return;
                        }
                    }
                    if (Keyword == "SendMoney_AddNewBtn")
                    {
                        //int a = (context.Get_no_of_accounts() + context.Get_bene_count_inter_branch() + context.Get_bene_count_inter_bank());
                        if ((context.Get_no_of_accounts() + context.Get_bene_count_inter_branch() + context.Get_bene_count_inter_bank()) <= 1)
                        {
                            return;
                        }
                    }
                    if (Keyword.Contains("MutualFund_InvestBtn"))
                    {
                        string temp = keyword.Locator.Replace("{Fund_Name}", context.Get_mutual_fund_name());
                        keyword.Locator = temp;
                    }
                    if (Keyword == "BillPayment_Category")
                    {
                        string temp = keyword.Locator.Replace("{BillPaymentCategory}", context.Get_BillPaymentCategory());
                        keyword.Locator = temp;
                    }
                    //if (Keyword.Contains("BillPayment_PayNextBtn"))
                    //{
                    //    string otp = context.Get_is_otp_req();
                    //    string tran_pass = context.Get_is_tranpass_req();
                    //    if ((otp == "0" && tran_pass == "0") || (otp == "1" && tran_pass == "0") || (otp == "0" && tran_pass == "1"))
                    //    {
                    //        return;
                    //    }
                    //}

                    //AppiumHelper apmhelper = new AppiumHelper();
                    //apmhelper.checkPageIsReady();

                    if (Keyword.Contains("TermDeposit_NoOfYears"))
                    {
                        string temp = keyword.Locator.Replace("{Years}", context.Get_TermDepositYears());
                        keyword.Locator = temp;
                    }
                    if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
                    {
                        locator_type = "xpath";
                    }
                    apmhelper.links(keyword.Locator, locator_type);
                }


            }

            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [Then(@"verify the schedule config ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheScheduleConfigOnSchema(string query, string db_value)
        {
            try
            {
                if (query.Contains("{Username}"))
                {
                    query = query.Replace("{Username}", context.GetUsername());
                }
                if (query.Contains("{ConsumerNo}"))
                {
                    query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                }
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
                string first_date = SourceDataTable.Rows[0][0].ToString();
                string last_date = SourceDataTable.Rows[0][1].ToString();

                DateTime EndDate = DateTime.Parse(last_date);
                DateTime StartDate = DateTime.Parse(first_date);

                string datestring = EndDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
                string datestring2 = StartDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);

                double date_diff = ((EndDate - StartDate).TotalDays) / 30;

                int res = Convert.ToInt32(date_diff) + 1;

                if (res != Convert.ToInt32(context.Get_BP_schedule_config()))
                {
                    throw new AssertFailedException(string.Format("The scheduled config {0} is not equal to newly scheduled bill {1}", res, context.Get_BP_schedule_config()));

                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I save Account Numbers")]
        public void WhenISaveAccountNumbers()
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                Element keyword = ContextPage.GetInstance().GetElement("Registration_AccountNo_Size");
                string locator_type = "id";
                if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
                {
                    locator_type = "xpath";
                }
                int count = apmhelper.SizeCountElements(keyword.Locator, locator_type);
                List<string> lst = new List<string>();
                List<string> lst_acc_no_total = new List<string>();
                string acc_no = "";

                for (int i = 1; i <= count; i++)
                {
                    string temp = keyword.Locator.Replace(")", ")[" + i + "]");
                    acc_no = apmhelper.ReturnKeywordValue(temp, "xpath");
                    if (acc_no.Contains("ON"))
                    {
                        acc_no = Regex.Replace(acc_no, "[A-Za-z ]", "").TrimEnd();
                        lst_acc_no_total.Add(acc_no.ToString());
                        continue;
                    }
                    else if (acc_no.Contains("OFF"))
                    {
                        acc_no = Regex.Replace(acc_no, "[A-Za-z ]", "").TrimEnd();
                        lst_acc_no_total.Add(acc_no.ToString());
                        lst.Add(acc_no.ToString());
                    }

                }

                context.SetIsnotLinkedAccNumbers(lst);
                context.SetAccNumbers(lst_acc_no_total);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"update the data by query ""(.*)"" on Schema ""(.*)""")]
        [When(@"update the data by query ""(.*)"" on Schema ""(.*)""")]
        [Then(@"update the data by query ""(.*)"" on Schema ""(.*)""")]
        public void GivenUpdateTheDataByQueryOnSchema(string query, string schema)
        {
            if (query != "")
            {
                if (query.Contains("{ConsumerNo}"))
                {
                    query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                }
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                if (query.Contains("{username}"))
                {
                    query = query.Replace("{username}", context.GetUsername());
                }
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, schema);
                }
                catch (Exception e)
                {
                    AppiumHelper.TakeScreenshot();
                    throw new AssertFailedException(e.Message);

                }
            }
        }

        [Given(@"update the data by query ""(.*)"" on DIGITAL_CHANNEL_SEC")]
        public void GivenUpdateTheDataByQueryOnDIGITAL_CHANNEL_SEC(string query)
        {
            if (query != "")
            {
                if (query.Contains("{ConsumerNo}"))
                {
                    query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                }
                if (query.Contains("{username}"))
                {
                    query = query.Replace("{username}", context.GetUsername());
                }
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, "DIGITAL_CHANNEL_SEC");
                }
                catch (Exception e)
                {
                    AppiumHelper.TakeScreenshot();
                    throw new AssertFailedException(e.Message);

                }
            }
        }
        [Given(@"update the data by query ""(.*)"" on QAT_BPS")]
        public void GivenUpdateTheDataByQueryOnQAT_BPS(string query)
        {
            if (query != "")
            {
                if (query.Contains("{ConsumerNo}"))
                {
                    query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                }
                if (query.Contains("{username}"))
                {
                    query = query.Replace("{username}", context.GetUsername());
                }
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, "QAT_BPS");
                }
                catch (Exception e)
                {
                    AppiumHelper.TakeScreenshot();
                    throw new AssertFailedException(e.Message);

                }
            }

        }



        [When(@"I select ""(.*)"" on ""(.*)""")]
        public void WhenISelectOn(string value, string Keyword)
        {
            string locator_type = "id";
            if (String.IsNullOrEmpty(value))
            {
                return;
            }
            try
            {
                if (context.GetCredit_Card_Check() != "" && context.GetCredit_Card_Check() != null)
                {
                    StringBuilder st = new StringBuilder(value);
                    st.Remove(4, 8);
                    st.Insert(4, "xxxxxxxx");
                    value = st.ToString();
                }
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                apmhelper.combobox(value, keyword.Locator, locator_type);
                if (Keyword.Equals("SendMoney_SchedulePayment_Frequency"))
                {
                    context.Setfrequency(value);
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I check values of combobox using database from ""(.*)"" on schema (.*) on combobox ""(.*)"" of list ""(.*)""")]
        public void WhenICheckValuesOfComboboxUsingDatabaseFromOnSchemaOnComboboxOfList(string query, string schema, string Keyword, string Lst_Keyword)
        {
            try
            {
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                AppiumHelper apmhelper = new AppiumHelper();
                string locator_type = "id";
                List<string> ui_list = new List<string>();
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                Element lst_keyword = ContextPage.GetInstance().GetElement(Lst_Keyword);
                if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
                {
                    locator_type = "xpath";
                }
                ui_list = apmhelper.return_combobox_values(keyword.Locator, lst_keyword.Locator, locator_type);
                if (ui_list.Count == SourceDataTable.Rows.Count)
                {
                    for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                    {
                        //count bhi lgalo and is deleted update krna
                        if (ui_list[i] != SourceDataTable.Rows[i][0].ToString())
                        {
                            throw new AssertFailedException(string.Format("The record on UI is {0} and record in database is {1}", ui_list[i], SourceDataTable.Rows[i][0].ToString()));
                        }
                    }
                }
                else
                {
                    throw new AssertFailedException(string.Format("The Record Count of UI is {0} and Record Count in Database is {1}", ui_list.Count.ToString(), SourceDataTable.Rows.Count.ToString()));
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheResultFromOnSchema(string query, string db_value)
        {
            try
            {
                if (query != "")
                {
                    if (query.Contains("Company_Code"))
                    {
                        query = query.Replace("{Company_Code}", context.GetCompany_Code());
                    }
                    if (query.Contains("customer_cnic"))
                    {
                        query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                    }

                    Thread.Sleep(2000);
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
                    string inst_type = SourceDataTable.Rows[0][0].ToString();

                    if (SourceDataTable.Rows.Count == 0)
                    {
                        throw new AssertFailedException(string.Format("there exists no record in database against query: {0}", query));
                    }

                    if (query.Contains("Instrument_type"))
                    {
                        string acc_type = context.GetAccountType();
                        if (!(inst_type.Contains(acc_type)))
                        {
                            throw new AssertFailedException(string.Format("The instrument Type against company : {0} and the expected instrument type is {1}", inst_type, acc_type));
                        }

                    }

                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        [When(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheListUsingOnSchema(string query, string schema)
        {
            try
            {
                List<string> message = new List<string>();
                List<string> db_result = new List<string>();
                //string db_result_value = "";
                if (query != "")
                {
                    if (query.Contains("{account_number}"))
                    {
                        query = query.Replace("{account_number}", context.GetBeneAccountNo());
                    }
                    if (query.Contains("{customer_name}"))
                    {
                        query = query.Replace("{customer_name}", context.GetUsername());
                    }
                    if (query.Contains("{customer_cnic}"))
                    {
                        query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                    }
                    if (query.Contains("Company_Code"))
                    {
                        query = query.Replace("{Company_Code}", context.GetCompany_Code());
                    }
                    //if (query.Contains("SELECT PP.NAME_OF_FUND from QAT_AMC.AMC_PRODUCT_PROFILE"))
                    //{
                    message = context.Get_scroll_items_list();
                    //}
                    if (query.Contains("PB.CONSUMER_NUMBER"))
                    {
                        message.Sort();
                    }
                    Thread.Sleep(2000);
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                    for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                    {
                        if (message[i] != SourceDataTable.Rows[i][0].ToString())
                        {
                            //string ali = SourceDataTable.Rows[i][0].ToString();

                            throw new AssertFailedException(string.Format("The Value of code is {0} and value of db is", message[i], SourceDataTable.Rows[i][0]));
                        }
                        //db_result.Add(SourceDataTable.Rows[i][0].ToString());
                    }
                    //if (message != db_result)
                    //{
                    //    throw new AssertFailedException(string.Format("The Value of code is {0} and value of db is", message, db_result));
                    //}
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [Given(@"verify the data using ""(.*)"" on Schema ""(.*)""")]
        [When(@"verify the data using ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the data using ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheDataUsingOnSchema(string query, string schema)
        {
            try
            {
                string message = "";
                string db_result = "";
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable2;
                if (query != "")
                {
                    if (query.Contains("{account_number}"))
                    {
                        query = query.Replace("{account_number}", context.GetBeneAccountNo());
                    }
                    if (query.Contains("{customer_name}"))
                    {
                        query = query.Replace("{customer_name}", context.GetUsername());
                    }
                    if (query.Contains("{customer_cnic}"))
                    {
                        query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                    }
                    if (query.Contains("Company_Code"))
                    {
                        query = query.Replace("{Company_Code}", context.GetCompany_Code());
                    }
                    //if (query.Contains("TT.CUSTOMER_NAME"))
                    //{
                    //    query = query.Replace("{GUID}", context.Get_HostReferenceNo());
                    //    string ali = "SELECT CI.FIRST_NAME FROM DC_CUSTOMER_INFO CI WHERE CI.CNIC = '" + context.GetCustomerCNIC() + "' AND CI.CUSTOMER_NAME = '" + context.GetUsername() + "'";
                    //    SourceDataTable2 = dLink.GetDataTable(ali,"DIGITAL_CHANNEL_SEC");
                    //    message = SourceDataTable2.Rows[0][0].ToString();
                    //}
                    if (query.Contains("TT.CUSTOMER_CNIC"))
                    {
                        query = query.Replace("{GUID}", context.Get_HostReferenceNo());
                        message = context.GetCustomerCNIC();
                    }
                    if (query.Contains("TT.CUSTOMER_MOBILE_NO"))
                    {
                        query = query.Replace("{GUID}", context.Get_HostReferenceNo());
                        SourceDataTable2 = dLink.GetDataTable("SELECT CI.MOBILE_NO FROM DC_CUSTOMER_INFO CI WHERE CI.CNIC = '" + context.GetCustomerCNIC() + "'", "DIGITAL_CHANNEL_SEC");
                        message = SourceDataTable2.Rows[0][0].ToString();
                    }
                    Thread.Sleep(2000);
                    DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                    db_result = SourceDataTable.Rows[0][0].ToString();
                    if (db_result.StartsWith("92"))
                    {
                        db_result = db_result.Remove(0, 2);
                        db_result = "0" + db_result;
                        //db_result = db_result.Replace("92", "0");
                    }
                    if (query.Contains("CREATED_ON") || (query.Contains("UPDATED_ON")) || (query.Contains("LAST_PASSWORD_CHANGED")) || (query.Contains("LAST_TRANS_PASSWORD_CHANGED")))
                    {
                        DateTime dt = Convert.ToDateTime(db_result);
                        db_result = dt.ToString("MM/dd/yyyy");
                        message = DateTime.Today.ToString("MM/dd/yyyy");
                    }
                    if (query.Contains("IVR_REQUIRED"))
                    {
                        context.SetIVRReq(db_result);
                    }
                    if (query.Contains("IS_IVR_ENABLED"))
                    {
                        if ((context.GetIVRReq() == "1" && db_result == "0"))
                        {
                            return;
                        }
                        else if (context.GetIVRReq() == "0" && db_result == "1")
                        {
                            return;
                        }

                    }
                    if (query.Contains("ENABLE_PSD_BIOMETRIC"))
                    {
                        context.SetEnablePSD(db_result);
                    }
                    if (query.Contains("Z.ENABLE_PSD "))
                    {
                        if ((context.GetEnablePSD() == "0" && db_result == "1") || (context.GetEnablePSD() == "1" && db_result == "0") || (context.GetEnablePSD() == "" && db_result == ""))
                        {
                            throw new AssertFailedException("ENABLE_PSD setting is not correct");
                        }
                    }
                    if (query.Contains("CUSTOMER_TYPE"))
                    {
                        context.SetCustomerType(db_result);
                    }

                    if (query.Contains("last_login"))
                    {
                        if (db_result == "")
                        {
                            if (context.GetLastLoginFlag() == true)
                            {
                                throw new AssertFailedException("Last Login is not updated in data base");
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (db_result != "")
                        {
                            if (context.GetLastLoginFlag() == false)
                            {
                                throw new AssertFailedException("Last login is not updated in data base");
                            }
                            else
                            {
                                DateTime lastlogin = Convert.ToDateTime(db_result);
                                db_result = lastlogin.ToString("MM/dd/yyyy");
                                string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                                message = today_date;
                                //Assert.AreEqual(db_result, today_date);
                            }
                        }
                        if (query.Contains("PARAM_CHANNEL_ID"))
                        {
                            if (db_result != "1")
                            {
                                throw new AssertFailedException(string.Format("The PARAM_CHANNEL_ID :{0} is not valid for HBL Web Internet Banking", db_result));
                            }
                        }
                        if (message != db_result)
                        {
                            throw new AssertFailedException(string.Format("The Value of code is {0} and value of db is", message, db_result));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [When(@"I select from date ""(.*)""")]
        public void WhenISelectFromDate(string date)
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                DateTime current_date = DateTime.Today;
                //DateTime given_date = Convert.ToDateTime(date);
                DateTime given_date = current_date.AddDays(Convert.ToInt32(date));
                string xpath_date = given_date.ToString("dd MMMM yyyy");
                int monthsApart = 12 * (current_date.Year - given_date.Year) + current_date.Month - given_date.Month;
                monthsApart = Math.Abs(monthsApart);
                for (int i = 0; i < monthsApart; i++)
                {
                    string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
                    platformVersion = platformVersion.Substring(0, platformVersion.Length - 2);
                    double pv = Convert.ToDouble(platformVersion);
                    if (pv <= 5.0)
                    {
                        apmhelper.scroll_down(0.65, 0.30);
                    }
                    else
                    {
                        apmhelper.scroll_right(0.6, 0.8, 0.2);
                    }
                }
                Element keyword = ContextPage.GetInstance().GetElement("SendMoney_SchedulePayment_CalendarDateSelect");
                //string temp = keyword.Locator.Replace("{calendar_date}", date.Split('/')[1].TrimStart(new Char[] { '0' }).ToString());
                string temp = keyword.Locator.Replace("{calendar_date}", xpath_date);
                //keyword.Locator = temp;
                apmhelper.links(temp, "xpath");
                context.Setcalendar_fromdate(given_date);
                keyword = ContextPage.GetInstance().GetElement("SendMoney_SchedulePayment_CalendarDateSelect_OK");
                apmhelper.links(keyword.Locator, "id");
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I select to date ""(.*)""")]
        public void WhenISelectToDate(string date)
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                DateTime current_date = DateTime.Today;
                //DateTime given_date = Convert.ToDateTime(date);
                DateTime given_date = Convert.ToDateTime(current_date.AddDays(Convert.ToInt32(date)));
                string xpath_date = given_date.ToString("dd MMMM yyyy");
                int monthsApart = 12 * (current_date.Year - given_date.Year) + current_date.Month - given_date.Month;
                monthsApart = Math.Abs(monthsApart);
                for (int i = 0; i < monthsApart; i++)
                {
                    string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
                    platformVersion = platformVersion.Substring(0, platformVersion.Length - 2);
                    double pv = Convert.ToDouble(platformVersion);
                    if (pv <= 5.0)
                    {
                        apmhelper.scroll_down(0.65, 0.30);
                    }
                    else
                    {
                        apmhelper.scroll_right(0.6, 0.7, 0.25);
                    }
                }
                Element keyword = ContextPage.GetInstance().GetElement("SendMoney_SchedulePayment_CalendarDateSelect");
                string temp = keyword.Locator.Replace("{calendar_date}", xpath_date);
                //keyword.Locator = temp;
                apmhelper.links(temp, "xpath");
                context.Setcalendar_todate(given_date);
                keyword = ContextPage.GetInstance().GetElement("SendMoney_SchedulePayment_CalendarDateSelect_OK");
                apmhelper.links(keyword.Locator, "id");
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I am verifying list of execution iterations on ""(.*)""")]
        public void WhenIAmVerifyingListOfExecutionIterationsOn(string Keyword)
        {
            try
            {
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                AppiumHelper apmhelper = new AppiumHelper();
                //selhelper.checkPageIsReady();
                //int loop_counter = 0;
                int loop_increment_counter = 0;
                string frequency = context.Getfrequency();
                if (frequency == "Daily")
                {
                    loop_increment_counter = 1;
                }
                else if (frequency == "Weekly")
                {
                    loop_increment_counter = 7;
                }
                else if (frequency == "Fortnightly")
                {
                    loop_increment_counter = 15;
                }
                else if (frequency == "Monthly")
                {
                    loop_increment_counter = 30;
                }
                else if (frequency == "Quarterly")
                {
                    loop_increment_counter = 90;
                }
                List<string> lst = new List<string>();
                List<string> lstui = new List<string>();
                double difference = 0;
                DateTime st_date = context.Getcalendar_fromdate().Date;
                DateTime ed_date = context.Getcalendar_todate().Date;
                difference = ((ed_date - st_date).TotalDays) + 1;
                difference = difference / loop_increment_counter;
                int diff = Convert.ToInt32(Math.Ceiling(difference));
                int counter = 1;
                for (int i = 0; i < diff * loop_increment_counter; i += loop_increment_counter)
                {
                    DateTime temp = (st_date.Date.AddDays(i));
                    temp = temp.Date;
                    lst.Add(temp.ToString("dd-MM-yyyy"));
                    apmhelper.scroll_to_element_text(temp.ToString("dd-MM-yyyy"));
                    string locator = keyword.Locator.Replace("{Date}", lst[counter - 1].ToString());
                    lstui.Add(apmhelper.ReturnKeywordValue(locator, "xpath"));
                    if (lst[counter - 1] != lstui[counter - 1])
                    {
                        throw new AssertFailedException(string.Format("The Iteration Date against keyword is: {0} and Iteration Date calculated by code is {1}", lstui[counter], lst[counter]));
                    }
                    //if (counter != 0 && counter % 8 == 0)
                    //{
                    //    apmhelper.scroll_down(0.65, 0.30);
                    //}
                    counter++;
                }
                context.Set_iteration_dates_schedule(lst);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        //[When(@"I am verifying list of execution iterations on ""(.*)""")]
        //public void WhenIAmVerifyingListOfExecutionIterationsOn(string Keyword)
        //{
        //    try
        //    {
        //        Element keyword = ContextPage.GetInstance().GetElement(Keyword);
        //        AppiumHelper apmhelper = new AppiumHelper();
        //        //selhelper.checkPageIsReady();
        //        //int loop_counter = 0;
        //        int loop_increment_counter = 0;
        //        string frequency = context.Getfrequency();
        //        if (frequency == "Daily")
        //        {
        //            loop_increment_counter = 1;
        //        }
        //        else if (frequency == "Weekly")
        //        {
        //            loop_increment_counter = 7;
        //        }
        //        else if (frequency == "Fortnightly")
        //        {
        //            loop_increment_counter = 15;
        //        }
        //        else if (frequency == "Monthly")
        //        {
        //            loop_increment_counter = 30;
        //        }
        //        else if (frequency == "Quarterly")
        //        {
        //            loop_increment_counter = 90;
        //        }
        //        List<string> lst = new List<string>();
        //        List<string> lstui = new List<string>();
        //        double difference = 0;
        //        DateTime st_date = context.Getcalendar_fromdate().Date;
        //        DateTime ed_date = context.Getcalendar_todate().Date;
        //        difference = ((ed_date - st_date).TotalDays) + 1;
        //        difference = difference / loop_increment_counter;
        //        int diff = Convert.ToInt32(Math.Ceiling(difference));
        //        int counter = 1;
        //        for (int i = 0; i < diff * loop_increment_counter; i += loop_increment_counter)
        //        {
        //            DateTime temp = (st_date.Date.AddDays(i));
        //            temp = temp.Date;
        //            lst.Add(temp.ToString("dd-MM-yyyy"));
        //            string locator = keyword.Locator.Replace("{Date}", lst[counter - 1].ToString());
        //            lstui.Add(apmhelper.ReturnKeywordValue(locator, "xpath"));
        //            if (lst[counter - 1] != lstui[counter - 1])
        //            {
        //                throw new AssertFailedException(string.Format("The Iteration Date against keyword is: {0} and Iteration Date calculated by code is {1}", lstui[counter], lst[counter]));
        //            }
        //            if (counter != 0 && counter % 8 == 0)
        //            {
        //                apmhelper.scroll_down(0.65, 0.30);
        //            }
        //            counter++;
        //        }
        //        context.Set_iteration_dates_schedule(lst);
        //    }
        //    catch (Exception exception)
        //    {
        //        AppiumHelper.TakeScreenshot();
        //        throw new AssertFailedException(exception.Message);
        //    }
        //}

        [When(@"I scroll down")]
        public void WhenIScrollDown()
        {
            Thread.Sleep(2000);
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                apmhelper.scroll_down(0.65, 0.30);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        //[When(@"I scroll to save the elements of list on ""(.*)""")]
        //public void WhenIScrollToSaveTheElementsOfListOn(string scroll_text)
        //{

        //}

        [When(@"I scroll to element text as ""(.*)""")]
        public void WhenIScrollToElementTextAs(string scroll_text)
        {
            Thread.Sleep(2000);
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                apmhelper.scroll_to_element_text(scroll_text);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I set list of elements from scroll view on ""(.*)"" as ""(.*)""")]
        public void WhenISetListOfElementsFromScrollViewOnAs(string Keyword, int count)
        {
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            AppiumHelper apmhelper = new AppiumHelper();
            List<string> lst = new List<string>();
            List<string> lst_ui = new List<string>();
            string locator_type = "id";
            if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
            {
                locator_type = "xpath";
            }
            try
            {
                for (int i = 0; i < count; i++)
                {
                    if (i != 0)
                    {
                        apmhelper.scroll_down(0.65, 0.20);
                    }

                    lst_ui = apmhelper.Return_Keyword_Elements_List(keyword.Locator, locator_type);
                    string[] other_elements = lst_ui.Except(lst).ToArray();
                    for (int j = 0; j < other_elements.Length; j++)
                    {
                        lst.Add(other_elements[j]);
                    }
                    lst_ui = null;
                }
                if (Keyword == "MutualFund_List_FundName")
                {
                    lst.RemoveAt(0);
                }
                context.Set_scroll_items_list(lst);

            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        //[When(@"I verify array of elements from scroll view on ""(.*)"" as ""(.*)"" on schema ""(.*)"" of ""(.*)""")]
        //public void WhenIVerifyArrayOfElementsFromScrollViewOnAsOnSchemaOf(string Keywords, string query, string schema, int column_count)
        //{
        //    string[] keywords_array = Keywords.Split(',');
        //    Element keyword;
        //    AppiumHelper apmhelper = new AppiumHelper();
        //    //string[,] arr_ui;
        //    //string[,] arr;
        //    string locator_type = "id";
        //    try
        //    {
        //        if (query.Contains("{username}"))
        //        {
        //            query = query.Replace("{username}", context.GetUsername());
        //        }
        //        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
        //        DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
        //        //Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
        //        Dictionary<string, string> dict = new Dictionary<string, string>();
        //        List<string> lst = new List<string>();
        //        List<string> keys = new List<string>();
        //        List<string> values = new List<string>();
        //        for (int i = 0; i < 3; i++)
        //        {
        //            if (i != 0)
        //            {
        //                apmhelper.scroll_down(0.65, 0.20);
        //            }
        //            //string temp_value = "";
        //            //for (int j = 0; j < keywords_array.Length; j++)
        //            //{
        //            //    //if(j == 0)
        //            //    //{
        //            keyword = ContextPage.GetInstance().GetElement(keywords_array[0].ToString());
        //            keys = apmhelper.Return_Keyword_Elements_List(keyword.Locator, locator_type);
        //            keyword = ContextPage.GetInstance().GetElement(keywords_array[0].ToString());
        //            values = apmhelper.Return_Keyword_Elements_List(keyword.Locator, locator_type);
        //            //    if(j != 0)
        //            //    {

        //            //    }
        //            //    //}
        //            //}
        //            //keyword = ContextPage.GetInstance().GetElement(keywords_array[0].ToString());
        //            //temp = apmhelper.Return_Keyword_Elements_List(keyword.Locator, locator_type);
        //            //keyword = ContextPage.GetInstance().GetElement(keywords_array[1].ToString());
        //            //temp = apmhelper.Return_Keyword_Elements_List(keyword.Locator, locator_type);
        //            //lst.Add(temp);
        //            //string[] other_elements = lst_ui.Except(lst).ToArray();
        //            //for (int j = 0; j < other_elements.Length; j++)
        //            //{
        //            //    lst.Add(other_elements[j]);
        //            //}
        //            //lst_ui = null;
        //        }
        //        //        arr = new string[SourceDataTable.Rows.Count, Convert.ToInt32(column_count)];
        //        //        arr_ui = new string[SourceDataTable.Rows.Count, Convert.ToInt32(column_count)];
        //        //        for (int i = 0; i < SourceDataTable.Rows.Count; i++)
        //        //        {

        //        //            keyword = ContextPage.GetInstance().GetElement(keywords_array[0].ToString());
        //        //            if (i != 0)
        //        //            {
        //        //                apmhelper.scroll_to_element_text(SourceDataTable.Rows[i][0].ToString());
        //        //            }

        //        //            for (int j = 0; j < Convert.ToInt32(column_count); j++)
        //        //            {
        //        //                keyword = null;
        //        //                keyword = ContextPage.GetInstance().GetElement(keywords_array[j].ToString());
        //        //                if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
        //        //                {
        //        //                    locator_type = "xpath";
        //        //                }
        //        //                if (keyword.Locator.Contains("{ConsumerNo}"))
        //        //                {
        //        //                    string temp = keyword.Locator.Replace("{ConsumerNo}", SourceDataTable.Rows[i][0].ToString());
        //        //                    keyword.Locator = temp;
        //        //                    locator_type = "xpath";
        //        //                }
        //        //                arr_ui[i, j] = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
        //        //                if (SourceDataTable.Rows[i][j].ToString() != arr_ui[i, j])
        //        //                {
        //        //                    string abs = SourceDataTable.Rows[i][j].ToString();
        //        //                }
        //        //            }
        //        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        AppiumHelper.TakeScreenshot();
        //        throw new AssertFailedException(exception.Message);
        //    }
        //}


        //[When(@"I verify array of elements from scroll view on ""(.*)"" as ""(.*)"" of ""(.*)""")]
        //public void WhenIVerifyArrayOfElementsFromScrollViewOnAsOf(string Keyword_array, int count, int column_count)
        //{
        //    string[] keyword_array = Keyword_array.Split(',');
        //    Element keyword;
        //    AppiumHelper apmhelper = new AppiumHelper();
        //    //List<string> lst = new List<string>();
        //    //List<string> lst_ui = new List<string>();
        //    string[,] arr_ui;
        //    string[,] arr;
        //    string locator_type = "id";

        //    try
        //    {
        //        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
        //        DataTable SourceDataTable = dlink.GetDataTable("SELECT PB.CONSUMER_NUMBER,PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE,PB.COMPANY_NAME,PB.CONSUMER_NAME FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'RIZI') AND PB.IS_ACTIVE = 1", "DIGITAL_CHANNEL_SEC");
        //        //message = SourceDataTable.Rows[0][0].ToString();
        //        arr = new string[SourceDataTable.Rows.Count, column_count];
        //        arr_ui = new string[SourceDataTable.Rows.Count,column_count];
        //        //string element_text = "";
        //        for (int i = 0; i < SourceDataTable.Rows.Count; i++)
        //        {

        //            keyword = ContextPage.GetInstance().GetElement(Keyword_array[0].ToString());
        //            //if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
        //            //{
        //            //    locator_type = "xpath";
        //            //}
        //            if (i != 0)
        //            {
        //                apmhelper.scroll_to_element_text(SourceDataTable.Rows[i][0].ToString());
        //            }

        //            for (int j = 0; j < column_count; j++)
        //            {
        //                keyword = null;
        //                keyword =  ContextPage.GetInstance().GetElement(Keyword_array[j].ToString());
        //                if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
        //                {
        //                    locator_type = "xpath";
        //                }
        //                arr_ui[i, j] = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
        //                if(SourceDataTable.Rows[i][j].ToString() != arr_ui[i, j])
        //                {

        //                }
        //            }
        //        }
        //        //if (Keyword == "MutualFund_List_FundName")
        //        //{
        //        //    lst.RemoveAt(0);
        //        //}
        //        //context.Set_scroll_items_list(lst);
        //    }
        //    catch (Exception exception)
        //    {
        //        AppiumHelper.TakeScreenshot();
        //        throw new AssertFailedException(exception.Message);
        //    }
        //}



        [When(@"I wait (.*)")]
        [Then(@"I wait (.*)")]
        [Given(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
        }

        [When(@"verify through ""(.*)"" on ""(.*)""")]
        [Then(@"verify through ""(.*)"" on ""(.*)""")]
        public void ThenVerifyThroughOn(string message, string Keyword)
        {
            string locator_type = "id";
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                if (message == "ConsumerNoContextVal")
                {
                    message = context.GetConsumer_No();
                }
                if (message == "ToAccountNoContextVal")
                {
                    message = context.GetToAccount_No();
                }
                if (Keyword.Equals("MutualFund_DisPopup") && context.Get_MutualFundDisclaimerPopup() == "")
                {
                    return;
                }
                if (Keyword.Equals("Forget_Login_Id_SuccessMessage_desc"))
                {
                    string message_ui = context.Get_mobile_no();
                    string mobile = message_ui.Substring(7, 4);
                    if (message.Contains("xxxxxxx"))
                    {
                        message = message.Replace("xxxxxxx", "xxxxxxx" + mobile);
                    }

                }

                //if (message == "Signup_PassPolicy")
                //{
                //    for (int i = 1; i <= 3; i++)
                //    {
                //        string temp = "";
                //        if (i == 1)
                //        {
                //            temp = context.GetPassPolicy1();
                //        }
                //        else if (i == 2)
                //        {
                //            temp = context.GetPassPolicy2();
                //        }
                //        else if (i == 3)
                //        {
                //            temp = context.GetPassPolicy3();
                //        }
                //        string loc = keyword.Locator.Replace("[i]", "[" + (i) + "]");
                //        apmhelper.verification(temp, keyword.Locator,locator_type);
                //    }
                //    return;
                //}
                if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
                {
                    locator_type = "xpath";
                }
                apmhelper.verification(message, keyword.Locator, locator_type);
                if (Keyword.Contains("BillPayment_TranSuccess") || Keyword.Contains("SendMoney_TranSuccessMessage"))
                {
                    keyword = null;
                    locator_type = "xpath";
                    string tranid_keyword;
                    //if (context.Get_mutual_fund_check() != 0)
                    //{
                    //    tranid_keyword = "MutualFund_ReceiptID";
                    //}
                    //else
                    //{
                    tranid_keyword = "MutualFund_ReceiptID";
                    //}
                    keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                    string tran_id = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    context.SetTransaction_Id(tran_id);
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        [Then(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        public void ThenVerifyThroughDatabaseOnOnSchemaOn(string query, string schema, string Keyword)
        {
            string locator_type = "id";

            if (query.Contains("DC_TRANSACTION"))
            {
                if (Keyword.Contains("SendMoney_TranToBank") || Keyword.Contains("SendMoney_TranType") || Keyword.Contains("BillPayment_TranType"))
                {
                    query = query + context.GetTransaction_Id() + "')";
                }
                else
                {
                    query = query + context.GetTransaction_Id() + "'";
                }
            }
            try
            {
                string message = "";
                AppiumHelper apmhelper = new AppiumHelper();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (keyword.Locator.StartsWith("/") || keyword.Locator.StartsWith("("))
                {
                    locator_type = "xpath";
                }
                if (query != "")
                {
                    if (query.Contains("{ConsumerNo}"))
                    {
                        query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                    }
                    if (query.Contains("{mutual_fund_name}"))
                    {
                        query = query.Replace("{mutual_fund_name}", context.Get_mutual_fund_name());
                    }
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                    message = SourceDataTable.Rows[0][0].ToString();
                    if (Keyword.Equals("BillPayment_Transaction_Unpaid_Amount") || Keyword.Equals("BillPayment_Transaction_Unpaid_Amount_Bene"))
                    {
                        if (context.Get_Is_Partial_Payment_Allowed() != "0")
                        {
                            return;
                        }
                        //BillPayment_Transaction_Unpaid_Amount
                        string SURCHARGE_ATTRIBUTE = "";
                        DateTime DUE_DATE_FORMAT;
                        string company_code = SourceDataTable.Rows[0][1].ToString();
                        DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][2].ToString()));
                        if (DUE_DATE_FORMAT < DateTime.Today)
                        {
                            string temp_query = "Select SURCHARGE_ATTRIBUTE from BPS_SURCHARGE_AUTOMATION AD where AD.COMPANY_CODE like '%" + company_code + "%'";
                            DataAccessComponent.DataAccessLink dLink2 = new DataAccessComponent.DataAccessLink();
                            DataTable SourceDataTable2 = dLink2.GetDataTable(temp_query, schema);
                            SURCHARGE_ATTRIBUTE = SourceDataTable2.Rows[0][0].ToString();

                            string query2 = "Select " + SURCHARGE_ATTRIBUTE + " from LP_BILLS L WHERE L.CONSUMER_NO = '" + context.GetConsumer_No() + "' AND TO_CHAR(L.BILLING_MONTH,'MM/YYYY') = '" + context.GetBilling_Month() + "'";

                            dLink2 = null;
                            dLink2 = new DataAccessComponent.DataAccessLink();
                            SourceDataTable2 = null;
                            SourceDataTable2 = dLink2.GetDataTable(query2, "QAT_BPS");
                            string amount_after_dd = SourceDataTable2.Rows[0][0].ToString();
                            message = amount_after_dd;
                        }
                    }
                    if (Keyword == "SendMoney_TranBeneName")
                    {
                        if (message == "")
                        {
                            string temp = query.Replace("DT.BENEFICIARY_NAME", "DT.FT_TO_ACCOUNT_TITLE");
                            query = temp;
                        }
                    }
                    
                    if (Keyword.Equals("SendMoney_TranAmount") || Keyword.Equals("TermDeposit_TranAmount") || Keyword.Equals("BillPayment_TranAmount"))
                    {
                        message = Convert.ToDecimal(message).ToString("0.00");
                    }
                    if (Keyword.Equals("BillPayment_TranAmount") && context.Get_FCY_Tran_Check() == true)
                    {
                        message = message + " PKR";
                    }
                    //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                    //{
                    //    context.SetConsumer_No(message);
                    //}
                }
                apmhelper.verification(message, keyword.Locator, locator_type);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [Then(@"verify the result from ""(.*)"" on db ""(.*)""")]
        public void ThenVerifyTheResultFromOnDb(string query, string schema)
        {
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, schema);
            if (SourceDataTable.Rows.Count == 0)
            {
                throw new AssertFailedException(string.Format("there exists no record in database against query: {0}", query));
            };
        }
        [Given(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        [When(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        [Then(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        public void WhenISetValueInContextFromDataAs(string value, string attribute)
        {
            try
            {
                if (attribute == "customer_type")
                {
                    context.SetCustomerType(value);
                }
                if (attribute == "customer_cnic")
                {
                    context.SetCustomerCNIC(value);
                }
                if (attribute == "SignupCheck")
                {
                    context.Set_signup_check(Convert.ToBoolean(value));
                }
                if (attribute == "scroll_text")
                {
                    context.SetScrollText(value);
                }
                if (attribute == "TranTypeBene")
                {
                    context.SetTranTypeBene(value);
                }
                if (attribute == "ConsumerNo")
                {
                    context.SetConsumer_No(value);
                }
                if (attribute == "ToAccount")
                {
                    context.SetToAccount_No(value);
                }
                if (attribute == "bene_name")
                {
                    context.SetBeneName(value);
                }
                if (attribute == "Company_Code")
                {
                    context.SetCompany_Code(value);
                }
                if (attribute == "Account_Type")
                {
                    context.SetAccountType(value);
                }
                if (attribute == "username")
                {
                    context.SetUsername(value);
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable("SELECT CNIC FROM DC_CUSTOMER_INFO K WHERE K.CUSTOMER_NAME = '" + value.ToUpper() + "'", "DIGITAL_CHANNEL_SEC");
                    string message = SourceDataTable.Rows[0][0].ToString();
                    context.SetCustomerCNIC(message);
                }
                if (attribute == "Transaction_Type")
                {
                    context.SetTranType(value);
                }
                if (attribute == "TermDepositYears")
                {
                    context.Set_TermDepositYears(value);
                }
                if (attribute == "term_deposit_flag")
                {
                    context.Set_term_deposit_check(Convert.ToInt32(value));
                }
                if (attribute == "mutual_fund_flag")
                {
                    if (context.Get_mutual_fund_check() == 0)
                    {
                        context.Set_mutual_fund_check(Convert.ToInt32(value));
                    }
                }
                if (attribute == "AccountForTag")
                {
                    List<string> lst = new List<string>();
                    if (value.Contains(","))
                    {
                        string[] pieces = value.Split(',');
                        lst.AddRange(pieces);
                    }
                    else
                    {
                        lst.Add(value.ToString());
                    }
                    context.SetAccountForTag(lst);
                }
                if (attribute == "MutualFundName")
                {
                    context.Set_mutual_fund_name(value);
                }
                if (attribute == "Last_login_flag")
                {
                    context.SetLastLoginFlag(Convert.ToBoolean(value));
                }
                if (attribute == "BillPayment_Category")
                {
                    context.Set_BillPaymentCategory(value);
                }
                if(attribute == "KMobileNo")
                {
                    context.Set_mobile_no(value);
                }
                if(attribute == "Credit_Card_check")
                {
                    context.SetCredit_Card_Check(value);
                }
                if(attribute == "FCY_Tran_Check")
                {
                    context.Set_FCY_Tran_Check(Convert.ToBoolean(value.ToLower()));
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"I set value in context from database ""(.*)"" as ""(.*)"" on Schema ""(.*)""")]
        [When(@"I set value in context from database ""(.*)"" as ""(.*)"" on Schema ""(.*)""")]
        [Then(@"I set value in context from database ""(.*)"" as ""(.*)"" on Schema ""(.*)""")]
        public void WhenISetValueInContextFromDatabaseAsOnSchema(string query, string attribute, string schema)
        {
            if (String.IsNullOrEmpty(query))
            {
                return;
            }
            try
            {
                if (query.Contains("{username}"))
                {
                    query = query.Replace("{username}", context.GetUsername());
                }
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                if (query.Contains("{Company_Code}"))
                {
                    query = query.Replace("{Company_Code}", context.GetCompany_Code());
                }
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                if (query.Contains("{mutual_fund_name}"))
                {
                    query = query.Replace("{mutual_fund_name}", context.Get_mutual_fund_name());
                }
                if (query.Contains("TT.HOST_REFERENCE_NO"))
                {
                    query = query.Replace("{TRAN_ID}", context.GetTransaction_Id());
                }
                //if (query.Contains("T.GUID"))
                //{
                //    query = query.Replace("{HostReferenceNo}", context.Get_HostReferenceNo());
                //}
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                string db_value = SourceDataTable.Rows[0][0].ToString();
                if (attribute == "Is_PaidMarking_Req")
                {
                    context.Set_Is_Paid_Marking_Req(db_value);
                }
                if (attribute == "No_Of_Accounts")
                {
                    context.Set_no_of_accounts(Convert.ToInt32(db_value));
                }
                if (attribute == "Beneficiary_Count_Inter_Branch")
                {
                    context.Set_bene_count_inter_branch(Convert.ToInt32(db_value));
                }
                if (attribute == "customer_type")
                {
                    context.SetCustomerType(db_value);
                }
                if (attribute == "Beneficiary_Count_Inter_Bank")
                {
                    context.Set_bene_count_inter_bank(Convert.ToInt32(db_value));
                }
                if (attribute == "mutual_fund_disclaimer_popup")
                {
                    context.Set_MutualFundDisclaimerPopup(db_value);
                }
                if (attribute == "customer_profile_id")
                {
                    context.Set_cust_profile_id(db_value);
                }
                if (attribute == "customer_cnic")
                {
                    context.SetCustomerCNIC(db_value);
                }
                if (attribute == "schedule_configuration")
                {
                    context.Set_BP_schedule_config(db_value);
                }
                if (attribute == "GUID")
                {
                    context.Set_HostReferenceNo(db_value);
                }
                if (attribute == "IS_PARTIAL_PAYMENT_ALLOWED")
                {
                    context.Set_Is_Partial_Payment_Allowed(db_value);
                }
                if(attribute == "account_count")
                {
                    context.Set_account_count(Convert.ToInt32(db_value));
                }
                if (attribute == "mobile_no")
                {
                    context.Set_mobile_no(db_value);
                }

                
                //Clob 
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [Then(@"I am performing Limit verification operation")]
        public void ThenIAmPerformingLimitVerificationOperation()
        {
            try
            {
                Dictionary<string, Tuple<string, string, string>> customer_limit_detail_dict = new Dictionary<string, Tuple<string, string, string>>();

                string limit_type_id = "";
                string config_value = "";
                string daily_consume_limit = "";
                string daily_rem_limit = "";

                AppiumHelper apmhelper = new AppiumHelper();
                //selhelper.checkPageIsReady();

                //Element Keyword_main = ContextPage.GetInstance().GetElement(Keyword);
                List<string> limit_management_elements = context.Get_scroll_items_list();
                int limit_type_count = limit_management_elements.Count;

                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable("select enable_psd from dc_customer_info k where K.CUSTOMER_NAME='" + context.GetUsername() + "'", "DIGITAL_CHANNEL_SEC");
                string enable_psd = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("select CONFIG_VALUE from dc_customer_info_config k where K.CUSTOMER_INFO_ID= (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME='" + context.GetUsername() + "') and K.CONFIG_NAME = 'CUSTOMER_REGISTRATION_PROCESS'", "DIGITAL_CHANNEL_SEC");
                if (SourceDataTable.Rows.Count != 0)
                {
                    config_value = SourceDataTable.Rows[0][0].ToString();
                }
                dlink = null;
                SourceDataTable = null;


                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("select CONFIG_VALUE from dc_customer_info_config k where K.CUSTOMER_INFO_ID= (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME='" + context.GetUsername() + "') and K.CONFIG_NAME = 'CUSTOMER_NATURE'", "DIGITAL_CHANNEL_SEC");
                if (SourceDataTable.Rows.Count != 0)
                {
                    config_value = SourceDataTable.Rows[0][0].ToString();
                }
                dlink = null;
                SourceDataTable = null;

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("select CUSTOMER_TYPE from dc_customer_info k where K.CUSTOMER_NAME='" + context.GetUsername() + "'", "DIGITAL_CHANNEL_SEC");
                string customer_type = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("select LIMIT_TYPE_ID from dc_customer_info k where K.CUSTOMER_NAME='" + context.GetUsername() + "'", "DIGITAL_CHANNEL_SEC");
                string limit_type_id_DB = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                if (customer_type == "D")
                {
                    if (enable_psd == "0")
                    {
                        if (config_value == "NRP")
                        {
                            limit_type_id = "5";
                        }
                        else if (config_value == "P")
                        {
                            limit_type_id = "8";
                        }
                        else
                        {
                            limit_type_id = "1";
                        }
                    }
                    else if (enable_psd == "1")
                    {
                        limit_type_id = "4";
                    }
                }
                else if (customer_type == "A")
                {
                    if (enable_psd == "0" && limit_type_id_DB == "6")
                    {
                        limit_type_id = "6";
                    }
                    else if (enable_psd == "1")
                    {
                        limit_type_id = "7";
                    }
                }
                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select COUNT(*) from dc_Tran_Type_limit_group_rules a where A.LIMIT_TYPE_ID='" + limit_type_id + "' and A.IS_CLIENT_VIEW = 1 and A.IS_ACTIVE = '1' and trunc(A.EFFECTIVE_FROM_DATE) <= sysdate and A.EFFECTIVE_TO_DATE >= sysdate ", "DIGITAL_CHANNEL_SEC");
                int is_client_view = Convert.ToInt32(SourceDataTable.Rows[0][0].ToString());
                dlink = null;
                SourceDataTable = null;

                if (is_client_view != limit_type_count)
                {
                    throw new Exception(string.Format("The required limit count against Limit Type id :{0} in Database :{1} is not equal with UI :{2}", limit_type_id, is_client_view, limit_type_count));
                }

                for (int i = 0; i < limit_type_count; i++)
                {
                    Element keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Type");
                    string per_transaction = "";
                    string limit_text = limit_management_elements[i];
                    apmhelper.scroll_to_element_text(limit_text);
                    //apmhelper.scroll_down(0.95, 0.001);
                    apmhelper.scroll_to_element_text_with_parent_sibling(limit_text,"Per Transaction");
                    string temp = keyword.Locator.Replace("{Limit_Type}", limit_text);
                    string limit_type = apmhelper.ReturnKeywordValue(temp,"xpath");
                    dlink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = dlink.GetDataTable("Select A.MIN_AMOUNT, A.MAX_AMOUNT, LIMIT_RULES_ID from dc_Tran_Type_limit_group_rules a where A.CLIENT_DESCRIPTION='" + limit_type + "' and A.LIMIT_TYPE_ID='" + limit_type_id + "'", "DIGITAL_CHANNEL_SEC");
                    string min_amount_check = SourceDataTable.Rows[0][0].ToString();
                    if (min_amount_check == "0")
                    {
                        per_transaction = "No Limit";
                    }
                    else
                    {
                        per_transaction = SourceDataTable.Rows[0][1].ToString();
                    }
                    string limit_rules_id = SourceDataTable.Rows[0][2].ToString();
                    dlink = null;
                    SourceDataTable = null;

                    dlink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = dlink.GetDataTable("Select A.MIN_LIMIT, A.MAX_LIMIT from dc_custom_limit_rules a where A.LIMIT_RULE_ID = '" + limit_rules_id + "' and A.CUSTOMER_INFO_ID=(Select CUSTOMER_INFO_ID from dc_customer_info L where L.customer_name ='" + context.GetUsername() + "') and A.IS_ACTIVE = '1' and trunc(A.EFFECTIVE_FROM_DATE) <= sysdate and A.EFFECTIVE_TO_DATE > sysdate ", "DIGITAL_CHANNEL_SEC");
                    if (SourceDataTable.Rows.Count != 0)
                    {
                        min_amount_check = SourceDataTable.Rows[0][0].ToString();
                        if (min_amount_check == "0")
                        {
                            per_transaction = "No Limit";
                        }
                        else
                        {
                            per_transaction = SourceDataTable.Rows[0][1].ToString();
                        }
                    }
                    dlink = null;
                    SourceDataTable = null;

                    Element Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Per_Tran");
                    string temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_text);
                    //apmhelper.scroll_to_element_text_by_index(limit_text,i);
                    string per_tran_UI = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                    if (per_tran_UI != "No Limit")
                    {
                        per_tran_UI = per_tran_UI.Remove(per_tran_UI.Length - 3);
                        per_tran_UI = per_tran_UI.Replace(",", "");
                    }

                    if (per_transaction != per_tran_UI)
                    {
                        throw new Exception(String.Format("Per Transaction Amount in Database :{0} is not equal with per transaction amount on website :{1} for limit type :{2}", per_transaction, per_tran_UI, limit_type));
                    }


                    dlink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_AMOUNT from dc_Tran_Type_limit_group_rules a where a.CLIENT_DESCRIPTION = '" + limit_type + "' and a.limit_type_id = '" + limit_type_id + "'", "DIGITAL_CHANNEL_SEC");
                    string old_limit = SourceDataTable.Rows[0][0].ToString();
                    context.SetOldLimit(old_limit);
                    dlink = null;
                    SourceDataTable = null;


                    dlink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_LIMIT from dc_custom_limit_rules a where A.LIMIT_RULE_ID = '" + limit_rules_id + "' and A.CUSTOMER_INFO_ID=(Select CUSTOMER_INFO_ID from dc_customer_info L where L.customer_name ='" + context.GetUsername() + "') and A.IS_ACTIVE = '1' and trunc(A.EFFECTIVE_FROM_DATE) <= sysdate and A.EFFECTIVE_TO_DATE > sysdate ", "DIGITAL_CHANNEL_SEC");
                    if (SourceDataTable.Rows.Count != 0)
                    {
                        old_limit = SourceDataTable.Rows[0][0].ToString();
                        context.SetOldLimit(old_limit);
                    }
                    dlink = null;
                    SourceDataTable = null;
                    Keyword = null;
                    temp_keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_daily_limit");
                    temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_text);
                    //apmhelper.scroll_to_element_text_by_index(limit_text, i);
                    string daily_limit = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                    daily_limit = daily_limit.Remove(daily_limit.Length - 3);
                    daily_limit = daily_limit.Replace(",", "");
                    if (old_limit != daily_limit)
                    {
                        throw new Exception(String.Format("Daily Debit Amount in Database :{0} is not equal with daily debit amount on website :{1} for limit type :{2}", old_limit, daily_limit, limit_type));
                    }




                    dlink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_AMOUNT from DC_LIMIT_CONSUMED I WHERE I.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info L where L.customer_name = '" + context.GetUsername() + "') and TRUNC(I.LAST_TXN_DATE) = Trunc(SYSDATE) and I.LIMIT_RULES_ID = '" + limit_rules_id + "'", "DIGITAL_CHANNEL_SEC");
                    if (SourceDataTable.Rows.Count != 0)
                    {
                        daily_consume_limit = SourceDataTable.Rows[0][0].ToString();
                    }
                    else
                    {
                        daily_consume_limit = "0";
                    }
                    dlink = null;
                    SourceDataTable = null;

                    Keyword = null;
                    temp_keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Availed");
                    temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_text);
                    //apmhelper.scroll_to_element_text_by_index(limit_text, i);
                    string daily_consume_limit_ui = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                    daily_consume_limit_ui = daily_consume_limit_ui.Remove(daily_consume_limit_ui.Length - 3);
                    daily_consume_limit_ui = daily_consume_limit_ui.Replace(",", "");
                    if (daily_consume_limit != daily_consume_limit_ui)
                    {
                        throw new Exception(String.Format("Database Availed limit :{0} is not equal with Availed limit on Website :{1} for limit type :{2}", daily_consume_limit, daily_consume_limit_ui, limit_type));
                    }



                    temp_keyword = null;
                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Remaining");
                    temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_type);
                    daily_rem_limit = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                    daily_rem_limit = daily_rem_limit.Remove(daily_rem_limit.Length - 3);
                    daily_rem_limit = daily_rem_limit.Replace(",", "");

                    if (Convert.ToInt32(daily_rem_limit) != (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)))
                    {
                        throw new Exception(String.Format("Website Remaining limit :{0} is not equal with Calculated Remaining amount from Availed Limit :{1} for limit type :{2}", daily_rem_limit, (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)), limit_type));
                    }

                    customer_limit_detail_dict.Add(limit_type, Tuple.Create(old_limit, limit_type_id, limit_rules_id));

                    Keyword = null;
                    temp_keyword = null;
                    limit_rules_id = temp = daily_limit = old_limit = per_tran_UI = per_transaction = min_amount_check = limit_type = temp_keyword = String.Empty;

                    context.SetCustLimitDetail(customer_limit_detail_dict);

                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new Exception(exception.Message);
            }
        }
        [Then(@"I am performing limit reduction operation on ""(.*)"" of slider ""(.*)"" of ""(.*)"" with new limit as ""(.*)""")]
        public void ThenIAmPerformingLimitReductionOperationOnOfSliderOfWithNewLimitAs(string keyword, string slider_keyword, string limit_type, int new_limit)
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                var cust_limit_dict = context.GetCustLimitDetail();
                string min_value = "";
                string max_value = "";
                string old_limit = "";
                string limit_type_id = "";
                string limit_rules_id = "";
                string daily_consume_limit = "";
                string daily_rem_limit = "";
                string temp_keyword = "";
                int counter = 0;

                foreach (var item in cust_limit_dict)
                {
                    if (item.Key == limit_type)
                    {
                        old_limit = item.Value.Item1;
                        limit_type_id = item.Value.Item2;
                        limit_rules_id = item.Value.Item3;
                        break;
                    }
                    counter++;
                }

                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable("Select A.CLIENT_LIMIT_INTERVAL from dc_Tran_Type_limit_group_rules a where a.CLIENT_DESCRIPTION = '" + limit_type + "' and a.limit_type_id = '" + limit_type_id + "'", "DIGITAL_CHANNEL_SEC");
                string step = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select A.IS_EDITABLE from dc_Tran_Type_limit_group_rules a where A.CLIENT_DESCRIPTION='" + limit_type + "' and A.LIMIT_TYPE_ID='" + limit_type_id + "' and A.IS_ACTIVE = '1' and trunc(A.EFFECTIVE_FROM_DATE) <= sysdate and A.EFFECTIVE_TO_DATE > sysdate ", "DIGITAL_CHANNEL_SEC");
                string is_editable = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                if (is_editable != "1")
                {
                    throw new Exception(string.Format("The required Limit Type :{0} is not allowed to edit", limit_type));
                }

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_AMOUNT from DC_LIMIT_CONSUMED I WHERE I.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info L where L.customer_name = '" + context.GetUsername() + "') and TRUNC(I.LAST_TXN_DATE) = Trunc(SYSDATE) and I.LIMIT_RULES_ID = '" + limit_rules_id + "'", "DIGITAL_CHANNEL_SEC");
                if (SourceDataTable.Rows.Count != 0)
                {
                    daily_consume_limit = SourceDataTable.Rows[0][0].ToString();
                }
                else
                {
                    daily_consume_limit = "0";
                }
                dlink = null;
                SourceDataTable = null;

                Element Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Availed");
                temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_type);
                apmhelper.scroll_to_element_text(limit_type);
                apmhelper.scroll_to_element_text_with_parent_sibling(limit_type, "Per Transaction");
                //apmhelper.scroll_down(0.65, 0.10);
                //apmhelper.scroll_to_element_text_by_index("Per Transaction - ",counter);
                string daily_consume_limit_ui = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                daily_consume_limit_ui = daily_consume_limit_ui.Remove(daily_consume_limit_ui.Length - 3);
                daily_consume_limit_ui = daily_consume_limit_ui.Replace(",", "");
                if (daily_consume_limit != daily_consume_limit_ui)
                {
                    throw new Exception(String.Format("Database Availed limit :{0} is not equal with Availed limit on Website :{1} for limit type :{2}", daily_consume_limit, daily_consume_limit_ui, limit_type));
                }
                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Remaining");
                temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_type);
                //apmhelper.ScrollToElement(temp_keyword,"xpath");
                daily_rem_limit = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                daily_rem_limit = daily_rem_limit.Remove(daily_rem_limit.Length - 3);
                daily_rem_limit = daily_rem_limit.Replace(",", "");

                if (Convert.ToInt32(daily_rem_limit) != (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)))
                {
                    throw new Exception(String.Format("UI Remaining limit :{0} is not equal with Calculated Remaining amount from Availed Limit :{1} for limit type :{2}", daily_rem_limit, (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)), limit_type));
                }

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement(keyword);
                Element Keyword2 = ContextPage.GetInstance().GetElement("MyAccount_Limit_Slider");
                string locator = Keyword.Locator.Replace("{Limit_Type}", limit_type);
                //string slider_locator = Keyword2.Locator.Replace("{Limit_Type}", limit_type);
                apmhelper.links(locator, "xpath");
                apmhelper.PressKey("Tab");
                apmhelper.PressKey("Tab");
                Keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Max");
                string max_value_ui = apmhelper.ReturnKeywordValue(Keyword.Locator,"id");
                max_value_ui = max_value_ui.Remove(max_value_ui.Length - 3);
                max_value_ui = max_value_ui.Replace(",", "");

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_NewLimit");
                
                //string slider_limit_ui = Keyword.Locator.Replace("{x}", limit_type);

                //int temp_count = Convert.ToInt32(max_value_ui) - Convert.ToInt32(old_limit);
                //temp_count = temp_count / Convert.ToInt32(step);

                //int result = Convert.ToInt32(old_limit) - new_limit;
                //int step_limit = result / Convert.ToInt32(step);

                //int step_limit_count = step_limit + temp_count;

                apmhelper.RangeSlider(Keyword2.Locator, "id",new_limit);

                keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Slider_Next");
                apmhelper.Button(Keyword.Locator, "id");

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select A.MIN_EDITABLE_AMOUNT from dc_Tran_Type_limit_group_rules a where A.CLIENT_DESCRIPTION='" + limit_type + "' and A.LIMIT_TYPE_ID='" + limit_type_id + "'", "DIGITAL_CHANNEL_SEC");
                min_value = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                Keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Min");
                apmhelper.scroll_to_element_by_id(Keyword.Locator);
                string min_value_ui = apmhelper.ReturnKeywordValue(Keyword.Locator,"id");
                min_value_ui = min_value_ui.Remove(min_value_ui.Length - 3);
                min_value_ui = min_value_ui.Replace(",", "");
                if (min_value != min_value_ui)
                {
                    throw new Exception(String.Format("Minimum Editable Aount in Database :{0} is not equal with Minimum Editable amount on website :{1} for limit type :{2}", min_value, min_value_ui, limit_type));
                }

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select A.MAX_EDITABLE_AMOUNT from dc_Tran_Type_limit_group_rules a where A.CLIENT_DESCRIPTION='" + limit_type + "' and A.LIMIT_TYPE_ID='" + limit_type_id + "'", "DIGITAL_CHANNEL_SEC");
                max_value = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                if (max_value != max_value_ui)
                {
                    throw new Exception(String.Format("Maximum Editable Aount in Database :{0} is not equal with Maximum Editable amount on website :{1} for limit type :{2}", max_value, max_value_ui, limit_type));
                }

                keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_NewLimit");
                //temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                apmhelper.scroll_to_element_by_id(Keyword.Locator);
                string new_edit_limit = apmhelper.ReturnKeywordValue(Keyword.Locator,"id");
                new_edit_limit = new_edit_limit.Remove(new_edit_limit.Length - 3);
                new_edit_limit = new_edit_limit.Replace(",", "");
                if (new_edit_limit != Convert.ToString(new_limit))
                {
                    throw new Exception(String.Format("New Given Limit Amount :{0} is not equal with New Limit amount on website :{1} for limit type :{2}", new_limit, new_edit_limit, limit_type));
                }

                keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Slider_Next_Save");
                apmhelper.Button(Keyword.Locator,"id");

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Slider_SucessMessage");
                string success_message = apmhelper.ReturnKeywordValue(Keyword.Locator,"id");

                dlink = new DataAccessComponent.DataAccessLink();
                if(Convert.ToInt32(old_limit) < new_limit)
                {
                    SourceDataTable = dlink.GetDataTable("Select C.RESULT_CODE_DESCRIPTION from dc_Response_code c where C.ERROR_CODE = 'IVR_CALL_INITIATION_SUCCESS'", "DIGITAL_CHANNEL_SEC");
                }
                
                else if (Convert.ToInt32(old_limit) >= new_limit)
                {
                    SourceDataTable = dlink.GetDataTable(" Select C.RESULT_CODE_DESCRIPTION from dc_Response_code c where C.ERROR_CODE = 'LIMIT_CHANGE_SUCCESSFULLY'", "DIGITAL_CHANNEL_SEC");
                }

                string edit_success_msg = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                if (edit_success_msg != success_message)
                {
                    throw new Exception(String.Format("Success Message in Database :{0} is not equal with Success Message on website :{1} for limit type :{2}", edit_success_msg, success_message, limit_type));
                }

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Slider_CloseBtn");
                apmhelper.Button(Keyword.Locator,"id");

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("Dashboard_Sidebar");
                apmhelper.Button(Keyword.Locator, "xpath");

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("Dashboard_Sidebar_MyAccount");
                apmhelper.Button(Keyword.Locator, "xpath");

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Management_Icon");
                apmhelper.Button(Keyword.Locator, "xpath");

                //keyword = null;
                //Keyword = ContextPage.GetInstance().GetElement("MyAccount_LimitMngOption");
                //selhelper.links(Keyword.Locator);

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_LIMIT from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='1'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
                string new_edited_limit = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Remaining");
                temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_type);
                apmhelper.scroll_to_element_text(limit_type);
                apmhelper.scroll_to_element_text_with_parent_sibling(limit_type, "Per Transaction");
                //apmhelper.scroll_down(0.65, 0.10);
                //apmhelper.scroll_to_element_text_by_index("Per Transaction - ", counter);
                string daily_rem_after_edit = apmhelper.ReturnKeywordValue(temp_keyword, "xpath");
                daily_rem_after_edit = daily_rem_after_edit.Remove(daily_rem_after_edit.Length - 3);
                daily_rem_after_edit = daily_rem_after_edit.Replace(",", "");

                if (Convert.ToInt32(daily_rem_after_edit) != (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)) - (Convert.ToInt32(old_limit) - new_limit))
                {
                    throw new Exception(String.Format("UI Remaining limit :{0} is not equal with Calculated Remaining amount after edit :{1} for limit type :{2}", daily_rem_limit, ((Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)) - (new_limit)), limit_type));
                }

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_daily_limit");
                temp_keyword = Keyword.Locator.Replace("{Limit_Type}", limit_type);
                //selhelper.ScrollToElement(temp_keyword);
                string daily_new_limit = apmhelper.ReturnKeywordValue(temp_keyword,"xpath");
                daily_new_limit = daily_new_limit.Remove(daily_new_limit.Length - 3);
                daily_new_limit = daily_new_limit.Replace(",", "");
                if (new_edited_limit != daily_new_limit)
                {
                    throw new Exception(String.Format("New Daily Limit in Database :{0} is not equal with New Daily Debit Limit on website :{1} for limit type :{2}", new_edited_limit, daily_new_limit, limit_type));
                }
                if (new_edited_limit != Convert.ToString(new_limit))
                {
                    throw new Exception(String.Format("New Daily Limit from Excel :{0} is not equal with New Daily Debit Limit on website :{1} for limit type :{2}", new_edited_limit, new_limit, limit_type));
                }
                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select UPDATED_ON from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='1'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
                string updated_on = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;
                temp_keyword = null;

                DateTime lastupdate = Convert.ToDateTime(updated_on);
                updated_on = lastupdate.ToString("MM/dd/yyyy");
                string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                if (updated_on != today_date)
                {
                    throw new Exception(String.Format("Updated on Date in Database :{0} does not match with today's date :{1} for limit type :{2}", updated_on, today_date, limit_type));
                }

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select EFFECTIVE_FROM_DATE from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='1'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
                string effective_from = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                lastupdate = Convert.ToDateTime(effective_from);
                effective_from = lastupdate.ToString("MM/dd/yyyy");
                if (updated_on != today_date)
                {
                    throw new Exception(String.Format("Effective From Date in Database :{0} does not match with today's date :{1} for limit type :{2}", effective_from, today_date, limit_type));
                }

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select EFFECTIVE_TO_DATE from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='1'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
                string effective_to = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                lastupdate = Convert.ToDateTime(effective_to);
                effective_to = lastupdate.ToString("MM/dd/yyyy");
                string todayDate = Convert.ToString(DateTime.Today.AddYears(30).ToString("MM/dd/yyyy"));
                if (effective_to != todayDate)
                {
                    throw new Exception(String.Format("Effective to Date in Database :{0} does not match with the 10 years later date :{1} for limit type :{2}", effective_from, todayDate, limit_type));
                }

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select UPDATED_ON, CREATED_ON, STATUS, IVR_ATTRIBUTE2, IVR_ATTRIBUTE3 from dc_transaction q where Q.CNIC= (Select CNIC from dc_customer_info a where A.CUSTOMER_NAME ='" + context.GetUsername() + "') and Q.LEAD_FIELD1='" + limit_type + "' and Q.CHANNEL_ID='1' order by Q.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
                string dc_updated_on = SourceDataTable.Rows[0][0].ToString();
                string dc_created_on = SourceDataTable.Rows[0][1].ToString();
                string status = SourceDataTable.Rows[0][2].ToString();
                string dc_ivr1 = SourceDataTable.Rows[0][3].ToString();
                string dc_ivr2 = SourceDataTable.Rows[0][4].ToString();
                dlink = null;
                SourceDataTable = null;

                lastupdate = Convert.ToDateTime(dc_updated_on);
                dc_updated_on = lastupdate.ToString("MM/dd/yyyy");
                if (dc_updated_on != today_date)
                {
                    throw new Exception(String.Format("Updated on Date in DC_Transaction table in Database :{0} does not match with today's date :{1} for limit type :{2}", dc_updated_on, today_date, limit_type));
                }

                lastupdate = Convert.ToDateTime(dc_created_on);
                dc_created_on = lastupdate.ToString("MM/dd/yyyy");
                if (dc_created_on != today_date)
                {
                    throw new Exception(String.Format("Created on Date in DC_Transaction table in Database :{0} does not match with today's date :{1} for limit type :{2}", dc_created_on, today_date, limit_type));
                }

                if (status != "Success")
                {
                    throw new Exception(String.Format("Status in DC_Transaction table in Database :{0} is not Success for limit type :{1}", dc_created_on, limit_type));
                }

                if (dc_ivr1 != old_limit + ".0")
                {
                    throw new Exception(String.Format("Old Limit in DC_Transaction table in Database :{0} does not match with the on website :{1} for limit type :{2}", dc_ivr1, old_limit, limit_type));
                }

                if (dc_ivr2 != Convert.ToString(new_limit))
                {
                    throw new Exception(String.Format("New Limit on in DC_Transaction table in Database :{0} does not match with the on website :{1} for limit type :{2}", dc_ivr2, Convert.ToString(new_limit), limit_type));
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [When(@"I verify user Mutual Fund status on schema ""(.*)"" as ""(.*)""")]
        public void WhenIVerifyUserMutualFundStatusOnSchemaAs(string schema, string counter)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            int size = 0;
            double amount_ui = 0;
            double amount_db = 0;
            string fund_name_loc = "";
            string folio_loc = "";
            string balance_loc = "";
            string unit_loc = "";
            string nav_loc = "";
            string nav_date_loc = "";
            string locator_type = "id";
            string temp_loc = "";
            try
            {
                if (context.Get_cust_profile_id() == null)
                {
                    Element Keyword = ContextPage.GetInstance().GetElement("MutualFund_DisPopup");
                    string message = "No mutual funds are found against your CNIC";
                    Assert.AreEqual(message, apmhelper.ReturnKeywordValue(Keyword.Locator, locator_type));

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_PopupBtn");
                    apmhelper.Button(Keyword.Locator, locator_type);
                }
                else
                {
                    Element Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_Tab");
                    apmhelper.links(Keyword.Locator, locator_type);

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_Table");
                    size = apmhelper.SizeCountElements(Keyword.Locator, locator_type) - 1;
                    if (counter.ToLower() != "all")
                    {
                        size = Convert.ToInt32(counter);
                    }
                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_FundName");
                    fund_name_loc = Keyword.Locator;

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_FolioNumber");
                    folio_loc = Keyword.Locator;

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_Balance");
                    balance_loc = Keyword.Locator;

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_Units");
                    unit_loc = Keyword.Locator;

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_Nav");
                    nav_loc = Keyword.Locator;

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MutualFund_Statement_NavDate");
                    nav_date_loc = Keyword.Locator;


                    string[,] array_mutual_fund_ui;
                    string[,] array_mutual_fund_db;
                    string new_query = "SELECT PP.NAME_OF_FUND, L.FOLIO_NO, CP.BALANCE, CP.UNITS, PP.NAV_PRICE, PP.NAV_DATE FROM AMC_CUSTOMER_PROFILE L INNER JOIN AMC_CUSTOMER_PORTFOLIO CP ON L.CUSTOMER_PROFILE_ID = CP.CUSTOMER_PROFILE_ID INNER JOIN AMC_PRODUCT_PROFILE PP ON CP.PRODUCT_ID = PP.PRODUCT_ID WHERE L.CNIC = '" + context.GetCustomerCNIC() + "' AND CP.AMOUNT <> 0 order by PP.NAME_OF_FUND";
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(new_query, schema);
                    array_mutual_fund_ui = new string[size, 6];
                    array_mutual_fund_db = new string[SourceDataTable.Rows.Count, 6];
                    int zero_count = 0;
                    for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                    {
                        string fund_name_db = SourceDataTable.Rows[i][0].ToString();
                        string folio_no_db = SourceDataTable.Rows[i][1].ToString();
                        double balance_d = Convert.ToDouble(SourceDataTable.Rows[i][2].ToString());
                        balance_d = Math.Round(balance_d, 2);
                        amount_db += balance_d;
                        //string balance_db = string.Format("{0:0.00}", balance_d);
                        string balance_db = balance_d.ToString();
                        string units_db = SourceDataTable.Rows[i][3].ToString();
                        string nav_db = SourceDataTable.Rows[i][4].ToString();
                        nav_db = Convert.ToString(Math.Round(Convert.ToDecimal(nav_db), 3));

                        string nav_date_db = SourceDataTable.Rows[0][5].ToString();
                        DateTime temp = Convert.ToDateTime(nav_date_db);
                        nav_date_db = temp.ToString("dd/MM/yyyy");

                        array_mutual_fund_db[i, 0] = fund_name_db;
                        array_mutual_fund_db[i, 1] = folio_no_db;
                        array_mutual_fund_db[i, 2] = balance_db;
                        array_mutual_fund_db[i, 3] = units_db;
                        array_mutual_fund_db[i, 4] = nav_db;
                        array_mutual_fund_db[i, 5] = nav_date_db;
                        fund_name_db = folio_no_db = balance_db = units_db = nav_db = nav_date_db = string.Empty;
                    }

                    Element Keyword_rownum = ContextPage.GetInstance().GetElement("MutualFund_Statement_TabRow");

                    for (int i = 1; i <= size; i++)
                    {

                        temp_loc = "";
                        temp_loc = Keyword_rownum.Locator.Replace("rownum", i.ToString());
                        if (i == 1)
                        {
                            temp_loc = temp_loc + "[2]";
                        }
                        apmhelper.links(temp_loc, "xpath");
                        string fund_name = apmhelper.ReturnKeywordValue(fund_name_loc, locator_type).Trim();
                        string folio_no = apmhelper.ReturnKeywordValue(folio_loc, locator_type).Trim();
                        string balance = apmhelper.ReturnKeywordValue(balance_loc, locator_type);
                        balance = balance.Replace("PKR", "").Trim();
                        balance = balance.Replace(",", "");
                        amount_ui += Convert.ToDouble(balance);
                        string unit = apmhelper.ReturnKeywordValue(unit_loc, locator_type);
                        unit = unit.Replace(",", "").Trim();
                        string nav = apmhelper.ReturnKeywordValue(nav_loc, locator_type).Trim();
                        string nav_date = apmhelper.ReturnKeywordValue(nav_date_loc, locator_type).Trim();
                        nav_date = nav_date.Replace("-", "/");
                        nav = nav.Replace("PKR", "").Trim();
                        array_mutual_fund_ui[i - 1, 0] = fund_name;
                        array_mutual_fund_ui[i - 1, 1] = folio_no;
                        array_mutual_fund_ui[i - 1, 2] = balance;
                        array_mutual_fund_ui[i - 1, 3] = unit;
                        array_mutual_fund_ui[i - 1, 4] = nav;
                        array_mutual_fund_ui[i - 1, 5] = nav_date;

                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("MutualFund_PopupBtn");
                        apmhelper.links(Keyword.Locator, locator_type);
                    }

                    if (array_mutual_fund_db.GetLength(0) != array_mutual_fund_ui.GetLength(0))
                    {
                        throw new Exception(string.Format("The size of customer mutual fund records are not equal with database :{0} and website :{1}", array_mutual_fund_db.GetLength(0), array_mutual_fund_ui.GetLength(0)));
                    }

                    for (int i = 0; i < array_mutual_fund_db.GetLength(0); i++)
                    {
                        for (int j = 0; j < array_mutual_fund_db.GetLength(0); j++)
                        {
                            //if (array_mutual_fund_db[i,2] == "0.00")
                            //{
                            //    break;
                            //}

                            if (array_mutual_fund_db[i, j].CompareTo(array_mutual_fund_ui[i, j]) != 0)
                            {
                                throw new Exception(string.Format("The value on UI :{0} is not equal with value on database :{1}", array_mutual_fund_db[i, j], array_mutual_fund_ui[i, j]));
                            }
                        }
                    }
                    if (amount_db != amount_ui)
                    {
                        throw new Exception(string.Format("Total Mutual fund amount in database :{0} is not equal with toal Mutual fund amount on UI :{1}", amount_db, amount_ui));
                    }
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [When(@"I save Account Balances")]
        public void WhenISaveAccountBalances()
        {
            AppiumHelper apmhelper = new AppiumHelper();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            bool loop_end_check = true;
            string locator_type = "id";
            bool account_check = false;
            Dictionary<string, string> dict_cc = new Dictionary<string, string>();
            bool loop_end_check_cc = true;
            string locator_type_cc = "id";
            bool account_check_cc = false;
            //int counter = 0;
            while (loop_end_check == true)
            {
                try
                {
                    Element keyword = ContextPage.GetInstance().GetElement("Accounts_Home_No");
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string account_no = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    if (account_no == "Term Deposit")
                    {
                        context.Set_term_deposit_check(2);
                    }
                    if (account_no == "HBL Mutual Funds")
                    {
                        context.Set_mutual_fund_check(2);
                    }
                    keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Balance");
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string balance = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    dict.Add(account_no, balance);
                    //counter++;
                    keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next");
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    account_check = true;
                    apmhelper.links_visibility(keyword.Locator, locator_type);
                    apmhelper.links(keyword.Locator, locator_type);
                    //account_check = true;
                }
                catch (Exception exception)
                {
                    if (account_check == false)
                    {
                        AppiumHelper.TakeScreenshot();
                        throw new AssertFailedException(exception.Message);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            context.Set_acc_balances(dict);
            if(context.GetCredit_Card_Check() != "" || context.GetCredit_Card_Check() != null)
            {
                while (loop_end_check_cc == true)
                {
                    try
                    {
                        Element keyword = ContextPage.GetInstance().GetElement("Accounts_Home_No_cc");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        string account_no = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type_cc);
                        keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Balance_cc");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        string balance = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type_cc);
                        dict_cc.Add(account_no, balance);
                        //counter++;
                        keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next_cc");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        account_check_cc = true;
                        apmhelper.links_visibility(keyword.Locator, locator_type_cc);
                        apmhelper.links(keyword.Locator, locator_type_cc);
                        //account_check = true;
                    }
                    catch (Exception exception)
                    {
                        if (account_check_cc == false)
                        {
                            AppiumHelper.TakeScreenshot();
                            throw new AssertFailedException(exception.Message);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                context.Set_creditcard_balances(dict_cc);
            }
        }
        [When(@"I save Transaction Info for MultiPayment")]
        public void WhenISaveTransactionInfoForMultiPayment()
        {
            try
            {
                string locator_type = "id";
                AppiumHelper apmhelper = new AppiumHelper();
                Element keyword = null;
                Dictionary<string, string> tran_dict = new Dictionary<string, string>();
                if (context.GetTranType() == "SendMoney")
                {
                    keyword = ContextPage.GetInstance().GetElement("SendMoney_TranFromAcc");
                }
                else if (context.GetTranType() == "BillPayment")
                {
                    keyword = ContextPage.GetInstance().GetElement("BillPayment_TranFromAcc");
                }
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                string tran_account = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                context.SetTran_Account(tran_account);
                keyword = ContextPage.GetInstance().GetElement("SendMoney_TranAmount");
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                string tran_balance = context.Get_multi_payment_amount().ToString();
                tran_dict = context.Get_acc_balances();

                foreach (var item in tran_dict)
                {
                    if (tran_account == item.Key)
                    {
                        decimal tran_balancee = Convert.ToDecimal(item.Value) - Convert.ToDecimal(tran_balance);
                        context.SetTran_Balance(tran_balancee);
                        break;
                    }

                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [When(@"I save Transaction Info")]
        [Then(@"I save Transaction Info")]
        public void WhenISaveTransactionInfo()
        {
            try
            {
                if (context.GetCredit_Card_Check() != "" || context.GetCredit_Card_Check() != null)
                {
                    string locator_type = "id";
                    AppiumHelper apmhelper = new AppiumHelper();
                    Element keyword = null;
                    Dictionary<string, string> tran_dict = new Dictionary<string, string>();
                    bool account_bal_checker = false;
                    if (context.GetTranType() == "BillPayment")
                    {
                        keyword = ContextPage.GetInstance().GetElement("BillPayment_TranFromAcc");
                    }
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string tran_account = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    context.SetTran_Account(tran_account);
                    keyword = ContextPage.GetInstance().GetElement("SendMoney_TranAmount");
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string tran_balance = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    tran_dict = context.Get_creditcard_balances();

                    foreach (var item in tran_dict)
                    {
                        if (tran_account == item.Key)
                        {
                            decimal tran_balancee = Convert.ToDecimal(item.Value) + Convert.ToDecimal(tran_balance);
                            context.SetTran_Balance(tran_balancee);
                            account_bal_checker = true;
                            continue;
                        }
                        if (account_bal_checker == true)
                        {
                            break;
                        }

                    }

                }
                else
                {
                    string locator_type = "id";
                    AppiumHelper apmhelper = new AppiumHelper();
                    Element keyword = null;
                    Dictionary<string, string> tran_dict = new Dictionary<string, string>();
                    bool account_bal_checker = false;
                    bool term_deposit_checker = false;
                    bool mutual_fund_checker = false;
                    if (context.GetTranType() == "SendMoney")
                    {
                        keyword = ContextPage.GetInstance().GetElement("SendMoney_TranFromAcc");
                    }
                    else if (context.GetTranType() == "BillPayment")
                    {
                        keyword = ContextPage.GetInstance().GetElement("BillPayment_TranFromAcc");
                    }
                    else
                    {
                        keyword = ContextPage.GetInstance().GetElement("SendMoney_TranFromAcc");
                    }
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string tran_account = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    context.SetTran_Account(tran_account);
                    keyword = ContextPage.GetInstance().GetElement("SendMoney_TranAmount");
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string tran_balance = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    tran_dict = context.Get_acc_balances();
                    if (context.Get_term_deposit_check() == 1)
                    {
                        tran_dict.Add("Term Deposit", "0");
                    }

                    foreach (var item in tran_dict)
                    {
                        if (item.Key == "Term Deposit")
                        {
                            if ((context.Get_term_deposit_check() == 1 || context.Get_term_deposit_check() == 2) && item.Key == "Term Deposit")
                            {
                                decimal term_deposit_bal = Convert.ToDecimal(item.Value) + Convert.ToDecimal(tran_balance);
                                context.Set_term_deposit_balance(term_deposit_bal);
                                term_deposit_checker = true;
                                continue;
                            }
                            //if (context.Get_term_deposit_check() == 2 && item.Key == "Term Deposit")
                            //{
                            //    decimal term_deposit_bal = Convert.ToDecimal(item.Value) + Convert.ToDecimal(tran_balance);
                            //    context.Set_term_deposit_balance(term_deposit_bal);
                            //    term_deposit_checker = true;
                            //    continue;
                            //}
                            //else if (tran_account == item.Key)
                            //{
                            //    decimal tran_balancee = Convert.ToDecimal(item.Value) - Convert.ToDecimal(tran_balance);
                            //    context.SetTran_Balance(tran_balancee);
                            //    account_bal_checker = true;
                            //    continue;
                            //}
                        }
                        //if (item.Key == "HBL Mutual Funds")
                        //{
                        //    if (context.Get_mutual_fund_check() == 1 && item.Key == "HBL Mutual Funds")
                        //    {
                        //        decimal mutual_fund_bal = Convert.ToDecimal(item.Value) + Convert.ToDecimal(tran_balance);
                        //        context.Set_mutual_fund_balance(mutual_fund_bal);
                        //        mutual_fund_checker = true;
                        //        continue;
                        //    }
                        //    //else if(context.Get_term_deposit_check() == 2 && item.Key == "HBL Mutual Funds")
                        //    //{

                        //    //}
                        //    //else if (tran_account == item.Key)
                        //    //{
                        //    //    decimal tran_balancee = Convert.ToDecimal(item.Value) - Convert.ToDecimal(tran_balance);
                        //    //    context.SetTran_Balance(tran_balancee);
                        //    //    account_bal_checker = true;
                        //    //    continue;
                        //    //}
                        //}
                        if (tran_account == item.Key)
                        {
                            decimal tran_balancee = Convert.ToDecimal(item.Value) - Convert.ToDecimal(tran_balance);
                            context.SetTran_Balance(tran_balancee);
                            account_bal_checker = true;
                            continue;
                        }
                        if (term_deposit_checker == true && account_bal_checker == true)
                        {
                            break;
                        }
                        //if (mutual_fund_checker == true && account_bal_checker == true)
                        //{
                        //    break;
                        //}

                    }
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }

        }
        [Then(@"I verify Account Balance")]
        public void ThenIVerifyAccountBalance()
        {
            string locator_type = "id";
            bool loop_end_check = true;
            decimal old_account_bal = 0;
            bool account_bal_checker = false;
            bool term_deposit_checker = false;
            //bool mutual_fund_checker = false;
            int account_count = context.Get_acc_balances().Count;
            int counter = 0;
            while (loop_end_check == true)
            {
                try
                {
                    if (context.GetCredit_Card_Check() != "" || context.GetCredit_Card_Check() != null)
                    {
                        AppiumHelper apmhelper = new AppiumHelper();
                        Element keyword = ContextPage.GetInstance().GetElement("Accounts_Home_No_cc");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        string account_no = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                        keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Balance_cc");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        string balance = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                        string tAccountNo = context.GeTran_Account();
                        if (tAccountNo == account_no)
                        {
                            old_account_bal = context.GetTran_Balance();
                            if (Convert.ToDecimal(balance) != old_account_bal)
                            {
                                throw new AssertFailedException(string.Format("The old account balance {0} is not equal to new account balance {1} after successfull transaction", old_account_bal, balance));
                            }
                            account_bal_checker = true;
                            keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next_cc");
                            try
                            {
                                apmhelper.links_visibility(keyword.Locator, locator_type);
                                apmhelper.links(keyword.Locator, locator_type);
                                counter++;
                                continue;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        if (account_bal_checker == true)
                        {
                            break;
                        }
                        if (counter == account_count - 1)
                        {
                            break;
                        }
                        counter++;
                        keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next_cc");
                        apmhelper.links_visibility(keyword.Locator, locator_type);
                        apmhelper.links(keyword.Locator, locator_type);
                    }
                    else
                    {
                        AppiumHelper apmhelper = new AppiumHelper();
                        Element keyword = ContextPage.GetInstance().GetElement("Accounts_Home_No");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        string account_no = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                        keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Balance");
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        string balance = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                        string tAccountNo = context.GeTran_Account();
                        if (tAccountNo == account_no || account_no == "Term Deposit") //||account_no == "HBL Mutual Funds")
                        {
                            if ((context.Get_term_deposit_check() == 1 || context.Get_term_deposit_check() == 2) && account_no == "Term Deposit")
                            {
                                old_account_bal = context.GetTran_Balance();
                                if (Convert.ToDecimal(balance) != old_account_bal)
                                {
                                    throw new AssertFailedException(string.Format("The Term Deposit balance {0} is not equal to new Term Deposit balance {1} after successfull transaction", old_account_bal, balance));
                                }
                                term_deposit_checker = true;
                                keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next");
                                try
                                {
                                    apmhelper.links_visibility(keyword.Locator, locator_type);
                                    apmhelper.links(keyword.Locator, locator_type);
                                    counter++;
                                    continue;
                                }
                                catch
                                {
                                    continue;
                                }

                            }
                            else if (context.Get_term_deposit_check() == 0 && account_no == "Term Deposit")
                            {
                                if (term_deposit_checker == true && account_bal_checker == true)
                                {
                                    break;
                                }
                                term_deposit_checker = true;
                                continue;
                            }
                            //else if (context.Get_mutual_fund_check() == 1 && account_no == "HBL Mutual Funds")
                            //{
                            //    old_account_bal = context.GetTran_Balance();
                            //    if (Convert.ToDecimal(balance) != old_account_bal)
                            //    {
                            //        throw new AssertFailedException(string.Format("The Mutual Fund balance {0} is not equal to new Mutual Fund balance {1} after successfull transaction", old_account_bal, balance));
                            //    }
                            //    mutual_fund_checker = true;
                            //    keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next");
                            //    try
                            //    {
                            //        apmhelper.links_visibility(keyword.Locator, locator_type);
                            //        apmhelper.links(keyword.Locator, locator_type);
                            //        counter++;
                            //        continue;
                            //    }
                            //    catch
                            //    {
                            //        continue;
                            //    }

                            //}
                            //else if (context.Get_mutual_fund_check() == 2 && account_no == "HBL Mutual Funds")
                            //{
                            //    old_account_bal = context.Get_mutual_fund_balance();
                            //    if (Convert.ToDecimal(balance) != old_account_bal)
                            //    {
                            //        throw new AssertFailedException(string.Format("The Mutual Fund balance {0} is not equal to new Mutual Fund balance {1} after successfull transaction", old_account_bal, balance));
                            //    }
                            //    mutual_fund_checker = true;
                            //    keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next");
                            //    try
                            //    {
                            //        apmhelper.links_visibility(keyword.Locator, locator_type);
                            //        apmhelper.links(keyword.Locator, locator_type);
                            //        counter++;
                            //        continue;
                            //    }
                            //    catch
                            //    {
                            //        continue;
                            //    }

                            //}

                            else if (tAccountNo == account_no)
                            {
                                old_account_bal = context.GetTran_Balance();
                                if (Convert.ToDecimal(balance) != old_account_bal)
                                {
                                    throw new AssertFailedException(string.Format("The old account balance {0} is not equal to new account balance {1} after successfull transaction", old_account_bal, balance));
                                }
                                account_bal_checker = true;
                                keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next");
                                try
                                {
                                    apmhelper.links_visibility(keyword.Locator, locator_type);
                                    apmhelper.links(keyword.Locator, locator_type);
                                    counter++;
                                    continue;
                                }
                                catch
                                {
                                    continue;
                                }
                            }

                        }
                        if (term_deposit_checker == true && account_bal_checker == true)
                        {
                            break;
                        }
                        //if (mutual_fund_checker == true && account_bal_checker == true)
                        //{
                        //    break;
                        //}
                        if (counter == account_count - 1)
                        {
                            break;
                        }
                        counter++;
                        keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Next");
                        apmhelper.links_visibility(keyword.Locator, locator_type);
                        apmhelper.links(keyword.Locator, locator_type);
                    }
                }
                catch (Exception exception)
                {
                    AppiumHelper.TakeScreenshot();
                    throw new AssertFailedException(exception.Message);
                }

            }
        }

        [When(@"verify bene status from (.*) on Schema ""(.*)""")]
        [Then(@"verify bene status from (.*) on Schema ""(.*)""")]
        public void WhenVerifyBeneStatusFromOnSchema(string query, string db_value)
        {
            try
            {
                if (query != "")
                {
                    if (query.Contains("{ConsumerNo}"))
                    {
                        query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                    }
                    if (query.Contains("Company_Code"))
                    {
                        query = query.Replace("{Company_Code}", context.GetCompany_Code());
                    }
                    string textboxvalue = context.GetBeneName();

                    Thread.Sleep(2000);
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
                    string bene_name = SourceDataTable.Rows[0][0].ToString();
                    AppiumHelper apmhelper = new AppiumHelper();

                    if (bene_name == "1" && context.GetBeneName() != "")
                    {
                        Element keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNickText");
                        apmhelper.SetTextBoxValue(textboxvalue, keyword.Locator, "id");
                        keyword = null;
                        Thread.Sleep(1000);
                        keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNickOkBtn");
                        apmhelper.Button(keyword.Locator, "id");
                    }
                    else if (bene_name == "1" && context.GetBeneName() == "")
                    {
                        Element keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNickCancelBtn");
                        apmhelper.Button(keyword.Locator, "id");
                    }
                    else if (bene_name != "1")
                    {
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I select consumers for multi bill payment as ""(.*)"" on ""(.*)""")]
        public void WhenISelectConsumersForMultiBillPaymentAsOn(string Consumer_No_List, string Keyword)
        {
            try
            {
                string BILL_BENE_NICK;
                string BILL_STATUS;
                string DUE_DATE;
                DateTime DUE_DATE_FORMAT;
                string amount;
                string ui_amount;
                string ui_duedate;
                string ui_bene_nick;
                AppiumHelper apmhelper = new AppiumHelper();
                string[] consumer_no_arr = Consumer_No_List.Split(',');
                context.Set_multi_bill_consumers(consumer_no_arr);
                for (int i = 0; i < consumer_no_arr.Length; i++)
                {
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    apmhelper.SetTextBoxValue(consumer_no_arr[i], keyword.Locator, "id");
                    string query = "SELECT PB.BILL_BENE_NICK,PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE,PB.COMPANY_SUB_CATEGORY FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CONSUMER_NUMBER = '" + consumer_no_arr[i] + "' AND PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')";
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                    BILL_BENE_NICK = SourceDataTable.Rows[0][0].ToString();
                    BILL_STATUS = SourceDataTable.Rows[0][1].ToString();
                    DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][2]));
                    if (DUE_DATE_FORMAT < DateTime.Today)
                    {
                        amount = SourceDataTable.Rows[0][4].ToString();
                    }
                    else
                    {
                        amount = SourceDataTable.Rows[0][3].ToString();
                    }
                    DUE_DATE = DUE_DATE_FORMAT.ToString("dd-MMM-yyyy");
                    Element Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNick_ViewScreen");
                    ui_bene_nick = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "id");
                    if(BILL_BENE_NICK == "")
                    {
                        BILL_BENE_NICK = SourceDataTable.Rows[0][5].ToString();
                    }
                    if (ui_bene_nick != BILL_BENE_NICK)
                    {
                        throw new AssertFailedException(string.Format("The Bene Nick in database {0} is not equal to Bene Nick On Screen {1}", BILL_BENE_NICK, ui_bene_nick));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_BillAmount_ViewScreen");
                    ui_amount = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "id");
                    ui_amount = ui_amount.Replace(@",", string.Empty);
                    if (ui_amount != amount)
                    {
                        throw new AssertFailedException(string.Format("The Amount database {0} is not equal to Amount On Screen {1}", amount, ui_amount));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_DueDate_ViewScreen");
                    ui_duedate = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "id");
                    ui_duedate = ui_duedate.Replace(@"Due Date: ", string.Empty);
                    if (ui_duedate != DUE_DATE)
                    {
                        throw new AssertFailedException(string.Format("The DueDate in database {0} is not equal to DueDate On Screen {1}", DUE_DATE, ui_duedate));
                    }
                    Element keyword2 = ContextPage.GetInstance().GetElement("BillPayment_SearchBeneConsumerNo");
                    Thread.Sleep(1000);
                    apmhelper.longpress(keyword2.Locator, "id");
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I verify bill details of consumer numbers for bill payment")]
        public void WhenIVerifyBillDetailsOfConsumerNumbersForBillPayment()
        {
            try
            {
                string BILL_STATUS;
                string ui_BILL_STATUS = "";
                string DUE_DATE;
                DateTime DUE_DATE_FORMAT;
                double amount = 0;
                string amount_within_dd;
                string ui_amount_within_dd = "";
                string amount_after_dd;
                string ui_amount_after_dd = "";
                string company_name;
                string ui_company_name = "";
                string consumer_name;
                string ui_consumer_name = "";
                string ui_amount;
                string ui_duedate = "";
                string COMPANY_CODE;
                string BILL_STATUS_ID;
                string SURCHARGE_ATTRIBUTE;
                AppiumHelper apmhelper = new AppiumHelper();
                string[] consumer_no_arr = context.Get_multi_bill_consumers();
                Element Temp_keyword;
                for (int i = 0; i < consumer_no_arr.Length; i++)
                {
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_BillingMonth");
                    string billing_month = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                    DateTime temp_var = Convert.ToDateTime(billing_month);
                    billing_month = temp_var.ToString("MM/yyyy");

                    string query = "Select L.COMPANY_CODE, L.DUE_DATE, l.BILL_AMOUNT, l.CONSUMER_NAME, L.BILL_STATUS_ID  FROM LP_BILLS L WHERE L.CONSUMER_NO ='" + consumer_no_arr[i] + "' and TO_CHAR(L.BILLING_MONTH,'MM/YYYY') = '" + billing_month + "'";
                    DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                    COMPANY_CODE = SourceDataTable.Rows[0][0].ToString();
                    DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][1]));
                    amount_within_dd = SourceDataTable.Rows[0][2].ToString();
                    //amount_after_dd = SourceDataTable.Rows[0][3].ToString();
                    consumer_name = SourceDataTable.Rows[0][3].ToString();
                    consumer_name = consumer_name.Replace(@" ", string.Empty);
                    BILL_STATUS_ID = SourceDataTable.Rows[0][4].ToString();
                    if (BILL_STATUS_ID == "1")
                    {
                        BILL_STATUS = "UNPAID";
                    }
                    else if (BILL_STATUS_ID == "2")
                    {
                        BILL_STATUS = "PAID";
                    }
                    else
                    {
                        BILL_STATUS = "BLOCKED";
                    }

                    query = "Select COMPANY_NAME, SURCHARGE_ATTRIBUTE from BPS_SURCHARGE_AUTOMATION AD where AD.COMPANY_CODE like '%" + COMPANY_CODE + "%'";
                    dLink = null;
                    dLink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = null;
                    SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                    company_name = SourceDataTable.Rows[0][0].ToString();
                    company_name = company_name.Replace("\r\n", "");
                    SURCHARGE_ATTRIBUTE = SourceDataTable.Rows[0][1].ToString();
                    query = "Select " + SURCHARGE_ATTRIBUTE + " from LP_BILLS L WHERE L.CONSUMER_NO = '" + consumer_no_arr[i] + "' AND TO_CHAR(L.BILLING_MONTH,'MM/YYYY') = '" + billing_month + "'";

                    dLink = null;
                    dLink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = null;
                    SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                    amount_after_dd = SourceDataTable.Rows[0][0].ToString();
                    if (DUE_DATE_FORMAT < DateTime.Today)
                    {
                        amount += Convert.ToInt32(amount_after_dd);
                    }
                    else
                    {
                        amount += Convert.ToInt32(amount_within_dd);
                    }
                    //Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    //apmhelper.SetTextBoxValue(consumer_no_arr[i], keyword.Locator, "id");
                    //query = "SELECT PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE,PB.COMPANY_NAME,PB.CONSUMER_NAME FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CONSUMER_NUMBER = '" + consumer_no_arr[i] + "' AND PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')";
                    //DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                    //DataTable SourceDataTable = dLink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                    //BILL_STATUS = SourceDataTable.Rows[0][0].ToString();
                    //DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][1]));
                    //amount_within_dd = SourceDataTable.Rows[0][2].ToString();
                    //amount_after_dd = SourceDataTable.Rows[0][3].ToString();
                    //company_name = SourceDataTable.Rows[0][4].ToString();
                    //consumer_name = SourceDataTable.Rows[0][5].ToString();
                    //consumer_name = consumer_name.Replace(@" ", string.Empty);
                    //if (DUE_DATE_FORMAT < DateTime.Today)
                    //{
                    //    amount += Convert.ToInt32(amount_after_dd);
                    //}
                    //else
                    //{
                    //    amount += Convert.ToInt32(amount_within_dd);
                    //}
                    DUE_DATE = DUE_DATE_FORMAT.ToString("dd-MMM-yyyy");
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_CompanyName");
                    if (company_name != "")
                    {
                        ui_company_name = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                    }
                    query = "SELECT CC.IS_PWD_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = (SELECT CC.COMPANY_CODE FROM BPS_COMPANY CC WHERE CC.COMPANY_NAME = '" + company_name + "') AND CC.CHANNEL_CODE = 'MB'";
                    dLink = null;
                    dLink = new DataAccessComponent.DataAccessLink();
                    SourceDataTable = null;
                    SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                    string tran_pass_req = SourceDataTable.Rows[0][0].ToString();
                    if (tran_pass_req == "1")
                    {
                        context.Set_is_tranpass_req(tran_pass_req);
                    }
                    //SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                    //BILL_STATUS = SourceDataTable.Rows[0][0].ToString();
                    if (ui_company_name != company_name)
                    {
                        throw new AssertFailedException(string.Format("The Company Name in database {0} is not equal to Company Name On Screen {1}", company_name, ui_company_name));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_ConsumerName");
                    if (consumer_name != "")
                    {
                        ui_consumer_name = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                        ui_consumer_name = ui_consumer_name.Replace(@" ", string.Empty);
                    }
                    if (ui_consumer_name != consumer_name)
                    {
                        throw new AssertFailedException(string.Format("The Consumer Name in database {0} is not equal to ConsumerName On Screen {1}", consumer_name, ui_consumer_name));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_DueDate");
                    if (DUE_DATE != "")
                    {
                        ui_duedate = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                    }
                    if (ui_duedate != DUE_DATE)
                    {
                        throw new AssertFailedException(string.Format("The Due Date in database {0} is not equal to Due Date On Screen {1}", DUE_DATE, ui_duedate));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_AmountWithInDD");
                    if (amount_within_dd != "")
                    {
                        ui_amount_within_dd = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                    }
                    if (ui_amount_within_dd != amount_within_dd.TrimStart(new Char[] { '0' }))
                    {
                        throw new AssertFailedException(string.Format("The Amount With In Due Date in database {0} is not equal to Amount With In Due Date On Screen {1}", amount_within_dd, ui_amount_within_dd));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_AmountAfterDD");
                    if (amount_after_dd != "")
                    {
                        ui_amount_after_dd = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                    }
                    if (ui_amount_after_dd != amount_after_dd.TrimStart(new Char[] { '0' }))
                    {
                        throw new AssertFailedException(string.Format("The Amount After Due Date in database {0} is not equal to Amount After Due Date On Screen {1}", amount_after_dd, ui_amount_after_dd));
                    }
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_BillStatus");
                    if (BILL_STATUS != "")
                    {
                        ui_BILL_STATUS = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                    }
                    if (ui_BILL_STATUS != BILL_STATUS)
                    {
                        throw new AssertFailedException(string.Format("The Bill Status in database {0} is not equal to Bill Status On Screen {1}", BILL_STATUS, ui_BILL_STATUS));
                    }
                    if (i != consumer_no_arr.Length - 1)
                    {
                        Temp_keyword = null;
                        Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_NextArrow");
                        apmhelper.links(Temp_keyword.Locator, "id");
                    }

                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_multipayment_totalamount");
                apmhelper.scroll_to_element_text("Next");
                string total_amount = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "id");
                total_amount = total_amount.Replace(@",", string.Empty);
                context.Set_multi_payment_amount(amount);
                if (amount.ToString() != total_amount)
                {
                    throw new AssertFailedException(string.Format("The Total Amount Calculated {0} is not equal to Total Amount On Screen {1}", amount, total_amount));
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }

        }
        [Then(@"verify multiple payments through ""(.*)"" on ""(.*)""")]
        public void ThenVerifyMultiplePaymentsThroughOn(string message, string Keyword)
        {
            string locator_type = "id";
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                if (message == "ConsumerNoContextVal")
                {
                    message = context.GetConsumer_No();
                }
                if (message == "ToAccountNoContextVal")
                {
                    message = context.GetToAccount_No();
                }
                string[] consumer_no_arr = context.Get_multi_bill_consumers();
                string[] tran_id_arr = new string[] { };
                for (int i = 0; i < consumer_no_arr.Length; i++)
                {
                    apmhelper.verification(message, keyword.Locator, locator_type);
                    if (Keyword.Contains("BillPayment_TranSuccess") || Keyword.Contains("SendMoney_TranSuccessMessage"))
                    {
                        keyword = null;
                        locator_type = "xpath";
                        string tranid_keyword = "SendMoney_TranID";
                        keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                        string tran_id = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                        tran_id_arr[i] = tran_id;
                    }
                }
                context.Set_multi_tran_ids(tran_id_arr);

            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Then(@"verify multiple payments summary ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyMultiplePaymentsSummaryOnAndOnAndOnAndOnAndOnAndOnOnSchema(string TranSuccessMessage, string Keyword1, string tran_type_query, string Keyword2, string tran_amount_query, string Keyword3, string from_account_query, string Keyword4, string company_name_query, string Keyword5, string consumer_no_query, string Keyword6, string schema)
        {
            //string[] tran_id_arr = context.Get_multi_tran_ids();
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword1);
            AppiumHelper apmhelper = new AppiumHelper();
            string locator_type = "xpath";
            string tran_id = "";
            if (keyword.Locator.StartsWith("/"))
            {
                locator_type = "xpath";
            }
            if (TranSuccessMessage == "ConsumerNoContextVal")
            {
                TranSuccessMessage = context.GetConsumer_No();
            }
            if (TranSuccessMessage == "ToAccountNoContextVal")
            {
                TranSuccessMessage = context.GetToAccount_No();
            }
            string[] queries = new string[5];
            string[] keywords = new string[5];
            queries[0] = tran_type_query;
            queries[1] = tran_amount_query;
            queries[2] = from_account_query;
            queries[3] = company_name_query;
            queries[4] = consumer_no_query;
            keywords[0] = Keyword2;
            keywords[1] = Keyword3;
            keywords[2] = Keyword4;
            keywords[3] = Keyword5;
            keywords[4] = Keyword6;
            locator_type = "id";
            string temp_query = "";
            for (int i = 0; i < consumer_no_arr.Length; i++)
            {
                apmhelper.verification(TranSuccessMessage, keyword.Locator, "xpath");
                if (Keyword1.Contains("BillPayment_TranSuccess") || Keyword1.Contains("SendMoney_TranSuccessMessage") || Keyword1.Contains("BillPayment_TranSuccess_MultiBill"))
                {
                    keyword = null;
                    locator_type = "xpath";
                    string tranid_keyword = "SendMoney_TranID";
                    keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                    tran_id = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                }
                for (int j = 0; j < queries.Length; j++)
                {
                    if (j == 0)
                    {
                        continue;
                    }
                    if (queries[j].Contains("DC_TRANSACTION"))
                    {
                        if (keywords[j].Contains("SendMoney_TranToBank") || keywords[j].Contains("SendMoney_TranType") || keywords[j].Contains("BillPayment_TranType"))
                        {
                            temp_query = queries[j];
                            queries[j] = queries[j] + tran_id + "')";
                        }
                        else
                        {
                            temp_query = queries[j];
                            queries[j] = queries[j] + tran_id + "'";
                        }
                    }
                    try
                    {
                        string message = "";
                        Thread.Sleep(3000);
                        keyword = null;
                        keyword = ContextPage.GetInstance().GetElement(keywords[j]);
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        if (queries[j] != "")
                        {
                            if (queries[j].Contains("{ConsumerNo}"))
                            {
                                queries[j] = queries[j].Replace("{ConsumerNo}", consumer_no_arr[i]);
                            }
                            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                            DataTable SourceDataTable = dlink.GetDataTable(queries[j], schema);
                            message = SourceDataTable.Rows[0][0].ToString();
                            if (keywords[j] == "SendMoney_TranBeneName")
                            {
                                if (message == "")
                                {
                                    string temp = queries[j].Replace("DT.BENEFICIARY_NAME", "DT.FT_TO_ACCOUNT_TITLE");
                                    queries[j] = temp;
                                }
                            }
                            if (queries[j].Equals("SendMoney_TranAmount") || keywords[j].Equals("BillPayment_TranAmount"))
                            {
                                message = Convert.ToDecimal(message).ToString("0.00");
                            }
                            //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                            //{
                            //    consumer_no_arr_new[i] = message;
                            //}
                        }
                        apmhelper.verification(message, keyword.Locator, locator_type);
                        queries[j] = temp_query;
                    }
                    catch (Exception exception)
                    {
                        AppiumHelper.TakeScreenshot();
                        throw new AssertFailedException(exception.Message);
                    }
                }
                if (i != consumer_no_arr.Length - 1)
                {
                    keyword = null;
                    keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_NextArrow");
                    apmhelper.links(keyword.Locator, "id");
                }
            }
        }

        [Then(@"verify transaction activity multiple payments ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTransactionActivityMultiplePaymentsOnAndOnAndOnAndOnAndOnAndOnOnSchema(string TranSuccessMessage, string Keyword1, string tran_type_query, string Keyword2, string tran_amount_query, string Keyword3, string from_account_query, string Keyword4, string company_name_query, string Keyword5, string consumer_no_query, string Keyword6, string schema)
        {
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword1);
            AppiumHelper apmhelper = new AppiumHelper();
            string locator_type = "xpath";
            string tran_id = "";
            if (keyword.Locator.StartsWith("/"))
            {
                locator_type = "xpath";
            }
            if (TranSuccessMessage == "ConsumerNoContextVal")
            {
                TranSuccessMessage = context.GetConsumer_No();
            }
            if (TranSuccessMessage == "ToAccountNoContextVal")
            {
                TranSuccessMessage = context.GetToAccount_No();
            }

            string[] queries = new string[5];
            string[] keywords = new string[5];
            queries[0] = tran_type_query;
            queries[1] = tran_amount_query;
            queries[2] = from_account_query;
            queries[3] = company_name_query;
            queries[4] = consumer_no_query;
            keywords[0] = Keyword2;
            keywords[1] = Keyword3;
            keywords[2] = Keyword4;
            keywords[3] = Keyword5;
            keywords[4] = Keyword6;
            locator_type = "id";
            string temp_query = "";
            for (int i = 1; i <= consumer_no_arr.Length; i++)
            {
                apmhelper.verification(TranSuccessMessage, keyword.Locator, "xpath");
                if (Keyword1.Contains("BillPayment_TranSuccess") || Keyword1.Contains("SendMoney_TranSuccessMessage") || Keyword1.Contains("BillPayment_TranSuccess_MultiBill"))
                {
                    keyword = null;
                    locator_type = "xpath";
                    string tranid_keyword = "SendMoney_TranID";
                    keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                    tran_id = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                }
                for (int j = 0; j < queries.Length; j++)
                {
                    if (queries[j].Contains("DC_TRANSACTION"))
                    {
                        if (keywords[j].Contains("SendMoney_TranToBank") || keywords[j].Contains("SendMoney_TranType") || keywords[j].Contains("BillPayment_TranType"))
                        {
                            temp_query = queries[j];
                            queries[j] = queries[j] + tran_id + "')";
                        }
                        else
                        {
                            temp_query = queries[j];
                            queries[j] = queries[j] + tran_id + "'";
                        }
                    }
                    try
                    {
                        string message = "";
                        Thread.Sleep(3000);
                        keyword = null;
                        keyword = ContextPage.GetInstance().GetElement(keywords[j]);
                        if (keyword.Locator.StartsWith("/"))
                        {
                            locator_type = "xpath";
                        }
                        if (queries[j] != "")
                        {
                            if (queries[j].Contains("{ConsumerNo}"))
                            {
                                queries[j] = queries[j].Replace("{ConsumerNo}", consumer_no_arr[i]);
                            }
                            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                            DataTable SourceDataTable = dlink.GetDataTable(queries[j], schema);
                            message = SourceDataTable.Rows[0][0].ToString();
                            if (keywords[j] == "SendMoney_TranBeneName")
                            {
                                if (message == "")
                                {
                                    string temp = queries[j].Replace("DT.BENEFICIARY_NAME", "DT.FT_TO_ACCOUNT_TITLE");
                                    queries[j] = temp;
                                }
                            }
                            if (queries[j].Equals("SendMoney_TranAmount") || keywords[j].Equals("BillPayment_TranAmount"))
                            {
                                message = Convert.ToDecimal(message).ToString("0.00");
                            }
                            //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                            //{
                            //    consumer_no_arr_new[i] = message;
                            //}
                        }
                        apmhelper.verification(message, keyword.Locator, locator_type);
                        queries[j] = temp_query;
                    }
                    catch (Exception exception)
                    {
                        AppiumHelper.TakeScreenshot();
                        throw new AssertFailedException(exception.Message);
                    }
                }
                if (i != consumer_no_arr.Length)
                {
                    keyword = null;
                    keyword = ContextPage.GetInstance().GetElement("TransactionActivity_Financial_Close");
                    apmhelper.links(keyword.Locator, "id");
                    keyword = null;
                    keyword = ContextPage.GetInstance().GetElement("TransactionActivity_LatestTranLink");
                    string temp = keyword.Locator.Replace("[1]", "[" + (i + 1).ToString() + "]");
                    apmhelper.links(keyword.Locator, "xpath");
                }
            }
        }


        [Then(@"verify multiple payments through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        public void ThenVerifyMultiplePaymentsThroughDatabaseOnOnSchemaOn(string query, string schema, string Keyword)
        {
            string[] tran_id_arr = context.Get_multi_tran_ids();
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            //string[] consumer_no_arr_new = new string[] { };
            for (int i = 0; i < tran_id_arr.Length; i++)
            {
                string locator_type = "id";
                if (query.Contains("DC_TRANSACTION"))
                {
                    if (Keyword.Contains("SendMoney_TranToBank") || Keyword.Contains("SendMoney_TranType") || Keyword.Contains("BillPayment_TranType"))
                    {
                        query = query + tran_id_arr[i] + "')";
                    }
                    else
                    {
                        query = query + tran_id_arr[i] + "'";
                    }
                }
                try
                {
                    string message = "";
                    AppiumHelper apmhelper = new AppiumHelper();
                    Thread.Sleep(3000);
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    if (query != "")
                    {
                        if (query.Contains("{ConsumerNo}"))
                        {
                            query = query.Replace("{ConsumerNo}", consumer_no_arr[i]);
                        }
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                        message = SourceDataTable.Rows[0][0].ToString();
                        if (Keyword == "SendMoney_TranBeneName")
                        {
                            if (message == "")
                            {
                                string temp = query.Replace("DT.BENEFICIARY_NAME", "DT.FT_TO_ACCOUNT_TITLE");
                                query = temp;
                            }
                        }
                        if (Keyword.Equals("SendMoney_TranAmount")) //|| Keyword.Equals("BillPayment_TranAmount"))
                        {
                            message = Convert.ToDecimal(message).ToString("0.00");
                        }
                        //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                        //{
                        //    consumer_no_arr_new[i] = message;
                        //}
                    }
                    apmhelper.verification(message, keyword.Locator, locator_type);
                }
                catch (Exception exception)
                {
                    AppiumHelper.TakeScreenshot();
                    throw new AssertFailedException(exception.Message);
                }
                //context.Set_multi_bill_consumers(consumer_no_arr_new);
            }
        }
        [Then(@"verify the result of schedule payment from database")]
        public void ThenVerifyTheResultOfSchedulePaymentFromDatabase()
        {
            try
            {
                string tran_master_id = "";
                string query = "SELECT TM.STATE,TM.SCHEDULED_AMOUNT,TM.SCHEDULED_TRAN_TYPE,TM.SCHEDULED_TRAN_MASTER_ID FROM DC_SCHEDULED_TRAN_MASTER TM WHERE TM.FUND_TRANSFER_BENEFICIARY_ID =  (SELECT FT.FUND_TRANSFER_BENEFICIARY_ID FROM DC_FUND_TRANSFER_BENEFICIARY FT WHERE FT.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "') AND FT.IS_DELETED = 0 AND FT.NICK = '" + context.GetCategory_value() + "') AND TM.IS_DELETED = 0";
                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                string state = SourceDataTable.Rows[0][0].ToString();
                string tran_amount = SourceDataTable.Rows[0][1].ToString();
                string tran_type = SourceDataTable.Rows[0][2].ToString();
                if ("41" != state)
                {
                    throw new AssertFailedException(string.Format("The State {0} is not equal to State in databse 41", state));
                }
                if (context.Get_tran_amount().ToString() != tran_amount)
                {
                    throw new AssertFailedException(string.Format("The Schedule Amount {0} is not equal to Schedule Amount in databse {1}", context.Get_tran_amount().ToString(), SourceDataTable.Rows[0][1].ToString()));
                }
                if ("LOCAL_FUND_TRANSFER" != tran_type)
                {
                    throw new AssertFailedException(string.Format("The Schedule transaction type {0} is not equal to Schedule transaction type in databse LOCAL_FUND_TRANSFER", tran_type));
                }
                tran_master_id = SourceDataTable.Rows[0][0].ToString();
                query = "";
                query = "SELECT TD.EXECUTION_DATE FROM DC_SCHEDULED_TRAN_DETAIL TD WHERE TD.SCHEDULED_TRAN_MASTER_ID = (SELECT MAX(TM.SCHEDULED_TRAN_MASTER_ID) FROM DC_SCHEDULED_TRAN_MASTER TM WHERE TM.FUND_TRANSFER_BENEFICIARY_ID = (SELECT FT.FUND_TRANSFER_BENEFICIARY_ID FROM DC_FUND_TRANSFER_BENEFICIARY FT WHERE FT.CUSTOMER_INFO_ID =(SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')AND FT.IS_DELETED = 0 AND FT.NICK = '" + context.GetCategory_value() + "')AND TM.IS_DELETED = 0) ORDER BY TD.EXECUTION_DATE ASC";
                SourceDataTable = null;
                SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                List<string> iterations = context.Get_iteration_dates_schedule();
                for (int i = 0; i < iterations.Count; i++)
                {
                    string temp = SourceDataTable.Rows[i][0].ToString();
                    DateTime dt = Convert.ToDateTime(temp);
                    temp = dt.ToString("dd-MM-yyyy");
                    if (iterations[i] != temp)
                    {
                        throw new AssertFailedException(string.Format("The Schedule Date {0} is not equal to Schedule Date in databse {1}", iterations[i], SourceDataTable.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
    }
}
