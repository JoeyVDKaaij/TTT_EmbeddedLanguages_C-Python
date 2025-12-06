using System;
using System.IO;
using UnityEngine;

/// <summary>
/// This script sets up and saves recorded data from all the tests done in the prototype.
/// </summary>
public static class DataLogger
{
    /// <summary>
    /// A struct that holds all the data from the tests.
    /// </summary>
    public struct TestRecord
    {
        public string header;
        public string name;
        public string language;
        public string arraySize;
        public string seed;
        public string width;
        public string height;
        public string kernel;
        public string iterations;
        public string executionTimeMs;
        public string dateOfTesting;
        public string timeOfTesting;

        public TestRecord(string pName, string pLanguage, string pDateOfTesting, string pTimeOfTesting)
        {
            header = "language,executionTimeMs,pDateOfTesting,timeOfTesting\n";
            
            name = pName;
            language = pLanguage;
            width = "";
            height = "";
            kernel = "";
            iterations = "";
            arraySize = "";
            seed = "";
            executionTimeMs = "";
            dateOfTesting = pDateOfTesting;
            timeOfTesting = pTimeOfTesting;
        }
    }
    
    /// <summary>
    /// Saves the data in a CSV file.
    /// </summary>
    /// <param name="pTestRecord">The record that holds all the data.</param>
    public static void SaveAsCSV(TestRecord pTestRecord)
    {
        string fileName = $"{pTestRecord.name}_benchmark.csv";
        fileName = fileName.Replace(" ", "_");
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // Write header if file doesn’t exist
        if (!File.Exists(path))
            File.WriteAllText(path, pTestRecord.header);

        string line = "";

        if (pTestRecord.language != "")
            line += $"{pTestRecord.language},";
        if (pTestRecord.arraySize != "")
            line += $"{pTestRecord.arraySize},";
        if (pTestRecord.seed != "")
            line += $"{pTestRecord.seed},";
        if (pTestRecord.width != "")
            line += $"{pTestRecord.width},";
        if (pTestRecord.height != "")
            line += $"{pTestRecord.height},";
        if (pTestRecord.kernel != "")
            line += $"{pTestRecord.kernel},";
        if (pTestRecord.iterations != "")
            line += $"{pTestRecord.iterations},";
        if (pTestRecord.executionTimeMs != "")
            line += $"{pTestRecord.executionTimeMs},";
        if (pTestRecord.dateOfTesting != "")
            line += $"{pTestRecord.dateOfTesting},";
        if (pTestRecord.timeOfTesting != "")
            line += $"{pTestRecord.timeOfTesting}";
        
        File.AppendAllText(path, line + "\n");
        
        Debug.Log($"Data saved in: {path}");
    }

    public static TestRecord GetTestRecordTemplate(string pName, string pLanguage)
    {
        TestRecord testRecordTemplate = new TestRecord(pName, pLanguage, DateTime.Today.ToString("dd-MM-yyyy"), DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
        return testRecordTemplate;
    }
}