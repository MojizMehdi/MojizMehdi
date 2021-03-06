using HBLAutomationWeb.Beans;
using HBLAutomationWeb.XML.apiconfiguration;
using HBLAutomationWeb.XML.ElementFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationWeb.Common
{
    class ContextPage
    {
        static ContextPage Instance;
        private int id;
        private StreamWriter sw;

        //private static CoreValidationContextPage CoreValidationContextPageObject;
        List<Tuple<string, string>> ContextData = new List<Tuple<string, string>>();




        //public string Response;
        string Bene_AccountNo;
        string Transaction_Id;
        string Consumer_No;
        string bene_name;
        string Acc_Statement_Days;
        decimal tran_balancee;
        string tran_account;
        string Bill_Status;
        string Company_Code;
        string frequency;
        bool from_to_date_flag = false;
        DateTime calendar_fromdate;
        DateTime calendar_todate;
        DateTime tempdate;
        string consumerno;
        Dictionary<string, string> acc_info;
        Dictionary<string, string> tran_info;
        Dictionary<string, Tuple<string, string, string>> cust_limit_detail;
        List<string> Account_No;
        List<string> AccountForTag;
        int sizecount;
        string Account_Type;
        string username;
        bool rating_check = false;
        string schedule_config;
        string customer_info_id;
        string is_tran_req;
        string is_otp_req;
        string Transaction_Category; string No_of_Transaction; string Tran_Type;
        string From_Date; string To_Date;
        string Min_Amount; string Max_Amount; string Acc_no_or_mobile;
        string bill_company; string payee_nick; string to_bank;
        bool tran_from_date_flag = false;
        bool tran_to_date_flag = false;
        string scroll_text;
        string customer_cnic;
        string customer_type;
        string ivr_req;
        string enable_psd;
        string enable_check;
        string pass_policy1;
        string pass_policy2;
        string pass_policy3;
        bool tran_pass_flag = false;
        bool last_login_flag = false;
        bool last_login_pass_flag = false;
        int multi_payment_amount;
        string Customer_Account_Type;
        string avail_limit_cc;
        string is_paid_req;
        string home_branch_del_flag;
        string TermDepositYears;
        int term_deposit_check;
        decimal term_deposit_balance;
        string customer_profile_id;
        string invest_fund_name;
        string[] multi_bill_consumers;
        string product_id;
        string old_limit;
        string fund_disclaimer_popup;
        bool signup_check;
        string offered_rate;
        string ref_no;
        int user_schedule_count;
        public string CutOvertime = null;
        public string Date = null;
        string HostReferenceNo;
        List<string> scroll_items_list;
        string is_si_allowed = "";
        string is_partial = "";
        decimal mutual_fund_bal;
        string Billing_Month;
        string mobile;
        bool change_loginID_check = false;
        string date_string = "";
        string schedule_tran_id = "";
        string bene_count = "";
        string bene_id = "";
        List<string> iteration_dates_schedule;
        bool is_delete = false;
        bool is_otp_false_AP = false;
        string acc_bal_ap = "";
        string acc_bal_ap_db = "";
        string commission_ap = "";
        string category_code = "";
        string tran_amount = "";
        string category_value = "";

        public void Set_CategoryValue(string category_value)
        {
            this.category_value = category_value;
        }
        public string Get_CategoryValue()
        {
            return category_value;
        }
        public void Set_TranAmount(string tran_amount)
        {
            this.tran_amount = tran_amount;
        }
        public string Get_TranAmount()
        {
            return tran_amount;
        }
        public void Set_Category_Code(string category_code)
        {
            this.category_code = category_code;
        }
        public string Get_Category_Code()
        {
            return this.category_code;
        }
        public void Set_IS_OTP_False(bool is_otp_false_AP)
        {
            this.is_otp_false_AP = is_otp_false_AP;
        }
        public bool Get_IS_OTP_False()
        {
            return this.is_otp_false_AP;
        }
        public void Set_Bene_Count(string bene_count)
        {
            this.bene_count = bene_count;
        }
        public string Get_Bene_Count()
        {
            return bene_count;
        }
        public void Set_AccBalance_AP_DB(string acc_bal_ap_db)
        {
            this.acc_bal_ap_db = acc_bal_ap_db;
        }
        public string Get_AccBalance_AP_DB()
        {
            return acc_bal_ap_db;
        }
        public void Set_AccBalance_AP(string acc_bal_ap)
        {
            this.acc_bal_ap = acc_bal_ap;
        }
        public string Get_AccBalance_AP()
        {
            return acc_bal_ap;
        }
        public void Set_Acc_Commission_AP(string commission_ap)
        {
            this.commission_ap = commission_ap;
        }
        public string Get_Acc_Commission_AP()
        {
            return commission_ap;
        }
        public void Set_ScheduleID(string schedule_tran_id)
        {
            this.schedule_tran_id = schedule_tran_id;
        }
        public string Get_Schedule_ID()
        {
            return schedule_tran_id;
        }
        public string Get_String_Date()
        {
            return date_string;
        }
        public void Set_String_Date(string date_string)
        {
            this.date_string = date_string;
        }
        public string Get_Mobile_No()
        {
            return mobile;
        }
        public void Set_Mobile_No(string mobile)
        {
            this.mobile = mobile;
        }
        public decimal Get_mutual_fund_balance()
        {
            return mutual_fund_bal;
        }
        public void Set_mutual_fund_balance(decimal mutual_fund_bal)
        {
            this.mutual_fund_bal += mutual_fund_bal;
        }
        public void Set_Is_Partial_Allow(string is_partial)
        {
            this.is_partial = is_partial;
        }
        public string Get_Is_Partial_Allow()
        {
            return is_partial;
        }
        public void Set_IS_SI_Allowed(string is_si_allowed)
        {
            this.is_si_allowed = is_si_allowed;
        }
        public string Get_IS_SI_Allowed()
        {
            return is_si_allowed;
        }
        public void Set_scroll_items_list(List<string> scroll_items_list)
        {
            this.scroll_items_list = scroll_items_list;
        }
        public List<string> Get_scroll_items_list()
        {
            return scroll_items_list;
        }
        public void Set_HostReferenceNo(string HostReferenceNo)
        {
            this.HostReferenceNo = HostReferenceNo;
        }
        public string Get_HostReferenceNo()
        {
            return HostReferenceNo;
        }
        public void SetUserScheduleCount(int user_schedule_count)
        {
            this.user_schedule_count = user_schedule_count;
        }
        public int GetUserScheduleCount()
        {
            return user_schedule_count;
        }
        public void SetFundDisclaimerPopup(string fund_disclaimer_popup)
        {
            this.fund_disclaimer_popup = fund_disclaimer_popup;
        }
        public string GetFundDisclaimerPopup()
        {
            return fund_disclaimer_popup;
        }
        public void SetOfferedRate(string offered_rate)
        {
            this.offered_rate = offered_rate;
        }
        public string GetOfferedRate()
        {
            return offered_rate;
        }
        public void SetTermRefNo(string ref_no)
        {
            this.ref_no = ref_no;
        }
        public string GetTermRefNo()
        {
            return ref_no;
        }
        public void SetOldLimit(string old_limit)
        {
            this.old_limit = old_limit;
        }
        public string GetOldLimit()
        {
            return old_limit;
        }
        public void SetProductID(string product_id)
        {
            this.product_id = product_id;
        }
        public string GetProductID()
        {
            return product_id;
        }
        public void SetInvestFundName(string invest_fund_name)
        {
            this.invest_fund_name = invest_fund_name;
        }
        public string GetInvestFundName()
        {
            return invest_fund_name;
        }
        public void SetCustomerProfileID(string customer_profile_id)
        {
            this.customer_profile_id = customer_profile_id;
        }
        public string GetCustomerProfileID()
        {
            return customer_profile_id;
        }
        public void Set_signup_check(bool signup_check)
        {
            this.signup_check = signup_check;
        }
        public bool Get_signup_check()
        {
            return this.signup_check;
        }
        public void Set_Change_LoginID_Check(bool change_loginID_check)
        {
            this.change_loginID_check = change_loginID_check;
        }
        public bool Get_Change_LoginID_Check()
        {
            return this.change_loginID_check;
        }
        public void Set_IS_DELETED(bool is_delete)
        {
            this.is_delete = is_delete;
        }
        public bool Get_IS_DELETED()
        {
            return this.is_delete;
        }
        public void SetIsPaidReq(string is_paid_req)
        {
            this.is_paid_req = is_paid_req;
        }
        public string GetIsPaidReq()
        {
            return is_paid_req;
        }
        public void SetHomeBranchDelFlag(string home_branch_del_flag)
        {
            this.home_branch_del_flag = home_branch_del_flag;
        }
        public string GetHomeBranchDelFlag()
        {
            return home_branch_del_flag;
        }
        public void SetCredit_Card_Check(string Customer_Account_Type)
        {
            this.Customer_Account_Type = Customer_Account_Type;
        }
        public string GetCredit_Card_Check()
        {
            return Customer_Account_Type;
        }
        public void SetTranFromDateFlag(bool tran_from_date_flag)
        {
            this.tran_from_date_flag = tran_from_date_flag;
        }
        public bool GetTranFromDateFlag()
        {
            return tran_from_date_flag;
        }
        public void SetTranPassFlag(bool tran_pass_flag)
        {
            this.tran_pass_flag = tran_pass_flag;
        }
        public bool GetTranPassFlag()
        {
            return tran_pass_flag;
        }
        public void SetLastLoginFlag(bool last_login_flag)
        {
            this.last_login_flag = last_login_flag;
        }
        public bool GetLastLoginFlag()
        {
            return last_login_flag;
        }
        public void SetLastLoginPassFlag(bool last_login_pass_flag)
        {
            this.last_login_pass_flag = last_login_pass_flag;
        }
        public bool GetLastLoginPassFlag()
        {
            return last_login_pass_flag;
        }
        //public void Set_is_tranpass_req(string is_tranpass_req)
        //{
        //    this.is_tranpass_req = is_tranpass_req;
        //}
        //public string Get_is_tranpass_req()
        //{
        //    return is_tranpass_req;
        //}
        public int Get_multi_payment_amount()
        {
            return multi_payment_amount;
        }
        public void Set_multi_payment_amount(int multi_payment_amount)
        {
            this.multi_payment_amount = multi_payment_amount;
        }

        //public void SetCreatedOnFlag(bool created_on_flag) 
        //{   
        //    this.created_on_flag = created_on_flag;
        //}
        //public bool GetCreatedOnFlag()
        //{
        //    return created_on_flag;
        //}
        //public void SetUpdatedOnFlag(bool updated_on_flag)
        //{
        //    this.updated_on_flag = updated_on_flag;
        //}
        //public bool GetUpdatedOnFlag()
        //{
        //    return updated_on_flag;
        //}
        public void Set_multi_bill_consumers(string[] multi_bill_consumers)
        {
            this.multi_bill_consumers = multi_bill_consumers;
        }
        public string[] Get_multi_bill_consumers()
        {
            return this.multi_bill_consumers;
        }
        public void SetTranToDateFlag(bool tran_to_date_flag)
        {
            this.tran_to_date_flag = tran_to_date_flag;
        }
        public bool GetTranToDateFlag()
        {
            return tran_to_date_flag;
        }
        public void SetTranCategory(string Transaction_Category)
        {
            this.Transaction_Category = Transaction_Category;
        }
        public string GetTranCategory()
        {
            return Transaction_Category;
        }
        public void SetNoOfTran(string No_of_Transaction)
        {
            this.No_of_Transaction = No_of_Transaction;
        }
        public string GetNoOfTran()
        {
            return No_of_Transaction;
        }
        public void SetTranType(string Tran_Type)
        {
            this.Tran_Type = Tran_Type;
        }
        public string GetTranType()
        {
            return Tran_Type;
        }
        public void SetMinAmount(string Min_Amount)
        {
            this.Min_Amount = Min_Amount;
        }
        public string GetMinAmount()
        {
            return Min_Amount;
        }
        public void SetMaxAmount(string Max_Amount)
        {
            this.Max_Amount = Max_Amount;
        }
        public string GetMaxAmount()
        {
            return Max_Amount;
        }
        public void SetAccNoMobile(string Acc_no_or_mobile)
        {
            this.Acc_no_or_mobile = Acc_no_or_mobile;
        }
        public string GetAccNoMobile()
        {
            return Acc_no_or_mobile;
        }
        public void SetBillCompany(string bill_company)
        {
            this.bill_company = bill_company;
        }
        public string GetBillCompany()
        {
            return bill_company;
        }
        public void SetPayeeNick(string payee_nick)
        {
            this.payee_nick = payee_nick;
        }
        public string GetPayeeNick()
        {
            return payee_nick;
        }
        public void SetToBank(string to_bank)
        {
            this.to_bank = to_bank;
        }
        public string GetToBank()
        {
            return to_bank;
        }

        public void SetFromDate(string From_Date)
        {
            this.From_Date = From_Date;
        }
        public string GetFromDate()
        {
            return From_Date;
        }
        public void SetToDate(string To_Date)
        {
            this.To_Date = To_Date;
        }
        public string GetToDate()
        {
            return To_Date;
        }

        public void SetRatingCheck(bool rating_check)
        {
            this.rating_check = rating_check;
        }
        public bool GetRatingCheck()
        {
            return rating_check;
        }
        public void SetAccNumbers(List<string> Account_No)
        {
            this.Account_No = Account_No;
        }
        public List<string> GetAccNumbers()
        {
            return this.Account_No;
        }
        public void SetAccountForTag(List<string> AccountForTag)
        {
            this.AccountForTag = AccountForTag;
        }
        public List<string> GetAccountForTag()
        {
            return this.AccountForTag;
        }
        public void Set_acc_balances(Dictionary<string, string> acc_info)
        {
            this.acc_info = acc_info;
        }
        public Dictionary<string, string> Get_acc_balance()
        {
            return this.acc_info;

        }
        public void SetCustLimitDetail(Dictionary<string, Tuple<string, string, string>> cust_limit_detail)
        {
            this.cust_limit_detail = cust_limit_detail;
        }
        public Dictionary<string, Tuple<string, string, string>> GetCustLimitDetail()
        {
            return this.cust_limit_detail;
        }

        public void Set_tran_balances(Dictionary<string, string> tran_info)
        {
            this.tran_info = tran_info;
        }

        public Dictionary<string, string> Get_tran_balance(Dictionary<string, string> tran_info)
        {
            return this.tran_info;
        }
        public bool Getfrom_to_date_flag()
        {
            return from_to_date_flag;
        }
        public void Setcalendar_fromdate(DateTime calendar_fromdate)
        {
            this.calendar_fromdate = calendar_fromdate;
            from_to_date_flag = true;
        }
        public DateTime Getcalendar_fromdate()
        {
            return calendar_fromdate;
        }
        public void Settempdate(DateTime tempdate)
        {
            this.tempdate = tempdate;
        }
        public DateTime Gettempdate()
        {
            return tempdate;
        }
        public void Setcalendar_todate(DateTime calendar_todate)
        {
            this.calendar_todate = calendar_todate;
        }
        public DateTime Getcalendar_todate()
        {
            return calendar_todate;
        }
        public void Setfrequency(string frequency)
        {
            this.frequency = frequency;
        }
        public List<string> Get_iteration_dates_schedule()
        {
            return iteration_dates_schedule;
        }
        public void Set_iteration_dates_schedule(List<string> iteration_dates_schedule)
        {
            this.iteration_dates_schedule = iteration_dates_schedule;
        }
        public string Getfrequency()
        {
            return frequency;
        }
        public void SetCompany_Code(string Company_Code)
        {
            this.Company_Code = Company_Code;
        }
        public string GetCompany_Code()
        {
            return Company_Code;
        }
        public void SetConsumer_No(string Consumer_No)
        {
            this.Consumer_No = Consumer_No;
        }
        public string GetConsumer_No()
        {
            return Consumer_No;
        }
        public void SetBill_Status(string Bill_Status)
        {
            this.Bill_Status = Bill_Status;
        }
        public string GetBill_Status()
        {
            return Bill_Status;
        }
        public string GetBilling_Month()
        {
            return Billing_Month;
        }
        public void SetBilling_Month(string Billing_Month)
        {
            this.Billing_Month = Billing_Month;
        }
        public void SetTransaction_Id(string Transaction_Id)
        {
            this.Transaction_Id = Transaction_Id;
        }
        public string GetTransaction_Id()
        {
            return Transaction_Id;
        }
        public void SetBeneAccountNo(string Bene_AccountNo)
        {
            this.Bene_AccountNo = Bene_AccountNo;
        }
        public string GetBeneAccountNo()
        {
            return Bene_AccountNo;
        }

        public void SetAccStatementDays(string Acc_Statement_Days)
        {
            this.Acc_Statement_Days = Acc_Statement_Days;
        }
        public string GetAccStatementDays()
        {
            return Acc_Statement_Days;
        }
        //tran_balancee
        public void SetTran_Balance(decimal tran_balancee)
        {
            this.tran_balancee = tran_balancee;
        }
        public decimal GetTran_Balance()
        {
            return tran_balancee;
        }
        public void SetBeneName(string bene_name)
        {
            this.bene_name = bene_name;
        }
        public string GetBeneName()
        {
            return bene_name;
        }
        public void SetTran_Account(string tran_account)
        {
            this.tran_account = tran_account;
        }
        public string GeTran_Account()
        {
            return tran_account;
        }
        public void SetSizeCount(int sizecount)
        {
            this.sizecount = sizecount;
        }
        public int GeTSizeCount()
        {
            return sizecount;
        }
        public void SetAccountType(string Account_Type)
        {
            this.Account_Type = Account_Type;
        }
        public string GetAccountType()
        {
            return Account_Type;
        }
        public void SetUsername(string username)
        {
            this.username = username.ToUpper();
        }
        public string GetUsername()
        {
            return username;
        }
        public void SetScheduleConfig(string schedule_config)
        {
            this.schedule_config = schedule_config;
        }
        public string GetScheduleConfig()
        {
            return schedule_config;
        }
        public void SetCustomerInfoID(string customer_info_id)
        {
            this.customer_info_id = customer_info_id;
        }
        public string GetCustomerInfoID()
        {
            return customer_info_id;
        }
        public void SetOTPReq(string is_otp_req)
        {
            this.is_otp_req = is_otp_req;
        }
        public string GetOTPReq()
        {
            return is_otp_req;
        }
        public void SetTranPassReq(string is_tran_req)
        {
            this.is_tran_req = is_tran_req;
        }
        public string GetTranPassReq()
        {
            return is_tran_req;
        }
        public void SetScrollText(string scroll_text)
        {
            this.scroll_text = scroll_text;
        }
        public string GetScrollText()
        {
            return scroll_text;
        }
        public void SetCustomerCNIC(string customer_cnic)
        {
            this.customer_cnic = customer_cnic;
        }
        public string GetCustomerCNIC()
        {
            return customer_cnic;
        }
        public void SetCustomerType(string customer_type)
        {
            this.customer_type = customer_type;
        }
        public string GetCustomerType()
        {
            return customer_type;
        }
        public void SetIVRReq(string ivr_req)
        {
            this.ivr_req = ivr_req;
        }
        public string GetIVRReq()
        {
            return ivr_req;
        }
        public void SetEnablePSD(string enable_psd)
        {
            this.enable_psd = enable_psd;
        }
        public string GetEnablePSD()
        {
            return enable_psd;
        }
        public void SetEnableCheck(string enable_check)
        {
            this.enable_check = enable_check;
        }
        public string GetEnableCheck()
        {
            return enable_check;
        }
        public void SetPassPolicy1(string pass_policy1)
        {
            this.pass_policy1 = pass_policy1;
        }
        public string GetPassPolicy1()
        {
            return pass_policy1;
        }
        public void SetPassPolicy2(string pass_policy2)
        {
            this.pass_policy2 = pass_policy2;
        }
        public string GetPassPolicy2()
        {
            return pass_policy2;
        }
        public void SetPassPolicy3(string pass_policy3)
        {
            this.pass_policy3 = pass_policy3;
        }
        public string GetPassPolicy3()
        {
            return pass_policy3;
        }
        public void SetCC_Limit(string avail_limit_cc)
        {
            this.avail_limit_cc = avail_limit_cc;
        }
        public string GetCC_Limit()
        {
            return avail_limit_cc;
        }
        public void Set_TermDepositYears(string TermDepositYears)
        {
            this.TermDepositYears = TermDepositYears;
        }
        public string Get_TermDepositYears()
        {
            return TermDepositYears;
        }
        public int Get_term_deposit_check()
        {
            return term_deposit_check;
        }
        public void Set_term_deposit_check(int term_deposit_check)
        {
            this.term_deposit_check = term_deposit_check;
        }
        public decimal Get_term_deposit_balance()
        {
            return term_deposit_balance;
        }
        public void Set_term_deposit_balance(decimal term_deposit_balance)
        {
            this.term_deposit_balance += term_deposit_balance;
        }
        //public string this[string key]    

        //{
        // returns value if exists
        //get { return acc_info[key]; }

        // updates if exists, adds if doesn't exist
        //set { acc_info[key] = value; }
        // }

        public static IWebDriver driver;

        //public ApiConfiguration apiConfiguration;
        public ElementFactory elementFactory;
        List<Element> Elements = new List<Element>();







        ExcelRecord excelRecord { get; set; }

        public ExcelRecord GetExcelRecord()
        {
            return excelRecord;

        }

        public void SetExcelRecord(ExcelRecord record)
        {
            excelRecord = record;
        }


        public string GetDate()
        {
            Date = DateTime.Today.AddDays(-1).ToString();
            Date = Date.Substring(0, 10);
            if (Date.Substring(10, 1) == " ")
            {
                Date = Date.Substring(0, 9);
            }
            return Date;
        }

        public string GetTime()
        {
            string localDate = DateTime.Now.AddMinutes(2).ToString();
            string time;
            if (localDate.Substring(localDate.Length - 11, 1) == " ")
            {
                time = localDate.Substring(localDate.Length - 10, 4);
            }
            else
            {
                time = localDate.Substring(localDate.Length - 11, 5);
            }
            string localDate2 = localDate.Substring(localDate.Length - 2, 2);
            CutOvertime = time + " " + localDate2;
            return CutOvertime;
        }

        public void SetElement(ElementFactory elements)
        {
            elementFactory = elements;
        }

        public Element GetElement(string keyword)
        {
            try
            {
                string Module = keyword.Split('_')[0];
                return elementFactory.Module.First
                    (m => m.Name.Equals(Module)).Element.First(e => e.Keyword.Equals(keyword));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("The keyword {0} is not found.", keyword));
            }
        }


        public ElementFactory GetElement()
        {
            return elementFactory;
        }



        //public string GetCardID()
        //{
        //    return CardID;
        //}

        //public string GetEndPoint()
        //{
        //    return EndPoint;
        //}

        //public string GetContentMessage()
        //{
        //    return ContentMessage;
        //}
        //public string GetFileName()
        //{
        //    return FileName;
        //}

        //public string GetFilePath()
        //{
        //    return FilePath;
        //}
        //public string GetBody()
        //{
        //    return Body;
        //}
        //public string GetCustomerID()
        //{
        //    return Customer_ID;
        //}

        //public string GetInstallation_id()
        //{
        //    return Installation_id;
        //}

        //public string GetCustomerId()
        //{
        //    return CustomerId;
        //}

        //public string GetQueryParam()
        //{
        //    return QueryParam;
        //}

        //public string GetExternalToken()
        //{
        //    return ExternalToken;
        //}
        //public string GetResourceID()
        //{
        //    return ResourceID;
        //}
        //public string GetContentType()
        //{
        //    return ContentType;
        //}
        //public string GetProductID()
        //{
        //    return ProductID;
        //}
        //public string GetTitle()
        //{
        //    return Title;
        //}
        //public int GetALIAS_ID()
        //{

        //    return AliasID;
        //}
        //public string GetAlias()
        //{
        //    return Alias;
        //}
        //public int GetWalletStatus()
        //{
        //    return WalletStatus;
        //}
        //public string GetSubscriptiontype()
        //{
        //    return Subscriptiontype;
        //}

        //public string GetUserID()
        //{
        //    return UserID;
        //}

        //public string GetRelationship_ID()
        //{
        //    return Relationship_ID;
        //}

        //public string GetDateInternationalTran()
        //{
        //    return DateInternationalTran;
        //}
        //public string GetTimeInternationalTran()
        //{
        //    return TimeInternationalTran;
        //}



        //public string GetProductCode()
        //{
        //    return productCode;
        //}

        public static IWebDriver Driver
        {
            set { driver = value; }
            get { return driver; }
        }

        //public void SetEndPoint(string endPoint)
        //{
        //    EndPoint = endPoint;
        //}

        ////Edited by Hunain
        //public void SetContentMessage(string contentMessage)
        //{
        //    ContentMessage = contentMessage;
        //}
        //public void SetDateInternationalTran(string date)
        //{
        //    DateInternationalTran = date;
        //}

        //public void SetTimeInternationalTran(string time)
        //{
        //    TimeInternationalTran = time;
        //}
        //public void SetFileName(string name)
        //{
        //    FileName = name;
        //}

        //public void SetCustomer_ID(string customer_id)
        //{
        //    Customer_ID = customer_id;
        //}

        //public void SetInstallation_ID(string installation_id)
        //{
        //    Installation_id = installation_id;
        //}

        //public void SetCardID(string cardid)
        //{
        //    CardID = cardid;
        //}
        //public void SetFilePath(string path)
        //{
        //    FilePath = path;
        //}

        //public void SetRelationship_ID(string relationship_id)
        //{
        //    Relationship_ID = relationship_id;
        //}
        //public void SetBody(string body)
        //{
        //    Body = body;
        //}

        //public void SetQueryParam(string query)
        //{
        //    QueryParam = query;
        //}

        //public void SetExternalToken(string external)
        //{
        //    ExternalToken = external;
        //}
        //public void SetResourceID(string resourceId)
        //{
        //    ResourceID = resourceId;
        //}
        //public void SetContentType(string content)
        //{
        //    ContentType = content;
        //}

        //public void SetProductCode(string productcode)
        //{
        //    productCode = productcode;
        //}
        //public void GetProductID(string pID)
        //{
        //    ProductID = pID;
        //}
        //public void GetTitle(string title)
        //{
        //    Title = Title;
        //}
        //public void GetALIAS_ID(int aliasid)
        //{

        //    AliasID = aliasid;
        //}
        //public void GetAlias(string alias)
        //{
        //    Alias = alias;
        //}
        //public void GetWalletStatus(int walletstatus)
        //{
        //    WalletStatus = walletstatus;
        //}
        //public void GetSubscriptiontype(string subscription)
        //{
        //    Subscriptiontype = subscription;
        //}

        //public void GetUserID(string user)
        //{
        //    UserID = user;
        //}

        //public void ResetPostData()
        //{
        //    ProductID = null;
        //    Title = null;
        //    AliasID = 0;
        //    Alias = null;
        //    WalletStatus = 0;
        //    Subscriptiontype = null;
        //    UserID = null;
        //    QueryParam = null;
        //    EndPoint = null;
        //    Body = null;
        //}



        //public string GetResponse()
        //{

        //    return Response;
        //}
        //public void setResponse(string responses)
        //{
        //    if (responses == null)
        //        throw new System.ArgumentNullException("Response is null.");

        //    this.Response = responses;
        //}




        //public void SetCardCVVPair(KeyValuePair<string, string> pair)
        //{
        //    CardCVVPair.Add(pair);
        //}

        //public string GetCVVAgainstCard(string cardNumber)
        //{
        //    try
        //    {

        //        foreach (KeyValuePair<string, string> p in CardCVVPair)
        //        {
        //            if (p.Key.Equals(cardNumber))
        //                return p.Value;

        //        }
        //        return null;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new AssertFailedException(exception.Message);
        //    }

        //}
        //public string GetContextData(string columnName)
        //{
        //    try
        //    {
        //        foreach (var p in ContextData)
        //        {
        //            if (p.Item1 == columnName)
        //                return p.Item2;
        //        }
        //        return null;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new AssertFailedException(exception.Message);
        //    }
        //}


        //public void SetContextData(string columnName, string columnValue)
        //{
        //    ContextData.Add(new Tuple<string, string>(columnName, columnValue));
        //}

        //public int RemoveContextData(string columnName)
        //{
        //    return ContextData.RemoveAll(x => x.Item1 == columnName);
        //}

        public static ContextPage GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ContextPage();

            }
            return Instance;
        }

        public ContextPage()
        {

        }



        public int getID()
        {
            return id;
        }
        public void setID(int id)
        {
            this.id = id;
        }

        public StreamWriter getStreamWriter()
        {
            return sw;
        }
        public void setStreamWriter(StreamWriter s)
        {
            this.sw = s;
        }


        //public string GetProductName()
        //{
        //    return ProductName;
        //}

        //public void SetProductName(string productname)
        //{
        //    ProductName = productname;
        //}

        //public string GetENTITY_NAME()
        //{
        //    return ENTITY_NAME;
        //}

        //public void SetENTITY_NAME(string entityname)
        //{
        //    ENTITY_NAME = entityname;
        //}






        //public string GetFEE_CODE()
        //{
        //    return FEE_CODE;
        //}

        //public void SetFEE_CODE(string feecode)
        //{
        //    FEE_CODE = feecode;
        //}


        //public string GetCloneName()
        //{
        //    return CloneName;
        //}

        //public void SetCloneName(string clonename)
        //{
        //    CloneName = clonename;
        //}

        //public void SETOTP(string otp)
        //{
        //    OTP = otp;
        //}
        //public string Getotp()
        //{
        //    return OTP;
        //}


        //public string GetNationalId()
        //{
        //    return NationalId;
        //}

        //public void SetNationalId(string nationalid)
        //{
        //    NationalId = nationalid;
        //}


        //public void SetCustomerId(string customerId)
        //{
        //    CustomerId = customerId;
        //}




        //public string GetEmployee_CNIC()
        //{
        //    return Employee_CNIC;
        //}

        //public void SetEmployee_CNIC(string employee_CNIC)
        //{
        //    Employee_CNIC = employee_CNIC;
        //}




        //public string GetCustomerNIC_Recard()
        //{
        //    return CustomerNIC_Recard;
        //}

        //public void SetCustomerNIC_Recard(string customerNIC_Recard)
        //{
        //    CustomerNIC_Recard = customerNIC_Recard;
        //}

        //public string Getcount()
        //{
        //    return count;
        //}

        //public void Setcount(string count_)
        //{
        //    count = count_;
        //}

        //public List<KeyValuePair<string, string>> files = new List<KeyValuePair<string, string>>();
        //public List<KeyValuePair<string, string>> formBody = new List<KeyValuePair<string, string>>();
        //public List<KeyValuePair<string, string>> getFiles()
        //{
        //    return files;
        //}

        //public void setFiles(List<KeyValuePair<string, string>> files)
        //{
        //    this.files = files;
        //}

        //public List<KeyValuePair<string, string>> getFormBody()
        //{
        //    return formBody;
        //}

        //public void setFormBody(List<KeyValuePair<string, string>> formBody)
        //{
        //    this.formBody = formBody;
        //}


        /////Only For IB will be generalized later

        //List<KeyValuePair<string, string>> Variables = new List<KeyValuePair<string, string>>();
        //public int CountOfNodes;
        //public int Counter = 0;
        //public string PayOrderReferenceNumber;
        //public string ChequeBookReferenceNumber;
        //public string DemoGraphicReferenceNumber;
        //public string NewCardReferenceNumber;
        //public string CardReferenceNumber;
        //public string CardOpeningReferenceNumber;
        //public string CardBlockingReferenceNumber;
        //public string CurrentTOKENForcardlinking;
        //public string temp { get; set; }
        //public string B_ID = null;
        //public string Session_ID = null;
        //public string RefreshToken = null;
        public string Current_LoginId = null;
        public List<KeyValuePair<string, string>> LoginId = new List<KeyValuePair<string, string>>();

        //public string StoredReferenceNumber = null;
        //public string StoredDeviceId = null;
        //public string AnotherStoredDeviceId = null;
        //public string StoredPushId = null;
        //public string AnotherStoredPushId = null;
        //public string CardToken = null;
        //public bool Core = false;

        ////AFEX
        //public string StoredUniqueId = null;


        //public string GetB_ID() { return B_ID; }

        //public void SetB_ID(string bid) { B_ID = bid; }

        public string GetCurrent_LoginId() { return Current_LoginId; }

        public void SetCurrent_LoginId(string current_loginid) { Current_LoginId = current_loginid; }

        public void SetLoginId(List<KeyValuePair<string, string>> loginid)
        {
            LoginId = loginid;
        }

        public string GetLoginId(string key)
        {
            foreach (var keyValue in LoginId)
            {
                if (keyValue.Key.Equals(key))
                {
                    return keyValue.Value;
                }
            }
            throw new Exception("Keyword is invalid for loginId");
        }


        //public string GetSession_ID() { return Session_ID; }

        //public void SetSession_ID(string sessionid) { Session_ID = sessionid; }

        //public string GetRefreshToken() { return this.RefreshToken; }

        //public void SetRefreshToken(string refreshToken) { this.RefreshToken = refreshToken; }

        //public string getStoredDeviceId()
        //{
        //    return StoredDeviceId;
        //}

        //public void setStoredDeviceId(string deviceId)
        //{
        //    this.StoredDeviceId = deviceId;
        //}

        //public string getStoredReferenceNumber()
        //{
        //    return StoredReferenceNumber;
        //}

        //public void setStoredReferenceNumber(string referenceNumber)
        //{
        //    this.StoredReferenceNumber = referenceNumber;
        //}

        //public string getAnotherStoredDeviceId()
        //{
        //    return AnotherStoredDeviceId;
        //}

        //public void setAnotherStoredDeviceId(string anotherDeviceId)
        //{
        //    this.AnotherStoredDeviceId = anotherDeviceId;
        //}

        //public string getStoredPushId()
        //{
        //    return StoredPushId;
        //}

        //public void setStoredPushId(string pushId)
        //{
        //    this.StoredPushId = pushId;
        //}

        //public string getAnotherStoredPushId()
        //{
        //    return AnotherStoredPushId;
        //}

        //public void setAnotherStoredPushId(string anotherPushId)
        //{
        //    this.AnotherStoredPushId = anotherPushId;
        //}

        //public string GetCardToken()
        //{
        //    return CardToken;
        //}

        //public void SetCardToken(string token)
        //{
        //    CardToken = token;
        //}

        //public string GetStoredUniqueId()
        //{
        //    return StoredUniqueId;
        //}

        //public void SetStoredUniqueId(string uniqueId)
        //{
        //    StoredUniqueId = uniqueId;
        //}

        //public bool GetCore()
        //{
        //    return Core;
        //}

        //public void SetCore(bool core)
        //{
        //    Core = core;
        //}

        ////By Sidra
        //public int GetCountOfNodes()
        //{
        //    return CountOfNodes;
        //}
        ////By Sidra
        //public void SetCountOfNodes(int count)
        //{
        //    CountOfNodes = count;
        //}

        ////By Sidra
        //public int GetCounter()
        //{
        //    return Counter;
        //}
        ////By Sidra
        //public void SetCounter(int count)
        //{
        //    Counter = count;
        //}

        ////By Sidra
        //public string GetPayOrderReferenceNumber()
        //{
        //    return PayOrderReferenceNumber;
        //}
        ////By Sidra
        //public void SetPayOrderReferenceNumber(string refnum)
        //{
        //    PayOrderReferenceNumber = refnum;
        //}

        //public void SetDemographicReferenceNumber(string refnum)
        //{
        //    DemoGraphicReferenceNumber = refnum;
        //}

        ////By Sidra
        //public string GetChequeBookReferenceNumber()
        //{
        //    return ChequeBookReferenceNumber;

        //}
        //public string GetDemographicReferenceNumber()
        //{
        //    return DemoGraphicReferenceNumber;
        //}
        ////By Ali Masood
        //public void SetChequeBookReferenceNumber(string refnum)
        //{
        //    ChequeBookReferenceNumber = refnum;
        //}



        //public string GetNewCardReferenceNumber()
        //{
        //    return NewCardReferenceNumber;
        //}
        ////ByAli Masood
        //public void SetNewCardkReferenceNumber(string refnum)
        //{
        //    NewCardReferenceNumber = refnum;
        //}
        //public void SetCardOpeningkReferenceNumber(string refnum)
        //{
        //    CardOpeningReferenceNumber = refnum;
        //}
        //public void SetCurrentTOKENForcardlinking(string refnum)
        //{
        //    CurrentTOKENForcardlinking = refnum;
        //}
        //public string GetCurrentTOKENForcardlinking()
        //{
        //    return CurrentTOKENForcardlinking;
        //}
        //public void SetCardBlockingkReferenceNumber(string refnum)
        //{
        //    CardBlockingReferenceNumber = refnum;
        //}

        //public string GetCardReferenceNumber()
        //{
        //    return CardReferenceNumber;
        //}
        //public string GetCardOpenReferenceNumber()
        //{
        //    return CardOpeningReferenceNumber;
        //}
        //public string GetCardblockingReferenceNumber()
        //{
        //    return CardBlockingReferenceNumber;
        //}
        ////By Sidra
        //public void SetCardReferenceNumber(string refnum)
        //{
        //    CardReferenceNumber = refnum;
        //}


        ////By Sidra
        //public string GetVariableValue(string variable)
        //{
        //    try
        //    {
        //        foreach (var key in Variables)
        //        {
        //            if (key.Key.Equals(variable))
        //                return key.Value;
        //        }
        //        return null;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new AssertFailedException(exception.Message);
        //    }
        //}
        ////By Sidra
        //public void SetVariableValue(string variable, string value)
        //{
        //    Variables.Add(new KeyValuePair<string, string>(variable, value));
        //}

        ////Added by Sidra
        //public static string replaceInstallationId(string query)
        //{
        //    if (query.Contains("{installationId}"))
        //    {
        //        query = query.Replace("{installationId}", ContextPage.GetInstance().GetVariableValue("installationId"));
        //    }
        //    return query;
        //}

        ////Added by Sidra
        //public static string ReplaceFilters(string query)
        //{
        //    query = replaceInstallationId(query);

        //    return query;
        //}



        //public Dictionary<string, string> inputlist { get; set; }
        //public Dictionary<string, string> savetable { get; set; }


        //public string CustomerCNIC { get; set; }
        //public string CustomerMobileNumber { get; set; }
        //public string BeneficaryID { get; set; }
        //public string payoutAgent { get; set; }
        //public string payoutAgentBranch { get; set; }
        //public string Name { get; set; }
        //public string CardHolderID { get; set; }
        //public string Promocode { get; set; }
        //public string quoteNumber { get; set; }
    }
}