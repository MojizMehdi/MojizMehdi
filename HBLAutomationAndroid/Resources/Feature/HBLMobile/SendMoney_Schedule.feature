Feature: SendMoney_Schedule
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@SendMoney @SendMoney_AddNew_Schedule @SendMoney_ViaBene_Schedule
Scenario Outline: When user try to login mobile banking for SendMoney
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page 
	And the test case expected result is "<Expected_Result>"
	And I wait 2000
	And I am clicking on "Login_permission_allow_btn"
	And I wait 1000
	And I am clicking on "Login_permission_allow_btn2"
	And I am clicking on "SendMoney_SkipBtn"
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And update the data by query "<Status_query>" on DIGITAL_CHANNEL_SEC
	When I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	And I wait 2000
	And I am performing on "Login_SignIn_Button"
	And I wait 30000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "BillPayment_RatingOkBtn"
	Then verify through "Welcome, " on "Login_Success_Text"
	@source:Data/SendMoneyMobileLogin.xlsx
	Examples: 
	|Case|Status_query|Login_UserId_Value|Login_Password_Value|OTP_Value|Expected_Result|



@SendMoney @SendMoney_AddNew_Schedule
Scenario Outline: When user try to send money mobile add new schedule payment
    Given the test case title is "<Case>"
    And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
    And the user is arrive to Mobile Banking home page
    And the test case expected result is "<Expected_Result>"
    And I am clicking on "Dashboard"
    #When I save Account Balances
    When I set value in context from data "0" as "term_deposit_flag"
    And I am clicking on "SendMoney_Link"
    And I wait 5000
    And I am clicking on "SendMoney_SkipBtn"
    And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>"
    And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Branch" on Schema "<db_val>"
    And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>"
    And I am clicking on "SendMoney_AddNewBtn"
    And I select "<From_Account_Value>" on "SendMoney_FromAccount"
    And I select "<Bank_Value>" on "SendMoney_Bank"
    And I set value in context from data "<Account_Number_Value>" as "ToAccount"
    And I set value in context from data "SendMoney" as "Transaction_Type"
    And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
    #And I am clicking on "SendMoney_AccVerifyBtn"
    And I wait 2000
    And I scroll down
    And I have given "<Amount_Value>" on "SendMoney_Amount"
    When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
    And I scroll down
    And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
    And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
    And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
    And I am clicking on "SendMoney_SchedulePayment_Check"
    And I scroll down
    And I select "<Frequency_Value>" on "SendMoney_SchedulePayment_Frequency"
    And I am clicking on "SendMoney_SchedulePayment_StartDate"
    And I select from date "<From_Date_Value>"
    #And I wait 2000
    And I am clicking on "SendMoney_SchedulePayment_EndDate"
    And I wait 2000
    And I select to date "<To_Date_Value>"
    And I am clicking on "SendMoney_NextBtn"
    And I am clicking on "SendMoney_SchedulePayment_ViewSummary"
    And I wait 3000
    And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
    And I am clicking on "SendMoney_Btn_Summary_OK"
    #And I wait 2000
    #And I am performing on "SendMoney_NextBtn"
    And I wait 3000
    And I scroll down
    And I wait 2000
    #And I scroll to element text as "One Time Password (OTP)"
    And I have given "<OTP_Value>" on "Login_OTP_field"
    And I wait 2000
    And I am performing on "SendMoney_NextBtn"
    And I wait 3000
    And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
    And I wait 2000
    And I am performing on "SendMoney_NextBtn"
    And I wait 3000
    And I am clicking on "BillPayment_Rating"
    #And I am clicking on "BillPayment_RatingOkBtn"
    #And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
    #And I save Transaction Info
    Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
    ##And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
    ##And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
    And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
    ##And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
    ##And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
    ##And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
    And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
    And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
    And I am clicking on "SendMoney_TranInfoClose"
    And I have given "<Bene_Nick>" on "SendMoney_SearchBeneField"
    Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
    And verify the result of schedule payment from database
    #And I am clicking on "Dashboard"
    ##And I verify Account Balance
    #And I am clicking on "Dashboard_Sidebar"
    #And I am clicking on "Dashboard_Sidebar_TranActivity"
    #And I am clicking on "TransactionActivity_Financial"
    #And I am clicking on "TransactionActivity_LatestTranLink"
    #And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
    #And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
    #And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
    #And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
    #And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
    #And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
    #And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
    @source:Data/SendMoney_Schedule.xlsx
    Examples:
    |Case|status_query|No_Of_Acconts_query|Bene_Count_Query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|Frequency_Value|From_Date_Value|To_Date_Value|OTP_Value|Tran_Pass_Value|Success_Message|from_account_query|frequency_query|purpose_query|db_val|Expected_Result|

	@SendMoney @SendMoney_ViaBene_Schedule
Scenario Outline: When user try to send money mobile using already added bene schedule payment
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And the test case expected result is "<Expected_Result>"
	And I am clicking on "Dashboard"
	When I am clicking on "SendMoney_Link"
	And I wait 2000
	And I am clicking on "SendMoney_SkipBtn"
	And I have given "<BeneName>" on "BillPayment_SearchBeneField"
	And I am clicking on "SendMoney_SearchBeneConsumerNo"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I am clicking on "SendMoney_SchedulePayment_Check"
	And I select "<Frequency_Value>" on "SendMoney_SchedulePayment_Frequency"
	And I am clicking on "SendMoney_SchedulePayment_StartDate"
	And I select from date "<From_Date_Value>"
	#And I wait 2000
	And I am clicking on "SendMoney_SchedulePayment_EndDate"
	And I wait 2000
	And I select to date "<To_Date_Value>"
	And I am clicking on "SendMoney_NextBtn"
	And I am clicking on "SendMoney_SchedulePayment_ViewSummary"
	And I wait 3000
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
	And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose_Bene"
	And verify the result of schedule payment from database 
	@source:Data/SendMoney_Schedule_Beneficiary.xlsx
	Examples: 
	|Case|status_query|BeneName|From_Account_Value|Amount_Value|PurposeOfPayment_Value|Frequency_Value|From_Date_Value|To_Date_Value|Tran_Pass_Value|Success_Message|tran_type_query|from_account_query|frequency_query|purpose_query|db_val|Expected_Result|
