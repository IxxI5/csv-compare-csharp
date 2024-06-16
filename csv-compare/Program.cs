using System;
/*
    VS Code Debugging Tip
    In case you experience errors while attempting to "Debug project associated with this file", 
    please remove the folder C:\Program Files (x86)/dotnet. 
    https://stackoverflow.com/questions/67413205/net-sdk-is-not-found-in-vscode
*/

/*  -Features-
    -Automatic Detection of Column Name (must be unique) inside.csv file, even if there are multiple lines of text preceding the Column of interest.
    -Automatically detects comma-separated or semicolon-separated files and compares a reference and measurements .csv data file.
    -The Measurements Series Length can be equal, larger, or smaller than the Reference Data Length (Result == Status.NotComparable). 
    -The Comparison Algorithm is automatically applied to Measurements Data with NOISE or being SHIFTED FORWARD IN TIME (e.g., up to 10 secs forward with a sampling rate of 10 msecs).
*/

namespace CSV.Compare
{
    class Program
    {
        // TimeSeriesCompare Class Test | 23.01.2020 Created by IxxI5
        static void Main(string[] args)
        {
            // Compares two Time Series. Measurements Series may be equal, larger but not smaller (Result == Status.NotComparable) in length than the Reference Series
            TimeSeriesCompare TSC = new TimeSeriesCompare()
            {
                ReferenceFile = @"data\Reference.gp.csv",
                MeasurementsFile = @"data\Measurements.gp.csv",
                ColumnName = "Voltage M1",
                Tolerance = 10,
            };

            TSC.Execute();                                                          // Load the Files and start the Algorithm Execution

            Console.WriteLine("Comparison Result: " + TSC.Result + " | " + "Similarity: " + TSC.Similarity.ToString("0.##") + "%" + " | " + "Passed IF >= " + (100 - TSC.Tolerance).ToString("0.##") + "%");
            Console.ReadLine();
        }
    }
}