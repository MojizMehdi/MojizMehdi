Feature: CashWithdrawl
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Cash_Withdrawl
Scenario Outline: As a user i want to Verify Cash Withdrawl from Agent Portal
	Given the test case title is "<Case>"
	And the test case expected result is "<Expected_Result>"
	And I save Balance and Commission of Agent 
	And I am clicking on "CashWithdrawl_TabOpt"
	And I am clicking on "CashWithdrawl_OptClick"
	When I have given "<mobile_no>" on "CashWithdrawl_Mobile"
	And I have given "<amount>" on "CashWithdrawl_Amount"
	And I am performing on "CashWithdrawl_NextBtn"

	@source:Data/CashWithdrawl.xlsx
	Examples: 
	|Case|Expected_Result|mobile_no|amount|pin|