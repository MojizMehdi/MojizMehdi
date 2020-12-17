Feature: ForgetChange
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Forget_Password
Scenario Outline: As a user i want to verify forget password for mobile banking
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic_no>" as "customer_cnic"
	And I set value in context from data "<login_id_value>" as "username"
	And update the data by query "<Status_Query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I set value in context from database "<customer_type_query>" as "customer_type" on Schema "DIGITAL_CHANNEL_SEC"
	And the user is arrive to Mobile Banking home page 
	And I wait 2000
	And I am clicking on "Login_permission_allow_btn"
	And I wait 1000
	And I am clicking on "Login_permission_allow_btn2"
	And I am clicking on "SendMoney_SkipBtn"
	When I am clicking on "Forget_PassowrdBtn"
	And I have given "<login_id_value>" on "Forget_PssowrdLoginId"
	And I am clicking on "Forget_Passowrd_Next"
	And I have given "<cnic_no>" on "Forget_PasswordCnic"
	And I have given "<card_number>" on "Forget_PasswordCardNumber"
	And I have given "<card_pin>" on "Forget_PasswordCardPin"
	And I have given "<card_email>" on "Forget_PasswordEmail"
	And I am clicking on "Forget_Passowrd_Next"
	And I wait 5000
	And verify the message "<lead_field_value>" through database on "<lead_field_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I scroll to element text as "One Time Password (OTP)"
	And I am clicking on "Forget_Passowrd_Next"
	And I wait 3000
	And verify the result from "<password_reset_req_query_old>" on Schema "DIGITAL_CHANNEL_SEC"
	And I have given "<Forget_Password_NewPass_Value>" on "Forget_Password_NewPass"
	And I have given "<Forget_Password_RepeatNewPass_Value>" on "Forget_Password_RepeatNewPass"
	And I am clicking on "Forget_Passowrd_Next"
	And verify through "<success_message>" on "Forget_Password_SuccessMessage"
	And I am clicking on "Forget_Password_SuccessMessageOKBtn"
	#And I am clicking on "Forget_Passowrd_Next"
	And verify the result from "<password_reset_req_query>" on Schema "DIGITAL_CHANNEL_SEC"
	When I have given "<login_id_value>" on "Login_UserId"
	And I have given "<Forget_Password_NewPass_Value>" on "Login_Password"
	And I wait 2000
	And I am performing on "Login_SignIn_Button"
	And I set value in context from data "<login_id_value>" as "username"
	And I wait 30000
	And I scroll to element text as "One Time Password (OTP)"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "BillPayment_RatingOkBtn"
	Then verify through "Welcome, " on "Login_Success_Text"
	@source:Data/ForgetPassword.xlsx
	Examples:
	|Case|Status_Query|login_id_value|customer_type_query|cnic_no|card_number|card_pin|card_email|lead_field_value|lead_field_query|OTP_Value|password_reset_req_query_old|Forget_Password_NewPass_Value|Forget_Password_RepeatNewPass_Value|success_message|password_reset_req_query|


@Forget_LoginID
Scenario Outline: As a user i want to verify forget login id for mobile banking
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic_no>" as "customer_cnic"
	And I set value in context from database "<customer_type_query>" as "customer_type" on Schema "DIGITAL_CHANNEL_SEC"
	And the user is arrive to Mobile Banking home page 
	And I wait 2000
	And I am clicking on "Login_permission_allow_btn"
	And I wait 1000
	And I am clicking on "Login_permission_allow_btn2"
	And I am clicking on "SendMoney_SkipBtn"
	When I am clicking on "Forget_Login_Id_btn"
	And I am clicking on "Forget_Login_Id_tab"
	And I have given "<cnic_no>" on "Forget_Login_Id_cnic"
	And I am clicking on "Forget_Passowrd_Next"
	And I wait 5000
	And I have given "<mobile_no>" on "Forget_Login_Id_MobileNo"
	And I have given "<debit_card_no>" on "Forget_Login_Id_debitcardnumber"
	And I have given "<credit_card_no>" on "Forget_Login_Id_creditcardnumber"
	And I have given "<email_value>" on "Forget_Login_Id_email"
	And I have given "<pin>" on "Forget_Login_Id_debitcardpin"
	And I am clicking on "Forget_Passowrd_Next"
	And I set value in context from database "<mob_no_query>" as "mobile_no" on Schema "DIGITAL_CHANNEL_SEC"
	And I wait 5000
	And verify through "<success_message>" on "Forget_Login_Id_SuccessMessage"
	And verify through "<success_message_desc>" on "Forget_Login_Id_SuccessMessage_desc"
	#And I am clicking on "Forget_Password_SuccessMessageOKBtn"
	And I am clicking on "Forget_Passowrd_Next"

	@source:Data/ForgetLoginId.xlsx
	Examples:
	|Case|customer_type_query|cnic_no|mobile_no|debit_card_no|credit_card_no|email_value|pin|success_message|success_message_desc|mob_no_query|



@Change_LoginId
Scenario Outline: As a user i want to verify change login id for mobile banking
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic_no>" as "customer_cnic"
	And I set value in context from database "<customer_type_query>" as "customer_type" on Schema "DIGITAL_CHANNEL_SEC"
	And the user is arrive to Mobile Banking home page
	When I am clicking on "Forget_Login_Id_btn"
	And I am clicking on "Forget_Change_Login_Id_tab"
	And I have given "<cnic_no>" on "Forget_Login_Id_cnic"
	And I am clicking on "Forget_Passowrd_Next"
	And I wait 5000
	#And I have given "<mobile_no>" on "Forget_Login_Id_MobileNo"
	And I have given "<debit_card_no>" on "Forget_Change_Login_Id_DebitCardNo"
	And I have given "<credit_card_no>" on "Forget_Login_Id_creditcardnumber"
	And I have given "<pin>" on "Forget_Login_Id_debitcardpin"
	And I have given "<email_value>" on "Forget_Login_Id_email"
	And I have given "<new_login_id>" on "Forget_Change_New_Login_Id"
	And I am clicking on "Forget_Passowrd_Next"
	And I wait 5000
	And I scroll to element text as "One Time Password (OTP)"
	And I am clicking on "Login_OTP_Verify_Button"
	And I have given "<Forget_Password_NewPass_Value>" on "Forget_Change_Password"
	And I have given "<Forget_Password_RepeatNewPass_Value>" on "Forget_Change_Repeat_Password"
	And I have given "<Forget_Password_TranPass_Value>" on "Forget_Change_TranPassword"
	And I have given "<Forget_Password_RepeatTranPass_Value>" on "Forget_Change_Repeat_TranPassword"
	And I am clicking on "Forget_Passowrd_Next"
	And verify through "<success_message>" on "Forget_Password_SuccessMessage"
	And I am clicking on "Forget_Password_SuccessMessageOKBtn"
	#And I am clicking on "Forget_Passowrd_Next"
	When I have given "<new_login_id>" on "Login_UserId"
	And I have given "<Forget_Password_NewPass_Value>" on "Login_Password"
	And I wait 2000
	And I am performing on "Login_SignIn_Button"
	And I set value in context from data "<new_login_id>" as "username"
	And I wait 30000
	And I scroll to element text as "One Time Password (OTP)"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "BillPayment_RatingOkBtn"
	Then verify through "Welcome, " on "Login_Success_Text"

	@source:Data/ChangeLoginId.xlsx
	Examples:
	|Case|customer_type_query|cnic_no|mobile_no|debit_card_no|credit_card_no|email_value|pin|new_login_id|Forget_Password_NewPass_Value|Forget_Password_RepeatNewPass_Value|Forget_Password_TranPass_Value|Forget_Password_RepeatTranPass_Value|success_message|