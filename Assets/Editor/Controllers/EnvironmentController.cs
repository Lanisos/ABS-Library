using System.Collections;
using System.Collections.Generic;
using MultipleEnums;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class EnvironmentController
{
    
    private Environment current_env;
    private static EnvironmentController instance;

    private EnvironmentController() {

    }

    public static EnvironmentController GetInstance() {
        if (instance == null) instance = new EnvironmentController();
        return instance;
    }

    public int CreateEnvironment(string env_name, float length, float height, Color background, time_amount time_units, int unit_amount) {
        //AssetDatabase.LoadAllAssetsAtPath("Assets/Prefabs/Environments");
        if (env_name.Length == 0 || env_name.Length > 50) return 1; //1 = Env name is either empty or too long
        Regex checker = new Regex("^[a-zA-Z0-9]*$");
        if (!checker.IsMatch(env_name)) return 2; //2 = Env name is not alphanumerical
        if (File.Exists(Application.dataPath+"/Prefabs/Environment/"+env_name+".prefab")) return 3; //3 = Env already exists
        if (unit_amount <= 0) return 4; //4 = Amount must be above 0
        GameObject loade_env = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Environment.prefab");
        //Environment env = loaded_env.GetComponentInChildren
        //Debug.Log(loade_env);
        GameObject loaded_env = (GameObject)PrefabUtility.InstantiatePrefab(loade_env);
        loaded_env.name = env_name;
        GameObject image = loaded_env.transform.Find("Environment Space").GetChild(0).gameObject;
        SpriteRenderer img_rend = image.GetComponent<SpriteRenderer>();
        image.transform.localScale = new Vector2(length,height);
        img_rend.color = background;
        SimulationTime sim_time = loaded_env.GetComponentInChildren<SimulationTime>();
        sim_time.InitializeTimer(time_units,unit_amount);
        CinemachineVirtualCamera cam = loaded_env.GetComponentInChildren<CinemachineVirtualCamera>();
        cam.m_Lens.OrthographicSize = Mathf.Max(length,height) * 0.7f;
        CreateBaseParameters(env_name);
        if (!Directory.Exists("Assets/Prefabs/Environment")) AssetDatabase.CreateFolder("Assets/Prefabs", "Environment");
        PrefabUtility.SaveAsPrefabAsset(loaded_env,"Assets/Prefabs/Environment/"+env_name.Replace(" ","")+"Env.prefab");
        Object.DestroyImmediate(loaded_env);
        Object.DestroyImmediate(loade_env);
        return 0; //Done correctly
    }

    public int DestroyEnvironment(string env_name) {
        if (env_name.Length == 0 || env_name.Length >= 50) return 1; //1 = Env name is either empty or too long
        Regex checker = new Regex("^[a-zA-Z0-9]*$");
        if (!checker.IsMatch(env_name)) return 2; //2 = Env name is not alphanumerical
        if (File.Exists(Application.dataPath+"/Prefabs/Environment/"+env_name+".prefab")) {
            List<string> failed = new List<string>();
            AssetDatabase.DeleteAssets(new string [] {"Assets/Prefabs/Environment/"+env_name+".prefab","Assets/Scripts/Parameter/"+env_name+"Parameters.cs"},failed);
            return 0;
        }
        else return 3; //3 = Env does not exist
    }

    private void CreateBaseParameters(string env_name) {
        TextAsset base_file = new TextAsset();
        base_file = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Parameter/EnvParameterTemplate.txt", typeof(TextAsset)) as TextAsset;
        string text = "";
        if (base_file != null) {
            text = base_file.text;
            text = text.Replace("SPECIFIC_PARAMETERS",env_name.Replace(" ","")+"EnvParameters");
        }
         using(StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/Scripts/Parameter/{0}.cs", new object[] { env_name.Replace(" ", "") + "EnvParameters" }))) {
            sw.Write(text);
        }
        AssetDatabase.Refresh();
    }
}
