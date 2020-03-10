using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulationComponents.ExtensionClasses
{
    class commonExtensions
    {
        public static bool IsEqual(int source, int targetr)
        {
            if (source == targetr)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
