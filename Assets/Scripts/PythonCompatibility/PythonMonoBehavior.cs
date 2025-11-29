using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Python.Runtime;
using System;

public class PythonMonoBehavior : MonoBehaviour
{
    [Header("Python Settings")]
    [SerializeField, Tooltip("Set the module name.")]
    private string moduleName;
    protected dynamic module;

    #region Start and end methods

    protected virtual void Awake()
    {
        Runtime.PythonDLL = Application.dataPath + "/StreamingAssets/embedded-python/python39.dll";
        PythonEngine.PythonHome = Application.dataPath + "/StreamingAssets/embedded-python";
        PythonEngine.Initialize(mode: ShutdownMode.Reload);
        using (Py.GIL())
        {
            try
            {
                module = Py.Import(moduleName);
            }
            catch (Exception e)
            {
                Debug.LogError("Python module has not been catched! Did you place it at the proper place and/or did Unity properly (re)import it? \n" + e);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        PythonEngine.Shutdown(ShutdownMode.Reload);
    }

    protected virtual void OnApplicationQuit()
    {
        if (PythonEngine.IsInitialized)
        {
            print("ending python");
            PythonEngine.Shutdown(ShutdownMode.Reload);
        }
    }
    
    #endregion
    
    #region Python fetching methods

    protected dynamic FetchMethod(string pMethodName)
    {
        using (Py.GIL())
        {
            try
            {
                return module.GetAttr(pMethodName);
            }
            catch (Exception e)
            {
                Debug.LogError("Python method has not been fetched! \n" + e);

                throw e;
            }
        }
    }
    
    #endregion
}
