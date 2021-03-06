Feature: ForgetChange
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


@ForgetChange @ForgetPassword
Scenario Outline: 2 As a user i want to verify forget password
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic_no>" as "customer_cnic"
	And I set value in context from database "<customer_type_query>" as "customer_type" on Schema "<db_val>"
	And I set value in context from database "<customer_info_id_query>" as "customer_info_id" on Schema "<db_val>"
	And the user is arrive to Internet Banking home page 	
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Forget_btn_Password"
	And I have given "<login_id>" on "Forget_Login_field"
	When I am performing on "Forget_PasswordNextbtn"
	And I have given "<cnic_no>" on "Forget_Password_CNIC"
	And I have given "<debit_card>" on "Forget_Password_DebitCardNo"
	And I have given "<card_pin>" on "Forget_Password_PIN"
	And I have given "<Credit_card>" on "Forget_Password_CreditCardNo"
	And I have given "<email>" on "Forget_Password_Email"
	And I am performing on "Forget_PasswordSubmitBtn"
	And I wait 5000
	And I set value in context from database "<mobile_no_query>" as "mobile_number" on Schema "<db_val>"
	And verify the message "<lead_field_value>" through database on "<lead_field_query>" on Schema "<db_val>"
	And verify through "<OTP_message>" on "Forget_Change_OTPMsg"
	And I set value in context from data "true" as "change_loginID_check"
	And I have given "" on "Login_OTP_field"
	And I am performing on "Forget_Password_OTPNextBtn"
	And I have given "<new_password>" on "Forget_LoginPass"
	And I have given "<new_re_password>" on "Forget_LoginRePass"
	And verify through database on "<new_password_policy_query>" on Schema "<db_val>" on "Forget_Policy"
	And I am performing on "Forget_Password_NextBtn"
	Then verify through "<success_message_password>" on "Forget_SuccessMsg"
	And I am performing on "Forget_OkBtn"
	And verify the result from "<last_pass_change_query>" on Schema "<db_val>"
	And verify the message "<is_password_change_required_value>" through database on "<password_change_req_query>" on Schema "<db_val>"
	And verify the message "<is_password_reset_required_value>" through database on "<is_password_reset_required_query>" on Schema "<db_val>"
	And verify the message "<customer_type>" through database on "<customer_type_tran_query>" on Schema "<db_val>"
	And verify the message "<cnic_no>" through database on "<cnic_tran_query>" on Schema "<db_val>"
	And verify the message "<email>" through database on "<email_tran_query>" on Schema "<db_val>"
	And verify the message "" through database on "<mobile_no_tran_query>" on Schema "<db_val>"
	And verify the result from "<created_on_tran_query>" on Schema "<db_val>"
	And verify the result from "<updated_on_tran_query>" on Schema "<db_val>"
	And I have given "<login_id>" on "Login_UserId"
	And I have given "<new_password>" on "Login_Password"
	And I am performing on "Login_SignIn_Button"
	And I have given "" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	And I am clicking on "Signup_SkipBtn"
	And I am clicking on "Login_Dashboard"
	And I wait 6000
	And verify through "Welcome" on "Login_Success_Text"
	And update the data by query "<password_query>" on DIGITAL_CHANNEL_SEC
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I select "Password Reset" on "Services_Transaction_Type"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Forget_Services_Row"
	And verify through database on "Successful " on Schema "<db_val>" on "Pay_MultiBill_SRV_TranStatus"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranDate"
	And verify through database on "<tran_debit_query>" on Schema "<db_val>" on "Forget_Services_TranPopup_Debit"
	And I am clicking on "Services_Transaction_Close_btn"
	And I am clicking on "Login_Logout_opt"
	And I am performing on "Login_LogoutBtn"

	@source:Data/ForgetPassword.xlsx
	Examples:
	|Case|login_id|customer_type_query|cnic_no|debit_card|card_pin|Credit_card|email|new_password|new_re_password|password_query|password_change_req_query|mobile_no_query|OTP_message|customer_info_id_query|success_message_password|db_val|lead_field_value|lead_field_query|last_pass_change_query|is_password_change_required_value|is_password_reset_required_value|is_password_reset_required_query|new_password_policy_query|tran_type_query|tran_date_query|tran_debit_query|created_on_tran_query|updated_on_tran_query|cnic_tran_query|customer_type_tran_query|email_tran_query|mobile_no_tran_query|customer_type|



@ForgetChange @ForgetLoginID
Scenario Outline: 2 As a user i want to verify forget Login ID
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic_no>" as "customer_cnic"
	And I set value in context from data "<mobile_no>" as "mobile_number"
	And the user is arrive to Internet Banking home page 	
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Forget_btn_Login"
	When I have given "<cnic_no>" on "Forget_CNIC"
	And I am performing on "Forget_Login_NextBtn"
	And I set value in context from database "<customer_type_query>" as "customer_type" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<mobile_no>" on "Forget_LoginID_Mobile"
	And I have given "<debit_card_no>" on "Forget_LoginID_DebitNo"
	And I have given "<credit_card_no>" on "Forget_LoginID_CreditNo"
	And I have given "<email_value>" on "Forget_LoginID_CreditEmail"
	And I have given "<pin>" on "Forget_LoginID_DebitPin"
	And I am performing on "Forget_LoginID_NextBtn"
	Then verify through "<success_message>" on "Forget_Success_LoginMsg"
	And I am performing on "Forget_Login_FinishBtn"
	And verify the message "<customer_type>" through database on "<customer_type_tran_query>" on Schema "<db_val>"
	And verify the message "<cnic_no>" through database on "<cnic_tran_query>" on Schema "<db_val>"
	And verify the message "<email>" through database on "<email_tran_query>" on Schema "<db_val>"
	And verify the message "" through database on "<mobile_no_tran_query>" on Schema "<db_val>"
	And verify the result from "<created_on_tran_query>" on Schema "<db_val>"
	And verify the result from "<updated_on_tran_query>" on Schema "<db_val>"
	And I have given "<login_id>" on "Login_UserId"
	And I have given "<login_password>" on "Login_Password"
	And I am performing on "Login_SignIn_Button"
	And I have given "" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	And I am clicking on "Login_Dashboard"
	And I wait 6000
	And verify through "Welcome" on "Login_Success_Text"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I select "Change User Credentials" on "Services_Transaction_Type"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Forget_ChangeLogin_Services_Row"
	And verify through database on "Successful " on Schema "<db_val>" on "Pay_MultiBill_SRV_TranStatus"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranDate"
	And I am clicking on "Services_Transaction_Close_btn"
	And I am clicking on "Login_Logout_opt"
	And I am performing on "Login_LogoutBtn"


	@source:Data/ForgetLoginID.xlsx
	Examples:
	|Case|customer_type_query|cnic_no|mobile_no|debit_card_no|credit_card_no|email_value|pin|success_message|login_id|login_password|tran_type_query|tran_date_query|customer_type_tran_query|cnic_tran_query|email_tran_query|mobile_no_tran_query|created_on_tran_query|updated_on_tran_query|



@ForgetChange @ChangeLoginID
Scenario Outline: 2 As a user i want to verify Change Login ID
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic>" as "customer_cnic"
	And the user is arrive to Internet Banking home page 	
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Forget_btn_Login"
	And I am clicking on "Forget_ChangeLoginNav"
	And I have given "<cnic>" on "Forget_ChangeCNIC"
	When I am performing on "Forget_ChangeLoginNextBtn"
	And I set value in context from database "<customer_type_query>" as "customer_type" on Schema "<db_val>"
	And I set value in context from database "<customer_info_id_query>" as "customer_info_id" on Schema "<db_val>"
	And verify through database on "<change_pass_policy_query>" on Schema "<db_val>" on "Forget_ChangeLogin_PassPolicy"
	And I have given "<debit_card>" on "Forget_Change_DebitNo"
	And I have given "<card_pin>" on "Forget_Change_DebitPin"
	And I have given "<Credit_card>" on "Forget_Change_CreditNo"
	And I have given "<email>" on "Forget_Change_CreditEmail"
	And I have given "<new_login_id>" on "Forget_Change_NewLogin"
	And I am performing on "Forget_Change_NextBtn"
	And I wait 5000
	And I set value in context from database "<mobile_no_query>" as "mobile_number" on Schema "<db_val>"
	And verify through "<OTP_message>" on "Forget_Change_OTPMsg"
	And I set value in context from data "true" as "change_loginID_check"
	And I have given "" on "Login_OTP_field"
	And I am performing on "Forget_Change_OTPNextBtn"
	And I have given "<new_password>" on "Forget_LoginPass"
	And I have given "<new_re_password>" on "Forget_LoginRePass"
	And I have given "<tran_pass>" on "Forget_Change_LoginTran"
	And I have given "<new_tran_re_pass>" on "Forget_Change_LoginReTran"
	And verify through database on "<new_password_policy_query>" on Schema "<db_val>" on "Forget_Policy"
	And I am performing on "Forget_Change_FinishBtn"
	Then verify through "<success_message_password>" on "Forget_SuccessMsg"
	And I am performing on "Forget_OkBtn"
	And verify the message "<new_login_id>" through database on "<new_login_id_query>" on Schema "<db_val>"
	And verify the result from "<last_tran_pass_change_query>" on Schema "<db_val>"
	And verify the result from "<last_pass_change_query>" on Schema "<db_val>"
	And verify the message "<is_password_change_required_value>" through database on "<is_password_change_required_query>" on Schema "<db_val>"
	And verify the message "<is_password_reset_required_value>" through database on "<is_password_reset_required_query>" on Schema "<db_val>"
	And verify the message "0" through database on "<is_account_blocked_query>" on Schema "<db_val>"
	And verify the message "0" through database on "<is_account_locked_query>" on Schema "<db_val>"
	And verify the message "0" through database on "<is_tran_password_locked_query>" on Schema "<db_val>"
	And verify the message "<customer_type>" through database on "<customer_type_tran_query>" on Schema "<db_val>"
	And verify the message "<cnic_no>" through database on "<cnic_tran_query>" on Schema "<db_val>"
	And verify the message "<email>" through database on "<email_tran_query>" on Schema "<db_val>"
	And verify the message "" through database on "<mobile_no_tran_query>" on Schema "<db_val>"
	And verify the message "<new_login_id>" through database on "<new_login_id_tran_query>" on Schema "<db_val>"
	And verify the result from "<created_on_tran_query>" on Schema "<db_val>"
	And verify the result from "<updated_on_tran_query>" on Schema "<db_val>"
	And I have given "<new_login_id>" on "Login_UserId"
	And I have given "<new_password>" on "Login_Password"
	And I am performing on "Login_SignIn_Button"
	And I have given "" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	And I am clicking on "Login_Dashboard"
	And I wait 6000
	And verify through "Welcome" on "Login_Success_Text"
	And update the data by query "<name_update_query>" on DIGITAL_CHANNEL_SEC
	And I wait 6000
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "Non Financial" on "Services_CategoryFilter"
	And I select "Change User Credentials" on "Services_Transaction_Type"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Forget_Change_Services_Row"
	And verify through database on "Successful " on Schema "<db_val>" on "Pay_MultiBill_SRV_TranStatus"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "MyAccount_TranPopup_TranDate"
	And I am clicking on "Services_Transaction_Close_btn"
	And I am clicking on "Login_Logout_opt"
	And I am performing on "Login_LogoutBtn"

	@source:Data/ChangeLoginID.xlsx
	Examples:
	|Case|cnic|debit_card|card_pin|Credit_card|email|new_login_id|customer_type_query|tran_pass|new_password|new_re_password|new_tran_re_pass|success_message_password|password_change_req_query|new_login_id_query|change_pass_policy_query|mobile_no_query|OTP_message|db_val|last_pass_change_query|last_tran_pass_change_query|is_password_change_required_value|is_password_change_required_query|is_password_reset_required_value|is_password_reset_required_query|name_update_query|customer_info_id_query|new_password_policy_query|is_account_blocked_query|is_account_locked_query|is_tran_password_locked_query|tran_type_query|tran_date_query|created_on_tran_query|updated_on_tran_query|cnic_tran_query|customer_type_tran_query|email_tran_query|mobile_no_tran_query|customer_type|new_login_id_tran_query|
