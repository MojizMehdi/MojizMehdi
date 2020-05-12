﻿Feature: BillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario Outline: When user try to login mobile banking for bill payment
	Given the test case title is "<Case>"
	And update the data by query "<Status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	When I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	And I wait 2000
	And I am performing on "Login_SignIn_Button"
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And I wait 30000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000
	@source:Data/HBLMobileLogin.xlsx
	Examples: 
	|Case|Status_query|Login_UserId_Value|Login_Password_Value|OTP_Value|
	
@BillPayment
Scenario Outline: 2 As a user i want to Verify Bill Payment through Mobile
	Given the test case title is "<Case>"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page
	When I save Account Balances 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "BillPayment_AddNewBtn"
	And I select "<Company_Value>" on "BillPayment_Category_Company"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_ConsumerNo"
	And I am clicking on "BillPayment_NextBtn"
	And I wait 8000
	And I select "<account_no>" on "BillPayment_FromAccount"
	#And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "BillPayment_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	And I want value from textview "BillPayment_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "BillPayment_Transaction_Unpaid_Amount"
	And I am verifying OTP and Transaction pass check on company code "<company_code_value>"
	And I am clicking on "BillPayment_NextBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I am clicking on "BillPayment_PayNextBtn"
	And I have transaction pass check and given <tran_pass_value> on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_PayBtn"
	And I wait 10000
	And I am clicking on "SendMoney_Rating"
	And I am clicking on "SendMoney_RatingOkBtn"
	And I wait 2000
	And I am clicking on "SendMoney_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "Transaction is successful. " on "BillPayment_TranSuccess"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And I am clicking on "BillPayment_TranInfoClose"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	#And I am clicking on "Pay_Transaction_ToggleAutoPay"
	##And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	#And I am clicking on "<schedule_type>"
	#And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	#And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	#And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	#And I set value in context from data "<bene_name>" as "bene_name"
	#And verify bene status from <bene_query> on Schema "<db_val2>"
	And I wait 2000
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	Then verify through "ConsumerNoContextVal" on "BillPayment_SearchBeneConsumerNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	@source:Data/BillPayment.xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Company_Value|BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|

