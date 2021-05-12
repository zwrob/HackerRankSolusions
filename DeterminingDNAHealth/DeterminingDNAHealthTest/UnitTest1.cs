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

            // przechodzi tylko TestCase 0
            Solution.GetDNAFromFile(Path.Combine(testFilesPath, "InputFileTest1.txt"),
                out DNA dna, out List<DNATester> dnaTesters);

            Assert.IsNotNull(dna);
            Assert.AreNotEqual(0, dnaTesters.Count);

            Assert.AreEqual("0 19", Solution.GetMinMaxHealth(dna,dnaTesters));

        }


        [TestMethod]
        public void TestMethod3()
        {
            // przechodzi Test Case: 0,1,10,11,12
           string result =  Solution.GeAndCheckDNAFromFile((Path.Combine(testFilesPath, "InputFileTest2.txt")));

           Assert.AreEqual("15806635 20688978289",result);

        }
    }
}
