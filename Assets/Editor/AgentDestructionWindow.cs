using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AgentDestructionWindow : EditorWindow
{
    private string agent_name = "";

    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(AgentDestructionWindow));
    }

    void OnGUI() {
        float label_value = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 250;
        agent_name = EditorGUILayout.TextField("Introduce the name of the agent to erase: ",agent_name);
        GUILayout.FlexibleSpace();
        int code = 1;
        if (GUILayout.Button("Erase agent")) {
            Debug.Log("Clicked");
            code = AgentController.GetInstance().DestroyAgent(agent_name);
            AgentDestroyErrorWindow.ShowWindow(code,agent_name);
        }
        EditorGUIUtility.labelWidth = label_value;
        if (code == 0) this.Close();
    }
}
