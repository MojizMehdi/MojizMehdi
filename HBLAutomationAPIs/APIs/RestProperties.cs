using HBLAutomationAPIs.Common;
using HBLAutomationAPIs.XML.apiconfiguration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HBLAutomationAPIs.APIs
{
    public class RestProperties
    {
        //ContextPage context = ContextPage.GetInstance();
        //ContextPage context = new ContextPage();
        /// <summary>
        /// Initiates API Request with Get Method
        /// </summary>
        /// <param name="token">Keyword of your token In apiconfiguration.xml</param>
        /// <returns>
        /// Response of API in IRestResponse
        /// </returns>
        /// 
        public IRestResponse CallPostAPIRequest()
        {
            try
            {

                var client = new RestClient(Configuration.GetInstance().GetByKey("BaseUri"));
                var request = new RestRequest(ContextPage.GetInstance().GetEndPoint() + ContextPage.GetInstance().GetQueryParam(), Method.POST);
                string[] header = ContextPage.GetInstance().Get_Api_header();
                foreach (var param in header)
                {
                    string[] parameter = param.Split(':');
                    request.AddHeader(parameter[0], parameter[1]);
                }
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Application/Json", ContextPage.GetInstance().Get_Api_body(), ParameterType.RequestBody);
               //request.AddParameter(parameter[1].ToString(), parameter[0], ParameterType.RequestBody);

                return client.Execute(request);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurred", ex);
            }

        }
        public IRestResponse CallGetAPIRequest()
        {
            try
            {
                
                var client = new RestClient(Configuration.GetInstance().GetByKey("BaseUri"));
                var request = new RestRequest(ContextPage.GetInstance().GetEndPoint() + ContextPage.GetInstance().GetQueryParam(), Method.GET);
                string[] header = ContextPage.GetInstance().Get_Api_header();
                foreach (var param in header)
                {
                    string[] parameter = param.Split(':');
                    request.AddHeader(parameter[0], parameter[1]);
                }

                return client.Execute(request);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurred", ex);
            }

        }

        /// <summary>
        /// Initiates API Request with Post Method
        /// </summary>
        /// <param name="token">Keyword of your token In apiconfiguration.xml</param>
        /// <returns>
        /// Response of API in IRestResponse
        /// </returns>
        //public IRestResponse CallPostAPIRequest(string token)
        //{
        //    try
        //    {
        //        ApiConfiguration apiConfiguration = ContextPage.GetInstance().GetApiConfiguration();
        //        RestObject apiConfiguration_restObject = apiConfiguration.RestObject.First(t => t.Keyword.Equals(token));

        //        var client = new RestClient(apiConfiguration_restObject.BaseURI);
        //        var request = new RestRequest(apiConfiguration_restObject.Authorization.Alias + ContextPage.GetInstance().GetEndPoint() + ContextPage.GetInstance().GetQueryParam(), Method.POST);

        //        foreach (var param in apiConfiguration_restObject.Authorization.Request.Header.Param)
        //        {
        //            if (param.Value.Contains("{token}"))
        //            {
        //                param.Value = param.Value.Replace("{token}", ContextPage.GetInstance().GetTokens(token));
        //            }
        //            request.AddHeader(param.Key, param.Value);
        //        }

        //        if(apiConfiguration_restObject.Authorization.Request.Header.Param.First(p=>p.Key == "Content-Type").Value.Equals("application/json"))
        //        {
        //            request.AddParameter("Application/Json", ContextPage.GetInstance().GetBody(), ParameterType.RequestBody);
        //        }
        //        else
        //        {

        //            if (ContextPage.GetInstance().getFormBody() != null)
        //            {
        //                foreach (var param in ContextPage.GetInstance().getFormBody())
        //                {
        //                    request.AddParameter(param.Key, param.Value, ParameterType.RequestBody);
        //                }
        //            }

        //            if (ContextPage.GetInstance().getFiles() != null)
        //            {
        //                foreach (var param in ContextPage.GetInstance().getFiles())
        //                {
        //                    request.AddFile(param.Key, param.Value);
        //                }
        //            }
        //        }
                

        //        return client.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception occurred", ex);
        //    }
        //}

        ///// <summary>
        ///// Initiates API Request with Delete Method
        ///// </summary>
        ///// <param name="token">Keyword of your token In apiconfiguration.xml</param>
        ///// <returns>
        ///// Response of API in IRestResponse
        ///// </returns>
        //public IRestResponse CallDeleteAPIRequest(string token)
        //{
        //    try
        //    {
        //        ApiConfiguration apiConfiguration = ContextPage.GetInstance().GetApiConfiguration();
        //        RestObject apiConfiguration_restObject = apiConfiguration.RestObject.First(t => t.Keyword.Equals(token));

        //        var client = new RestClient(apiConfiguration_restObject.BaseURI);
        //        var request = new RestRequest(apiConfiguration_restObject.Authorization.Alias + ContextPage.GetInstance().GetEndPoint() + ContextPage.GetInstance().GetQueryParam(), Method.DELETE);

        //        foreach (var param in apiConfiguration_restObject.Authorization.Request.Header.Param)
        //        {
        //            if (param.Value.Contains("{token}"))
        //            {
        //                param.Value = param.Value.Replace("{token}", ContextPage.GetInstance().GetTokens(token));
        //            }
        //            request.AddHeader(param.Key, param.Value);
        //        }
        //        request.AddParameter("Application/Json", ContextPage.GetInstance().GetBody(), ParameterType.RequestBody);

        //        return client.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception occurred", ex);
        //    }
        //}

        ///// <summary>
        ///// Initiates API Request with Put Method
        ///// </summary>
        ///// <param name="token">Keyword of your token In apiconfiguration.xml</param>
        ///// <returns>
        ///// Response of API in IRestResponse
        ///// </returns>
        //public IRestResponse CallPutAPIRequest(string token)
        //{
        //    try
        //    {
        //        ApiConfiguration apiConfiguration = ContextPage.GetInstance().GetApiConfiguration();
        //        RestObject apiConfiguration_restObject = apiConfiguration.RestObject.First(t => t.Keyword.Equals(token));
        //        var client = new RestClient(apiConfiguration_restObject.BaseURI);
        //        var request = new RestRequest(apiConfiguration_restObject.Authorization.Alias + ContextPage.GetInstance().GetEndPoint() + ContextPage.GetInstance().GetQueryParam(), Method.PUT);

        //        foreach (var param in apiConfiguration_restObject.Authorization.Request.Header.Param)
        //        {
        //            if (param.Value.Contains("{token}"))
        //            {
        //                param.Value = param.Value.Replace("{token}", ContextPage.GetInstance().GetTokens(token));
        //            }
        //            request.AddHeader(param.Key, param.Value);
        //        }
        //        request.AddParameter("Application/Json", ContextPage.GetInstance().GetBody(), ParameterType.RequestBody);

        //        return client.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception occurred", ex);
        //    }
        //}

        ///// <summary>
        ///// Initiates API Request with Patch Method
        ///// </summary>
        ///// <param name="token">Keyword of your token In apiconfiguration.xml</param>
        ///// <returns>
        ///// Response of API in IRestResponse
        ///// </returns>
        //public IRestResponse CallPatchAPIRequest(string token)
        //{
        //    try
        //    {
        //        ApiConfiguration apiConfiguration = ContextPage.GetInstance().GetApiConfiguration();
        //        RestObject apiConfiguration_restObject = apiConfiguration.RestObject.First(t => t.Keyword.Equals(token));
        //        var client = new RestClient(apiConfiguration_restObject.BaseURI);
        //        var request = new RestRequest(apiConfiguration_restObject.Authorization.Alias + ContextPage.GetInstance().GetEndPoint() + ContextPage.GetInstance().GetQueryParam(), Method.PATCH);
        //        foreach (var param in apiConfiguration_restObject.Authorization.Request.Header.Param)
        //        {
        //            if (param.Value.Contains("{token}"))
        //            {
        //                param.Value = param.Value.Replace("{token}", ContextPage.GetInstance().GetTokens(token));
        //            }
        //            request.AddHeader(param.Key, param.Value);
        //        }
        //        request.AddParameter("Application/Json", ContextPage.GetInstance().GetBody(), ParameterType.RequestBody);

        //        return client.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception occurred", ex);
        //    }
        //}

        ///// <summary>
        ///// Initiates API Request with Post Method (only for KONG Use)
        ///// </summary>
        ///// <param name="token">Keyword of your token In apiconfiguration.xml</param>
        ///// <returns>
        ///// Response of API in IRestResponse
        ///// </returns>
        //public IRestResponse CallPostAPIRequestForKongAuth(string baseURI, string extendedURI, List<Param> HeaderParam, List<Param> BodyParam)
        //{
        //    try
        //    {
        //        var client = new RestClient(baseURI);
        //        var request = new RestRequest(extendedURI, Method.POST);
        //        foreach (var param in HeaderParam)
        //        {
        //            request.AddHeader(param.Key, param.Value);
        //        }
        //        foreach (var param in BodyParam)
        //        {
        //            request.AddParameter(param.Key, param.Value);
        //        }
        //        return client.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception occurred", ex);
        //    }


        //}

    }
}
