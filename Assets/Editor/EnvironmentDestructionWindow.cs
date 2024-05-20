using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnvironmentDestructionWindow : EditorWindow
{
    private string env_name = "";
    
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(EnvironmentDestructionWindow));
    }

    void OnGUI() {
        float label_value = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 250;
        env_name = EditorGUILayout.TextField("Introduce name of environment to erase: ",env_name);
        GUILayout.FlexibleSpace();
        int code = 1;
        if (GUILayout.Button("Erase environment")) {
            Debug.Log("Clicked");
            code = EnvironmentController.GetInstance().DestroyEnvironment(env_name);
            EnvDestroyErrorCheck.ShowWindow(code,env_name);
        }
        EditorGUIUtility.labelWidth = label_value;
        if (code == 0) this.Close();
    }
}
