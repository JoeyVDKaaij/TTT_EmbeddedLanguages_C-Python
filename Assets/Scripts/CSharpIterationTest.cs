using UnityEngine;
using System.Diagnostics;
using System.Numerics;
using TMPro;
using Debug = UnityEngine.Debug;

/// <summary>
/// A script that checks how fast C# handles looping through a set amount of iterations.
/// </summary>
public class CSharpIterationTest : MonoBehaviour
{
    [Header("Iteration Settings")]
    [SerializeField, Tooltip("Set the text that displays how long a set amount of iterations took!")]
    private TMP_Text timerText;
    [SerializeField, Tooltip("Set the input field that contains the amount of iterations!")]
    private TMP_InputField _inputField;
    
    public void Iterate()
    {
        if (_inputField == null || _inputField.text == "") return;
        
        int currentIteration = 0;
        BigInteger amountOfIterations = BigInteger.Parse(_inputField.text);
        
        DataLogger.TestRecord record = DataLogger.GetTestRecordTemplate("Iterations", "C#");
        record.header = "language,iterations,executionTimeMs,pDateOfTesting,timeOfTesting\n";
        record.iterations = _inputField.text;
        
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < amountOfIterations; i++)
        {
            currentIteration++;
        }
        sw.Stop();

        record.executionTimeMs = sw.ElapsedMilliseconds.ToString();
        DataLogger.SaveAsCSV(record);
        
        Debug.Log($"Iterated {currentIteration} times! This took {sw.ElapsedMilliseconds.ToString()} milliseconds or {((float)sw.ElapsedMilliseconds / 1000).ToString("0.00")} seconds!.");
        
        if (timerText != null)
            timerText.text = sw.ElapsedMilliseconds.ToString() + " Milliseconds!";
    }
}
