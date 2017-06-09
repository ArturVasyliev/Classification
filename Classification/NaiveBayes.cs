using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    class NaiveBayes
    {
        //names of classes
        private string[] classes;

        //amount of text samples in each class
        private int[] sumTextsInClasses;

        //amount of words in each class
        private int[] sumWordsInClasses;

        //frequency dict for each class
        private Dictionary<string, int>[] wordFreqInClasses;

        //amount of unique words in training set
        private int uniqueWords;

        //amount of text samples in training set
        private int countTexts;

        public NaiveBayes(string[] classNames, string[][] classTexts)
        {
            Learn(classNames, classTexts);
        }

        public void Learn(string[] classNames, string[][] classTexts)
        {
            int countClasses = classNames.Length;
            classes = classNames;
            sumTextsInClasses = new int[countClasses];
            sumWordsInClasses = new int[countClasses];
            wordFreqInClasses = new Dictionary<string, int>[countClasses];
            uniqueWords = 0;
            countTexts = 0;
            for (int c = 0; c < countClasses; c++)
            {
                sumTextsInClasses[c] = classTexts[c].Length;
                countTexts += classTexts[c].Length;
                wordFreqInClasses[c] = new Dictionary<string, int>();
                for(int i = 0; i < classTexts[c].Length; i++)
                {
                    string[] words = classTexts[c][i].Split(' ');
                    sumWordsInClasses[c] += words.Length;
                    for(int w = 0; w < words.Length; w++)
                    {
                        if (wordFreqInClasses[c].ContainsKey(words[w]))
                        {
                            wordFreqInClasses[c][words[w]] += 1;
                        }
                        else
                        {
                            wordFreqInClasses[c].Add(words[w], 1);
                        }
                    }
                }
                uniqueWords += wordFreqInClasses[c].Count;
            }
        }

        public void Start(string text)
        {
            int resultClass = 0;
            double currentClassScore = 0;
            double[] scoresOfClasses = new double[classes.Length];
            double maxClassScore = double.MinValue;
            string[] words = text.Split(' ');
            Console.WriteLine("NB Started");
            Console.WriteLine("Text = '{0}'", text);
            //count a probability of being in class c for each class
            for (int c = 0; c < classes.Length; c++)
            {
                Console.Write("Class {0} score = ", c);
                currentClassScore = Math.Log(sumTextsInClasses[c] / (double)countTexts);
                for (int i = 0; i < words.Length; i++)
                {
                    wordFreqInClasses[c].TryGetValue(words[i],out int w);
                    currentClassScore += Math.Log((w + 1) / (double)(uniqueWords + sumWordsInClasses[c]));
                }
                Console.WriteLine("{0:F3}", currentClassScore);
                if(currentClassScore > maxClassScore)
                {
                    maxClassScore = currentClassScore;
                    resultClass = c;
                }
                scoresOfClasses[c] = currentClassScore;
            }

            //Normalization of dimension to probability system
            double percent = 0;
            foreach(double score in scoresOfClasses)
            {
                percent += Math.Pow(Math.E, score);
            }
            double res = 0;
            for(int sc = 0; sc < scoresOfClasses.Length; sc++)
            {
                res = (Math.Pow(Math.E, scoresOfClasses[sc]) / percent) * 100;
                Console.WriteLine("Text is {0:F3}% in class {1}", res, sc);
            }

            Console.WriteLine("Text is {0}", classes[resultClass]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Bayes is learning!");
            sb.Append("Classes:");
            int i = 0;
            foreach (string s in classes)
            {
                sb.Append(" " + i + "-" + s);
                i++;
            }
            sb.AppendLine();
            sb.Append("Amount of texts in class:");
            foreach (int s in sumTextsInClasses)
                sb.Append(" " + s);
            sb.AppendLine();
            sb.AppendLine(string.Format("Amount of texts: {0}", countTexts));
            sb.Append("Amount of words in class:");
            foreach (int s in sumWordsInClasses)
                sb.Append(" " + s);
            sb.AppendLine();
            sb.AppendLine(string.Format("Amount of unique words: {0}", uniqueWords));
            return sb.ToString();
        }
    }
}
