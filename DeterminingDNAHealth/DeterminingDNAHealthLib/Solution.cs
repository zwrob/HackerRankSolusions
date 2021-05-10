using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace DeterminingDNAHealthLib
{

    //https://www.hackerrank.com/challenges/determining-dna-health/problem

    public class Solution
    {
        public static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<string> genes = Console.ReadLine().TrimEnd().Split(' ').ToList();

            List<int> health = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(healthTemp => Convert.ToInt32(healthTemp)).ToList();

            int s = Convert.ToInt32(Console.ReadLine().Trim());

            for (int sItr = 0; sItr < s; sItr++)
            {
                string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

                int first = Convert.ToInt32(firstMultipleInput[0]);

                int last = Convert.ToInt32(firstMultipleInput[1]);

                string d = firstMultipleInput[2];
            }
        }

        public static string GetMinMaxHealth(DNA dna, List<DNATester> testers)
        {
            int maxHealth = 0;
            int minHealth = int.MaxValue;
            foreach(DNATester tester in testers)
            {
                int health = GetHealth(dna,tester);
                if (health < minHealth) { minHealth = health; }
                if (health > maxHealth) { maxHealth = health; }
            }

            return $"{minHealth} {maxHealth}"; // min max
        }

        private static int GetHealth(DNA dna,DNATester tester)
        {
            int result = 0;
            for (int i = tester.HealthIndexFirst; i <= tester.HealthIndexLast;i++)
            {
                string gene = dna.Genes[i];
                int health = dna.Health[i];

                int numberOfGenes = GetPositions(tester.DnaToCheck, gene).Count();
                result += numberOfGenes * health;
            }

            return result;
        }



        public static List<int> GetPositions( string source, string searchString)
        {
            List<int> ret = new List<int>();
            int len = searchString.Length;
            int start = -1;
            while (true)
            {
                start = source.IndexOf(searchString, start + 1);
                if (start == -1)
                {
                    break;
                }
                else
                {
                    ret.Add(start);
                }
            }
            return ret;
        }


        public static bool GetDNAFromFile(string fileName, out DNA dna, out List<DNATester> dnaTesters)
        {
            var fileLines = File.ReadAllLines(fileName);

            int n = int.Parse(fileLines[0]);

            List<string> genes = fileLines[1].Split(' ').ToList();
            List<int> health = fileLines[2].Split(' ').
                 ToList().Select(healthTemp => Convert.ToInt32(healthTemp)).ToList();

            dna = new DNA(genes, health);

            dnaTesters = new List<DNATester>();
            int s = Convert.ToInt32(fileLines[3]);
            for (int sItr = 4; sItr < 4 + s; sItr++)
            {
                // zrobic to jako klasę i listę 
                string[] firstMultipleInput = fileLines[sItr].Split(' ');

                int first = Convert.ToInt32(firstMultipleInput[0]);

                int last = Convert.ToInt32(firstMultipleInput[1]);

                string d = firstMultipleInput[2];

                dnaTesters.Add(new DNATester(d, first, last));
            }

            return true;
        }


    }

}
