using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Python.Runtime;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SortingRandomizedArrayTest : PythonMonoBehavior
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
            var sw = Stopwatch.StartNew();
            pyObject = pythonMethod(pyObject);
            sw.Stop();
            Debug.Log("Sorted array looks like this: " + pyObject.ToString());
            Debug.Log($"Sorted array with {arraySize} elements in Python! This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");
            
            if (pythonTimerText != null)
                pythonTimerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
        }
        else
        {
            var sw = Stopwatch.StartNew();
            array = SortArray(array);
            sw.Stop();
            Debug.Log("Sorted array looks like this: " + array.ToString());
            Debug.Log($"Sorted array with {arraySize} elements in C#! This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");
            
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