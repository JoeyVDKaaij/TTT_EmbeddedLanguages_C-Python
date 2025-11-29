using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
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
