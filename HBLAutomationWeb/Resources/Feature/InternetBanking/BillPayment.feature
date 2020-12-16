Feature: BillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@BillPayment @MultiBill_Pay @MultiBill_Home @SingleBill_Pay_AddNew_Schedule @SingleBill_Home_AddNew_Schedule @SingleBill_Pay_Bene_Schedule @SingleBill_Home_AddNew @SingleBill_Pay_AddNew @SingleBill_Pay_Bene
Scenario Outline: 1 As a user i want to Verify login for HBL Web Bill Payment
	Given the test case title is "<Case>"
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And the user is arrive to Internet Banking home page
	#And the test case expected result is "<Expected_Result>" 
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I wait 5000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	Then verify through "Welcome" on "Login_Success_Text"
	@source:Data/IBBillPayment_Login.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|Expected_Result|


@BillPayment @SingleBill_Home_AddNew
Scenario Outline: As a user i want to Verify Add New Bill Payment From Home Screen Icon
	Given the test case title is "<Case>"
	And the test case expected result is "<Expected_Result>"
	And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	And I set value in context from database "<bene_count_query>" as "Bene_Count" on Schema "<db_val>"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	#And the test case expected result is "<Expected_Result>"
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I am clicking on "Pay_LinkHomeIcon"
	And I wait 4000
	And I am clicking on "Pay_AddNewBtn"
	#And I set list of elements from scroll view on "Pay_BillPaymentCategory_List"
	#And verify the list using "<category_list_query>" on Schema "<db_val2>"
	When I am clicking on link "<Category_Value>" on "Pay_BillPaymentCategory"
	And I am clicking on link "<Company_Value>" on "Pay_BillPaymentCategory_Company"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And I set value in context from database "<is_paid_query>" as "IS_PAID_REQ" on Schema "<db_val2>"
	And I set value in context from database "<is_partial_payment_query>" as "IS_Partial_Payment" on Schema "<db_val2>"
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	#And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_BillPayment_ConsumerNo"
	And I am performing on "Pay_BillPayment_NextBtn"
	And Set parameter in context class "Pay_BillPayment_BillingMonth"
	And I verify bill payment inquiry for Web
	And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "Pay_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#And Set parameter in context class "<company_code_value>"
	And I have given "<partial_amount>" on "Pay_Transaction_Unpaid_Amount"
	And I want value from textbox "Pay_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "Pay_Transaction_Unpaid_Amount"
	And I am verifying OTP and Transaction pass check on company code <company_code_value>
	And I am performing on "Pay_BillPayment_Inquiry_NextBtn"
	And I scroll to element "Pay_BillPayment_PayBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I have transaction pass check and given "<tran_pass_value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I press Enter on "Pay_BillPayment_PayBtn"
	And I wait 10000
	And I save Transaction Info
	Then verify through "Transaction is successful." on "Pay_Transaction_TranSuccessMessage"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And verify the result from "<bill_status_id_query>" on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmount_AddNew_CloseBtn"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmount_CloseBtn"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	And verify through "ConsumerNoContextVal" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I am clicking on "Services_Last_Transaction"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And I am clicking on "Services_Transaction_Close_btn"
	#And I am performing on "Pay_Transaction_PayBill_RatingCloseBtn"
	#And verify the result from <result_query> on Schema "<result_db_value>"
	@source:Data/IBBillPayment_Home_AddNew.xlsx
	Examples: 
	|Case|Expected_Result|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|bene_name|bene_query|instrument_type|consumer_label_query|is_paid_query|bill_status_id_query|is_partial_payment_query|partial_amount|category_list_query|bene_count_query|


@BillPayment @SingleBill_Pay_AddNew
Scenario Outline: 1 As a user i want to Verify Add New Bill Payment through PAY
	Given the test case title is "<Case>"
	And the test case expected result is "<Expected_Result>"
	And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	And I set value in context from database "<bene_count_query>" as "Bene_Count" on Schema "<db_val>"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	#And the test case expected result is "<Expected_Result>"
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I am clicking on "Pay_Link"
	And I wait 4000
	When I am clicking on "Pay_AddNewBtn"
	#And I set list of elements from scroll view on "Pay_BillPaymentCategory_List"
	#And verify the list using "<category_list_query>" on Schema "<db_val2>"
	And I am clicking on link "<Category_Value>" on "Pay_BillPaymentCategory"
	And I am clicking on link "<Company_Value>" on "Pay_BillPaymentCategory_Company"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And I set value in context from database "<is_paid_query>" as "IS_PAID_REQ" on Schema "<db_val2>"
	And I set value in context from database "<is_partial_payment_query>" as "IS_Partial_Payment" on Schema "<db_val2>"
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	#And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_BillPayment_ConsumerNo"
	And I am performing on "Pay_BillPayment_NextBtn"
	And Set parameter in context class "Pay_BillPayment_BillingMonth"
	And I verify bill payment inquiry for Web
	And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "Pay_Bill_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#And Set parameter in context class "<company_code_value>"
	And I have given "<partial_amount>" on "Pay_Transaction_Unpaid_Amount"
	And I want value from textbox "Pay_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "Pay_Transaction_Unpaid_Amount"
	And I am verifying OTP and Transaction pass check on company code <company_code_value>
	And I am performing on "Pay_BillPayment_Inquiry_NextBtn"
	And I scroll to element "Pay_BillPayment_PayBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I have transaction pass check and given "<tran_pass_value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I press Enter on "Pay_BillPayment_PayBtn"
	And I wait 10000
	And I save Transaction Info
	Then verify through "Transaction is successful." on "Pay_Transaction_TranSuccessMessage"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And verify the result from "<bill_status_id_query>" on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmount_AddNew_CloseBtn"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmount_CloseBtn"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	And verify through "ConsumerNoContextVal" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I am clicking on "Services_Last_Transaction"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And I am clicking on "Services_Transaction_Close_btn"
	#And I am performing on "Pay_Transaction_PayBill_RatingCloseBtn"
	#And verify the result from <result_query> on Schema "<result_db_value>"
	@source:Data/IBBillPayment_Pay_AddNew.xlsx
	Examples: 
	|Case|Expected_Result|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|bene_name|bene_query|instrument_type|consumer_label_query|is_paid_query|bill_status_id_query|is_partial_payment_query|partial_amount|category_list_query|bene_count_query|


@BillPayment @SingleBill_Pay_Bene
Scenario Outline: As a user i want to Verify Bill Payment through PAY with Bene
	Given the test case title is "<Case>"
	And the test case expected result is "<Expected_Result>"
	And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	#And the test case expected result is "<Expected_Result>"
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	When I am clicking on "Pay_Link"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And I set value in context from database "<is_paid_query>" as "IS_PAID_REQ" on Schema "<db_val2>"
	And I set value in context from database "<is_partial_payment_query>" as "IS_Partial_Payment" on Schema "<db_val2>"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	And I am clicking on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	#And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And Set parameter in context class "Pay_BillPayment_BillingMonth"
	And I verify bill payment inquiry for Web
	And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "Pay_Bill_Bene_Status"
	#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
	#And verify color of bill status on "Pay_Bill_Status_Color"
	#And Set parameter in context class "<company_code_value>"
	And I have given "100" on "Pay_Transaction_Unpaid_Amount"
	And I want value from textbox "Pay_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "Pay_Transaction_Unpaid_Amount"
	And I am verifying OTP and Transaction pass check on company code <company_code_value>
	And I have transaction pass check and given "<tran_pass_value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I press Enter on "Pay_BillPayment_Bene_PayBtn"
	And I wait 8000
	And I save Transaction Info
	Then verify through "Transaction is successful." on "Pay_Transaction_TranSuccessMessage"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And verify the result from "<bill_status_id_query>" on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmountBene_CloseBtn"
	#And I set value in context from data "<bene_name>" as "bene_name"
	#And verify bene status from <bene_query> on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	And verify through "ConsumerNoContextVal" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I am clicking on "Services_Last_Transaction"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "MyAccount_Forgot_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And I am performing on "MyAccount_Forgot_CloseBtn"
	#And verify the result from <result_query> on Schema "<result_db_value>"
	@source:Data/IBBillPayment_Pay_Bene.xlsx
	Examples: 
	|Case|Expected_Result|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|bene_name|bene_query|instrument_type|consumer_label_query|is_paid_query|bill_status_id_query|is_partial_payment_query|category_list_query|partial_amount|



@BillPayment @MultiBill_Pay
Scenario Outline: As a user i want to Verify MULTI Bill Payment through PAY
	Given the test case title is "<Case>" 
	And the test case expected result is "<Expected_Result>"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	#And the test case expected result is "<Expected_Result>"
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances 
	When I am clicking on "Pay_Link"
	And I am clicking on "Pay_MultiBillIcon"
	And I wait 5000
	And I select consumers for multi bill payment as "<Consumer_No_List>" on "SendMoney_SearchBeneField"
	#And I am clicking on "Pay_MultiBill_NextBtn_Icon"
	And I verify bill details of consumer numbers for bill payment
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	And I have transaction pass check and given "<tran_password>" on "Pay_Transaction_PayBill_TransactionPassword"	
	And I press Enter on "Pay_MultiBill_PayBtn"
	And I wait 5000
	And I save Transaction Info
	Then verify multiple payments summary "Transaction is successful." on "Pay_MultiBill_Success_Msg" and "<tran_type_query>" on "Pay_MultiBill_TranType" and "<tran_amount_query>" on "Pay_MultiBill_TranAmount" and "<from_account_query>" on "Pay_MultiBill_FromAccount" and "<company_name_query>" on "Pay_MultiBill_CompanyName" and "<consumer_no_query>" on "Pay_MultiBill_ConsumerNo" on Schema "<db_val>"
	And I am clicking on "Pay_MultiBill_Tran_Ok_Btn"
	And I wait 2000
	#And I am clicking on "Pay_Transaction_PayBill_Rating"  Delete customer_info_id from customer survey table
	#And I am clicking on "Pay_MultiBill_RatingOKBtn"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I verify multiple payment details in Transaction Activity "Successful" on "Pay_MultiBill_SRV_TranStatus" and "<tran_type_query>" on "Pay_MultiBill_SRV_TranType" and "<tran_amount_query>" on "Pay_MultiBill_SRV_TranAmount" and "<from_account_query>" on "Pay_MultiBill_SRV_FromAcc" and "<company_name_query>" on "Pay_MultiBill_SRV_CompanyName" and "<consumer_no_query>" on "Pay_MultiBill_SRV_ConsumerNo" on Schema "<db_val>"
	And I am clicking on "Login_Dashboard"

	@source:Data/MultiBillPayment_Pay.xlsx
	Examples: 
	|Case|Expected_Result|Consumer_No_List|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|tran_password|



@BillPayment @MultiBill_Home
Scenario Outline: As a user i want to Verify MULTI Bill Payment through Home
	Given the test case title is "<Case>" 
	And the test case expected result is "<Expected_Result>"
	And I set value in context from data "BillPayment" as "Transaction_Type"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	#And the test case expected result is "<Expected_Result>"
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances 
	When I am clicking on "Pay_LinkHomeIcon"
	#And I am clicking on "Pay_MultiBillIcon"
	And I wait 5000
	And I select consumers for multi bill payment as "<Consumer_No_List>" on "SendMoney_SearchBeneField"
	#And I am clicking on "Pay_MultiBill_NextBtn_Icon"
	And I verify bill details of consumer numbers for bill payment
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	And I have transaction pass check and given "<tran_password>" on "Pay_Transaction_PayBill_TransactionPassword"	
	And I press Enter on "Pay_MultiBill_PayBtn"
	And I wait 5000
	And I save Transaction Info
	Then verify multiple payments summary "Transaction is successful." on "Pay_MultiBill_Success_Msg" and "<tran_type_query>" on "Pay_MultiBill_TranType" and "<tran_amount_query>" on "Pay_MultiBill_TranAmount" and "<from_account_query>" on "Pay_MultiBill_FromAccount" and "<company_name_query>" on "Pay_MultiBill_CompanyName" and "<consumer_no_query>" on "Pay_MultiBill_ConsumerNo" on Schema "<db_val>"
	And I am clicking on "Pay_MultiBill_Tran_Ok_Btn"
	And I wait 2000
	#And I am clicking on "Pay_Transaction_PayBill_Rating"  Delete customer_info_id from customer survey table
	#And I am clicking on "Pay_MultiBill_RatingOKBtn"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I verify multiple payment details in Transaction Activity "Successful" on "Pay_MultiBill_SRV_TranStatus" and "<tran_type_query>" on "Pay_MultiBill_SRV_TranType" and "<tran_amount_query>" on "Pay_MultiBill_SRV_TranAmount" and "<from_account_query>" on "Pay_MultiBill_SRV_FromAcc" and "<company_name_query>" on "Pay_MultiBill_SRV_CompanyName" and "<consumer_no_query>" on "Pay_MultiBill_SRV_ConsumerNo" on Schema "<db_val>"
	And I am clicking on "Login_Dashboard"

	@source:Data/MultiBillPayment_Home.xlsx
	Examples: 
	|Case|Expected_Result|Consumer_No_List|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|tran_password|
