using SpamClassifierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamClassifierApp.Helpers
{
    internal class BruteForceHelper
    {
        // We keep the CalculateDistance method here to reuse in KNN
        public double CalculateDistance(string input1, string input2)
        {
            int length1 = input1.Length;
            int length2 = input2.Length;
            int maxLen = Math.Max(length1, length2);
            int diff = 0;

            for (int i = 0; i < Math.Min(length1, length2); i++)
            {
                if (input1[i] != input2[i])
                    diff++;
            }

            diff += Math.Abs(length1 - length2);

            return (double)diff / maxLen; // normalized distance between 0 and 1
        }

        public List<(double Distance, string Label)> GetNearestNeighbors(string input, List<Email> trainingData)
        {
            var distances = new List<(double Distance, string Label)>();

            foreach (var email in trainingData)
            {
                double dist = CalculateDistance(input, email.Content);
                distances.Add((dist, email.Label));
            }

            // Sort distances with your existing QuickSort helper
            QuickSortHelper.QuickSort(distances, 0, distances.Count - 1);

            return distances;
        }
    }
}
