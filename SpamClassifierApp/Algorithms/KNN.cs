using SpamClassifierApp.Helpers;
using SpamClassifierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpamClassifierApp.Algorithms
{
    internal class KNN
    {
        private List<Email> trainingData;
        private BruteForceHelper bruteForceHelper = new BruteForceHelper();

        public void Train(List<Email> emails)
        {
            trainingData = emails;
        }

        public (string Label, int SpamVotes, int HamVotes, List<(double Distance, string Label)> TopK) Classify(string input, int k)
        {
            // Get sorted neighbors with distances from helper
            var distances = bruteForceHelper.GetNearestNeighbors(input, trainingData);

            // Take top k nearest neighbors
            var topK = distances.Take(k).ToList();

            int spamVotes = topK.Count(x => x.Label.ToLower() == "spam");
            int hamVotes = topK.Count(x => x.Label.ToLower() == "ham");

            string label = spamVotes > hamVotes ? "spam" : "ham";

            return (label, spamVotes, hamVotes, topK);
        }
    }
}