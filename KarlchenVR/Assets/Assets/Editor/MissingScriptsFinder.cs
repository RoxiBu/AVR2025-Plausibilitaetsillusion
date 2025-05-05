using UnityEngine;
using UnityEditor;
using System.IO;

public class MissingScriptsFinder : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts in Assets")]
    static void FindMissingScriptsInAssets()
    {
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        int missingCount = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            Component[] components = prefab.GetComponentsInChildren<Component>(true);
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script in prefab: {path}", prefab);
                    missingCount++;
                }
            }
        }

        if (missingCount == 0)
            Debug.Log("✔️ Keine fehlenden Scripts in Prefabs gefunden.");
        else
            Debug.Log($"❗ Es wurden {missingCount} fehlende Scripts in Prefabs gefunden.");
    }
}
