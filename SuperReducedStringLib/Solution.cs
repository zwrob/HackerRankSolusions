using System;
using System.Collections.Generic;
using System.Text;

namespace SuperReducedStringLib
{
    //https://www.hackerrank.com/challenges/reduced-string/problem
    public static class Solution
    {
        public static string superReducedString(string s)
        {
            StringBuilder result = new StringBuilder();
            string check = s;

            do
            {
                result = new StringBuilder();
                for (int i = 0; i < check.Length; i++)
                {
                    if (i < check.Length - 1 && check[i] == check[i + 1]) { i++; }
                    else { result.Append(check[i]); }
                }

                string rs = result.ToString();
                if (string.IsNullOrEmpty(rs) || check == rs) { break; }
                check = rs;

            } while (true);

            return (result.Length == 0) ? "Empty String" : result.ToString();
        }
    }
}
