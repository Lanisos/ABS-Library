using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Text.RegularExpressions;

public class AgentReflex : Agent
{
    public void InitializeAgent(string name, agent_shape shape, Color color, float vision, uint population, float radius, float circ_x, float circ_y, float caps_x, float caps_y) {
        done_initializing = false;
        agent_name = name;
        vision_range = (uint) (int) vision;
        initial_population = population;
        current_shape = shape;
        agent_color = color;
        agent_units = new List<GameObject>();
        CreateBaseBehaviour(agent_name);
        base.CreateBaseParameters(agent_name);
        Debug.Log("Behaviour and Parameters created for Agent");
        GameObject agent_unit = new GameObject();
        agent_unit.name = agent_name + " Unit";
        agent_unit.layer = LayerMask.GetMask("Agents");
        agent_unit.tag = "Agent";
        CircleCollider2D vision_circle = agent_unit.AddComponent<CircleCollider2D>();
        vision_circle.isTrigger = true;
        vision_circle.radius = vision_range;
        SpriteRenderer agent_aspect = agent_unit.AddComponent<SpriteRenderer>();
        agent_aspect.color = color;
        agent_aspect.drawMode = SpriteDrawMode.Sliced;
        Rigidbody2D body = agent_unit.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Dynamic;
        body.simulated = true;
        body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        body.freezeRotation = true;
        switch (shape) {
            case agent_shape.circle:
                agent_aspect.sprite = Resources.Load<Sprite>("Sprites/circle_agent");
                CircleCollider2D circle_collider = agent_unit.AddComponent<CircleCollider2D>();
                circle_collider.radius = radius;
                agent_radius = radius;
                agent_aspect.size = new Vector2(radius*2,radius*2);
                break;
            case agent_shape.cube:
                agent_aspect.sprite = Resources.Load<Sprite>("Sprites/cube_agent");
                BoxCollider2D box_collider = agent_unit.AddComponent<BoxCollider2D>();
                box_collider.size = new Vector2(circ_x,circ_y);
                agent_size = new Vector2(circ_x,circ_y);
                agent_aspect.size = new Vector2(circ_x,circ_y);
                break;
            case agent_shape.person:
                agent_aspect.sprite = Resources.Load<Sprite>("Sprites/humanoid_agent");
                CapsuleCollider2D capsule_collider = agent_unit.AddComponent<CapsuleCollider2D>();
                capsule_collider.size = new Vector2(caps_x,caps_y);
                agent_size = new Vector2(caps_x,caps_y);
                agent_aspect.size = new Vector2(caps_x,caps_y);
                break;
        }
        #if UNITY_EDITOR
        if (!Directory.Exists("Assets/Resources")) AssetDatabase.CreateFolder("Assets", "Recources");
        else if (!Directory.Exists("Assets/Resources/Created Agent Units")) AssetDatabase.CreateFolder("Assets/Resources", "Created Agent Units");
        relative_unit_path = "Created Agent Units/"+agent_name.Replace(" ","")+"Unit";
        PrefabUtility.SaveAsPrefabAsset(agent_unit,"Assets/Resources/"+relative_unit_path+".prefab");
        Object.DestroyImmediate(agent_unit);
        #endif
        /*for (int i = 0; i < initial_population; ++i) {
            AddAgentUnits(i);
        }*/
        done_initializing = true;
    }

    protected void CreateBaseBehaviour(string name) {
        TextAsset base_file = new TextAsset();
        #if UNITY_EDITOR
        base_file = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Behaviour/BehaviourModelTemplate.txt", typeof(TextAsset)) as TextAsset;
        string text = "";
        if (base_file != null) {
            text = base_file.text;
            text = text.Replace("SPECIFIC_BEHAVIOUR",name.Replace(" ","")+"Behaviour");
        }
        using(StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/Scripts/Behaviour/{0}.cs", new object[] { agent_name.Replace(" ", "") + "Behaviour" }))) {
            sw.Write(text);
        }
        AssetDatabase.Refresh();
        #endif
    }
}
