using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Small Script that quits the application
/// </summary>
[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(QuitApplication);
    }

    private void QuitApplication()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
}
