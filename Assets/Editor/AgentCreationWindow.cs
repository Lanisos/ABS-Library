using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MultipleEnums;

public class AgentCreationWindow : EditorWindow
{
    
    
    private types_of_agents selected;
    private agent_shape chosen_shape;
    private string chosen_name;
    private float chosen_vision = 5;
    private int chosen_population = 1;
    private float radius = 1;
    private float circ_x = 1;
    private float circ_y = 1;
    private float caps_x = 0.5f;
    private float caps_y = 1f;
    private Color chosen_color = Color.white;
    //private float speed_value = 1f;
    //private int memory_size = 3;
    //int chosen_time_scale = 1;

    private static AgentController controller;

    public static void ShowWindow() {
        controller = AgentController.GetInstance();
        EditorWindow.GetWindow(typeof(AgentCreationWindow));
    }

    void OnGUI() {
        selected = (types_of_agents)EditorGUILayout.EnumPopup("Agent type", selected);
        chosen_name = EditorGUILayout.TextField("Agent's name", chosen_name);
        //chosen_time_scale = EditorGUILayout.IntField("");
        chosen_shape = (agent_shape)EditorGUILayout.EnumPopup("Agent's shape", chosen_shape);
        chosen_color = EditorGUILayout.ColorField("Agent's sprite color",chosen_color);
        switch (chosen_shape) {
            case agent_shape.circle:
                radius = EditorGUILayout.Slider("Agent's radius", radius, 0.1f, 100);
                break;
            case agent_shape.cube:
                circ_x = EditorGUILayout.Slider("Agent's base length",circ_x,0.1f,100);
                circ_y = EditorGUILayout.Slider("Agent's base height",circ_y,0.1f,100);
                break;
            case agent_shape.person:
                caps_x = EditorGUILayout.Slider("Agent's base length",caps_x,0.1f,100);
                caps_y = EditorGUILayout.Slider("Agent's base height",caps_y,0.1f,100);
                break;
        }
        chosen_vision = EditorGUILayout.Slider("Agent's range of vision", chosen_vision, 0, 100);
        chosen_population = EditorGUILayout.IntField("Initial population of agent",chosen_population);
        //speed_value = EditorGUI
        //if (selected == types_of_agents.model_based) memory_size = EditorGUILayout.IntField("Agent's memory size",memory_size);
        if(GUILayout.Button("Create New Agent")) {
            Debug.Log("About to start creating the agent");
            Debug.Log(controller);
            int code = controller.CreateAgent(selected,chosen_name,chosen_shape,chosen_color,chosen_vision,chosen_population,radius,circ_x,circ_y,caps_x,caps_y);
            AgentErrorCheckerWindow.ShowWindow(code,chosen_name);
            if (code == 0) this.Close();
        }
    }
}

