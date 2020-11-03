Feature: SendMoney
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@SendMoney
Scenario Outline: 1 As a user i want to Verify login for HBL Web Send Money
	Given the test case title is "<Case>"
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And the user is arrive to Internet Banking home page 
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I wait 5000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	Then verify through "Welcome" on "Login_Success_Text"
	@source:Data/IBLogin.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|



@SendMoney
Scenario Outline: As a user i want to Verify Send Money by adding Beneficiary
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I set value in context from data "<Account_Number_Value>" as "Bene_AccountNo"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	When I am clicking on "SendMoney_Link"
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
	And I am clicking on "SendMoney_NextBtn"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	And I have given "<Tran_Pass_Value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I am clicking on "SendMoney_SendBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
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
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "Services_Transaction_Close_btn"
	@source:Data/SendMoney.xlsx
	Examples: 
	|Case|status_query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|

@SendMoney
Scenario Outline: As a user i want to Verify Send Money by adding Beneficiary schedule payment
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I set value in context from data "<Account_Number_Value>" as "Bene_AccountNo"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	When I am clicking on "SendMoney_Link"
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
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
	And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am performing on "SendMoney_CloseBtn"
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	#And I am clicking on "Services_Link"
	#And I am clicking on "Services_Transaction_Activity"
	#And I am clicking on "Services_Last_Transaction"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	#And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	#And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	#And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	#And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	#And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And I am clicking on "Services_Transaction_Close_btn"
	@source:Data/SendMoney_Schedule.xlsx
	Examples: 
	|Case|status_query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|Frequency_Value|From_Date_Value|From_Month_Value|From_Year_Value|To_Date_Value|To_Month_Value|To_Year_Value|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|from_account_query|frequency_query|purpose_query|db_val|tran_amount_query|to_account_query|to_bank_query|



	@SendMoney
Scenario Outline: As a user i want to Verify Send Money by using Beneficiary
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I set value in context from data "<Account_Number_Value>" as "Bene_AccountNo"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	When I am clicking on "SendMoney_Link"
	And I wait 3000
	And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "SendMoney_BeneClick"
	And I wait 2000
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPaymentBene"
	And I am clicking on "SendMoney_NextBtn"
	And I have given "<Tran_Pass_Value>" on "Pay_Transaction_PayBill_TransactionPassword"
	And I am clicking on "SendMoney_SendBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Pay_Transaction_Success_Amount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
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
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "Services_Transaction_Close_btn"

	@source:Data/SendMoney_Beneficiary.xlsx
	Examples: 
	|Case|status_query|From_Account_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|


	@SendMoney
Scenario Outline: As a user i want to Verify Send Money by using Beneficiary schedule payment
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	And I set value in context from data "<Account_Number_Value>" as "Bene_AccountNo"
	#And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	When I am clicking on "SendMoney_Link"
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
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
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

	@source:Data/SendMoney_Schedule_Beneficiary.xlsx
	Examples: 
	|Case|status_query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|Frequency_Value|From_Date_Value|From_Month_Value|From_Year_Value|To_Date_Value|To_Month_Value|To_Year_Value|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|from_account_query|frequency_query|purpose_query|db_val|tran_amount_query|to_account_query|to_bank_query|
