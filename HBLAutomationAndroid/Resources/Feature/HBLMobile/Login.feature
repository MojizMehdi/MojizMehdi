Feature: Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario Outline: When user try to login mobile banking
	Given the test case title is "<Case>"
	And the user is arrive to Mobile Banking home page 
	When I have given "<Login_UserId_Value>" on "Login_UserId"
	And I have given "<Login_Password_Value>" on "Login_Password"
	And I am performing on "Login_SignIn_Button"
	And I wait 40000 
	@source:Data/HBLMobileLogin.xlsx
	Examples: 
	|Case|Login_UserId_Value|Login_Password_Value|OTP_Value|



	@mytag
Scenario Outline: When user try to send money mobile
	Given the test case title is "<Case>"
	And update the data by query "<status_query>" on DIGITAL_CHANNEL_SEC
	And the user is arrive to Mobile Banking home page 
	When I am clicking on "SendMoney_Link"
	#And I am clicking on "SendMoney_SkipBtn"
	And I am clicking on "SendMoney_AddNewBtn"
	And I select "<From_Account_Value>" on "SendMoney_FromAccount"
	And I select "<Bank_Value>" on "SendMoney_Bank"
	And I have given "<Account_Number_Value>" on "SendMoney_ToAccount"
	And I scroll down
	And I have given "<Amount_Value>" on "SendMoney_Amount"
	When I select "<PurposeOfPayment_Value>" on "SendMoney_PurposeOfPayment"
	And I scroll down
	And I have given "<Bene_Nick>" on "SendMoney_BeneNick"
	And I have given "<Bene_Mobile_No>" on "SendMoney_BeneMobileNo"
	And I have given "<Bene_Email>" on "SendMoney_BeneEmail"
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I scroll down
	And I wait 2000
	And I have given "12345678" on "SendMoney_OTP"
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	And I have given "Tran_Pass_Value" on "SendMoney_TranPass"
	And I am performing on "SendMoney_NextBtn"
	And I wait 3000
	@source:Data/SendMoney.xlsx
	Examples: 
	|Case|status_query|From_Account_Value|Bank_Value|Account_Number_Value|Amount_Value|PurposeOfPayment_Value|Bene_Nick|Bene_Mobile_No|Bene_Email|OTP_Value|Tran_Pass_Value|Success_Message|tran_type_query|tran_amount_query|from_account_query|to_account_query|to_bank_query|bene_name_query|purpose_query|db_val|