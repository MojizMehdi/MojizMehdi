Feature: Investments
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


@Investment @Mutual_Fund @Term_Deposit
Scenario Outline: 1 As a user i want to Verify login for HBL Web Investments
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
	@source:Data/Investments_Login.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|



@Investsments @Term_Deposit
Scenario Outline: When user try to term deposit thorugh Investments
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I am clicking on "Login_Dashboard"
	And I set value in context from data "1" as "term_deposit_flag"
	And I count Number of Account
	When I save Account Balances
	And I am clicking on "Investment_Icon"
	And I am clicking on "Investment_TermDeposit_Icon"
	And I set value in context from data "<Deposit_Years_Value>" as "TermDepositYears"
	And I wait 5000
	And I select "<term_deposit_type>" on "Investment_ETDRTYpe_List"
	And I am clicking on "Investment_TermDep_Tenure"
	And I select "<account_no>" on "Investment_FromAcc_List"
	And I select "<profit_account>" on "Investment_ProfitAcc_List"
	And I have given "<Amount_Value>" on "Investment_Amount"
	And I am performing on "Investment_NextBtn"
	And I scroll to element "Investment_TermDep_ScrollText"
	And I am performing on "Investment_TermDep_AcceptBtn"
	And I have given "<Tran_Pass_Value>" on "Signup_TransactionPassword"
	And I scroll to element "Investment_TermDep_ReqBtn"
	And I am performing on "Investment_TermDep_ReqBtn"
	Then verify through "<Success_Message>" on "Investment_TranSuccessMessage"
	#And verify through database on "<tran_type_query>" on Schema "<db_val>" on "Investment_TranType"
	#And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Investment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Investment_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "Investment_TranToAcc"
	And verify through database on "<date_query>" on Schema "<db_val>" on "Investment_TranDate"
	And I save Transaction Info
	And I am performing on "Investment_TranCloseBtn"
	And I am clicking on "Pay_Transaction_PayBill_Rating"
	And I am clicking on "Investment_TermDep_RatingOkBtn" 
	And I am clicking on "Login_Dashboard"
	And I verify Account Balance
	And I am clicking on "Investment_TermDepost_DetailBtn"
	And I set value in context from database "<reference_no_query>" as "term_dep_ref_no" on Schema "<db_val>"
	And verify through "" on "Investment_TermDetTotalAmount"
	And I scroll to element "Investment_TermRefNo"
	And verify through database on "<reference_no_query>" on Schema "<db_val>" on "Investment_TermRefNo"
	And verify through "<Amount_Value>" on "Investment_TermDetAmount"
	And verify through "<Deposit_Years_Value>" on "Investment_TermDetPeriod"
	And verify through "OfferedRateContextVal" on "Investment_TermDetRate"
	And I am clicking on "Login_Dashboard"
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "Investment_TranType"
	#And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Investment_TranAmount"
	And verify through database on "<from_account_query>" on Schema "<db_val>" on "Investment_TranFromAcc"
	And verify through database on "<to_account_query>" on Schema "<db_val>" on "Investment_TranToAcc"
	And verify through database on "<date_query>" on Schema "<db_val>" on "Investment_TranDate"
	And I am performing on "Investment_TranActivityCloseBtn"

@source:Data/ETDR.xlsx
	Examples: 
	|Case|status_query|status_query2|Category_Value|Deposit_Years_Value|account_no|profit_account|Amount_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|db_val|term_deposit_type|date_query|reference_no_query|



@Investsments @Mutual_Fund
Scenario Outline: When user try to verify Mutual Fund
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page
	And I set value in context from data "<invest_option>" as "invest_fund_name"
    And I set value in context from database "<disclaimer_query>" as "fund_disclaimer_popup" on Schema "QAT_AMC"
	And I am clicking on "Login_Dashboard"
	And I count Number of Account
	When I save Account Balances
	And I am clicking on "Investment_Icon"
	And I am clicking on "Investment_MutualFund_Icon"
	And I set value in context from database "<cust_profile_id_query>" as "cust_profile_id" on Schema "QAT_AMC"
	And I verify user Mutual Fund status on schema "<db_val3>"
	And I am clicking on "Investment_MutualFund_InvestTab"
	Then I set list of elements from scroll view on "Investment_MutualFund_FundList"
	And verify the list using "<Fund_Names_query>" on Schema "QAT_AMC"
	#And I am clicking on "Investment_MutualFund_Icon"
	And I scroll to element "Investment_MutualFund_InvestBtn"
	And I am performing on "Investment_MutualFund_InvestBtn"
	And verify through "<disclaimer_message>" on "Investment_MutualFund_DisPopup"
	And I am performing on "Investment_MutualFund_PopupBtn"
	And I select "<from_acc>" on "Investment_MutualFund_FromAcc"
	And verify through "<invest_option>" on "Investment_MutualFund_FUndName"
	And verify through database on "<gl_account_query>" on Schema "<db_val3>" on "Investment_MutualFund_FundAccNo"
	And I have given "<amount>" on "Investment_MutualFund_Amount"
	And I check values of combobox using database from "<folio_no_query>" on schema <db_val3> on combobox "Investment_MutualFund_FolioNumber" of list "Investment_MutualFund_FolioNumberList"
	And I select "<folio_no>" on "Investment_MutualFund_FolioNumber"
	And verify through database on "<tran_timing_query>" on Schema "<db_val3>" on "Investment_MutualFund_TranTiming"
	And I am performing on "Investment_MutualFund_NextBtn"
	And I scroll to element "Investment_MutualFund_ScrollTxt"
	And I am performing on "Investment_MutualFund_AgreeBtn"
	And I scroll to element "Investment_MutualFund_TranPass"
	And I have given "<tran_pass>" on "Investment_MutualFund_TranPass"
	And I am performing on "Investment_MutualFund_InvestBtnFinal"
	And I wait 6000
	And verify through "<success_msg>" on "Investment_MutualFund_TranSuccessMessage"
	And I set value in context from database "<GUID_query>" as "GUID" on Schema "<db_val>"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "Investment_MutualFund_TranDate"
	#And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Investment_MutualFund_TranAmount"
	And verify through database on "<from_acc_query>" on Schema "<db_val>" on "Investment_MutualFund_TranFromAcc"
	And verify through database on "<to_acc_query>" on Schema "<db_val>" on "Investment_MutualFund_TranToAcc"
	And verify through database on "<fund_name_query>" on Schema "<db_val>" on "Investment_MutualFund_TranFundName"
    And verify the message "<amount>" through database on "<tran_amount_verify_query>" on Schema "<db_val3>"
	And I am performing on "Investment_MutualFund_TranCloseBtn"
	And I verify Account Balance
	And I am clicking on "Services_Link"
	And I am clicking on "Services_Transaction_Activity"
	And I am clicking on "Services_Last_Transaction"
	And verify through "Successful" on "MyAccount_Forgot_TranSuccessMessage"
	And verify through database on "<tran_type_query>" on Schema "<db_val>" on "Investment_TranType"
	And verify through database on "<tran_date_query>" on Schema "<db_val>" on "Investment_TranDate"
	#And verify through database on "<tran_amount_query>" on Schema "<db_val>" on "Investment_TranAmount"
	And verify through database on "<from_acc_query>" on Schema "<db_val>" on "Investment_TranToAcc"
	And verify through database on "<to_acc_query>" on Schema "<db_val>" on "Investment_TranToAcc"
	And verify through database on "<fund_name_query>" on Schema "<db_val>" on "Investment_TranFundName"
	And I am performing on "Investment_TranActivityCloseBtn"

@source:Data/MutualFund.xlsx
	Examples: 
	|Case|cust_profile_id_query|fund_name|from_acc|amount|db_val3|invest_option|disclaimer_message|gl_account_query|tran_timing_query|tran_pass|success_msg|disclaimer_query|tran_type_query|tran_date_query|tran_amount_query|from_acc_query|to_acc_query|fund_name_query|GUID_query|tran_amount_verify_query|folio_no_query|folio_no|Fund_Names_query|db_val|

