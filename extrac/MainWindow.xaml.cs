using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace extrac
{
    public partial class MainWindow : Window
    {
        private string selectedCsvFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectCsvFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedCsvFilePath = openFileDialog.FileName;
                FilePathTextBlock.Text = selectedCsvFilePath;
            }
        }

        private void ExtractRandomSongs_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCsvFilePath))
            {
                MessageBox.Show("Please select a CSV file first.");
                return;
            }

            // Load CSV data into a list
            List<string> csvLines = File.ReadAllLines(selectedCsvFilePath).ToList();

            if (csvLines.Count <= 1)
            {
                MessageBox.Show("CSV file does not contain enough songs.");
                return;
            }

            // Shuffle the CSV lines (skip the header)
            Random random = new Random();
            csvLines = csvLines.Skip(1).OrderBy(x => random.Next()).ToList();

            // Extract the first 100 lines (songs)
            List<string> extractedSongs = csvLines.Take(100).ToList();

            // Get the user-specified file name
            string outputFileName = FileNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(outputFileName))
            {
                MessageBox.Show("Please enter an output file name.");
                return;
            }

            // Define the output file path
            string outputPath = $"{outputFileName}.txt";

            // Write the extracted songs to a text file
            File.WriteAllLines(outputPath, extractedSongs, Encoding.UTF8);

            MessageBox.Show($"100 random songs extracted and saved to {outputPath}.");
        }

    }
}
