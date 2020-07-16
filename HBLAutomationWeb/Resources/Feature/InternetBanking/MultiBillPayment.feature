Feature: MultiBillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MultiBillPayment
Scenario Outline: As a user I want to verify Multiple Bill Payment scenario
	Given the test case title is "<Case>" 
	And I set value in context from data "BillPayment" as "Transaction_Type"
	#And update the data by query "<status_query>" on QAT_BPS
	#And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances 
	When I am clicking on "Pay_Link"
	And I am clicking on "Pay_MultiBillIcon"
	And I select consumers for multi bill payment as "<Consumer_No_List>" on "SendMoney_SearchBeneField"
	#And I am clicking on "Pay_MultiBill_NextBtn_Icon"
	And I verify bill details of consumer numbers for bill payment
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	And I have transaction pass check and given "pakistan2" on "Pay_Transaction_PayBill_TransactionPassword"
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

	@source:Data/MultiBillPayment.xlsx
	Examples: 
	|Case|Consumer_No_List|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|

