Feature: BillPayment
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@BillPayment
Scenario Outline: As a user i want to Verify Bill Payment from Agent Portal
	Given the test case title is "<Case>"
	And the test case expected result is "<Expected_Result>"
	And I set value in context from data "<ConsumerNo_Value>" as "ConsumerNo"
	And I set value in context from data "<Company_Value>" as "CompanyName"
	And I set value in context from data "<company_code>" as "Company_Code"
	And I set value in context from data "<Category_Value>" as "Category_value"
	And I set value in context from database "<category_code_query>" as "category_code" on Schema "<db_val2>"
	And I set value in context from database "<is_partial_payment_query>" as "IS_Partial_Payment" on Schema "<db_val2>"
	And I set value in context from database "<is_paid_query>" as "IS_PAID_REQ" on Schema "<db_val2>"
	And update the data by query "<status_query>" on QAT_BPS
	And I save Balance and Commission of Agent 
	And I am clicking on "AgentPortal_OTCTran_TabOpt"
	And I am clicking on "AgentPortal_OTCTran_BillOpt"
	When I select "<Category_Value>" on "Pay_BillType_AP"
	And I set list of elements from scroll view on "Pay_CategoryList_AP"
	And verify the list using "<bps_category_list_query>" on Schema "<db_val2>"
	And I set list of elements from scroll view on "Pay_SubCategoryList_AP"
	And verify the list using "<bps_sub_category_list_query>" on Schema "<db_val2>"
	And I select "<Company_Value>" on "Pay_CompanyType_AP"
	And verify through database on "<consumer_no_label_query>" on Schema "<db_val2>" on "Pay_Consumer_label_AP"
	And I have given "<ConsumerNo_Value>" on "Pay_ConsumerNo_AP"
	And I have given "<Depositor_Number>" on "Pay_DepositorNo_AP"
	And I fill out bill payment details of Agent portal with additional values "<additional_fields_values>"
	And I am performing on "Pay_BillNextBtn_AP"
	And I wait 3000
	And Set parameter in context class "Pay_BillPayment_BillingMonth_AP"
	And I verify bill payment inquiry for Web
	And I have given "<amount>" on "Pay_Amount_AP"
	And I have given "<mpin>" on "Pay_Mpin_AP"
	And I am performing on "Pay_SubmitBtn_AP"
	Then verify through "Success! " on "Pay_TranMessage_AP"
	And verify through "<success_tran_message>" on "Pay_Tran_Full_Message_AP"
	And I save Transaction Info
	And verify the result from "<tpe_amount_query>" on Schema "<db_val_tpe>"
	And verify the result from "<tpe_tran_date_query>" on Schema "<db_val_tpe>"
	And verify the result from "<tpe_created_on_query>" on Schema "<db_val_tpe>"
	And verify the result from "<tpe_updated_on_query>" on Schema "<db_val_tpe>"
	And verify the message "BB" through database on "<tpe_from_acc_type>" on Schema "<db_val_tpe>"
	And verify the message "BB" through database on "<tpe_to_acc_type>" on Schema "<db_val_tpe>"
	And verify the message "<tpe_response_value>" through database on "<tpe_response_query>" on Schema "<db_val_tpe>"
	And verify the message "<ConsumerNo_Value>" through database on "<tpe_consumer_query>" on Schema "<db_val_tpe>"
	And verify the message "<Depositor_Number>" through database on "<tpe_mobile_query>" on Schema "<db_val_tpe>"
	And verify the message "<Company_Value>" through database on "<tpe_company_name_query>" on Schema "<db_val_tpe>"
	And verify the message "<Category_Value>" through database on "<tpe_category_query>" on Schema "<db_val_tpe>"
	And verify the message "<company_code>" through database on "<tpe_company_code_query>" on Schema "<db_val_tpe>"
	And verify the result of two queries "<tpe_from_acc_num_query>" on Schema "<db_val_tpe>" with "<from_acc_value_query>" on Schema "<db_val_bb>" through database
	And verify the result of two queries "<tpe_to_acc_no_query>" on Schema "<db_val_tpe>" with "<to_acc_value_query>" on Schema "<db_val_bb>" through database
	And verify the result from "<bill_status_id_query>" on Schema "<db_val2>"
	And verify the message "<bb_response_value>" through database on "<bb_response_query>" on Schema "<db_val_bb>"
	And verify the message "<bb_status_value>" through database on "<bb_status_query>" on Schema "<db_val_bb>"
	And verify the message "<ConsumerNo_Value>" through database on "<bb_consumer_no_query>" on Schema "<db_val_bb>"
	And verify the message "<company_code>" through database on "<bb_company_code_query>" on Schema "<db_val_bb>"
	And verify the message "<Company_Value>" through database on "<bb_company_name_query>" on Schema "<db_val_bb>"
	And verify the result from "<bb_amount_query>" on Schema "<db_val_bb>"
	And verify the message "BB" through database on "<bb_from_acc_type_query>" on Schema "<db_val_bb>"
	And verify the message "BB" through database on "<bb_to_acc_type_query>" on Schema "<db_val_bb>"
	And verify the result of two queries "<bb_from_acc_query>" on Schema "<db_val_bb>" with "<from_acc_value_query>" on Schema "<db_val_bb>" through database
	And verify the result of two queries "<bb_to_acc_query>" on Schema "<db_val_bb>" with "<to_acc_value_query>" on Schema "<db_val_bb>" through database
	And verify the message "<Depositor_Number>" through database on "<bb_mobile_query>" on Schema "<db_val_bb>"
	And verify the message "<Category_Value>" through database on "<bb_company_type_query>" on Schema "<db_val_bb>"
	And verify the result from "<tpe_created_on_query>" on Schema "<db_val_bb>"
	And verify the result from "<tpe_updated_on_query>" on Schema "<db_val_bb>"
	And I set value in context from database "<tpe_guid_query>" as "GUID" on Schema "<db_val_tpe>"
	And verify the message "<bb_status_value>" through database on "<bps_bill_status_query>" on Schema "<db_val2>"
	And verify the message "<Company_Value>" through database on "<bps_company_name_query>" on Schema "<db_val2>"
	And verify the result from "<bps_created_on_query>" on Schema "<db_val2>"
	And verify the result from "<bps_updated_on_query>" on Schema "<db_val2>"
	And verify the message "<ConsumerNo_Value>" through database on "<bps_consumer_no_query>" on Schema "<db_val2>"
	And verify the result from "<bps_amount_query>" on Schema "<db_val2>"
	And verify the message "<Category_Value>" through database on "<bps_company_code_query>" on Schema "<db_val2>"
	And verify the message "<channel_code_value>" through database on "<bps_channel_code_query>" on Schema "<db_val2>"
	And verify the message "<company_code>" through database on "<access_key_query>" on Schema "<db_val_tpe>"
	And verify the message "<channel_code_value>" through database on "<channel_code_query>" on Schema "<db_val_tpe>"
	And verify the message "<channel_code_value>" through database on "<bb_channel_code_query>" on Schema "<db_val_bb>"
	And verify the message "<company_code>" through database on "<bb_access_key_query>" on Schema "<db_val_bb>"
	And verify the result of two queries "<agent_id_query>" on Schema "<db_val_tpe>" with "<agent_id_value_query>" on Schema "<db_val_bb>" through database
	And verify the result of two queries "<agent_userid_query>" on Schema "<db_val_tpe>" with "<agent_user_id_value_query>" on Schema "<db_val_bb>" through database
	And verify the result from "<time_stamp_one_ag_query>" on Schema "<db_val_bb>"
	And verify the result from "<time_stamp_two_ag_query>" on Schema "<db_val_bb>"
	And verify the message "<company_code>" through database on "<commission_key_ag_query>" on Schema "<db_val_bb>"
	And verify the message "<company_code>" through database on "<commission_key_tpe_query>" on Schema "<db_val_tpe>"
	And verify the result of two queries "<agent_login_id_query>" on Schema "<db_val_bb>" with "<agent_login_id_value_query>" on Schema "<db_val_bb>" through database
	And verify the result of two queries "<city_value_query>" on Schema "<db_val_bb>" with "<city_query>" on Schema "<db_val_tpe>" through database
	And verify the result of two queries "<trade_name_value_query>" on Schema "<db_val_bb>" with "<trade_name_query>" on Schema "<db_val_tpe>" through database
	And verify the result of two queries "<ag_guid_query>" on Schema "<db_val_bb>" with "<tpe_guid_query>" on Schema "<db_val_tpe>" through database
	And verify the result of two queries "<city_value_query>" on Schema "<db_val_bb>" with "<city_ag_query>" on Schema "<db_val_bb>" through database
	And verify the message "<sub_channel_value>" through database on "<sub_channel_ag_query>" on Schema "<db_val_bb>"
	And verify the result of two queries "<agent_user_id_ag_query>" on Schema "<db_val_bb>" with "<agent_user_id_value_query>" on Schema "<db_val_bb>" through database
	And verify the message "<response_code_value>" through database on "<response_code_query>" on Schema "<db_val_tpe>"
	And verify the message "<tran_status_value>" through database on "<tran_status_tpe_query>" on Schema "<db_val_tpe>"
	And verify the message "<sub_channel_value>" through database on "<channel_tpe_query>" on Schema "<db_val_tpe>"
	And verify the result of two queries "<alerting_sms_query>" on Schema "<db_val_tpe>" with "<alerting_sms_value_query>" on Schema "<db_val_alert>" through database
	And I verify Balance and Commission of Agent

	@source:Data/BillPayment.xlsx
	Examples: 
	|Case|Expected_Result|Category_Value|Company_Value|ConsumerNo_Value|Depositor_Number|company_code|additional_fields_values|amount|mpin|db_val2|category_code_query|is_partial_payment_query|status_query|tpe_consumer_query|tpe_amount_query|tpe_from_acc_num_query|tpe_from_acc_type|tpe_to_acc_no_query|tpe_to_acc_type|tpe_response_query|tpe_response_value|tpe_company_code_query|tpe_tran_date_query|tpe_mobile_query|tpe_category_query|tpe_created_on_query|tpe_updated_on_query|tpe_sender_mobile_query|db_val_tpe|tpe_company_name_query|from_acc_value_query|to_acc_value_query|bill_status_id_query|is_paid_query|bb_company_type_query|bb_created_on_query|bb_updated_on_query|bb_mobile_query|bb_from_acc_query|bb_from_acc_type_query|bb_to_acc_query|bb_to_acc_type_query|bb_status_query|bb_amount_query|bb_company_name_query|bb_company_code_query|bb_consumer_no_query|bb_response_query|bb_response_value|bb_status_value|db_val_bb|tpe_guid_query|bps_consumer_no_query|bps_amount_query|bps_to_acc_query|bps_company_code_query|bps_channel_code_query|channel_code_value|bps_created_on_query|bps_updated_on_query|bps_company_name_query|bps_bill_status_query|bps_category_list_query|bps_sub_category_list_query|consumer_no_label_query|success_tran_message|access_key_query|channel_code_query|bb_channel_code_query|bb_access_key_query|agent_id_query|agent_id_value_query|agent_userid_query|agent_user_id_value_query|time_stamp_one_ag_query|time_stamp_two_ag_query|commission_key_ag_query|commission_key_tpe_query|agent_login_id_query|agent_login_id_value_query|city_value_query|city_query|trade_name_query|trade_name_value_query|ag_guid_query|city_ag_query|sub_channel_ag_query|sub_channel_value|agent_user_id_ag_query|response_code_query|response_code_value|tran_status_tpe_query|tran_status_value|channel_tpe_query|alerting_sms_query|alerting_sms_value_query|db_val_alert|