Feature: SendMoneyViaMore
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@SendMoney @SendMoneyHBLKonnect
Scenario Outline: When user try to send money mobile HBL or Konnect Account using more button
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Branch" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_AddNewBtn_interbranch"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	And I am clicking on "SendMoney_AccVerifyBtn"
	And I wait 5000
	And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I scroll down
	And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I scroll down
	And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose"
	And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoneyViaMore_Interbranch.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|No_Of_Acconts_query|Bene_Count_Query|Category_Value|From_Account_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|count_query|


@SendMoney @SendMoneyHBLKonnect
Scenario Outline: When user try to send money mobile Other Bank Account using more button
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_AddNewBtn_interbranch"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	And I am clicking on "SendMoney_AccVerifyBtn"
	And I wait 5000
	And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I scroll down
	And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I scroll down
	And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose"
	And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoneyViaMore_Interbank.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Bene_Count_Query|Category_Value|From_Account_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|count_query|


@SendMoney @SendMoneyHBLKonnect
Scenario Outline: When user try to send money mobile Schedule HBL or Konnect Account using more button
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Branch" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_AddNewBtn_interbranch"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I select "<Bank_Value>" on "SendMoney_Bank"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	And I am clicking on "SendMoney_AccVerifyBtn"
	And I wait 5000
	And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I scroll down
	And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	And I am clicking on "SendMoney_SchedulePayment_Check"
	And I scroll down
	And I select "<Frequency_Value>" on "SendMoney_SchedulePayment_Frequency"
	And I am clicking on "SendMoney_SchedulePayment_StartDate"
	And I select from date "<From_Date_Value>"
	#And I wait 2000
	And I am clicking on "SendMoney_SchedulePayment_EndDate"
	And I wait 2000
	And I select to date "<To_Date_Value>"
	And I am clicking on "SendMoney_NextBtn"
	And I am clicking on "SendMoney_SchedulePayment_ViewSummary"
	And I wait 3000
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I scroll down
	And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose"
	And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And verify the result of schedule payment from database 
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoneyViaMore_Interbranch_Schedule.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|No_Of_Acconts_query|Bene_Count_Query|Category_Value|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|Frequency_Value|From_Date_Value|To_Date_Value|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|count_query|


@SendMoney @SendMoneyHBLKonnect
Scenario Outline: When user try to send money mobile Schedule Other Bank Account using more button 
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I am clicking on "SendMoney_AddNewBtn_interbank"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I select "<Bank_Value>" on "SendMoney_Bank"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	And I am clicking on "SendMoney_AccVerifyBtn"
	And I wait 5000
	And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I scroll down
	And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	And I am clicking on "SendMoney_SchedulePayment_Check"
	And I scroll down
	And I select "<Frequency_Value>" on "SendMoney_SchedulePayment_Frequency"
	And I am clicking on "SendMoney_SchedulePayment_StartDate"
	And I select from date "<From_Date_Value>"
	#And I wait 2000
	And I am clicking on "SendMoney_SchedulePayment_EndDate"
	And I wait 2000
	And I select to date "<To_Date_Value>"
	And I am clicking on "SendMoney_NextBtn"
	And I am clicking on "SendMoney_SchedulePayment_ViewSummary"
	And I wait 3000
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I scroll down
	And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose"
	And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And verify the result of schedule payment from database 
	And I am clicking on "Dashboard"
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoneyViaMore_Interbank_Schedule.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Bene_Count_Query|Category_Value|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|Frequency_Value|From_Date_Value|To_Date_Value|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|count_query|

@mytag
Scenario Outline: When user try to send money mobile Schedule HBL Or Konnect using more button 
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	#And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I wait 2000
	And I have given "<BeneName>" on "BillPayment_SearchBeneField"
	And I am clicking on "SendMoney_SearchBeneConsumerNo"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	#And I select "<Bank_Value>" on "SendMoney_Bank"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	#And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	#And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	#And I scroll down
	#And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	#And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	#And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	#And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	#And I scroll down
	#And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	#And I wait 2000
	#And I am performing on "SendMoney_NextBtn"
	#And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose_Bene"
	And I wait 2000
	#And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	#Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And I am clicking on "Dashboard"
	And I wait 2000
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoneyViaMore_Interbank_Schedule.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Category_Value|BeneName|From_Account_Value|Amount_Value|PurposeOfPayment_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|count_query|
		

@mytag
Scenario Outline: When user try to send money mobile interbranch using already added bene via more
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	#And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I wait 2000
	And I have given "<BeneName>" on "BillPayment_SearchBeneField"
	And I am clicking on "SendMoney_SearchBeneConsumerNo"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	#And I select "<Bank_Value>" on "SendMoney_Bank"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	#And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	#And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	#And I scroll down
	#And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	#And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	#And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	#And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	#And I scroll down
	#And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	#And I wait 2000
	#And I am performing on "SendMoney_NextBtn"
	#And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose_Bene"
	And I wait 2000
	#And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	#Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And I am clicking on "Dashboard"
	And I wait 2000
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoney(ViaBene)_Interbranch.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Category_Value|BeneName|From_Account_Value|Amount_Value|PurposeOfPayment_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|


	@mytag
Scenario Outline: When user try to send money mobile interbank using already added bene via more
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	#And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I wait 2000
	And I have given "<BeneName>" on "BillPayment_SearchBeneField"
	And I am clicking on "SendMoney_SearchBeneConsumerNo"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	#And I select "<Bank_Value>" on "SendMoney_Bank"
	And I set value in context from data "<Account_Number_Value>" as "ToAccount"
	And I set value in context from data "SendMoney" as "Transaction_Type"
	#And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	#And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	#And I scroll down
	#And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	#And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	#And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	#And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	#And I scroll down
	#And I wait 2000
	#And I scroll to element text as "One Time Password (OTP)"
	#And I wait 2000
	#And I am performing on "SendMoney_NextBtn"
	#And I wait 3000
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	And I save Transaction Info
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose_Bene"
	And I wait 2000
	#And I have given "<Account_Number_Value>" on "SendMoney_SearchBeneField"
	#Then verify through "ToAccountNoContextVal" on "SendMoney_SearchBeneAccountNo"
	And I am clicking on "Dashboard"
	And I wait 2000
	And I verify Account Balance
	And I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_TranActivity"
	And I am clicking on "TransactionActivity_Financial"
	And I am clicking on "TransactionActivity_LatestTranLink"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "SendMoney_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "SendMoney_TranToAcc"
	And verify through database on "<to_bank_query>" on Schema "<db_val>" on "SendMoney_TranToBank"
	#And verify through database on "<bene_name_query>" on Schema "<db_val>" on "SendMoney_TranBeneName"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	@source:Data/SendMoney(ViaBene)_Interbank.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Category_Value|BeneName|From_Account_Value|Amount_Value|PurposeOfPayment_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|


@mytag
Scenario Outline: When user try to send money mobile schedule interbranch using already added bene via more
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	#And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I wait 2000
	And I have given "<BeneName>" on "BillPayment_SearchBeneField"
	And I am clicking on "SendMoney_SearchBeneConsumerNo"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I am clicking on "SendMoney_SchedulePayment_Check"
	And I select "<Frequency_Value>" on "SendMoney_SchedulePayment_Frequency"
	And I am clicking on "SendMoney_SchedulePayment_StartDate"
	And I select from date "<From_Date_Value>"
	#And I wait 2000
	And I am clicking on "SendMoney_SchedulePayment_EndDate"
	And I wait 2000
	And I select to date "<To_Date_Value>"
	And I am clicking on "SendMoney_NextBtn"
	And I am clicking on "SendMoney_SchedulePayment_ViewSummary"
	And I wait 3000
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
	And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose_Bene"
	And verify the result of schedule payment from database 
	@source:Data/SendMoney(ViaBene)_Interbranch_Schedule.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Category_Value|BeneName|From_Account_Value|Amount_Value|PurposeOfPayment_Value|Frequency_Value|From_Date_Value|To_Date_Value|Tran_Pass_Value|Success_Message|tran_type_query|from_account_query|frequency_query|purpose_query|db_val|


	@mytag
Scenario Outline: When user try to send money mobile schedule interbank using already added bene via more
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	And I am clicking on "Dashboard"
	When I save Account Balances
	And I set value in context from data "0" as "term_deposit_flag" 
	And I set value in context from data "<FCY_Check>" as "FCY_Tran_Check"
	#And I set value in context from database "<No_Of_Acconts_query>" as "No_Of_Accounts" on Schema "<db_val>" 
	#And I set value in context from database "<Bene_Count_Query>" as "Beneficiary_Count_Inter_Bank" on Schema "<db_val>" 
	And I am clicking on "Dashboard_More"
	And I have given "<Category_Value>" on "SendMoney_SearchBeneField"
	And I am clicking on "BillPayment_CategoryLink"
	And I wait 2000
	And I have given "<BeneName>" on "BillPayment_SearchBeneField"
	And I am clicking on "SendMoney_SearchBeneConsumerNo"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	And verify the message using element "SendMoney_Buy_Rate" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "SendMoney_Converted_Amount" through database on "<conversion_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I am clicking on "SendMoney_SchedulePayment_Check"
	And I select "<Frequency_Value>" on "SendMoney_SchedulePayment_Frequency"
	And I am clicking on "SendMoney_SchedulePayment_StartDate"
	And I select from date "<From_Date_Value>"
	#And I wait 2000
	And I am clicking on "SendMoney_SchedulePayment_EndDate"
	And I wait 2000
	And I select to date "<To_Date_Value>"
	And I am clicking on "SendMoney_NextBtn"
	And I am clicking on "SendMoney_SchedulePayment_ViewSummary"
	And I wait 3000
	And I am verifying list of execution iterations on "SendMoney_Btn_Summary_Iteration_Dates"
	And I am clicking on "SendMoney_Btn_Summary_OK"
	And I have given "<Tran_Pass_Value>" on "SendMoney_TranPass"
	And I wait 2000
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I am clicking on "BillPayment_Rating"
	#And I am clicking on "BillPayment_RatingOkBtn"
	#And I am clicking on "BillPayment_Rating_Feedback_OkBtn"
	Then verify through "<Success_Message>" on "SendMoney_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "SendMoney_TranType"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "SendMoney_TranSourceAcc"
	And verify through database on "<frequency_query>" on Schema "<db_val>" on "SendMoney_TranFrequency"
	And verify through database on "<purpose_query>" on Schema "<db_val>" on "SendMoney_TranPurpose"
	And I am clicking on "SendMoney_TranInfoClose_Bene"
	And verify the result of schedule payment from database 
	@source:Data/SendMoney(ViaBene)_Interbank_Schedule.xlsx
	Examples: 
	|Case|status_query|FCY_Check|conversion_query|Category_Value|BeneName|From_Account_Value|Amount_Value|PurposeOfPayment_Value|Frequency_Value|From_Date_Value|To_Date_Value|Tran_Pass_Value|Success_Message|tran_type_query|from_account_query|frequency_query|purpose_query|db_val|
