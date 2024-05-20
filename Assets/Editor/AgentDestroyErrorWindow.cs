using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AgentDestroyErrorWindow : EditorWindow
{

    private static int code;

    private static string chosen_name;

    public static void ShowWindow(int given_code, string name)
    {
        code = given_code;
        chosen_name = name;
        EditorWindow.GetWindow(typeof(AgentDestroyErrorWindow));
    }

    void OnGUI() {
        GUIStyle style = new GUIStyle(EditorStyles.textField);
        switch (code) {
            case 0:
                style.normal.textColor = Color.green;
                GUILayout.Label("Agent " + chosen_name + " successfully destroyed!",style);
                break;
            case 1:
                style.normal.textColor = Color.red;
                GUILayout.Label("ERROR: Agent's name must be between 1 and 50 characters!",style);
                break;
            case 2:
                style.normal.textColor = Color.red;
                GUILayout.Label("ERROR: Agent's name must only have alphanumerical characters!",style);
                break;
            case 3:
                style.normal.textColor = Color.red;
                GUILayout.Label("ERROR: Agent's name is not registered!",style);
                break;
        }
        if (GUILayout.Button("Close")) this.Close();
    }    
}
