using SpamClassifierApp.Algorithms;
using SpamClassifierApp.Data;
using SpamClassifierApp.Helpers;
using SpamClassifierApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpamClassifierApp
{
    public partial class Form1 : Form
    {
        private KNN knn = new KNN();
        private List<Email> emails;

        public Form1()
        {
            InitializeComponent();

            // Always show KNN's K selector
            lblk.Visible = true;
            numK.Visible = true;

            // Tooltips for inputs
            toolTip1.SetToolTip(txtEmail, "Enter your email text here.");
            toolTip1.SetToolTip(lblk, "Select number of neighbors for KNN.");
            toolTip1.SetToolTip(numK, "Select number of neighbors for KNN.");
            toolTip1.SetToolTip(btnClassify, "Click to classify the email using KNN.");
            toolTip1.SetToolTip(txtDetails, "See detailed output and vote breakdown here.");

            // Load and preprocess emails
            emails = DataLoader.LoadEmails(@"Data/SampleEmails.csv");
            foreach (var e in emails)
                e.Content = Preprocessing.Preprocess(e.Content);

            // Train KNN model
        }

        private void btnClassify_Click(object sender, EventArgs e)
        {
            string rawEmail = txtEmail.Text;
            string cleanedEmail = Preprocessing.Preprocess(rawEmail);

            if (string.IsNullOrWhiteSpace(cleanedEmail))
            {
                lblResult.Text = "";
                txtDetails.Clear();
                MessageBox.Show("Please enter a valid email message to classify.", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int k = (int)numK.Value;
            var (label, spamVotes, hamVotes, topKList) = knn.Classify(cleanedEmail, k);

            lblResult.Text = $"Prediction: {label.ToUpper()}";
            lblResult.ForeColor = label == "spam" ? Color.Red : Color.Green;

            txtDetails.Clear();
            txtDetails.AppendText("K-Nearest Neighbors (KNN) Classification\r\n");
            txtDetails.AppendText("=========================================\r\n");
            txtDetails.AppendText($"K Value         : {k}\r\n");
            txtDetails.AppendText($"SPAM Votes      : {spamVotes}\r\n");
            txtDetails.AppendText($"HAM Votes       : {hamVotes}\r\n");
            txtDetails.AppendText($"Final Prediction: {label.ToUpper()}\r\n");

            if (topKList.Count > 0)
            {
                txtDetails.AppendText("\r\nTop K Nearest Neighbors\r\n");
                txtDetails.AppendText("---------------------------------------------------\r\n");

                int index = 1;
                foreach (var (distance, voteLabel) in topKList)
                {
                    txtDetails.AppendText($"#{index,-2} Distance: {distance:F4}   Label: {voteLabel.ToUpper()}\r\n");
                    index++;
                }
            }
            else
            {
                txtDetails.AppendText("\r\n⚠️ No neighbors found.\r\n");
            }
        }
    }
}
