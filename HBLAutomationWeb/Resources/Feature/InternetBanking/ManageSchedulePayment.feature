Feature: ManageSchedulePayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MngSchedule
Scenario Outline: As a user I want to verify Schedule Management from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I set value in context from database "<schedule_count_query>" as "user_schedule_count" on Schema "<db_val>"
	When I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_MngSch_Icon"
	Then I am verifying schedule payments from My Account


	@source:Data/ManageScheduleVerify.xlsx
	Examples: 
	|Case|db_val|schedule_count_query|