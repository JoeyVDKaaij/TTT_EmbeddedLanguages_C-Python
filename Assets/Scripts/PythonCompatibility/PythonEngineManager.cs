using System;
using UnityEngine;
using Python.Runtime;

public class PythonEngineManager : MonoBehaviour
{
    public static PythonEngineManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (transform.parent != null) DontDestroyOnLoad(transform.parent.gameObject);
            else DontDestroyOnLoad(this);
            
            
            Runtime.PythonDLL = Application.dataPath + "/StreamingAssets/embedded-python/python39.dll";
            PythonEngine.PythonHome = Application.dataPath + "/StreamingAssets/embedded-python";
            PythonEngine.Initialize(mode: ShutdownMode.Reload);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            PythonEngine.Shutdown(ShutdownMode.Reload);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        if (PythonEngine.IsInitialized)
        {
            Debug.Log("ending python");
            PythonEngine.Shutdown(ShutdownMode.Reload);
        }
    }
}
