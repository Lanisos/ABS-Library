using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using MultipleEnums;

public class ReflexSimpleFactory : AgentFactory
{
    // Start is called before the first frame update
    public override Agent CreateAgent(string name, agent_shape shape, Color color, float vision, int population, float radius, float circ_x, float circ_y, float caps_x, float caps_y)
    {
        //Agent simple_agent = new Agent();
        GameObject agent = new GameObject();
        agent.name = name;
        AgentSimple simple_agent = agent.AddComponent<AgentSimple>();
        Debug.Log("About to Initialize new kind of simple Agent");
        simple_agent.InitializeAgent(name,shape,color,vision,(uint)(int)population,radius,circ_x,circ_y,caps_x,caps_y);
        if (!Directory.Exists("Assets/Prefabs/Created Agent Types")) AssetDatabase.CreateFolder("Assets/Prefabs","Created Agent Types");
        PrefabUtility.SaveAsPrefabAsset(agent,"Assets/Prefabs/Created Agent Types/"+name.Replace(" ","")+".prefab");
        Object.DestroyImmediate(agent);
        return simple_agent;
    }
}
