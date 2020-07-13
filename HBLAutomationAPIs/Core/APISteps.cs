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

    }
}
