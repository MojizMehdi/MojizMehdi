Feature: BeneManagement
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@BeneMng @BeneVerificationSendMoney @BeneVerificationBillPay @BeneEditing @BeneDeletion @BeneAddition
Scenario Outline: As a user i want to Verify login for HBL Web Beneficiary Management
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
	@source:Data/Bene_Mng_Login.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|



@BeneMng @BeneVerificationSendMoney
Scenario Outline: As a user I want to verify Send Money Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I wait 10000
	And I am clicking on "BeneManage_SendMoney_Tab"
	Then I want to verify already added beneficiaries with query "<send_money_query>" of Schema "<db_val>" on keyword "BeneManage_SendMoney_BeneCount"

	@source:Data/BeneVerification_SendMoney.xlsx
	Examples: 
	|Case|Expected_Result|db_val|send_money_query|


@BeneMng @BeneVerificationBillPay
Scenario Outline: As a user I want to verify Bill Payment Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I wait 10000
	And I am clicking on "BeneManage_Pay_Tab"
	Then I want to verify already added beneficiaries with query "<pay_query>" of Schema "<db_val>" on keyword "BeneManage_Pay_BeneCount"

	@source:Data/BeneVerification_Pay.xlsx
	Examples: 
	|Case|Expected_Result|db_val|pay_query|


@BeneMng @BeneEditing
Scenario Outline: As a user I want to verify Editing Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I wait 10000
	And I am clicking on "BeneManage_SendMoney_Tab"
	And I scroll to element "BeneManage_Edit"
	And I set value in context from Keyword "BeneManage_Edit_Title" as "Bene_Name"
	Then I press Enter on "BeneManage_Edit"
	And I set value in context from Keyword "BeneManage_Edit_To_Bank" as "bene_bank"
	And I have given "<nick>" on "BeneManage_Edit_Nick"
	And I have given "<email>" on "BeneManage_Edit_Email"
	And I have given "<mobile>" on "BeneManage_Edit_Mobile"
	And I am performing "OK" alert operation on cross icon on "BeneManage_Edit_UpdateBtn"
	And verify through "<success_message>" on "BeneManage_Delete_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "<db_val>" on "BeneManage_Delete_TranBene"
	And verify the message "<account_no>" through database on "<to_account_query>" on Schema "<db_val>"
	And verify the message "<bene_op_value>" through database on "<bene_op_type_query>" on Schema "<db_val>"
	And verify the result from "<account_title_query>" on Schema "<db_val>"
	And verify the result from "<bene_bank_query>" on Schema "<db_val>"
	And verify the result of two queries "<bene_id_query>" on Schema "<db_val>" with "<bene_id_tran_query>" on Schema "<db_val>" through database
	And I am performing on "MyAccount_TranPopUp_CloseBtn"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I select "Beneficiary Management" on "Services_Transaction_Type"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "<db_val>" on "BeneManage_Delete_TranBene"
	And I am performing on "Investment_TranActivityCloseBtn"

	@source:Data/BeneEditing.xlsx
	Examples: 
	|Case|Expected_Result|account_no|success_message|nick|email|mobile|tran_type_query|tran_date_query|tran_bene_name_query|bene_op_type_query|bene_op_value|db_val|to_account_query|account_title_query|bene_bank_query|bene_id_tran_query|bene_id_query|



@BeneMng @BeneDeletion
Scenario Outline: As a user I want to verify Deleting Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I wait 10000
	And I am clicking on "BeneManage_SendMoney_Tab"
	And I scroll to element "BeneManage_Delete"
	And I set value in context from Keyword "BeneManage_Edit_Title" as "Bene_Name"
	Then I press Enter on "BeneManage_Edit"
	And I set value in context from Keyword "BeneManage_Edit_To_Bank" as "bene_bank"
	And I am performing "OK" alert operation on cross icon on "BeneManage_Delete"
	And verify through "<success_message>" on "BeneManage_Delete_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "<db_val>" on "BeneManage_Delete_TranBene"
	And verify the message "<account_no>" through database on "<to_account_query>" on Schema "<db_val>"
	And verify the message "<bene_op_value>" through database on "<bene_op_type_query>" on Schema "<db_val>"
	And verify the result from "<account_title_query>" on Schema "<db_val>"
	And verify the result from "<bene_bank_query>" on Schema "<db_val>"
	And verify the result of two queries "<bene_id_query>" on Schema "<db_val>" with "<bene_id_tran_query>" on Schema "<db_val>" through database
	And I set value in context from data "true" as "is_delete"
	And I am performing on "BeneManage_Delete_TranCloseBtn"
	And I have given "<account_no>" on "SendMoney_SearchBeneField"
	And verify through "<account_no>" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And verify the result from "<delete_bene_query>" on Schema "<db_val>"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I select "Beneficiary Management" on "Services_Transaction_Type"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "<db_val>" on "BeneManage_Delete_TranBene"
	And I am performing on "Investment_TranActivityCloseBtn"

	@source:Data/BeneDeletion.xlsx
	Examples: 
	|Case|Expected_Result|account_no|success_message|tran_type_query|tran_bene_name_query|tran_date_query|delete_bene_query|bene_op_type_query|bene_op_value|to_account_query|account_title_query|bene_bank_query|bene_id_tran_query|bene_id_query|db_val|


@BeneMng @BeneAddition
Scenario Outline: As a user i want to Verify Beneficiary Addition for Send Money
	Given the test case title is "<Case>"
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	And I set value in context from data "<Mobile_No>" as "mobile_number"
	#And update the data by query "<StatusQuery>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page
	And the test case expected result is "<Expected_Result>"
 	And I am clicking on "Login_Dashboard"
	And I am clicking on "BeneManage_Link"
	And I wait 10000
	And I am clicking on "BeneManage_SendMoney_Tab"
	And I am clicking on "BeneManage_AddNewBtn"
	When I select "<Bank_Value>" on "BeneManage_Bank"
	And I have given "<account_no>" on "BeneManage_AccountNo"
	And I have given "<BeneNick_Value>" on "BeneManage_BeneNick"
	And I have given "<Email>" on "BeneManage_PayeeEmail"
	And I have given "<Mobile_No>" on "BeneManage_PayeeMobileNumber"
	And I am performing on "BeneManage_Validate_Button"
	And I wait 3000
	And I set value in context from Keyword "BeneManage_Add_ToAcc" as "Bene_Name"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I press Enter on "BeneManage_Add_OtpSaveBtn"
	Then verify through "Congratulations" on "BeneManage_TranSuccessMessage"
	And verify through database on "<tran_response_query>" on Schema "<db_val>" on "BeneManage_TranResponseMsg"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BeneManage_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "BeneManage_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "<db_val>" on "BeneManage_TranNick"
	And verify the message "<bene_op_value>" through database on "<bene_op_type_query>" on Schema "<db_val>"
	And verify the message "<account_no>" through database on "<to_account_query>" on Schema "<db_val>"
	And verify the result from "<account_title_query>" on Schema "<db_val>"
	And verify the message "<Bank_Value>" through database on "<bene_bank_query>" on Schema "<db_val>"
	And verify the result of two queries "<bene_id_query>" on Schema "<db_val>" with "<bene_id_tran_query>" on Schema "<db_val>" through database
	And I am performing on "BeneManage_Tran_CloseBtn"
	And I have given "<account_no>" on "SendMoney_SearchBeneField"
	And verify through "<account_no>" on "Pay_Transaction_PayBill_BeneSearchConsumerNo"
	And I set value in context from Keyword "BeneManage_Edit_Title" as "Bene_Name"
	And verify the message "<account_no>" through database on "<account_no_query>" on Schema "<db_val>"
	And verify the message "<Email>" through database on "<email_query>" on Schema "<db_val>"
	And verify the message "<Mobile_No>" through database on "<mobile_query>" on Schema "<db_val>"
	And verify the message "<BeneNick_Value>" through database on "<nick_query>" on Schema "<db_val>"
	And verify the message "0" through database on "<added_bene_query>" on Schema "<db_val>"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I select "Beneficiary Management" on "Services_Transaction_Type"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "BeneManage_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "BeneManage_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "<db_val>" on "BeneManage_TranNick"
	And I am performing on "Investment_TranActivityCloseBtn"

	@source:Data/BeneficiaryAddition.xlsx
	Examples: 
	|Case|Expected_Result|account_no|Bank_Value|BeneNick_Value|Email|Mobile_No|OTP_Value|tran_type_query|tran_response_query|tran_date_query|tran_bene_name_query|account_no_query|email_query|mobile_query|nick_query|added_bene_query|db_val|bene_op_type_query|bene_op_value|to_account_query|account_title_query|bene_bank_query|bene_id_tran_query|bene_id_query|

