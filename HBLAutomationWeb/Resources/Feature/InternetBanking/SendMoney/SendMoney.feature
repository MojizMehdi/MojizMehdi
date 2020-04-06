Feature: SendMoney
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@SendMoney
Scenario Outline: 1 As a user i want to Verify login for HBL Web Send Money
Given the test case title is "<Case>"
And the user is arrive to Internet Banking home page 
And I have given "<Login_UserId_Value>" on "Login_UserId"
And I have given "<Login_Password_Value>" on "Login_Password"
When I am performing on "Login_SignIn_Button"
And I wait 3000
And I have given "<OTP_Value>" on "Login_OTP_field"
And I am performing on "Login_OTP_Verify_Button"
@source:Data/IBLogin.xlsx
Examples: 
|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|

@SendMoney
Scenario Outline: 2 As a user i want to Verify Send Money by adding Beneficiary
Given the test case title is "<Case>"
And the user is arrive to Internet Banking home page 
When I am clicking on "SendMoney_Link"
And I am clicking on "SendMoney_AddNewBtn"
And I select "<From_Account_Value>" on "SendMoney_FromAccount"
And I select "<Bank_Value>" on "SendMoney_Bank"
And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
And I have given "<Amount_Value>" on "SendMoney_Amount"
And I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
And I am clicking on "SendMoney_NextBtn"
And I have given "12345" on "Login_OTP_field"
And I am performing on "Login_OTP_Verify_Button"
And I have given "pakistan3" on "Pay_Transaction_PayBill_TransactionPassword"
And I am clicking on "SendMoney_SendBtn"
@source:Data/SendMoney.xlsx
Examples: 
|Case|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|
