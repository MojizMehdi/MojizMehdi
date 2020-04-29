using HBLAutomationAndroid.Beans;
using HBLAutomationAndroid.Common;
using HBLAutomationAndroid.Pages;
using HBLAutomationAndroid.XML.ElementFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace HBLAutomationAndroid.Core
{
    [Binding]
    public class LoginSteps
    {
        private ContextPage context = ContextPage.GetInstance();
        [Given(@"the test case title is ""(.*)""")]
        public void GivenTheTestCaseTitleIs(string p0)
        {
            ExcelRecord record = new ExcelRecord();
            record.FeatureName = FeatureContext.Current.FeatureInfo.Title;
            record.ScenarioName = ScenarioContext.Current.ScenarioInfo.Title + p0;
            ContextPage.GetInstance().SetExcelRecord(record);
        }

        [Given(@"the user is arrive to Mobile Banking home page")]
        public void GivenTheUserIsArriveToMobileBankingHomePage()
        {
            try
            {
                ContextPage.driver = DriverFactory.getDriver();
            }
            catch (Exception exception)
            {

                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I have given ""(.*)"" on ""(.*)""")]
        public void WhenIHaveGivenOn(string textboxvalue, string Keyword)
        {
            if (String.IsNullOrEmpty(textboxvalue))
            {
                return;
            }
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                //keyword.Locator used instead od locator
                apmhelper.SetTextBoxValue(textboxvalue, keyword.Locator);

            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I am performing on ""(.*)""")]
        public void WhenIAmPerformingOn(string Keyword)
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                apmhelper.Button(keyword.Locator);
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }

        [When(@"I am clicking on ""(.*)""")]
        public void WhenIAmClickingOn(string Keyword)
        {
            try
            {
                if (!String.IsNullOrEmpty(Keyword))
                {
                    AppiumHelper apmhelper = new AppiumHelper();
                    //apmhelper.checkPageIsReady();
                    Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                    apmhelper.links(keyword.Locator);
                }


            }

            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }


        [Given(@"update the data by query ""(.*)"" on Schema ""(.*)""")]
        public void GivenUpdateTheDataByQueryOnSchema(string query, string schema)
        {
            if (query != "")
            {
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, schema);
                }
                catch (Exception e)
                {
                    throw new AssertFailedException(e.Message);

                }
            }
        }

        [Given(@"update the data by query ""(.*)"" on DIGITAL_CHANNEL_SEC")]
        public void GivenUpdateTheDataByQueryOnDIGITAL_CHANNEL_SEC(string query)
        {
            if (query != "")
            {
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, "DIGITAL_CHANNEL_SEC");
                }
                catch (Exception e)
                {
                    throw new AssertFailedException(e.Message);

                }
            }
        }
        [Given(@"update the data by query ""(.*)"" on QAT_BPS")]
        public void GivenUpdateTheDataByQueryOnQAT_BPS(string query)
        {
            if (query != "")
            {
                try
                {
                    DataAccessComponent.DataAccessLink dlink = new DataAccessComponent.DataAccessLink();
                    dlink.GetNonQueryResult(query, "QAT_BPS");
                }
                catch (Exception e)
                {
                    throw new AssertFailedException(e.Message);

                }
            }

        }



        [When(@"I select ""(.*)"" on ""(.*)""")]
        public void WhenISelectOn(string value, string Keyword)
        {
            if (String.IsNullOrEmpty(value))
            {
                return;
            }
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                //apmhelper.checkPageIsReady();
                Element keyword = ContextPage.GetInstance().GetElement(Keyword);
                apmhelper.combobox(value, keyword.Locator);
                if (Keyword.Equals("SendMoney_Frequency"))
                {
                    context.Setfrequency(value);
                }
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I scroll down")]
        public void WhenIScrollDown()
        {
            try
            {
                AppiumHelper apmhelper = new AppiumHelper();
                apmhelper.scroll_down();
            }
            catch (Exception exception)
            {
                AppiumHelper.TakeScreenshot();
                throw new AssertFailedException(exception.Message);
            }
        }
        [When(@"I wait (.*)")]
        public void WhenIWait(int p0)
        {
            Thread.Sleep(p0);
        }


    }
}
