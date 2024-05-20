using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MultipleEnums;

public class ItemCreationWindow : EditorWindow
{

    private agent_shape chosen_shape;
    private types_of_items item_type;
    private string chosen_name;
    private float radius = 1;
    private float circ_x = 1;
    private float circ_y = 1;
    private float caps_x = 0.5f;
    private float caps_y = 1f;


    private static ItemController controller;

    public static void ShowWindow() {
        controller = ItemController.GetInstance();
        EditorWindow.GetWindow(typeof(ItemCreationWindow));
    }

    // Update is called once per frame
    void OnGUI()
    {
        item_type = (types_of_items)EditorGUILayout.EnumPopup("Item type", item_type);
        chosen_name = EditorGUILayout.TextField("Item name", chosen_name);
        chosen_shape = (agent_shape)EditorGUILayout.EnumPopup("Item shape", chosen_shape);
        switch (chosen_shape) {
            case agent_shape.circle:
                radius = EditorGUILayout.Slider("Item radius", radius, 0.1f, 100);
                break;
            case agent_shape.cube:
                circ_x = EditorGUILayout.Slider("Item base length",circ_x,0.1f,100);
                circ_y = EditorGUILayout.Slider("Item base height",circ_y,0.1f,100);
                break;
            case agent_shape.person:
                caps_x = EditorGUILayout.Slider("Item base length",caps_x,0.1f,100);
                caps_y = EditorGUILayout.Slider("Item base height",caps_y,0.1f,100);
                break;
        }
        if(GUILayout.Button("Create New Item")) {
            switch (chosen_shape) {
                case agent_shape.person:
                    circ_x = caps_x;
                    circ_y = caps_y;
                    break;
            }
            int code = controller.CreateItem(item_type,chosen_name,chosen_shape,radius,circ_x,circ_y);
            ItemCreaErrorCheck.ShowWindow(code,chosen_name);
            if (code == 0) this.Close();
        }
    }
}
