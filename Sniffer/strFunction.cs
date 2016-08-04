using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniffer
{
    class StrFunction
    {
        public static bool mystrncmp(byte[] str1, byte[] str2, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (str1[i] == str2[i+6])
                    ;
                else
                    return false;
            }
            return true;
        }
    }
}
