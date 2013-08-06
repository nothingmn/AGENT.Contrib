using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Util
{

    public class Random
    {
        private static System.Random rnd = new System.Random();

        /// <summary>
        /// Return an int greater or equal at min and less than max
        /// </summary>
        public static int Next(int min, int max)
        {

            lock (rnd)
            {
                if (min > max) //if min greater than max we swap them
                {
                    int temp = max;
                    max = min;
                    min = temp;
                }
            
                int num = rnd.Next(max - min);
                return num + min;
            }
        }
    }
}
