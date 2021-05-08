using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace ConnectedCellsInAGrid
{
    // https://www.hackerrank.com/challenges/connected-cell-in-a-grid/problem
    public class Result
    {

        /*
         * Complete the 'connectedCell' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts 2D_INTEGER_ARRAY matrix as parameter.
         */

        static List<List<int[]>> groups = new List<List<int[]>>();

        public static int connectedCell(List<List<int>> matrix)
        {
            int maxGroupSize = 0;

            for (int r = 0; r < matrix.Count; r++)
            {
                List<int> row = matrix[r];
                
                for (int c = 0; c < row.Count; c++)
                {
                    if (row[c] == 1)
                    {
                        // znajdź grupy sąsiadujące z punktem r,c
                        var groupsIndexes = new List<int>(FindConnectedGroups(r, c, row.Count));

                        switch (groupsIndexes.Count())
                        {
                            case 0:
                                // dodanie nowej grupy
                                groups.Add(new List<int[]>() { new int[2] { r, c } });
                                break;
                            case 1:
                                // dodanie punktu do znalezionej grupy
                                int idx = groupsIndexes.First();
                                groups[idx].Add(new int[2] { r, c });
                                break;
                            default: // > 1
                                List<int[]> newList = new List<int[]>();
                                List<List<int[]>> newGroups = new List<List<int[]>>();
                                

                                for (int i = 0; i < groups.Count; i++)
                                {
                                    if (groupsIndexes.Contains(i)) { newList.AddRange(groups[i]); }
                                    else
                                    {
                                        newGroups.Add(new List<int[]>(groups[i]));
                                    }
                                }
                                newList.Add(new int[2] { r, c });
                                newGroups.Add(newList);
                                groups = newGroups;


                                break;
                        }
                    }
                }
            }

            maxGroupSize = groups.Max(x => x.Count);

            return maxGroupSize;
        }

        private static IEnumerable<int> FindConnectedGroups(int r, int c,int maxC)
        {
            // zwraca idex  z groups dla grup sąsiadujących z punktem
            // należy sprawdzić czy akie punkty są w grupach : r,c-1 ; r-1,c-1 ; r-1,c ; r-1,c+1 ; r,c+1 

            
            List<int[]> pointsToCheck = new List<int[]>()
            {
                new int[2] { r , c - 1 },    // c > 0
                new int[2] { r - 1, c - 1 }, // r > 0 && c > 0
                new int[2] { r - 1, c },     // r > 0
                new int[2] { r - 1, c + 1 }, // r > 0 && c < maxC - 1
                new int[2] { r, c + 1 }      // c < maxC - 1
            };

            var list = groups.Select((v, k) => new { Index = k, Value = v }).
                Where(x => x.Value.Any(y => pointsToCheck.Any(z => z[0]==y[0] && z[1]==y[1])))
                .Select(t =>  t.Index);

            return list;
        }
    }


    public class Solution
    {
        public static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int n = Convert.ToInt32(Console.ReadLine().Trim());

            int m = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<int>> matrix = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
            }

            int result = Result.connectedCell(matrix);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }
}

