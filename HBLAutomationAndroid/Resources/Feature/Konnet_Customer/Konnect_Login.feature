Feature: Konnect_Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario Outline: 1When user try to device tagging for konnect
	Given the test case title is "<Case>"
	And update the data by query "<Status_query>" on Schema "TEST_SMS_BANKING"
	And the user is arrive to Mobile Banking home page
	#And I am switiching activity as package "com.hbl.bbcustomerapp" as activity "com.hbl.bbcustomerapp.ui.activities.HomeActivity"
	And I am clicking on "Login_permission_allow_btn"
	And I wait 3000
	And I am clicking on "Login_permission_allow_btn2"
	And I am clicking on "Login_Tagging_Next"
	When I have given "<Mobile_No_Value>" on "Login_Tagging_MobileNo"
	And I am clicking on "Login_Tagging_Next"
	And I wait 3000
	And update the data by query "<ip_tagging_query>" on Schema "TEST_SMS_BANKING"
	And I am resetting app
	@source:Data/Konnect_Tagging.xlsx
	Examples: 
	|Case|Status_query|ip_tagging_query|Mobile_No_Value|

	@mytag
Scenario Outline: 2When user try to login for konnect
	Given the test case title is "<Case>"
	#And update the data by query "<Status_query>" on Schema "TEST_SMS_BANKING"
	And the user is arrive to Mobile Banking home page
	And I set value in context from data "<mobile_no_value>" as "KMobileNo"
	And I am clicking on "Login_permission_allow_btn"
	And I wait 3000
	And I am clicking on "Login_permission_allow_btn2"
	And I am clicking on "Login_Tagging_Next"
	#When I have given "<Mobile_No_Value>" on "Login_Tagging_MobileNo"
	#And I am clicking on "Login_Tagging_Next"
	#When I have given "<user_id_value>" on "Login_K_UserId"
	When I have given "<pin_value>" on "Login_K_Pin"
	And I am clicking on "Login_K_SignInBtn"
	And I wait 3000
	Then verify the message using element "Login_K_DisplayName" through database on "<name_verification_query>" on Schema "TEST_SMS_BANKING"
	#And I am switiching activity as package "com.hbl.bbcustomerapp" as activity "com.hbl.bbcustomerapp.ui.activities.HomeActivity"
	#And I am switiching activity as package "com.hbl.bbcustomerapp" as activity "com.hbl.bbcustomerapp.ui.activities.SplashActivity"
	#And update the data by query "<ip_tagging_query>" on Schema "TEST_SMS_BANKING"
	#And I am resetting app
	@source:Data/Konnect_Login.xlsx
	Examples: 
	|Case|mobile_no_value|pin_value|name_verification_query|