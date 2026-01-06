using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

[CustomEditor(typeof(BoatManager))]
public class BoatEditor : Editor
{
    private Editor boatsDataEditor = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SerializedProperty serializedData = serializedObject.FindProperty("data");

        Object data = serializedData.objectReferenceValue;
        Editor.CreateCachedEditor(data, null, ref boatsDataEditor);
        boatsDataEditor.OnInspectorGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
