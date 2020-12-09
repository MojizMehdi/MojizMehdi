Feature: MutualFund
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Mutual_Fund
Scenario Outline: 1When user try to login mobile banking for mutual fund
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
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000 
	Then verify through "Welcome, " on "Login_Success_Text"
	@source:Data/MutualFundMobileLogin.xlsx
	Examples: 
	|Case|Status_query|Login_UserId_Value|Login_Password_Value|Cnic_query|OTP_Value|Expected_Result|


@Mutual_Fund
Scenario Outline: When user try to mutual fund
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page
	And the test case expected result is "<Expected_Result>"
	And I am clicking on "Dashboard"
	And I wait 4000
	And I set value in context from data "1" as "mutual_fund_flag"
	#When I save Account Balances
	When I set value in context from data "<Mutual_Fund_Value>" as "MutualFundName"
	And I set value in context from database "<disclaimer_query>" as "mutual_fund_disclaimer_popup" on Schema "<db_val3>"
	And I set value in context from database "<cust_profile_id_query>" as "customer_profile_id" on Schema "<db_val3>"
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	#And I verify user Mutual Fund status on schema "<db_val3>" as "<statement_counter>"
	And I am clicking on "MutualFund_Investment_Tab"
	And I set list of elements from scroll view on "MutualFund_List_FundName" as "3"
	And verify the list using "<Fund_Names_query>" on Schema "QAT_AMC"
	And I scroll to element text as "<Mutual_Fund_Value>"
	And I am clicking on "MutualFund_InvestBtn"
	And verify through "<disclaimer_message>" on "MutualFund_DisPopup"
	And I am clicking on "MutualFund_PopupBtn"
	And I select "<account_no>" on "MutualFund_FromAccount"
	And I check values of combobox using database from "<folio_no_query>" on schema <db_val3> on combobox "MutualFund_FolioNumber" of list "MutualFund_FolioNumber_List_Locator"
	And I select "<folio_no>" on "MutualFund_FolioNumber"
	And I have given "<amount_value>" on "MutualFund_FundAmount"
	And verify through database on "<gl_account_query>" on Schema "<db_val3>" on "MutualFund_FundAccNo"
	#And verify through database on "<tran_timing_query>" on Schema "<db_val3>" on "Investment_MutualFund_TranTiming"
	And I am clicking on "MutualFund_NextBtn"
	And I wait 10000
	And I scroll to element text as "and risks involved"
	And I am clicking on "MutualFund_AgreeBtn"
	And verify through database on "<tran_timing_query>" on Schema "<db_val3>" on "MutualFund_CutOff_Timings"
	And I have given "<tran_pass_value>" on "SendMoney_TranPass"
	And I am clicking on "MutualFund_NextBtn"
	And I wait 5000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And I set value in context from database "<HostReferenceNo_query>" as "HostReferenceNo" on Schema "<db_val4>"
	And I set value in context from database "<GUID_query>" as "GUID" on Schema "<db_val>"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "TermDeposit_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "MutualFund_TranToAcc"
	And verify through database on "<fund_name_query>" on Schema "<db_val>" on "MutualFund_Tran_FundName"
	And verify the message "<folio_no>" through database on "<folio_no_verify_query>" on Schema "<db_val3>"
	#And verify the data using "<customer_name_verify_query>" on Schema "<db_val3>"
	And verify the data using "<customer_cnic_verify_query>" on Schema "<db_val3>"
	#And verify the data using "<customer_mobile_no_query>" on Schema "<db_val3>"
	And verify the message "<amount_value>" through database on "<tran_amount_verify_query>" on Schema "<db_val3>"
	#And I save Transaction Info
	And I am clicking on "TermDeposit_CloseBtn"
	And I wait 3000
	And I am clicking on "Dashboard"
	And I wait 2000
	#And I verify Account Balance
	
	@source:Data/MutualFund.xlsx
	Examples: 
	|Case|cust_profile_id_query|Category_Value|Fund_Names_query|statement_counter|Mutual_Fund_Value|account_no|folio_no_query|folio_no|amount_value|gl_account_query|tran_timing_query|tran_pass_value|Success_Message|GUID_query|tran_type_query|tran_amount_query|from_account_query|to_account_query|fund_name_query|disclaimer_message|disclaimer_query|db_val|db_val2|db_val3|db_val4|folio_no_verify_query|customer_name_verify_query|customer_cnic_verify_query|customer_mobile_no_query|tran_amount_verify_query|Expected_Result|