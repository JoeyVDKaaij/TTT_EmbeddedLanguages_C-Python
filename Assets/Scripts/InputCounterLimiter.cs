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
    [SerializeField, Tooltip("Set to true if the InputField needs to adhere to a minimum number.")]
    private bool checkMinimum = true;
    [SerializeField, Tooltip("Set the minimum value of the InputField.")]
    private int minimumNumber = 1;
    [SerializeField, Tooltip("Set to true if the InputField needs to adhere to an odd number.")]
    private bool checkOdd = false;
    
    private TMP_InputField _inputField;
    
    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        
        _inputField.onEndEdit.AddListener(UpdateInputByOddNumbers);
        _inputField.onEndEdit.AddListener(UpdateInputByMinimum);
    }

    public void UpdateInputByMinimum(string pInput)
    {
        if (!checkMinimum || pInput == "") return;
        
        int number = int.Parse(pInput);
        
        if (number < minimumNumber) _inputField.SetTextWithoutNotify(minimumNumber.ToString());
    }

    public void UpdateInputByOddNumbers(string pInput)
    {
        if (!checkOdd || pInput == "") return;
        
        int number = int.Parse(pInput);
        
        if (number % 2 == 0) _inputField.SetTextWithoutNotify((number + 1).ToString());
    }
}
