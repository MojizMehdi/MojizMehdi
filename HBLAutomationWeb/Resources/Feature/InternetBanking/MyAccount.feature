﻿Feature: MyAccount
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MyAccount
Scenario Outline: As a user I want to verify Account Linking & De-Linking from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I count Number of Account
	And I save Account Balances
	And I am clicking on "MyAccount_Icon"
	#And I am clicking on "MyAccount_AccLinkOption"
	And I select "<De_Linking_Account>" for Account linking or de-linking "<operation_type>" with success message as "<Success_message>"
	When I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "<Transaction_Category>" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	Then verify through database on "Successful" on Schema "<db_val>" on "Pay_MultiBill_SRV_TranStatus"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "Pay_MultiBill_SRV_TranType"
	And I am clicking on "MyAccount_Services_CloseBtn"

	@source:Data/MyAccount.xlsx
	Examples: 
	|Case|Linking_Account|De_Linking_Account|operation_type|Success_message|db_val|tran_type_query|Transaction_Category|



@MyAccount
Scenario Outline: As a user I want to verify Limit Management from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "MyAccount_Icon"
	And I wait 5000
	And I am clicking on "MyAccount_LimitMngOption"
	And I am performing operation "MyAccount_Limit_Edit_Icon" of Slider "MyAccount_Limit_Slider" of "<limit_type>" with new limit as "<new_limit>"
	When I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "<Transaction_Category>" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"

	@source:Data/LimitManagement.xlsx
	Examples: 
	|Case|limit_type|new_limit|Transaction_Category|


@MyAccount
Scenario Outline: As a user i want to verify change user login password
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button" 
	And verify through "Welcome" on "Login_Success_Text"
	And I wait 5000
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_ChangePassOptn"
	And verify the result from "<password_policy_query>" on Schema "<db_val>"
	And verify through "MyAccount_PassPolicy" on "MyAccount_Forgot_UserPassPolicy1"
	And I have given "<Login_Password_Value>" on "Settings_login_old_pass"
	And I have given "<Login_New_Password>" on "Settings_login_new_pass"
	And I have given "<Login_New_Password>" on "Settings_login_confirm_pass"
	And I am performing on "Settings_change_login_button"
	Then verify through "<success_message>" on "MyAccount_Forgot_Tran_Success"
	And I am clicking on "MyAccount_ChangePassOKBtn"
	And verify the result from "<password_change_req_query>" on Schema "<db_val>"
	And verify the result from "<IS_PASSWORD_RESET_REQUIRED>" on Schema "<db_val>"
	And verify the result from "<LAST_PASSWORD_CHANGED>" on Schema "<db_val>"
	And verify the result from "<LGN_PWD_CHANGED_POPUP_COUNT>" on Schema "<db_val>"
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_New_Password>" on "Login_Password"
	And I am performing on "Login_SignIn_Button"
	And verify through "Welcome" on "Login_Success_Text"
	And update the data by query "<Update_Password_query>" on DIGITAL_CHANNEL_SEC
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	@source:Data/ChangeLoginPassword.xlsx
	Examples:
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|Login_New_Password|Update_Password_query|password_change_req_query|IS_PASSWORD_RESET_REQUIRED|password_policy_query|success_message|LAST_PASSWORD_CHANGED|LGN_PWD_CHANGED_POPUP_COUNT|tran_type_query|success_message_query|tran_date_query|db_val|




@MyAccount
Scenario Outline: As a user i want to verify change user Transaction password
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button" 
	And verify through "Welcome" on "Login_Success_Text"
	And I sleep "5000"
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_ChangeTranPassOptn"
	And verify the result from "<password_policy_query>" on Schema "<db_val>"
	And verify through "MyAccount_PassPolicy" on "MyAccount_Forgot_TranPassPolicy1"
	And I have given "<tran_old_password>" on "MyAccount_TranOldPass"
	And I have given "<tran_new_password>" on "MyAccount_TranNewPass"
	And I have given "<tran_new_password>" on "MyAccount_TranConfirmPass"
	And I am performing on "MyAccount_TranChangeBtn"
	Then verify through "<success_message>" on "MyAccount_TranPopup_TranSuccess"
	And verify through database on "<success_message_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranDate"
	And I am performing on "MyAccount_TranPopupClose"
	And verify the result from "<LAST_TRANS_PASSWORD_CHANGED>" on Schema "<db_val>"
	And I am clicking on "Login_Dashboard"
	And update the data by query "<Update_Password_query>" on DIGITAL_CHANNEL_SEC

	@source:Data/ChangeTransactionPassword.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|tran_old_password|tran_new_password|Update_Password_query|password_policy_query|success_message|db_val|LAST_TRANS_PASSWORD_CHANGED|



@MyAccount
Scenario Outline: As a user i want to verify Forgot Transaction password
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button" 
	And verify through "Welcome" on "Login_Success_Text"
	And I sleep "5000"
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_ChangeTranPassOptn"
	And I am clicking on "MyAccount_Tran_ForgotLink"
	And I have given "<cnic>" on "MyAccount_Forgot_TranCNIC"
	And I have given "<dob>" on "MyAccount_Forgot_TranDOB"
	And I have given "<cardNo>" on "MyAccount_Forgot_TranCardNo"
	And I have given "<pin>" on "MyAccount_Forgot_TranPIN"
	And I am performing on "MyAccount_Forgot_TranContBtn"
	And verify the result from "<password_policy_query>" on Schema "<db_val>"
	And verify through "MyAccount_PassPolicy" on "MyAccount_Forgot_TranPassPolicy1"
	And I have given "<tran_new_password>" on "MyAccount_Forgot_Tran_NewPass"
	And I have given "<tran_new_password>" on "MyAccount_Forgot_Tran_CnfrmPass"
	Then I am performing on "MyAccount_Forgot_Tran_ChangeBtn"
	And verify through "success_message" on "MyAccount_Forgot_Tran_Success"
	And I am performing on "MyAccount_Forgot_Tran_OkBtn"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_Status"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "MyAccount_Forgot_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "MyAccount_Forgot_TranDate"
	And I am performing on "MyAccount_Forgot_CloseBtn"
	And update the data by query "<Update_Password_query>" on DIGITAL_CHANNEL_SEC


	@source:Data/ForgotTransactionPassword.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|tran_new_password|Update_Password_query|password_policy_query|success_message|tran_type_query|tran_date_query|db_val|cnic|dob|cardNo|pin|LAST_TRANS_PASSWORD_CHANGED|



@MyAccount
Scenario Outline: As a user I want to verify Cheque Book Request from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<home_branch_option>" as "home_branch_del_flag"
	And I count Number of Account
	And I am clicking on "MyAccount_Icon"
	And I wait 5000
	And I am clicking on "MyAccount_CheqBook_Opt"
	And verify the message "D" through database on "<customer_type_query>" on Schema "<db_val>"
	When I select "<for_account>" on "MyAccount_CheqBook_forAcc"
	And I select "<no_of_cheque>" on "MyAccount_CheqBook_No"
	And I am clicking on "MyAccount_CheqBook_BranchCheck"
	And I select "<City>" on "MyAccount_CheqBook_CityList"
	And I select "<Branch>" on "MyAccount_CheqBook_BranchList"
	And I have given "<transaction_password>" on "MyAccount_CheqBook_TranPass"
	Then I am performing on "MyAccount_CheqBook_ReqBtn"

	@source:Data/ChequeBook.xlsx
	Examples: 
	|Case|for_account|no_of_cheque|home_branch_option|City|Branch|transaction_password|customer_type_query|db_val|



@MyAccount
Scenario Outline: As a user I want to verify Pay Order Request from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I count Number of Account
	And I am clicking on "MyAccount_Icon"
	And I wait 5000
	And I am clicking on "MyAccount_PayOrder_Opt"
	When I select "<for_account>" on "MyAccount_PayOrder_ForAcc"
	And I have given "<amount>" on "MyAccount_PayOrder_Amount"
	And I have given "<bene_name>" on "MyAccount_PayOrder_Bene"
	And I select "<purpose>" on "MyAccount_PayOrder_Purpose"
	And I select "<city>" on "MyAccount_PayOrder_City"
	And I select "<branch>" on "MyAccount_PayOrder_Branch"
	And I have given "<transaction_password>" on "MyAccount_CheqBook_TranPass"
	Then I am performing on "MyAccount_PayOrder_ReqBtn"


	@source:Data/PayOrder.xlsx
	Examples: 
	|Case|for_account|amount|bene_name|purpose|city|branch|transaction_password|



@MyAccount
Scenario Outline: As a user I want to verify Withholding Tax Certificate Request from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I count Number of Account
	And I am clicking on "MyAccount_Icon"
	And I wait 5000
	And I am clicking on "MyAccount_WithHold_Tax_Opt"
	When I select "<for_account>" on "MyAccount_WithHoldTax_ForAcc"
	And I am clicking on "MyAccount_WithHoldTax_FromDate"
	And I select date "<from_day>" on month "<from_month>" on year "<from_year>"
	And I am clicking on "MyAccount_WithHoldTax_ToDate"
	And I select date "<to_day>" on month "<to_month>" on year "<to_year>"
	Then I am performing on "MyAccount_WithHoldTax_ReqBtn"
	And verify the message "Success" through database on "<tran_status_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<for_account>" through database on "<from_account_tran_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<from_date>" through database on "<from_account_tran_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<to_date>" through database on "<to_date_tran_query>" on Schema "DIGITAL_CHANNEL_SEC"


	@source:Data/WithHoldingTaxCertifcate.xlsx
	Examples: 
	|Case|for_account|from_day|from_month|from_year|from_date|to_day|to_month|to_year|to_date|tran_status_query|from_account_tran_query|from_date_tran_query|to_date_tran_query|