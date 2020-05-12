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
        public void WhenIHaveTransactionPassCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword)
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
                apmhelper.SetTextBoxValue(otp_value, keyword.Locator,locator_type);
            }
        }



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
                    if (Keyword.Contains("BillPayment_NextBtn"))
                    {
                        if(context.Get_is_otp_req() == "0" && context.Get_is_tranpass_req() == "0")
                        {
                            return;
                        }
                    }
                    if (Keyword.Contains("BillPayment_PayNextBtn"))
                    {
                        string otp = context.Get_is_otp_req();
                        string tran_pass = context.Get_is_tranpass_req();
                        if ((otp == "0" && tran_pass == "0") || (otp == "1" && tran_pass == "0") || (otp == "0" && tran_pass == "1"))
                        {
                            return;
                        }
                    }

                    AppiumHelper apmhelper = new AppiumHelper();
                    //apmhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    if (keyword.Locator.StartsWith("/"))
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
                if (Keyword.Equals("SendMoney_Frequency"))
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
                    if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                    {
                        context.SetConsumer_No(message);
                    }
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


    }
}
