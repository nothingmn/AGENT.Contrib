using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Util
{
    
    public class Random
    {
        private static System.Random rnd = new System.Random();

        public static int Next(int min, int max)
        {
            lock (rnd)
            {
                int num = rnd.Next(max);
                while(num < min)
                {
                    num = rnd.Next(max);
                }
                return num;
            }
        }
    }
}
