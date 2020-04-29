using HBLAutomationAndroid.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationAndroid.Beans
{
    class ExcelWriter
    {
        static ExcelWriter Instance;
        public static ExcelWriter GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ExcelWriter();
                // Instance.Init();
            }
            return Instance;
        }
        public void WriteOutputFile()
        {
            string buildId = Configuration.GetInstance().GetByKey("buildIdCustom");
            string fileName = Configuration.GetInstance().GetByKey("ExcelOutputFileName").ToString() + "Execution_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string savelocation = Configuration.GetInstance().GetByKey("ExcelOutputFilePath").ToString() + DateTime.Now.ToString("yyyyMMdd") + "/";

            savelocation = savelocation.Replace("{buildIdCustom}", buildId);
            if (!Directory.Exists(savelocation))
            {
                Directory.CreateDirectory(savelocation);
            }

            using (var w = new StreamWriter(savelocation + fileName + ".csv"))
            {
                var header = string.Format("{0},{1},{2},{3}", "Feature", "Scenario", "Status", "Error Message");
                w.WriteLine(header);
                w.Flush();




                //for(int i=1;i<excelRecord.Count;i++)
                foreach (ExcelRecord rec in excelRecord)
                {
                    if (!(rec == null))
                    {
                        //var line = string.Format("{0},{1},{2},{3},{4}", rec.FeatureName, rec.ScenarioName, "'" + rec.STAN, "'" + rec.ExpectedResult, "'" + rec.ActualResult);
                        var line = string.Format("{0},{1},{2},{3}", rec.FeatureName, rec.ScenarioName, rec.Result, rec.ErrorMessage);
                        w.WriteLine(line);
                        w.Flush();
                    }
                }
            }
        }




        public List<ExcelRecord> excelRecord = new List<ExcelRecord>();
    }
}
