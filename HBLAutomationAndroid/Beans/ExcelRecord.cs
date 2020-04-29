using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationAndroid.Beans
{
    class ExcelRecord
    {
        public string FeatureName { get; set; }
        public string ScenarioName { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public string TestcaseName { get; set; }
    }
}
