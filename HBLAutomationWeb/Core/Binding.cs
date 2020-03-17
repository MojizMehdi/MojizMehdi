using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using HBLAutomationWeb.Beans;
using HBLAutomationWeb.Common;
using HBLAutomationWeb.XML.apiconfiguration;
using HBLAutomationWeb.XML.ElementFactory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TechTalk.SpecFlow;


namespace HBLAutomationWeb.Core
{
    [Binding]
    public sealed class Binding
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static KlovReporter klov;

        private readonly IObjectContainer _objectContainer;
        public Binding(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [StepArgumentTransformation]
        public string[] transformToArrayOfString(string values)
        {
            string sourceString = values;
            string[] stringSeparators = new string[] { "," };
            return sourceString.Split(stringSeparators, StringSplitOptions.None);
        }

        [BeforeTestRun]
        public static void beforeTestRun()
        {
            var htmlReporter = new ExtentHtmlReporter(@"D:\Automation\Automation_Report-" + DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + ".html");
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            extent = new ExtentReports();



            extent.AttachReporter(htmlReporter);
            ///////////////////////////////////////////////////////


            if (File.Exists("D:/Automation/list.csv"))

            {

                StreamWriter sw = File.CreateText("D:/Automation/list.csv");
                ContextPage.GetInstance().setStreamWriter(sw);
            }
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            try
            {
                if (Common.Configuration.GetInstance().GetByKey("ConnectionString_DIGITAL_CHANNEL_SEC") != null && Common.Configuration.GetInstance().GetByKey("ProviderName") != null)
                {
                    connectionStringsSection.ConnectionStrings["DIGITAL_CHANNEL_SEC"].ConnectionString = Common.Configuration.GetInstance().GetByKey("ConnectionString_DIGITAL_CHANNEL_SEC");
                    connectionStringsSection.ConnectionStrings["DIGITAL_CHANNEL_SEC"].ProviderName = Common.Configuration.GetInstance().GetByKey("ProviderName");
                    config.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");
                }
                if (Common.Configuration.GetInstance().GetByKey("ConnectionString_QAT_BPS") != null && Common.Configuration.GetInstance().GetByKey("ProviderName") != null)
                {
                    connectionStringsSection.ConnectionStrings["QAT_BPS"].ConnectionString = Common.Configuration.GetInstance().GetByKey("ConnectionString_QAT_BPS");
                    connectionStringsSection.ConnectionStrings["QAT_BPS"].ProviderName = Common.Configuration.GetInstance().GetByKey("ProviderName");
                    config.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");
                }

            }
            catch (Exception ex)
            {
            }


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            ElementFactory elementFactory;
            string pathOfElementXML = PathFinder.GetPath();
            pathOfElementXML += "\\HBLAutomationWeb\\Resources\\ElementXML\\ElementFactory.xml";
            XmlSerializer serializer1 = new XmlSerializer(typeof(ElementFactory));
            XmlTextReader xmlReader1 = new XmlTextReader(pathOfElementXML);
            elementFactory = (ElementFactory)serializer1.Deserialize(xmlReader1);
            ContextPage.GetInstance().SetElement(elementFactory);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }


        [BeforeScenario]
        public static void beforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }
        [BeforeStep]
        public void BeforeStep()
        {


        }
        [AfterScenario]
        public void AfterScenario()
        {
            ExcelRecord rec = ContextPage.GetInstance().GetExcelRecord();
            if (!(rec == null))
            {
                if (ScenarioContext.Current.TestError == null && (rec.ExpectedResult == null && rec.ActualResult == null))
                {
                    rec.Result = "PASS";
                }
                if (rec.ExpectedResult == null && rec.ActualResult == null && ScenarioContext.Current.TestError != null)
                {
                    rec.Result = "FAIL";
                    string error = ScenarioContext.Current.TestError.Message;
                    error = error.Replace(",", " ");
                    error = error.Replace("\n", " ");
                    error = error.Replace("\r\n", " ");
                    error = error.Replace("\r", " ");
                    rec.ErrorMessage = error;
                }
                if (rec.ExpectedResult != null && rec.ActualResult != null)
                {
                    if (rec.ExpectedResult.Equals(rec.ActualResult) && ScenarioContext.Current.TestError == null)
                    {
                        rec.Result = "PASS";
                    }
                    else
                    {
                        if (ScenarioContext.Current.TestError != null)

                        {
                            rec.Result = "FAIL";
                            string error = ScenarioContext.Current.TestError.Message;
                            error = error.Replace(",", " ");
                            error = error.Replace("\n", " ");
                            error = error.Replace("\r\n", " ");
                            error = error.Replace("\r", " ");
                            rec.ErrorMessage = error;

                        }
                    }

                }


                ContextPage.GetInstance().SetExcelRecord(rec);
            }
            ExcelWriter.GetInstance().excelRecord.Add(ContextPage.GetInstance().GetExcelRecord());

            //Flyshing Out Contextual Data//
            //ContextPage.GetInstance().ResetPostData();
            //Flyshing Out Contextual Data//

        }

        [AfterTestRun]
        public static void afterTestRun()
        {
            ExcelWriter.GetInstance().WriteOutputFile();
            if (File.Exists("D:/Automation/list.csv"))
            {
                ContextPage.GetInstance().getStreamWriter().Close();
            }
            ExcelWriter.GetInstance().WriteOutputFile();
            extent.Flush();
        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(ScenarioContext.Current, null);
            if (ScenarioContext.Current.TestError == null && (!(TestResult.ToString().Contains("skipped") || TestResult.ToString() == "StepDefinitionPending")))
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            //else if (ScenarioContext.Current.TestError != null && (!(TestResult.ToString().Contains("skipped") || TestResult.ToString() == "StepDefinitionPending")))
            //{
            //    if (stepType == "Given")
            //        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    else if (stepType == "When")
            //        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    else if (stepType == "Then")
            //        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            //    else if (stepType == "And")
            //        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

            //}

            //Pending Status
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "And")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");

            }

            if (TestResult.ToString().Contains("skipped"))
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Skipped because of previous errors");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Skipped because of previous errors");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Skipped because of previous errors");
                else if (stepType == "And")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Skipped because of previous errors");
            }
        }
    }
}
