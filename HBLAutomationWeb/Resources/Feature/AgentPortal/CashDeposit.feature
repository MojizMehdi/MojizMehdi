Feature: CashDeposit
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Cash_Deposit
Scenario Outline: As a user i want to Verify Cash Deposit from Agent Portal
	Given the test case title is "<Case>"
	And the test case expected result is "<Expected_Result>"
	And I save Balance and Commission of Agent 
	And I am clicking on "AgentPortal_CashWithdrawl_TabOpt"
	And I am clicking on "CashWithdrawl_OptClick"
	When I have given "<mobile_no>" on "CashWithdrawl_Mobile"


	@source:Data/CashDeposit.xlsx
	Examples: 
	|Case|Expected_Result|mobile_no|amount|pin|