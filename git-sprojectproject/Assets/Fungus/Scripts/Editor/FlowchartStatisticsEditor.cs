using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlowchartStatistics))]
public class FlowchartStatisticsEditor : Editor
{

    [MenuItem("Tools/Fungus/Create/Flowchart statistics", false, 1)]
    static void CreateFlowchartStatistics()
    {
        if (Selection.activeGameObject == null)
        {
            EditorUtility.DisplayDialog("Create flowchart statistics", "Select the flowchart to gather statistics from.", "OK");
            return;
        }

        Fungus.Flowchart flowchart = Selection.activeGameObject.GetComponent<Fungus.Flowchart>();
        if (flowchart == null)
        {
            EditorUtility.DisplayDialog("Create flowchart statistics", "Select the flowchart to gather statistics from.", "OK");
            return;
        }

        FlowchartStatistics flowchartStatistics = flowchart.GetComponent<FlowchartStatistics>();
        if(flowchartStatistics == null)
            flowchartStatistics = flowchart.gameObject.AddComponent<FlowchartStatistics>();

        Selection.activeObject = flowchartStatistics;
    }

    public override void OnInspectorGUI()
    {
        FlowchartStatistics record = target as FlowchartStatistics;
        if (record == null)
            return;

        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
                record.SaveData();

            if (GUILayout.Button("Load new"))
                record.LoadData(false, true);

            if (GUILayout.Button("Reload"))
            {
                if (EditorUtility.DisplayDialog("Confirm reload", "Reloading will overwrite recent statistics.", "OK", "Cancel"))
                    record.LoadData(true, true);
            }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear new statistics"))
            {
                record.SaveData();
                record.ClearData(true, false);
                record.LoadData(true, false);
            }
            if (GUILayout.Button("Clear all statistics"))
            {
                record.ClearData(true, true);
            }
        GUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
            Fungus.EditorUtils.FlowchartEditor.FlowchartDataStale = true;
    }

}