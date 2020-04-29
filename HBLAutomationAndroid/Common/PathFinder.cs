using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationAndroid.Common
{
    class PathFinder
    {
        public static string GetPath()
        {

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = currentDirectory;
            while (true)
            {
                if (Directory.Exists(path + "\\HBLAutomationAndroid"))
                {
                    return path;
                }
                else
                {
                    path = Directory.GetParent(path).ToString();
                }
            }


        }
    }
}
