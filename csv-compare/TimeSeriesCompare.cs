using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace CSV.Compare
{
    // TimeSeriesCompare Class | 23.01.2020 Created (Updated: 16.03.2020) by IxxI5
    class TimeSeriesCompare
    {
        /// <summary>
        /// Status Available Values
        /// </summary>
        public enum Status
        {
            InPreparation,
            Passed,
            Failed,
            NotComparable
        }

        /// <summary>
        /// List Upwards Shifts Counter
        /// </summary>
        private int shiftsCounter;

        /// <summary>
        /// Store the found Column Index 
        /// </summary>
        private int columnIndex = -1;

        /// <summary>
        /// Reference Data List Field
        /// </summary>
        private List<double> referenceData = new List<double>();

        /// <summary>
        /// Measured Data List Field
        /// </summary>
        private List<double> measuredData = new List<double>();

        // Constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TimeSeriesCompare()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TimeSeriesCompare(string referenceFile, string measurementsFile, string columnName, double tolerance)
        {
            Result = Status.InPreparation;

            ReferenceFile = referenceFile;
            MeasurementsFile = measurementsFile;

            ColumnName = columnName;
            Tolerance = tolerance;
        }

        // Properties

        /// <summary>
        /// Comparison Result
        /// </summary>
        public Status Result { get; set; }

        /// <summary>
        /// Reference File Path
        /// </summary>
        public string ReferenceFile { get; set; }

        /// <summary>
        /// Reference File Path
        /// </summary>
        public string MeasurementsFile { get; set; }

        /// <summary>
        /// Column Name (to compare)
        /// </summary>
        public string ColumnName { set; get; }

        /// <summary>
        /// Tolerance in Percentage
        /// </summary>
        public double Tolerance { set; get; }

        /// <summary>
        /// Similarity in Percentage
        /// </summary>
        public double Similarity { get; set; }

        // Methods

        /// <summary>
        /// Method: Return the Sum of Time Series Differences Squared
        /// </summary>
        /// <param name="referenceData"></param>
        /// <param name="measuredData"></param>
        /// <param name="shiftsCounter"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        private double SumDiffSquared(List<double> referenceData, List<double> measuredData)
        {
            double sum = 0.0;
            double sumReference = 0.0;

            if (referenceData.Count == 0)
            {
                Result = Status.InPreparation;
                return sum;
            }
            else if (measuredData.Count < referenceData.Count)
            {
                Result = Status.NotComparable;
                return sum;
            }

            while (measuredData.Count > referenceData.Count)
            {
                if (shiftsCounter < 1000)
                {
                    shiftsCounter++;
                    measuredData.RemoveAt(0);                               // Shift measuredData towards lower index positions (upwards)
                }
            }

            if (measuredData.Count >= referenceData.Count)
            {
                for (int i = 0; i < referenceData.Count; i++)
                {
                    double diff = referenceData[i] - measuredData[i];       // difference between referenceData and measuredData elements
                    sum += diff * diff;                                     // sum of differences squared 
                    sumReference += referenceData[i] * referenceData[i];    // sum of the reference values squared
                }
            }

            if (sumReference > 0)
            {
                if ((1 - Math.Sqrt(sum / sumReference)) * 100 >= 100 - Math.Abs(Tolerance))
                {
                    Result = Status.Passed;
                }
                else if ((1 - Math.Sqrt(sum / sumReference)) * 100 < 100 - Math.Abs(Tolerance))
                {
                    Result = Status.Failed;
                }
            }

            Similarity = (1 - Math.Sqrt(sum / sumReference)) * 100;

            return sum;
        }

        /// <summary>
        /// Method: Load Reference or Measurements File to a List
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="storeInList"></param>
        private void LoadFile(string filepath, List<double> storeInList)
        {
            string[]? lines = null;

            try
            {
                lines = File.ReadAllLines(filepath);
            }
            catch (IOException)
            {
                throw new IOException("Reference or Measurements File not found (or not accessible)");
            }

            int commasCount = string.Join("", lines).Count(x => x == ',');
            int semicolonsCount = string.Join("", lines).Count(x => x == ';');

            string[]? content = null;

            foreach (string line in lines)
            {
                if (commasCount > semicolonsCount)
                {
                    content = line.Split(',');
                }
                else
                {
                    content = line.Split(';');
                }

                if (columnIndex == -1)
                {
                    var index = 0;

                    foreach (string column in content)
                    {
                        if (column == ColumnName)
                        {
                            columnIndex = index;
                            break;
                        }

                        index++;
                    }

                    continue;
                }

                if (columnIndex != -1 && content[columnIndex] != "")
                {
                    storeInList.Add(Convert.ToDouble(content[columnIndex])); // Store the Reference Time Series in a List
                }
            }

            columnIndex = -1;
        }

        /// <summary>
        /// Method: Load the Reference and Measurements File and start the Comparing Algorithm
        /// </summary>
        public void Execute()
        {
            LoadFile(ReferenceFile, referenceData);
            LoadFile(MeasurementsFile, measuredData);

            SumDiffSquared(referenceData, measuredData);
        }
    }
}
