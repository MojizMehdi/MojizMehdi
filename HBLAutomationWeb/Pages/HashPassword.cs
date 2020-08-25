using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationWeb.Pages
{
    class HashPassword
    {
        public static string PasswordEncryptor(string r_Text)
        {
            return BCrypt.Net.BCrypt.HashString(r_Text);
            //return BCrypt.Net.BCrypt.HashPassword(r_Text, BCrypt.Net.BCrypt.GenerateSalt(31));
        }

        public static bool CompareEncryptedString(string r_PlainText, string r_HashedText)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(r_PlainText, r_HashedText ?? "");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
