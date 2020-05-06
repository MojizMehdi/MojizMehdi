Feature: PayBillWithBene
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers



@BillPayment
Scenario Outline: 2 As a user i want to Verify Bill Payment through PAY with Bene
Given the test case title is "<Case>"
And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
And update the data by query "<status_query>" on QAT_BPS
And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
And the user is arrive to Internet Banking home page 
And I am clicking on "Login_Dashboard"
And I count Number of Account
And I save Account Balances
When I am clicking on "Pay_Link"
And I set value in context from data "<company_code_value>" as "Company_Code"
And I set value in context from data "<account_type>" as "Account_Type"
And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
And I am clicking on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
And I select "<account_no>" on "Pay_BillPayment_accountno"
And verify the result from "<instrument_type>" on Schema "<db_val2>"
And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_BillPayment_ConsumerNo"
And I am performing on "Pay_BillPayment_NextBtn"
And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
And I wait 5000
And Set parameter in context class "Pay_Bill_Status"
#And verify color of element "<Bill_Status_Background>" on "Pay_Bill_Status_Color"
#And verify color of bill status on "Pay_Bill_Status_Color"
#And Set parameter in context class "<company_code_value>"
And I want value from textbox "Pay_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "Pay_Transaction_Unpaid_Amount"
And I am verifying OTP and Transaction pass check on company code <company_code_value>
And I am performing on "Pay_BillPayment_Inquiry_NextBtn"
And I scroll to element "Pay_BillPayment_PayBtn"
And I have otp check and given <OTP_Value> on "Login_OTP_field"
And I have transaction pass check and given <tran_pass_value> on "Pay_Transaction_PayBill_TransactionPassword"
And I press Enter on "Pay_BillPayment_PayBtn"
And I wait 8000
And I save Transaction Info
Then verify through "Transaction is successful." on "Pay_Transaction_Success"
And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
And I am clicking on "Pay_Transaction_ToggleAutoPay"
#And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
And I am clicking on "<schedule_type>"
And verify the result from "<schedule_config>" on Schema "<db_val>"
And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
And verify the schedule config "<schedule_verify>" on Schema "<db_val>"
And I set value in context from data "<bene_name>" as "bene_name"
And verify bene status from <bene_query> on Schema "<db_val2>"
And I am clicking on "Pay_Transaction_PayBillAmount_CloseBtn"
And I am clicking on "Pay_Transaction_PayBill_Rating"
And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
Then verify through "ConsumerNoContextVal" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
And I am clicking on "Login_Dashboard"
And I verify Account Balance
#And I am performing on "Pay_Transaction_PayBill_RatingCloseBtn"
#And verify the result from <result_query> on Schema "<result_db_value>"
@source:Data/IBBillPayment.xlsx
Examples: 
|Case|status_query|status_query2|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|

