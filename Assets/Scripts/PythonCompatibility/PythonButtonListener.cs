using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A small script that invokes Python methods since that is not build in the Button component.
/// </summary>
[RequireComponent(typeof(Button))]
public class PythonButtonListener : PythonMonoBehaviour
{
    [SerializeField, Tooltip("Set the name of the Python method that gets called when the button gets pressed.")]
    private string[] MethodNames;
    
    private Button _button;
    
    protected override void Awake()
    {
        base.Awake();
        
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        foreach (string methodName in MethodNames)
        {
            dynamic pythonMethod = FetchMethod(methodName);
            if (pythonMethod != null)
                pythonMethod();
        }
    }
}
