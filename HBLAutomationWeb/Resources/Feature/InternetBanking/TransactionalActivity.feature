Feature: TransactionalActivity
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@TransActivity
Scenario Outline: 2 As a user i want to Verify my Transaction activities
Given the test case title is "<Case>"
And I set all excel values "<Transaction_Category>" "<No_of_Transaction>" "<Tran_Type>" "<from_day>" "<from_month>" "<from_year>" "<to_day>" "<to_month>" "<to_year>" "<Min_Amount>" "<Max_Amount>" "<Acc_no_or_mobile>" "<bill_company>" "<payee_nick>" "<to_bank>"  in context class
#And I generate query based on given data
And I am clicking on "Services_Link"
And I am clicking on "Services_Transaction_Activity"
And I wait 5000
When I select "<Transaction_Category>" on "Services_CategoryFilter"
And I select "<No_of_Transaction>" on "Services_NoOfTransaction"
And I select "<Tran_Type>" on "Services_Transaction_Type"
When I am clicking on "Services_Date_From"
And I select date "<from_day>" on month "<from_month>" on year "<from_year>"
When I am clicking on "Services_Date_To"
And I select date "<to_day>" on month "<to_month>" on year "<to_year>"
And I have given "<Min_Amount>" on "Services_MIN_TRAN_AMOUNT"
And I have given "<Max_Amount>" on "Services_MAX_TRAN_AMOUNT"
And I have given "<Acc_no_or_mobile>" on "Services_FROM_ACCOUNT"
When I scroll to element "Services_Clear_Btn"
And I select on dropdown search "Services_BILL_COMPANY" to select "<bill_company>" on "Services_BILL_COMPANY_List"
And I select on dropdown search "Services_BENEFICIARY_NAME" to select "<payee_nick>" on "Services_BENEFICIARY_NAME_List"
And I select on dropdown search "Services_BENEFICIARY_BANK" to select "<to_bank>" on "Services_BENEFICIARY_BANK_List"
And I am performing on "Services_Search_Btn"
Then I generate query based on given data

@source:Data/TransactionActivity.xlsx
Examples:
|Case|Transaction_Category|No_of_Transaction|Tran_Type|from_day|from_month|from_year|to_day|to_month|to_year|Min_Amount|Max_Amount|Acc_no_or_mobile|bill_company|payee_nick|to_bank|
