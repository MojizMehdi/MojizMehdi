Feature: Login_AP
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Login_AP
Scenario Outline: As a user i want to Verify login for Agent Portal Web
	Given the test case title is "<Case>"
	And I set value in context from data "<Login_Id>" as "AP_username"
	And I set value in context from database "<mobile_no_query>" as "mobile_number" on Schema "<db_val_AP>"
	And the user is arrive to Internet Banking home page 
	And I have given "<Login_Id>" on "Login_APUserId"
	And I am performing on "Login_AP_ID_Btn"
	And I have given "<Login_Password>" on "Login_APPassword"
	When I am performing on "Login_APLogin_Btn"
	And I wait 3000 
	And I have given "" on "Login_APOTP_field"
	And verify through "<otp_text>" on "Login_APOTP_Txt"
	And I am performing on "Login_APOTP_Btn"
	Then verify through database on "<agent_name_query>" on Schema "QAT_BB_SYSTEM" on "Login_APName_Success_Text"
	And verify through database on "<balance_inquiry_query>" on Schema "QAT_BB_SYSTEM" on "Pay_Balance_AP"
	

	@source:Data/AP_Login.xlsx
	Examples: 
	|Case|Expected_Result|Login_Id|Login_Password|db_val_AP|mobile_no_query|otp_text|agent_name_query|balance_inquiry_query|