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
        //string [,] AccBalance;
        Dictionary<string, string> acc_info;
        Dictionary<string, string> tran_info;



        public string CutOvertime = null;
        public string Date = null;

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

        //public string[,] AccBalances 
        // {
        // get
        // {
        //      return AccBalance;
        //  }
        // set
        //   {
        //      AccBalance = value;
        //  }
        //  }
        //public void SetAccBalance(string [,] arr)
        //{
        //    AccBalance = arr;
        // }

        //public string[,] GetAccBalance()
        //{
        //    return AccBalance;
        //}


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
