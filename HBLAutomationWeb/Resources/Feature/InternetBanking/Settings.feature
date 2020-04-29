Feature: Settings
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Settings
	Scenario Outline: As a user i want to verify change user login password
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	#And I sleep "10000"
	And I am clicking on "Settings_icon"
	And I have given "<Login_Password_Value>" on "Settings_login_old_pass"
	And I have given "<Login_New_Password>" on "Settings_login_new_pass"
	And I have given "<Login_New_Password>" on "Settings_login_confirm_pass"
	And I am performing on "Settings_change_login_button"
	Then verify through "{message}" on "{Keyword}"
	And update the data by query "<Update_Password_query>" on DIGITAL_CHANNEL_SEC
	 
	@source:Data/Settings.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|Login_New_Password|Update_Password_query|