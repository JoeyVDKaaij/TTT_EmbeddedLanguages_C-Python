using TMPro;
using UnityEngine;

/// <summary>
/// Quick script that sets the Input field number to the correct value if it goes too low.
/// Not sure why this wasn't build in already...
/// </summary>
[RequireComponent(typeof(TMP_InputField))]
public class InputCounterLimiter : MonoBehaviour
{
    [Header("InputField Settings")]
    [SerializeField, Tooltip("Set the minimum value of the InputField.")]
    private int minimumNumber = 1;
    
    private TMP_InputField _inputField;
    
    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        
        _inputField.onEndEdit.AddListener(UpdateInputByMinimum);
    }

    public void UpdateInputByMinimum(string pInput)
    {
        if (pInput == "") return;
        
        int number = int.Parse(pInput);
        
        if (number < minimumNumber) _inputField.SetTextWithoutNotify(minimumNumber.ToString());
    }
}
