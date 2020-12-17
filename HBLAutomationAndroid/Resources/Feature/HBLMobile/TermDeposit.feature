Feature: TermDeposit
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Term_Deposit
Scenario Outline: When user try to login mobile banking term deposit
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
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And I wait 30000
	And I scroll to element text as "One Time Password (OTP)"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000 
	Then verify through "Welcome, " on "Login_Success_Text"
	@source:Data/TermDepositMobileLogin.xlsx
	Examples: 
	|Case|Status_query|Login_UserId_Value|Login_Password_Value|OTP_Value|Expected_Result|


@Term_Deposit
Scenario Outline: When user try to term deposit
	Given the test case title is "<Case>"
	#And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page
	And the test case expected result is "<Expected_Result>"
	And I am clicking on "Dashboard"
	And I wait 4000
	#When I save Account Balances
	When I set value in context from data "1" as "term_deposit_flag" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I wait 3000
	And I set value in context from data "<Deposit_Years_Value>" as "TermDepositYears"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	And I am clicking on "TermDeposit_NoOfYears"
	And I select "<account_no>" on "TermDeposit_Account"
	And I select "<profit_account>" on "TermDeposit_Profit_Account"
	And I scroll down
	And I have given "<Amount_Value>" on "TermDeposit_Amount"
	And I am clicking on "TermDeposit_Next"
	And I wait 2000
	And I scroll down
	And I am clicking on "TermDeposit_AgreeBtn"
	And I wait 2000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I am clicking on "TermDeposit_SubmitBtn"
	And I wait 5000
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "TermDeposit_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	#And I save Transaction Info
	And I am clicking on "TermDeposit_CloseBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I am clicking on "Dashboard"
	And I wait 2000
	#And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	@source:Data/TermDeposit.xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Deposit_Years_Value|account_no|profit_account|Amount_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|db_val|Expected_Result|
