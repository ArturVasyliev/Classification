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
                Console.WriteLine("Class " + c);
                for(int i = 0; i < classTexts[c].Length; i++)
                {
                    Console.WriteLine("Text " + i);
                    string[] words = classTexts[c][i].Split(' ');
                    sumWordsInClasses[c] += words.Length;
                    for(int w = 0; w < words.Length; w++)
                    {
                        Console.WriteLine("Word " + w + " = " + words[w]);
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
            //count a probability of being in class c for each class
            for(int c = 0; c < classes.Length; c++)
            {
                currentClassScore = Math.Log(sumTextsInClasses[c] / countTexts);
                for (int i = 0; i < words.Length; i++)
                {
                    wordFreqInClasses[c].TryGetValue(words[i],out int w);
                    currentClassScore += Math.Log((w + 1) / (uniqueWords + sumWordsInClasses[c]));
                }
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
            percent = (Math.Pow(Math.E, maxClassScore) / percent) * 100;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Bayes is learning!");
            sb.Append("Classes:");
            foreach (string s in classes)
                sb.Append(" " + s);
            sb.AppendLine();
            sb.Append("Amount of texts in class:");
            foreach (int s in sumTextsInClasses)
                sb.Append(" " + s);
            sb.AppendLine();
            sb.AppendLine(string.Format("Amount of texts: {01}", countTexts));
            sb.Append("Amount of words in class:");
            foreach (int s in sumWordsInClasses)
                sb.Append(" " + s);
            sb.AppendLine();
            sb.AppendLine(string.Format("Amount of unique words: {01}", uniqueWords));
            return sb.ToString();
        }
    }
}
