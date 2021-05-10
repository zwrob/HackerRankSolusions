using System;
using System.Collections.Generic;
using System.Text;

namespace DeterminingDNAHealthLib
{
    public struct DNATester
    {

        public string DnaToCheck { get; }
        public int HealthIndexFirst { get; }
        public int HealthIndexLast { get; }

        public DNATester(string dnaToCheck,int healthIndexFirst,int healthIndexLast )
        {
            DnaToCheck = dnaToCheck;
            HealthIndexFirst = healthIndexFirst;
            HealthIndexLast = healthIndexLast;
        }


    }
}
