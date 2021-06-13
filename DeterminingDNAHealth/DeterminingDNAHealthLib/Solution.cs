using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using DeterminingDNAHealthLib.AhoCorasick;

namespace DeterminingDNAHealthLib
{

    // https://www.hackerrank.com/challenges/determining-dna-health/problem

    [ObsoleteAttribute("This class is obsolete. Use class SolutionAhoCorasick instead.", false)]
    public class Solution
    {
        /*
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
            long maxHealth = 0;
            long minHealth = long.MaxValue;
            foreach(DNATester tester in testers)
            {
                long health = GetHealth(dna,tester);
                if (health < minHealth) { minHealth = health; }
                if (health > maxHealth) { maxHealth = health; }
            }

            return $"{minHealth} {maxHealth}"; // min max
        }

        private static long GetHealth(DNA dna,DNATester tester)
        {
            long result = 0;
            for (int i = tester.HealthIndexFirst; i <= tester.HealthIndexLast;i++)
            {
                string gene = dna.Genes[i];
                int health = dna.Health[i];

                var indx = GetPositions(tester.DnaToCheck, gene);
                

                long numberOfGenes = GetPositions(tester.DnaToCheck, gene).Count();
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

        */

        public static string GetAndCheckDNAFromFile(string fileName)
        {
            var fileLines = File.ReadAllLines(fileName);

            int n = int.Parse(fileLines[0]);

            List<string> genes = fileLines[1].Split(' ').ToList();
            List<int> health = fileLines[2].Split(' ').
                 ToList().Select(healthTemp => Convert.ToInt32(healthTemp)).ToList();


            long maxHealth = 0;
            long minHealth = long.MaxValue;

            var grouped = genes.Select((v, k) => new { index = k, value = v }).
                GroupBy(x => x.value).
                Select(m => new { key = m.Key, indexes = m.Select(i=>i.index).ToList<int>() });


            Dictionary<string,List<int>> groupedGenes = grouped.ToDictionary(x => x.key,x => x.indexes);

            int s = Convert.ToInt32(fileLines[3]);
            for (int sItr = 4; sItr < 4 + s; sItr++)
            {
                // zrobic to jako klasę i listę 
                string[] firstMultipleInput = fileLines[sItr].Split(' ');

                int first = Convert.ToInt32(firstMultipleInput[0]);

                int last = Convert.ToInt32(firstMultipleInput[1]);

                string d = firstMultipleInput[2];

                long healthValue = GetHealth(groupedGenes, health, d, first, last);

                if (healthValue < minHealth) { minHealth = healthValue; }
                if (healthValue > maxHealth) { maxHealth = healthValue; }
            }

            return $"{minHealth} {maxHealth}";
        }


        private static long GetHealth(Dictionary<string, List<int>> genes, List<int> health, string gene,int first,int last)
        {

            long result = 0;
    
            foreach (var geneValPair in genes.Where(x=>gene.Contains( x.Key)) )
            {

                int numberOccurs = NumberOfOccurs(gene, geneValPair.Key);
                for (int i = 0; i < geneValPair.Value.Count; i++)
                {
                    int idx = geneValPair.Value[i];
                    if (idx >= first && idx <= last)
                    {
                        result += health[idx] * numberOccurs; 
                    }
                }
                
            }

            return result;
        }

        static int NumberOfOccurs(string source, string searchString)
        {

            int result = 0;
            try
            {
                int start = source.IndexOf(searchString[0]);
                int stop = source.LastIndexOf(searchString[0]);
                stop = (((stop + searchString.Length) > source.Length) ? (source.Length - searchString.Length) : stop) + 1;

                for (int i = start; i <  stop ; i++)

                {
                    if (source.Substring(i, searchString.Length)
                            .Equals(searchString))
                    {
                        result++;
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

          
            
            return result;
        }


        public static string GetAndCheckDNAFromFileAhoCorasick(string fileName)
        {
            var fileLines = File.ReadAllLines(fileName);

            int n = int.Parse(fileLines[0]);

            List<string> genes = fileLines[1].Split(' ').ToList();
            List<int> health = fileLines[2].Split(' ').
                 ToList().Select(healthTemp => Convert.ToInt32(healthTemp)).ToList();


            long maxHealth = 0;
            long minHealth = long.MaxValue;

            var grouped = genes.Select((v, k) => new { index = k, value = v }).
                GroupBy(x => x.value).
                Select(m => new { key = m.Key, indexes = m.Select(i => i.index).ToList<int>() });


            Dictionary<string, List<int>> groupedGenes = grouped.ToDictionary(x => x.key, x => x.indexes);

            /*
            Trie trie = new Trie();
            foreach(var f in grouped)
            {
                trie.Add(f.key);
            }
            trie.Build();
            */

            int s = Convert.ToInt32(fileLines[3]);
            for (int sItr = 4; sItr < 4 + s; sItr++)
            {
                // zrobic to jako klasę i listę 
                string[] firstMultipleInput = fileLines[sItr].Split(' ');

                int first = Convert.ToInt32(firstMultipleInput[0]);

                int last = Convert.ToInt32(firstMultipleInput[1]);

                string d = firstMultipleInput[2];

                long healthValue = GetHealth(groupedGenes, health, d, first, last, null);

                if (healthValue < minHealth) { minHealth = healthValue; }
                if (healthValue > maxHealth) { maxHealth = healthValue; }
            }

            return $"{minHealth} {maxHealth}";
        }

        private static long GetHealth(Dictionary<string, List<int>> genes, List<int> health,
            string gene, int first, int last,Trie trie_)
        {

            long result = 0;

            foreach (var geneValPair in genes.Where(x => gene.Contains(x.Key)))
            {

                Trie trie = new Trie();
                trie.Add(geneValPair.Key);
               
                trie.Build();


                int numberOccurs = trie.Find(gene)/*.Where(x => x == geneValPair.Key)*/.Count();// NumberOfOccurs(gene, geneValPair.Key);
                for (int i = 0; i < geneValPair.Value.Count; i++)
                {
                    int idx = geneValPair.Value[i];
                    if (idx >= first && idx <= last)
                    {
                        result += health[idx] * numberOccurs;
                    }
                }

            }

            return result;
        }


    }

}

/*
          Trie trie = new Trie();
          trie.Add("el");
       //   trie.Add("world");

          // build search tree
          trie.Build();

          string text = "hello and welcome to this beautiful world!";

          // find words
          foreach (string word in trie.Find(text))
          {
              var f = word;
          }
          */