using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DeterminingDNAHealthLib;
using System.Collections.Generic;

namespace DeterminingDNAHealthTest
{
    //https://www.hackerrank.com/challenges/determining-dna-health/problem

    [TestClass]
    public class UnitTest1
    {
        readonly string testFilesPath = @"../../../TestFiles/";

        [TestMethod]
        public void TestMethod1()
        {
            Solution.GetDNAFromFile(Path.Combine(testFilesPath, "InputFileTest1.txt"),
                out DNA dna, out List<DNATester> dnaTesters);

            Assert.IsNotNull(dna);
            Assert.AreNotEqual(0, dnaTesters.Count);

            Assert.AreEqual("0 19", Solution.GetMinMaxHealth(dna,dnaTesters));

        }
    }
}
