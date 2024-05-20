using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;
using System.IO;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AgentController 
{
    [SerializeField] [HideInInspector] private static Dictionary<string,Agent> environment_agents;

    [SerializeField] [HideInInspector] private static AgentController instance;

    private AgentController() {
        environment_agents = new Dictionary<string,Agent>();
    }

    public static AgentController GetInstance() {
        if (instance == null) instance = new AgentController();
        return instance;
    }

    public int CreateAgent(types_of_agents selected, string name, agent_shape shape, Color color, float vision, int population, float radius, float circ_x, float circ_y, float caps_x, float caps_y) {
        //Debug.Log("Starting the creation");
        //Debug.Log(environment_agents.Count);
        if (name.Length == 0 || name.Length > 50) return 1;  //1 = Agent name is either empty or too long
        Regex checker = new Regex("^[a-zA-Z0-9]*$");
        if (!checker.IsMatch(name)) return 2; //2 = Agent name is not alphanumerical
        if (File.Exists(Application.dataPath+"/Prefabs/Created Agent Types/"+name+".prefab")) return 3; //3 = Agent name is already registered
        //if (i.Value.gameObject.transform.GetComponentInChildren<SpriteRenderer>().color == color) return 4; //4 = Agent color is already registered
        if (population == 0) return 5; //5 = Agent cannot start without units, at least should start with 1 unit
        //Debug.Log("Done with error codes");
        AgentFactory factory = null;
        switch (selected) {
            case types_of_agents.reflex_simple:
                factory = new ReflexSimpleFactory();
                break;
            case types_of_agents.model_based:
                factory = new ModelBasedFactory();
                break;
        }
        //Debug.Log("About to call factory");
        Agent new_agent = factory.CreateAgent(name,shape,color,vision,population,radius,circ_x,circ_y,caps_x,caps_y);
        environment_agents.Add(name,new_agent);
        //if (NameChecker(name)) return 1;
        //if (ColorChecker(color)) return 2;
        return 0;
    }

    public int DestroyAgent(string agent_name)
    {
        if (agent_name.Length == 0 || agent_name.Length >= 50) return 1; //1 = Agent name is either empty or too long
        Regex checker = new Regex("^[a-zA-Z0-9]*$");
        if (!checker.IsMatch(agent_name)) return 2; //2 = Agent name is not alphanumerical
        if (File.Exists(Application.dataPath+"/Prefabs/Created Agent Types/"+agent_name+".prefab")) {
            Debug.Log("Match found");
            //Agent destroyee;
            //environment_agents.TryGetValue(agent_name, out destroyee);
            List<string> failed = new List<string>();
            AssetDatabase.DeleteAssets(new string [] {"Assets/Prefabs/Created Agent Types/"+agent_name.Replace(" ","")+".prefab","Assets/Scripts/Behaviour/"+agent_name.Replace(" ","")+"Behaviour.cs",
            "Assets/Scripts/Parameter/"+agent_name.Replace(" ","")+"Parameters.cs","Assets/Resources/Created Agent Units/"+agent_name.Replace(" ","")+"Unit.prefab"}, failed);
            return 0;
        }
        
        else {Debug.Log("Match not found");return 3;}
        
    }

    public void RegisterAgents() {
        //if (environment_agents.Count > 0) {
            if (!Directory.Exists("Assets/Prefabs/Created Agent Types")) AssetDatabase.CreateFolder("Assets/Prefabs","Created Agent Types");
            string[] all_prefabs = AssetDatabase.FindAssets("", new string[] {"Assets/Prefabs/Created Agent Types"});
            Debug.Log(all_prefabs.Length);
            foreach (string i in all_prefabs) {
                Debug.Log("Do I even get here?");
                Debug.Log(AssetDatabase.GUIDToAssetPath(i));
                GameObject agent_prefab = (GameObject)AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(i));
                Debug.Log(agent_prefab);
                //GameObject agent = (GameObject)PrefabUtility.InstantiatePrefab(agent_prefab);
                //Debug.Log(agent);
                environment_agents.Add(agent_prefab.GetComponent<Agent>().GetAgentName(),agent_prefab.GetComponent<Agent>());
                Object.DestroyImmediate(agent_prefab);
            }
        //}
    }
}
