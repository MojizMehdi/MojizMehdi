Feature: MultiBillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@MultiBillPayment
Scenario Outline: As a user I want to verify Multiple Bill Payment scenario
	Given the test case title is "<Case>"
	And I set value in context from data "<Pay_BillPayment_ConsumerNo_Value>" as "ConsumerNo"
	And update the data by query "<status_query>" on QAT_BPS
	And update the data by query "<status_query2>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	And I save Account Balances
	When I am clicking on "Pay_Link"
	And I am clicking on "Pay_MultiBillIcon"



	@source:Data/MultiBillPayment.xlsx
	Examples: 
	|Case|status_query|status_query2|
