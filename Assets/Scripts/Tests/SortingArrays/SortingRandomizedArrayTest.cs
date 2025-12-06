using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Python.Runtime;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// This script tests how fast C# and Python sorts an array.
/// </summary>
public class SortingRandomizedArrayTest : PythonMonoBehaviour
{
    [Header("Sorting Array Test Settings")]
    [SerializeField, Tooltip("Set the text that displays how long a set amount of iterations took!")]
    private TMP_Text cSharpTimerText;
    [SerializeField, Tooltip("Set the text that displays how long a set amount of iterations took!")]
    private TMP_Text pythonTimerText;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _arraySizeInputField;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _seedInputField;
    
    public void SortRandomArray(bool pTestInPython)
    {
        if (_seedInputField == null || _arraySizeInputField == null ||
            _seedInputField.text == "" || _arraySizeInputField.text == "") return;
        

        dynamic pythonMethod = FetchMethod("ReturnRandomizedArray");
        
        int arraySize = int.Parse(_arraySizeInputField.text);
        int seed = int.Parse(_seedInputField.text);

        PyObject pyObject = pythonMethod(arraySize, seed);
        List<float> array = pyObject.As<float[]>().ToList();
        
        pythonMethod = FetchMethod("SortArrayTest");
        
        if (pTestInPython)
        {
            DataLogger.TestRecord record = DataLogger.GetTestRecordTemplate("Sorting Random Array", "Python");
            record.arraySize = arraySize.ToString();
            record.seed = seed.ToString();
            record.header = "language,arraySize,seed,executionTimeMs,pDateOfTesting,timeOfTesting\n";
            
            var sw = Stopwatch.StartNew();
            pyObject = pythonMethod(pyObject);
            sw.Stop();
            
            Debug.Log("Sorted array looks like this: " + pyObject.ToString());
            Debug.Log($"Sorted array with {arraySize} elements in Python! This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");
            
            record.executionTimeMs = sw.ElapsedMilliseconds.ToString();
            DataLogger.SaveAsCSV(record);
            
            if (pythonTimerText != null)
                pythonTimerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
        }
        else
        {
            DataLogger.TestRecord record = DataLogger.GetTestRecordTemplate("Sorting Random Array", "C#");
            record.arraySize = arraySize.ToString();
            record.seed = seed.ToString();
            record.header = "language,arraySize,seed,executionTimeMs,pDateOfTesting,timeOfTesting\n";
            
            var sw = Stopwatch.StartNew();
            array = SortArray(array);
            sw.Stop();
            
            Debug.Log("Sorted array looks like this: " + array.ToString());
            Debug.Log($"Sorted array with {arraySize} elements in C#! This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");

            record.executionTimeMs = sw.ElapsedMilliseconds.ToString();
            DataLogger.SaveAsCSV(record);
            
            if (cSharpTimerText != null)
                cSharpTimerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
        }
    }

    private List<float> SortArray(List<float> pArray)
    {
        pArray.Sort();
        return pArray;
    }
}