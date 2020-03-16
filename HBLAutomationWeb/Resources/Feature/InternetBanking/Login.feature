Feature: Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Bene_Addition @BillPayment
Scenario Outline: 1 As a user i want to Verify login for HBL Web
	Given the test case title is "<Case>"
	And the user is arrive to Internet Banking home page 
	And I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	When I am performing on "Login_SignIn_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	@source:Data/IBLogin.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|


@Bene_Addition
	Scenario Outline: As a user i want to Verify Beneficiary Addition for HBL Web
	Given the test case title is "<Case>"
	#And update the data by query "<StatusQuery>"
	And the user is arrive to Internet Banking home page
	And I am clicking on "BeneficiaryManagement_Link"
	And I am clicking on "BeneficiaryManagement_AddNewBtn"
	And I select "<BeneficiaryManagement_Bank_Value>" on "BeneficiaryManagement_Bank"
	And select value from database by "<account_query>"
	And I have given "<BeneficiaryManagement_AccountNo_Value>" on "BeneficiaryManagement_AccountNo"
	And I have given "<BeneficiaryManagement_BeneNick_Value>" on "BeneficiaryManagement_BeneNick"
	And I have given "<BeneficiaryManagement_PayeeEmail_Value>" on "BeneficiaryManagement_PayeeEmail"
	And I have given "<BeneficiaryManagement_PayeeMobileNumber_Value>" on "BeneficiaryManagement_PayeeMobileNumber"
	When I am performing on "BeneficiaryManagement_Validate_Button"
	And I have given "<OTP_Value>" on "Login_OTP_field"
	And I am performing on "Login_OTP_Verify_Button"
	@source:Data/BeneficiaryAddition.xlsx
	Examples: 
	|Case|StatusQuery|BeneficiaryManagement_Bank_Value|account_query|BeneficiaryManagement_AccountNo_Value|BeneficiaryManagement_BeneNick_Value|BeneficiaryManagement_PayeeEmail_Value|BeneficiaryManagement_PayeeMobileNumber_Value|OTP_Value|
