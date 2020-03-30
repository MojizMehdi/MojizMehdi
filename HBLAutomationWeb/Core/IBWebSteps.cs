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
            //if(textboxvalue == "ali")
            //{
            //    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            //    DataTable SourceDataTable = dlink.GetDataTable("SELECT CI.CUSTOMER_NAME FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_INFO_ID = '9483386'", "QADB");
            //    string CustomerName = SourceDataTable.Rows[0][0].ToString();

            //    dlink = new DataAccessComponent.DataAccessLink();
            //    SourceDataTable = dlink.GetDataTable("SELECT LB.COMPANY_CODE FROM LP_BILLS LB WHERE LB.CONSUMER_NO = '0400000263263' and LB.BILL_STATUS_ID=1 and LB.STAGING_ID='32551140'", "QAT_BPS");
            //    string Company = SourceDataTable.Rows[0][0].ToString();
            //}
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
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
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
                    string locator = "";
                    if (Keyword.Contains("BillPaymentCategory"))
                    {
                        locator = keyword.Locator.Replace("{BillPaymentCategory}", text);
                        keyword.Locator = locator;
                    }
                    if (Keyword.Contains("Pay_BillPaymentCategory_Company"))
                    {
                        locator = keyword.Locator.Replace("{Pay_BillPaymentCategory_Company}", text);
                        keyword.Locator = locator;
                    }
                    //Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    selhelper.links(keyword.Locator);
                }

            }

            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [Given(@"I select ""(.*)"" on ""(.*)""")]
        public void GivenISelectOn(string value, string Keyword)
        {
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
        public void GivenUpdateTheDataByQueryOnDIGITAL_CHANNEL_SEC(string query)
        {
            if (query != "")
            {
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

        [When(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
        }

        [Then(@"verify the result from (.*) on Schema ""(.*)""")]
        public void ThenVerifyTheResultFromOnSchema(string query, string db_value)
        {

            if (query != "")
            {
                Thread.Sleep(2000);
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
                if (SourceDataTable.Rows.Count == 0)
                {
                    throw new AssertFailedException(string.Format("there exists no record in database against query: {0}", query));
                };
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
                if (Keyword.Contains("Pay_Transaction_Success"))
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
                query = query + context.GetTransaction_Id() + "'";
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
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                    message = SourceDataTable.Rows[0][0].ToString();
                    if (Keyword.Equals("Pay_Transaction_Success_Amount"))
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
            bool due_date = false;
            string value = "";
            string value2 = "";
            SeleniumHelper selhelper = new SeleniumHelper();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);

            if (query != "")
            {
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
                    if(Convert.ToDateTime(value2) < DateTime.Today)
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


    }
}
