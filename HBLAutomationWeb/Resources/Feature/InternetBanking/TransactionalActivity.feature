Feature: TransactionalActivity
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


@Login
Scenario Outline: 1 As a user i want to Verify login for HBL Web Transaction activities
	Given the test case title is "<Case>"
	And I set value in context from data "<Login_UserId_Value>" as "username"
	And the user is arrive to Internet Banking home page 
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I wait 5000
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	Then verify through "Welcome" on "Login_Success_Text"
	@source:Data/IBLogin.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|


@TransActivity
Scenario Outline: As a user i want to Verify my Transaction activities
Given the test case title is "<Case>"
And the user is arrive to Internet Banking home page
And I set all excel values "<Transaction_Category>" "<No_of_Transaction>" "<Tran_Type>" "<from_day>" "<from_month>" "<from_year>" "<to_day>" "<to_month>" "<to_year>" "<Min_Amount>" "<Max_Amount>" "<Acc_no_or_mobile>" "<bill_company>" "<payee_nick>" "<to_bank>"  in context class
#And I generate query based on given data
And I am clicking on "Services_Link"
And I am clicking on "Services_Transaction_Activity"
And I wait 5000
When I select "<Transaction_Category>" on "Services_CategoryFilter"
And I select "<No_of_Transaction>" on "Services_NoOfTransaction"
And I select "<Tran_Type>" on "Services_Transaction_Type"
And I am clicking on "Services_Date_From"
#And I select day "<from_day>" and calculate date 
And I select date "<from_day>" on month "<from_month>" on year "<from_year>"
And I am clicking on "Services_Date_To"
And I select date "<to_day>" on month "<to_month>" on year "<to_year>"
And I have given "<Min_Amount>" on "Services_MIN_TRAN_AMOUNT"
And I have given "<Max_Amount>" on "Services_MAX_TRAN_AMOUNT"
And I have given "<Acc_no_or_mobile>" on "Services_FROM_ACCOUNT"
And I scroll to element "Services_Clear_Btn"
And I select on dropdown search "Services_BILL_COMPANY" to select "<bill_company>" on "Services_BILL_COMPANY_List"
And I select on dropdown search "Services_BENEFICIARY_NAME" to select "<payee_nick>" on "Services_BENEFICIARY_NAME_List"
And I select on dropdown search "Services_BENEFICIARY_BANK" to select "<to_bank>" on "Services_BENEFICIARY_BANK_List"
And I am performing on "Services_Search_Btn"
Then I generate query based on given data

@source:Data/TransactionActivity.xlsx
Examples:
|Case|Transaction_Category|No_of_Transaction|Tran_Type|from_day|from_month|from_year|to_day|to_month|to_year|Min_Amount|Max_Amount|Acc_no_or_mobile|bill_company|payee_nick|to_bank|
