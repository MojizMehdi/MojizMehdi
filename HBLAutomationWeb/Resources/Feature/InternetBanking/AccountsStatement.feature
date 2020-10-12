Feature: Accounts
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@AccountsStatement
	Scenario Outline: As a user i want to Verify Account statement generation
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I am clicking on "Accounts_Link"
	And I am clicking on "Accounts_FromDate"
	When I select date "1" on month "Jan" on year "2020"
	And I am clicking on "Accounts_ToDate"
	And I select date "11" on month "Mar" on year "2020"
	And I want value from textbox "Accounts_NoOfDays" on database "<db_value>" as "<query>"
	And I have given "100" on "Accounts_NoOfDays"
	And I am performing on "Accounts_Generate_Button"
	And I sleep 5000
	Then I am performing on "Accounts_CSV_Button"
	#And I am performing on "Account_PDF_Button"
	@source:Data/AccountsStatement.xlsx
	Examples: 
	|Case|db_value|query|