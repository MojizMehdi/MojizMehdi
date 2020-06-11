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
    public partial class MultiBillPaymentFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "MultiBillPayment.feature"
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "MultiBillPayment", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "MultiBillPayment")))
            {
                global::HBLAutomationWeb.Resources.Feature.InternetBanking.MultiBillPaymentFeature.FeatureSetup(null);
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
        
        public virtual void AsAUserIWantToVerifyMultipleBillPaymentScenario(string @case, string status_Query, string status_Query2, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "MultiBillPayment"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("As a user I want to verify Multiple Bill Payment scenario", @__tags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I set value in context from data \"<Pay_BillPayment_ConsumerNo_Value>\" as \"Consume" +
                    "rNo\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And(string.Format("update the data by query \"{0}\" on QAT_BPS", status_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And(string.Format("update the data by query \"{0}\" on DIGITAL_CHANNEL_SEC", status_Query2), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I am clicking on \"Login_Dashboard\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I count Number of Account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I save Account Balances", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.When("I am clicking on \"Pay_Link\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
 testRunner.And("I am clicking on \"Pay_MultiBillIcon\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("As a user I want to verify Multiple Bill Payment scenario: When valid bill detail" +
            "s are provided 05151110478500")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MultiBillPayment")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("MultiBillPayment")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/MultiBillPayment.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When valid bill details are provided 05151110478500")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When valid bill details are provided 05151110478500")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:status_query", "UPDATE LP_BILLS L SET L.BILL_STATUS_ID=1 , L.DUE_DATE=TRUNC(SYSDATE) WHERE L.CONS" +
            "UMER_NO=\'{ConsumerNo}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:status_query2", @"BEGIN UPDATE DC_SCHEDULED_TRAN_MASTER STM SET STM.STATE = 46 , STM.IS_DELETED = 1 WHERE STM.BILL_BENEFICIARY_ID = (SELECT BPB.BENEFICIARY_ID FROM DC_BILL_PAYMENT_BENEFICIARY BPB WHERE BPB.CONSUMER_NUMBER = '{ConsumerNo}' AND BPB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'YASIR113') AND BPB.IS_ACTIVE = 1);UPDATE DC_BILL_PAYMENT_BENEFICIARY DPB SET DPB.IS_SI_SCHEDULED = 0,DPB.IS_ACTIVE = 0 WHERE DPB.CONSUMER_NUMBER = '{ConsumerNo}' AND DPB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'YASIR113') AND DPB.IS_ACTIVE = 1;COMMIT;END;")]
        public virtual void AsAUserIWantToVerifyMultipleBillPaymentScenario_WhenValidBillDetailsAreProvided05151110478500()
        {
#line 7
this.AsAUserIWantToVerifyMultipleBillPaymentScenario("When valid bill details are provided 05151110478500", "UPDATE LP_BILLS L SET L.BILL_STATUS_ID=1 , L.DUE_DATE=TRUNC(SYSDATE) WHERE L.CONS" +
                    "UMER_NO=\'{ConsumerNo}\'", @"BEGIN UPDATE DC_SCHEDULED_TRAN_MASTER STM SET STM.STATE = 46 , STM.IS_DELETED = 1 WHERE STM.BILL_BENEFICIARY_ID = (SELECT BPB.BENEFICIARY_ID FROM DC_BILL_PAYMENT_BENEFICIARY BPB WHERE BPB.CONSUMER_NUMBER = '{ConsumerNo}' AND BPB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'YASIR113') AND BPB.IS_ACTIVE = 1);UPDATE DC_BILL_PAYMENT_BENEFICIARY DPB SET DPB.IS_SI_SCHEDULED = 0,DPB.IS_ACTIVE = 0 WHERE DPB.CONSUMER_NUMBER = '{ConsumerNo}' AND DPB.CUSTOMER_INFO_ID = (SELECT CI.CUSTOMER_INFO_ID FROM DC_CUSTOMER_INFO CI WHERE CI.CUSTOMER_NAME = 'YASIR113') AND DPB.IS_ACTIVE = 1;COMMIT;END;", new string[] {
                        "source:Data/MultiBillPayment.xlsx"});
#line hidden
        }
    }
}
#pragma warning restore
#endregion

