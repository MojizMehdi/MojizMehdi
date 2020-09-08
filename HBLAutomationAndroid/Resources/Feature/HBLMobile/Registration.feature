Feature: Registration
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


@Registration
Scenario Outline: 2 As a user i want to Signup using Debit Card Mobile
	Given the test case title is "<Case>"	
	And the user is arrive to Mobile Banking home page 
	And I set value in context from data "<CNIC_D>" as "customer_cnic"
	And I set value in context from data "True" as "SignupCheck"
	And I set value in context from data "<account_tag1>" as "AccountForTag" 
	And I am clicking on "Registration_HomeBtn"
	When I have given "<CNIC_D>" on "Registration_Cnic"
	And I am clicking on "Registration_NextBtn"
	And I wait 15000
	And I scroll down
	And I scroll down
	And I am clicking on "Registration_AgreeBtn"
	And I have given "<Debit_card_no>" on "Registration_CardNo"
	And I have given "<pin>" on "Registration_CardPin"
	And I have given "<Login_id>" on "Registration_LoginId"
	#And verify the message using element "Registration_Password_Policy" through database on "<login_id_policy_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I am clicking on "Registration_NextBtn"
	And I wait 5000
	And I am clicking on "Registration_AccountNo_Marking"
	And I save Account Numbers
	And I am clicking on "Registration_AccMark_NextBtn"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Registration_NextBtn"
	And I wait 10000
	And verify the message "<account_tag1>" through database on "<account_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<account_tag1>" through database on "<account_tag_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<account_tag1>" through database on "<account_tag_query_not_linked>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "Registration_Password_Policy" through database on "<password_policy_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<login_pass>" on "Registration_Password"
	And I have given "<login_pass>" on "Registration_Confirm_Password"
	And I have given "<tran_pass>" on "Registration_Tran_Password"
	And I have given "<tran_pass>" on "Registration_Confirm_Tran_Password"
	And I am clicking on "Registration_NextBtn"
	And I wait 2000
	Then verify through "<success_message>" on "Registration_SuccessMessage"
	And I am clicking on "Registration_SuccessMessage_OK_Btn"
	And verify the message "<Login_id>" through database on "<login_id_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<created_on_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<updated_on_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<last_pass_change_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<last_tran_pass_change_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<is_password_change_required_value>" through database on "<is_password_change_required_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<is_password_reset_required_value>" through database on "<is_password_reset_required_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the data using "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the message "<tran_pass_encrypted_value>" through database on "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<customer_type>" through database on "<customer_type_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<IVR_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<IVR_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<PARAM_CHANNEL_ID_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<ENABLE_PSD_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<ENABLE_PSD_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<Login_id>" on "Login_UserId"
	And I set value in context from data "<Login_id>" as "username"
	And I have given "<login_pass>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I set value in context from data "true" as "Last_login_flag"
	And I wait 3000
	And I am clicking on "Registration_Facebook"
	And I am clicking on "Registration_Facebook_Submit"
	And I am clicking on "Registration_Facebook_Submit_OK"
	#Then verify through "Welcome" on "Login_Success_Text"
	Then verify the data using "<Last_login_query>" on Schema "DIGITAL_CHANNEL_SEC"
	@source:Data/DebitRegistration.xlsx
	Examples:
	|Case|CNIC_D|Debit_card_no|pin|Login_id|login_id_policy_query|OTP_Value|password_policy_query|login_pass|tran_pass|account_tag1|account_query|account_tag_query|account_tag_query_not_linked|success_message|login_id_query|created_on_query|updated_on_query|last_pass_change_query|last_tran_pass_change_query|is_password_change_required_value|is_password_change_required_query|is_password_reset_required_value|is_password_reset_required_query|Last_login_query|tran_pass_encrypted_value|transaction_password_query|customer_type|customer_type_query|IVR_require_query|IVR_check_query|PARAM_CHANNEL_ID_query|ENABLE_PSD_require_query|ENABLE_PSD_check_query|
	
	
@Registration
Scenario Outline: 2 As a user i want to Signup using Credit Card Mobile
	Given the test case title is "<Case>"	
	And the user is arrive to Mobile Banking home page 
	#And I set value in context from data "<CNIC_D>" as "username"
	And I set value in context from data "<CNIC_D>" as "customer_cnic"
	And I set value in context from data "True" as "SignupCheck"
	#And I set value in context from data "<account_tag1>" as "AccountForTag" 
	And I am clicking on "Registration_HomeBtn"
	When I have given "<CNIC_D>" on "Registration_Cnic"
	And I am clicking on "Registration_NextBtn"
	And I wait 15000
	And I scroll down
	And I scroll down
	And I am clicking on "Registration_AgreeBtn"
	#And verify the message using element "Registration_Password_Policy" through database on "<login_id_policy_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<Credit_card_no>" on "Registration_CardNo"
	And I have given "<Customer_Email>" on "Registration_Email"
	And I have given "<Login_id>" on "Registration_LoginIdCC"
	And I am clicking on "Registration_NextBtn"
	And I wait 5000
	#And I am clicking on "Registration_AccountNo_Marking"
	#And I save Account Numbers
	#And I am clicking on "Registration_AccMark_NextBtn"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Registration_NextBtn"
	And I wait 10000
	#And verify the message "<account_tag1>" through database on "<account_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the message "<account_tag1>" through database on "<account_tag_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message using element "Registration_Password_Policy" through database on "<password_policy_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<login_pass>" on "Registration_Password"
	And I have given "<login_pass>" on "Registration_Confirm_Password"
	And I have given "<tran_pass>" on "Registration_Tran_Password"
	And I have given "<tran_pass>" on "Registration_Confirm_Tran_Password"
	And I am clicking on "Registration_NextBtn"
	And I wait 2000
	Then verify through "<success_message>" on "Registration_SuccessMessage"
	And I am clicking on "Registration_SuccessMessage_OK_Btn"
	And verify the message "<Login_id>" through database on "<login_id_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<created_on_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<updated_on_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<last_pass_change_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<last_tran_pass_change_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<is_password_change_required_value>" through database on "<is_password_change_required_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<is_password_reset_required_value>" through database on "<is_password_reset_required_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the data using "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the message "<tran_pass_encrypted_value>" through database on "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the data using "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	#And verify the message "<tran_pass_encrypted_value>" through database on "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<customer_type>" through database on "<customer_type_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<IVR_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<IVR_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<PARAM_CHANNEL_ID_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<ENABLE_PSD_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<ENABLE_PSD_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<Login_id>" on "Login_UserId"
	And I set value in context from data "<Login_id>" as "username"
	And I have given "<login_pass>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I set value in context from data "true" as "Last_login_flag"
	And I wait 3000
	And I am clicking on "Registration_Facebook"
	And I am clicking on "Registration_Facebook_Submit"
	And I am clicking on "Registration_Facebook_Submit_OK"
	#Then verify through "Welcome" on "Login_Success_Text"
	Then verify the data using "<Last_login_query>" on Schema "DIGITAL_CHANNEL_SEC"
	@source:Data/CreditRegistration.xlsx
	Examples:
	|Case|CNIC_D|Credit_card_no|Customer_Email|Login_id|login_id_policy_query|OTP_Value|password_policy_query|login_pass|tran_pass|success_message|login_id_query|created_on_query|updated_on_query|last_pass_change_query|last_tran_pass_change_query|is_password_change_required_value|is_password_change_required_query|is_password_reset_required_value|is_password_reset_required_query|Last_login_query|tran_pass_encrypted_value|transaction_password_query|customer_type|customer_type_query|IVR_require_query|IVR_check_query|PARAM_CHANNEL_ID_query|ENABLE_PSD_require_query|ENABLE_PSD_check_query|


	@Registration
Scenario Outline: 2 As a user i want to Signup using Account Mobile
	Given the test case title is "<Case>"	
	And the user is arrive to Mobile Banking home page 
	And I set value in context from data "<CNIC_D>" as "username"
	And I set value in context from data "<CNIC_D>" as "customer_cnic"
	And I set value in context from data "True" as "SignupCheck"
	#And I set value in context from data "<account_tag1>" as "AccountForTag" 
	And I am clicking on "Registration_HomeBtn"
	When I have given "<CNIC_D>" on "Registration_Cnic"
	And I am clicking on "Registration_NextBtn"
	And I wait 15000
	And I scroll down
	And I scroll down
	And I am clicking on "Registration_AgreeBtn"
	And I am clicking on "Registration_NextBtn"
	And I have given "<Login_id>" on "Registration_LoginId"
	And I am clicking on "Registration_NextBtn"
	And I change screen attribute value on "Login_OTP_field"
	And I have given "<OTP_Value>" on "Login_OTP_field"

	@source:Data/AccountRegistration.xlsx
	Examples:
	|Case|CNIC_D|Credit_card_no|Customer_Email|Login_id|OTP_Value|password_policy_query|login_pass|tran_pass|success_message|login_id_query|created_on_query|updated_on_query|Last_login_query|tran_pass_encrypted_value|transaction_password_query|customer_type|customer_type_query|IVR_require_query|IVR_check_query|PARAM_CHANNEL_ID_query|ENABLE_PSD_require_query|ENABLE_PSD_check_query|
