Feature: ForgetChange
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@ForgetPassword
Scenario Outline: 2 As a user i want to verify forget password
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic_no>" as "customer_cnic"
	And the user is arrive to Internet Banking home page 	
	And I am clicking on "Forget_btn"
	And I have given "<login_id>" on "Forget_Login_field"
	When I am performing on "Forget_PasswordNextbtn"
	And verify the result from "<customer_type_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I am giving user details based on customer type as "<cnic_no>" and "<debit_card>" and "<card_pin>" and "<Credit_card>" and "<email>" on "Forget_Password_CNIC" and "Forget_Password_Debit" and "Forget_Password_PIN"
	And I am performing on "Forget_PasswordSubmitBtn"
	And verify through database on "<tran_message_qeury>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordResponseMsg"
	And verify through database on "<tran_id_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranID"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranDate"
	And verify through database on "<tran_debit_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranDebit"
	#And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranType"
	And I am performing on "Forget_PasswordTranSubmitBtn"
	And update the data by query "<password_query>" on DIGITAL_CHANNEL_SEC
	Then I have given "<login_id>" on "Login_UserId"
	And I have given "<activation_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	Then I have given "<new_password>" on "Forget_PasswordNew"
	And I have given "<new_password>" on "Forget_PasswordNewConfirm"
	And I am performing on "Signup_SubmitBtn"
	And verify through database on "<tran_message_qeury>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordResponseMsg"
	And verify through database on "<tran_id_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranID"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranDate"
	#And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranType"
	And I am performing on "Forget_PasswordTranSubmitBtn"
	And verify the result from "<password_change_req_query>" on Schema "DIGITAL_CHANNEL_SEC"
	Then I have given "<login_id>" on "Login_UserId"
	And I have given "<new_password>" on "Login_Password"
	And I am performing on "Signup_SubmitBtn"
	And I wait 4000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	Then verify through "Welcome" on "Login_Success_Text"

	@source:Data/ForgetPassword.xlsx
	Examples:
	|Case|login_id|customer_type_query|cnic_no|debit_card|card_pin|Credit_card|email|tran_message_qeury|tran_id_query|tran_type_query|tran_date_query|tran_debit_query|password_change_req_query|



@ChangeLoginID
Scenario Outline: 2 As a user i want to verify Change Login ID
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic>" as "customer_cnic"
	And the user is arrive to Internet Banking home page 	
	And I am clicking on "Forget_btn"
	And I am clicking on "Forget_ChangeLink"
	And I am clicking on "Forget_ChangeLoginNav"
	And I have given "<cnic>" on "Forget_ChangeCNIC"
	When I am performing on "Forget_ChangeLoginSubmitBtn"
	And verify the result from "<customer_type_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And I am giving user details based on customer type as "<new_login_id>" and "<debit_card>" and "<card_pin>" and "<Credit_card>" and "<email>" on "Forget_ChangeLogin_NewLogin" and "Forget_ChangeLogin_DCard" and "Forget_ChangeLogin_PIN"  
	And I am performing on "Forget_ChangeLogin_SubmitBtn"
	Then verify through "<success_msg>" on "Forget_ChangeSuccessMsg"
	And I am performing on "Forget_ChangeOkBtn"
	And verify the message "<new_login_id>" through database on "<new_login_id_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And update the data by query "<password_query>" on DIGITAL_CHANNEL_SEC
	Then I have given "<Login_id>" on "Login_UserId"
	And I have given "<activation_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	And verify through "<password_policy>" on "Signup_PassPolicy"
	And verify through "<password_policy1>" on "Signup_PassPolicy1"
	And verify through "<password_policy2>" on "Signup_PassPolicy2"
	And verify through "<password_policy3>" on "Signup_PassPolicy3"
	Then I have given "<new_password>" on "Signup_LoginPassword"
	Then I have given "<new_password>" on "Signup_LoginPassword"
	And I have given "<new_password>" on "Signup_ReLoginPassword"
	And I have given "<activation_password>" on "Signup_TransactionPassword"
	And I have given "<activation_password>" on "Signup_ReTransactionPassword"
	And I am performing on "Signup_SubmitBtn"
	And verify through "<success_message>" on "Signup_PaswwordText"
	And I am performing on "Signup_PaswwordOkBtn"
	And verify the result from "<password_change_req_query>" on Schema "DIGITAL_CHANNEL_SEC"
	Then I have given "<Login_id>" on "Login_UserId"
	And I have given "<new_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	And I wait 2000
	And I am clicking on "Signup_FeedbackOptionHBL"
	And I have given "<feedback_option>" on "Signup_FeedbackText"
	And I am performing on "Signup_FeedbackSubmit"
	And verify through "Thank you for letting us know." on "Signup_FeedbackMessage"
	And I am performing on "Signup_FeedbackOkBtn"
	Then verify through "Welcome" on "Login_Success_Text"

	@source:Data/ChangeLoginID.xlsx
	Examples:
	|Case|cnic|debit_card|card_pin|Credit_card|email|new_login_id|customer_type_query|success_msg|password_query|activation_password|new_password|success_message_password|password_change_req_query|feedback_option|new_login_id_query|password_policy|password_policy1|password_policy2|password_policy3|

@ForgetLoginID
Scenario Outline: 2 As a user i want to verify forget Login ID
	Given the test case title is "<Case>"
	And I set value in context from data "<cnic>" as "customer_cnic"
	And the user is arrive to Internet Banking home page 	
	And I am clicking on "Forget_btn"
	And I have given "<cnic>" on "Forget_CNIC"
	And I have given "<mobile_no>" on "Forget_MobileNo"
	When I am performing on "Forget_SubmitBtn"
	And verify through database on "<tran_message_qeury>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordResponseMsg"
	And verify through database on "<tran_id_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranID"
	And verify through database on "<tran_date_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranDate"
	#And verify through database on "<tran_type_query>" on Schema "DIGITAL_CHANNEL_SEC" on "Forget_PasswordTranType"
	And I am performing on "Forget_PasswordTranSubmitBtn"

	@source:Data/ForgetLoginID.xlsx
	Examples:
	|Case|cnic|mobile_no|tran_message_qeury|tran_id_query|tran_type_query|tran_date_query|