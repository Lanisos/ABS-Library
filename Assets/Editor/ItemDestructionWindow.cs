using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDestructionWindow : EditorWindow
{
    private string item_name = "";
    
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(ItemDestructionWindow));
    }

    void OnGUI() {
        float label_value = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 250;
        item_name = EditorGUILayout.TextField("Introduce name of item to erase: ",item_name);
        GUILayout.FlexibleSpace();
        int code = 1;
        if (GUILayout.Button("Erase item")) {
            Debug.Log("Clicked");
            code = ItemController.GetInstance().DestroyItem(item_name);
            ItemDestroyErrorCheck.ShowWindow(code,item_name);
        }
        EditorGUIUtility.labelWidth = label_value;
        if (code == 0) this.Close();
    }
}
