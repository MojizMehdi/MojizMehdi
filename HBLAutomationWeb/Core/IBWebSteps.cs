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
using System.Linq;

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

            if (Keyword.Contains("Login_OTP_field"))
            {
                SeleniumHelper selhelper = new SeleniumHelper();

                textboxvalue = selhelper.GetOTP();

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

            if (Keyword.Contains("Signup_FeedbackText"))
            {
                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable("Select ATTRIBUTE_1 from DC_CUSTOMER_REG_FEEDBACK a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info j where J.CNIC='" + context.GetCustomerCNIC() + "') ", "DIGITAL_CHANNEL_SEC");
                string message = SourceDataTable.Rows[0][0].ToString();

                Assert.AreEqual(textboxvalue, message);
            }
            if (Keyword.Contains("Signup_TransactionPassword"))
            {
                context.SetTranPassFlag(true);
            }
            if (context.GetLastLoginPassFlag() == true)
            {
                context.SetLastLoginFlag(true);
            }
        }
        [Given(@"I am performing on ""(.*)""")]
        [When(@"I am performing on ""(.*)""")]
        [Then(@"I am performing on ""(.*)""")]
        public void WhenIAmPerformingOn(string Keyword)
        {
            if (Keyword.Contains("Forget_"))
            {
                if (context.GetCustomerType() == "C" && Keyword.Equals("Forget_PasswordSubmitBtn"))
                {
                    Keyword.Replace("Forget_PasswordSubmitBtn", "PasswordSubmitBtnCC");
                }
                if (context.GetCustomerType() == "C" && Keyword.Equals("Forget_ChangeLogin_SubmitBtn"))
                {
                    Keyword.Replace("Forget_ChangeLogin_SubmitBtn", "Forget_ChangeLogin_SubmitBtnCC");
                }
            }
            try
            {
                if (Keyword.Equals("Pay_BillPayment_Inquiry_NextBtn"))
                {
                    if (context.GetOTPReq() == "1" || context.GetTranPassReq() == "1")
                    {
                        SeleniumHelper selhelper = new SeleniumHelper();
                        selhelper.checkPageIsReady();
                        Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                        selhelper.Button(keyword.Locator);
                    }
                }
                else
                {
                    SeleniumHelper selhelper = new SeleniumHelper();
                    selhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    selhelper.Button(keyword.Locator);
                }
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
                SeleniumHelper selhelper = new SeleniumHelper();
                selhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);

                if (Keyword.Contains("Services_Date"))
                {


                    if (context.GetTranFromDateFlag() == false || context.GetTranToDateFlag() == false)
                    {
                        return;
                    }
                }

                if (Keyword == "Pay_Transaction_PayBill_Rating" || Keyword == "Pay_Transaction_PayBill_RatingOkBtn")
                {
                    selhelper.rating(keyword.Locator);
                    if (context.GetRatingCheck() == true)
                    {
                        string query = "Select PARAM_ANSWER_ID from DC_CUSTOMER_REG_FEEDBACK  i where I.CUSTOMER_INFO_ID='1{customer_info_id}'";
                        query = query.Replace("{customer_info_id}", context.GetCustomerInfoID());
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                        string rating_ans = SourceDataTable.Rows[0][0].ToString();

                        if (rating_ans != "5")
                        {
                            throw new AssertFailedException("Rating verification Failed, Given and DB Value is not equal");

                        }
                    }
                }
                if (Keyword.Equals("Signup_AccountToggle"))
                {
                    List<string> AccList = new List<string>();
                    AccList = context.GetAccountForTag();
                    foreach (var acc_no in AccList)
                    {
                        string locator = keyword.Locator.Replace("{i}", acc_no);
                        selhelper.links(locator);
                    }

                }
                else if (!String.IsNullOrEmpty(Keyword))
                {
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
            if (count == 1)
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

        [When(@"I select on dropdown search ""(.*)"" to select ""(.*)"" on ""(.*)""")]
        public void WhenISelectOnDropdownSearchToSelectOn(string Keyword1, string value, string Keyword2)
        {
            int count = context.GeTSizeCount();
            if (count == 1)
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
                Element keyword1 = ContextPage.GetInstance().GetElement(Keyword1);
                Element keyword2 = ContextPage.GetInstance().GetElement(Keyword2);
                selhelper.comboboxSearch(value, keyword1.Locator, keyword2.Locator);
            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


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
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                if (query.Contains("{customer_info_id}"))
                {
                    query = query.Replace("{customer_info_id}", context.GetCustomerInfoID());
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
                if (query.Contains("LOGIN_PASSWORD")) ;
                {
                    context.SetLastLoginPassFlag(true);
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
            if (attribute == "Transaction_Type")
            {
                context.SetTranType(value);
            }
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
            if (attribute == "scroll_text")
            {
                context.SetScrollText(value);
            }
            if (attribute == "customer_cnic")
            {
                context.SetCustomerCNIC(value);
            }
            if (attribute == "AccountForTag")
            {
                List<string> lst = new List<string>();
                if (value.Contains(","))
                {
                    string[] delimiters = { "," };
                    string[] pieces = value.Split(delimiters, StringSplitOptions.None);
                    lst.AddRange(pieces);
                }
                else
                {
                    lst.Add(value.ToString());
                }
                context.SetAccountForTag(lst);
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
        [Then(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
        }

        [When(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        [Given(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheResultFromOnSchema(string query, string db_value)
        {
            if (query != "")
            {
                if (query.Contains("{customer_info_id}"))
                {
                    query = query.Replace("{customer_info_id}", context.GetCustomerInfoID());
                }
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }

                if (query.Contains("Company_Code"))
                {
                    query = query.Replace("{Company_Code}", context.GetCompany_Code());
                }

                Thread.Sleep(2000);
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
                string inst_type = SourceDataTable.Rows[0][0].ToString();

                if (query.Contains("IVR_REQUIRED"))
                {
                    context.SetIVRReq(inst_type);
                }
                if (query.Contains("IS_IVR_ENABLED"))
                {
                    if ((context.GetIVRReq() == "1" && inst_type == "0"))
                    {
                        return;
                    }
                    else if (context.GetIVRReq() == "0" && inst_type == "1")
                    {
                        return;
                    }
                    //if ((context.GetIVRReq() != "1" && inst_type != "0") || (context.GetIVRReq() != "0" && inst_type != "1"))
                    //{
                    //    throw new AssertFailedException("IS_IVR_ENABLED setting is not correct");
                    //}
                }
                if (query.Contains("ENABLE_PSD_BIOMETRIC"))
                {
                    context.SetEnablePSD(inst_type);
                }
                if (query.Contains("Z.ENABLE_PSD "))
                {
                    if ((context.GetEnablePSD() != "1" && inst_type != "1") || (context.GetEnablePSD() != "0" && inst_type != "0"))
                    {
                        throw new AssertFailedException("ENABLE_PSD setting is not correct");
                    }
                }
                if (query.Contains("CUSTOMER_TYPE"))
                {
                    context.SetCustomerType(inst_type);
                }
                if (query.Contains("IS_PASSWORD_CHANGED_REQUIRED"))
                {
                    if (inst_type != "0")
                    {
                        throw new AssertFailedException("Is Password change is not equal to 0");
                    }
                }
                if (query.Contains("TRANSACTION_PASSWORD"))
                {
                    if (inst_type == "")
                    {
                        if (context.GetTranPassFlag() == true)
                        {
                            throw new AssertFailedException("Transaction Password is not updated in data base");
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (inst_type != "")
                    {
                        if (context.GetTranPassFlag() == false)
                        {
                            throw new AssertFailedException("Transaction Password is not updated in data base");
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                if (query.Contains("last_login"))
                {
                    if (inst_type == "")
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
                    if (inst_type != "")
                    {
                        if (context.GetLastLoginFlag() == false)
                        {
                            throw new AssertFailedException("Transaction Password is not updated in data base");
                        }
                        else
                        {
                            DateTime lastlogin = Convert.ToDateTime(inst_type);
                            inst_type = lastlogin.ToString("MM/dd/yyyy");
                            string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                            Assert.AreEqual(inst_type, today_date);
                        }
                    }
                    if (query.Contains("created_on"))
                    {
                        if (inst_type != null)
                        {
                            DateTime lastlogin = Convert.ToDateTime(inst_type);
                            inst_type = lastlogin.ToString("MM/dd/yyyy");
                            string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                            Assert.AreEqual(inst_type, today_date);
                        }
                        else
                        {
                            throw new AssertFailedException("Created On is not updated in data base");
                        }
                    }
                    if (query.Contains("updated_on"))
                    {
                        if (inst_type != null)
                        {
                            DateTime lastlogin = Convert.ToDateTime(inst_type);
                            inst_type = lastlogin.ToString("MM/dd/yyyy");
                            string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                            Assert.AreEqual(inst_type, today_date);
                        }
                        else
                        {
                            throw new AssertFailedException("Updated On is not updated in data base");
                        }
                    }
                }

                if (query.Contains("APPLICATION_PARAMETER_ID='906'"))
                {
                    context.SetScheduleConfig(inst_type);
                }

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
                if (query.Contains("PARAM_CHANNEL_ID"))
                {
                    if (inst_type != "2")
                    {
                        throw new AssertFailedException(string.Format("The PARAM_CHANNEL_ID :{0} is not valid for HBL Web Internet Banking", inst_type));
                    }
                }
                if (query.Contains("LOGIN_PSWD_POLICY_DESC"))
                {
                    string[] delimiters = { "<br>" };
                    string[] pieces = inst_type.Split(delimiters, StringSplitOptions.None);
                    context.SetPassPolicy1(pieces[0]);
                    context.SetPassPolicy2(pieces[1]);
                    context.SetPassPolicy3(pieces[2]);
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

                if (bene_name == "1" && context.GetBeneName() != "")
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


        [Given(@"verify through ""(.*)"" on ""(.*)""")]
        [Then(@"verify through ""(.*)"" on ""(.*)""")]
        [When(@"verify through ""(.*)"" on ""(.*)""")]
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
                if (message == "Signup_PassPolicy")
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        string temp = "";
                        if (i == 1)
                        {
                            temp = context.GetPassPolicy1();
                        }
                        else if (i == 2)
                        {
                            temp = context.GetPassPolicy2();
                        }
                        else if (i == 3)
                        {
                            temp = context.GetPassPolicy3();
                        }
                        string loc = keyword.Locator.Replace("[i]", "[" + (i) + "]");
                        selhelper.verification(temp, keyword.Locator);
                    }
                    return;
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

            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"verify the message ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        [Given(@"verify the message ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the message ""(.*)"" through database on ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheMessageThroughDatabaseOnOn(string value, string query, string schema)
        {
            if (query.Contains("{customer_cnic}"))
            {
                query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
            }
            try
            {
                if (query.Contains("K.IS_ACTIVE"))
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
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

                if (query.Contains("K.IS_ACCOUNT_LINK"))
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);

                    List<string> db_account_list = new List<string>();

                    List<string> account_list = new List<string>();
                    account_list = context.GetAccountForTag();
                    int count = account_list.Count;

                    for (int i = 0; i < count; i++)
                    {
                        string message = SourceDataTable.Rows[i][0].ToString();
                        db_account_list.Add(message);
                    }
                    if (db_account_list.Count != account_list.Count)
                    {
                        throw new AssertFailedException(string.Format("Account Numbers which were tagged during Sign up:{0} are not the same as in Database:{1}", account_list, db_account_list));
                    }
                }
                else
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
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


        [When(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        [Then(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        public void ThenVerifyThroughDatabaseOnOnSchemaOn(string query, string schema, string Keyword)
        {
            if (query.Contains("{customer_cnic}"))
            {
                query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
            }
            if (query.Contains("DC_TRANSACTION"))
            {
                if (Keyword.Contains("SendMoney_TranToBank") || Keyword.Contains("SendMoney_TranType") || Keyword.Contains("Forget_PasswordTranType"))
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
                    if (keyword.Equals("Forget_PasswordTranID"))
                    {
                        context.SetTransaction_Id(message);
                    }
                    if (Keyword.Equals("Pay_Transaction_Success_Amount") || Keyword.Equals("Pay_Transaction_Unpaid_Amount"))
                    {
                        message = Convert.ToDecimal(message).ToString("0.00");
                    }
                    if (Keyword.Equals("Pay_Transaction_Success_ConsumerNo"))
                    {
                        context.SetConsumer_No(message);
                    }
                    if (Keyword.Contains("TranDate"))
                    {
                        DateTime tran_date = Convert.ToDateTime(message);
                        message = tran_date.ToString("MM/dd/yyyy");
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
        [Given(@"I scroll down ""(.*)"" on ""(.*)""")]
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
            if (Keyword.Contains("Signup_Scroll"))
            {
                keyword.Locator = keyword.Locator.Replace("{K}", context.GetScrollText());
            }
            selhelper.ScrollToElement(keyword.Locator);
        }

        [When(@"I have otp check and given (.*) on ""(.*)""")]
        public void WhenIHaveOtpCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword)
        {

            if (context.GetOTPReq() == "1")
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                otp_value = selhelper.GetOTP();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                selhelper.SetTextBoxValue(otp_value, keyword.Locator);


            }
        }

        [When(@"I have transaction pass check and given (.*) on ""(.*)""")]
        public void WhenIHaveTransactionPassCheckAndGivenOnOnCompanyCode(string otp_value, string Keyword)
        {

            if (context.GetTranPassReq() == "1")
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                //selhelper.checkPageIsReady();
                Thread.Sleep(3000);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                selhelper.SetTextBoxValue(otp_value, keyword.Locator);
            }
        }
        [When(@"I am verifying OTP and Transaction pass check on company code (.*)")]
        public void WhenIAmVerifyingOTPAndTransactionPassCheckOnCompanyCode(string company_code_value)
        {
            string query = "SELECT CC.IS_PWD_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = '" + company_code_value + "' AND CC.CHANNEL_CODE = 'MB'";
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, "QAT_BPS");
            string is_tran_req = SourceDataTable.Rows[0][0].ToString();
            context.SetTranPassReq(is_tran_req);

            string query2 = "SELECT CC.IS_OTP_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = '" + company_code_value + "' AND CC.CHANNEL_CODE = 'MB'";
            DataAccessComponent.DataAccessLink dlink2 = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable2 = dlink2.GetDataTable(query2, "QAT_BPS");
            string is_otp_req = SourceDataTable2.Rows[0][0].ToString();
            context.SetOTPReq(is_otp_req);
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
            if (date == "" && month == "" && year == "")
            {
                if (context.GetTranFromDateFlag() == false || context.GetTranToDateFlag() == false)
                {
                    return;
                }

                if (context.Getfrom_to_date_flag() == false)
                {
                    string temp = DateTime.Now.Date.AddDays(1).ToString("dd MMM yyyy");
                    date = temp.Substring(0, 2);
                    month = temp.Substring(3, 3);
                    year = temp.Substring(7, 4);
                    if (Convert.ToInt32(date) < 10)
                    {
                        date = date.Substring(1, 1);
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
                if (lst[counter - 1] != lstui[counter - 1])
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
            int count = 0;
            int x = context.GeTSizeCount();
            string acc_no = "";
            string acc_bal = "";
            for (int i = 0; i < x; i++)
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
            string[] consumer_no_arr = context.Get_multi_bill_consumers();

            SeleniumHelper selhelper = new SeleniumHelper();
            Dictionary<string, string> tran_dict = new Dictionary<string, string>();

            if (consumer_no_arr.Length > 0)
            {
                Element keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_FromAccount");

                string tran_account = selhelper.ReturnKeywordValue(keyword.Locator);
                context.SetTran_Account(tran_account);

                int tran_balance = context.Get_multi_payment_amount();
                context.SetTran_Balance(Convert.ToDecimal(tran_balance));
            }
            else
            {
                Element keyword = ContextPage.GetInstance().GetElement("Pay_Transaction_Success_FromAccount");
                Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Transaction_Success_Amount");

                string tran_account = selhelper.ReturnKeywordValue(keyword.Locator);
                context.SetTran_Account(tran_account);

                string tran_balance = selhelper.ReturnKeywordValue(keyword2.Locator);
                context.SetTran_Balance(Convert.ToDecimal(tran_balance));
            }
            //tran_dict.Add(tran_account, tran_balance);

            //Dictionary<string, string> old_dict = ;
            tran_dict = context.Get_acc_balance();

            foreach (var item in tran_dict)
            {
                if (context.GeTran_Account() == item.Key)
                {
                    decimal tran_balancee = Convert.ToDecimal(item.Value) - context.GetTran_Balance();
                    context.SetTran_Balance(tran_balancee);
                    break;
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
            Element Keyword = ContextPage.GetInstance().GetElement("Pay_Account_No-Count");
            int size = selhelper.SizeCountElements(Keyword.Locator);
            context.SetSizeCount(size);
        }

        //To verify the schedule configuration from Data base 
        [Then(@"verify the schedule config ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheScheduleConfigOnSchema(string query, string db_value)
        {
            string query3 = "Select CUSTOMER_INFO_ID from dc_customer_info i where I.CUSTOMER_NAME='{usernmae}'";
            query3 = query3.Replace("{usernmae}", context.GetUsername());

            DataAccessComponent.DataAccessLink dLink3 = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable3 = dLink3.GetDataTable(query3, db_value);
            string customer_info_id = SourceDataTable3.Rows[0][0].ToString();
            context.SetCustomerInfoID(customer_info_id);

            query = query.Replace("{customer_info_id}", customer_info_id);

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

            if (res != Convert.ToInt32(context.GetScheduleConfig()))
            {
                throw new AssertFailedException(string.Format("The scheduled config {0} is not equal to newly scheduled bill {1}", res, context.GetScheduleConfig()));

            }
        }

        //To save all values from excel sheet in context class for Transaction Activity process
        [Given(@"I set all excel values ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)""  in context class")]
        public void GivenISetAllExcelValuesInContextClass(string Transaction_Category, string No_of_Transaction, string Tran_Type, string from_day, string from_month, string from_year, string to_day, string to_month, string to_year, string Min_Amount, string Max_Amount, string Acc_no_or_mobile, string bill_company, string payee_nick, string to_bank)
        {
            DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();

            if (to_bank != "")
            {
                DataTable SourceDataTable = dLink.GetDataTable("Select FUND_TRANSFER_BANK_ID from DC_FUND_TRANSFER_BANK i where i.bank_name= '" + to_bank + "'", "DIGITAL_CHANNEL_SEC");
                to_bank = SourceDataTable.Rows[0][0].ToString();
            }

            if (bill_company != "")
            {
                dLink = null;
                dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable("Select COMPANY_CODE from BPS_Company i where I.COMPANY_NAME = '" + bill_company + "'", "QAT_BPS");
                bill_company = SourceDataTable.Rows[0][0].ToString();
            }
            if (Transaction_Category == "Financial")
            {
                Transaction_Category = "F";
            }
            if (Transaction_Category == "Non Financial")
            {
                Transaction_Category = "NF";
            }

            context.SetTranCategory(Transaction_Category);
            context.SetNoOfTran(No_of_Transaction);
            context.SetTranType(Tran_Type);
            context.SetMinAmount(Min_Amount);
            context.SetMaxAmount(Max_Amount);
            context.SetAccNoMobile(Acc_no_or_mobile);
            context.SetBillCompany(bill_company);
            context.SetPayeeNick(payee_nick);
            context.SetToBank(to_bank);

            if (from_day != "" && from_month != "" && from_year != "")
            {
                context.SetTranFromDateFlag(true);
                string from_date = from_day + " " + from_month + " " + from_year;
                context.SetFromDate(from_date);
            }
            if (to_day != "" && to_month != "" && to_year != "")
            {
                context.SetTranToDateFlag(true);
                string to_date = to_day + " " + to_month + " " + to_year;
                context.SetToDate(to_date);
            }

        }

        //To make a generic query based on given value
        [When(@"I generate query based on given data")]
        [Given(@"I generate query based on given data")]
        [Then(@"I generate query based on given data")]
        public void WhenIGenerateQueryBasedOnGivenData()
        {
            string query = "SELECT CUSTOMER_INFO_ID FROM dc_customer_info i where I.CUSTOMER_NAME='{user}'";
            query = query.Replace("{user}", context.GetUsername());

            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
            string customer_info_id = SourceDataTable.Rows[0][0].ToString();
            context.SetCustomerInfoID(customer_info_id);

            string main_query = "select CREATED_ON, TRANSACTION_AMOUNT, STATUS from dc_transaction P where P.customer_info_id ='" + customer_info_id + "' and ";

            if (context.GetNoOfTran() != "")
            {
                main_query = main_query + "ROWNUM <= " + Convert.ToInt32(context.GetNoOfTran()) + " and ";
            }
            if (context.GetAccNoMobile() != "")
            {
                main_query = main_query + "P.TO_ACCOUNT like '%" + context.GetAccNoMobile() + "%' and ";
            }
            if (context.GetPayeeNick() != "")
            {
                main_query = main_query + "P.BENEFICIARY_NAME like '%" + context.GetPayeeNick() + "%' and ";
            }
            if (context.GetToBank() != "")
            {
                main_query = main_query + "P.FUND_TRANSFER_BANK_ID = '" + context.GetToBank() + "' and ";
            }
            if (context.GetBillCompany() != "")
            {
                main_query = main_query + "P.COMPANY_CODE = '" + context.GetBillCompany() + "' and ";
            }
            if (context.GetMinAmount() != "" && context.GetMaxAmount() == "")
            {
                main_query = main_query + "P.TRANSACTION_AMOUNT >= '" + context.GetMinAmount() + "' and ";
            }
            if (context.GetMinAmount() == "" && context.GetMaxAmount() != "")
            {
                main_query = main_query + "P.TRANSACTION_AMOUNT <= '" + context.GetMaxAmount() + "' and ";
            }
            if (context.GetMinAmount() != "" && context.GetMaxAmount() != "")
            {
                main_query = main_query + "P.TRANSACTION_AMOUN between '" + context.GetMinAmount() + "' and '" + context.GetMaxAmount() + "' and ";
            }
            if (context.GetFromDate() != "" && context.GetToDate() == "")
            {
                main_query = main_query + "P.CREATED_ON >=  TO_DATE('" + context.GetFromDate() + "', 'mm/dd/yyyy') and ";
            }
            if (context.GetFromDate() == "" && context.GetToDate() != "")
            {
                main_query = main_query + "P.CREATED_ON >=  TO_DATE('" + context.GetToDate() + "', 'mm/dd/yyyy') and ";
            }
            if (context.GetFromDate() != "" && context.GetToDate() != "")
            {
                main_query = main_query + "P.CREATED_ON between TO_DATE('" + context.GetFromDate() + "', 'mm/dd/yyyy') and TO_DATE ('" + context.GetToDate() + "', 'mm/dd/yyyy') and ";
            }

        }
        // Forgot Password journey based on customer type
        [When(@"I am giving user details based on customer type as ""(.*)"" and ""(.*)"" and ""(.*)"" and ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" and ""(.*)""")]
        public void WhenIAmGivingUserDetailsBasedOnCustomerTypeAsAndAndAndAndOnAndAnd(string value1, string debit_card, string debit_pin, string credit_card, string email, string keyword1, string keyword2, string keyword3)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element keyword_value1 = ContextPage.GetInstance().GetElement(keyword1);
            Element keyword_debit_credit = ContextPage.GetInstance().GetElement(keyword2);
            Element keyword_pin_email = ContextPage.GetInstance().GetElement(keyword3);

            string locator2 = keyword_debit_credit.Locator;
            string locator3 = keyword_pin_email.Locator;

            if (context.GetCustomerType() == "C" && keyword1 == "Forget_Password_CNIC")
            {
                locator2 = keyword_debit_credit.Locator.Replace("ForgotTransactionPassword.cardNumber", "ForgotTransactionPasswordCC.cardnumber");
                locator3 = keyword_pin_email.Locator.Replace("ForgotTransactionPassword.pin", "ForgotTransactionPasswordCC.email");
                selhelper.SetTextBoxValue(value1, keyword_value1.Locator);
                selhelper.SetTextBoxValue(credit_card, locator2);
                selhelper.SetTextBoxValue(email, locator3);
            }
            if (context.GetCustomerType() == "C" && keyword1 == "Forget_ChangeLogin_NewLogin")
            {
                locator2 = keyword_debit_credit.Locator.Replace("CustomerAccountType.cardNumber", "CustomerAccountType.creditCardNumber");
                locator3 = keyword_pin_email.Locator.Replace("CustomerAccountType.pin", "CustomerAccountType.email");
                selhelper.SetTextBoxValue(value1, keyword_value1.Locator);
                selhelper.SetTextBoxValue(credit_card, locator2);
                selhelper.SetTextBoxValue(email, locator3);
            }
            else
            {
                selhelper.SetTextBoxValue(value1, keyword_value1.Locator);
                selhelper.SetTextBoxValue(debit_card, keyword_debit_credit.Locator);
                selhelper.SetTextBoxValue(debit_pin, keyword_pin_email.Locator);
            }

        }
        [Given(@"I verify if text exist on webpage of ""(.*)""")]
        [When(@"I verify if text exist on webpage of ""(.*)""")]
        [Then(@"I verify if text exist on webpage of ""(.*)""")]
        public void GivenIVerifyIfTextExistOnWebpageOf(string keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element Keyword = ContextPage.GetInstance().GetElement(keyword);
            string keyword_text = "";
            keyword_text = selhelper.ReturnKeywordValue(Keyword.Locator);

            if (keyword_text == "")
            {
                throw new AssertFailedException("Urdu Text does not exist on web page");
            }
        }
        //To perform cancel operation on cross icon
        [Given(@"I am performing ""(.*)"" alert operation on cross icon on ""(.*)""")]
        public void GivenIAmPerformingAlertOperationOnCrossIconOn(string option, string keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element Keyword = ContextPage.GetInstance().GetElement(keyword);
            selhelper.AlertOperation(option, Keyword.Locator);
        }


        // To check dvl setting on sign up to decide for dob field
        [When(@"verify DVL setting through database on ""(.*)"" on Schema ""(.*)"" with date of birth ""(.*)"" on keyword ""(.*)""")]
        public void WhenVerifyDVLSettingThroughDatabaseOnOnSchemaWithDateOfBirthOnKeyword(string query, string schema, string value, string keyword)
        {
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, schema);
            string message = SourceDataTable.Rows[0][0].ToString();

            if (message == "1")
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                Element Keyword = ContextPage.GetInstance().GetElement(keyword);
                selhelper.SetTextBoxValue(value, Keyword.Locator);
            }
        }
        //For clicking on any generic xpath
        [Then(@"I am clicking on keyword ""(.*)"" with value ""(.*)""")]
        [Given(@"I am clicking on keyword """"(.*)""value""")]
        [When(@"I am clicking on keyword """"(.*)""value""")]
        public void ThenIAmClickingOnKeywordWithValue(string value, string keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element Keyword = ContextPage.GetInstance().GetElement(keyword);
            string locator = Keyword.Locator;

            if (keyword.Contains("FeedbackOption"))
            {
                locator = Keyword.Locator.Replace("{X}", value);
                selhelper.links(locator);

                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dlink.GetDataTable("Select FEEDBACK from DC_CUSTOMER_REG_FEEDBACK a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info j where J.CNIC='" + context.GetCustomerCNIC() + "') ", "DIGITAL_CHANNEL_SEC");
                string message = SourceDataTable.Rows[0][0].ToString();

                Assert.AreEqual(value, message);
            }
        }
        [When(@"I save Account Numbers")]
        public void WhenISaveAccountNumbers()
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element keyword = ContextPage.GetInstance().GetElement("Signup_TagAccountNo");

            List<string> lst = new List<string>();
            int count = 0;
            int x = context.GeTSizeCount();
            string acc_no = "";

            for (int i = 0; i < x; i++)
            {
                int temp_counter = count + 1;

                string temp = keyword.Locator.Replace("{i}", temp_counter.ToString());
                acc_no = selhelper.ReturnKeywordValue(temp);
                lst.Add(acc_no.ToString());
                count++;
            }

            context.SetAccNumbers(lst);
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
            SeleniumHelper selhelper = new SeleniumHelper();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            Thread.Sleep(3000);
            string[] consumer_no_arr = Consumer_No_List.Split(',');
            context.Set_multi_bill_consumers(consumer_no_arr);
            for (int i = 0; i < consumer_no_arr.Length; i++)
            {
                selhelper.SetTextBoxValue(consumer_no_arr[i], keyword.Locator);

                string query = "SELECT PB.BILL_BENE_NICK,PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CONSUMER_NUMBER = '" + consumer_no_arr[i] + "' AND PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')";
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
                Element Temp_keyword = ContextPage.GetInstance().GetElement("Pay_Multi_UIBeneNick");
                ui_bene_nick = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_bene_nick != BILL_BENE_NICK)
                {
                    throw new AssertFailedException(string.Format("The Bene Nick in database {0} is not equal to Bene Nick On Screen {1}", BILL_BENE_NICK, ui_bene_nick));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_Multi_UIAmount");
                ui_amount = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_amount = ui_amount.Replace("\r\nPKR\r\nUnpaid", string.Empty);
                if (ui_amount != amount)
                {
                    throw new AssertFailedException(string.Format("The Amount database {0} is not equal to Amount On Screen {1}", amount, ui_amount));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillDueDate");
                ui_duedate = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_duedate = ui_duedate.Replace(@"Due Date: ", string.Empty);
                if (ui_duedate != DUE_DATE)
                {
                    throw new AssertFailedException(string.Format("The DueDate in database {0} is not equal to DueDate On Screen {1}", DUE_DATE, ui_duedate));
                }
                Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Multi_ConsumerNoCheck");
                selhelper.links(keyword2.Locator);
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
            SeleniumHelper selhelper = new SeleniumHelper();
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            Element Temp_keyword;
            for (int i = 0; i < consumer_no_arr.Length; i++)
            {
                if (i <= consumer_no_arr.Length)
                {
                    Temp_keyword = null;
                    Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_NextBtn_Icon");
                    string tem_key = Temp_keyword.Locator.Replace("{a}", i.ToString());
                    selhelper.links(tem_key);
                }

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
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillCompanyName_Inquiry");
                ui_company_name = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                query = "SELECT CC.IS_PWD_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = (SELECT CC.COMPANY_CODE FROM BPS_COMPANY CC WHERE CC.COMPANY_NAME = '" + company_name + "') AND CC.CHANNEL_CODE = 'MB'";
                dLink = null;
                dLink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = null;
                SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                string tran_pass_req = SourceDataTable.Rows[0][0].ToString();
                if (tran_pass_req == "1")
                {
                    context.SetTranPassReq(tran_pass_req);
                }
                //SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                //BILL_STATUS = SourceDataTable.Rows[0][0].ToString();
                if (ui_company_name != company_name)
                {
                    throw new AssertFailedException(string.Format("The Company Name in database {0} is not equal to Company Name On Screen {1}", company_name, ui_company_name));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillConsumerName_Inquiry");
                ui_consumer_name = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_consumer_name = ui_consumer_name.Replace(@" ", string.Empty);
                if (ui_consumer_name != consumer_name)
                {
                    throw new AssertFailedException(string.Format("The Consumer Name in database {0} is not equal to ConsumerName On Screen {1}", consumer_name, ui_consumer_name));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillDueDate_Inquiry");
                ui_duedate = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_duedate != DUE_DATE)
                {
                    throw new AssertFailedException(string.Format("The Due Date in database {0} is not equal to Due Date On Screen {1}", DUE_DATE, ui_duedate));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillAmount_Inquiry");
                ui_amount_within_dd = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_amount_within_dd != amount_within_dd)
                {
                    throw new AssertFailedException(string.Format("The Amount With In Due Date in database {0} is not equal to Amount With In Due Date On Screen {1}", amount_within_dd, ui_amount_within_dd));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillAmountDD_inquiry");
                ui_amount_after_dd = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_amount_after_dd != amount_after_dd)
                {
                    throw new AssertFailedException(string.Format("The Amount After Due Date in database {0} is not equal to Amount After Due Date On Screen {1}", amount_after_dd, ui_amount_after_dd));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillStatus_Inquiry");
                ui_BILL_STATUS = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_BILL_STATUS != BILL_STATUS)
                {
                    throw new AssertFailedException(string.Format("The Bill Status in database {0} is not equal to Bill Status On Screen {1}", BILL_STATUS, ui_BILL_STATUS));
                }

            }
            Temp_keyword = null;
            Temp_keyword = ContextPage.GetInstance().GetElement("SendMoney_Amount");
            ui_amount = selhelper.ReturnTextBoxValue(Temp_keyword.Locator);
           // ui_amount = ui_amount.Replace(@".00", string.Empty);
            context.Set_multi_payment_amount(Convert.ToInt32(amount));
            if (Convert.ToDecimal(amount).ToString("0.00") != ui_amount)
            {
                throw new AssertFailedException(string.Format("The Total Amount Calculated {0} is not equal to Total Amount On Screen {1}", amount, ui_amount));
            }
        }
        [Then(@"verify multiple payments summary ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyMultiplePaymentsSummaryOnAndOnAndOnAndOnAndOnAndOnOnSchema(string TranSuccessMessage, string Keyword1, string tran_type_query, string Keyword2, string tran_amount_query, string Keyword3, string from_account_query, string Keyword4, string company_name_query, string Keyword5, string consumer_no_query, string Keyword6, string schema)
        {
            //string[] tran_id_arr = context.Get_multi_tran_ids();
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword1);
            SeleniumHelper selhelper = new SeleniumHelper();
            string tran_id = "";
            
            selhelper.verification(TranSuccessMessage, keyword.Locator);
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
            string temp_query = "";
            for (int i = 0; i < consumer_no_arr.Length; i++)
            { 
                keyword = null;
                    
                string tranid_keyword = "Pay_MultiBill_Success_TranID";
                keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                tran_id = selhelper.ReturnKeywordValue(keyword.Locator);
                
                for (int j = 0; j < queries.Length; j++)
                {
                    if (queries[j].Contains("DC_TRANSACTION"))
                    {
                        if (keywords[j].Contains("Pay_MultiBill_TranType"))
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
                        keyword = null;
                        keyword = ContextPage.GetInstance().GetElement(keywords[j]);
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
                            if (keywords[j].Equals("Pay_MultiBill_TranAmount"))
                            {
                                message = Convert.ToDecimal(message).ToString("0.00");
                            }
                            //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                            //{
                            //    consumer_no_arr_new[i] = message;
                            //}
                        }
                        selhelper.verification(message, keyword.Locator);
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
                    keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_NextArrow");
                    selhelper.links(keyword.Locator);
                }
            }
        }
        [Then(@"I verify multiple payment details in Transaction Activity ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" and ""(.*)"" on ""(.*)"" on Schema ""(.*)""")]
        public void ThenIVerifyMultiplePaymentDetailsInTransactionActivityOnAndOnAndOnAndOnAndOnAndOnOnSchema(string TranSuccessMessage, string Keyword1, string tran_type_query, string Keyword2, string tran_amount_query, string Keyword3, string from_account_query, string Keyword4, string company_name_query, string Keyword5, string consumer_no_query, string Keyword6, string schema)
        {
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            SeleniumHelper selhelper = new SeleniumHelper();

            string tran_id = "";
            int a = 1;

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
            string temp_query = "";

            for (int i = consumer_no_arr.Length - 1; i >=0; i--)
            {
                if (a <= consumer_no_arr.Length)
                {

                    Element temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_Services_row");
                    string temp_key = temp_keyword.Locator.Replace("{k}", Convert.ToString(a));
                    selhelper.links(temp_key);

                    string tranid_keyword = "Pay_MultiBill_SRV_TranID";
                    Element keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                    tran_id = selhelper.ReturnKeywordValue(keyword.Locator);

                    keyword = null;
                    keyword = ContextPage.GetInstance().GetElement(Keyword1);
                    selhelper.verification(TranSuccessMessage, keyword.Locator);

                    for (int j = 0; j < queries.Length; j++)
                    {
                        if (queries[j].Contains("DC_TRANSACTION"))
                        {
                            if (keywords[j].Contains("Pay_MultiBill_TranType") || keywords[j].Contains("Pay_MultiBill_SRV_TranType"))
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
                            keyword = null;
                            keyword = ContextPage.GetInstance().GetElement(keywords[j]);
                            if (queries[j] != "")
                            {
                                if (queries[j].Contains("{ConsumerNo}"))
                                {
                                    queries[j] = queries[j].Replace("{ConsumerNo}", consumer_no_arr[i]);
                                }
                                DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                                DataTable SourceDataTable = dlink.GetDataTable(queries[j], schema);
                                message = SourceDataTable.Rows[0][0].ToString();

                                if (keywords[j].Equals("Pay_MultiBill_TranAmount") || (keywords[j].Equals("Pay_MultiBill_SRV_TranAmount")))
                                {
                                    message = Convert.ToDecimal(message).ToString("0.00");
                                }
                                //if (Keyword.Equals("BillPayment_TranSucess_ConsumerNo"))
                                //{
                                //    consumer_no_arr_new[i] = message;
                                //}
                            }
                            selhelper.verification(message, keyword.Locator);
                            queries[j] = temp_query;

                        }

                        catch (Exception exception)
                        {

                            throw new AssertFailedException(exception.Message);
                        }
                    }
                    keyword = null;
                    keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_SRV_CloseBtn");
                    selhelper.Button(keyword.Locator);

                    a++;
                }
            }
        }

        //For linking or de-linking accounts from profile. 
        [Given(@"I select ""(.*)"" for Account linking or de-linking ""(.*)"" with success message as ""(.*)""")]
        public void GivenISelectForAccountLinkingOrDe_LinkingWithSuccessMessageAs(string accounts_no, string acc_operation_type, string success_message)
        {
            string username = context.GetUsername();
            string[] account_no_arr;
            SeleniumHelper selhelper = new SeleniumHelper();

            account_no_arr = accounts_no.Split(',');

            for (int i = 0; i < account_no_arr.Length; i++)
            {
                Element keyword = ContextPage.GetInstance().GetElement("MyAccount_LinkToggleBtn");
                string temp_keyword = keyword.Locator.Replace("{account_no}", account_no_arr[i]);
                selhelper.links(temp_keyword);
            }
            Element keyword2 = ContextPage.GetInstance().GetElement("MyAccount_LinkSubmitBtn");
            selhelper.Button(keyword2.Locator);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("Login_OTP_field");
            string textboxvalue = selhelper.GetOTP();
            selhelper.SetTextBoxValue(textboxvalue, keyword2.Locator);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("MyAccount_LinkVerifyBtn");
            selhelper.Button(keyword2.Locator);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("Pay_MultiBill_SRV_TranID");
            string tranID = selhelper.ReturnKeywordValue(keyword2.Locator);
            context.SetTransaction_Id(tranID);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("Pay_MultiBill_SRV_TranType");
            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable("SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = '" + tranID + "')", "DIGITAL_CHANNEL_SEC");
            string tran_type = SourceDataTable.Rows[0][0].ToString();
            selhelper.verification(tran_type, keyword2.Locator);
            keyword2 = null;

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict = context.Get_acc_balance();
            var account_list = dict.Keys.ToList();

            if (acc_operation_type == "delinking")
            {
                account_list.Except(account_no_arr.ToList());

                Element keyword3 = ContextPage.GetInstance().GetElement("MyAccount_Tran_DeLink");
                DataAccessComponent.DataAccessLink dlink2 = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable2 = dlink2.GetDataTable("SELECT LEAD_FIELD2 FROM DC_TRANSACTION AC where AC.TRANSACTION_ID ='" + tranID + "' And AC.CHANNEL_ID = '2'", "DIGITAL_CHANNEL_SEC");
                string de_link_accounts = SourceDataTable2.Rows[0][0].ToString();
                selhelper.verification(accounts_no, keyword3.Locator);
                selhelper.verification(accounts_no, de_link_accounts);
                dlink2 = null;
                SourceDataTable2 = null;
                for (int k = 0; k < account_no_arr.Length; k++)
                {
                    dlink2 = new DataAccessComponent.DataAccessLink();
                    SourceDataTable2 = dlink2.GetDataTable("select IS_ACCOUNT_LINK from dc_customer_account k where K.CUSTOMER_INFO_ID= (Select customer_info_id from dc_customer_info q where Q.CUSTOMER_NAME='" + context.GetUsername() + "' AND K.ACCOUNT_NO='" + account_no_arr[k] + "'", "DIGITAL_CHANNEL_SEC");
                    string is_account_link = SourceDataTable2.Rows[0][0].ToString();

                    selhelper.verification(is_account_link, "0");
                    keyword3 = null;

                    dlink2 = null;
                    SourceDataTable2 = null;
                    dlink2 = new DataAccessComponent.DataAccessLink();
                    SourceDataTable2 = dlink2.GetDataTable("select UPDATED_ON from dc_customer_account k where K.CUSTOMER_INFO_ID= (Select customer_info_id from dc_customer_info q where Q.CUSTOMER_NAME='" + context.GetUsername() + "' AND K.ACCOUNT_NO='" + account_no_arr[k] + "'", "DIGITAL_CHANNEL_SEC");
                    string updated_on = SourceDataTable2.Rows[0][0].ToString();

                    DateTime lastlogin = Convert.ToDateTime(updated_on);
                    updated_on = lastlogin.ToString("MM/dd/yyyy");
                    string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                    Assert.AreEqual(updated_on, today_date);
                }
            }
            else if (acc_operation_type == "linking")
            {
                account_list.AddRange(account_no_arr.ToList());

                Element keyword3 = ContextPage.GetInstance().GetElement("MyAccount_Tran_Link");
                DataAccessComponent.DataAccessLink dlink2 = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable2 = dlink2.GetDataTable("SELECT LEAD_FIELD1 FROM DC_TRANSACTION AC where AC.TRANSACTION_ID ='" + tranID + "' And AC.CHANNEL_ID = '2'", "DIGITAL_CHANNEL_SEC");
                string link_accounts = SourceDataTable2.Rows[0][0].ToString();
                selhelper.verification(link_accounts, accounts_no);
                selhelper.verification(accounts_no, keyword3.Locator);
                dlink2 = null;
                SourceDataTable2 = null;
                for (int k = 0; k < account_no_arr.Length; k++)
                {
                    dlink2 = new DataAccessComponent.DataAccessLink();
                    SourceDataTable2 = dlink2.GetDataTable("select IS_ACCOUNT_LINK from dc_customer_account k where K.CUSTOMER_INFO_ID= (Select customer_info_id from dc_customer_info q where Q.CUSTOMER_NAME='" + context.GetUsername() + "' AND K.ACCOUNT_NO='" + account_no_arr[k] + "'", "DIGITAL_CHANNEL_SEC");
                    string is_account_link = SourceDataTable2.Rows[0][0].ToString();

                    selhelper.verification(is_account_link, "1");
                    keyword3 = null;

                    dlink2 = null;
                    SourceDataTable2 = null;
                    dlink2 = new DataAccessComponent.DataAccessLink();
                    SourceDataTable2 = dlink2.GetDataTable("select UPDATED_ON from dc_customer_account k where K.CUSTOMER_INFO_ID= (Select customer_info_id from dc_customer_info q where Q.CUSTOMER_NAME='" + context.GetUsername() + "' AND K.ACCOUNT_NO='" + account_no_arr[k] + "'", "DIGITAL_CHANNEL_SEC");
                    string updated_on = SourceDataTable2.Rows[0][0].ToString();

                    DateTime lastlogin = Convert.ToDateTime(updated_on);
                    updated_on = lastlogin.ToString("MM/dd/yyyy");
                    string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                    Assert.AreEqual(updated_on, today_date);
                }
            }

            keyword2 = ContextPage.GetInstance().GetElement("MyAccount_TranPopUp_Text");
            selhelper.verification(success_message, keyword2.Locator);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("MyAccount_TranPopUp_CloseBtn");
            selhelper.links(keyword2.Locator);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("Login_Dashboard");
            selhelper.links(keyword2.Locator);
            keyword2 = null;

            keyword2 = ContextPage.GetInstance().GetElement("Pay_Account_No-Count");
            int size = selhelper.SizeCountElements(keyword2.Locator);

            string acc_no = "";
            List<string> new_account_list = new List<string>();

            for (int i = 0; i < size; i++)
            {

                Element keyword = ContextPage.GetInstance().GetElement("Pay_Acc_No");
                string temp = keyword.Locator.Replace("{i}", "[" + (i + 1).ToString() + "]");

                string text = selhelper.ReturnKeywordValue(temp);
                string[] split = text.Split('|');
                acc_no = split[0].Trim();

                new_account_list.Add(acc_no);
            }
            if (new_account_list != account_list)
            {
                throw new AssertFailedException(string.Format("The new Account list : {0} is not equal to old account list : {1}", new_account_list, account_list));
            }
        }

    }
}

