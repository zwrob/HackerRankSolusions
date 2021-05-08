using ConnectedCellsInAGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnitTestProject1
{
    // Zadanie https://www.hackerrank.com/challenges/connected-cell-in-a-grid/problem

    [TestClass]
    public class UnitTest1
    {
        readonly string testFilesPath = @"../../TestFiles/";

        private List<List<int>> GetTestInputDataMatrix(string fileName)
        {
            List<List<int>> matrix = new List<List<int>>();

            var fileLines = File.ReadAllLines(Path.Combine(testFilesPath, fileName));

            // The first line contains an integer , the number of rows in the matrix.
            // The second line contains an integer , the number of columns in the matrix.
            // Each of the next  lines contains  space - separated integers.

            int n = int.Parse(fileLines[0]);
            int m = int.Parse(fileLines[0]);

            for (int i = 2; i < n+2; i++)
            {
                matrix.Add(fileLines[i].TrimEnd().Split(' ').ToList().
                    Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
            }

            return matrix;
        }

        [TestMethod]
        public void TestMethod1()
        {

            List<List<int>> matrix = GetTestInputDataMatrix("InputFileTest1.txt");

            Assert.AreEqual(5, Result.connectedCell(matrix));
        }

        [TestMethod]
        public void TestMethod2()
        {

            List<List<int>> matrix = GetTestInputDataMatrix("InputFileTest2.txt");

            Assert.AreEqual(5, Result.connectedCell(matrix));
        }

        [TestMethod]
        public void TestMethod3()
        {

            List<List<int>> matrix = GetTestInputDataMatrix("InputFileTest3.txt");

            Assert.AreEqual(9, Result.connectedCell(matrix));
        }

        [TestMethod]
        public void TestMethod4()
        {

            List<List<int>> matrix = GetTestInputDataMatrix("InputFileTest4.txt");

            Assert.AreEqual(29, Result.connectedCell(matrix));
        }
    }
}
