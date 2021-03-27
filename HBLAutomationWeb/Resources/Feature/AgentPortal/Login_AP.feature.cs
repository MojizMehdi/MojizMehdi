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
namespace HBLAutomationWeb.Resources.Feature.AgentPortal
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class Login_APFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "Login_AP.feature"
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Login_AP", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Login_AP")))
            {
                global::HBLAutomationWeb.Resources.Feature.AgentPortal.Login_APFeature.FeatureSetup(null);
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
        
        public virtual void AsAUserIWantToVerifyLoginForAgentPortalWeb(string @case, string expected_Result, string login_Id, string login_Password, string db_Val_AP, string mobile_No_Query, string otp_Text, string agent_Name_Query, string balance_Inquiry_Query, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Login_AP"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("As a user i want to Verify login for Agent Portal Web", @__tags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"AP_username\"", login_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And(string.Format("I set value in context from database \"{0}\" as \"mobile_number\" on Schema \"{1}\"", mobile_No_Query, db_Val_AP), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_APUserId\"", login_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I am performing on \"Login_AP_ID_Btn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_APPassword\"", login_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.When("I am performing on \"Login_APLogin_Btn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.And("I wait 3000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I have given \"\" on \"Login_APOTP_field\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And(string.Format("verify through \"{0}\" on \"Login_APOTP_Txt\"", otp_Text), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I am performing on \"Login_APOTP_Btn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.Then(string.Format("verify through database on \"{0}\" on Schema \"QAT_BB_SYSTEM\" on \"Login_APName_Succe" +
                        "ss_Text\"", agent_Name_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 21
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"QAT_BB_SYSTEM\" on \"Pay_Balance_AP\"", balance_Inquiry_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("As a user i want to Verify login for Agent Portal Web: When user id and password " +
            "are valid for Agent Portal")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Login_AP")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Login_AP")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/AP_Login.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When user id and password are valid for Agent Portal")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When user id and password are valid for Agent Portal")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Expected_Result", "Pass")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_Id", "1342432707")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Login_Password", "Hbl@8080")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:db_val_AP", "QAT_BB_SYSTEM")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:mobile_no_query", "Select MOBILE_NUMBER from BB_AGENT_USER K WHERE K.LOGIN_ID = \'{customer_name}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:otp_text", "Alert! Ap ka Agent k Mobile pr OTP generate hogya hai.")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:agent_name_query", "Select FIRST_NAME from BB_AGENT_USER K WHERE K.LOGIN_ID = \'{customer_name}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:balance_inquiry_query", "Select LM.CREDIT from BB_ACCOUNT LM INNER JOIN BB_AGENT KM ON LM.ACCOUNT_ID = KM." +
            "ACCOUNT_ID INNER JOIN BB_AGENT_USER ZM ON KM.AGENT_ID = ZM.AGENT_ID WHERE ZM.LOG" +
            "IN_ID = \'{customer_name}\'")]
        public virtual void AsAUserIWantToVerifyLoginForAgentPortalWeb_WhenUserIdAndPasswordAreValidForAgentPortal()
        {
#line 7
this.AsAUserIWantToVerifyLoginForAgentPortalWeb("When user id and password are valid for Agent Portal", "Pass", "1342432707", "Hbl@8080", "QAT_BB_SYSTEM", "Select MOBILE_NUMBER from BB_AGENT_USER K WHERE K.LOGIN_ID = \'{customer_name}\'", "Alert! Ap ka Agent k Mobile pr OTP generate hogya hai.", "Select FIRST_NAME from BB_AGENT_USER K WHERE K.LOGIN_ID = \'{customer_name}\'", "Select LM.CREDIT from BB_ACCOUNT LM INNER JOIN BB_AGENT KM ON LM.ACCOUNT_ID = KM." +
                    "ACCOUNT_ID INNER JOIN BB_AGENT_USER ZM ON KM.AGENT_ID = ZM.AGENT_ID WHERE ZM.LOG" +
                    "IN_ID = \'{customer_name}\'", new string[] {
                        "source:Data/AP_Login.xlsx"});
#line hidden
        }
    }
}
#pragma warning restore
#endregion