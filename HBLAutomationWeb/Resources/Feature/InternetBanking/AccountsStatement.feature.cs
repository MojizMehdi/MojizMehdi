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
namespace HBLAutomationWeb.Resources.Feature.InternetBanking
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class AccountsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "AccountsStatement.feature"
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Accounts", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Accounts")))
            {
                global::HBLAutomationWeb.Resources.Feature.InternetBanking.AccountsFeature.FeatureSetup(null);
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
        
        public virtual void _1AsAUserIWantToVerifyLoginForHBLWebSendMoney(string @case, string login_UserId_Value, string login_Password_Value, string oTP_Value, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Login"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1 As a user i want to Verify login for HBL Web Send Money", @__tags);
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"username\"", login_UserId_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_UserId\"", login_UserId_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_Password\"", login_Password_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.When("I am performing on \"Login_SignIn_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.And("I wait 5000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_OTP_field\"", oTP_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I am performing on \"Login_OTP_Verify_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.Then("verify through \"Welcome\" on \"Login_Success_Text\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("1 As a user i want to Verify login for HBL Web Send Money: When user id and passw" +
            "ord are valid Multi_Bill")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Accounts")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Login")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/IBLogin.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When user id and password are valid Multi_Bill")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When user id and password are valid Multi_Bill")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_UserId_Value", "GANGSTER")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_Password_Value", "pakistan1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:OTP_Value", "12345678")]
        public virtual void _1AsAUserIWantToVerifyLoginForHBLWebSendMoney_WhenUserIdAndPasswordAreValidMulti_Bill()
        {
#line 8
this._1AsAUserIWantToVerifyLoginForHBLWebSendMoney("When user id and password are valid Multi_Bill", "GANGSTER", "pakistan1", "12345678", new string[] {
                        "source:Data/IBLogin.xlsx"});
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("1 As a user i want to Verify login for HBL Web Send Money: When user id and passw" +
            "ord are valid mutual_fund")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Accounts")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Login")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/IBLogin.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When user id and password are valid mutual_fund")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When user id and password are valid mutual_fund")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_UserId_Value", "MF123")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_Password_Value", "pakistan1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:OTP_Value", "12345678")]
        public virtual void _1AsAUserIWantToVerifyLoginForHBLWebSendMoney_WhenUserIdAndPasswordAreValidMutual_Fund()
        {
#line 8
this._1AsAUserIWantToVerifyLoginForHBLWebSendMoney("When user id and password are valid mutual_fund", "MF123", "pakistan1", "12345678", new string[] {
                        "source:Data/IBLogin.xlsx"});
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("1 As a user i want to Verify login for HBL Web Send Money: When user id and passw" +
            "ord are valid normal_user")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Accounts")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Login")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/IBLogin.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When user id and password are valid normal_user")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When user id and password are valid normal_user")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_UserId_Value", "YASIR113")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_Password_Value", "pakistan1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:OTP_Value", "12345678")]
        public virtual void _1AsAUserIWantToVerifyLoginForHBLWebSendMoney_WhenUserIdAndPasswordAreValidNormal_User()
        {
#line 8
this._1AsAUserIWantToVerifyLoginForHBLWebSendMoney("When user id and password are valid normal_user", "YASIR113", "pakistan1", "12345678", new string[] {
                        "source:Data/IBLogin.xlsx"});
#line hidden
        }
        
        public virtual void AsAUserIWantToVerifyAccountStatementGeneration(string @case, string db_Value, string query, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "AccountsStatement"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("As a user i want to Verify Account statement generation", @__tags);
#line 25
 this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And("I am clicking on \"Accounts_Link\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("I am clicking on \"Accounts_FromDate\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.When("I select date \"1\" on month \"Jan\" on year \"2020\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.And("I am clicking on \"Accounts_ToDate\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("I select date \"11\" on month \"Mar\" on year \"2020\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And(string.Format("I want value from textbox \"Accounts_NoOfDays\" on database \"{0}\" as \"{1}\"", db_Value, query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("I have given \"100\" on \"Accounts_NoOfDays\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("I am performing on \"Accounts_Generate_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And("I sleep 5000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.Then("I am performing on \"Accounts_CSV_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("As a user i want to Verify Account statement generation:  As a user i want to Ver" +
            "ify Account statement generation")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Accounts")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("AccountsStatement")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/AccountsStatement.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", " As a user i want to Verify Account statement generation")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", " As a user i want to Verify Account statement generation")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:db_value", "DIGITAL_CHANNEL_SEC")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:query", "Select PARAMTER_VALUE from DC_APPLICATION_PARAM_DETAIL I where I.PARAMETER_NAME=\'" +
            "ACC_STATEMENT_MAX_DAY\'")]
        public virtual void AsAUserIWantToVerifyAccountStatementGeneration_AsAUserIWantToVerifyAccountStatementGeneration()
        {
#line 25
 this.AsAUserIWantToVerifyAccountStatementGeneration(" As a user i want to Verify Account statement generation", "DIGITAL_CHANNEL_SEC", "Select PARAMTER_VALUE from DC_APPLICATION_PARAM_DETAIL I where I.PARAMETER_NAME=\'" +
                    "ACC_STATEMENT_MAX_DAY\'", new string[] {
                        "source:Data/AccountsStatement.xlsx"});
#line hidden
        }
    }
}
#pragma warning restore
#endregion
