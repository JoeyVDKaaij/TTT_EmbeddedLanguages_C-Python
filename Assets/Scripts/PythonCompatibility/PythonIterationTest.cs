using System.Diagnostics;
using System.Numerics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PythonIterationTest : PythonMonoBehavior
{
    [Header("Iteration Settings")]
    [SerializeField, Tooltip("Set the text that displays how long a set amount of iterations took!")]
    private TMP_Text timerText;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _inputField;
    
    public void Iterate()
    {
        if (_inputField == null && _inputField.text == "") return;
        
        
        var sw = Stopwatch.StartNew();

        dynamic iterateMethod = FetchMethod("Iterate");

        int currentIteration = iterateMethod(_inputField.text);
        
        sw.Stop();
        Debug.Log($"Iterated {currentIteration} times! This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");
        
        if (timerText != null)
            timerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
    }
}
