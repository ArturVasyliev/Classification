using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    class KNearestNeighbor
    {
        private DataPoint[] learnedPoints;
        private List<DataPoint> data;
        private int learnedLength;
        private int classesCount;

        public KNearestNeighbor(DataPoint [] points, int amountOfClasses)
        {
            Learn(points, amountOfClasses);
        }

        public void Learn(DataPoint[] points, int amountOfClasses)
        {
            learnedPoints = points;
            learnedLength = learnedPoints.Length;
            classesCount = amountOfClasses;
            data = new List<DataPoint>();
        }

        public void Start(DataPoint[] points, int k)
        {
            if (learnedPoints == null || learnedPoints.Length == 0)
            {
                throw new Exception("The algorithm must Learn some data before Start");
            }

            if(k > learnedPoints.Length)
            {
                throw new Exception("K must be lower than amount of learned elements");
            }

            double[][] distAndIndex = new double[learnedLength][];
            for (int i = 0; i < learnedLength; i++)
            {
                distAndIndex[i] = new double[2];
            }

            for (int i = 0; i < points.Length; i++)
            {
                Console.WriteLine("Current point: {0}", points[i]);
                // For every test sample, calculate distance from every training sample
                Parallel.For(0, learnedLength, index =>
                {
                    double dist = Math.Sqrt(Math.Pow(points[i].X - learnedPoints[index].X, 2) + Math.Pow(points[i].Y - learnedPoints[index].Y, 2));
                    // Storing distance as well as index 
                    distAndIndex[index][0] = dist;
                    distAndIndex[index][1] = index;
                });

                // Sort distances and take top K (?What happens in case of multiple points at the same distance?)
                var chosenDistances = distAndIndex.AsParallel().OrderBy(t => t[0]).Take(k);

                Console.WriteLine("Nearest K:");
                foreach (double[] element in chosenDistances)
                {
                    Console.WriteLine("{0} - {1} ; ", learnedPoints[(int)element[1]], element[0]);
                }

                // Get most frequent class for nearest points
                List<double> frequencyVocabulary = new List<double>();
                for (int p = 0; p < classesCount; p++)
                    frequencyVocabulary.Add(0);

                foreach(double[] element in chosenDistances)
                {
                    frequencyVocabulary[learnedPoints[(int)element[1]].Class]++;
                }

                points[i].Class = frequencyVocabulary.IndexOf(frequencyVocabulary.Max());

                Console.WriteLine("{0} in class {1} = {2}%", points[i], points[i].Class, (frequencyVocabulary[points[i].Class]/ chosenDistances.Count())*100.0);

            }

            data.AddRange(points);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(500);
            sb.AppendLine("Learned set: ");
            string[] classes = new string[classesCount];
            foreach (DataPoint dp in learnedPoints)
                classes[dp.Class] += dp.ToString() + " ";
            for(int i = 0; i < classesCount; i++)
            {
                sb.Append(string.Format("Class {0}: ", i));
                sb.AppendLine(classes[i]);
            }
            sb.AppendLine("Data set: ");
            classes = new string[classesCount];
            foreach (DataPoint dp in data)
                classes[dp.Class] += dp.ToString() + " ";
            for (int i = 0; i < classesCount; i++)
            {
                sb.Append(string.Format("Class {0}: ", i));
                sb.AppendLine(classes[i]);
            }

            return sb.ToString();
        }
    }
}
