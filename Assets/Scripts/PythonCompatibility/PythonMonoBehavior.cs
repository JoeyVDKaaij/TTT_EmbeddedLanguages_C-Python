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
