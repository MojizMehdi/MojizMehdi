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
            try
            {
                if (Keyword == "SendMoney_Rating" || Keyword == "SendMoney_RatingOkBtn" || Keyword == "SendMoney_Rating_Feedback_OkBtn")
                {
                    AppiumHelper apmhelper = new AppiumHelper();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    apmhelper.rating(keyword.Locator);
                }
                else if (!String.IsNullOrEmpty(Keyword))
                {
                    if (Keyword.Contains("BillPayment_AddNewBtn"))
                    {
                        string query = "SELECT COUNT(*) FROM DC_BILL_PAYMENT_BENEFICIARY BB WHERE BB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME ='" + context.GetUsername() + "') AND BB.COMPANY_SUB_CATEGORY = '" + context.GetCategory_value() + "' AND BB.IS_ACTIVE = 1";
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                        if (SourceDataTable.Rows.Count == 0)
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
                    //if (Keyword.Contains("BillPayment_PayNextBtn"))
                    //{
                    //    string otp = context.Get_is_otp_req();
                    //    string tran_pass = context.Get_is_tranpass_req();
                    //    if ((otp == "0" && tran_pass == "0") || (otp == "1" && tran_pass == "0") || (otp == "0" && tran_pass == "1"))
                    //    {
                    //        return;
                    //    }
                    //}

                    AppiumHelper apmhelper = new AppiumHelper();
                    //apmhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
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

        [When(@"I select from date ""(.*)""")]
        public void WhenISelectFromDate(string date)
        {
            AppiumHelper apmhelper = new AppiumHelper();
            DateTime current_date = DateTime.Today;
            DateTime given_date = Convert.ToDateTime(date);
            int monthsApart = 12 * (current_date.Year - given_date.Year) + current_date.Month - given_date.Month;
            monthsApart = Math.Abs(monthsApart);
            for (int i = 0; i < monthsApart; i++)
            {
                apmhelper.scroll_right(0.6, 0.8, 0.2);
            }
            Element keyword = ContextPage.GetInstance().GetElement("SendMoney_SchedulePayment_CalendarDateSelect");
            string temp = keyword.Locator.Replace("{calendar_date}", date.Split('/')[1].TrimStart(new Char[] { '0' }).ToString());
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
            int monthsApart = 12 * (current_date.Year - given_date.Year) + current_date.Month - given_date.Month;
            monthsApart = Math.Abs(monthsApart);
            for (int i = 0; i < monthsApart; i++)
            {
                apmhelper.scroll_right(0.6, 0.8, 0.2);
            }
            Element keyword = ContextPage.GetInstance().GetElement("SendMoney_SchedulePayment_CalendarDateSelect");
            string temp = keyword.Locator.Replace("{calendar_date}", date.Split('/')[1].TrimStart(new Char[] { '0' }).ToString());
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
                    apmhelper.scroll_down();
                }
                counter++;
            }
            context.Set_iteration_dates_schedule(lst);
        }

        [When(@"I scroll down")]
        public void WhenIScrollDown()
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                apmhelper.scroll_down();
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I wait (.*)")]
        [Then(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
        }

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
                apmhelper.verification(message, keyword.Locator,locator_type);
                if (Keyword.Contains("BillPayment_TranSuccess") || Keyword.Contains("SendMoney_TranSuccessMessage"))
                {
                    keyword = null;
                    locator_type = "xpath";
                    string tranid_keyword = "SendMoney_TranID";
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
                if (keyword.Locator.StartsWith("/"))
                {
                    locator_type = "xpath";
                }
                if (query != "")
                {
                    if (query.Contains("{ConsumerNo}"))
                    {
                        query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
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
                    if (Keyword.Equals("SendMoney_TranAmount")|| Keyword.Equals("BillPayment_TranAmount"))
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
            }
            if(attribute == "Transaction_Type")
            {
                context.SetTranType(value);
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
            tran_dict = context.Get_acc_balance();

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
        public void WhenISaveTransactionInfo()
        {
            string locator_type = "id";
            AppiumHelper apmhelper = new AppiumHelper();
            Element keyword = null;
            Dictionary<string, string> tran_dict = new Dictionary<string, string>();
            if (context.GetTranType() == "SendMoney")
            {
                keyword = ContextPage.GetInstance().GetElement("SendMoney_TranFromAcc");
            }
            else if(context.GetTranType() == "BillPayment")
            {
                keyword = ContextPage.GetInstance().GetElement("BillPayment_TranFromAcc");
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
            tran_dict = context.Get_acc_balance();

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
        [Then(@"I verify Account Balance")]
        public void ThenIVerifyAccountBalance()
        {
            string locator_type = "id";
            bool loop_end_check = true;
            decimal old_account_bal = 0;
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
                    if(tAccountNo == account_no)
                    {
                        old_account_bal = context.GetTran_Balance();
                        if (Convert.ToDecimal(balance) != old_account_bal)
                        {
                            throw new AssertFailedException(string.Format("The old account balance {0} is not equal to new account balance {1} after successfull transaction", old_account_bal, balance));
                        }
                        break;
                    }
                    keyword = ContextPage.GetInstance().GetElement("Accounts_Home_Previous");
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
                throw new AssertFailedException(string.Format("The State {0} is not equal to State in databse 41", context.Get_tran_amount().ToString()));
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
