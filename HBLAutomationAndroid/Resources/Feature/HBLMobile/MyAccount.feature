Feature: MyAccount
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Limit_Management
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

