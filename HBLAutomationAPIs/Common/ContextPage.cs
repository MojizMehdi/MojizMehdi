using HBLAutomationAPIs.Beans;
using HBLAutomationAPIs.XML.apiconfiguration;
using HBLAutomationAPIs.XML.ElementFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationAPIs.Common
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
        public string EndPoint = null;
        public string QueryParam = null;
        public string CutOvertime = null;
        public string Date = null;
        public ApiConfiguration apiConfiguration;
        IRestResponse response;
        string[] Api_Header;
        string Api_Body;
        string RRN;
        string api_format;
        string RRN_FetchDBCard;
        string baseuri;

        public string Get_baseuri()
        {
            return baseuri;
        }
        public void Set_baseuri(string baseuri)
        {
            this.baseuri = baseuri;
        }
        public string Get_api_format()
        {
            return api_format;
        }
        public void Set_api_format(string api_format)
        {
            this.api_format = api_format;
        }
        public void Set_RRN_FetchDBCard(string RRN_FetchDBCard)
        {
            this.RRN_FetchDBCard = RRN_FetchDBCard;
        }
        public string Get_RRN_FetchDBCard()
        {
            return this.RRN_FetchDBCard;
        }
        public void Set_RRN(string RRN)
        {
            this.RRN = RRN;
        }
        public string Get_RRN()
        {
            return this.RRN;
        }
        public void Set_Response(IRestResponse response)
        {
            this.response = response;
        }
        public IRestResponse Get_Response()
        {
            return this.response;
        }
        public void Set_Api_Body(string body)
        {
            Api_Body = body;
        }
        public string Get_Api_body()
        {
            return this.Api_Body;
        }

        public void Set_Api_header(string[] header)
        {
            Api_Header = new string[header.Length];
            Api_Header = header;
        }
        public string[] Get_Api_header()
        {
           return this.Api_Header;
        }


        public void SetApiConfiguration(ApiConfiguration apiConfiguration)
        {
            this.apiConfiguration = apiConfiguration;
        }

        public ApiConfiguration GetApiConfiguration()
        {
            return apiConfiguration;
        }
        public void SetQueryParam(string query)
        {
            QueryParam = query;
        }
        public string GetQueryParam()
        {
            return QueryParam;
        }

        public string GetEndPoint()
        {
            return EndPoint;
        }
        public void SetEndPoint(string endPoint)
        {
            EndPoint = endPoint;
        }
        public void SetTranFromDateFlag(bool tran_from_date_flag)
        {
            this.tran_from_date_flag = tran_from_date_flag;
        }
        public bool GetTranFromDateFlag()
        {
            return tran_from_date_flag;
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
        public void Set_acc_balances(Dictionary<string, string> acc_info)
        {
            this.acc_info = acc_info;
        }
        public Dictionary<string,string> Get_acc_balance()
        {
            return this.acc_info;

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
        

        //public string this[string key]   ivr_req

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
