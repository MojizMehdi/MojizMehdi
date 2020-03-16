﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace HBLAutomationWeb.Resources.Feature.InternetBanking
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("BillPayment")]
    public partial class BillPaymentFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "BillPayment.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "BillPayment", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("1 As a user i want to Verify login for HBL Web Bill Payment")]
        [NUnit.Framework.CategoryAttribute("BillPayment")]
        [NUnit.Framework.TestCaseAttribute("When user id and password are valid", "abby", "pakistan1", "12345", null, Category="source:Data/IBLogin.xlsx")]
        public virtual void _1AsAUserIWantToVerifyLoginForHBLWebBillPayment(string @case, string login_UserId_Value, string login_Password_Value, string oTP_Value, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "BillPayment"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1 As a user i want to Verify login for HBL Web Bill Payment", @__tags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_UserId\"", login_UserId_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_Password\"", login_Password_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.When("I am performing on \"Login_SignIn_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.And("I wait 3000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_OTP_field\"", oTP_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I am performing on \"Login_OTP_Verify_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("2 As a user i want to Verify Bill Payment through PAY")]
        [NUnit.Framework.CategoryAttribute("BillPayment")]
        [NUnit.Framework.TestCaseAttribute("When valid bill details are provided", "UPDATE LP_BILLS L SET L.BILL_STATUS_ID=1 , L.DUE_DATE=TRUNC(SYSDATE) WHERE L.CONS" +
            "UMER_NO=\'0400000069505\'", "QAT_BPS", "Electricity Bill Payment", "K-ELECTRIC", "0400000069505", "KESC0001", "12345", "pakistan3", "SELECT DT.TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.BILL_COMPANY FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.BILL_CONSUMER_NUMBER FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'" +
            "", "QADB", null, Category="source:Data/IBBillPayment.xlsx")]
        public virtual void _2AsAUserIWantToVerifyBillPaymentThroughPAY(string @case, string status_Query, string dB_Value, string category_Value, string company_Value, string pay_BillPayment_ConsumerNo_Value, string company_Code_Value, string oTP_Value, string tran_Pass_Value, string tran_Amount_Query, string from_Account_Query, string company_Name_Query, string consumer_No_Query, string db_Val, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "BillPayment"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("2 As a user i want to Verify Bill Payment through PAY", @__tags);
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
 testRunner.And(string.Format("update the data by query \"{0}\" on Schema \"{1}\"", status_Query, dB_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("I am clicking on \"Pay_Link\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.When("I am performing on \"Pay_AddNewBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
 testRunner.And(string.Format("I am clicking on link \"{0}\" on \"Pay_BillPaymentCategory\"", category_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And(string.Format("I am clicking on link \"{0}\" on \"Pay_BillPaymentCategory_Company\"", company_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And(string.Format("I have given \"{0}\" on \"Pay_BillPayment_ConsumerNo\"", pay_BillPayment_ConsumerNo_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("I am performing on \"Pay_BillPayment_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("I wait 3000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("I am performing on \"Pay_BillPayment_Inquiry_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("I scroll to element \"Pay_BillPayment_PayBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And(string.Format("I have otp check and given {0} on \"Login_OTP_field\" on company code {1}", oTP_Value, company_Code_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And(string.Format("I have transaction pass check and given {0} on \"Pay_Transaction_PayBill_Transacti" +
                        "onPassword\" on company code {1}", tran_Pass_Value, company_Code_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("I press Enter on \"Pay_BillPayment_PayBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("I wait 10000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.Then("verify through \"Transaction is successful.\" on \"Pay_Transaction_Success\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 41
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Pay_Transaction_Success_Amou" +
                        "nt\"", tran_Amount_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Pay_Transaction_Success_From" +
                        "Account\"", from_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Pay_Transaction_Success_Comp" +
                        "anyName\"", company_Name_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Pay_Transaction_Success_Cons" +
                        "umerNo\"", consumer_No_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("I am clicking on \"Pay_Transaction_ToggleAutoPay\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("I am clicking on \"Pay_Transaction_PayBillAmount_RadioBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.And("I am clicking on \"Pay_Transaction_PayBillAmount_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And("I am clicking on \"Pay_Transaction_PayBillAmount_AgreeBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.And("I am clicking on \"Pay_Transaction_PayBillAmount_CloseBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
