using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AgentErrorCheckerWindow : EditorWindow
{
    private static int code;

    private static string chosen_name;

    // Start is called before the first frame update
    public static void ShowWindow(int given_code, string name) {
        code = given_code;
        chosen_name = name;
        EditorWindow.GetWindow(typeof(AgentErrorCheckerWindow));
    }

    void OnGUI() {
        GUIStyle style = new GUIStyle(EditorStyles.textField);
        switch (code) {
                case 0:
                    style.normal.textColor = Color.green;
                    GUILayout.Label("Agent " + chosen_name + " successfully created!",style);
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
                    GUILayout.Label("ERROR: Agent's name is already registered!",style);
                    break;
                case 4:
                    style.normal.textColor = Color.red;
                    GUILayout.Label("ERROR: Another Agent already has that exact color!",style);
                    break;
                case 5:
                    style.normal.textColor = Color.red;
                    GUILayout.Label("ERROR: Agent must start with at least 1 Agent Unit!",style);
                    break;
        }
        if (GUILayout.Button("Close")) this.Close();
    }
    
}
