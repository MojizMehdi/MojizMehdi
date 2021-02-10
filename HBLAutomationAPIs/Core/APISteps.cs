using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using HBLAutomationAPIs.APIs;
using HBLAutomationAPIs.Beans;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using HBLAutomationAPIs.Common;
//using HBLAutomationAPIs.Model;

namespace HBLAutomationAPIs.Core
{
    [Binding]
    public class APISteps
    {
        private ContextPage context = ContextPage.GetInstance();
        //private ExcelRecord record = ContextPage.GetInstance().GetExcelRecord();
        [Given(@"the test case title is ""(.*)""")]
        public void GivenTheTestCaseTitleIs(string p0)
        {
            ExcelRecord record = new ExcelRecord();
            record.FeatureName = FeatureContext.Current.FeatureInfo.Title;
            record.ScenarioName = ScenarioContext.Current.ScenarioInfo.Title + p0;
            ContextPage.GetInstance().SetExcelRecord(record);
        }
       
        [When(@"the endpoint is ""(.*)""")]
        [Given(@"the endpoint is ""(.*)""")]
        public void WhenTheEndpointIs(string EndPoint)
        {
            //EndPoint = EndPoint;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            context.SetEndPoint(EndPoint);
        }
        [Given(@"the base uri is ""(.*)""")]
        public void GivenTheBaseUriIs(string baseuri)
        {
            context.Set_baseuri(baseuri);
        }

        [When(@"the queryparameter is ""(.*)""")]
        public void WhenTheQueryParameterIs(string query)
        {
            try
            {
                //query = (query);
                context.SetQueryParam(query);
            }
            catch (Exception ex)
            {
                throw new AssertFailedException(ex.Message);
            }
        }
        [When(@"the API header is ""(.*)""")]
        public void WhenTheAPIHeaderIs(string header)
        {
            string[] api_header = header.Split(',');
            for (int i = 0; i < api_header.Length; i++)
            {
                if (api_header[i].Contains("x-req-id"))
                {
                    string RRN = ContextPage.GetInstance().Get_RRN();
                    string res = (Convert.ToDouble(RRN) + 1).ToString();
                    string act_res = "";
                    int count = 0;
                    while (act_res.Length != 12)
                    {
                        if (count == (12 - res.Length))
                        {
                            act_res += res;
                        }
                        else
                        {
                            act_res += "0";
                        }
                        count++;
                    }
                    api_header[i] = "x-req-id:" + act_res;
                    ContextPage.GetInstance().Set_RRN(act_res);
                    break; 
                }
            }
            context.Set_Api_header(api_header);
        }
        [When(@"Get request is made")]
        public void WhenGetRequestIsMade()
        {
            ExcelRecord rec = ContextPage.GetInstance().GetExcelRecord();
            RestProperties rest_prop = new RestProperties();
            IRestResponse response = rest_prop.CallGetAPIRequest();
            context.Set_Response(response);
            string ali = response.Content.ToString();
            string replacement = Regex.Replace(ali, "\"|{|}", "");
            replacement = replacement.Replace(",", "   ");
            rec.Response = replacement;
            ContextPage.GetInstance().SetExcelRecord(rec);
        }
        [When(@"Post request is made")]
        public void WhenPostRequestIsMade()
        {
            ExcelRecord rec = ContextPage.GetInstance().GetExcelRecord();
            RestProperties rest_prop = new RestProperties();
            string format = context.Get_api_format();
            IRestResponse response = rest_prop.CallPostAPIRequest(format);
            int code = (int)response.StatusCode;
            context.Set_Response(response);
            string replacement = response.Content.ToString();
            if (context.Get_api_format() == "xml")
            {
                replacement = replacement.Replace("\n", "");
            }
            if (context.Get_api_format() == "json")
            {
                replacement = Regex.Replace(replacement, "\"|{|}", "");
                replacement = replacement.Replace(",", "   ");
            }
            rec.Response = replacement;
            ContextPage.GetInstance().SetExcelRecord(rec);
        }
        [When(@"the body is ""(.*)""")]
        public void WhenTheBodyIs(string body)
        {
            if (body.Contains("{RRN}"))
            {
                string RRN = ContextPage.GetInstance().Get_RRN_FetchDBCard();
                string res = (Convert.ToDouble(RRN) + 1).ToString();
                string act_res = "MB_";
                int count = 0;
                while (act_res.Length != 20)
                {
                    if (count == (17 - res.Length))
                    {
                        act_res += res;
                    }
                    else
                    {
                        act_res += "0";
                    }
                    count++;
                }
                body = body.Replace("{RRN}", act_res);
                act_res = act_res.Replace("MB_", string.Empty);
                ContextPage.GetInstance().Set_RRN_FetchDBCard(act_res);
            }
            if (body.Contains("{CNIC}"))
            {
                body = body.Replace("{CNIC}", context.GetCustomerCNIC());
            }
            context.Set_Api_Body(body);
        }
        [Given(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        [When(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        [Then(@"I set value in context from data ""(.*)"" as ""(.*)""")]
        public void WhenISetValueInContextFromDataAs(string value, string attribute)
        {
            if(attribute == "format")
            {
                context.Set_api_format(value);
            }
            if (attribute == "CNIC")
            {
                context.SetCustomerCNIC(value);
            }
        }
    }
}
