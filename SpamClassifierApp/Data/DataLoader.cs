using SpamClassifierApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpamClassifierApp.Data
{
    public static class DataLoader
    {
        public static List<Email> LoadEmails(string csvFilePath)
        {
            var emails = new List<Email>();
            var lines = File.ReadAllLines(csvFilePath);

            for (int i = 0; i < lines.Length; i++)  // Adjust if no header in your file
            {
                // Assuming CSV format: "email content","label"
                var parts = SplitCsvLine(lines[i]);

                if (parts.Length == 2)
                {
                    emails.Add(new Email
                    {
                        Content = parts[0].Trim('"').Trim(),
                        Label = parts[1].Trim('"').Trim().ToLower()
                    });
                }
            }

            return emails;
        }

        // Helper to split CSV line on comma outside quotes
        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var current = "";

            foreach (var c in line)
            {
                if (c == '"') inQuotes = !inQuotes;
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current);
                    current = "";
                }
                else
                {
                    current += c;
                }
            }
            result.Add(current);
            return result.ToArray();
        }
    }
}
