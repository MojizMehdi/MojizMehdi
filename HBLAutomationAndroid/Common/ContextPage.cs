using HBLAutomationAndroid.Beans;
using HBLAutomationAndroid.XML.apiconfiguration;
using HBLAutomationAndroid.XML.ElementFactory;
using HBLAutomationWeb.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HBLAutomationAndroid.Common
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
        string Acc_Statement_Days;
        string Bill_Status;
        string Billing_Month;
        string Company_Code;
        string frequency;
        bool from_to_date_flag = false;
        DateTime calendar_fromdate;
        DateTime calendar_todate;
        DateTime tempdate;
        Dictionary<string, string> acc_info;
        Dictionary<string, string> creditcard_balances;
        string username;
        decimal tran_balance;
        string tran_account;
        string to_account;
        string category_value;
        string Account_Type;
        string is_otp_req;
        string is_tranpass_req = "0";
        string Tran_Type;
        string bene_name;
        bool TranTypeBene;
        public string CutOvertime = null;
        public string Date = null;
        string[] multi_bill_consumers;
        string[] multi_tran_ids;
        double multi_payment_amount;
        double tran_amount;
        List<string> iteration_dates_schedule;
        string TermDepositYears;
        int term_deposit_check;
        int mutual_fund_check = 0;
        decimal term_deposit_balance = 0;
        decimal mutual_fund_balance = 0;
        int no_of_accounts;
        string Is_Paid_Marking_Req;
        int bene_count_inter_branch;
        int bene_count_inter_bank;
        string scroll_text;
        string customer_cnic;
        List<string> AccountForTag;
        List<string> Account_No;
        List<string> IsnotLinkedAccNumbers;
        int sizecount;
        bool signup_check;
        string ivr_req;
        string enable_psd;
        bool tran_pass_flag = false;
        bool last_login_flag = false;
        string customer_type;
        string mutual_fund_name;
        string MutualFundDisclaimerPopup;
        string cust_profile_id;
        string BP_schedule_config;
        List<string> scroll_items_list;
        string GUID;
        string HostReferenceNo;
        string BillPaymentCategory;
        string Is_Partial_Payment_Allowed;
        int account_count = 0;
        string old_limit;
        Dictionary<string, Tuple<string, string, string>> cust_limit_detail;
        string mobile_no;
        string CreditCard_Check;
        bool FCY_Tran_Check;
        decimal conversion_rate;
        //public static string platform_version;

        //public void Set_platform_version(string platform_version)
        //{
        //    this.platform_version = platform_version;
        //}
        //public string Get_platform_version()
        //{
        //    return platform_version;
        //}
        public string GetPlainTextFromHtml(string htmlString)
        {
            //var plainText = HtmlUtilities.ConvertToPlainText(htmlString);
            string htmlTagPattern = "<.*?>";
            var regexCss = new Regex(@"<style.*?</style>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);
            regexCss = new Regex(@"<head.*?</head>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);
            htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
            htmlString = Regex.Replace(htmlString, "{.*?}",string.Empty);
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            htmlString = htmlString.Replace(" ", string.Empty);
            htmlString = htmlString.Replace("\r", string.Empty).Replace("\n", string.Empty);
            return htmlString;
        }
        public void Set_conversion_rate(decimal conversion_rate)
        {
            this.conversion_rate = conversion_rate;
        }
        public decimal Get_conversion_rate()
        {
            return conversion_rate;
        }
        public void Set_FCY_Tran_Check(bool FCY_Tran_Check)
        {
            this.FCY_Tran_Check = FCY_Tran_Check;
        }
        public bool Get_FCY_Tran_Check()
        {
            return FCY_Tran_Check;
        }
        public void SetCredit_Card_Check(string CreditCard_Check)
        {
            this.CreditCard_Check = CreditCard_Check;
        }
        public string GetCredit_Card_Check()
        {
            return CreditCard_Check;
        }
        public void Set_mobile_no(string kmobile_no)
        {
            this.mobile_no = kmobile_no;
        }
        public string Get_mobile_no()
        {
            return mobile_no;
        }
        public void SetCustLimitDetail(Dictionary<string, Tuple<string, string, string>> cust_limit_detail)
        {
            this.cust_limit_detail = cust_limit_detail;
        }
        public Dictionary<string, Tuple<string, string, string>> GetCustLimitDetail()
        {
            return this.cust_limit_detail;
        }
        public void SetOldLimit(string old_limit)
        { 
            this.old_limit = old_limit;
        }
        public string GetOldLimit()
        {
            return old_limit;
        }
        public void Set_account_count(int account_count)
        {
            this.account_count = account_count;
        }
        public int Get_account_count()
        {
            return account_count;
        }
        public void Set_Is_Partial_Payment_Allowed(string Is_Partial_Payment_Allowed)
        {
            this.Is_Partial_Payment_Allowed = Is_Partial_Payment_Allowed;
        }
        public string Get_Is_Partial_Payment_Allowed()
        {
            return Is_Partial_Payment_Allowed;
        }
        public void Set_HostReferenceNo(string HostReferenceNo)
        {
            this.HostReferenceNo = HostReferenceNo;
        }
        public string Get_HostReferenceNo()
        {
            return HostReferenceNo;
        }
        public void Set_GUID(string GUID)
        {
            this.GUID = GUID;
        }
        public string Get_GUID()
        {
            return GUID;
        }
        public void Set_scroll_items_list(List<string> scroll_items_list)
        {
            this.scroll_items_list = scroll_items_list;
        }
        public List<string> Get_scroll_items_list()
        {
            return scroll_items_list;
        }
        public void Set_BP_schedule_config(string BP_schedule_config)
        {
            this.BP_schedule_config = BP_schedule_config;
        }
        public string Get_BP_schedule_config()
        {
            return BP_schedule_config;
        }
        public void Set_cust_profile_id(string cust_profile_id)
        {
            this.cust_profile_id = cust_profile_id;
        }
        public string Get_cust_profile_id()
        {
            return cust_profile_id;
        }
        public void Set_MutualFundDisclaimerPopup(string MutualFundDisclaimerPopup)
        {
            this.MutualFundDisclaimerPopup = MutualFundDisclaimerPopup;
        }
        public string Get_MutualFundDisclaimerPopup()
        {
            return MutualFundDisclaimerPopup;
        }
        public void Set_mutual_fund_name(string mutual_fund_name)
        {
            this.mutual_fund_name = mutual_fund_name;
        }
        public string Get_mutual_fund_name()
        {
            return mutual_fund_name;
        }
        public void SetCustomerType(string customer_type)
        {
            this.customer_type = customer_type;
        }
        public string GetCustomerType()
        {
            return customer_type;
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
        public void SetEnablePSD(string enable_psd)
        {
            this.enable_psd = enable_psd;
        }
        public string GetEnablePSD()
        {
            return enable_psd;
        }
        public void SetIVRReq(string ivr_req)
        {
            this.ivr_req = ivr_req;
        }
        public string GetIVRReq()
        {
            return ivr_req;
        }
        public void Set_signup_check(bool signup_check)
        {
            this.signup_check = signup_check;
        }
        public bool Get_signup_check()
        {
            return this.signup_check;
        }
        //DateTime start_date;
        //DateTime end_date;

        //public DateTime Get_end_date()
        //{
        //    return end_date;
        //}
        //public void Set_end_date(DateTime end_date)
        //{
        //    this.end_date = end_date;
        //}
        //public DateTime Get_start_date()
        //{
        //    return start_date;
        //}
        //public void Set_start_date(DateTime start_date)
        //{
        //    this.start_date = start_date;
        //}
        public void SetAccNumbers(List<string> Account_No)
        {
            this.Account_No = Account_No;
        }
        public List<string> GetAccNumbers()
        {
            return this.Account_No;
        }
        public void SetIsnotLinkedAccNumbers(List<string> IsnotLinkedAccNumbers)
        {
            this.IsnotLinkedAccNumbers = IsnotLinkedAccNumbers;
        }
        public List<string> GetIsnotLinkedAccNumbers()
        {
            return this.IsnotLinkedAccNumbers;
        }
        public void SetSizeCount(int sizecount)
        {
            this.sizecount = sizecount;
        }
        public int GeTSizeCount()
        {
            return sizecount;
        }
        public void SetAccountForTag(List<string> AccountForTag)
        {
            this.AccountForTag = AccountForTag;
        }
        public List<string> GetAccountForTag()
        {
            return this.AccountForTag;
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
        public int Get_bene_count_inter_bank()
        {
            return bene_count_inter_bank;
        }
        public void Set_bene_count_inter_bank(int bene_count_inter_bank)
        {
            this.bene_count_inter_bank = bene_count_inter_bank;
        }
        public int Get_bene_count_inter_branch()
        {
            return bene_count_inter_branch;
        }
        public void Set_bene_count_inter_branch(int bene_count_inter_branch)
        {
            this.bene_count_inter_branch = bene_count_inter_branch;
        }
        public int Get_no_of_accounts()
        {
            return no_of_accounts;
        }
        public void Set_no_of_accounts(int no_of_accounts)
        {
            this.no_of_accounts = no_of_accounts;
        }
        public string Get_Is_Paid_Marking_Req()
        {
            return Is_Paid_Marking_Req;
        }
        public void Set_Is_Paid_Marking_Req(string Is_Paid_Marking_Req)
        {
            this.Is_Paid_Marking_Req = Is_Paid_Marking_Req;
        }
        public int Get_term_deposit_check()
        {
            return term_deposit_check;
        }
        public void Set_term_deposit_check(int term_deposit_check)
        {
            this.term_deposit_check = term_deposit_check;
        }
        public int Get_mutual_fund_check()
        {
            return mutual_fund_check;
        }
        public void Set_mutual_fund_check(int mutual_fund_check)
        {
            this.mutual_fund_check = mutual_fund_check;
        }
        public decimal Get_mutual_fund_balance()
        {
            return mutual_fund_balance;
        }
        public void Set_mutual_fund_balance(decimal mutual_fund_balance)
        {
            this.mutual_fund_balance += mutual_fund_balance;
        }
        public decimal Get_term_deposit_balance()
        {
            return term_deposit_balance;
        }
        public void Set_term_deposit_balance(decimal term_deposit_balance)
        {
            this.term_deposit_balance += term_deposit_balance;
        }
        public double Get_tran_amount()
        {
            return tran_amount;
        }
        public void Set_tran_amount(double tran_amount)
        {
            this.tran_amount = tran_amount;
        }
        public List<string> Get_iteration_dates_schedule()
        {
            return iteration_dates_schedule;
        }
        public void Set_iteration_dates_schedule(List<string> iteration_dates_schedule)
        {
            this.iteration_dates_schedule = iteration_dates_schedule;
        }
        public double Get_multi_payment_amount()
        {
            return multi_payment_amount;
        }
        public void Set_multi_payment_amount(double multi_payment_amount)
        {
            this.multi_payment_amount = multi_payment_amount;
        }

        public void Set_multi_bill_consumers(string[] multi_bill_consumers)
        {
            this.multi_bill_consumers = multi_bill_consumers;
        }
        public string[] Get_multi_bill_consumers()
        {
            return this.multi_bill_consumers;
        }
        public void Set_multi_tran_ids(string[] multi_tran_ids)
        {
            this.multi_tran_ids = multi_tran_ids;
        }
        public string[] Get_multi_tran_ids()
        {
            return this.multi_tran_ids;
        }
        public void SetTranTypeBene(string TranTypeBene)
        {
            if (TranTypeBene == "0")
            {
                this.TranTypeBene = false;
            }
            else if (TranTypeBene == "1")
            {
                this.TranTypeBene = true;
            }
        }
        public bool GetTranTypeBene()
        {
            return TranTypeBene;
        }
        public void SetBeneName(string bene_name)
        {
            this.bene_name = bene_name;
        }
        public string GetBeneName()
        {
            return bene_name;
        }
        public void Set_is_otp_req(string is_otp_req)
        {
            this.is_otp_req = is_otp_req;
        }
        public string Get_is_otp_req()
        {
            return is_otp_req;
        }
        public void Set_is_tranpass_req(string is_tranpass_req)
        {
            this.is_tranpass_req = is_tranpass_req;
        }
        public string Get_is_tranpass_req()
        {
            return is_tranpass_req;
        }

        public void SetAccountType(string Account_Type)
        {
            this.Account_Type = Account_Type;
        }
        public string GetAccountType()
        {
            return Account_Type;
        }
        public void SetCategory_value(string category_value)
        {
            this.category_value = category_value;
        }
        public string GetCategory_value()
        {
            return category_value;
        }

        public void SetTran_Account(string tran_account)
        {
            this.tran_account = tran_account;
        }
        public string GeTran_Account()
        {
            return tran_account;
        }
        public void SetTran_Balance(decimal tran_balance)
        {
            this.tran_balance = tran_balance;
        }
        public decimal GetTran_Balance()
        {
            return tran_balance;
        }

        public void Set_acc_balances(Dictionary<string, string> acc_info)
        {
            this.acc_info = acc_info;
        }
        public Dictionary<string, string> Get_acc_balances()
        {
            return this.acc_info;

        }
        public void Set_creditcard_balances(Dictionary<string, string> creditcard_balances)
        {
            this.creditcard_balances = creditcard_balances;
        }
        public Dictionary<string, string> Get_creditcard_balances()
        {
            return this.creditcard_balances;

        }
        public void Set_BillPaymentCategory(string BillPaymentCategory)
        {
            this.BillPaymentCategory = BillPaymentCategory;
        }
        public string Get_BillPaymentCategory()
        {
            return BillPaymentCategory;
        }

        public void SetTranType(string Tran_Type)
        {
            this.Tran_Type = Tran_Type;
        }
        public string GetTranType()
        {
            return Tran_Type;
        }
        public void Set_TermDepositYears(string TermDepositYears)
        {
            this.TermDepositYears = TermDepositYears;
        }
        public string Get_TermDepositYears()
        {
            return TermDepositYears;
        }
        public void SetUsername(string username)
        {
            this.username = username.ToUpper();
        }
        public string GetUsername()
        {
            return username;
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
        public void SetToAccount_No(string to_account)
        {
            this.to_account = to_account;
        }
        public string GetToAccount_No()
        {
            return this.to_account;
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
        public string GetBilling_Month()
        {
            return Billing_Month;
        }
        public void SetBilling_Month(string Billing_Month)
        {
            this.Billing_Month = Billing_Month;
        }
        public string GetBill_Status()
        {
            return Bill_Status;
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


        //public static AppiumDriver<AndroidElement> driver;
        //public static AppiumDriver<AndroidElement> driver;
        public static AppiumDriver<AndroidElement> driver;/*= new WpDriver(new Uri("http://localhost:9999"), capabillities);*/
        //public static IWebDriver driver;

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

        // public static IWebDriver Driver

        public static AppiumDriver<AndroidElement> Driver
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

        //public string GetACCOUNT_NO()
        //{
        //    return ACCOUNT_NO;
        //}

        //public void SetACCOUNT_NO(string accountno)
        //{
        //    ACCOUNT_NO = accountno;
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
