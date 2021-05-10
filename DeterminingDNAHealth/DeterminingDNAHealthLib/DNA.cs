using System;
using System.Collections.Generic;
using System.Text;

namespace DeterminingDNAHealthLib
{
    public struct DNA
    {
        public List<string> Genes { get; }
        public List<int> Health { get; }

        public DNA(List<string> genes, List<int> health)
        {
            Genes = genes;
            Health = health;
        }


    }
}
