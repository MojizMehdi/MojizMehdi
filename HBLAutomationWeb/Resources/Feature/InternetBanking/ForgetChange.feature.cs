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
    public partial class ForgetChangeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "ForgetChange.feature"
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ForgetChange", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "ForgetChange")))
            {
                global::HBLAutomationWeb.Resources.Feature.InternetBanking.ForgetChangeFeature.FeatureSetup(null);
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
        
        public virtual void _2AsAUserIWantToVerifyForgetPassword(string @case, string login_Id, string customer_Type_Query, string cnic_No, string debit_Card, string card_Pin, string credit_Card, string email, string tran_Message_Qeury, string tran_Id_Query, string tran_Type_Query, string tran_Date_Query, string tran_Debit_Query, string password_Change_Req_Query, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ForgetPassword"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("2 As a user i want to verify forget password", @__tags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"customer_cnic\"", cnic_No), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("I am clicking on \"Forget_btn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And(string.Format("I have given \"{0}\" on \"Forget_Login_field\"", login_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.When("I am performing on \"Forget_PasswordNextbtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 14
 testRunner.And(string.Format("verify the result from \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\"", customer_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And(string.Format("I am giving user details based on customer type as \"{0}\" and \"{1}\" and \"{2}\" and " +
                        "\"{3}\" and \"{4}\" on \"Forget_Password_CNIC\" and \"Forget_Password_Debit\" and \"Forge" +
                        "t_Password_PIN\"", cnic_No, debit_Card, card_Pin, credit_Card, email), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("I am performing on \"Forget_PasswordSubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordResponseMsg\"", tran_Message_Qeury), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranID\"", tran_Id_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranDate\"", tran_Date_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranDebit\"", tran_Debit_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranType\"", tran_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And("I am performing on \"Forget_PasswordTranSubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And("update the data by query \"<password_query>\" on DIGITAL_CHANNEL_SEC", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.Then(string.Format("I have given \"{0}\" on \"Login_UserId\"", login_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 25
 testRunner.And("I have given \"<activation_password>\" on \"Login_Password\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.And("I am performing on \"Login_SignIn_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.Then("I have given \"<new_password>\" on \"Forget_PasswordNew\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 28
 testRunner.And("I have given \"<new_password>\" on \"Forget_PasswordNewConfirm\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("I am performing on \"Signup_SubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordResponseMsg\"", tran_Message_Qeury), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranID\"", tran_Id_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranDate\"", tran_Date_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranType\"", tran_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("I am performing on \"Forget_PasswordTranSubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And(string.Format("verify the result from \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\"", password_Change_Req_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.Then(string.Format("I have given \"{0}\" on \"Login_UserId\"", login_Id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 37
 testRunner.And("I have given \"<new_password>\" on \"Login_Password\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("I am performing on \"Signup_SubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("I wait 4000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("I have given \"<OTP_Value>\" on \"Login_OTP_field\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("I am performing on \"Login_OTP_Verify_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.Then("verify through \"Welcome\" on \"Login_Success_Text\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("2 As a user i want to verify forget password: When I am verifying Forget Password" +
            " Scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ForgetChange")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ForgetPassword")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/ForgetPassword.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When I am verifying Forget Password Scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When I am verifying Forget Password Scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:login_id", "YASIR113")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:customer_type_query", "Select I.CUSTOMER_TYPE from dc_customer_info i where I.CNIC =\'{customer_cnic}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:cnic_no", "1350361299161")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:debit_card", "4902860007216383")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:card_pin", "1234")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Credit_card", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:email", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_message_qeury", "Select O.RESPONSE_MESSAGE from dc_transaction o where O.CNIC =\'{customer_cnic}\' o" +
            "rder by CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_id_query", "Select O.TRANSACTION_ID from dc_transaction o where O.CNIC =\'{customer_cnic}\' ord" +
            "er by CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_type_query", "Select O.TRANSACTION_TYPE_ID from dc_transaction o where O.CNIC=\'{customer_cnic}\'" +
            " and O.STATUS=\'Success\' order by CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_date_query", "Select O.created_on from dc_transaction o where O.CNIC =\'{customer_cnic}\' order b" +
            "y CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_debit_query", "Select O.DEBIT_CARD_NUMBER from dc_transaction o where O.CNIC =\'{customer_cnic}\' " +
            "order by CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_change_req_query", "pakistan2")]
        public virtual void _2AsAUserIWantToVerifyForgetPassword_WhenIAmVerifyingForgetPasswordScenario()
        {
#line 7
this._2AsAUserIWantToVerifyForgetPassword("When I am verifying Forget Password Scenario", "YASIR113", "Select I.CUSTOMER_TYPE from dc_customer_info i where I.CNIC =\'{customer_cnic}\'", "1350361299161", "4902860007216383", "1234", "", "", "Select O.RESPONSE_MESSAGE from dc_transaction o where O.CNIC =\'{customer_cnic}\' o" +
                    "rder by CREATED_ON desc", "Select O.TRANSACTION_ID from dc_transaction o where O.CNIC =\'{customer_cnic}\' ord" +
                    "er by CREATED_ON desc", "Select O.TRANSACTION_TYPE_ID from dc_transaction o where O.CNIC=\'{customer_cnic}\'" +
                    " and O.STATUS=\'Success\' order by CREATED_ON desc", "Select O.created_on from dc_transaction o where O.CNIC =\'{customer_cnic}\' order b" +
                    "y CREATED_ON desc", "Select O.DEBIT_CARD_NUMBER from dc_transaction o where O.CNIC =\'{customer_cnic}\' " +
                    "order by CREATED_ON desc", "pakistan2", new string[] {
                        "source:Data/ForgetPassword.xlsx"});
#line hidden
        }
        
        public virtual void _2AsAUserIWantToVerifyChangeLoginID(
                    string @case, 
                    string cnic, 
                    string debit_Card, 
                    string card_Pin, 
                    string credit_Card, 
                    string email, 
                    string new_Login_Id, 
                    string customer_Type_Query, 
                    string success_Msg, 
                    string password_Query, 
                    string activation_Password, 
                    string new_Password, 
                    string success_Message_Password, 
                    string password_Change_Req_Query, 
                    string feedback_Option, 
                    string new_Login_Id_Query, 
                    string password_Policy, 
                    string password_Policy1, 
                    string password_Policy2, 
                    string password_Policy3, 
                    string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ChangeLoginID"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("2 As a user i want to verify Change Login ID", @__tags);
#line 51
this.ScenarioSetup(scenarioInfo);
#line 52
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"customer_cnic\"", cnic), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I am clicking on \"Forget_btn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("I am clicking on \"Forget_ChangeLink\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("I am clicking on \"Forget_ChangeLoginNav\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And(string.Format("I have given \"{0}\" on \"Forget_ChangeCNIC\"", cnic), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.When("I am performing on \"Forget_ChangeLoginSubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
 testRunner.And(string.Format("verify the result from \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\"", customer_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.And(string.Format("I am giving user details based on customer type as \"{0}\" and \"{1}\" and \"{2}\" and " +
                        "\"{3}\" and \"{4}\" on \"Forget_ChangeLogin_NewLogin\" and \"Forget_ChangeLogin_DCard\" " +
                        "and \"Forget_ChangeLogin_PIN\"", new_Login_Id, debit_Card, card_Pin, credit_Card, email), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
 testRunner.And("I am performing on \"Forget_ChangeLogin_SubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
 testRunner.Then(string.Format("verify through \"{0}\" on \"Forget_ChangeSuccessMsg\"", success_Msg), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 64
 testRunner.And("I am performing on \"Forget_ChangeOkBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And(string.Format("verify the message \"{0}\" through database on \"{1}\" on Schema \"DIGITAL_CHANNEL_SEC" +
                        "\"", new_Login_Id, new_Login_Id_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And(string.Format("update the data by query \"{0}\" on DIGITAL_CHANNEL_SEC", password_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.Then("I have given \"<Login_id>\" on \"Login_UserId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 68
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_Password\"", activation_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.And("I am performing on \"Login_SignIn_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And(string.Format("verify through \"{0}\" on \"Signup_PassPolicy\"", password_Policy), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And(string.Format("verify through \"{0}\" on \"Signup_PassPolicy1\"", password_Policy1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And(string.Format("verify through \"{0}\" on \"Signup_PassPolicy2\"", password_Policy2), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And(string.Format("verify through \"{0}\" on \"Signup_PassPolicy3\"", password_Policy3), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
 testRunner.Then(string.Format("I have given \"{0}\" on \"Signup_LoginPassword\"", new_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 75
 testRunner.Then(string.Format("I have given \"{0}\" on \"Signup_LoginPassword\"", new_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 76
 testRunner.And(string.Format("I have given \"{0}\" on \"Signup_ReLoginPassword\"", new_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.And(string.Format("I have given \"{0}\" on \"Signup_TransactionPassword\"", activation_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And(string.Format("I have given \"{0}\" on \"Signup_ReTransactionPassword\"", activation_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.And("I am performing on \"Signup_SubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
 testRunner.And("verify through \"<success_message>\" on \"Signup_PaswwordText\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("I am performing on \"Signup_PaswwordOkBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And(string.Format("verify the result from \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\"", password_Change_Req_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.Then("I have given \"<Login_id>\" on \"Login_UserId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 84
 testRunner.And(string.Format("I have given \"{0}\" on \"Login_Password\"", new_Password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
 testRunner.And("I am performing on \"Login_SignIn_Button\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.And("I wait 2000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("I am clicking on \"Signup_FeedbackOptionHBL\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.And(string.Format("I have given \"{0}\" on \"Signup_FeedbackText\"", feedback_Option), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
 testRunner.And("I am performing on \"Signup_FeedbackSubmit\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
 testRunner.And("verify through \"Thank you for letting us know.\" on \"Signup_FeedbackMessage\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
 testRunner.And("I am performing on \"Signup_FeedbackOkBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
 testRunner.Then("verify through \"Welcome\" on \"Login_Success_Text\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("2 As a user i want to verify Change Login ID: When I am verifying Change Login ID" +
            " Scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ForgetChange")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ChangeLoginID")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/ChangeLoginID.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When I am verifying Change Login ID Scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When I am verifying Change Login ID Scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:cnic", "1350361299161")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:debit_card", "4902860007216383")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:card_pin", "1234")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Credit_card", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:email", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:new_login_id", "abbzabbz123")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:customer_type_query", "Select I.CUSTOMER_TYPE from dc_customer_info i where I.CNIC =\'{customer_cnic}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:success_msg", "You have successfully changed your login ID Login ID: ABBZABBZ123 A one time acti" +
            "vation password has been sent to your mobile number and/or email address registe" +
            "red with HBL. Please use the activation password to login")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_query", "pakistan2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:activation_password", "Begin update dc_customer_info p set P.LOGIN_PASSWORD=\'$2a$10$fZ3EqVq2W9QWb2silU6s" +
            "VuDUr.2XrXNSHjU98hOuQsCE/Dr1oN6cy\' where P.CNIC=\'{customer_cnic}\';COMMIT;END;")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:new_password", "pakistan1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:success_message_password", "Your Login password and Transaction password have been changed.Please login with " +
            "new password")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_change_req_query", "681168")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:feedback_option", "select IS_PASSWORD_CHANGED_REQUIRED from dc_customer_info P where P.CNIC =\'{custo" +
            "mer_cnic}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:new_login_id_query", "select CUSTOMER_NAME from dc_customer_info P where P.CNIC =\'{customer_cnic}\'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_policy", "Password Policy")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_policy1", "As per HBL policy, your login password should contain the followings.")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_policy2", "Should be between 8 and 15 characters with at least 1 alphabet and 1 numeric digi" +
            "t")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:password_policy3", "Special characters allowed are ! @ # $ . &")]
        public virtual void _2AsAUserIWantToVerifyChangeLoginID_WhenIAmVerifyingChangeLoginIDScenario()
        {
#line 51
this._2AsAUserIWantToVerifyChangeLoginID("When I am verifying Change Login ID Scenario", "1350361299161", "4902860007216383", "1234", "", "", "abbzabbz123", "Select I.CUSTOMER_TYPE from dc_customer_info i where I.CNIC =\'{customer_cnic}\'", "You have successfully changed your login ID Login ID: ABBZABBZ123 A one time acti" +
                    "vation password has been sent to your mobile number and/or email address registe" +
                    "red with HBL. Please use the activation password to login", "pakistan2", "Begin update dc_customer_info p set P.LOGIN_PASSWORD=\'$2a$10$fZ3EqVq2W9QWb2silU6s" +
                    "VuDUr.2XrXNSHjU98hOuQsCE/Dr1oN6cy\' where P.CNIC=\'{customer_cnic}\';COMMIT;END;", "pakistan1", "Your Login password and Transaction password have been changed.Please login with " +
                    "new password", "681168", "select IS_PASSWORD_CHANGED_REQUIRED from dc_customer_info P where P.CNIC =\'{custo" +
                    "mer_cnic}\'", "select CUSTOMER_NAME from dc_customer_info P where P.CNIC =\'{customer_cnic}\'", "Password Policy", "As per HBL policy, your login password should contain the followings.", "Should be between 8 and 15 characters with at least 1 alphabet and 1 numeric digi" +
                    "t", "Special characters allowed are ! @ # $ . &", new string[] {
                        "source:Data/ChangeLoginID.xlsx"});
#line hidden
        }
        
        public virtual void _2AsAUserIWantToVerifyForgetLoginID(string @case, string cnic, string mobile_No, string tran_Message_Qeury, string tran_Id_Query, string tran_Type_Query, string tran_Date_Query, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ForgetLoginID"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("2 As a user i want to verify forget Login ID", @__tags);
#line 99
this.ScenarioSetup(scenarioInfo);
#line 100
 testRunner.Given(string.Format("the test case title is \"{0}\"", @case), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 101
 testRunner.And(string.Format("I set value in context from data \"{0}\" as \"customer_cnic\"", cnic), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
 testRunner.And("the user is arrive to Internet Banking home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.And("I am clicking on \"Forget_btn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 104
 testRunner.And("I am clicking on \"Forget_ChangeLink\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
 testRunner.And(string.Format("I have given \"{0}\" on \"Forget_CNIC\"", cnic), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.And(string.Format("I have given \"{0}\" on \"Forget_MobileNo\"", mobile_No), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.When("I am performing on \"Forget_SubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 108
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordResponseMsg\"", tran_Message_Qeury), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranID\"", tran_Id_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranDate\"", tran_Date_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
 testRunner.And(string.Format("verify through database on \"{0}\" on Schema \"DIGITAL_CHANNEL_SEC\" on \"Forget_Passw" +
                        "ordTranType\"", tran_Type_Query), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.And("I am performing on \"Forget_PasswordTranSubmitBtn\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("2 As a user i want to verify forget Login ID: When I am verifying Forget Login ID" +
            " scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ForgetChange")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ForgetLoginID")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("source:Data/ForgetLoginID.xlsx")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "When I am verifying Forget Login ID scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Case", "When I am verifying Forget Login ID scenario")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:cnic", "1350314051455")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:mobile_no", "03110204994")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_message_qeury", "Select O.RESPONSE_MESSAGE from dc_transaction o where O.CNIC =\'{customer_cnic}\' o" +
            "rder by CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_id_query", "Select O.TRANSACTION_ID from dc_transaction o where O.CNIC =\'{customer_cnic}\' ord" +
            "er by CREATED_ON desc")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_type_query", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
            "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
            "TION_ID = \'")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:tran_date_query", "Select O.created_on from dc_transaction o where O.CNIC =\'{customer_cnic}\' order b" +
            "y CREATED_ON desc")]
        public virtual void _2AsAUserIWantToVerifyForgetLoginID_WhenIAmVerifyingForgetLoginIDScenario()
        {
#line 99
this._2AsAUserIWantToVerifyForgetLoginID("When I am verifying Forget Login ID scenario", "1350314051455", "03110204994", "Select O.RESPONSE_MESSAGE from dc_transaction o where O.CNIC =\'{customer_cnic}\' o" +
                    "rder by CREATED_ON desc", "Select O.TRANSACTION_ID from dc_transaction o where O.CNIC =\'{customer_cnic}\' ord" +
                    "er by CREATED_ON desc", "SELECT AC.DESCRIPTION FROM DC_TRANSACTION_ACTIVITY_CONFIG AC WHERE AC.TRANSACTION" +
                    "_TYPE_ID =(SELECT DT.TRANSACTION_TYPE_ID FROM DC_TRANSACTION DT WHERE DT.TRANSAC" +
                    "TION_ID = \'", "Select O.created_on from dc_transaction o where O.CNIC =\'{customer_cnic}\' order b" +
                    "y CREATED_ON desc", new string[] {
                        "source:Data/ForgetLoginID.xlsx"});
#line hidden
        }
    }
}
#pragma warning restore
#endregion

