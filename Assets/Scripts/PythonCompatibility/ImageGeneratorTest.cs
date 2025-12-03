using System.Diagnostics;
using Python.Runtime;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ImageGeneratorTest : PythonMonoBehavior
{
    [Header("Image Generator Test Settings")]
    [SerializeField, Tooltip("Set the text that displays how long a set amount of iterations took!")]
    private TMP_Text pythonTimerText;
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
        
        int width = int.Parse(_widthInputField.text);
        int height = int.Parse(_heightInputField.text);
        int kernel = int.Parse(_kernelInputField.text);
        int iterations = int.Parse(_iterationsInputField.text);
        
        
        var sw = Stopwatch.StartNew();
        dynamic pythonMethod = FetchMethod("TestGaussianBlur");
        PyObject pyObject = pythonMethod(width, height, kernel, iterations);
        sw.Stop();
        Debug.Log(pyObject.ToString());
        Debug.Log($"Generated image with width: {width}, height: {height}, kernel: {kernel}, kernel iterations: {iterations}." +
                  $" This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");

        if (pythonTimerText != null)
            pythonTimerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
    }

    private bool CheckAnyInvalidInputFields()
    {
        return _widthInputField == null || _heightInputField == null || _kernelInputField == null ||
               _iterationsInputField == null || _widthInputField.text == "" || _heightInputField.text == "" || 
               _kernelInputField.text == "" || _iterationsInputField.text == "";
    }
}