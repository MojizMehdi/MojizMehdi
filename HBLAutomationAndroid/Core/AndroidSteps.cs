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

        [Given(@"the user is arrive to Mobile Banking home page")]
        public void GivenTheUserIsArriveToMobileBankingHomePage()
        {
            try
            {
                ContextPage.driver = DriverFactory.getDriver();
            }
            catch (Exception exception)
            {

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


        [When(@"I have given ""(.*)"" on ""(.*)""")]
        [Then(@"I have given ""(.*)"" on ""(.*)""")]
        public void WhenIHaveGivenOn(string textboxvalue, string Keyword)
        {
            string locator_type = "id";
            if (Keyword.Contains("Login_OTP_field"))
            {
                AppiumHelper apmhelper = new AppiumHelper();
                textboxvalue = apmhelper.GetOTP();

            }
            if (Keyword.Contains("SendMoney_Amount"))
            {
                context.Set_tran_amount(Convert.ToInt32(textboxvalue));
            }
            if (Keyword.Contains("SendMoney_SearchBeneField") || Keyword.Contains("BillPayment_SearchBeneField"))
            {
                context.SetCategory_value(textboxvalue);
            }
            if (String.IsNullOrEmpty(textboxvalue))
            {
                return;
            }
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                //keyword.Locator used instead od locator
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                apmhelper.SetTextBoxValue(textboxvalue, keyword.Locator,locator_type);

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
            DataTable SourceDataTable2;
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
            try
            {
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

                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"verify the message using element ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the message using element ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheMessageUsingElementThroughDatabaseOnOnSchema(string Keyword, string query, string schema)
        {
            string locator_type = "id";
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, schema);
            string message = SourceDataTable.Rows[0][0].ToString();
            if (message.Contains("<br>"))
            {
                message = message.Replace("<br>", string.Empty);
            }
            AppiumHelper apmhelper = new AppiumHelper();
            //apmhelper.checkPageIsReady();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            //keyword.Locator used instead od locator
            if (keyword.Locator.StartsWith("/"))
            {
                locator_type = "xpath";
            }
            string value = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
            if(value.Contains("\r") || value.Contains("\n"))
            {
                value = value.Replace("\r", string.Empty).Replace("\n", string.Empty);
            }
            Assert.AreEqual(message, value);
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
            string locator_type = "id";
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            AppiumHelper apmhelper = new AppiumHelper();
            if (keyword.Locator.StartsWith("/"))
            {
                locator_type = "xpath";
            }
            string temp = apmhelper.ReturnKeywordValue(keyword.Locator,locator_type);
            if (Keyword.Equals("BillPayment_Bill_Status"))
            {
                context.SetBill_Status(temp);
            }
        }

        [When(@"I want value from textview ""(.*)"" on database ""(.*)"" as ""(.*)""")]
        public void WhenIWantValueFromTextviewOnDatabaseAs(string Keyword, string db_value, string query)
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
                if (Keyword.Equals("Pay_Transaction_Unpaid_Amount"))
                {
                    context.SetCompany_Code(SourceDataTable.Rows[0][1].ToString());
                    string temp_query = query.Replace("LB.BILL_AMOUNT", "LB.DUE_DATE");
                    SourceDataTable = null;
                    SourceDataTable = dLink.GetDataTable(temp_query, db_value);
                    value2 = SourceDataTable.Rows[0][0].ToString();
                    if (Convert.ToDateTime(value2) < DateTime.Today)
                    {
                        query = "SELECT L.CONSUMER_NAME_TEMPLATE FROM BPS_COMPANY_CHANNEL L WHERE L.CHANNEL_CODE='MB'  AND L.COMPANY_CODE = '" + context.GetCompany_Code() + "'";
                        SourceDataTable = dLink.GetDataTable(query, db_value);
                        value = SourceDataTable.Rows[0][0].ToString();
                        string code = "Payable After Due Date|<FS_01:ATTRIBUTE";
                        code = value.Substring(value.IndexOf(code) + code.Length);
                        code = code.Split(new string[] { ">;" }, 2, StringSplitOptions.None)[0];
                        query = temp_query.Replace("LB.DUE_DATE", "LB.ATTRIBUTE_" + code);
                        SourceDataTable = dLink.GetDataTable(query, db_value);
                        value = SourceDataTable.Rows[0][0].ToString();
                    }
                    value = Convert.ToDecimal(value).ToString("0.00");
                }


            }
            if (keyword.Locator.StartsWith("/"))
            {
                locator_type = "xpath";
            }
            string keyword_value = apmhelper.ReturnKeywordValue(keyword.Locator,locator_type);
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
                apmhelper.Button(keyword.Locator,locator_type);
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

        [When(@"I have otp check and given (.*) on ""(.*)""")]
        public void WhenIHaveOtpCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword)
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
                apmhelper.SetTextBoxValue(otp_value, keyword.Locator,locator_type);
            }
        }



        [When(@"I have transaction pass check and given (.*) on ""(.*)""")]
        public void WhenIHaveTransactionPassCheckAndGivenOnOnCompanyCode(string tran_pass_value, string Keyword)
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
                apmhelper.SetTextBoxValue(tran_pass_value, keyword.Locator,locator_type);
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
                if (Keyword == "SendMoney_Rating" || Keyword == "SendMoney_RatingOkBtn" || Keyword == "SendMoney_Rating_Feedback_OkBtn")
                {
                    
                    //Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    apmhelper.rating(keyword.Locator);
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
                         apmhelper.links(locator,locator_type);
                    }
                }
                else if (!String.IsNullOrEmpty(Keyword))
                {
                    if (Keyword.Contains("BillPayment_AddNewBtn"))
                    {
                        string query = "SELECT COUNT(*) FROM DC_BILL_PAYMENT_BENEFICIARY BB WHERE BB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME ='" + context.GetUsername() + "') AND BB.COMPANY_SUB_CATEGORY = '" + context.GetCategory_value() + " Bill Payment' AND BB.IS_ACTIVE = 1";
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                        int a = Convert.ToInt32(SourceDataTable.Rows[0][0]);
                        if (Convert.ToInt32(SourceDataTable.Rows[0][0]) == 0)
                        {
                            return;
                        };
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
                        if((context.Get_no_of_accounts() + context.Get_bene_count_inter_branch()) <= 1)
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
                    apmhelper.links(keyword.Locator,locator_type);
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
            if (query.Contains("{Username}"))
            {
                query.Replace("{Username}", context.GetUsername());
            }
            if (query.Contains("{ConsumerNo}"))
            {
                query.Replace("{ConsumerNo}", context.GetConsumer_No());
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

        [When(@"I save Account Numbers")]
        public void WhenISaveAccountNumbers()
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
                acc_no = apmhelper.ReturnKeywordValue(temp,"xpath");
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

        [Given(@"update the data by query ""(.*)"" on Schema ""(.*)""")]
        public void GivenUpdateTheDataByQueryOnSchema(string query, string schema)
        {
            if (query != "")
            {
                if (query.Contains("{ConsumerNo}"))
                {
                    query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                }
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, schema);
                }
                catch (Exception e)
                {
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
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, "DIGITAL_CHANNEL_SEC");
                }
                catch (Exception e)
                {
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
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, "QAT_BPS");
                }
                catch (Exception e)
                {
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
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                apmhelper.combobox(value, keyword.Locator,locator_type);
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
            if(ui_list.Count == SourceDataTable.Rows.Count)
            {
                for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                {
                    if(ui_list[i] != SourceDataTable.Rows[i][0].ToString())
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

        [When(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheResultFromOnSchema(string query, string db_value)
        {
            if (query != "")
            {
                if (query.Contains("Company_Code"))
                {
                    query = query.Replace("{Company_Code}", context.GetCompany_Code());
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

        [Given(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        [When(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheListUsingOnSchema(string query, string schema)
        {
            List<string> message = new List<string>();
            List<string> db_result = new List<string>();
            string db_result_value = "";
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
                if(query.Contains("SELECT PP.NAME_OF_FUND from QAT_AMC.AMC_PRODUCT_PROFILE"))
                {
                    message = context.Get_scroll_items_list();
                }
                Thread.Sleep(2000);
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                for (int i = 0; i < SourceDataTable.Rows.Count; i++)
                {
                    if (message[i] != SourceDataTable.Rows[i][0].ToString())
                    {
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


        [Given(@"verify the data using ""(.*)"" on Schema ""(.*)""")]
        [When(@"verify the data using ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the data using ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheDataUsingOnSchema(string query, string schema)
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


        [When(@"I select from date ""(.*)""")]
        public void WhenISelectFromDate(string date)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            DateTime current_date = DateTime.Today;
            DateTime given_date = Convert.ToDateTime(date);
            string xpath_date = given_date.ToString("dd MMMM yyyy");
            int monthsApart = 12 * (current_date.Year - given_date.Year) + current_date.Month - given_date.Month;
            monthsApart = Math.Abs(monthsApart);
            for (int i = 0; i < monthsApart; i++)
            {
                if (Convert.ToDouble(Configuration.GetInstance().GetByKey("platformVersion")) <= 5.0)
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
        [When(@"I select to date ""(.*)""")]
        public void WhenISelectToDate(string date)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            DateTime current_date = DateTime.Today;
            DateTime given_date = Convert.ToDateTime(date);
            string xpath_date = given_date.ToString("dd MMMM yyyy");
            int monthsApart = 12 * (current_date.Year - given_date.Year) + current_date.Month - given_date.Month;
            monthsApart = Math.Abs(monthsApart);
            for (int i = 0; i < monthsApart; i++)
            {
                string platformVersion = Configuration.GetInstance().GetByKey("platformVersion");
                double pv = Convert.ToDouble(platformVersion);
                if (pv <= 5.1)
                {
                    apmhelper.scroll_down(0.65, 0.30);
                }
                else
                {
                    apmhelper.scroll_right(0.6, 0.8, 0.2);
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

        [When(@"I am verifying list of execution iterations on ""(.*)""")]
        public void WhenIAmVerifyingListOfExecutionIterationsOn(string Keyword)
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
                string locator = keyword.Locator.Replace("{Date}", lst[counter - 1].ToString());
                lstui.Add(apmhelper.ReturnKeywordValue(locator, "xpath"));
                if (lst[counter - 1] != lstui[counter - 1])
                {
                    throw new AssertFailedException(string.Format("The Iteration Date against keyword is: {0} and Iteration Date calculated by code is {1}", lstui[counter], lst[counter]));
                }
                if(counter != 0 && counter % 8 == 0)
                {
                    apmhelper.scroll_down(0.65,0.30);
                }
                counter++;
            }
            context.Set_iteration_dates_schedule(lst);
        }

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
        [When(@"I scroll to save the elements of list on ""(.*)""")]
        public void WhenIScrollToSaveTheElementsOfListOn(string scroll_text)
        {
            
        }

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
            if (keyword.Locator.StartsWith("/")|| keyword.Locator.StartsWith("("))
            {
                locator_type = "xpath";
            }
            try
            {
                for (int i = 0; i < count; i++)
                {
                    if(i != 0)
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
                if(Keyword == "MutualFund_List_FundName")
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
                if (Keyword.Equals("MutualFund_DisPopup") && context.Get_MutualFundDisclaimerPopup() == null)
                {
                    return;
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
                apmhelper.verification(message, keyword.Locator,locator_type);
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
                    string tran_id = apmhelper.ReturnKeywordValue(keyword.Locator,locator_type);
                    context.SetTransaction_Id(tran_id);
                }
            }
            catch (Exception exception)
            {

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
                    if(Keyword == "SendMoney_TranBeneName")
                    {
                        if(message == "")
                        {
                            string temp = query.Replace("DT.BENEFICIARY_NAME", "DT.FT_TO_ACCOUNT_TITLE");
                            query = temp;
                        }
                    }
                    if (Keyword.Equals("SendMoney_TranAmount")|| Keyword.Equals("TermDeposit_TranAmount") || Keyword.Equals("BillPayment_TranAmount"))
                    {
                        message = Convert.ToDecimal(message).ToString("0.00");
                    }
                    //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                    //{
                    //    context.SetConsumer_No(message);
                    //}
                }
                apmhelper.verification(message, keyword.Locator,locator_type);
            }
            catch (Exception exception)
            {

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
            if (attribute == "customer_cnic")
            {
                context.SetCustomerCNIC(value);
            }
            if(attribute == "SignupCheck")
            {
                context.Set_signup_check(Convert.ToBoolean(value));
            }
            if (attribute == "scroll_text")
            {
                context.SetScrollText(value);
            }
            if(attribute == "TranTypeBene")
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
            if(attribute == "Transaction_Type")
            {
                context.SetTranType(value);
            }
            if(attribute == "TermDepositYears")
            {
                context.Set_TermDepositYears(value);
            }
            if(attribute == "term_deposit_flag")
            {
                if (context.Get_term_deposit_check() == 0)
                {
                    context.Set_term_deposit_check(Convert.ToInt32(value));
                }
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
            if(attribute == "Last_login_flag")
            {
                context.SetLastLoginFlag(Convert.ToBoolean(value));
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
                if (attribute == "No_Of_Accounts")
                {
                    context.Set_no_of_accounts(Convert.ToInt32(db_value));
                }
                if(attribute == "Beneficiary_Count_Inter_Branch")
                {
                    context.Set_bene_count_inter_branch(Convert.ToInt32(db_value));
                }
                if (attribute == "Beneficiary_Count_Inter_Bank")
                {
                    context.Set_bene_count_inter_bank(Convert.ToInt32(db_value));
                }
                if(attribute == "mutual_fund_disclaimer_popup")
                {
                    context.Set_MutualFundDisclaimerPopup(db_value);
                }
                if(attribute == "customer_profile_id")
                {
                    context.Set_cust_profile_id(db_value);
                }
                if (attribute == "customer_cnic")
                {
                    context.SetCustomerCNIC(db_value);
                }
                if(attribute == "schedule_configuration")
                {
                    context.Set_BP_schedule_config(db_value);
                }
                if(attribute == "GUID")
                {
                    context.Set_HostReferenceNo(db_value);
                }
            }
            catch
            {

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
                    if(counter.ToLower() != "all")
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

                    string[,] array_mutual_fund_ui = new string[size, 6];
                    string[,] array_mutual_fund_db = new string[size, 6];

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
                    string new_query = "SELECT PP.NAME_OF_FUND, L.FOLIO_NO, CP.BALANCE, CP.UNITS, PP.NAV_PRICE, PP.NAV_DATE FROM AMC_CUSTOMER_PROFILE L INNER JOIN AMC_CUSTOMER_PORTFOLIO CP ON L.CUSTOMER_PROFILE_ID = CP.CUSTOMER_PROFILE_ID INNER JOIN AMC_PRODUCT_PROFILE PP ON CP.PRODUCT_ID = PP.PRODUCT_ID WHERE L.CNIC = '" + context.GetCustomerCNIC() + "' order by PP.NAME_OF_FUND";
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(new_query, schema);
                    for (int i = 0; i < size; i++)
                    {
                        string fund_name_db = SourceDataTable.Rows[i][0].ToString();
                        string folio_no_db = SourceDataTable.Rows[i][1].ToString();
                        double balance_d = Convert.ToDouble(SourceDataTable.Rows[i][2].ToString());
                        balance_d = Math.Round(balance_d, 2);
                        amount_db += balance_d;
                        string balance_db = string.Format("{0:0.00}", balance_d);
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
                    if (array_mutual_fund_db.GetLength(0) != array_mutual_fund_ui.GetLength(0))
                    {
                        throw new Exception(string.Format("The size of customer mutual fund records are not equal with database :{0} and website :{1}", array_mutual_fund_db.GetLength(0), array_mutual_fund_ui.GetLength(0)));
                    }

                    for (int i = 0; i < array_mutual_fund_db.GetLength(0); i++)
                        for (int j = 0; j < array_mutual_fund_db.GetLength(0); j++)
                            if (array_mutual_fund_db[i, j] != array_mutual_fund_ui[i, j])
                            {
                                throw new Exception(string.Format("The value on UI :{0} is not equal with value on database :{1}", array_mutual_fund_db[i, j], array_mutual_fund_ui[i, j]));
                            }

                    if (amount_db != amount_ui)
                    {
                        throw new Exception(string.Format("Total Mutual fund amount in database :{0} is not equal with toal Mutual fund amount on UI :{1}", amount_db, amount_ui));
                    }
                }
            }
            catch (Exception exception)
            {
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
            //int counter = 0;
            while(loop_end_check == true)
            {
                try
                {
                    Element keyword = ContextPage.GetInstance().GetElement("Accounts_Home_No");
                    if (keyword.Locator.StartsWith("/"))
                    {
                        locator_type = "xpath";
                    }
                    string account_no = apmhelper.ReturnKeywordValue(keyword.Locator, locator_type);
                    if(account_no == "Term Deposit")
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
                    apmhelper.links_visibility(keyword.Locator, locator_type);
                    apmhelper.links(keyword.Locator, locator_type);
                }
                catch
                {
                    break;
                }
            }
            context.Set_acc_balances(dict);
        }
        [When(@"I save Transaction Info for MultiPayment")]
        public void WhenISaveTransactionInfoForMultiPayment()
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


        [When(@"I save Transaction Info")]
        [Then(@"I save Transaction Info")]
        public void WhenISaveTransactionInfo()
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
            else if(context.GetTranType() == "BillPayment")
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
            string tran_account = apmhelper.ReturnKeywordValue(keyword.Locator,locator_type);
            context.SetTran_Account(tran_account);
            keyword = ContextPage.GetInstance().GetElement("SendMoney_TranAmount");
            if (keyword.Locator.StartsWith("/"))
            {
                locator_type = "xpath";
            }
            string tran_balance = apmhelper.ReturnKeywordValue(keyword.Locator,locator_type);
            tran_dict = context.Get_acc_balances();

            foreach (var item in tran_dict)
            {
                if (item.Key == "Term Deposit")
                {
                    if (context.Get_term_deposit_check() == 2 && item.Key == "Term Deposit")
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
        [Then(@"I verify Account Balance")]
        public void ThenIVerifyAccountBalance()
        {
            string locator_type = "id";
            bool loop_end_check = true;
            decimal old_account_bal = 0;
            bool account_bal_checker = false;
            bool term_deposit_checker = false;
            bool mutual_fund_checker = false;
            int account_count = context.Get_acc_balances().Count;
            int counter = 0;
            while (loop_end_check == true)
            {
                try
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
                    if(tAccountNo == account_no || account_no == "Term Deposit") //||account_no == "HBL Mutual Funds")
                    {
                        if (context.Get_term_deposit_check() == 1 && account_no == "Term Deposit")
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
                        else if (context.Get_term_deposit_check() == 2 && account_no == "Term Deposit")
                        {
                            old_account_bal = context.Get_term_deposit_balance();
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
                    if(term_deposit_checker == true && account_bal_checker == true)
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
                catch (Exception exception)
                {

                    throw new AssertFailedException(exception.Message);
                }

            }
        }

        [When(@"verify bene status from (.*) on Schema ""(.*)""")]
        [Then(@"verify bene status from (.*) on Schema ""(.*)""")]
        public void WhenVerifyBeneStatusFromOnSchema(string query, string db_value)
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
                    apmhelper.SetTextBoxValue(textboxvalue, keyword.Locator,"id");
                    keyword = null;
                    Thread.Sleep(1000);
                    keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNickOkBtn");
                    apmhelper.Button(keyword.Locator,"id");
                }
                else if (bene_name == "1" && context.GetBeneName() == "")
                {
                   Element  keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNickCancelBtn");
                   apmhelper.Button(keyword.Locator, "id");
                }
                else if (bene_name != "1")
                {
                    return;
                }
            }
        }
        [When(@"I select consumers for multi bill payment as ""(.*)"" on ""(.*)""")]
        public void WhenISelectConsumersForMultiBillPaymentAsOn(string Consumer_No_List, string Keyword)
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
                string query = "SELECT PB.BILL_BENE_NICK,PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CONSUMER_NUMBER = '" + consumer_no_arr[i] + "' AND PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')";
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                BILL_BENE_NICK = SourceDataTable.Rows[0][0].ToString();
                BILL_STATUS = SourceDataTable.Rows[0][1].ToString();
                DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][2]));
                if(DUE_DATE_FORMAT < DateTime.Today)
                {
                    amount = SourceDataTable.Rows[0][4].ToString();
                }
                else
                {
                    amount = SourceDataTable.Rows[0][3].ToString();
                }
                DUE_DATE = DUE_DATE_FORMAT.ToString("dd-MMM-yyyy");
                Element Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_BeneNick_ViewScreen");
                ui_bene_nick = apmhelper.ReturnKeywordValue(Temp_keyword.Locator,"id");
                if(ui_bene_nick != BILL_BENE_NICK)
                {
                    throw new AssertFailedException(string.Format("The Bene Nick in database {0} is not equal to Bene Nick On Screen {1}", BILL_BENE_NICK, ui_bene_nick));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_BillAmount_ViewScreen");
                ui_amount = apmhelper.ReturnKeywordValue(Temp_keyword.Locator,"id");
                ui_amount = ui_amount.Replace(@",", string.Empty);
                if (ui_amount != amount)
                {
                    throw new AssertFailedException(string.Format("The Amount database {0} is not equal to Amount On Screen {1}", amount, ui_amount));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_DueDate_ViewScreen");
                ui_duedate = apmhelper.ReturnKeywordValue(Temp_keyword.Locator,"id");
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
        [When(@"I verify bill details of consumer numbers for bill payment")]
        public void WhenIVerifyBillDetailsOfConsumerNumbersForBillPayment()
        {
            string BILL_STATUS;
            string ui_BILL_STATUS;
            string DUE_DATE;
            DateTime DUE_DATE_FORMAT;
            int amount = 0;
            string amount_within_dd;
            string ui_amount_within_dd;
            string amount_after_dd;
            string ui_amount_after_dd;
            string company_name;
            string ui_company_name;
            string consumer_name;
            string ui_consumer_name;
            string ui_amount;
            string ui_duedate;
            AppiumHelper apmhelper = new AppiumHelper();
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            Element Temp_keyword;
            for (int i = 0; i < consumer_no_arr.Length; i++)
            {
                //Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                //apmhelper.SetTextBoxValue(consumer_no_arr[i], keyword.Locator, "id");
                string query = "SELECT PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE,PB.COMPANY_NAME,PB.CONSUMER_NAME FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CONSUMER_NUMBER = '" + consumer_no_arr[i] + "' AND PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')";
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                BILL_STATUS = SourceDataTable.Rows[0][0].ToString();
                DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][1]));
                amount_within_dd = SourceDataTable.Rows[0][2].ToString();
                amount_after_dd = SourceDataTable.Rows[0][3].ToString();
                company_name = SourceDataTable.Rows[0][4].ToString();
                consumer_name = SourceDataTable.Rows[0][5].ToString();
                consumer_name = consumer_name.Replace(@" ", string.Empty);
                if (DUE_DATE_FORMAT < DateTime.Today)
                {
                    amount += Convert.ToInt32(amount_after_dd);
                }
                else
                {
                    amount += Convert.ToInt32(amount_within_dd);
                }
                DUE_DATE = DUE_DATE_FORMAT.ToString("dd-MMM-yyyy");
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_CompanyName");
                ui_company_name = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                query = "SELECT CC.IS_PWD_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = (SELECT CC.COMPANY_CODE FROM BPS_COMPANY CC WHERE CC.COMPANY_NAME = '" + company_name + "') AND CC.CHANNEL_CODE = 'MB'";
                dLink = null;
                dLink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = null;
                SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                string tran_pass_req = SourceDataTable.Rows[0][0].ToString();
                if(tran_pass_req == "1")
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
                ui_consumer_name = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                ui_consumer_name = ui_consumer_name.Replace(@" ", string.Empty);
                if (ui_consumer_name != consumer_name)
                {
                    throw new AssertFailedException(string.Format("The Consumer Name in database {0} is not equal to ConsumerName On Screen {1}", consumer_name, ui_consumer_name));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_DueDate");
                ui_duedate = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                if (ui_duedate != DUE_DATE)
                {
                    throw new AssertFailedException(string.Format("The Due Date in database {0} is not equal to Due Date On Screen {1}", DUE_DATE, ui_duedate));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_AmountWithInDD");
                ui_amount_within_dd = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                if (ui_amount_within_dd != amount_within_dd)
                {
                    throw new AssertFailedException(string.Format("The Amount With In Due Date in database {0} is not equal to Amount With In Due Date On Screen {1}", amount_within_dd, ui_amount_within_dd));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_AmountAfterDD");
                ui_amount_after_dd = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
                if (ui_amount_after_dd != amount_after_dd)
                {
                    throw new AssertFailedException(string.Format("The Amount After Due Date in database {0} is not equal to Amount After Due Date On Screen {1}", amount_after_dd, ui_amount_after_dd));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("BillPayment_Inquiry_BillStatus");
                ui_BILL_STATUS = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "xpath");
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
            string total_amount = apmhelper.ReturnKeywordValue(Temp_keyword.Locator, "id");
            total_amount = total_amount.Replace(@",", string.Empty);
            context.Set_multi_payment_amount(amount);
            if (amount.ToString() != total_amount)
            {
                throw new AssertFailedException(string.Format("The Total Amount Calculated {0} is not equal to Total Amount On Screen {1}", amount, total_amount));
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

                throw new AssertFailedException(exception.Message);
            }
        }

        [Then(@"verify multiple payments summary ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyMultiplePaymentsSummaryOnAndOnAndOnAndOnAndOnAndOnOnSchema(string TranSuccessMessage, string Keyword1, string tran_type_query, string Keyword2, string tran_amount_query, string Keyword3, string from_account_query, string Keyword4, string company_name_query, string Keyword5, string consumer_no_query, string Keyword6,string schema)
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
                //apmhelper.verification(TranSuccessMessage, keyword.Locator, locator_type);
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
                //apmhelper.verification(TranSuccessMessage, keyword.Locator, locator_type);
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
                        if (Keyword.Equals("SendMoney_TranAmount") || Keyword.Equals("BillPayment_TranAmount"))
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

                    throw new AssertFailedException(exception.Message);
                }
                //context.Set_multi_bill_consumers(consumer_no_arr_new);
            }
        }
        [Then(@"verify the result of schedule payment from database")]
        public void ThenVerifyTheResultOfSchedulePaymentFromDatabase()
        {
            string tran_master_id = "";
            string query = "SELECT TM.STATE,TM.SCHEDULED_AMOUNT,TM.SCHEDULED_TRAN_TYPE,TM.SCHEDULED_TRAN_MASTER_ID FROM DC_SCHEDULED_TRAN_MASTER TM WHERE TM.FUND_TRANSFER_BENEFICIARY_ID =  (SELECT FT.FUND_TRANSFER_BENEFICIARY_ID FROM DC_FUND_TRANSFER_BENEFICIARY FT WHERE FT.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() +"') AND FT.IS_DELETED = 0 AND FT.NICK = '" + context.GetCategory_value() + "') AND TM.IS_DELETED = 0";
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
    }
}
