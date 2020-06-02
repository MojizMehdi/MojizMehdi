﻿Feature: Registration
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Registration
Scenario Outline: 2 As a user i want to Signup using Debit Card
	Given the test case title is "<Case>"	
	And I set value in context from data "<scroll_text>" as "scroll_text"
	And I set value in context from data "<CNIC_D>" as "customer_cnic"
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Signup_registerBtn"
	And I am performing "Ok" alert operation on cross icon on "Signup_CrossIcon"
	And I am clicking on "Signup_registerBtn" 
	# # If you select ok option on above step then repeat Signup_registerBtn clicking step"
	And I am clicking on "Signup_DebitCardNav"
	And verify through "<debit_req_text1>" on "Signup_DebitReqText1"
	And verify through "<debit_req_text2>" on "Signup_DebitReqText2"
	And I am clicking on "Signup_Debit_NextBtn"
	And I am performing on "Signup_LngTextBtn"
	And I verify if text exist on webpage of "Signup_UrduText"
	And I am performing on "Signup_LngTextBtn"
	When I scroll to element "Signup_Scroll"
	And I am performing on "Signup_AcceptBtn"
	And verify DVL setting through database on "<dvl_query>" on Schema "DIGITAL_CHANNEL_SEC" with date of birth "<dob>" on keyword "Signup_DOB"
	And I have given "<CNIC_D>" on "Signup_CNIC"
	And I have given "<Debit_card_no>" on "Signup_CardNo"
	And I have given "<pin>" on "Signup_CardPin"
	And I have given "<Login_id>" on "Signup_LoginId"
	And I am performing on "Signup_ContinueBtn"
	And verify through "<account_tag1>" on "Signup_TagAccountNo"
	And I am clicking on "Signup_AccountToggle"
	And I am performing on "Signup_LastRegisterBtn"
	And verify the message "<account_tag1>" through database on "<account_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify through "<Login_id>" on "Signup_LoginIdTextVerf"
	And verify through "<success_message1>" on "Signup_SuccessMessage1"
	And verify through "<success_message2>" on "Signup_SuccessMessage2"
	And verify through "<success_message3>" on "Signup_SuccessMessage3"
	And I am performing on "Signup_SuccessNextBtn"	 
	And verify the result from "<IVR_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<IVR_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And update the data by query "<password_query>" on DIGITAL_CHANNEL_SEC
	Then I have given "<Login_id>" on "Login_UserId"
	And I have given "<activation_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	And verify through "<password_policy>" on "Signup_PassPolicy"
	And verify through "<password_policy1>" on "Signup_PassPolicy1"
	And verify through "<password_policy2>" on "Signup_PassPolicy2"
	And verify through "<password_policy3>" on "Signup_PassPolicy3"
	Then I have given "<new_password>" on "Signup_LoginPassword"
	And I have given "<new_password>" on "Signup_ReLoginPassword"
	And I have given "<activation_password>" on "Signup_TransactionPassword"
	And I have given "<activation_password>" on "Signup_ReTransactionPassword"
	And I am performing on "Signup_SubmitBtn"
	And verify through "<success_message>" on "Signup_PaswwordText"
	And I am performing on "Signup_PaswwordOkBtn"
	And verify the message "<Login_id>" through database on "<login_id_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<password_change_req_query>" on Schema "DIGITAL_CHANNEL_SEC"
	Then I have given "<Login_id>" on "Login_UserId"
	And I have given "<new_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	And I wait 2000
	And I am clicking on keyword "Signup_FeedbackOptionHBL" with value "<feedback_type>"
	And I have given "<feedback_option>" on "Signup_FeedbackText"
	And I am performing on "Signup_FeedbackSubmit"
	And verify through "Thank you for letting us know." on "Signup_FeedbackMessage"
	And I am performing on "Signup_FeedbackOkBtn"
	Then verify through "Welcome" on "Login_Success_Text"

	@source:Data/DebitRegistration.xlsx
	Examples:
	|Case|CNIC_D|Debit_card_no|pin|dob|Login_id|scroll_text|activation_password|password_query|account_tag1|new_password|success_message|account_query|feedback_type|feedback_option|password_change_req_query|transaction_password_query|success_message1|success_message2|success_message3|debit_req_text1|debit_req_text2|login_id_query|password_policy|password_policy1|password_policy2|password_policy3|IVR_require_query|IVR_check_query|dvl_query|


@Registration
Scenario Outline: 2 As a user i want to Signup using Credit Card
	Given the test case title is "<Case>"
	And I set value in context from data "<scroll_text>" as "scroll_text"
	And the user is arrive to Internet Banking home page 
	And I am clicking on "Signup_registerBtn"
	And I am clicking on "Signup_CreditCardNav"
	And verify through "<credit_req_text1>" on "Signup_CreditReqText1"
	And verify through "<credit_req_text2>" on "Signup_CreditReqText2"
	And verify through "<credit_req_text3>" on "Signup_CreditReqText3"
	And I am clicking on "Signup_Credit_NextBtn"
	When I scroll to element "Signup_Scroll"
	And I am performing on "Signup_AcceptBtn"
	And verify DVL setting through database on "<dvl_query>" on Schema "DIGITAL_CHANNEL_SEC" with date of birth "<dob>" on keyword "Signup_DOB"
	And I have given "<CNIC_C>" on "Signup_CNIC"
	And I have given "<credit_card_no>" on "Signup_CardNo"
	And I have given "<email>" on "Signup_Email"
	And I have given "<Login_id>" on "Signup_LoginId"
	And I am performing on "Signup_ContinueBtn"
	And verify through "<Login_id>" on "Signup_LoginIdTextVerf"
	And verify through "<success_message1>" on "Signup_SuccessMessage1"
	And verify through "<success_message2>" on "Signup_SuccessMessage2"
	And verify through "<success_message3>" on "Signup_SuccessMessage3"
	And I am performing on "Signup_SuccessNextBtn"	 
	And verify the result from "<IVR_require_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<IVR_check_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And update the data by query "<password_query>" on DIGITAL_CHANNEL_SEC
	Then I have given "<Login_id>" on "Login_UserId"
	And I have given "<activation_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	And verify through "<password_policy>" on "Signup_PassPolicy"
	And verify through "<password_policy1>" on "Signup_PassPolicy1"
	And verify through "<password_policy2>" on "Signup_PassPolicy2"
	And verify through "<password_policy3>" on "Signup_PassPolicy3"
	Then I have given "<new_password>" on "Signup_LoginPassword"
	Then I have given "<new_password>" on "Signup_LoginPassword"
	And I have given "<new_password>" on "Signup_ReLoginPassword"
	And I have given "<activation_password>" on "Signup_TransactionPassword"
	And I have given "<activation_password>" on "Signup_ReTransactionPassword"
	And I am performing on "Signup_SubmitBtn"
	And verify through "<success_message>" on "Signup_PaswwordText"
	And I am performing on "Signup_PaswwordOkBtn"
	And verify the message "<Login_id>" through database on "<login_id_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<customer_type_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<transaction_password_query>" on Schema "DIGITAL_CHANNEL_SEC"
	And verify the result from "<password_change_req_query>" on Schema "DIGITAL_CHANNEL_SEC"
	Then I have given "<Login_id>" on "Login_UserId"
	And I have given "<new_password>" on "Login_Password"
	And  I am performing on "Login_SignIn_Button"
	And I wait 2000
	And I am clicking on keyword "Signup_FeedbackOptionHBL" with value "<feedback_type>"
	And I have given "<feedback_option>" on "Signup_FeedbackText"
	And I am performing on "Signup_FeedbackSubmit"
	And verify through "Thank you for letting us know." on "Signup_FeedbackMessage"
	And I am performing on "Signup_FeedbackOkBtn"
	Then verify through "Welcome" on "Login_Success_Text"

	@source:Data/CreditRegistration.xlsx
	Examples:
	|Case|CNIC_C|credit_card_no|email|dob|Login_id|scroll_text|activation_password|password_query|new_password|success_message|account_query|feedback_type|feedback_option|password_change_req_query|transaction_password_query|customer_type_query|success_message1|success_message2|success_message3|login_id_query|password_policy|password_policy1|password_policy2|password_policy3|credit_req_text1|credit_req_text2|credit_req_text3|dvl_query|IVR_require_query|IVR_check_query|


