using HBLAutomationWeb.Beans;
using HBLAutomationWeb.Common;
using HBLAutomationWeb.Pages;
using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using TechTalk.SpecFlow;
using System.Data;
using HBLAutomationWeb.XML.ElementFactory;
using System.Threading;
using OpenQA.Selenium;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Collections.Generic;

namespace HBLAutomationWeb.Core
{
    [Binding]
    public class IBWebSteps
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

        [Given(@"the user is arrive to Internet Banking home page")]
        public void GivenTheUserIsArriveToInternetBankingLoginPage()
        {
            try
            {
                ContextPage.Driver = DriverFactory.getDriver();
            }
            catch (Exception exception)
            {

                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"I have given ""(.*)"" on ""(.*)""")]
        [When(@"I have given ""(.*)"" on ""(.*)""")]
        [Then(@"I have given ""(.*)"" on ""(.*)""")]
        public void GivenIHaveGivenOn(string textboxvalue, string Keyword)
        {
            string otp = "";
            string Key = "cf345ae2xz40yfc8";
            string IV = "abcaqwerabcaqwer";

            //if(textboxvalue == "ali")
            //{
            //    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            //    DataTable SourceDataTable = dlink.GetDataTable("SELECT CI.CUSTOMER_NAME FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_INFO_ID = '9483386'", "QADB");
            //    string CustomerName = SourceDataTable.Rows[0][0].ToString();

            //    dlink = new DataAccessComponent.DataAccessLink();
            //    SourceDataTable = dlink.GetDataTable("SELECT LB.COMPANY_CODE FROM LP_BILLS LB WHERE LB.CONSUMER_NO = '0400000263263' and LB.BILL_STATUS_ID=1 and LB.STAGING_ID='32551140'", "QAT_BPS");
            //    string Company = SourceDataTable.Rows[0][0].ToString();
            //}

            //if (Keyword.Contains("Pay_Transaction_MaxBillAmount_value") && textboxvalue == "")
            //{
            // return;
            // }
            if (Keyword.Contains("Login_OTP_field"))
            {
                string query = "Select PARAMTER_VALUE  from DC_APPLICATION_PARAM_DETAIL i where I.PARAMETER_NAME='OTP_HISTORY_ENCRYPTED'";
                string schema = "DIGITAL_CHANNEL_SEC";

                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable =  dLink.GetDataTable(query, schema);
                string otp_flag = SourceDataTable.Rows[0][0].ToString();

                if (otp_flag == "1")
                {
                    string query3 = "Select CUSTOMER_INFO_ID from dc_customer_info i where I.CUSTOMER_NAME='{usernmae}'";
                    query3 = query3.Replace("{usernmae}", context.GetUsername());

                    DataAccessComponent.DataAccessLink dLink3 = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable3 = dLink3.GetDataTable(query3, schema);
                    string consumer_no = SourceDataTable3.Rows[0][0].ToString();

                    string query2 = "Select I.OTP from DC_OTP_HISTORY I where I.CUSTOMER_INFO_ID='{consumer_no}' ORDER BY I.GENERATED_ON DESC";
                    query2 = query2.Replace("{consumer_no}", consumer_no);

                    DataAccessComponent.DataAccessLink dLink2 = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable2 = dLink2.GetDataTable(query2, schema);
                    otp = SourceDataTable2.Rows[0][0].ToString();

                    SeleniumHelper selhelper = new SeleniumHelper();
                    string dec_otp = selhelper.OTPDecrypt(otp, Key, IV);
                    
                }

                


            }
            if (Keyword.Contains("Pay_Card_Expiry_Date"))
            {
                return;
            }
            if (Keyword.Contains("Accounts_NoOfDays"))
            {
                if ((Convert.ToInt32(textboxvalue) > Convert.ToInt32(context.GetAccStatementDays())) && (Convert.ToInt32(textboxvalue) == 0))
                {
                    throw new AssertFailedException("Number of Days must be between 1 and 100");
                }
            }

            if (Keyword.Contains("BeneficiaryManagement_AccountNo"))
            {
                textboxvalue = context.GetBeneAccountNo();
            }
            if (String.IsNullOrEmpty(textboxvalue))
            {
                return;
            }
            try
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                selhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                //keyword.Locator used instead od locator
                selhelper.SetTextBoxValue(textboxvalue, keyword.Locator);

            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }

        }

        [When(@"I am performing on ""(.*)""")]
        [Then(@"I am performing on ""(.*)""")]
        public void WhenIAmPerformingOn(string Keyword)
        {
            try
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                selhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                selhelper.Button(keyword.Locator);
            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"I am clicking on ""(.*)""")]
        [When(@"I am clicking on ""(.*)""")]
        [Then(@"I am clicking on ""(.*)""")]
        public void WhenIAmClickingOn(string Keyword)
        {

            try
            {

                
                if (Keyword == "Pay_Transaction_PayBill_Rating" || Keyword == "Pay_Transaction_PayBill_RatingOkBtn")
                {
                    SeleniumHelper selhelper = new SeleniumHelper();
                    selhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    selhelper.rating(keyword.Locator);
                }
                else if (!String.IsNullOrEmpty(Keyword))
                {
                    SeleniumHelper selhelper = new SeleniumHelper();
                    selhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    selhelper.links(keyword.Locator);
                }


            }

            catch (Exception exception)
            {
                if (Keyword != "Pay_AddNewBtn")
                {

                    SeleniumHelper.TakeScreenshot();
                    throw new AssertFailedException(exception.Message);
                }
            }

        }

        [When(@"I am clicking on link ""(.*)"" on ""(.*)""")]
        public void WhenIAmClickingOnLinkOn(string text, string Keyword)
        {  
            try
            {
                if (!String.IsNullOrEmpty(Keyword))
                {
                    SeleniumHelper selhelper = new SeleniumHelper();
                    selhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    string locator = keyword.Locator;
                    if (Keyword.Contains("BillPaymentCategory"))
                    {
                        locator = keyword.Locator.Replace("{BillPaymentCategory}", text);
                        //keyword.Locator = locator;
                    }
                    if (Keyword.Contains("Pay_BillPaymentCategory_Company"))
                    {
                        locator = keyword.Locator.Replace("{Pay_BillPaymentCategory_Company}", text);
                        //keyword.Locator = locator;
                    }
                    //Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    selhelper.links(locator);
                }

            }

            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"I select ""(.*)"" on ""(.*)""")]
        [When(@"I select ""(.*)"" on ""(.*)""")]
        public void GivenISelectOn(string value, string Keyword)
        {
            int count = context.GeTSizeCount();
            if (count== 1)
            {
                return;
            }
            if (String.IsNullOrEmpty(value))
            {
                return;
            }
            try
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                selhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                selhelper.combobox(value, keyword.Locator);
                if (Keyword.Equals("SendMoney_Frequency"))
                {
                    context.Setfrequency(value);
                }
            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        //[Given(@"update the data by query ""(.*)""")]
        //public void GivenUpdateTheDataByQuery(string query)
        //{
        //    if (query != "")
        //    {
        //        try
        //        {
        //            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
        //            dlink.GetNonQueryResult(query, "QADB");
        //        }
        //        catch (Exception e)
        //        {
        //            throw new AssertFailedException(e.Message);

        //        }
        //    }
        //}

        [Given(@"update the data by query ""(.*)"" on Schema ""(.*)""")]
        public void GivenUpdateTheDataByQueryOnSchema(string query, string schema)
        {
            if (query != "")
            {
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
        [When(@"update the data by query ""(.*)"" on DIGITAL_CHANNEL_SEC")]
        [Then(@"update the data by query ""(.*)"" on DIGITAL_CHANNEL_SEC")]
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
        [Given(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        [When(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        [Then(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        public void GivenISetValueInContextFromDataAs(string value, string attribute)
        {
            if (attribute == "ConsumerNo")
            {
                context.SetConsumer_No(value);
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
        }

        [When(@"I sleep (.*)")]
        public void WhenISleep(int count)
        {
            Thread.Sleep(count);
        }
        [Given(@"select value from database by ""(.*)""")]
        public void GivenSelectValueFromDatabaseBy(string query)
        {
            if (query != "")
            {
                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                string Bene_AccountNo = SourceDataTable.Rows[0][0].ToString();
                context.SetBeneAccountNo(Bene_AccountNo);
            }
        }

        [Given(@"I wait (.*)")]
        [When(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
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

                if (SourceDataTable.Rows.Count == 0 || (bene_name == "1"))
                {
                    SeleniumHelper selhelper = new SeleniumHelper();
                    Element keyword = ContextPage.GetInstance().GetElement("Pay_Bene_checkbox");
                    selhelper.links(keyword.Locator);

                    Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Bene_text");
                    selhelper.SetTextBoxValue(textboxvalue, keyword2.Locator);

                    Element keyword3 = ContextPage.GetInstance().GetElement("Pay_Bene_button");
                    selhelper.Button(keyword3.Locator);
                }
            }
        }



        [Then(@"verify through ""(.*)"" on ""(.*)""")]
        public void ThenVerifyThroughOn(string message, string Keyword)
        {
            try
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                //selhelper.Scroll(keyword.Locator);
                if (message == "ConsumerNoContextVal")
                {
                    message = context.GetConsumer_No();
                }
                selhelper.verification(message, keyword.Locator);
                if (Keyword.Contains("Pay_Transaction_Success") || Keyword.Contains("SendMoney_TranSuccessMessage"))
                {
                    keyword = null;
                    string tranid_keyword = "Pay_Transaction_ID";
                    keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                    string tran_id = selhelper.ReturnKeywordValue(keyword.Locator);
                    context.SetTransaction_Id(tran_id);
                }

                //        try
                //        {
                //            SeleniumHelper selhelper1 = new SeleniumHelper();
                //            selhelper1.checkPageIsReady();
                //            Thread.Sleep(3000);
                //            selhelper.verification(message, keyword.Locator);

                //        }
                //        catch (Exception exception)
                //        {
                //            SeleniumHelper.TakeScreenshot();
                //            throw new AssertFailedException(exception.Message);
                //        }
                ////context.SetPage(selhelper);
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
            if (query.Contains("DC_TRANSACTION"))
            {
                if (Keyword.Contains("SendMoney_TranToBank") || Keyword.Contains("SendMoney_TranType"))
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
                SeleniumHelper selhelper = new SeleniumHelper();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                if (query != "")
                {
                    if (query.Contains("{ConsumerNo}"))
                    {
                        query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                        //query = a;
                    }
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                    message = SourceDataTable.Rows[0][0].ToString();
                    if (Keyword.Equals("Pay_Transaction_Success_Amount") || Keyword.Equals("Pay_Transaction_Unpaid_Amount"))
                    {
                        message = Convert.ToDecimal(message).ToString("0.00");
                    }
                    if (Keyword.Equals("Pay_Transaction_Success_ConsumerNo"))
                    {
                        context.SetConsumer_No(message);
                    }
                }
                //selhelper.Scroll(keyword.Locator);


                selhelper.verification(message, keyword.Locator);
            }
            catch (Exception exception)
            {

                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I scroll down ""(.*)"" on ""(.*)""")]
        public void WhenIScrollDownOn(int count, string Keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            //selhelper.checkPageIsReady();
            Thread.Sleep(3000);
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            selhelper.ScrollDown(count, keyword.Locator);
        }

        [When(@"I press Enter on ""(.*)""")]
        public void WhenIPressEnterOn(string Keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            //selhelper.checkPageIsReady();
            Thread.Sleep(3000);
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            selhelper.PressEnter(keyword.Locator);
        }


        [When(@"I scroll to element ""(.*)""")]
        public void WhenIScrollToElement(string Keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            //selhelper.checkPageIsReady();
            Thread.Sleep(3000);
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            selhelper.ScrollToElement(keyword.Locator);
        }

        [When(@"I have otp check and given (.*) on ""(.*)"" on company code (.*)")]
        public void WhenIHaveOtpCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword, string company_code_value)
        {
            string query = "SELECT CC.IS_OTP_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = '" + company_code_value + "' AND CC.CHANNEL_CODE = 'MB'";
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, "QAT_BPS");
            string is_otp_req = SourceDataTable.Rows[0][0].ToString();
            if (is_otp_req == "1")
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                selhelper.SetTextBoxValue(otp_value, keyword.Locator);
            }
        }

        [When(@"I have transaction pass check and given (.*) on ""(.*)"" on company code (.*)")]
        public void WhenIHaveTransactionPassCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword, string company_code_value)
        {
            string query = "SELECT CC.IS_PWD_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = '" + company_code_value + "' AND CC.CHANNEL_CODE = 'MB'";
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, "QAT_BPS");
            string is_tran_req = SourceDataTable.Rows[0][0].ToString();
            if (is_tran_req == "1")
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                selhelper.SetTextBoxValue(otp_value, keyword.Locator);
            }
        }

        [When(@"I want value from textbox ""(.*)"" on database ""(.*)"" as ""(.*)""")]
        public void WhenIWantValueFromTextboxOnDatabaseAs(string Keyword, string db_value, string query)
        {
            //bool due_date = false;
            string value = "";
            string value2 = "";
            SeleniumHelper selhelper = new SeleniumHelper();
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

            string keyword_value = selhelper.ReturnTextBoxValue(keyword.Locator);
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

        [When(@"I select date ""(.*)"" on month ""(.*)"" on year ""(.*)""")]
        public void WhenISelectDateOnMonthOnYear(string date, string month, string year)
        {
            if(date == "" && month == "" && year == "")
            {
                if(context.Getfrom_to_date_flag() == false)
                {
                    string temp = DateTime.Now.Date.AddDays(1).ToString("dd MMM yyyy");
                    date = temp.Substring(0, 2);
                    month = temp.Substring(3, 3);
                    year = temp.Substring(7, 4);
                    if (Convert.ToInt32(date) < 10)
                    {
                        date = date.Substring(1,1);
                    }
                }
                else if (context.Getfrom_to_date_flag() == true)
                {
                    string temp = DateTime.Now.Date.AddDays(30).ToString("dd MMM yyyy");
                    date = temp.Substring(0, 2);
                    month = temp.Substring(3, 3);
                    year = temp.Substring(7, 4);
                    if (Convert.ToInt32(date) < 10)
                    {
                        date = date.Substring(1, 1);
                    }
                }
            }
            string keyword_date = "//a[contains(text(), ";
            string keyword_month = "//select[@class='ui-datepicker-month']";
            string keyword_year = "//select[@class='ui-datepicker-year']";
            SeleniumHelper selhelper = new SeleniumHelper();
            selhelper.checkPageIsReady();
            //Element keyword = ContextPage.GetInstance().GetElement(keyword_month);
            selhelper.combobox(month, keyword_month);
            //keyword = null; 
            //keyword = ContextPage.GetInstance().GetElement(keyword_year);
            selhelper.combobox(year, keyword_year);
            //keyword = null;
            keyword_date = keyword_date + "'" + date + "')]";
            //keyword = ContextPage.GetInstance().GetElement(keyword_year);
            selhelper.links(keyword_date);
            if (Convert.ToInt32(date) < 10)
            {
                date = ("0" + date);
            }
            string complete_date = date + " " + month + " " + year;
            DateTime temp_date = DateTime.ParseExact(complete_date, "dd MMM yyyy", CultureInfo.InvariantCulture);
            context.Settempdate(temp_date);
        }
        [When(@"I set calendar from date")]
        public void WhenISetCalendarFromDate()
        {
            context.Setcalendar_fromdate(context.Gettempdate());
        }
        [When(@"I set calendar to date")]
        public void WhenISetCalendarToDate()
        {
            context.Setcalendar_todate(context.Gettempdate());
        }
        [When(@"Set parameter in context class ""(.*)""")]
        public void WhenSetParameterInContextClass(string Keyword)
        {
            if (Keyword.Equals("Pay_Bill_Status"))
            {
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                SeleniumHelper selhelper = new SeleniumHelper();
                selhelper.checkPageIsReady();
                string bill_status = selhelper.ReturnKeywordValue(keyword.Locator);
                context.SetBill_Status(bill_status);
            }
        }

        [When(@"verify color of bill status on ""(.*)""")]
        public void WhenVerifyColorOfBillStatusOn(string Keyword)
        {
            string color_code = "";
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            SeleniumHelper selhelper = new SeleniumHelper();
            selhelper.checkPageIsReady();
            string color_code_web = selhelper.ReturnBackgroundColorKeywordValue(keyword.Locator);
            if (context.GetBill_Status() == "Unpaid")
            {
                color_code = "b75858";
            }
            else if (context.GetBill_Status() == "Paid")
            {
                color_code = "green";
            }
            else if (context.GetBill_Status() == "Blocked")
            {
                color_code = "b75858";
            }
            if (color_code.ToLower() != color_code_web.ToLower())
            {
                throw new AssertFailedException(string.Format("The Color Code against keyword is: {0} and color code given by user is {1}", color_code_web, color_code));
            }
        }

        [When(@"I am verifying list of execution iterations on ""(.*)""")]
        public void WhenIAmVerifyingListOfExecutionIterationsOn(string Keyword)
        {
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            SeleniumHelper selhelper = new SeleniumHelper();
            selhelper.checkPageIsReady();
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
            int diff = Convert.ToInt32(difference) + 1;
            int counter = 1;
            for (int i = 0; i < diff * loop_increment_counter; i += loop_increment_counter)
            {
                DateTime temp = (st_date.Date.AddDays(i));
                temp = temp.Date;
                lst.Add(temp.ToString("dd MMM yyyy"));
                string locator = keyword.Locator.Replace("{Iteration_Number}", counter.ToString());
                lstui.Add(selhelper.ReturnKeywordValue(locator));
                if(lst[counter - 1] != lstui[counter - 1])
                {
                    throw new AssertFailedException(string.Format("The Iteration Date against keyword is: {0} and Iteration Date calculated by code is {1}", lstui[counter], lst[counter]));
                }
                counter++;
            }
            //for (int i = 1; i <= loop_counter; i += loop_increment_counter)
            //{

            //}
        }
        


        [Given(@"I save Account Balances")]
        
        public void GivenISaveAccountBalances()
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //string[,] arr = new string[,] { };
            int count = 0;
            int x = context.GeTSizeCount();
            string acc_no = "";
            string acc_bal = "";
            for (int i=0; i < x; i++)
            {
               
                for (int j = 0; j < 2; j++)
                {
                    int temp_counter = count + 1;
                    if (j == 0)
                    {                                              
                        Element keyword = ContextPage.GetInstance().GetElement("Pay_Acc_No");
                        string temp = keyword.Locator.Replace("{i}", "[" + temp_counter.ToString() + "]");                                              

                        string text = selhelper.ReturnKeywordValue(temp);
                        string[] split = text.Split('|');
                        acc_no = split[0].Trim();

                    }
                    else if (j == 1)
                    {
                        Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Acc_Balance");
                        string temp2 = keyword2.Locator.Replace("{j}", "[" + temp_counter.ToString() + "]");
                        

                        acc_bal = selhelper.ReturnKeywordValue(temp2);                        
                    }

                }
                dict.Add(acc_no, acc_bal);
                count++;
            }
            context.Set_acc_balances(dict);
            
            
        }
        [When(@"I save Transaction Info")]
        public void WhenISaveTransactionInfo()
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Dictionary<string, string> tran_dict = new Dictionary<string, string>();
            Element keyword = ContextPage.GetInstance().GetElement("Pay_Transaction_Success_FromAccount");
            string tran_account = selhelper.ReturnKeywordValue(keyword.Locator);
            context.SetTran_Account(tran_account);

            Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Transaction_Success_Amount");
            string tran_balance = selhelper.ReturnKeywordValue(keyword2.Locator);

            //tran_dict.Add(tran_account, tran_balance);

            //Dictionary<string, string> old_dict = ;
            tran_dict = context.Get_acc_balance();

            foreach (var item in tran_dict)
            {
                if (tran_account == item.Key)
                {
                    decimal tran_balancee = Convert.ToDecimal(item.Value) - Convert.ToDecimal(tran_balance);
                    context.SetTran_Balance(tran_balancee);
                }
              
            }

        }

        [Then(@"I verify Account Balance")]
        public void ThenIVerifyAccountBalance()
        {
            try
            {


                SeleniumHelper selhelper = new SeleniumHelper();
                Element keyword = ContextPage.GetInstance().GetElement("Pay_Final_Account_Balance");
                string tAccountNo = context.GeTran_Account();
                string temp = keyword.Locator.Replace("{k}", tAccountNo);
                string new_account_bal = selhelper.ReturnKeywordValue(temp);

                decimal old_account_bal = context.GetTran_Balance();



                if (Convert.ToDecimal(new_account_bal) != old_account_bal)
                {
                    throw new AssertFailedException(string.Format("The old account balance {0} is not equal to new account balance {1} after successfull transaction", old_account_bal, new_account_bal));
                }
            }
            catch (Exception exception)
            {

                throw new AssertFailedException(exception.Message);
            }


        }

        [Given(@"I count Number of Account")]
        public void GivenICountNumberOfAccount()
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element keyword = ContextPage.GetInstance().GetElement("Pay_Account_No-Count");
            int size = selhelper.SizeCountElements(keyword.Locator);
            context.SetSizeCount(size);

        }

    }

}
