using System;
using Python.Runtime;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
/// <summary>
/// This small script allows for easier reimport of Python lib packages by clicking
/// on one of the dropdown menus next to the file and edit menu without restarting Unity.
/// Note that any changes made to your own project still requires restarting Unity.
/// </summary>
public class PythonMenu
{
    [MenuItem("Python Scripting/Reimport Python Project")]
    private static void ReimportPythonProject()
    {
        AssetDatabase.ImportAsset(
            "Assets/StreamingAssets/embedded-python/Lib", 
            ImportAssetOptions.ImportRecursive);
    }
}
#endif
