Feature: MyAccount
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MyAccount @Limit_Management
Scenario Outline: When user try to login mobile banking for MyAccount
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page 
	And the test case expected result is "<Expected_Result>"
	And I wait 2000
	And I am clicking on "Login_permission_allow_btn"
	And I wait 1000
	And I am clicking on "Login_permission_allow_btn2"
	And I am clicking on "SendMoney_SkipBtn"
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And update the data by query "<Status_query>" on DIGITAL_CHANNEL_SEC
	When I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	And I wait 2000
	And I am performing on "Login_SignIn_Button"
	And I wait 30000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am clicking on "Login_OTP_Verify_Button"
	And I wait 5000
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "BillPayment_Rating"
	And I am clicking on "BillPayment_RatingOkBtn"
	Then verify through "Welcome, " on "Login_Success_Text"
	#And I am switiching activity as package "com.hbl.android.hblmobilebanking" as activity "com.hbl.android.hblmobilebanking.account.HomeScreen"
	@source:Data/My_Account_Login.xlsx
	Examples: 
	|Case|Status_query|Login_UserId_Value|Login_Password_Value|OTP_Value|Expected_Result|
	



@MyAccount @Limit_Management
Scenario Outline: As a user i want to Verify Limits Mobile
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page
	And I am clicking on "Dashboard"
	When I am clicking on "Dashboard_Sidebar"
	And I am clicking on "Dashboard_Sidebar_MyAccount"
	And I am clicking on "MyAccount_Limit_Management_Icon"
	And I set list of elements from scroll view on "MyAccount_Limit_Type_count" as "3"
	Then I am performing Limit verification operation
	And I am performing limit reduction operation on "MyAccount_Limit_Edit_Icon" of slider "MyAccount_Limit_Slider" of "<limit_type>" with new limit as "<new_limit>"
	
	@source:Data/LimitManagement.xlsx
	Examples: 
	|Case|limit_type|new_limit|

