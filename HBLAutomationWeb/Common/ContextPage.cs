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
        string Acc_Statement_Days;
        string Bill_Status;
        string Company_Code;
        ////public string ENTITY_NAME = null;
        ////public string ACCOUNT_NO = null;
        ////public string EndPoint = null;
        ////public string QueryParam = null;
        ////public string ResourceID = null;
        ////public string ContentType = null;
        ////public string ProductID = null;
        ////public string Title = null;
        ////public int AliasID = 0;
        ////public string Alias = null;
        ////public int WalletStatus = 0;
        ////public string Subscriptiontype = null;
        ////public string UserID = null;
        ////public string ExternalToken = null;
        ////public string Body = null;
        ////public string FileName = null;
        ////public string FilePath = null;
        ////public string ContentMessage = null;
        ////public string productCode = null;
        ////public string CardID = null;
        ////public string Relationship_ID = null;
        ////public string Customer_ID = null;
        ////public string Customer_CNIC = null;
        ////public string ProductName = null;
        ////public string FEE_CODE = null;
        ////public string CloneName = null;
        ////public string OTP = null;
        ////public string NationalId = null;
        ////public int STAN;
        ////public string RequestId = null;
        ////public string Token = null;
        ////public string Document_ID = null;

        ////public string voucherNumber = null;
        ////public string apiContest = null;
        public string CutOvertime = null;
        public string Date = null;

        ////public string CustomerId = null;
        ////public string CustomerNIC_Recard = null;
        ////public string Employee_CNIC = null;
        ////public string calculatedHash = null;

        ////public string Merchantid = null;
        ////public string handlerId = null;
        ////public string clientID = null;
        ////public string consumerID = null;
        ////public string channelID = null;
        ////public string count = null;
        ////public string DeviceID = null;
        ////public string Installation_id = null;
        ////public string DateInternationalTran = null;
        ////public string TimeInternationalTran = null;
        ////public string KeyError = null;
        //public int ErrorCount = 0;
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
        //public void SetErrorCountValue(int ErrorCountvalue)
        //{
        //    ErrorCount = ErrorCountvalue;
        //}

        //public int GetErrorCountValue()
        //{
        //    return ErrorCount;
        //}

        //public void SetKeyErrorValue(string error)
        //{
        //    KeyError = error;
        //}
        //public string GetKeyErrorValue()
        //{
        //    return KeyError;
        //}

        //public void SetClientID(string clientIDD)
        //{
        //    clientIDD = clientID;
        //}

        //public string GetClientID()
        //{
        //    return clientID;
        //}



        //public void setDeviceID(string deviceId)
        //{
        //    DeviceID = deviceId;
        //}

        //public string getDeviceID()
        //{
        //    return DeviceID;
        //}
        //public void setxconsumerID(string consumerIDD)
        //{
        //    consumerIDD = consumerID;
        //}
        //public string getConsumerID()
        //{
        //    return consumerID;
        //}

        //public string GetChannelID()
        //{
        //    return consumerID;
        //}
        //public string cardAcceptorTermId = null;
        //public string cardAcceptorIdCode = null;




        //public int t_log_id;

        public static IWebDriver driver;

        //public ApiConfiguration apiConfiguration;
        public ElementFactory elementFactory;
        List<Element> Elements = new List<Element>();





        //List<KeyValuePair<string, string>> Tokens = new List<KeyValuePair<string, string>>();

        ExcelRecord excelRecord { get; set; }

        public ExcelRecord GetExcelRecord()
        {
            return excelRecord;

        }

        public void SetExcelRecord(ExcelRecord record)
        {
            excelRecord = record;
        }



        //public string GetAPIContext()
        //{
        //    try
        //    {
        //        return apiContest;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AssertFailedException("Something went wrong with the Framework");
        //    }
        //}
        //public void setAPIContext(string file)
        //{
        //    if (file == null)
        //        throw new System.ArgumentNullException("API Context is null.");
        //    this.apiContest = file;
        //}



        //public void generateHASH(string hashvalues)
        //{
        //    SHA256 sha256Enc = SHA256.Create();
        //    byte[] bytes = Encoding.UTF8.GetBytes(hashvalues);
        //    byte[] hashKey = sha256Enc.ComputeHash(bytes);
        //    StringBuilder hashVal = new StringBuilder();
        //    for (int i = 0; i < hashKey.Length; i++)
        //    {
        //        hashVal.Append(hashKey[i].ToString("x2"));
        //    }
        //    string hash = hashVal.ToString().ToLower();
        //    calculatedHash = hash;
        //}
        //public string getCalculatedHash()
        //{
        //    return calculatedHash;
        //}


        //public void SetTokens(List<KeyValuePair<string, string>> tokens)
        //{
        //    Tokens = tokens;
        //}


        //public void SETStan(int stan)
        //{


        //    STAN = stan + 1;
        //}
        //public int GetStan()
        //{
        //    return STAN;
        //}

        //public void SetTlogId(int t_log)
        //{


        //    t_log_id = t_log;
        //}
        //public int GeTlogId()
        //{
        //    return t_log_id;
        //}

        //public void SETVoucher(string voucher)
        //{


        //    voucherNumber = voucher;
        //}
        //public string GetVoucherNumber()
        //{
        //    return voucherNumber;
        //}


        //public void SETCardAcceptorTermId(string TermId)
        //{


        //    cardAcceptorTermId = TermId;
        //}
        //public string GetCardAcceptorTermId()
        //{
        //    return cardAcceptorTermId;
        //}



        //public void SETCardAcceptorIdCode(string idCode)
        //{


        //    cardAcceptorIdCode = idCode;
        //}
        //public string GetCardAcceptorIdCode()
        //{
        //    return cardAcceptorIdCode;
        //}













        //public void SETDocId(string docId)
        //{


        //    Document_ID = docId;
        //}
        //public string GetDocId()
        //{
        //    return Document_ID;
        //}
        //public string GetReqId()
        //{
        //    return RequestId;
        //}
        //public void SETReqId(string reqId)
        //{
        //    RequestId = reqId;
        //}


        //public string GetMerchantId()
        //{
        //    return Merchantid;
        //}
        //public void SETMerchantId(string MerchantId)
        //{
        //    Merchantid = MerchantId;
        //}



        //public string GetHandlerId()
        //{
        //    return handlerId;
        //}
        //public void SETHandlerId(string handlerId)
        //{
        //    this.handlerId = handlerId;
        //}
        //public string GetTokens(string key)
        //{
        //    foreach (var keyValue in Tokens)
        //    {
        //        if (keyValue.Key.Equals(key))
        //        {
        //            return keyValue.Value;
        //        }
        //    }
        //    throw new Exception("Failed to generate token from Kong Server.");
        //}

        //// public void SETOTP(string otp)
        //// {
        ////     OTP = otp;
        //// }


        //// public string Getotp()
        //// {
        ////     return OTP;
        //// }
        //public string GetToken()
        //{
        //    return Token;
        //}
        //public void SetToken(string token)
        //{
        //    Token = token;
        //}


        //public void SetApiConfiguration(ApiConfiguration apiConfiguration)
        //{
        //    this.apiConfiguration = apiConfiguration;
        //}

        //public ApiConfiguration GetApiConfiguration()
        //{
        //    return apiConfiguration;
        //}

        //public void SetCNIC(string cnic)
        //{
        //    Customer_CNIC = cnic;
        //}

        //public string GetCNIC()
        //{
        //    return Customer_CNIC;
        //}
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
