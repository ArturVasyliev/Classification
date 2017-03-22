using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    class Program
    {
        static void Main(string[] args)
        {
            //// K Nearest Neighbor test

            //DataPoint[] points = new DataPoint[]
            //{
            //    new DataPoint(3, 10, 0), new DataPoint(4, 6, 0), new DataPoint(5, 4, 0), new DataPoint(5, 8, 0),
            //    new DataPoint(5, 12, 1), new DataPoint(5, 15, 1), new DataPoint(6, 6, 0), new DataPoint(7, 9, 0),
            //    new DataPoint(7, 15, 1), new DataPoint(8, 7, 0), new DataPoint(8, 12, 1), new DataPoint(9, 17, 1),
            //    new DataPoint(10, 12, 1), new DataPoint(11, 14, 1), new DataPoint(12, 6, 2), new DataPoint(12, 9, 2),
            //    new DataPoint(14, 11, 2), new DataPoint(14, 16, 1), new DataPoint(15, 4, 2), new DataPoint(16, 8, 2)
            //};

            //DataPoint[] inputPoints = new DataPoint[]
            //{
            //    new DataPoint(10, 10, 0), new DataPoint(14, 7, 0), new DataPoint(10, 0, 0), new DataPoint(11, 6, 0)
            //};

            //KNearestNeighbor alg = new KNearestNeighbor(points, 3);
            //alg.Start(inputPoints, 5);
            //Console.Write(alg);

            // Naive Bayes Classifier test

            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("Hello", 4);
            dict.Add("Ge", 1);
            Console.WriteLine(dict.Count);


        }
    }
}
