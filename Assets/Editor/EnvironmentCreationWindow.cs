using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MultipleEnums;

public class EnvironmentCreationWindow : EditorWindow
{
    
    private string env_name;
    private float length = 10;
    private float height = 10;
    private Color background = Color.black;
    private time_amount time_units = time_amount.second;
    private int unit_amount = 1;
    
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(EnvironmentCreationWindow));
    }

    void OnGUI() {
        env_name = EditorGUILayout.TextField("Environment's Name: ",env_name);
        length = EditorGUILayout.Slider("Environment's length (x): ", length, 5, 250);
        height = EditorGUILayout.Slider("Environment's height (y): ", height, 5, 250);
        background = EditorGUILayout.ColorField("Environment's Color: ", background);
        time_units = (time_amount)EditorGUILayout.EnumPopup("Time unit of Environment: ", time_units);
        unit_amount = EditorGUILayout.IntField("Amount of units per cicle: ", unit_amount);
        if(GUILayout.Button("Create New Environment")) {
            int code = EnvironmentController.GetInstance().CreateEnvironment(env_name,length,height,background,time_units,unit_amount);
            EnvCreaErrorCheck.ShowWindow(code,env_name);
            if (code == 0) this.Close();
        }
    }
}
