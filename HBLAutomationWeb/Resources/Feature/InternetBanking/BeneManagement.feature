Feature: BeneManagement
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@BeneMng
Scenario Outline: As a user I want to verify Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I am clicking on "BeneManage_SendMoney_Tab"
	Then I want to verify already added beneficiaries with query "<send_money_query>" of Schema "<db_val>" on keyword "BeneManage_SendMoney_BeneCount"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "BeneManage_Link"
	And I am clicking on "BeneManage_Pay_Tab"
	Then I want to verify already added beneficiaries with query "<pay_query>" of Schema "<db_val>" on keyword "BeneManage_Pay_BeneCount"

	@source:Data/BeneVerification.xlsx
	Examples: 
	|Case|db_val|send_money_query|pay_query|


@BeneMng
Scenario Outline: As a user I want to verify Editing Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I am clicking on "BeneManage_SendMoney_Tab"
	Then I am clicking on "BeneManage_Edit"
	And I have given "<nick>" on "BeneManage_Edit_Nick"
	And I have given "<email>" on "BeneManage_Edit_Email"
	And I have given "<mobile>" on "BeneManage_Edit_Mobile"
	And I am performing "OK" alert operation on cross icon on "BeneManage_Edit_UpdateBtn"
	And verify through "<success_message>" on "BeneManage_Delete_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranBene"
	And I am performing on "MyAccount_TranPopUp_CloseBtn"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranBene"
	And I am performing on "Investment_TranActivityCloseBtn"

	@source:Data/BeneEditing.xlsx
	Examples: 
	|Case|success_message|nick|email|mobile|tran_type_query|tran_date_query|tran_bene_name_query|



@BeneMng
Scenario Outline: As a user I want to verify Deleting Beneficiaries
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	And I am clicking on "Login_Dashboard"
	When I am clicking on "BeneManage_Link"
	And I am clicking on "BeneManage_SendMoney_Tab"
	Then I am clicking on "BeneManage_Delete"
	And I am performing "OK" alert operation on cross icon on "Signup_CrossIcon"
	And verify through "<success_message>" on "BeneManage_Delete_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranBene"
	And I am performing on "BeneManage_Delete_TranCloseBtn"
	And verify the result from "<delete_bene_query>" on Schema "<db_val>"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranType"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_Delete_TranBene"
	And I am performing on "Investment_TranActivityCloseBtn"

	@source:Data/BeneDeletion.xlsx
	Examples: 
	|Case|account_no|success_message|tran_type_query|tran_bene_name_query|tran_date_query|delete_bene_query|


@BeneMng
Scenario Outline: As a user i want to Verify Beneficiary Addition for Send Money
	Given the test case title is "<Case>"
	And I set value in context from data "<account_no>" as "Bene_AccountNo"
	And update the data by query "<StatusQuery>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page
 	And I am clicking on "Login_Dashboard"
	And I am clicking on "BeneManage_Link"
	And I am clicking on "BeneManage_SendMoney_Tab"
	And I am clicking on "BeneManage_AddNewBtn"
	When I select "<Bank_Value>" on "BeneManage_Bank"
	And I have given "<account_no>" on "BeneManage_AccountNo"
	And I have given "<BeneNick_Value>" on "BeneManage_BeneNick"
	And I have given "<Email>" on "BeneManage_PayeeEmail"
	And I have given "<Mobile_No>" on "BeneManage_PayeeMobileNumber"
	Then I am performing on "BeneManage_Validate_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	And verify through "Congratulations" on "BeneManage_TranCongrats"
	And verify through database on "<tran_response_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranResponseMsg"
	And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranType"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranNick"
	And I am performing on "BeneManage_Tran_CloseBtn"
	And verify the message "<account_no>" through database on "<account_no_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<Email>" through database on "<email_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<Mobile_No>" through database on "<mobile_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<BeneNick_Value>" through database on "<nick_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through database on "<tran_response_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranResponseMsg"
	And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranType"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranDate"
	And verify through database on "<tran_bene_name_query>" on Schema "DIGITAL_CHANNEL_SEC" on "BeneManage_TranNick"
	And I am performing on "Investment_TranActivityCloseBtn"

	@source:Data/BeneficiaryAddition.xlsx
	Examples: 
	|Case|account_no|StatusQuery|Bank_Value|BeneNick_Value|Email|Mobile_No|OTP_Value|tran_type_query|tran_response_query|tran_date_query|tran_bene_name_query|account_no_query|email_query|mobile_query|nick_query|

