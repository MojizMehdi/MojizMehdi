Feature: Registration
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


@Registration
Scenario Outline: 2 As a user i want to Signup using Debit Card Mobile
	Given the test case title is "<Case>"	
	And the user is arrive to Mobile Banking home page 
	And I set value in context from data "<CNIC_D>" as "username"
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
	And I am clicking on "Registration_NextBtn"
	And I wait 5000
	And I am clicking on "Registration_AccountNo_Marking"
	And I save Account Numbers
	And I am clicking on "Registration_AccMark_NextBtn"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Registration_NextBtn"
	And I wait 5000
	And verify the message "<account_tag1>" through database on "<account_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<account_tag1>" through database on "<account_tag_query>" on Schema "DIGITAL_CHANNEL_SEC"
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
	And verify the data using "<last_login_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the message "<customer_type>" through database on "<customer_type_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<IVR_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<IVR_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<PARAM_CHANNEL_ID_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<ENABLE_PSD_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the data using "<ENABLE_PSD_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	@source:Data/DebitRegistration.xlsx
	Examples:
	|Case|CNIC_D|Debit_card_no|pin|Login_id|password_policy_query|login_pass|tran_pass|account_tag1|account_query|account_tag_query|success_message|login_id_query|created_on_query|updated_on_query|Last_login_query|transaction_password_query|customer_type|customer_type_query|IVR_require_query|IVR_check_query|PARAM_CHANNEL_ID_query|


