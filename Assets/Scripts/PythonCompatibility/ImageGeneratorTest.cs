using System.Diagnostics;
using Python.Runtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// This script handles how python generates an image from the values Unity gives it.
/// </summary>
public class ImageGeneratorTest : PythonMonoBehaviour
{
    [Header("Image Generator Test Settings")]
    [SerializeField, Tooltip("Set the text that displays how long a set amount of iterations took!")]
    private TMP_Text pythonTimerText;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _seedInputField;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _widthInputField;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _heightInputField;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _kernelInputField;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _iterationsInputField;
    
    public void ImageGenerator()
    {
        if (CheckAnyInvalidInputFields()) return;
        
        int seed = int.Parse(_seedInputField.text);
        int width = int.Parse(_widthInputField.text);
        int height = int.Parse(_heightInputField.text);
        int kernel = int.Parse(_kernelInputField.text);
        int iterations = int.Parse(_iterationsInputField.text);

        DataLogger.TestRecord record = DataLogger.GetTestRecordTemplate("Image Generator", "Python");
        record.header = "language,seed,width,height,kernel,iterations,executionTimeMs,pDateOfTesting,timeOfTesting\n";
        record.seed = _seedInputField.text;
        record.width = _widthInputField.text;
        record.height = _heightInputField.text;
        record.kernel = _kernelInputField.text;
        record.iterations = _iterationsInputField.text;
        
        var sw = Stopwatch.StartNew();
        dynamic pythonMethod = FetchMethod("TestGaussianBlur");
        PyObject pyObject = pythonMethod(seed, width, height, kernel, iterations);
        sw.Stop();
        
        Debug.Log(pyObject.ToString());
        Debug.Log($"Generated image with seed: {seed}, width: {width}, height: {height}, kernel: {kernel}, kernel iterations: {iterations}." +
                  $" This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");

        record.executionTimeMs = sw.ElapsedMilliseconds.ToString();
        DataLogger.SaveAsCSV(record);
        
        if (pythonTimerText != null)
            pythonTimerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
    }

    private bool CheckAnyInvalidInputFields()
    {
        return _seedInputField == null || _widthInputField == null || _heightInputField == null || _kernelInputField == null ||
               _iterationsInputField == null || _seedInputField.text == "" || _widthInputField.text == "" || _heightInputField.text == "" || 
               _kernelInputField.text == "" || _iterationsInputField.text == "";
    }
}