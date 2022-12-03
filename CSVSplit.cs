using System;
using System.IO;

namespace CsvSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if the input and output file paths were provided
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CsvSplitter <inputFile> <outputFile>");
                return;
            }

            // Set the input and output file paths from the command-line arguments
            string inputFile = args[0];
            string outputFile = args[1];

            // Set the maximum size of the output files (in bytes)
            long maxSize = 4 * 1024 * 1024;

            // Initialize the output file counter
            int fileCounter = 1;

            // Open the input file for reading
            using (StreamReader reader = new StreamReader(inputFile))
            {
                // Create the first output file
                using (StreamWriter writer = new StreamWriter($"{outputFile}{fileCounter}.csv"))
                {
                    // Read the input file line by line
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Check if the output file has reached the maximum size
                        if (writer.BaseStream.Length > maxSize)
                        {
                            // Close the current output file
                            writer.Close();

                            // Increment the output file counter
                            fileCounter++;

                            // Create a new output file
                            writer = new StreamWriter($"{outputFile}{fileCounter}.csv");
                        }

                        // Write the current line to the output file
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}
