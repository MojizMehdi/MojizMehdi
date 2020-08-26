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
    public partial class InvestmentsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "Investments.feature"
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Investments", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Investments")))
            {
                global::HBLAutomationWeb.Resources.Feature.InternetBanking.InvestmentsFeature.FeatureSetup(null);
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
        
        public virtual void WhenUserTryToTermDepositThorughInvestments(
                    string @case, 
                    string status_Query, 
                    string status_Query2, 
                    string category_Value, 
                    string deposit_Years_Value, 
                    string account_No, 
                    string profit_Account, 
                    string amount_Value, 
                    string tran_Pass_Value, 
                    string success_Message, 
                    string tran_Type_Query, 
                    string tran_Amount_Query, 
                    string from_Account_Query, 
                    string to_Account_Query, 
                    string db_Val, 
                    string term_Deposit_Type, 
                    string date_Query, 
                    string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Investsments"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When user try to term deposit thorugh Investments", @__tags);
#line 9
this.ScenarioSetup(scenarioInfo);
#line 10
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 11
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I am clicking on \"Login_Dashboard\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I set value in context from data \"1\" as \"term_deposit_flag\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I count Number of Account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.When("I save Account Balances", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.And("I am clicking on \"Investment_Icon\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I am clicking on \"Investment_TermDeposit_Icon\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"TermDepositYears\"", deposit_Years_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I wait 5000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And(string.Format("I select \"{0}\" on \"Investment_ETDRTYpe_List\"", term_Deposit_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("I am clicking on \"Investment_TermDep_Tenure\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And(string.Format("I select \"{0}\" on \"Investment_FromAcc_List\"", account_No), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And(string.Format("I select \"{0}\" on \"Investment_ProfitAcc_List\"", profit_Account), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And(string.Format("I have given \"{0}\" on \"Investment_Amount\"", amount_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("I am performing on \"Investment_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.And("I scroll to element \"Investment_TermDep_ScrollText\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.And("I am performing on \"Investment_TermDep_AcceptBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And(string.Format("I have given \"{0}\" on \"Signup_TransactionPassword\"", tran_Pass_Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("I am performing on \"Investment_TermDep_ReqBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.Then(string.Format("verify through \"{0}\" on \"Investment_TranSuccessMessage\"", success_Message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 31
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranType\"", tran_Type_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranAmount\"", tran_Amount_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranFromAcc\"", from_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranToAcc\"", to_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranDate\"", date_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And("I save Transaction Info", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And("I am performing on \"Investment_TranCloseBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("I am clicking on \"Pay_Transaction_PayBill_Rating\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("I am clicking on \"Investment_TermDep_RatingOkBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("I am clicking on \"Login_Dashboard\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("I verify Account Balance", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("I am clicking on \"Services_Link\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("I am clicking on \"Services_Transaction_Activity\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("I am clicking on \"Services_Last_Transaction\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("verify through \"Successful\" on \"MyAccount_Forgot_TranSuccessMessage\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranType\"", tran_Type_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranAmount\"", tran_Amount_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranFromAcc\"", from_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranToAcc\"", to_Account_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_TranDate\"", date_Query, db_Val), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("I am performing on \"Investment_TranActivityCloseBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("When user try to term deposit thorugh Investments: As a user I want to verify Ter" +
            "m Deposit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Investments")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Investsments")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/ETDR.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "As a user I want to verify Term Deposit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "As a user I want to verify Term Deposit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:status_query", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:status_query2", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Category_Value", "Term Deposit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Deposit_Years_Value", "One Year")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:account_no", "12757900256303")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:profit_account", "02197900643103")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Amount_Value", "25500")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Tran_Pass_Value", "pakistan2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Success_Message", "Your transaction has been processed successfully.")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_type_query", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
            "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
            "TION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_amount_query", "SELECT DT.TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:from_account_query", "SELECT DT.FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:to_account_query", "SELECT DT.TO_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:db_val", "DIGITAL_CHANNEL_SEC")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:term_deposit_type", "HBL Advantage Plus")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:date_query", "SELECT DT.CREATED_ON FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'")]
        public virtual void WhenUserTryToTermDepositThorughInvestments_AsAUserIWantToVerifyTermDeposit()
        {
#line 9
this.WhenUserTryToTermDepositThorughInvestments("As a user I want to verify Term Deposit", "", "", "Term Deposit", "One Year", "12757900256303", "02197900643103", "25500", "pakistan2", "Your transaction has been processed successfully.", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
                    "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
                    "TION_ID = \'", "SELECT DT.TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "SELECT DT.TO_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", "DIGITAL_CHANNEL_SEC", "HBL Advantage Plus", "SELECT DT.CREATED_ON FROM DC_TRANSACTION DT WHERE DT.TRANSACTION_ID = \'", new string[] {
                        "source:Data/ETDR.xlsx"});
#line hidden
        }
        
        public virtual void WhenUserTryToVerifyMutualFund(
                    string @case, 
                    string cust_Profile_Id_Query, 
                    string fund_Name, 
                    string from_Acc, 
                    string amount, 
                    string db_Val3, 
                    string invest_Option, 
                    string disclaimer_Message, 
                    string gl_Account_Query, 
                    string tran_Timing_Query, 
                    string tran_Pass, 
                    string success_Msg, 
                    string disclaimer_Query, 
                    string tran_Type_Query, 
                    string tran_Date_Query, 
                    string tran_Amount_Query, 
                    string from_Acc_Query, 
                    string to_Acc_Query, 
                    string fund_Name_Query, 
                    string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Investsments"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When user try to verify Mutual Fund", @__tags);
#line 60
this.ScenarioSetup(scenarioInfo);
#line 61
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 62
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"invest_fund_name\"", invest_Option), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 64
    testRunner.And(string.Format("I set value in context from database \"{0}\" as \"fund_disclaimer_popup\" on Schema \"" +
                        "QAT_AMC\"", disclaimer_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("I am clicking on \"Login_Dashboard\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("I count Number of Account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.When("I save Account Balances", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
 testRunner.And("I am clicking on \"Investment_Icon\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.And("I am clicking on \"Investment_MutualFund_Icon\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And(string.Format("verify the result from \"{0}\" on Schema \"QAT_AMC\"", cust_Profile_Id_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And(string.Format("I verify user Mutual Fund status on schema \"{0}\"", db_Val3), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.Then("I am clicking on \"Investment_MutualFund_InvestTab\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 73
 testRunner.And("I am clicking on \"Investment_MutualFund_InvestBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
 testRunner.And(string.Format("verify through \"{0}\" on \"Investment_MutualFund_DisPopup\"", disclaimer_Message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
 testRunner.And("I am performing on \"Investment_MutualFund_PopupBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
 testRunner.And(string.Format("I select \"{0}\" on \"Investment_MutualFund_FromAcc\"", from_Acc), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.And(string.Format("verify through \"{0}\" on \"Investment_MutualFund_FUndName\"", invest_Option), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_MutualFund_FundAc" +
                        "cNo\"", gl_Account_Query, db_Val3), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.And(string.Format("I have given \"{0}\" on \"Investment_MutualFund_Amount\"", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"{1}\" on \"Investment_MutualFund_TranTi" +
                        "ming\"", tran_Timing_Query, db_Val3), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("I am performing on \"Investment_MutualFund_NextBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("I scroll to element \"Investment_MutualFund_ScrollTxt\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.And("I am performing on \"Investment_MutualFund_AgreeBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.And("I scroll to element \"Investment_MutualFund_TranPass\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
 testRunner.And(string.Format("I have given \"{0}\" on \"Investment_MutualFund_TranPass\"", tran_Pass), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.And("I am performing on \"Investment_MutualFund_InvestBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("I wait 6000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.And(string.Format("verify through \"{0}\" on \"Investment_MutualFund_TranSuccessMessage\"", success_Msg), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"\"", tran_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"\"", tran_Date_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"\"", tran_Amount_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"\"", from_Acc_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"\"", to_Acc_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"\"", fund_Name_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
    testRunner.And(string.Format("I set value in context from database \"{0}\" as \"fund_disclaimer_popup\" on Schema \"" +
                        "QAT_AMC\"", disclaimer_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 96
 testRunner.And("I am performing on \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
 testRunner.And("I am clicking on \"Services_Link\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
 testRunner.And("I am clicking on \"Services_Transaction_Activity\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 99
 testRunner.And("I am clicking on \"Services_Last_Transaction\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
 testRunner.And("verify through \"Successful\" on \"MyAccount_Forgot_TranSuccessMessage\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"Investment_TranType\"", tran_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"Investment_TranDate\"", tran_Date_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"Investment_TranAmount\"", tran_Amount_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 104
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"Investment_TranToAcc\"", from_Acc_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"Investment_TranToAcc\"", to_Acc_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"<db_val>\" on \"Investment_TranFundName" +
                        "\"", fund_Name_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("When user try to verify Mutual Fund: When user try to verify Mutual Fund Process")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Investments")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Investsments")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/MutualFund.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When user try to verify Mutual Fund Process")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When user try to verify Mutual Fund Process")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:cust_profile_id_query", "SELECT CUSTOMER_PROFILE_ID FROM AMC_CUSTOMER_PROFILE L WHERE L.CNIC=\'{customer_cn" +
            "ic}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:fund_name", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:from_acc", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:amount", "1000")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:db_val3", "QAT_AMC")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:invest_option", "HBL Equity Fund")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:disclaimer_message", @"Disclaimer: You have chosen to invest in Equity Related Scheme (High Risk Category). Your risk profile does not warrant you to invest in Equity Related Scheme. Kindly press ""Next"" to acknowledge that you understand the risk associated with this investment.")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:gl_account_query", "SELECT GL_ACCOUNT_NO FROM AMC_PRODUCT_CHANNEL PP INNER JOIN AMC_PRODUCT_PROFILE C" +
            "P ON PP.PRODUCT_ID = CP.PRODUCT_ID where CP.NAME_OF_FUND = \'{invest_fund_name}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_timing_query", "Select CUT_OFF_TIME from AMC_PRODUCT_PROFILE PP where PP.NAME_OF_FUND = \'{invest_" +
            "fund_name}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_pass", "pakistan2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:success_msg", "You have successfully invested in HBL Mutual Funds, for further assistance, pleas" +
            "e call HBL PhoneBanking on 021 - 111 - 111 - 425")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:disclaimer_query", "Select length(PP.POPUP_DISCLAIMER) from AMC_PRODUCT_PROFILE PP where PP.NAME_OF_F" +
            "UND = \'{invest_fund_name}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_type_query", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
            "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
            "TION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_date_query", "Select CREATED_ON FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSACTI" +
            "ON_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_amount_query", "Select TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.T" +
            "RANSACTION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:from_acc_query", "Select FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSAC" +
            "TION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:to_acc_query", "Select TO_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSACTI" +
            "ON_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:fund_name_query", "Select PRODUCT_CODE FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSAC" +
            "TION_ID = \'")]
        public virtual void WhenUserTryToVerifyMutualFund_WhenUserTryToVerifyMutualFundProcess()
        {
#line 60
this.WhenUserTryToVerifyMutualFund("When user try to verify Mutual Fund Process", "SELECT CUSTOMER_PROFILE_ID FROM AMC_CUSTOMER_PROFILE L WHERE L.CNIC=\'{customer_cn" +
                    "ic}\'", "", "", "1000", "QAT_AMC", "HBL Equity Fund", @"Disclaimer: You have chosen to invest in Equity Related Scheme (High Risk Category). Your risk profile does not warrant you to invest in Equity Related Scheme. Kindly press ""Next"" to acknowledge that you understand the risk associated with this investment.", "SELECT GL_ACCOUNT_NO FROM AMC_PRODUCT_CHANNEL PP INNER JOIN AMC_PRODUCT_PROFILE C" +
                    "P ON PP.PRODUCT_ID = CP.PRODUCT_ID where CP.NAME_OF_FUND = \'{invest_fund_name}\'", "Select CUT_OFF_TIME from AMC_PRODUCT_PROFILE PP where PP.NAME_OF_FUND = \'{invest_" +
                    "fund_name}\'", "pakistan2", "You have successfully invested in HBL Mutual Funds, for further assistance, pleas" +
                    "e call HBL PhoneBanking on 021 - 111 - 111 - 425", "Select length(PP.POPUP_DISCLAIMER) from AMC_PRODUCT_PROFILE PP where PP.NAME_OF_F" +
                    "UND = \'{invest_fund_name}\'", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
                    "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
                    "TION_ID = \'", "Select CREATED_ON FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSACTI" +
                    "ON_ID = \'", "Select TRANSACTION_AMOUNT FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.T" +
                    "RANSACTION_ID = \'", "Select FROM_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSAC" +
                    "TION_ID = \'", "Select TO_ACCOUNT FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSACTI" +
                    "ON_ID = \'", "Select PRODUCT_CODE FROM DC_TRANSACTION DT WHERE DT.CHANNEL_ID=\'2\'and  DT.TRANSAC" +
                    "TION_ID = \'", new string[] {
                        "source:Data/MutualFund.xlsx"});
#line hidden
        }
    }
}
#pragma warning restore
#endregion

