Feature: Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Login
Scenario Outline: 1 As a user i want to Verify login for HBL Web
	Given the test case title is "<Case>"
	#And I set value in context from data "<Login_UserId_Value>" as "username"
	And the user is arrive to Internet Banking home page 
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I wait 5000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	Then verify through "Welcome" on "Login_Success_Text"
	@source:Data/IBLogin.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|
