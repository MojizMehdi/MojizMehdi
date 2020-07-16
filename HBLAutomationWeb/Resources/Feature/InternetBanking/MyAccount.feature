Feature: MyAccount
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MyAccount
Scenario Outline: As a user I want to verify Account Linking & De-Linking from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I count Number of Account
	And I save Account Balances
	And I am clicking on "MyAccount_Icon"
	#And I am clicking on "MyAccount_AccLinkOption"
	And I select "<De_Linking_Account>" for Account linking or de-linking "<operation_type>" with success message as "<Success_message>"
	When I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I select "<Transaction_Category>" on "Services_CategoryFilter"
	And I scroll to element "Services_Clear_Btn"
	And I am performing on "Services_Search_Btn"
	And I am clicking on "Services_Last_Transaction"
	Then verify through database on "Successful" on Schema "<db_val>" on "Pay_MultiBill_SRV_TranStatus"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "Pay_MultiBill_SRV_TranType"
	And I am clicking on "MyAccount_Services_CloseBtn"

	@source:Data/MyAccount.xlsx
	Examples: 
	|Case|Linking_Account|De_Linking_Account|operation_type|Success_message|db_val|tran_type_query|



@MyAccount
Scenario Outline: As a user I want to verify Limit Management from My Account
	Given the test case title is "<Case>" 
	And the user is arrive to Internet Banking home page
	And I am clicking on "MyAccount_Icon"
	And I am clicking on "MyAccount_LimitMngOption"

	



	@source:Data/MyAccount.xlsx
	Examples: 
	|Case|Linking_Account|De_Linking_Account|operation_type|Success_message|db_val|tran_type_query|