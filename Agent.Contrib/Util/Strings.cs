using System;
using System.Text;
using Microsoft.SPOT;

namespace Agent.Contrib.Util
{
    public static class Strings
    {
        /// <summary>
        /// Joins an array of strings into a single string with an optional seperator
        /// </summary>
        /// <param name="array"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string Join(string[] array, char seperator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i]);
                if (!IsNullOrEmpty(seperator) && i < array.Length - 1) sb.Append(seperator);
            }
            return sb.ToString();
        }

        public static bool IsNullOrEmpty(string input)
        {
            return (input == null || input == "");
        }

        public static bool IsNullOrEmpty(char input)
        {
            return (input.ToString() == "");
        }
    }
}