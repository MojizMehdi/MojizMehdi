﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace HBLAutomationAndroid.Resources.Feature.HBLMobile
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class LoginFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "Login.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Login", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Login")))
            {
                global::HBLAutomationAndroid.Resources.Feature.HBLMobile.LoginFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(TestContext);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void WhenUserTryToLoginMobileBanking(string @case, string login_UserId_Value, string login_Password_Value, string oTP_Value, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "mytag"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When user try to login mobile banking", @__tags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("the user is arrive to Mobile Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When(string.Format("I have given \"{0}\" on \"Login_UserId\"", login_UserId_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_Password\"", login_Password_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I wait 2000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I am performing on \"Login_SignIn_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I wait 40000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("When user try to login mobile banking: When user id and password are valid")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Login")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("mytag")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/HBLMobileLogin.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When user id and password are valid")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When user id and password are valid")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_UserId_Value", "abby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_Password_Value", "pakistan1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:OTP_Value", "12345678")]
        public virtual void WhenUserTryToLoginMobileBanking_WhenUserIdAndPasswordAreValid()
        {
#line 7
this.WhenUserTryToLoginMobileBanking("When user id and password are valid", "abby", "pakistan1", "12345678", new string[] {
                        "source:Data/HBLMobileLogin.xlsx"});
#line hidden
        }
        
        public virtual void WhenUserTryToSendMoneyMobile(
                    string @case, 
                    string status_Query, 
                    string from_Account_Value, 
                    string bank_Value, 
                    string account_Number_Value, 
                    string amount_Value, 
                    string purposeOfPayment_Value, 
                    string bene_Nick, 
                    string bene_Mobile_No, 
                    string bene_Email, 
                    string oTP_Value, 
                    string tran_Pass_Value, 
                    string success_Message, 
                    string tran_Type_Query, 
                    string tran_Amount_Query, 
                    string from_Account_Query, 
                    string to_Account_Query, 
                    string to_Bank_Query, 
                    string bene_Name_Query, 
                    string purpose_Query, 
                    string db_Val, 
                    string count_Query, 
                    string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "mytag"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When user try to send money mobile", @__tags);
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.And(string.Format("update the data by query \"{0}\" on DIGITAL_CHANNEL_SEC", status_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("the user is arrive to Mobile Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.When("I am clicking on \"SendMoney_Link\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.And("I am clicking on \"SendMoney_AddNewBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And(string.Format("I select \"{0}\" on \"SendMoney_FromAccount\"", from_Account_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And(string.Format("I select \"{0}\" on \"SendMoney_Bank\"", bank_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_ToAccount\"", account_Number_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("I scroll down", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_Amount\"", amount_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.When(string.Format("I select \"{0}\" on \"SendMoney_PurposeOfPayment\"", purposeOfPayment_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.And("I scroll down", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_BeneNick\"", bene_Nick), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_BeneMobileNo\"", bene_Mobile_No), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_BeneEmail\"", bene_Email), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("I wait 2000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("I am performing on \"SendMoney_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("I wait 3000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("I scroll down", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("I wait 2000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_OTP\"", oTP_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("I wait 2000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("I am performing on \"SendMoney_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.And("I wait 3000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And(string.Format("I have given \"{0}\" on \"SendMoney_TranPass\"", tran_Pass_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.And("I wait 2000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("I am performing on \"SendMoney_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("I wait 3000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("I am clicking on \"SendMoney_Rating\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I am clicking on \"SendMoney_RatingOkBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I am clicking on \"SendMoney_Rating_Feedback_OkBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.Then(string.Format("verify through \"{0}\" on \"SendMoney_TranSuccessMessage\"", success_Message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 56
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"SendMoney_TranType\"", tran_Type_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"SendMoney_TranAmount\"", tran_Amount_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"SendMoney_TranFromAcc\"", from_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"SendMoney_TranToAcc\"", to_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"SendMoney_TranToBank\"", to_Bank_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"SendMoney_TranPurpose\"", purpose_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
 testRunner.And(string.Format("verify the result from \"{0}\" on db \"<dbval>\"", count_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("When user try to send money mobile: When valid Account Details are provided with " +
            "adding new Bene")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Login")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("mytag")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/SendMoney.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When valid Account Details are provided with adding new Bene")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When valid Account Details are provided with adding new Bene")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:status_query", @"BEGIN UPDATE  DC_FUND_TRANSFER_BENEFICIARY TF SET TF.IS_DELETED = 1 WHERE TF.ACCOUNT_NO = '06047900194203' AND TF.CUSTOMER_INFO_ID = (SELECT CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO DCI WHERE DCI.CUSTOMER_NAME = 'ABBY');DELETE FROM DC_DATA_CACHE DC WHERE DC.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'ABBY');COMMIT;END;")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:From_Account_Value", "02197900643103")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Bank_Value", "HBL / Konnect")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Account_Number_Value", "06047900194203")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Amount_Value", "10")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:PurposeOfPayment_Value", "Courier Services")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Bene_Nick", "AliAbbas1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Bene_Mobile_No", "03121223345")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Bene_Email", "aliabb111@gmail.com")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:OTP_Value", "12345678")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Tran_Pass_Value", "pakistan2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Success_Message", "Your transaction has been processed successfully.")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_type_query", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
            "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
            "TION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_amount_query", "SELECT DT.TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:from_account_query", "SELECT DT.FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:to_account_query", "SELECT DT.TO_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:to_bank_query", "SELECT DB.BANK_NAME FROM DC_FUND_TRANSFER_BANK DB WHERE DB.FUND_TRANSFER_BANK_ID " +
            "= (SELECT DT.BANK_ID FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:bene_name_query", "SELECT DT.BENEFICIARY_NAME FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:purpose_query", "SELECT DT.PURPOSE_OF_PAYMENT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:db_val", "DIGITAL_CHANNEL_SEC")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:count_query", "")]
        public virtual void WhenUserTryToSendMoneyMobile_WhenValidAccountDetailsAreProvidedWithAddingNewBene()
        {
#line 22
this.WhenUserTryToSendMoneyMobile("When valid Account Details are provided with adding new Bene", @"BEGIN UPDATE  DC_FUND_TRANSFER_BENEFICIARY TF SET TF.IS_DELETED = 1 WHERE TF.ACCOUNT_NO = '06047900194203' AND TF.CUSTOMER_INFO_ID = (SELECT CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO DCI WHERE DCI.CUSTOMER_NAME = 'ABBY');DELETE FROM DC_DATA_CACHE DC WHERE DC.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'ABBY');COMMIT;END;", "02197900643103", "HBL / Konnect", "06047900194203", "10", "Courier Services", "AliAbbas1", "03121223345", "aliabb111@gmail.com", "12345678", "pakistan2", "Your transaction has been processed successfully.", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
                    "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
                    "TION_ID = \'", "SELECT DT.TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.TO_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DB.BANK_NAME FROM DC_FUND_TRANSFER_BANK DB WHERE DB.FUND_TRANSFER_BANK_ID " +
                    "= (SELECT DT.BANK_ID FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.BENEFICIARY_NAME FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.PURPOSE_OF_PAYMENT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "DIGITAL_CHANNEL_SEC", "", new string[] {
                        "source:Data/SendMoney.xlsx"});
#line hidden
        }
    }
}
#pragma warning restore
#endregion

