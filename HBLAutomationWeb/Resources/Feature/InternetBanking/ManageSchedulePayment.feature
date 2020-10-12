Feature: ManageSchedulePayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MngSchedule
Scenario Outline: As a user I want to verify Schedule Management from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	When I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am verifying schedule payments of Send Money from My Account
	And I am verifying schedule payments of Bill Payment from My Account


	@source:Data/ManageScheduleVerify.xlsx
	Examples: 
	|Case|db_val|schedule_count_query|


@MngSchedule
Scenario Outline: As a user I want to verify Schedule Management after adding Send Money schedular with new Bene
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	When I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	And I am clicking on "MyAccount_MngSch_AddNew"
	And I am clicking on "MyAccount_MngSch_AddNew_SendMoney"
	And I set value in context from data "<Account_Number_Value>" as "Bene_AccountNo"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And I am clicking on "SendMoney_Link"
	And I am clicking on "SendMoney_AddNewBtn"
	And I wait 2000
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I select "<Bank_Value>" on "SendMoney_Bank"
	And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	And I am clicking on "SendMoney_ScheduleCheck"
	And I scroll to element "SendMoney_Frequency"
	And I select "<Frequency_Value>" on "SendMoney_Frequency"
	And I am clicking on "SendMoney_Schedule_FromDate"
	And I select date "<From_Date_Value>" on month "<From_Month_Value>" on year "<From_Year_Value>"
	And I set calendar from date
	And I am clicking on "SendMoney_Schedule_ToDate"
	And I select date "<To_Date_Value>" on month "<To_Month_Value>" on year "<To_Year_Value>"
	And I set calendar to date
	And I am clicking on "SendMoney_Btn_ViewSummary"
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I scroll to element "SendMoney_NextBtn"
	And I am clicking on "SendMoney_NextBtn"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	And I have given "<Tran_Pass_Value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I am clicking on "SendMoney_ScheduleBtn"
	And I save Transaction Info
	And verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
	And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am performing on "SendMoney_CloseBtn"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am verifying schedule payments of Send Money from My Account


	@source:Data/MngSchedule_SendMoneyAddNew.xlsx
	Examples: 
	|Case|schedule_count_query|status_query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|


@MngSchedule
Scenario Outline: As a user I want to verify Schedule Management after adding Send Money schedular with existing Bene
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	When I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	And I am clicking on "MyAccount_MngSch_AddNew"
	And I am clicking on "MyAccount_MngSch_AddNew_SendMoney"
	And I set value in context from data "<Account_Number_Value>" as "Bene_AccountNo"
	And I am clicking on "SendMoney_Link"
	And I wait 3000
	And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "SendMoney_BeneClick"
	And I wait 2000
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPaymentBene"
	And I am clicking on "SendMoney_ScheduleCheck"
	And I select "<Frequency_Value>" on "SendMoney_Frequency"
	And I am clicking on "SendMoney_Schedule_FromDate"
	And I select date "<From_Date_Value>" on month "<From_Month_Value>" on year "<From_Year_Value>"
	And I set calendar from date
	And I am clicking on "SendMoney_Schedule_ToDate"
	And I select date "<To_Date_Value>" on month "<To_Month_Value>" on year "<To_Year_Value>"
	And I set calendar to date
	And I am clicking on "SendMoney_Btn_ViewSummary"
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I am clicking on "SendMoney_NextBtn"
	And I have given "<Tran_Pass_Value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I am clicking on "SendMoney_ScheduleBtn"
	And I save Transaction Info
	When verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
	And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am performing on "SendMoney_CloseBtn"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I am clicking on "Services_Last_Transaction"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "Services_Transaction_Close_btn"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am verifying schedule payments of Send Money from My Account


	@source:Data/ManageSchedule_SendMoney_WithBene.xlsx
	Examples: 
	|Case|schedule_count_query|status_query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|Frequency_Value|From_Date_Value|From_Month_Value|From_Year_Value|To_Date_Value|To_Month_Value|To_Year_Value|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|from_account_query|frequency_query|purpose_query|db_val|tran_amount_query|to_account_query|to_bank_query|


@MngSchedule
Scenario Outline: As a user I want to verify Schedule Management after adding Bill Payment schedular with new bene
	Given the test case title is "<Case>" 
	And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	And I am clicking on "MyAccount_MngSch_AddNew"
	And I am clicking on "MyAccount_MngSch_AddNew_Bill"
	When I am clicking on "Pay_AddNewBtn"
	And I am clicking on link "<Category_Value>" on "Pay_BillPaymentCategory"
	And I am clicking on link "<Company_Value>" on "Pay_BillPaymentCategory_Company"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And I set value in context from database "<IS_SI_Allowed_query>" as "IS_SI_Allowed" on Schema "<db_val2>"
	And I set value in context from database "<is_paid_query>" as "IS_PAID_REQ" on Schema "<db_val2>"
	And I set value in context from database "<is_partial_payment_query>" as "IS_Partial_Payment" on Schema "<db_val2>"
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	And verify through database on "<consumer_label_query>" on Schema "<db_val2>" on "Pay_ConsumerLabel_Check"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_BillPayment_ConsumerNo"
	And I am performing on "Pay_BillPayment_NextBtn"
	And Set parameter in context class "Pay_BillPayment_BillingMonth"
	And I verify bill payment inquiry for Web
	And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "Pay_Bill_Status"
	#####exxcel colum partial amount
	And I have given "100" on "Pay_Transaction_Unpaid_Amount"
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
	And verify through "Transaction is successful." on "Pay_Transaction_TranSuccessMessage"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And verify the result from "<bill_status_id_query>" on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_ToggleAutoPay"
	#And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	And I am clicking on "<schedule_type>"
	And I set value in context from database "<schedule_config>" as "schedule_config" on Schema "<db_val>"
	And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I wait 3000
	And verify the schedule config "<schedule_verify>" on Schema "<db_val>"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmount_CloseBtn"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
	And I wait 3000
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	Then verify through "ConsumerNoContextVal" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
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
	And I am clicking on "Login_Dashboard"
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am verifying schedule payments of Bill Payment from My Account


	@source:Data/ManageSchedule_Bill_AddNewBene.xlsx
	Examples: 
	|Case|schedule_count_query|status_query|status_query2|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|consumer_label_query|IS_SI_Allowed_query|is_paid_query|bill_status_id_query|is_partial_payment_query|


@MngSchedule
Scenario Outline: As a user I want to verify Schedule Management after adding Bill Payment schedular with existing bene
	Given the test case title is "<Case>" 
	And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<expiry_date>" as "Credit_Card_check"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	And I am clicking on "MyAccount_MngSch_AddNew"
	And I am clicking on "MyAccount_MngSch_AddNew_Bill"
	And I set value in context from data "<company_code_value>" as "Company_Code"
	And I set value in context from data "<account_type>" as "Account_Type"
	And I set value in context from database "<IS_SI_Allowed_query>" as "IS_SI_Allowed" on Schema "<db_val2>"
	And I set value in context from database "<is_paid_query>" as "IS_PAID_REQ" on Schema "<db_val2>"
	And I set value in context from database "<is_partial_payment_query>" as "IS_Partial_Payment" on Schema "<db_val2>"
	When I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	And I am clicking on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I select "<account_no>" on "Pay_BillPayment_accountno"
	And verify the result from "<instrument_type>" on Schema "<db_val2>"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_BillPayment_ConsumerNo"
	And I am performing on "Pay_BillPayment_NextBtn"
	And I have given "<expiry_date>" on "Pay_Card_Expiry_Date"
	And I wait 5000
	And Set parameter in context class "Pay_Bill_Status"
	And I have given "100" on "Pay_Transaction_Unpaid_Amount"
	And I want value from textbox "Pay_Transaction_Unpaid_Amount" on database "<db_val2>" as "<Bill_Amount_query>"
	And verify through database on "<Bill_Amount_query>" on Schema "<db_val2>" on "Pay_Transaction_Unpaid_Amount"
	And I am verifying OTP and Transaction pass check on company code <company_code_value>
	And I am performing on "Pay_BillPayment_Inquiry_NextBtn"
	And I scroll to element "Pay_BillPayment_PayBtn"
	And I have otp check and given <OTP_Value> on "Login_OTP_field"
	And I have transaction pass check and given "<tran_pass_value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I press Enter on "Pay_BillPayment_PayBtn"
	And I wait 8000
	And I save Transaction Info
	And verify through "Transaction is successful." on "Pay_Transaction_TranSuccessMessage"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Pay_Transaction_Success_FromAccount"
	And verify through database on "<company_name_query>" on Schema "<db_val>" on "Pay_Transaction_Success_CompanyName"
	And verify through database on "<consumer_no_query>" on Schema "<db_val>" on "Pay_Transaction_Success_ConsumerNo"
	And verify the result from "<bill_status_id_query>" on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_ToggleAutoPay"
	#And I am clicking on "Pay_Transaction_PayBillAmount_RadioBtn"
	And I am clicking on "<schedule_type>"
	And I set value in context from database "<schedule_config>" as "schedule_config" on Schema "<db_val>"
	And I have given "<maximum_amount>" on "Pay_Transaction_MaxBillAmount_value"
	And I am clicking on "Pay_Transaction_PayBillAmount_NextBtn"
	And I am clicking on "Pay_Transaction_PayBillAmount_AgreeBtn"
	And I wait 3000
	And verify the schedule config "<schedule_verify>" on Schema "<db_val>"
	And I set value in context from data "<bene_name>" as "bene_name"
	And verify bene status from <bene_query> on Schema "<db_val2>"
	And I am clicking on "Pay_Transaction_PayBillAmount_CloseBtn"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Pay_Transaction_PayBill_RatingOkBtn"
	And I have given "<Pay_BillPayment_ConsumerNo_Value>" on "Pay_Transaction_PayBill_BeneSearchTextbox"
	And verify through "ConsumerNoContextVal" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	Then I am verifying schedule payments of Bill Payment from My Account


	@source:Data/ManageSchedule_Bill_WithBene.xlsx
	Examples: 
	|Case|schedule_count_query|status_query|status_query2|Category_Value|Company_Value|Pay_BillPayment_ConsumerNo_Value|Bill_Amount_query|company_code_value|OTP_Value|tran_pass_value|tran_type_query|tran_amount_query|from_account_query|company_name_query|consumer_no_query|db_val|db_val2|account_no|account_type|expiry_date|schedule_type|maximum_amount|bene_name|bene_query|instrument_type|schedule_config|schedule_verify|consumer_label_query|IS_SI_Allowed_query|is_paid_query|bill_status_id_query|is_partial_payment_query|


@MngSchedule
Scenario Outline: As a user I want to verify deleting a Send Money Schedule 
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	When I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am performing "OK" alert operation on cross icon on "MyAccount_MngSch_Delete"
	And verify through "<success_msg>" on "MyAccount_MngSch_DeleteMsg"
	And I am performing on "MyAccount_MngSch_DeleteOkBtn"
	And verify the message "1" through database on "<delete_verify_query>" on Schema "<db_val>"


	@source:Data/ManageSchedule_Delete_SendMoney.xlsx
	Examples: 
	|Case|db_val|account_no|success_msg|delete_verify_query|


@MngSchedule
Scenario Outline: As a user I want to verify deleting a Bill Payment Schedule 
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	And I set value in context from data "<consumer_no>" as "Bene_AccountNo"
	When I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am performing "OK" alert operation on cross icon on "MyAccount_MngSch_Delete"
	And verify through "<success_msg>" on "MyAccount_MngSch_DeleteMsg"
	And I am performing on "MyAccount_MngSch_DeleteOkBtn"
	And verify the message "1" through database on "<delete_verify_query>" on Schema "<db_val>"


	@source:Data/ManageSchedule_Delete_BillPayment.xlsx
	Examples: 
	|Case|db_val|consumer_no|success_msg|delete_verify_query|