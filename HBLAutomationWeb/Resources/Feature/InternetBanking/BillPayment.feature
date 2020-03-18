Feature: BillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@BillPayment
Scenario Outline: 1 As a user i want to Verify login for HBL Web Bill Payment
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page 
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I wait 3000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	@source:Data/IBLogin.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|

	@BillPayment
Scenario Outline: 2 As a user i want to Verify Bill Payment through PAY
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Pay_Link"
	When I am clicking on "Pay_AddNewBtn"
	And I am clicking on link "<Category_Value>" on "Pay_BillPaymentCategory"
	And I am clicking on link "<Company_Value>" on "Pay_BillPaymentCategory_Company"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_BillPayment_ConsumerNo"
	And I am performing on "Pay_BillPayment_NextBtn"
	And I wait 3000
	And I am performing on "Pay_BillPayment_Inquiry_NextBtn"
	And I scroll to element "Pay_BillPayment_PayBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field" on company code <company_code_value>
	And I have transaction pass check and given <tran_pass_value> on "Pay_Transaction_PayBill_TransactionPassword" on company code <company_code_value>
	And I press Enter on "Pay_BillPayment_PayBtn"
	And I wait 10000
	Then verify through "Transaction is successful." on "Pay_Transaction_Success"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And I am clicking on "Pay_Transaction_ToggleAutoPay"
	And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I am clicking on "Pay_Transaction_PayBillAmount_CloseBtn"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	Then verify through "ConsumerNoContextVal" on "Pay_Transaction_Success"
	#And I am performing on "Pay_Transaction_PayBill_RatingCloseBtn"
	#And verify the result from <result_query> on Schema "<result_db_value>"
	@source:Data/IBBillPayment.xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|company_code_value|OTP_Value|tran_pass_value|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|