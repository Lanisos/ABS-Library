using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using MultipleEnums;

public class ModelBasedFactory : AgentFactory
{
    // Start is called before the first frame update
    public override Agent CreateAgent(string name, agent_shape shape, Color color, float vision, int population, float radius, float circ_x, float circ_y, float caps_x, float caps_y)
    {
        GameObject agent = new GameObject();
        agent.name = name;
        AgentReflex model_agent = agent.AddComponent<AgentReflex>();
        model_agent.InitializeAgent(name,shape,color,vision,(uint)(int)population,radius,circ_x,circ_y,caps_x,caps_y);
        if (!Directory.Exists("Assets/Prefabs/Created Agent Types")) AssetDatabase.CreateFolder("Assets/Prefabs","Created Agent Types");
        PrefabUtility.SaveAsPrefabAsset(agent,"Assets/Prefabs/Created Agent Types/"+name.Replace(" ","")+".prefab");
        Object.DestroyImmediate(agent);
        return model_agent;
    }
}
