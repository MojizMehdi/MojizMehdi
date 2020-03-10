using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationWeb.Common
{
    public class Configuration
    {
        static Configuration Instance;
        public static Configuration GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Configuration();
                Instance.Init();
            }
            return Instance;
        }

        public void Init()
        {
        }

        public string GetByKey(string key)
        {

            try
            {
                return Environment.GetEnvironmentVariable(key).ToString();

            }
            catch (Exception ex)
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
                }
                catch (Exception e)
                {
                    throw new AssertFailedException(string.Format("Parameter {0} has been not passed from Application config and command line", key.ToString()));
                }
            }


        }

        public string GetHostnameFromURL(string url)
        {
            Uri uri = new Uri(url);
            return uri.Host;

        }
    }
}
