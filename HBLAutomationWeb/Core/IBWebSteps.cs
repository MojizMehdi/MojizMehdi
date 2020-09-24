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
            if (Keyword.Contains("Pay_Transaction_MaxBillAmount_value") && context.Get_IS_SI_Allowed() == "0")
            {
                return;
            }
            if (Keyword.Contains("Pay_Transaction_Unpaid_Amount") && context.Get_Is_Partial_Allow() == "0")
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
                    if (Keyword.Equals("Investment_MutualFund_PopupBtn") && context.GetFundDisclaimerPopup() == "")
                    {
                        return;
                    }
                    SeleniumHelper selhelper = new SeleniumHelper();
                    selhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    //selhelper.ScrollToElement(keyword.Locator);
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

                else if (Keyword == "Pay_Transaction_PayBill_Rating")
                {
                    selhelper.rating(keyword.Locator);
                }

                else if (Keyword.Contains("RatingOkBtn"))
                {
                    if (context.GetRatingCheck() == true)
                    {
                        selhelper.links(keyword.Locator);

                        string query = "Select PARAM_ANSWER_ID from DC_CUSTOMER_REG_FEEDBACK i where I.CUSTOMER_INFO_ID= (Select CUSTOMER_INFO_ID from dc_customer_info k where k.customer_name = '{customer_name}')";
                        query = query.Replace("{customer_name}", context.GetUsername());
                        DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                        DataTable SourceDataTable = dlink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                        string rating_ans = SourceDataTable.Rows[0][0].ToString();

                        if (rating_ans != "5")
                        {
                            throw new AssertFailedException("Rating verification Failed, Given and DB Value is not equal");
                        }
                    }
                }
                else if (Keyword.Equals("Registration_AccountToggle"))
                {
                    List<string> AccList = new List<string>();
                    AccList = context.GetAccountForTag();
                    foreach (var acc_no in AccList)
                    {
                        string locator = keyword.Locator.Replace("{i}", acc_no);
                        selhelper.links(locator);
                    }
                }
                else if (Keyword.Equals("MyAccount_CheqBook_BranchCheck"))
                {
                    if (context.GetHomeBranchDelFlag() != "")
                    {
                        selhelper.links(keyword.Locator);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (!String.IsNullOrEmpty(Keyword))
                {
                    if (Keyword.Contains("Investment_TermDep_Tenure"))
                    {
                        keyword.Locator = keyword.Locator.Replace("{Year}", context.Get_TermDepositYears());

                        string Keyword2 = keyword.Locator + "//parent::div//b[contains(text(),'Offered')]//following-sibling::span";
                        string off_rate = selhelper.ReturnKeywordValue(Keyword2);
                        context.SetOfferedRate(off_rate);
                    }
                    if (Keyword.Equals("BeneManage_Edit"))
                    {
                        keyword.Locator = keyword.Locator.Replace("{x}", context.GetBeneAccountNo());
                    }
                    if (Keyword.Contains("Investment_MutualFund_InvestBtn"))
                    {
                        keyword.Locator = keyword.Locator.Replace("{x}", context.GetInvestFundName());
                    }
                    if (Keyword.Contains("Pay_Transaction_ToggleAutoPay") || Keyword.Contains("Pay_Transaction_PayBillAmount_RadioBtn") || Keyword.Contains("Pay_Transaction_MaxBillAmount_RadioBtn") || Keyword.Contains("Pay_Transaction_PayBillAmount_NextBtn") || Keyword.Contains("Pay_Transaction_PayBillAmount_AgreeBtn"))
                    {
                        if (context.Get_IS_SI_Allowed() == "0")
                        {
                            return;
                        }
                    }
                    //selhelper.ScrollToElement(keyword.Locator);
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
        [Then(@"I select ""(.*)"" on ""(.*)""")]
        public void GivenISelectOn(string value, string Keyword)
        {
            if (Keyword.Contains("Acc_List") || Keyword.Contains("accountno"))
            {
                int count = context.GeTSizeCount();
                if (count == 1)
                {
                    return;
                }
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
            if (Keyword1.Contains("Acc_List"))
            {
                int count = context.GeTSizeCount();
                if (count == 1)
                {
                    return;
                }
            }
            if (String.IsNullOrEmpty(value))
            {
                if (Keyword1.Contains("MyAccount_CheqBook_CityList") || Keyword1.Contains("MyAccount_CheqBook_BranchList"))
                {
                    if (context.GetHomeBranchDelFlag() != "")
                    {
                        return;
                    }
                }
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
                if (query.Contains("{customer_name}"))
                {
                    query = query.Replace("{customer_name}", context.GetUsername());
                }
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
                if (query.Contains("{account_number}"))
                {
                    query = query.Replace("{account_number}", context.GetBeneAccountNo());
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
                if (query.Contains("LOGIN_PASSWORD"))
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
            if (attribute == "SignupCheck")
            {
                context.Set_signup_check(Convert.ToBoolean(value));
            }
            if (attribute == "invest_fund_name")
            {
                context.SetInvestFundName(value);
            }
            if (attribute == "TermDepositYears")
            {
                context.Set_TermDepositYears(value);
            }
            if (attribute == "term_deposit_flag")
            {
                context.Set_term_deposit_check(Convert.ToInt32(value));
            }
            if (attribute == "Bene_AccountNo")
            {
                context.SetBeneAccountNo(value);
            }
            if (attribute == "home_branch_del_flag")
            {
                context.SetHomeBranchDelFlag(value);
            }
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

                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable("SELECT CNIC FROM DC_CUSTOMER_INFO K WHERE K.CUSTOMER_NAME = '" + value.ToUpper() + "'", "DIGITAL_CHANNEL_SEC");
                string message = SourceDataTable.Rows[0][0].ToString();
                context.SetCustomerCNIC(message);
            }
            if (attribute == "scroll_text")
            {
                context.SetScrollText(value);
            }
            if (attribute == "customer_cnic")
            {
                context.SetCustomerCNIC(value);
            }
            if (attribute == "Credit_Card_check")
            {
                context.SetCredit_Card_Check(value);
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

        [Given(@"I wait (.*)")]
        [When(@"I wait (.*)")]
        [Then(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
        }

        [Given(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        [When(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        [Then(@"verify the result from ""(.*)"" on Schema ""(.*)""")]
        public void WhenVerifyTheResultFromOnSchema(string query, string db_value)
        {
            if (query != "")
            {
                if (query.Contains("{ConsumerNo}"))
                {
                    query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
                }
                if (query.Contains("{customer_name}"))
                {
                    query = query.Replace("{customer_name}", context.GetUsername());
                }
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
                if (query.Contains("{bene_account_no}"))
                {
                    query = query.Replace("{bene_account_no}", context.GetBeneAccountNo());
                }
            }

            Thread.Sleep(2000);
            DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dLink.GetDataTable(query, db_value);
            string inst_type = SourceDataTable.Rows[0][0].ToString();

            if(context.GetIsPaidReq() == "1")
            {
                if (inst_type != "2")
                {
                    throw new Exception("Bill Status ID in not equal to PAID in LP_BILLS table in Database");
                }
            }

            if (query.Contains("IVR_REQUIRED"))
            {
                context.SetIVRReq(inst_type);
            }
            if (query.Contains("IS_IVR_ENABLED"))
            {
                if ((context.GetIVRReq() == inst_type))
                {
                    throw new AssertFailedException("IVR setting is not correct");
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
                if ((context.GetEnablePSD() != inst_type))
                {
                    throw new AssertFailedException("ENABLE_PSD setting is not correct");
                }
            }
            if (query.Contains("CUSTOMER_TYPE"))
            {
                context.SetCustomerType(inst_type);
            }
            if (query.Contains("IS_PASSWORD_CHANGED_REQUIRED") || (query.Contains("IS_PASSWORD_RESET_REQUIRED") || query.Contains("LGN_PWD_CHANGED_POPUP_COUNT")))
            {
                if (inst_type != "0")
                {
                    throw new AssertFailedException("The value is not equal to 0");
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
                if (query.Contains("created_on") || (query.Contains("updated_on")) || (query.Contains("LAST_PASSWORD_CHANGED")) || (query.Contains("LAST_TRANS_PASSWORD_CHANGED")))
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
                        throw new AssertFailedException("Time is not updated in data base");
                    }
                }
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
            if ((query.Contains("LOGIN_PSWD_POLICY_DESC")) || (query.Contains("TXN_PSWD_POLICY_DESC")))
            {
                string[] delimiters = { "<br>" };
                string[] pieces = inst_type.Split(delimiters, StringSplitOptions.None);
                context.SetPassPolicy1(pieces[0]);
                context.SetPassPolicy2(pieces[1]);
                context.SetPassPolicy3(pieces[2]);
            }
            if (query.Contains("IS_DELETED"))
            {
                if (inst_type != "1")
                {
                    throw new AssertFailedException(string.Format("The recent deleted bene's IS DELETED property is not updated in datebase i.e. :{0}", inst_type));
                }
            }
        }



        [Given(@"verify bene status from (.*) on Schema ""(.*)""")]
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
                else if (message == "OfferedRateContextVal")
                {
                    message = context.GetOfferedRate();
                }
                else if (Keyword.Equals("Investment_TermDetAmount"))
                {
                    int temp = Convert.ToInt32(message);
                    message = temp.ToString("N0");
                }
                else if (Keyword.Equals("Investment_TermDetPeriod"))
                {
                    if (message.Contains("One")) { message.Replace("One", "1"); }
                    if (message.Contains("Two")) { message.Replace("Two", "2"); }
                    if (message.Contains("Three")) { message.Replace("Three", "3"); }
                    if (message.Contains("Five")) { message.Replace("Five", "5"); }
                    if (message.Contains("Ten")) { message.Replace("Ten", "10"); }

                }
                else if (message == "MyAccount_PassPolicy")
                {
                    string temp_1 = context.GetPassPolicy1().Trim();
                    selhelper.verification(temp_1, keyword.Locator);
                    for (int i = 2; i <= 3; i++)
                    {
                        string temp = "";
                        if (i == 2)
                        {
                            temp = context.GetPassPolicy2().Trim();
                        }
                        else if (i == 3)
                        {
                            temp = context.GetPassPolicy3().Trim();
                        }
                        if (Keyword.Equals("MyAccount_Forgot_UserPassPolicy1"))
                        {
                            Element keyword2 = ContextPage.GetInstance().GetElement("MyAccount_Forgot_UserPassPolicy2");
                            string temp_loc = keyword2.Locator.Replace("[i]", "[" + (i) + "]");
                            selhelper.verification(temp, temp_loc);
                            temp_loc = null;
                        }
                        else if (Keyword.Equals("MyAccount_Forgot_TranPassPolicy1"))
                        {
                            Element keyword2 = ContextPage.GetInstance().GetElement("MyAccount_Forgot_TranPassPolicy2");
                            string temp_loc = keyword2.Locator.Replace("[i]", "[" + (i) + "]");
                            selhelper.verification(temp, temp_loc);
                            temp_loc = null;
                        }

                    }
                }
                else if (Keyword.Equals("Investment_TermDetTotalAmount"))
                {
                    string amount = selhelper.ReturnKeywordValue(keyword.Locator);
                    amount = amount.Replace("Consolidated Amount (PKR)", "").Trim();
                    amount = amount.Replace(",", "");

                    message = Convert.ToString(context.Get_term_deposit_balance());

                    Assert.AreEqual(message, amount);
                
                }
                else
                {
                    if (Keyword.Equals("Investment_MutualFund_DisPopup") && context.GetFundDisclaimerPopup() == "")
                    {
                        return;
                    }
                    if (Keyword.Contains("Investment_TermDetAmount") || Keyword.Contains("Investment_TermDetPeriod") || Keyword.Contains("Investment_TermDetRate"))
                    {
                        keyword.Locator = keyword.Locator.Replace("{x}", context.GetTermRefNo());
                    }

                    selhelper.verification(message, keyword.Locator);
                }
                if (Keyword.Contains("TranSuccessMessage"))
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
            if (query.Contains("{TRANSACTION_ID}"))
            {
                query = query.Replace("{TRANSACTION_ID}", context.GetTransaction_Id());
            }
            if (query.Contains("{GUID}"))
            {
                query = query.Replace("{GUID}", context.Get_HostReferenceNo());
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

                    var areEquivalent = (account_list.Count == db_account_list.Count) && !account_list.Except(db_account_list).Any();

                    if (areEquivalent == false)
                    {
                        throw new AssertFailedException(string.Format("Account Numbers during Sign up:{0} are not the same as in Database:{1}", account_list, db_account_list));
                    }
                }

                else if (query.Contains("K.IS_ACCOUNT_LINK"))
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

                    var areEquivalent = (account_list.Count == db_account_list.Count) && !account_list.Except(db_account_list).Any();

                    if (areEquivalent == false)
                    {
                        throw new AssertFailedException(string.Format("Account Numbers which were tagged during Sign up:{0} are not the same as in Database:{1}", account_list, db_account_list));
                    }
                }
                else
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(query, schema);
                    string message = SourceDataTable.Rows[0][0].ToString();
                    Assert.AreEqual(message.ToUpper(), value.ToUpper());
                }

            }
            catch (Exception exception)
            {

                throw new AssertFailedException(exception.Message);
            }
        }

        [Given(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        [When(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        [Then(@"verify through database on ""(.*)"" on Schema ""(.*)"" on ""(.*)""")]
        public void ThenVerifyThroughDatabaseOnOnSchemaOn(string query, string schema, string Keyword)
        {
            string SURCHARGE_ATTRIBUTE = "";
            DateTime DUE_DATE_FORMAT;

            if (query.Contains("{customer_cnic}"))
            {
                query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
            }
            if (query.Contains("{customer_name}"))
            {
                query = query.Replace("{customer_name}", context.GetUsername());
            }
            if (query.Contains("DC_TRANSACTION"))
            {
                if (Keyword.Contains("SendMoney_TranToBank") || Keyword.Contains("TranType"))
                {
                    query = query + context.GetTransaction_Id() + "')";
                }
                else
                {
                    query = query + context.GetTransaction_Id() + "'";
                }
            }
            if (query.Contains("{invest_fund_name}"))
            {
                query = query.Replace("{invest_fund_name}", context.GetInvestFundName());
            }
            if (query.Contains("Company_Code"))
            {
                query = query.Replace("{Company_Code}", context.GetCompany_Code());
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

                    if(Keyword.Contains("TranAmount"))
                    {
                        message = Convert.ToDecimal(message).ToString("0.00");
                    }

                    if (Keyword.Equals("Pay_Transaction_Unpaid_Amount"))
                    {
                        string company_code = SourceDataTable.Rows[0][1].ToString();
                        DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][2].ToString()));
                        if (DUE_DATE_FORMAT < DateTime.Today)
                        {
                            string temp_query = "Select SURCHARGE_ATTRIBUTE from BPS_SURCHARGE_AUTOMATION AD where AD.COMPANY_CODE like '%" + company_code + "%'";
                            DataAccessComponent.DataAccessLink dLink2 = new DataAccessComponent.DataAccessLink();
                            DataTable SourceDataTable2 = dLink2.GetDataTable(temp_query, schema);
                            SURCHARGE_ATTRIBUTE = SourceDataTable2.Rows[0][0].ToString();

                            string query2 = "Select " + SURCHARGE_ATTRIBUTE + " from LP_BILLS L WHERE L.CONSUMER_NO = '" + context.GetConsumer_No() + "'";

                            dLink2 = null;
                            dLink2 = new DataAccessComponent.DataAccessLink();
                            SourceDataTable2 = null;
                            SourceDataTable2 = dLink2.GetDataTable(query2, "QAT_BPS");
                            string amount_after_dd = SourceDataTable2.Rows[0][0].ToString();
                            message = amount_after_dd;
                        }
                    }
                    if (query.Contains("LOGIN_AND_T_PWRD_DESC_BEFORE_LOGIN"))
                    {
                        string[] delimiters = { "<br>" };
                        string[] pieces = message.Split(delimiters, StringSplitOptions.None);
                        message = pieces[0] + " " + pieces[1];
                    }
                    if (Keyword.Equals("Registration_PassPolicyText"))
                    {
                        string temp1 = "";
                        string temp2 = "";
                        for (int i = 1; i <= 2; i++)
                        {
                            string loc = keyword.Locator.Replace("{i}", Convert.ToString(i));
                            if (i == 1)
                            {
                                temp1 = selhelper.ReturnKeywordValue(loc);
                            }
                            if (i == 2)
                            {
                                temp2 = selhelper.ReturnKeywordValue(loc);
                            }
                        }
                        string keyword_text = temp1 + " " + temp2;
                        Assert.AreEqual(message, keyword_text);
                        return;
                    }
                    if (Keyword.Equals("Forget_PasswordTranID"))
                    {
                        context.SetTransaction_Id(message);
                    }
                    if (Keyword.Equals("Pay_Transaction_Success_Amount") || Keyword.Equals("Pay_Transaction_Unpaid_Amount") || Keyword.Equals("MyAccount_PayOrder_Tran_Amount"))
                    {
                        message = Convert.ToDecimal(message).ToString("0.00");
                    }
                    if (Keyword.Equals("Pay_Transaction_Success_ConsumerNo"))
                    {
                        context.SetConsumer_No(message);
                    }
                    if (Keyword.Contains("TranDate"))
                    {
                        DateTime lastlogin = Convert.ToDateTime(message);
                        message = lastlogin.ToString("MM/dd/yyyy");
                        string today_date = DateTime.Today.ToString("MM/dd/yyyy");
                        Assert.AreEqual(message, today_date);
                        return;
                    }
                    if (Keyword.Contains("Investment_TermRefNo"))
                    {
                        keyword.Locator = keyword.Locator.Replace("{x}", context.GetTermRefNo());
                        string ref_no = selhelper.ReturnKeywordValue(keyword.Locator);
                        ref_no = ref_no.Replace("Reference #", "").Trim();
                        Assert.AreEqual(message, ref_no);
                        return;
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

        [Given(@"I press Enter on ""(.*)""")]
        [When(@"I press Enter on ""(.*)""")]
        [Then(@"I press Enter on ""(.*)""")]
        public void WhenIPressEnterOn(string Keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            //selhelper.checkPageIsReady();
            Thread.Sleep(3000);
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            if (Keyword.Contains("Investment_MutualFund_InvestBtn"))
            {
                keyword.Locator = keyword.Locator.Replace("{x}", context.GetInvestFundName());
            }
            selhelper.PressEnter(keyword.Locator);
        }

        [Given(@"I scroll to element ""(.*)""")]
        [When(@"I scroll to element ""(.*)""")]
        [Then(@"I scroll to element ""(.*)""")]
        public void WhenIScrollToElement(string Keyword)
        {

            SeleniumHelper selhelper = new SeleniumHelper();
            //selhelper.checkPageIsReady();
            Thread.Sleep(3000);
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            if (Keyword.Contains("Registration_Scroll"))
            {
                keyword.Locator = keyword.Locator.Replace("{K}", context.GetScrollText());
            }
            if (Keyword.Equals("Investment_TermRefNo"))
            {
                keyword.Locator = keyword.Locator.Replace("{x}", context.GetTermRefNo());
            }
            if (Keyword.Contains("Investment_MutualFund_InvestBtn"))
            {
                keyword.Locator = keyword.Locator.Replace("{x}", context.GetInvestFundName());
            }
            selhelper.ScrollToElement(keyword.Locator);
        }
        [Given(@"I have otp check and given (.*) on ""(.*)""")]
        [When(@"I have otp check and given (.*) on ""(.*)""")]
        [Then(@"I have otp check and given (.*) on ""(.*)""")]
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

        [When(@"I have transaction pass check and given ""(.*)"" on ""(.*)""")]
        public void WhenIHaveTransactionPassCheckAndGivenOn(string otp_value, string Keyword)
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

        [Given(@"I want value from textbox ""(.*)"" on database ""(.*)"" as ""(.*)""")]
        [When(@"I want value from textbox ""(.*)"" on database ""(.*)"" as ""(.*)""")]
        [Then(@"I want value from textbox ""(.*)"" on database ""(.*)"" as ""(.*)""")]
        public void WhenIWantValueFromTextboxOnDatabaseAs(string Keyword, string db_value, string query)
        {
            if (Keyword.Contains("Pay_Transaction_Unpaid_Amount") && context.Get_Is_Partial_Allow() == "1")
            {
                return;
            }
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
        [Given(@"I select date ""(.*)"" on month ""(.*)"" on year ""(.*)""")]
        [When(@"I select date ""(.*)"" on month ""(.*)"" on year ""(.*)""")]
        [Then(@"I select date ""(.*)"" on month ""(.*)"" on year ""(.*)""")]
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
        [Given(@"I am verifying list of execution iterations on ""(.*)""")]
        [When(@"I am verifying list of execution iterations on ""(.*)""")]
        [Then(@"I am verifying list of execution iterations on ""(.*)""")]
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
        [When(@"I save Account Balances")]
        [Then(@"I save Account Balances")]
        public void GivenISaveAccountBalances()
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int count = 0;
            int x = context.GeTSizeCount();
            string acc_no = "";
            string acc_bal = "";
            string cc_available_limit = "";
            if (context.GetCredit_Card_Check() == "" || context.GetCredit_Card_Check() == null)
            {
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
            }
            else
            {
                Element keyword = ContextPage.GetInstance().GetElement("Pay_Acc_No_CC");
                acc_no = selhelper.ReturnKeywordValue(keyword.Locator);

                Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Acc_Bal_CC");
                acc_bal = selhelper.ReturnKeywordValue(keyword2.Locator);

                Element keyword3 = ContextPage.GetInstance().GetElement("Pay_Aval_Limit_CC");
                cc_available_limit = selhelper.ReturnKeywordValue(keyword2.Locator);
                context.SetCC_Limit(cc_available_limit);

                dict.Add(acc_no, acc_bal);
            }

            Element temp_keyword = ContextPage.GetInstance().GetElement("Investment_AccountInfo");
            string acc_text = selhelper.ReturnKeywordValue(temp_keyword.Locator);
            if (acc_text.Contains("Term Deposit") && context.Get_term_deposit_check() == 1)
            {
                context.Set_term_deposit_check(2);
                Element temp_keyword1 = ContextPage.GetInstance().GetElement("Investment_TermDep_Balance");
                Element temp_keyword2 = ContextPage.GetInstance().GetElement("Investment_TermDep_AccNo");

                dict.Add(selhelper.ReturnKeywordValue(temp_keyword2.Locator), selhelper.ReturnKeywordValue(temp_keyword1.Locator));
            }
            else if (context.Get_term_deposit_check() == 1)
            {
                context.Set_term_deposit_check(2);
                dict.Add("Term Deposit", "0.00");
            }

            if (context.GetInvestFundName() != null && acc_text.Contains("HBL Mutual Funds"))
            {
                Element temp_keyword3 = ContextPage.GetInstance().GetElement("Investment_MutualFund_AccName");
                Element temp_keyword4 = ContextPage.GetInstance().GetElement("Investment_MutualFund_Balance");

                string acc_name = selhelper.ReturnKeywordValue(temp_keyword3.Locator);
                string acc_balance = selhelper.ReturnKeywordValue(temp_keyword4.Locator);
                dict.Add(acc_name, acc_balance);
            }
            context.Set_acc_balances(dict);
        }


        [Given(@"I save Transaction Info")]
        [When(@"I save Transaction Info")]
        [Then(@"I save Transaction Info")]
        public void WhenISaveTransactionInfo()
        {
            string[] consumer_no_arr = context.Get_multi_bill_consumers();
            bool term_deposit_checker = false;
            string tran_balance = "";
            SeleniumHelper selhelper = new SeleniumHelper();
            Dictionary<string, string> tran_dict = new Dictionary<string, string>();

            if (consumer_no_arr != null)
            {
                Element keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_FromAccount");

                string tran_account = selhelper.ReturnKeywordValue(keyword.Locator);
                context.SetTran_Account(tran_account);

                int int_tran_balance = context.Get_multi_payment_amount();
                context.SetTran_Balance(Convert.ToDecimal(int_tran_balance));
            }
            else
            {
                Element keyword = ContextPage.GetInstance().GetElement("Pay_Transaction_Success_FromAccount");
                Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Transaction_Success_Amount");

                string tran_account = selhelper.ReturnKeywordValue(keyword.Locator);
                context.SetTran_Account(tran_account);

                tran_balance = selhelper.ReturnKeywordValue(keyword2.Locator);
                context.SetTran_Balance(Convert.ToDecimal(tran_balance));
            }

            tran_dict = context.Get_acc_balance();
            if (context.GetCredit_Card_Check() == "" || context.GetCredit_Card_Check() == null)
            {
                foreach (var item in tran_dict)
                {
                    if (context.GeTran_Account() == item.Key || item.Key == "Term Deposit")
                    {
                        if (context.Get_term_deposit_check() == 2 && item.Key == "Term Deposit")
                        {
                            decimal term_deposit_bal = Convert.ToDecimal(item.Value) + Convert.ToDecimal(tran_balance);
                            context.Set_term_deposit_balance(term_deposit_bal);
                            term_deposit_checker = true;
                            continue;
                        }
                        else if (context.GeTran_Account() == item.Key)
                        {
                            decimal tran_balancee = Convert.ToDecimal(item.Value) - context.GetTran_Balance();
                            context.SetTran_Balance(tran_balancee);
                            continue;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in tran_dict)
                {
                    if ((context.GeTran_Account().Substring(0, 4) == item.Key.Substring(0, 4)) && (context.GeTran_Account().Substring(context.GeTran_Account().Length - 4) == item.Key.Substring(item.Key.Length - 4)))
                    {
                        decimal tran_balancee = Convert.ToDecimal(item.Value) + context.GetTran_Balance();
                        context.SetTran_Balance(tran_balancee);

                        string new_cc_limit = Convert.ToString(Convert.ToDecimal(context.GetCC_Limit()) - context.GetTran_Balance());
                        context.SetCC_Limit(new_cc_limit);
                        break;
                    }
                }
            }

        }
        [When(@"I verify Account Balance")]
        [Then(@"I verify Account Balance")]
        public void ThenIVerifyAccountBalance()
        {
            decimal old_account_bal = 0;
            string new_account_bal = "";
            string new_cc_avail_limit = "";
            try
            {
                old_account_bal = context.GetTran_Balance();

                SeleniumHelper selhelper = new SeleniumHelper();
                if (context.GetCredit_Card_Check() == "" || context.GetCredit_Card_Check() == null)
                {
                    Element keyword = ContextPage.GetInstance().GetElement("Pay_Final_Account_Balance");
                    string tAccountNo = context.GeTran_Account();
                    string temp = keyword.Locator.Replace("{k}", tAccountNo);
                    new_account_bal = selhelper.ReturnKeywordValue(temp);

                    if (Convert.ToDecimal(new_account_bal) != old_account_bal)
                    {
                        throw new AssertFailedException(string.Format("The old account balance {0} is not equal to new account balance {1} after successfull transaction", old_account_bal, new_account_bal));
                    }

                    if (context.Get_acc_balance().ContainsKey("Term Deposit"))
                    {
                        if (context.Get_term_deposit_check() == 2 && context.Get_acc_balance().ContainsKey("Term Deposit"))
                        {
                            Element temp_keyword1 = ContextPage.GetInstance().GetElement("Investment_TermDep_Balance");
                            string new_etdr_bal = selhelper.ReturnKeywordValue(temp_keyword1.Locator);
                            old_account_bal = context.Get_term_deposit_balance();

                            if (Convert.ToDecimal(new_etdr_bal) != old_account_bal)
                            {
                                throw new AssertFailedException(string.Format("The Term Deposit balance {0} is not equal to new Term Deposit balance {1} after successfull transaction", old_account_bal, new_etdr_bal));
                            }
                        }
                    }
                    else if(context.Get_acc_balance().ContainsKey("HBL Mutual Funds Balance"))
                    {
                        Element temp_keyword1 = ContextPage.GetInstance().GetElement("Investment_MutualFund_Balance");
                        string new_mutual_bal = selhelper.ReturnKeywordValue(temp_keyword1.Locator);
                        old_account_bal = context.Get_mutual_fund_balance();

                        if (Convert.ToDecimal(new_mutual_bal) != old_account_bal)
                        {
                            throw new AssertFailedException(string.Format("The Mutual Fund balance in statement {0} is not equal to Mutual Fund balance on Website {1}", old_account_bal, new_mutual_bal));
                        }
                    }
                }
                else
                {
                    Element keyword1 = ContextPage.GetInstance().GetElement("Pay_Acc_Bal_CC");
                    new_account_bal = selhelper.ReturnKeywordValue(keyword1.Locator);
                    keyword1 = null;

                    keyword1 = ContextPage.GetInstance().GetElement("Pay_Aval_Limit_CC");
                    new_cc_avail_limit = selhelper.ReturnKeywordValue(keyword1.Locator);

                    if (new_cc_avail_limit != context.GetCC_Limit())
                    {
                        throw new AssertFailedException(string.Format("The new credit card limit {0} is not equal to old credit card limit {1} after successfull transaction", new_cc_avail_limit, context.GetCC_Limit()));
                    }
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
            if (context.GetCredit_Card_Check() == null || context.GetCredit_Card_Check() == "")
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                Element Keyword = ContextPage.GetInstance().GetElement("Pay_Account_No-Count");
                int size = selhelper.SizeCountElements(Keyword.Locator);
                context.SetSizeCount(size);
            }
            else
            {
                context.SetSizeCount(1);
            }
        }

        //To verify the schedule configuration from Data base 
        [Then(@"verify the schedule config ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheScheduleConfigOnSchema(string query, string db_value)
        {
            if (query.Contains("{customer_name}"))
            {
                query = query.Replace("{customer_name}", context.GetUsername());
            }
            if (query.Contains("{ConsumerNo}"))
            {
                query = query.Replace("{ConsumerNo}", context.GetConsumer_No());
            }
            if (context.Get_IS_SI_Allowed() == "0")
            {
                return;
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
        [When(@"I am performing ""(.*)"" alert operation on cross icon on ""(.*)""")]
        [Then(@"I am performing ""(.*)"" alert operation on cross icon on ""(.*)""")]
        public void GivenIAmPerformingAlertOperationOnCrossIconOn(string option, string keyword)
        {
            SeleniumHelper selhelper = new SeleniumHelper();
            Element Keyword = ContextPage.GetInstance().GetElement(keyword);

            if (keyword.Equals("BeneManage_Delete"))
            {
                Keyword.Locator = Keyword.Locator.Replace("{x}", context.GetBeneAccountNo());
            }

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
            Element keyword = ContextPage.GetInstance().GetElement("Registration_AccountSize");

            List<string> lst = new List<string>();
            int x = selhelper.SizeCountElements(keyword.Locator);
            string acc_no = "";
            int count = 0;

            keyword = null;
            keyword = ContextPage.GetInstance().GetElement("Registration_TagAccountNo");

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
            string ui_bill_status;
            Thread.Sleep(3000);
            SeleniumHelper selhelper = new SeleniumHelper();
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            string[] consumer_no_arr = Consumer_No_List.Split(',');
            context.Set_multi_bill_consumers(consumer_no_arr);
            for (int i = 0; i < consumer_no_arr.Length; i++)
            {
                selhelper.SetTextBoxValue(consumer_no_arr[i], keyword.Locator);

                string query = "SELECT PB.BILL_BENE_NICK,PB.BILL_STATUS,PB.DUE_DATE,PB.AMOUNT_BEFORE_DUE_DATE,PB.AMOUNT_AFTER_DUE_DATE, PB.COMPANY_SUB_CATEGORY FROM DC_BILL_PAYMENT_BENEFICIARY PB WHERE PB.CONSUMER_NUMBER = '" + consumer_no_arr[i] + "' AND PB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "')";
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");
                BILL_BENE_NICK = SourceDataTable.Rows[0][0].ToString();
                if (BILL_BENE_NICK == "")
                {
                    BILL_BENE_NICK = SourceDataTable.Rows[0][5].ToString();
                }
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
                    throw new AssertFailedException(string.Format("The Bene Nick in database {0} is not equal to Bene Nick On Screen {1} :{2}", BILL_BENE_NICK, ui_bene_nick, consumer_no_arr[i]));
                }

                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_Multi_UIBillStatus");
                ui_bill_status = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_bill_status = ui_bill_status.ToUpper();
                if (ui_bill_status != BILL_STATUS)
                {
                    throw new AssertFailedException(string.Format("The Bill Status in database {0} is not equal to status On Screen {1} :{2} ", BILL_STATUS, ui_bill_status, consumer_no_arr[i]));
                }

                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_Multi_UIAmount");
                ui_amount = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_amount = ui_amount.Replace("\r\nPKR\r\nUnpaid", string.Empty);
                if (ui_amount.Contains(","))
                {
                    ui_amount = ui_amount.Replace(",", "");
                }
                if (ui_amount != amount)
                {
                    throw new AssertFailedException(string.Format("The Amount database {0} is not equal to Amount On Screen {1} :{2}", amount, ui_amount, consumer_no_arr[i]));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillDueDate");
                ui_duedate = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_duedate = ui_duedate.Replace(@"Due Date: ", string.Empty);
                if (ui_duedate != DUE_DATE)
                {
                    throw new AssertFailedException(string.Format("The DueDate in database {0} is not equal to DueDate On Screen {1} :{2}", DUE_DATE, ui_duedate, consumer_no_arr[i]));
                }
                Element keyword2 = ContextPage.GetInstance().GetElement("Pay_Multi_ConsumerNoCheck");
                selhelper.links(keyword2.Locator);
            }
        }
        [When(@"I verify bill details of consumer numbers for bill payment")]
        public void WhenIVerifyBillDetailsOfConsumerNumbersForBillPayment()
        {
            string BILL_STATUS;
            string BILL_STATUS_ID;
            string ui_BILL_STATUS;
            string DUE_DATE;
            string COMPANY_CODE;
            string SURCHARGE_ATTRIBUTE;
            DateTime DUE_DATE_FORMAT;
            int amount = 0;
            string amount_within_dd;
            string ui_amount_within_dd;
            string amount_after_dd = "";
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
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillBillingMonth_Inquiry");
                string billing_month = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                DateTime temp_var = Convert.ToDateTime(billing_month);
                billing_month = temp_var.ToString("dd/MM/yyyy");

                string query = "Select L.COMPANY_CODE, L.DUE_DATE, l.BILL_AMOUNT, l.CONSUMER_NAME, L.BILL_STATUS_ID  FROM LP_BILLS L WHERE L.CONSUMER_NO ='" + consumer_no_arr[i] + "' and L.BILLING_MONTH = To_DATE('" + billing_month + "', 'dd/MM/YYYY')";
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                COMPANY_CODE = SourceDataTable.Rows[0][0].ToString();
                DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable.Rows[0][1]));
                amount_within_dd = SourceDataTable.Rows[0][2].ToString();
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
                query = "Select " + SURCHARGE_ATTRIBUTE + " from LP_BILLS L WHERE L.CONSUMER_NO = '" + consumer_no_arr[i] + "'";

                dLink = null;
                dLink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = null;
                SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                amount_after_dd = Convert.ToString(Convert.ToInt32(SourceDataTable.Rows[0][0].ToString()));
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
                query = "SELECT CC.IS_PWD_REQUIRED, cc.IS_PAID_MARKING_REQUIRED FROM BPS_COMPANY_CHANNEL CC WHERE CC.COMPANY_CODE = (SELECT CC.COMPANY_CODE FROM BPS_COMPANY CC WHERE CC.COMPANY_NAME = '" + company_name + "') AND CC.CHANNEL_CODE = 'MB'";
                dLink = null;
                dLink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = null;
                SourceDataTable = dLink.GetDataTable(query, "QAT_BPS");
                string tran_pass_req = SourceDataTable.Rows[0][0].ToString();
                string is_paid_req = SourceDataTable.Rows[0][1].ToString();
                context.SetIsPaidReq(is_paid_req);
                if (tran_pass_req == "1")
                {
                    context.SetTranPassReq(tran_pass_req);
                }

                if (ui_company_name != company_name)
                {
                    throw new AssertFailedException(string.Format("The Company Name in database {0} is not equal to Company Name On Screen {1} for consumer number :{2}", company_name, ui_company_name, consumer_no_arr[i]));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillConsumerName_Inquiry");
                ui_consumer_name = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                ui_consumer_name = ui_consumer_name.Replace(@" ", string.Empty);
                if (ui_consumer_name != consumer_name)
                {
                    throw new AssertFailedException(string.Format("The Consumer Name in database {0} is not equal to ConsumerName On Screen {1} for consumer number :{2}", consumer_name, ui_consumer_name, consumer_no_arr[i]));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillDueDate_Inquiry");
                ui_duedate = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_duedate != DUE_DATE)
                {
                    throw new AssertFailedException(string.Format("The Due Date in database {0} is not equal to Due Date On Screen {1} for consumer number :{2}", DUE_DATE, ui_duedate, consumer_no_arr[i]));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillAmount_Inquiry");
                ui_amount_within_dd = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_amount_within_dd.Contains(","))
                {
                    ui_amount_within_dd = ui_amount_within_dd.Replace(",", "");
                }
                if (ui_amount_within_dd != amount_within_dd)
                {
                    throw new AssertFailedException(string.Format("The Amount With In Due Date in database {0} is not equal to Amount With In Due Date On Screen {1} for consumer number :{2}", amount_within_dd, ui_amount_within_dd, consumer_no_arr[i]));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillAmountDD_inquiry");
                ui_amount_after_dd = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_amount_after_dd.Contains(","))
                {
                    ui_amount_after_dd = ui_amount_after_dd.Replace(",", "");
                }
                if (ui_amount_after_dd != amount_after_dd)
                {
                    throw new AssertFailedException(string.Format("The Amount After Due Date in database {0} is not equal to Amount After Due Date On Screen {1} for consumer number :{2}", amount_after_dd, ui_amount_after_dd, consumer_no_arr[i]));
                }
                Temp_keyword = null;
                Temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBillStatus_Inquiry");
                ui_BILL_STATUS = selhelper.ReturnKeywordValue(Temp_keyword.Locator);
                if (ui_BILL_STATUS != BILL_STATUS)
                {
                    throw new AssertFailedException(string.Format("The Bill Status in database {0} is not equal to Bill Status On Screen {1} for consumer number :{2}", BILL_STATUS, ui_BILL_STATUS, consumer_no_arr[i]));
                }

            }
            Temp_keyword = null;
            Temp_keyword = ContextPage.GetInstance().GetElement("SendMoney_Amount");
            ui_amount = selhelper.ReturnTextBoxValue(Temp_keyword.Locator);
            if (ui_amount.Contains(","))
            {
                ui_amount = ui_amount.Replace(",", "");
            }
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
            string amount_after_dd = "";
            string amount = "";
            string BILL_STATUS = "";
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
            for (int i = consumer_no_arr.Length-1; i >= 0; i--)
            {
                keyword = null;

                string tranid_keyword = "Pay_MultiBill_Success_TranID";
                keyword = ContextPage.GetInstance().GetElement(tranid_keyword);
                tran_id = selhelper.ReturnKeywordValue(keyword.Locator);
                context.SetTransaction_Id(tran_id);

                for (int j = 0; j < queries.Length; j++)
                {
                    if (j == 0)
                    {
                        continue;
                    }
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

                string dc_tran_query = "Select TRANSACTION_AMOUNT, BILL_COMPANY, BILL_STATUS, BILL_CONSUMER_NUMBER, BILL_COMPANY_CODE, DT.CREATED_ON FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = '" + tran_id + "' and DT.CHANNEL_ID = '2'";

                DataAccessComponent.DataAccessLink dlink2 = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable2 = dlink2.GetDataTable(dc_tran_query, schema);
                string tran_amount = SourceDataTable2.Rows[0][0].ToString();
                string tran_BILL_COMPANY = SourceDataTable2.Rows[0][1].ToString();
                string tran_BILL_STATUS = SourceDataTable2.Rows[0][2].ToString();
                string tran_BILL_CONSUMER_NUMBER = SourceDataTable2.Rows[0][3].ToString();
                string tran_BILL_COMPANY_CODE = SourceDataTable2.Rows[0][4].ToString();
                string tran_CREATED_ON = SourceDataTable2.Rows[0][5].ToString();
                DateTime lastlogin = Convert.ToDateTime(tran_CREATED_ON);
                tran_CREATED_ON = lastlogin.ToString("MM/dd/yyyy");

                dlink2 = null;
                SourceDataTable2 = null;
                string lp_bills__query = "Select L.COMPANY_CODE, L.DUE_DATE, l.BILL_AMOUNT, L.BILL_STATUS_ID, L.PAID_DATE  FROM LP_BILLS L WHERE L.CONSUMER_NO ='" + consumer_no_arr[i] + "' order by UPDATED_ON desc";
                dlink2 = new DataAccessComponent.DataAccessLink();
                SourceDataTable2 = dlink2.GetDataTable(lp_bills__query, "QAT_BPS");
                string COMPANY_CODE = SourceDataTable2.Rows[0][0].ToString();
                DateTime DUE_DATE_FORMAT = (Convert.ToDateTime(SourceDataTable2.Rows[0][1]));
                string amount_within_dd = SourceDataTable2.Rows[0][2].ToString();
                string BILL_STATUS_ID = SourceDataTable2.Rows[0][3].ToString();
                string updated_on = SourceDataTable2.Rows[0][4].ToString();
                lastlogin = Convert.ToDateTime(updated_on);
                updated_on = lastlogin.ToString("MM/dd/yyyy");

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

                string query = "Select COMPANY_NAME, SURCHARGE_ATTRIBUTE from BPS_SURCHARGE_AUTOMATION AD where AD.COMPANY_CODE like '%" + COMPANY_CODE + "%'";
                dlink2 = null;
                SourceDataTable2 = null;
                dlink2 = new DataAccessComponent.DataAccessLink();
                SourceDataTable2 = dlink2.GetDataTable(query, "QAT_BPS");
                string company_name = SourceDataTable2.Rows[0][0].ToString();
                company_name = company_name.Replace("\r\n", "");
                string SURCHARGE_ATTRIBUTE = SourceDataTable2.Rows[0][1].ToString();
                if (DUE_DATE_FORMAT < DateTime.Today)
                {
                    query = "Select " + SURCHARGE_ATTRIBUTE + " from LP_BILLS L WHERE L.CONSUMER_NO = '" + consumer_no_arr[i] + "'";

                    dlink2 = null;
                    dlink2 = new DataAccessComponent.DataAccessLink();
                    SourceDataTable2 = null;
                    SourceDataTable2 = dlink2.GetDataTable(query, "QAT_BPS");
                    amount_after_dd = SourceDataTable2.Rows[0][0].ToString();
                    amount = amount_after_dd;
                }
                else
                {
                    amount = amount_within_dd;
                }

                if (tran_amount != amount)
                {
                    throw new AssertFailedException(string.Format("The Value in DC_Transaction table : {0} is not equal to respected value in LP_BILLS table : {1} for consumer number :{2}", tran_amount, amount, consumer_no_arr[i]));
                }
                if (tran_BILL_COMPANY != company_name)
                {
                    throw new AssertFailedException(string.Format("The Value in DC_Transaction table : {0} is not equal to respected value in LP_BILLS table : {1} :{2}", tran_BILL_COMPANY, company_name, consumer_no_arr[i]));
                }
                if ((context.GetIsPaidReq() == "1") && (BILL_STATUS != "PAID"))
                {
                    throw new AssertFailedException("Bill Status is updated according to Is_Paid_Mark_required");
                }
                if (tran_BILL_COMPANY_CODE != COMPANY_CODE)
                {
                    throw new AssertFailedException(string.Format("The Value in DC_Transaction table : {0} is not equal to respected value in LP_BILLS table : {1} :{2}", tran_BILL_COMPANY_CODE, COMPANY_CODE, consumer_no_arr[i]));
                }
                if (tran_CREATED_ON != updated_on)
                {
                    throw new AssertFailedException(string.Format("The Value in DC_Transaction table : {0} is not equal to respected value in LP_BILLS table : {1} :{2}", tran_CREATED_ON, updated_on, consumer_no_arr[i]));
                }

                if (i != 0)
                {
                    keyword = null;
                    keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_BackArrow");
                    selhelper.links(keyword.Locator);
                }
            }
        }

        //For linking or de-linking accounts from profile. 
        [Given(@"I select ""(.*)"" for Account linking or de-linking ""(.*)"" with success message as ""(.*)""")]
        [When(@"I select ""(.*)"" for Account linking or de-linking ""(.*)"" with success message as ""(.*)""")]
        [Then(@"I select ""(.*)"" for Account linking or de-linking ""(.*)"" with success message as ""(.*)""")]
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

            //keyword2 = ContextPage.GetInstance().GetElement("Pay_MultiBill_SRV_TranType");
            //DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            //DataTable SourceDataTable = dlink.GetDataTable("SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = '" + tranID + "')", "DIGITAL_CHANNEL_SEC");
            //string tran_type = SourceDataTable.Rows[0][0].ToString();
            //selhelper.verification(tran_type, keyword2.Locator);
            //keyword2 = null;

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

        [Then(@"i am performing Limit verification operation with ""(.*)""")]
        public void ThenIAmPerformingLimitVerificationOperationWith(string keyword)
        {
            try
            {
                Dictionary<string, Tuple<string, string, string>> customer_limit_detail_dict = new Dictionary<string, Tuple<string, string, string>>();

                string limit_type_id = "";
                string config_value = "";
                string daily_consume_limit = "";
                string daily_rem_limit = "";

                SeleniumHelper selhelper = new SeleniumHelper();
                selhelper.checkPageIsReady();

                Element Keyword_main = ContextPage.GetInstance().GetElement(keyword);
                int limit_type_count = selhelper.SizeCountElements(Keyword_main.Locator);

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
                    else if(enable_psd == "1")
                    {
                        limit_type_id = "4";
                        //if (config_value == "P")
                    }
                }
                else if(customer_type == "A")
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

                //if (!(customer_type == "A" && enable_psd == "0" && limit_type_id_DB == "6"))
                //{
                //    throw new Exception(string.Format("The values setting in data base is not correct for CUSTOMER_TYPE :{0}, ENABLE_PSD :{1} and LIMIT_TYPE_iD", customer_type, enable_psd, limit_type_id_DB));
                //}

                //if (enable_psd == "0")
                //{
                //    limit_type_id = "1";
                //}
                //else if (enable_psd == "1")
                //{
                //    limit_type_id = "4";
                //}

                //else if (config_value == "NRP")
                //{
                //    limit_type_id = "5";
                //}

                //else if (config_value == "P")
                //{
                //    limit_type_id = "8";
                //}
                //context.SetLimitTypeID(limit_type_id);

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select COUNT(*) from dc_Tran_Type_limit_group_rules a where A.LIMIT_TYPE_ID='" + limit_type_id + "' and A.IS_CLIENT_VIEW = 1 and A.IS_ACTIVE = '1' and trunc(A.EFFECTIVE_FROM_DATE) <= sysdate and A.EFFECTIVE_TO_DATE >= sysdate ", "DIGITAL_CHANNEL_SEC");
                int is_client_view = Convert.ToInt32(SourceDataTable.Rows[0][0].ToString());
                dlink = null;
                SourceDataTable = null;

                if (is_client_view != limit_type_count)
                {
                    throw new Exception(string.Format("The required limit count against Limit Type id :{0} in Database :{1} is not equal with UI :{2}", limit_type_id, is_client_view, limit_type_count));
                }

                for (int i = 1; i <= limit_type_count; i++)
                {
                    string per_transaction = "";

                    string temp = Keyword_main.Locator + "[" + Convert.ToString(i) + "]";
                    selhelper.ScrollToElement(temp);
                    string limit_type = selhelper.ReturnKeywordValue(temp);

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
                    string temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                    selhelper.ScrollToElement(temp_keyword);
                    string per_tran_UI = selhelper.ReturnKeywordValue(temp_keyword);
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
                    temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                    selhelper.ScrollToElement(temp_keyword);
                    string daily_limit = selhelper.ReturnKeywordValue(temp_keyword);
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
                    temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                    string daily_consume_limit_ui = selhelper.ReturnKeywordValue(temp_keyword);
                    daily_consume_limit_ui = daily_consume_limit_ui.Remove(daily_consume_limit_ui.Length - 3);
                    daily_consume_limit_ui = daily_consume_limit_ui.Replace(",", "");
                    if (daily_consume_limit != daily_consume_limit_ui)
                    {
                        throw new Exception(String.Format("Database Availed limit :{0} is not equal with Availed limit on Website :{1} for limit type :{2}", daily_consume_limit, daily_consume_limit_ui, limit_type));
                    }

                    temp_keyword = null;
                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Remaining");
                    temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                    daily_rem_limit = selhelper.ReturnKeywordValue(temp_keyword);
                    daily_rem_limit = daily_rem_limit.Remove(daily_rem_limit.Length - 3);
                    daily_rem_limit = daily_rem_limit.Replace(",", "");

                    if (Convert.ToInt32(daily_rem_limit) != (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)))
                    {
                        throw new Exception(String.Format("Website Remaining limit :{0} is not equal with Calculated Remaining amount from Availed Limit :{1} for limit type :{2}", daily_rem_limit, (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)), limit_type));
                    }

                    customer_limit_detail_dict.Add(limit_type, Tuple.Create(old_limit, limit_type_id, limit_rules_id));

                    Keyword = null;
                    temp_keyword = null;
                    limit_rules_id = temp = daily_limit = old_limit = per_tran_UI = per_transaction = min_amount_check  = limit_type = temp_keyword = String.Empty;
                }
                context.SetCustLimitDetail(customer_limit_detail_dict);
            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [Then(@"I am performing limit reduction operation on ""(.*)"" of slider ""(.*)"" of ""(.*)"" with new limit as ""(.*)""")]
        public void ThenIAmPerformingLimitReductionOperationOnOfSliderOfWithNewLimitAs(string keyword, string slider_keyword, string limit_type, int new_limit)
        {
            try
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                var cust_limit_dict = context.GetCustLimitDetail();
                string min_value = "";
                string max_value = "";
                string old_limit = "";
                string limit_type_id = "";
                string limit_rules_id = "";
                string daily_consume_limit = "";
                string daily_rem_limit = "";
                string temp_keyword = "";

                foreach (var item in cust_limit_dict)
                {
                    if (item.Key == limit_type)
                    {
                        old_limit = item.Value.Item1;
                        limit_type_id = item.Value.Item2;
                        limit_rules_id = item.Value.Item3;
                        break;
                    }
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
                SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_AMOUNT from DC_LIMIT_CONSUMED I WHERE I.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info L where L.customer_name = '" + context.GetUsername() +"') and TRUNC(I.LAST_TXN_DATE) = Trunc(SYSDATE) and I.LIMIT_RULES_ID = '" + limit_rules_id + "'", "DIGITAL_CHANNEL_SEC");
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
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                string daily_consume_limit_ui = selhelper.ReturnKeywordValue(temp_keyword);
                daily_consume_limit_ui = daily_consume_limit_ui.Remove(daily_consume_limit_ui.Length - 3);
                daily_consume_limit_ui = daily_consume_limit_ui.Replace(",", "");
                if (daily_consume_limit != daily_consume_limit_ui)
                {
                    throw new Exception(String.Format("Database Availed limit :{0} is not equal with Availed limit on Website :{1} for limit type :{2}", daily_consume_limit, daily_consume_limit_ui, limit_type));
                }
                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Remaining");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                daily_rem_limit = selhelper.ReturnKeywordValue(temp_keyword);
                daily_rem_limit = daily_rem_limit.Remove(daily_rem_limit.Length - 3);
                daily_rem_limit = daily_rem_limit.Replace(",", "");

                if (Convert.ToInt32(daily_rem_limit) != (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)))
                {
                    throw new Exception(String.Format("Website Remaining limit :{0} is not equal with Calculated Remaining amount from Availed Limit :{1} for limit type :{2}", daily_rem_limit, (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)), limit_type));
                }
                
                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement(keyword);
                Element Keyword2 = ContextPage.GetInstance().GetElement(slider_keyword);

                string locator = Keyword.Locator.Replace("{x}", limit_type);
                string slider_locator = Keyword2.Locator.Replace("{x}", limit_type);

                selhelper.ScrollToElement(locator);
                selhelper.PressEnter(locator);

                Keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Max");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                string max_value_ui = selhelper.ReturnKeywordValue(temp_keyword);
                max_value_ui = max_value_ui.Remove(max_value_ui.Length - 3);
                max_value_ui = max_value_ui.Replace(",", "");

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_NewLimit");
                string slider_limit_ui = Keyword.Locator.Replace("{x}", limit_type);

                //int temp_count = Convert.ToInt32(max_value_ui) - Convert.ToInt32(old_limit);
                //temp_count = temp_count / Convert.ToInt32(step);

                //int result = Convert.ToInt32(old_limit) - new_limit;
                //int step_limit = result / Convert.ToInt32(step);

                //int step_limit_count = step_limit + temp_count;

                selhelper.RangeSlider(slider_locator, new_limit, Convert.ToInt32(step), slider_limit_ui, Convert.ToInt32(max_value_ui));

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select A.MIN_EDITABLE_AMOUNT from dc_Tran_Type_limit_group_rules a where A.CLIENT_DESCRIPTION='" + limit_type + "' and A.LIMIT_TYPE_ID='" + limit_type_id + "'", "DIGITAL_CHANNEL_SEC");
                min_value = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                Keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Min");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                string min_value_ui = selhelper.ReturnKeywordValue(temp_keyword);
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
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_NewPrice");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                string new_edit_limit = selhelper.ReturnKeywordValue(temp_keyword);
                new_edit_limit = new_edit_limit.Remove(new_edit_limit.Length - 3);
                new_edit_limit = new_edit_limit.Replace(",", "");
                if (new_edit_limit != Convert.ToString(new_limit))
                {
                    throw new Exception(String.Format("New Given Limit Amount :{0} is not equal with New Limit amount on website :{1} for limit type :{2}", new_limit, new_edit_limit, limit_type));
                }

                keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Save_Btn");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.Button(temp_keyword);

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Succ_Txt");
                string success_message = selhelper.ReturnKeywordValue(Keyword.Locator);

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select C.RESULT_CODE_DESCRIPTION from dc_Response_code c where C.ERROR_CODE = 'LIMIT_CHANGE_SUCCESSFULLY'", "DIGITAL_CHANNEL_SEC");
                string edit_success_msg = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                if (edit_success_msg != success_message)
                {
                    throw new Exception(String.Format("Success Message in Database :{0} is not equal with Success Message on website :{1} for limit type :{2}", edit_success_msg, success_message, limit_type));
                }

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Edit_Succ_Btn");
                selhelper.Button(Keyword.Locator);

                keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_LimitMngOption");
                selhelper.links(Keyword.Locator);

                dlink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dlink.GetDataTable("Select DAILY_DEBIT_LIMIT from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='2'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
                string new_edited_limit = SourceDataTable.Rows[0][0].ToString();
                dlink = null;
                SourceDataTable = null;

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_Remaining");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                string daily_rem_after_edit = selhelper.ReturnKeywordValue(temp_keyword);
                daily_rem_after_edit = daily_rem_after_edit.Remove(daily_rem_after_edit.Length - 3);
                daily_rem_after_edit = daily_rem_after_edit.Replace(",", "");

                if (Convert.ToInt32(daily_rem_after_edit) != (Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)) - (Convert.ToInt32(old_limit) - new_limit))
                {
                    throw new Exception(String.Format("Website Remaining limit :{0} is not equal with Calculated Remaining amount after edit :{1} for limit type :{2}", daily_rem_limit, ((Convert.ToInt32(old_limit) - Convert.ToInt32(daily_consume_limit_ui)) - (new_limit)), limit_type));
                }

                Keyword = null;
                temp_keyword = null;
                Keyword = ContextPage.GetInstance().GetElement("MyAccount_Limit_daily_limit");
                temp_keyword = Keyword.Locator.Replace("{x}", limit_type);
                selhelper.ScrollToElement(temp_keyword);
                string daily_new_limit = selhelper.ReturnKeywordValue(temp_keyword);
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
                SourceDataTable = dlink.GetDataTable("Select UPDATED_ON from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='2'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
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
                SourceDataTable = dlink.GetDataTable("Select EFFECTIVE_FROM_DATE from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='2'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
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
                SourceDataTable = dlink.GetDataTable("Select EFFECTIVE_TO_DATE from DC_CUSTOM_LIMIT_RULES a where A.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where L.CUSTOMER_NAME = '" + context.GetUsername() + "') and A.CHANNEL_ID='2'  order by A.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
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
                SourceDataTable = dlink.GetDataTable("Select UPDATED_ON, CREATED_ON, STATUS, IVR_ATTRIBUTE2, IVR_ATTRIBUTE3 from dc_transaction q where Q.CNIC= (Select CNIC from dc_customer_info a where A.CUSTOMER_NAME ='" + context.GetUsername() + "') and Q.LEAD_FIELD1='" + limit_type + "' and Q.CHANNEL_ID='2' order by Q.UPDATED_ON desc", "DIGITAL_CHANNEL_SEC");
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

                if (dc_ivr1 != old_limit)
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
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [Then(@"I want to verify already added beneficiaries with query ""(.*)"" of Schema ""(.*)"" on keyword ""(.*)""")]
        public void WhenIWantToVerifyAlreadyAddedBeneficiariesWithQueryOfSchemaOnKeyword(string query, string schema, string keyword)
        {

            if (query.Contains("{customer_name}"))
            {
                query = query.Replace("{customer_name}", context.GetUsername());
            }

            SeleniumHelper selhelper = new SeleniumHelper();
            Element Keyword = ContextPage.GetInstance().GetElement(keyword);
            try
            {
                if (Keyword.Locator.Contains("send-money"))
                {
                    string count_query = "select count(*) from DC_FUND_TRANSFER_BENEFICIARY b where B.IS_DELETED = 0 and B.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info k where k.customer_name ='" + context.GetUsername() + "')";
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(count_query, schema);
                    string count = SourceDataTable.Rows[0][0].ToString();
                    dlink = null;
                    SourceDataTable = null;

                    if (count == "0")
                    {
                        Element temp_keyword = ContextPage.GetInstance().GetElement("BeneManage_SendMoney_ZeroCountText");
                        string null_text = @"To Transfer, you need to first add beneficiary. Please press ""Add New"" button to proceed";
                        string null_text_ui = selhelper.ReturnKeywordValue(temp_keyword.Locator);
                        Assert.AreEqual(null_text, null_text_ui);
                    }
                    else
                    {
                        Dictionary<string, Tuple<string, string>> db_dict = new Dictionary<string, Tuple<string, string>>();
                        Dictionary<string, Tuple<string, string>> ui_dict = new Dictionary<string, Tuple<string, string>>();

                        dlink = new DataAccessComponent.DataAccessLink();
                        SourceDataTable = dlink.GetDataTable(query, schema);

                        for (int j = 0; j < Convert.ToInt32(count); j++)
                        {
                            string db_acc_no = (SourceDataTable.Rows[j][0].ToString()).Trim();
                            string db_acc_title = (SourceDataTable.Rows[j][1].ToString()).Trim();
                            string db_nick = (SourceDataTable.Rows[j][2].ToString()).Trim();

                            db_dict.Add(db_acc_no, Tuple.Create(db_acc_title, db_nick));

                        }

                        int size = selhelper.SizeCountElements(Keyword.Locator);
                        for (int i = 1; i <= size; i++)
                        {

                            Element Keyword2 = ContextPage.GetInstance().GetElement("BeneManage_SendMoney_Nick");
                            string locator = Keyword2.Locator.Replace("{i}", Convert.ToString(i));
                            selhelper.ScrollToElement(locator);
                            string nick = selhelper.ReturnKeywordValue(locator);
                            nick = nick.Trim();
                            Keyword2 = null;
                            locator = null;

                            Keyword2 = ContextPage.GetInstance().GetElement("BeneManage_SendMoney_Acc_Title");
                            locator = Keyword2.Locator.Replace("{i}", Convert.ToString(i));
                            string account_title = selhelper.ReturnKeywordValue(locator);
                            account_title = account_title.Trim();
                            Keyword2 = null;
                            locator = null;

                            Keyword2 = ContextPage.GetInstance().GetElement("BeneManage_SendMoney_Acc_No");
                            locator = Keyword2.Locator.Replace("{i}", Convert.ToString(i));
                            string account_no = selhelper.ReturnKeywordValue(locator);
                            account_no = account_no.Trim();
                            Keyword2 = null;
                            locator = null;

                            ui_dict.Add(account_no, Tuple.Create(account_title, nick));
                        }
                        if (db_dict.Count != ui_dict.Count)
                        {
                            throw new Exception(string.Format("The Number of Beneficiaries are not equal in Datab base :{0} and Website :{1}", db_dict.Count, ui_dict.Count));
                        }
                        var db_dictKeys = db_dict.Select(item => item.Key);
                        var ui_dictKeys = ui_dict.Select(item => item.Key);

                        if (!db_dictKeys.All(key => ui_dictKeys.Contains(key)))
                        {
                            throw new Exception(string.Format("The Set of Account Numbers are not same in Datab base :{0} and Website :{1}", db_dict.Keys, ui_dict.Keys));
                        }
                        foreach (var item in db_dict)
                        {
                            // Get first items
                            var string1_Dict1 = item.Value.Item1;
                            var string1_Dict2 = ui_dict[item.Key].Item1;

                            // Check first items
                            if (!string1_Dict1.Equals(string1_Dict2))
                            {
                                throw new Exception(string.Format("The Beneficiary Title for Account No :{0} in Datab base :{1} is not equal with Beneficiary title on Website :{2}", item.Key, string1_Dict1, string1_Dict2));
                            }

                            // Get second items
                            var string2_Dict1 = item.Value.Item2;
                            var string2_Dict2 = ui_dict[item.Key].Item2;

                            // Check second items
                            if (!string2_Dict1.Equals(string2_Dict2))
                            {
                                throw new Exception(string.Format("The Beneficiary Nick for Account No :{0} in Datab base :{1} is not equal with Beneficiary title on Website :{2}", item.Key, string2_Dict1, string2_Dict2));
                            }
                        }

                        //var tmp = db_dict.Where(x => ui_dict.Any(z => x.Key == z.Key && x.Value.Item1 == z.Value.Item1)
                        //&& !ui_dict.Any(z => x.Key == z.Key && x.Value.Item1 == z.Value.Item1 && x.Value.Item2 == z.Value.Item2));
                    }
                }
                else if (Keyword.Locator.Contains("pay"))
                {
                    string bill_count_query = "select count (distinct COMPANY_SUB_CATEGORY) from DC_BILL_PAYMENT_BENEFICIARY b where B.IS_ACTIVE = 1 and B.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where l.customer_name = '" + context.GetUsername() + "')";
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(bill_count_query, schema);
                    string count = SourceDataTable.Rows[0][0].ToString();
                    dlink = null;
                    SourceDataTable = null;

                    if (count == "0")
                    {
                        Element temp_keyword = ContextPage.GetInstance().GetElement("BeneManage_Pay_ZeroCountText");
                        string null_text = @"No payee is added please add payee from ""Make New Payment"" option";
                        string null_text_ui = selhelper.ReturnKeywordValue(temp_keyword.Locator).Trim();
                        Assert.AreEqual(null_text, null_text_ui);
                    }
                    else
                    {
                        Element temp_Keyword = ContextPage.GetInstance().GetElement("BeneManage_Pay_Bene_categSize");
                        int temp_size = selhelper.SizeCountElements(temp_Keyword.Locator);

                        string[] sub_category_dbvalue = new string[Convert.ToInt32(count)];
                        string[] sub_category_uivalue = new string[temp_size];

                        if (Convert.ToInt32(count) != temp_size)
                        {
                            throw new Exception(string.Format("The COMPANY_SUB_CATEGORY count in Database :{0} is not equal with the count on website :{1}", count, temp_size));
                        }
                        else
                        {
                            for (int k = 0; k < Convert.ToInt32(count); k++)
                            {
                                string catg_query = "select  distinct COMPANY_SUB_CATEGORY from DC_BILL_PAYMENT_BENEFICIARY B  where B.IS_ACTIVE = 1 and B.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where l.customer_name = '" + context.GetUsername() + "')";
                                dlink = new DataAccessComponent.DataAccessLink();
                                SourceDataTable = dlink.GetDataTable(catg_query, schema);
                                string catg_db_value = SourceDataTable.Rows[k][0].ToString().Trim();
                                sub_category_dbvalue[k] = catg_db_value;

                                temp_Keyword = null;
                                temp_Keyword = ContextPage.GetInstance().GetElement("BeneManage_Pay_CatgValue");
                                string loc = temp_Keyword.Locator.Replace("{k}", Convert.ToString(k + 1));
                                selhelper.ScrollToElement(loc);
                                loc = selhelper.ReturnKeywordValue(loc);
                                sub_category_uivalue[k] = loc.Trim();
                            }
                            if (!Enumerable.SequenceEqual(sub_category_dbvalue.OrderBy(t => t), sub_category_uivalue.OrderBy(t => t)))
                            {
                                throw new Exception(string.Format("The COMPANY_SUB_CATEGORY values in Database :{0} is not equal with the value on website :{1}", sub_category_dbvalue, sub_category_uivalue));
                            }
                        }
                        Dictionary<string, Tuple<string, string>> db_dict = new Dictionary<string, Tuple<string, string>>();
                        Dictionary<string, Tuple<string, string>> ui_dict = new Dictionary<string, Tuple<string, string>>();

                        foreach (string item in sub_category_dbvalue)
                        {
                            string query2 = query.Replace("{company_sub_category}", item);

                            dlink = null;
                            SourceDataTable = null;
                            string item_query = "select count(*) from DC_BILL_PAYMENT_BENEFICIARY B  where B.IS_ACTIVE = 1 and B.CUSTOMER_INFO_ID = (Select CUSTOMER_INFO_ID from dc_customer_info l where l.customer_name = '" + context.GetUsername() + "') and B.COMPANY_SUB_CATEGORY = '" + item + "'";
                            dlink = new DataAccessComponent.DataAccessLink();
                            SourceDataTable = dlink.GetDataTable(item_query, schema);
                            int item_count = Convert.ToInt32(SourceDataTable.Rows[0][0].ToString());

                            for (int z = 0; z < item_count; z++)
                            {
                                dlink = new DataAccessComponent.DataAccessLink();
                                SourceDataTable = dlink.GetDataTable(query2, schema);
                                string db_consumer_no = SourceDataTable.Rows[z][0].ToString().Trim();
                                string db_company_name = SourceDataTable.Rows[z][1].ToString().Trim();
                                string db_bill_bene_nick = SourceDataTable.Rows[z][2].ToString().Trim();
                                if (db_bill_bene_nick == "")
                                {
                                    db_bill_bene_nick = item;
                                }
                                db_dict.Add(db_consumer_no, Tuple.Create(db_company_name, db_bill_bene_nick));

                                Element Keyword2 = ContextPage.GetInstance().GetElement("BeneManage_Pay_Nick");
                                string locator = Keyword2.Locator.Replace("{j}", Convert.ToString(z + 1));
                                locator = locator.Replace("{sub_catg_value}", item);
                                selhelper.ScrollToElement(locator);
                                string bill_bene_nick = selhelper.ReturnKeywordValue(locator);
                                bill_bene_nick = bill_bene_nick.Trim();
                                Keyword2 = null;
                                locator = null;

                                Keyword2 = ContextPage.GetInstance().GetElement("BeneManage_Pay_CompanyName");
                                locator = Keyword2.Locator.Replace("{j}", Convert.ToString(z + 1));
                                locator = locator.Replace("{sub_catg_value}", item);
                                string company_name = selhelper.ReturnKeywordValue(locator);
                                company_name = company_name.Trim();
                                Keyword2 = null;
                                locator = null;

                                Keyword2 = ContextPage.GetInstance().GetElement("BeneManage_Pay_ConsumerNo");
                                locator = Keyword2.Locator.Replace("{j}", Convert.ToString(z + 1));
                                locator = locator.Replace("{sub_catg_value}", item);
                                string consumer_no = selhelper.ReturnKeywordValue(locator);
                                consumer_no = consumer_no.Trim();
                                Keyword2 = null;
                                locator = null;

                                ui_dict.Add(consumer_no, Tuple.Create(company_name, bill_bene_nick));
                            }
                        }

                        if (db_dict.Count != ui_dict.Count)
                        {
                            throw new Exception(string.Format("The Number of Beneficiaries are not equal in Datab base :{0} and Website :{1}", db_dict.Count, ui_dict.Count));
                        }
                        var db_dictKeys = db_dict.Select(item => item.Key);
                        var ui_dictKeys = ui_dict.Select(item => item.Key);

                        if (!db_dictKeys.All(key => ui_dictKeys.Contains(key)))
                        {
                            throw new Exception(string.Format("The Set of Consumer Numbers are not same in Datab base :{0} and Website :{1}", db_dict.Keys, ui_dict.Keys));
                        }
                        foreach (var item in db_dict)
                        {
                            // Get first items
                            var string1_Dict1 = item.Value.Item1;
                            var string1_Dict2 = ui_dict[item.Key].Item1;

                            // Check first items
                            if (!string1_Dict1.Equals(string1_Dict2))
                            {
                                throw new Exception(string.Format("The Comapny Name for Account No :{0} in Datab base :{1} is not equal with Comapny Name on Website :{2}", item.Key, string1_Dict1, string1_Dict2));
                            }

                            // Get second items
                            var string2_Dict1 = item.Value.Item2;
                            var string2_Dict2 = ui_dict[item.Key].Item2;

                            // Check second items
                            if (!string2_Dict1.Equals(string2_Dict2))
                            {
                                throw new Exception(string.Format("The Beneficiary Nick for Consumer No :{0} in Datab base :{1} is not equal with Beneficiary title on Website :{2}", item.Key, string2_Dict1, string2_Dict2));
                            }
                        }
                    }
                }
            }

            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        //    else if (Keyword.Locator.Contains("pay"))
        //    {
        //        
        //}
        //catch (Exception exception)
        //{
        //    SeleniumHelper.TakeScreenshot();
        //    throw new AssertFailedException(exception.Message);
        //}

        [Given(@"verify the message ""(.*)"" with decoding the hash on database ""(.*)"" on ""(.*)""")]
        [When(@"verify the message ""(.*)"" with decoding the hash on database ""(.*)"" on ""(.*)""")]
        [Then(@"verify the message ""(.*)"" with decoding the hash on database ""(.*)"" on ""(.*)""")]
        public void ThenVerifyTheMessageWithDecodingTheHashOnDatabaseOn(string value, string query, string schema)
        {
            if (query != "")
            {
                if (query.Contains("{customer_name}"))
                {
                    query = query.Replace("{customer_name}", context.GetUsername());
                }
            }

            string dec = HashPassword.PasswordEncryptor(value);

            DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dlink.GetDataTable(query, schema);
            string hash = SourceDataTable.Rows[0][0].ToString();

            Assert.AreEqual(hash, dec);

            //string encryptstring = BCrypt.Net.BCrypt.HashString(value, Convert.ToInt16(BCrypt.Net.BCrypt.GenerateSalt(31)));
            //string temp = BCrypt.Net.BCrypt.HashPassword(value, Convert.ToInt32(BCrypt.Net.BCrypt.GenerateSalt(31)));


        }

        [When(@"I verify user Mutual Fund status on schema ""(.*)""")]
        public void WhenIVerifyUserMutualFundStatusOnSchema(string schema)
        {
            try
            {
                SeleniumHelper selhelper = new SeleniumHelper();
                int size = 0;
                Double amount_ui = 0;
                Double amount_db = 0;

                if (context.GetCustomerProfileID() == null)
                {
                    Element Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_ErrorMsg");
                    string message = "No mutual funds are found against your CNIC";
                    Assert.AreEqual(message, selhelper.ReturnKeywordValue(Keyword.Locator));

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_ErrorBtn");
                    selhelper.Button(Keyword.Locator);
                }
                else
                {
                    Element Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_StatementTab");
                    selhelper.links(Keyword.Locator);

                    Keyword = null;
                    Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_TableSize");
                    size = selhelper.SizeCountElements(Keyword.Locator);

                    string[,] array_mutual_fund_ui = new string[size, 6];

                    for (int i = 1; i <= size; i++)
                    {
                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_Fund_NameLoc");
                        string fund_name_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));

                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_FolioLoc");
                        string folio_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));

                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_BalanceLoc");
                        string balance_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));

                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_unit_loc");
                        string unit_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));

                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_nav_loc");
                        string nav_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));

                        Keyword = null;
                        Keyword = ContextPage.GetInstance().GetElement("Investment_MutualFund_nav_date_loc");
                        string nav_date_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));

                        string fund_name = selhelper.ReturnKeywordValue(fund_name_loc).Trim();
                        string folio_no = selhelper.ReturnKeywordValue(folio_loc).Trim();
                        string balance = selhelper.ReturnKeywordValue(balance_loc);
                        balance = balance.Replace("PKR", "").Trim();
                        balance = balance.Replace(",", "");
                        amount_ui += Convert.ToDouble(balance);
                        string unit = selhelper.ReturnKeywordValue(unit_loc);
                        unit = unit.Replace(",", "").Trim();
                        string nav = selhelper.ReturnKeywordValue(nav_loc).Trim();
                        string nav_date = selhelper.ReturnKeywordValue(nav_date_loc).Trim();
                        nav_date = nav_date.Replace("-", "/");
                        //DateTime temp = Convert.ToDateTime(nav_date);
                        //nav_date = temp.ToString("dd/MM/yyyy");
                        nav = nav.Replace("PKR", "").Trim();
                        //nav = nav_char[0].Trim();

                        array_mutual_fund_ui[i - 1, 0] = fund_name;
                        array_mutual_fund_ui[i - 1, 1] = folio_no;
                        array_mutual_fund_ui[i - 1, 2] = balance;
                        array_mutual_fund_ui[i - 1, 3] = unit;
                        array_mutual_fund_ui[i - 1, 4] = nav;
                        array_mutual_fund_ui[i - 1, 5] = nav_date;

                        fund_name_loc = folio_loc = balance_loc = unit_loc = nav_loc = nav_date_loc = String.Empty;
                        fund_name = folio_no = balance = unit = nav = nav_date = String.Empty;

                    }

                    string new_query = "SELECT PP.NAME_OF_FUND, L.FOLIO_NO, CP.BALANCE, CP.UNITS, PP.NAV_PRICE, PP.NAV_DATE FROM AMC_CUSTOMER_PROFILE L INNER JOIN AMC_CUSTOMER_PORTFOLIO CP ON L.CUSTOMER_PROFILE_ID = CP.CUSTOMER_PROFILE_ID INNER JOIN AMC_PRODUCT_PROFILE PP ON CP.PRODUCT_ID = PP.PRODUCT_ID WHERE L.CNIC = '" + context.GetCustomerCNIC() + "' order by PP.NAME_OF_FUND";
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    DataTable SourceDataTable = dlink.GetDataTable(new_query, schema);
                    int row_count = SourceDataTable.Rows.Count;

                    string[,] array_mutual_fund_db = new string[row_count, 6];

                    for (int i = 0; i < row_count; i++)
                    {
                        string fund_name_db = SourceDataTable.Rows[i][0].ToString();
                        string folio_no_db = SourceDataTable.Rows[i][1].ToString();
                        Double balance_d = Convert.ToDouble(SourceDataTable.Rows[i][2].ToString());
                        balance_d = Math.Round(balance_d, 2);
                        amount_db += balance_d;
                        string balance_db = String.Format("{0:0.00}", balance_d);
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

                        fund_name_db = folio_no_db = balance_db = units_db = nav_db = nav_date_db = String.Empty;
                    }
                    if (array_mutual_fund_db.GetLength(0) != array_mutual_fund_ui.GetLength(0))
                    {
                        throw new Exception(string.Format("The size of customer mutual fund records are not equal with database :{0} and website :{1}", array_mutual_fund_db.GetLength(0), array_mutual_fund_ui.GetLength(0)));
                    }

                    for (int i = 0; i < array_mutual_fund_db.GetLength(0); i++)
                        for (int j = 0; j < array_mutual_fund_db.GetLength(0); j++)
                            if (array_mutual_fund_db[i, j] != array_mutual_fund_ui[i, j])
                            {
                                throw new Exception(string.Format("The value on Website :{0} is not equal with value on database :{1}", array_mutual_fund_db[i, j], array_mutual_fund_ui[i, j]));
                            }

                    if (amount_db != amount_ui)
                    {
                        throw new Exception(string.Format("Total Mutual fund amount in database :{0} is not equal with toal Mutual fund amount on UI :{1}", amount_db, amount_ui));
                    }
                    context.Set_mutual_fund_balance(Convert.ToDecimal(amount_db));
                }
            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }

        }
        [Given(@"I set value in context from database ""(.*)"" as ""(.*)"" on Schema ""(.*)""")]
        [When(@"I set value in context from database ""(.*)"" as ""(.*)"" on Schema ""(.*)""")]
        [Then(@"I set value in context from database ""(.*)"" as ""(.*)"" on Schema ""(.*)""")]
        public void GivenISetValueInContextFromDatabaseAsOnSchema(string query, string attribute, string schema)
        {
            if (String.IsNullOrEmpty(query))
            {
                return;
            }
            try
            {
                if (query.Contains("{customer_name}"))
                {
                    query = query.Replace("{customer_name}", context.GetUsername());
                }
                if (query.Contains("{invest_fund_name}"))
                {
                    query = query.Replace("{invest_fund_name}", context.GetInvestFundName());
                }
                if (query.Contains("DC_TRANSACTION"))
                {
                    query = query + context.GetTransaction_Id() + "'";
                }
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                string db_value = SourceDataTable.Rows[0][0].ToString();
                if (attribute == "fund_disclaimer_popup")
                {
                    context.SetFundDisclaimerPopup(db_value);
                }
                if (attribute == "term_dep_ref_no")
                {
                    context.SetTermRefNo(db_value);
                }
                if (attribute == "schedule_config")
                {
                    context.SetScheduleConfig(db_value);
                }
                if (attribute == "cust_profile_id")
                {
                    context.SetCustomerProfileID(db_value);
                }
                if(attribute == "user_schedule_count")
                {
                    context.SetUserScheduleCount(Convert.ToInt32(db_value));
                }
                if (attribute == "GUID")
                {
                    context.Set_HostReferenceNo(db_value);
                }
                if (attribute == "IS_SI_Allowed")
                {
                    context.Set_IS_SI_Allowed(db_value);
                }
                if (attribute == "IS_PAID_REQ")
                {
                    context.SetIsPaidReq(db_value);
                }
                if (attribute == "IS_Partial_Payment")
                {
                    context.Set_Is_Partial_Allow(db_value);
                }
            }
            catch (Exception exception)
            {
                throw new AssertFailedException(exception.Message);
            }
        }
        [Given(@"I am verifying schedule payments from My Account")]
        [When(@"I am verifying schedule payments from My Account")]
        [Then(@"I am verifying schedule payments from My Account")]
        public void WhenIAmVerifyingSchedulePaymentsFromMyAccount()
        {
            string fund_transfer_id = ""; string ui_nick = "" ; string ui_amount = "";
            string schedule_id = ""; string ui_purpose = ""; string ui_acc_detail = "";
            string schedule_amount = ""; string temp_loc = ""; string acc_detail = "";
            string nick = ""; string branch_name = ""; string acc_title = "";
            string acc_no = ""; string purpose_db = ""; string frequency_type = "";
            string from_acc_db = ""; string first_date_db = ""; string last_date_db = "";
            string schedule_img = ""; string failed_img = ""; string cancelled_img = "";
            string successful_img = "";

            SeleniumHelper selhelper = new SeleniumHelper();
            Element Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_SendMoneyCount");
            int send_money_count = selhelper.SizeCountElements(Keyword.Locator);

            Keyword = null;
            Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_BillPayCount");
            int bill_pay_count = selhelper.SizeCountElements(Keyword.Locator);

            int schedular_count = send_money_count + bill_pay_count;

            DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
            DataTable SourceDataTable = dLink.GetDataTable("SELECT count(*) FROM DC_SCHEDULED_TRAN_MASTER TM where TM.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "') and TM.LAST_EXECUTION_DATE > sysdate and TM.IS_DELETED = 0 and TM.BILL_BENEFICIARY_ID = 0", "DIGITAL_CHANNEL_SEC");
            int db_send_count = Convert.ToInt32(SourceDataTable.Rows[0][0].ToString());
            dLink = null;
            SourceDataTable = null;

            dLink = new DataAccessComponent.DataAccessLink();
            SourceDataTable = dLink.GetDataTable("SELECT count(*) FROM DC_SCHEDULED_TRAN_MASTER TM where TM.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "') and TM.LAST_EXECUTION_DATE > sysdate and TM.IS_DELETED = 0 and FUND_TRANSFER_BENEFICIARY_ID = 0", "DIGITAL_CHANNEL_SEC");
            int db_bill_pay_count = Convert.ToInt32(SourceDataTable.Rows[0][0].ToString());
            dLink = null;
            SourceDataTable = null;

            int db_schedular_count = db_bill_pay_count + db_send_count;

            string[,] arr_sch_sendmoney_db = new string[db_send_count, 4];
            string[,] arr_sch_sendmoney_ui = new string[send_money_count, 4];

            if (db_send_count != send_money_count)
            {
                throw new Exception(string.Format("User Schedule count in database :{0} is not equal with schedule count on website :{1}", db_send_count, send_money_count));
            }

            if (db_schedular_count == 0 || schedular_count == 0)
            {
                throw new Exception(string.Format("User Schedule count in database :{0} and schedule count on website :{1} is equal to zero", db_send_count, send_money_count));
            }

            for (int i = 1; i <= send_money_count; i++)
            {
                dLink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dLink.GetDataTable("SELECT TM.FUND_TRANSFER_BENEFICIARY_ID, TM.SCHEDULED_TRAN_MASTER_ID, TM.SCHEDULED_AMOUNT, PK.PARAMETER_NAME, LM.PARAMETER_NAME, SOURCE_ACCOUNT_TITLE ,TM.SOURCE_ACCOUNT, SOURCE_ACCOUNT_BRANCH_NAME , FIRST_EXECUTION_DATE , LAST_EXECUTION_DATE FROM DC_SCHEDULED_TRAN_MASTER TM INNER JOIN DC_APPLICATION_PARAM_DETAIL PK on PK.APPLICATION_PARAMETER_ID = TM.PARAM_PURPOSE_OF_PAYMENT_ID INNER JOIN DC_APPLICATION_PARAM_DETAIL LM ON LM.APPLICATION_PARAMETER_ID = TM.PARAM_FREQUENCY_ID where TM.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = '" + context.GetUsername() + "') and TM.LAST_EXECUTION_DATE > sysdate and TM.IS_DELETED = 0 and TM.BILL_BENEFICIARY_ID = 0", "DIGITAL_CHANNEL_SEC");
                fund_transfer_id = SourceDataTable.Rows[i - 1][0].ToString();
                schedule_id = SourceDataTable.Rows[i - 1][1].ToString();
                schedule_amount = SourceDataTable.Rows[i - 1][2].ToString();
                purpose_db = SourceDataTable.Rows[i - 1][3].ToString();
                frequency_type = SourceDataTable.Rows[i - 1][4].ToString();
                from_acc_db = SourceDataTable.Rows[i - 1][5].ToString() + " | " + SourceDataTable.Rows[i - 1][6].ToString() + " | " + SourceDataTable.Rows[i - 1][7].ToString();
                first_date_db = SourceDataTable.Rows[i - 1][8].ToString();
                last_date_db = SourceDataTable.Rows[i - 1][9].ToString();

                if (!(schedule_amount.Contains(".")))
                {
                    schedule_amount = schedule_amount + ".00";
                }

                DateTime temp_date = Convert.ToDateTime(first_date_db);
                first_date_db = temp_date.ToString("dd-MM-yyyy");

                temp_date = Convert.ToDateTime(last_date_db);
                last_date_db = temp_date.ToString("dd-MM-yyyy");

                arr_sch_sendmoney_db[i - 1, 1] = Convert.ToString(schedule_amount);
                arr_sch_sendmoney_db[i - 1, 3] = purpose_db;

                dLink = null;
                SourceDataTable = null;
                dLink = new DataAccessComponent.DataAccessLink();
                SourceDataTable = dLink.GetDataTable("Select NICK, ACCOUNT_TITLE, ACCOUNT_NO, BRANCH_NAME from DC_FUND_TRANSFER_BENEFICIARY LL where LL.FUND_TRANSFER_BENEFICIARY_ID ='" + fund_transfer_id +"'" , "DIGITAL_CHANNEL_SEC");
                nick = SourceDataTable.Rows[0][0].ToString();
                acc_title = SourceDataTable.Rows[0][1].ToString();
                acc_no = SourceDataTable.Rows[0][2].ToString();
                branch_name = SourceDataTable.Rows[0][3].ToString();
                acc_detail = acc_title + " " + acc_no + " " + branch_name;

                arr_sch_sendmoney_db[i - 1, 0] = nick;
                arr_sch_sendmoney_db[i - 1, 2] = acc_detail;

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_RowTitle");
                temp_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));
                ui_nick = selhelper.ReturnKeywordValue(temp_loc);
                ui_nick = ui_nick.Trim();

                Keyword = null;
                temp_loc = null;
                dLink = null;
                SourceDataTable = null;

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_RowAmount");
                temp_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));
                ui_amount = (selhelper.ReturnKeywordValue(temp_loc)).Trim();                

                Keyword = null;
                temp_loc = null;

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_RowPurpose");
                temp_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));
                ui_purpose = selhelper.ReturnKeywordValue(temp_loc);
                ui_purpose = ui_purpose.Trim();

                Keyword = null;
                temp_loc = null;

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_RowDetail");
                temp_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));
                ui_acc_detail = selhelper.ReturnKeywordValue(temp_loc);
                ui_acc_detail = ui_acc_detail.Replace("\r\n", " ");

                Keyword = null;
                temp_loc = null;

                arr_sch_sendmoney_ui[i - 1, 0] = ui_nick;
                arr_sch_sendmoney_ui[i - 1, 1] = Convert.ToString(ui_amount);
                arr_sch_sendmoney_ui[i - 1, 2] = ui_acc_detail;
                arr_sch_sendmoney_ui[i - 1, 3] = ui_purpose;

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_RowClick");
                temp_loc = Keyword.Locator.Replace("{i}", Convert.ToString(i));
                selhelper.links(temp_loc);

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_FromVal");
                string from_value_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (from_acc_db != from_value_ui)
                {
                    throw new Exception(string.Format("From Account value in Database :{0} is not equal with From Account value on website :{1}", from_acc_db, from_value_ui));
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_TOVal");
                string to_value_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (acc_detail != to_value_ui)
                {
                    throw new Exception(string.Format("To Account value in Database :{0} is not equal with To Account value on website :{1}", acc_detail, to_value_ui));
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_FreqVal");
                string frequency_value_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (frequency_value_ui != frequency_type)
                {
                    throw new Exception(string.Format("Frequency Type in Database :{0} is not equal with frequency type on website :{1}", frequency_type, frequency_value_ui));
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_PurposeVal");
                string purpose_value_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (purpose_db != purpose_value_ui)
                {
                    throw new Exception(string.Format("Purpose of Payment in Database :{0} is not equal with Purpose of Payment on website :{1}", purpose_db , purpose_value_ui));
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_TypeVal");
                string type_value_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;


                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_AmountVal");
                string amount_value_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (Convert.ToString(schedule_amount) != amount_value_ui)
                {
                    throw new Exception(string.Format("Schedule Amount in Database :{0} is not equal with shcedule amount on website :{1}", schedule_amount, amount_value_ui));
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_FirstDateVal");
                string first_date_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (first_date_db != first_date_ui)
                {
                    throw new Exception(string.Format("First Executaion Date in Database :{0} is not equal with First Executaion Date on website :{1}", first_date_db, first_date_ui));
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_LastDateVal");
                string last_date_ui = selhelper.ReturnTextBoxValue(Keyword.Locator);
                Keyword = null;

                if (last_date_db != last_date_ui)
                {
                    throw new Exception(string.Format("Last Executaion Date in Database :{0} is not equal with Last Executaion Date on website :{1}", last_date_db, last_date_ui));
                }

                Dictionary<string, string> schedule_img_ui_dict = new Dictionary<string, string>();

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_SummaryClick");
                selhelper.links(Keyword.Locator);
                Keyword = null;

                for (int j = 1; j <= 4; j++)
                {
                    Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_Summ_TypeImg");
                    string temp_keyword = Keyword.Locator.Replace("{i}", Convert.ToString(j));
                    if (j == 1)
                    {
                        schedule_img = selhelper.ReturnAttributeValue("src" ,temp_keyword);
                        schedule_img_ui_dict.Add(schedule_img, "Scheduled");

                    }
                    else if (j == 2)
                    {
                        failed_img = selhelper.ReturnAttributeValue("src", temp_keyword);
                        schedule_img_ui_dict.Add(failed_img, "Failed");
                    }
                    else if (j == 3)
                    {
                        cancelled_img = selhelper.ReturnAttributeValue("src", temp_keyword);
                        schedule_img_ui_dict.Add(cancelled_img, "Disabled");
                    }
                    else
                    {
                        successful_img = selhelper.ReturnAttributeValue("src", temp_keyword);
                        schedule_img_ui_dict.Add(successful_img, "Success");
                    }
                    Keyword = null;
                    temp_keyword = null;
                }

                Keyword = ContextPage.GetInstance().GetElement("MyAccount_MngSch_Schedule_Img");
                int temp_count = selhelper.SizeCountElements(Keyword.Locator);

                Element Keyword2 = ContextPage.GetInstance().GetElement("MyAccount_MngSch_Schedule_Date");

                dLink = new DataAccessComponent.DataAccessLink();
                string query = "SELECT JK.PARAMETER_NAME, ZM.EXECUTION_DATE FROM DC_SCHEDULED_TRAN_DETAIL ZM INNER JOIN DC_APPLICATION_PARAM_DETAIL JK ON ZM.PARAM_EXECUTION_STATUS_ID = JK.APPLICATION_PARAMETER_ID WHERE ZM.SCHEDULED_TRAN_MASTER_ID = '" + schedule_id + "' AND ZM.CREATED_BY = (SELECT CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CL WHERE CL.CUSTOMER_NAME = '" + context.GetUsername() + "')  ORDER BY ZM.EXECUTION_DATE asc";
                SourceDataTable = dLink.GetDataTable(query, "DIGITAL_CHANNEL_SEC");

                if (temp_count != SourceDataTable.Rows.Count)
                {
                    throw new Exception(String.Format("Number of scheduled entries in Database :{0} is not equal with Number of Scheduled entries on Wbesite :{1}", SourceDataTable.Rows.Count, temp_count));
                }
                for (int k = 0; k < SourceDataTable.Rows.Count; k++)
                {
                    string temp_keywod_img_status = Keyword.Locator + "[" + Convert.ToString(k + 1) + "]";
                    string temp_keyword_date = Keyword2.Locator + "[" + Convert.ToString(k + 1) + "]";
                    string ui_schedule_date = selhelper.ReturnKeywordValue(temp_keyword_date);
                    string ui_schedule_img = selhelper.ReturnAttributeValue("src", temp_keywod_img_status);
                    string ui_schedule_img_status = "";

                    string db_schedule_status = SourceDataTable.Rows[k][0].ToString();
                    string db_schedule_date = SourceDataTable.Rows[k][1].ToString();
                    temp_date = Convert.ToDateTime(last_date_db);
                    db_schedule_date = temp_date.ToString("dd-MM-yyyy");

                    foreach(var item in schedule_img_ui_dict)
                    {
                        if (item.Key == ui_schedule_img)
                        {
                            ui_schedule_img_status = item.Value;
                            break;
                        }
                    }

                    if (db_schedule_date != ui_schedule_date)
                    {
                        throw new Exception(String.Format("Schedule Date in Database :{0} is not equal with website :{1}", db_schedule_date, ui_schedule_date));
                    }
                }

                Keyword = ContextPage.GetInstance().GetElement("");
                selhelper.Button(Keyword.Locator);
                Keyword = null;

                schedule_amount = ui_amount = ui_nick = ui_purpose = ui_acc_detail = temp_loc = acc_detail = nick = branch_name = 
                acc_title = acc_no = purpose_db  = String.Empty;
                schedule_img_ui_dict.Clear();
            }
            for (int i = 0; i < arr_sch_sendmoney_db.GetLength(0); i++)
                for (int j = 0; j < arr_sch_sendmoney_ui.GetLength(0); j++)
                    if (arr_sch_sendmoney_db[i, j] != arr_sch_sendmoney_ui[i, j])
                    {
                        throw new Exception(string.Format("The value on Website :{0} is not equal with value on database :{1}", arr_sch_sendmoney_db[i, j], arr_sch_sendmoney_ui[i, j]));
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

            for (int i = consumer_no_arr.Length - 1; i >= 0; i--)
            {
                if (a <= consumer_no_arr.Length)
                {

                    Element temp_keyword = ContextPage.GetInstance().GetElement("Pay_MultiBill_Services_row");
                    string temp_key = temp_keyword.Locator.Replace("{k}", Convert.ToString(a));
                    selhelper.links(temp_key);

                    string tranid_keyword = "Forget_PasswordTranID";
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
        [Then(@"I set list of elements from scroll view on ""(.*)""")]
        public void ThenISetListOfElementsFromScrollViewOnAs(string Keyword)
        {
            Element keyword = ContextPage.GetInstance().GetElement(Keyword);
            SeleniumHelper selhelper = new SeleniumHelper();
            //List<string> lst = new List<string>();
            List<string> lst_ui = new List<string>();

            try
            {
                int lst_count = selhelper.SizeCountElements(keyword.Locator);
                for (int i = 1; i <= lst_count; i++)
                {
                    string temp_keyword = "(" + keyword.Locator + ")[" + Convert.ToString(i) + "]";
                    selhelper.ScrollToElement(temp_keyword);
                    string list_element = selhelper.ReturnKeywordValue(temp_keyword);
                    lst_ui.Add(list_element);
                    //string[] other_elements = lst_ui.Except(lst).ToArray();
                    //int result = lst_ui.Count(s => s != "");
                    //int scroll_count = lst_ui.Count - result;
                    //for (int j = 0; j < other_elements.Length; j++)
                    //{
                    //    lst.Add(other_elements[j]);
                    //}
                    list_element = null;
                }
                //if (Keyword == "Investment_MutualFund_FundList")
                //{
                //    lst.RemoveAt(0);
                //}
                context.Set_scroll_items_list(lst_ui);

            }
            catch (Exception exception)
            {
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [Then(@"verify the list using ""(.*)"" on Schema ""(.*)""")]
        public void ThenVerifyTheListUsingOnSchema(string query, string schema)
        {
            try
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
                    if (query.Contains("SELECT PP.NAME_OF_FUND from QAT_AMC.AMC_PRODUCT_PROFILE"))
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
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [Then(@"I check values of combobox using database from ""(.*)"" on schema (.*) on combobox ""(.*)"" of list ""(.*)""")]
        public void ThenICheckValuesOfComboboxUsingDatabaseFromOnSchemaOnComboboxOfList(string query, string schema, string Keyword, string Lst_Keyword)
        {
            try
            {
                if (query.Contains("{customer_cnic}"))
                {
                    query = query.Replace("{customer_cnic}", context.GetCustomerCNIC());
                }
                SeleniumHelper selhelper = new SeleniumHelper();
                List<string> ui_list = new List<string>();
                DataAccessComponent.DataAccessLink dLink = new DataAccessComponent.DataAccessLink();
                DataTable SourceDataTable = dLink.GetDataTable(query, schema);
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                Element lst_keyword = ContextPage.GetInstance().GetElement(Lst_Keyword);

                ui_list = selhelper.return_combobox_values(keyword.Locator, lst_keyword.Locator);
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
                SeleniumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

    }
}

