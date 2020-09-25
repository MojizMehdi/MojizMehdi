Feature: BillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario Outline: When user try to login mobile banking for bill payment
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Login_permission_allow_btn"
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
	@source:Data/HBLMobileLogin.xlsx
	Examples: 
	|Case|Status_query|Login_UserId_Value|Login_Password_Value|OTP_Value|
	
@BillPayment
Scenario Outline: As a user i want to Verify Bill Payment through Mobile by make new payment
	Given the test case title is "<Case>"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	When I save Account Balances 
	When I set value in context from data "0" as "term_deposit_flag" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_AddNewBtn"
	And I have given "<Company_Value>" on "BillPayment_Category_Company"
	And I am clicking on "BillPayment_Category_Company_Select"
	#And I select "<Company_Value>" on "BillPayment_Category_Company"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And verify the message using element "BillPayment_ConsumerNo_Label" through database on "<consumer_number_label_query>" on Schema "<db_val2>"
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_ConsumerNo"
	And I am clicking on "BillPayment_NextBtn"
	And I wait 8000
	#BillingMonth(Setall)
	And Set parameter in context class "BillPayment_Inquiry_BillingMonth"
	#PaidMarking(Setall)
	And I set value in context from database "<paid_marking_query>" as "Is_PaidMarking_Req" on Schema "<db_val2>"
	And I verify bill payment inquiry for mobile
	And I select "<account_no>" on "BillPayment_FromAccount"
	#And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "BillPayment_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#partial_payment_req_query
	And I set value in context from database "<partial_payment_check_query>" as "IS_PARTIAL_PAYMENT_ALLOWED" on Schema "<db_val2>"
	And I want value from textview "BillPayment_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "BillPayment_Transaction_Unpaid_Amount"
	#partial_payment
	And I have given "<partial_payment_amount>" on "BillPayment_Transaction_Unpaid_Amount_Field"
	And I am verifying OTP and Transaction pass check on company code "<company_code_value>"
	And I am clicking on "BillPayment_NextBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I am clicking on "BillPayment_CheckNextBtn"
	And I have transaction pass check and given <tran_pass_value> on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_PayBtn"
	And I wait 10000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	And I wait 2000
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "Transaction is successful. " on "BillPayment_TranSuccess"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And verify the message "2" through database on "<LP_BillStatus_query>" on Schema "<db_val2>"
	And I am clicking on "BillPayment_TranInfoClose"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	#And I am clicking on "Pay_Transaction_ToggleAutoPay"
	##And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	#And I am clicking on "<schedule_type>"
	#And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	#And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	#And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I wait 2000
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	Then verify through "ConsumerNoContextVal" on "BillPayment_SearchBeneConsumerNo"
	And I am clicking on "Dashboard"
	#And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"

	@source:Data/BillPayment.xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Company_Value|BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|bene_name|bene_query|instrument_type|consumer_number_label_query|paid_marking_query|LP_BillStatus_query|partial_payment_check_query|partial_payment_amount|

@BillPayment
Scenario Outline: As a user i want to Verify Bill Payment through Mobile by make new payment schedule
	Given the test case title is "<Case>"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag"  
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_AddNewBtn"
	And I have given "<Company_Value>" on "BillPayment_Category_Company"
	And I am clicking on "BillPayment_Category_Company_Select"
	#And I select "<Company_Value>" on "BillPayment_Category_Company"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And verify the message using element "BillPayment_ConsumerNo_Label" through database on "<consumer_number_label_query>" on Schema "<db_val2>"
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_ConsumerNo"
	And I am clicking on "BillPayment_NextBtn"
	And I wait 8000
	#BillingMonth(Setall)
	And Set parameter in context class "BillPayment_Inquiry_BillingMonth"
	And I set value in context from database "<paid_marking_query>" as "Is_PaidMarking_Req" on Schema "<db_val2>"
	And I verify bill payment inquiry for mobile
	And I select "<account_no>" on "BillPayment_FromAccount"
	#And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "BillPayment_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#partial_payment_req_query
	And I set value in context from database "<partial_payment_check_query>" as "IS_PARTIAL_PAYMENT_ALLOWED" on Schema "<db_val2>"
	And I want value from textview "BillPayment_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "BillPayment_Transaction_Unpaid_Amount"
	#partial_payment
	And I have given "<partial_payment_amount>" on "BillPayment_Transaction_Unpaid_Amount_Field"
	And I am verifying OTP and Transaction pass check on company code "<company_code_value>"
	And I am clicking on "BillPayment_NextBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I am clicking on "BillPayment_CheckNextBtn"
	And I have transaction pass check and given <tran_pass_value> on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_PayBtn"
	And I wait 10000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	And I wait 2000
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "Transaction is successful. " on "BillPayment_TranSuccess"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And verify the message "2" through database on "<LP_BillStatus_query>" on Schema "<db_val2>"
	And I am clicking on "BillPayment_TranInfoClose"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	And I set value in context from database "<schedule_config>" as "schedule_configuration" on Schema "<db_val>"
	And I am clicking on "BillPayment_MultiPayment_Schedule_Toggle"
	And I am clicking on "<schedule_type>"
	And I have given "<maximum_amount>" on "BillPayment_MultiPayment_SpecificAmount_Field"
	And I am clicking on "BillPayment_MultiPayment_Schedule_Next"
	And I am clicking on "BillPayment_MultiPayment_Schedule_Agree"
	And I wait 3000
	And verify the schedule config "<schedule_verify>" on Schema "<db_val>"
	And I am clicking on "BillPayment_MultiPayment_Schedule_Close"
	And I wait 2000
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	Then verify through "ConsumerNoContextVal" on "BillPayment_SearchBeneConsumerNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"

	@source:Data/BillPayment(Schedule).xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Company_Value|BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|consumer_number_label_query|paid_marking_query|LP_BillStatus_query|partial_payment_check_query|partial_payment_amount|


@BillPayment
Scenario Outline: As a user i want to Verify Bill Payment through Mobile by already added bene
	Given the test case title is "<Case>"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "1" as "TranTypeBene"
	And update the data by query "<status_query>" on QAT_BPS
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	When I save Account Balances 
	And I set value in context from data "0" as "term_deposit_flag" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_SkipBtn"
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	And I am clicking on "BillPayment_SearchBeneConsumerNo"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And verify the message using element "BillPayment_ConsumerNo_Label" through database on "<consumer_number_label_query>" on Schema "<db_val2>"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I wait 8000
	And I select "<account_no>" on "BillPayment_FromAccount_Bene"
	#BillingMonth(Setall)
	And Set parameter in context class "BillPayment_Inquiry_BillingMonth"
	And I set value in context from database "<paid_marking_query>" as "Is_PaidMarking_Req" on Schema "<db_val2>"
	And I verify bill payment inquiry for mobile
	#And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "BillPayment_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#partial_payment_req_query
	And I set value in context from database "<partial_payment_check_query>" as "IS_PARTIAL_PAYMENT_ALLOWED" on Schema "<db_val2>"
	And I want value from textview "BillPayment_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "BillPayment_Transaction_Unpaid_Amount"
	#partial_payment
	And I have given "<partial_payment_amount>" on "BillPayment_Transaction_Unpaid_Amount_Field"
	And I am verifying OTP and Transaction pass check on company code "<company_code_value>"
	And I am clicking on "BillPayment_NextBtn"
	And I have transaction pass check and given <tran_pass_value> on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_PayBtn"
	And I wait 10000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	And I wait 2000
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I wait 2000
	And I save Transaction Info
	Then verify through "Transaction is successful. " on "BillPayment_TranSuccess"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And verify the message "2" through database on "<LP_BillStatus_query>" on Schema "<db_val2>"
	And I am clicking on "BillPayment_TranInfoClose_Bene"
	#And I am clicking on "Pay_Transaction_ToggleAutoPay"
	##And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	#And I am clicking on "<schedule_type>"
	#And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	#And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	#And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I wait 2000
	#And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	#Then verify through "ConsumerNoContextVal" on "BillPayment_SearchBeneConsumerNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"

	@source:Data/BillPayment(ExistingBene).xlsx
	Examples: 
	|Case|status_query|Category_Value|Company_Value|BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|consumer_number_label_query|paid_marking_query|LP_BillStatus_query|partial_payment_check_query|partial_payment_amount|


@BillPayment
Scenario Outline: As a user i want to Verify Bill Payment through Mobile by already added bene via home icon
	Given the test case title is "<Case>"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "1" as "TranTypeBene"
	And update the data by query "<status_query>" on QAT_BPS
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag"  
	And I am clicking on "Dashboard_BillPayment"
	And I am clicking on "SendMoney_SkipBtn"
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	And I am clicking on "BillPayment_SearchBeneConsumerNo"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And verify the message using element "BillPayment_ConsumerNo_Label" through database on "<consumer_number_label_query>" on Schema "<db_val2>"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I wait 8000
	#BillingMonth(Setall)
	And Set parameter in context class "BillPayment_Inquiry_BillingMonth"
	And I set value in context from database "<paid_marking_query>" as "Is_PaidMarking_Req" on Schema "<db_val2>"
	And I verify bill payment inquiry for mobile
	And I select "<account_no>" on "BillPayment_FromAccount_Bene"
	#And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "BillPayment_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#partial_payment_req_query
	And I set value in context from database "<partial_payment_check_query>" as "IS_PARTIAL_PAYMENT_ALLOWED" on Schema "<db_val2>"
	And I want value from textview "BillPayment_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "BillPayment_Transaction_Unpaid_Amount"
	#partial_payment
	And I have given "<partial_payment_amount>" on "BillPayment_Transaction_Unpaid_Amount_Field"
	And I am verifying OTP and Transaction pass check on company code "<company_code_value>"
	And I am clicking on "BillPayment_NextBtn"
	And I have transaction pass check and given "<tran_pass_value>" on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_PayBtn"
	And I wait 10000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	And I wait 2000
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "Transaction is successful. " on "BillPayment_TranSuccess"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And verify the message "2" through database on "<LP_BillStatus_query>" on Schema "<db_val2>"
	And I am clicking on "BillPayment_TranInfoClose_Bene"
	#And I am clicking on "Pay_Transaction_ToggleAutoPay"
	##And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	#And I am clicking on "<schedule_type>"
	#And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	#And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	#And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I wait 2000
	#And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	#Then verify through "ConsumerNoContextVal" on "BillPayment_SearchBeneConsumerNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"

	@source:Data/BillPayment(ExistingBene)ViaHome.xlsx
	Examples: 
	|Case|status_query|Category_Value|Company_Value|BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|consumer_number_label_query|paid_marking_query|LP_BillStatus_query|partial_payment_check_query|partial_payment_amount|


	@BillPayment
Scenario Outline: As a user i want to Verify Bill Payment through Mobile by make new payment via home icon
	Given the test case title is "<Case>"
	And I set value in context from data "<BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<BillPayment_Category_Value>" as "BillPayment_Category"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag"  
	And I am clicking on "Dashboard_BillPayment"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_AddNewBtn"
	And I am clicking on "<BillPayment_Category>"
	And I have given "<Company_Value>" on "BillPayment_Category_Company"
	And I am clicking on "BillPayment_Category_Company_Select"
	#And I select "<Company_Value>" on "BillPayment_Category_Company"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And verify the message using element "BillPayment_ConsumerNo_Label" through database on "<consumer_number_label_query>" on Schema "<db_val2>"
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_ConsumerNo"
	And I am clicking on "BillPayment_NextBtn"
	And I wait 8000
	#BillingMonth(Setall)
	And Set parameter in context class "BillPayment_Inquiry_BillingMonth"
	And I set value in context from database "<paid_marking_query>" as "Is_PaidMarking_Req" on Schema "<db_val2>"
	And I verify bill payment inquiry for mobile
	And I select "<account_no>" on "BillPayment_FromAccount"
	#And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "BillPayment_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#partial_payment_req_query
	And I set value in context from database "<partial_payment_check_query>" as "IS_PARTIAL_PAYMENT_ALLOWED" on Schema "<db_val2>"
	And I want value from textview "BillPayment_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "BillPayment_Transaction_Unpaid_Amount"
	#partial_payment
	And I have given "<partial_payment_amount>" on "BillPayment_Transaction_Unpaid_Amount_Field"
	And I am verifying OTP and Transaction pass check on company code "<company_code_value>"
	And I am clicking on "BillPayment_NextBtn"
	#partial_payment
	And I have given "<partial_payment_amount>" on "BillPayment_Transaction_Unpaid_Amount_Field"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I am clicking on "BillPayment_CheckNextBtn"
	And I have transaction pass check and given <tran_pass_value> on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_PayBtn"
	And I wait 10000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	And I wait 2000
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "Transaction is successful. " on "BillPayment_TranSuccess"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And verify the message "2" through database on "<LP_BillStatus_query>" on Schema "<db_val2>"
	And I am clicking on "BillPayment_TranInfoClose"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	#And I am clicking on "Pay_Transaction_ToggleAutoPay"
	##And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	#And I am clicking on "<schedule_type>"
	#And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	#And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	#And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I wait 2000
	And I have given "<BillPayment_ConsumerNo_Value>" on "BillPayment_SearchBeneField"
	Then verify through "ConsumerNoContextVal" on "BillPayment_SearchBeneConsumerNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"

	@source:Data/BillPaymentViaHome.xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Company_Value|BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|bene_name|bene_query|instrument_type|consumer_number_label_query|paid_marking_query|LP_BillStatus_query|partial_payment_check_query|partial_payment_amount|


@BillPayment
Scenario Outline: As a user i want to Verify Multiple Bill Payment through Mobile by bene via home icon
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	#When I save Account Balances 
	When I set value in context from data "0" as "term_deposit_flag"
	And I am clicking on "Dashboard_BillPayment"
	And I am clicking on "SendMoney_SkipBtn"
	And I set list of elements from scroll view on "BillPayment_MultiPayment_Benelist_ConsumerNo" as "3"
	And verify the list using "<bene_check_query>" on Schema "<db_val>"
	#And I verify array of elements from scroll view on "<bene_check_keywords>" as "<bene_check_query>" on schema "<db_val>" of "2"
	And I select consumers for multi bill payment as "<Consumer_Numbers>" on "BillPayment_SearchBeneField"
	And I am clicking on "BillPayment_MultiBillSelect_Next"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I verify bill details of consumer numbers for bill payment
	And I am clicking on "BillPayment_NextBtn"
	And I have transaction pass check and given "<tran_pass_value>" on "BillPayment_TransactionPassword"
	And I am clicking on "BillPayment_MultiPayment_PayBtn"
	And I wait 20000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	And I wait 2000
	#And I am clicking on "BillPayment_Rating_FeedbacBillPayment_TranSuccess_MultiBillk_OkBtn"
	And I save Transaction Info for MultiPayment
	Then verify multiple payments summary "Transaction is successful." on "BillPayment_TranSuccess_MultiBill" and "<tran_type_query>" on "BillPayment_TranType" and "<tran_amount_query>" on "BillPayment_TranAmount" and "<from_account_query>" on "BillPayment_TranFromAcc" and "<company_name_query>" on "BillPayment_CompanyName" and "<consumer_no_query>" on "BillPayment_TranSucess_ConsumerNo" on Schema "<db_val>"
	#Then verify multiple payments through "Transaction is successful. " on "BillPayment_TranSuccess_MultiBill"
	#And verify multiple payments through database on "<tran_type_query>" on Schema "<db_val>" on "BillPayment_TranType"
	#And verify multiple payments through database on "<tran_amount_query>" on Schema "<db_val>" on "BillPayment_TranAmount"
	#And verify multiple payments through database on "<from_account_query>" on Schema "<db_val>" on "BillPayment_TranFromAcc"
	#And verify multiple payments through database on "<company_name_query>" on Schema "<db_val>" on "BillPayment_CompanyName"
	#And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "BillPayment_TranSucess_ConsumerNo"
	And I am clicking on "BillPayment_TranInfoClose"
	And I wait 2000
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	#Then verify multiple payments summary "Transaction is successful. " on "BillPayment_TranSuccess_MultiBill" and "<tran_type_query>" on "BillPayment_TranType" and "<tran_amount_query>" on "BillPayment_TranAmount" and "<from_account_query>" on "BillPayment_TranFromAcc" and "<company_name_query>" on "BillPayment_CompanyName" and "<consumer_no_query>" on "BillPayment_TranSucess_ConsumerNo" on Schema "<db_val>"
	And verify transaction activity multiple payments "Successful" on "BillPayment_TranSuccess_MultiBill_TranActivity" and "<tran_type_query>" on "BillPayment_TranType" and "<tran_amount_query>" on "BillPayment_TranAmount" and "<from_account_query>" on "BillPayment_TranFromAcc" and "<company_name_query>" on "BillPayment_CompanyName" and "<consumer_no_query>" on "BillPayment_TranSucess_ConsumerNo" on Schema "<db_val>"
	@source:Data/MultiBillPayment.xlsx
	Examples: 
	|Case|status_query|Consumer_Numbers|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|bene_check_query|db_val|db_val2|