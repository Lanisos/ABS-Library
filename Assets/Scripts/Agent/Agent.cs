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

public class Agent : MonoBehaviour
{
    [SerializeField] protected string agent_name;

    [SerializeField] protected uint initial_population;

    [SerializeField] protected float vision_range;

    protected agent_shape current_shape;

    /*[SerializeField] private enum sprite {circle, cube, person};

    [SerializeField] private sprite sprite_type;*/

    //[SerializeField] private float speed;

    //private bool attach_parameters;
    //private bool attach_behaviour;

    protected bool check_behaviour = true;

    protected bool check_parameters = true;

    [SerializeField] protected Environment env;

    protected Vector2 env_collider;

    [SerializeField] [HideInInspector] protected List<GameObject> agent_units;

    protected List<Vector2> unit_positions;

    protected Color agent_color;

    protected Vector2 agent_size;

    protected float agent_radius;

    [SerializeField] [HideInInspector] protected bool done_initializing;

    [SerializeField] [HideInInspector] protected string relative_unit_path;

    //private float compilation_time = 1f;

    //private GameObject behaviour_placeholder;

    //private GameObject parameter_placeholder;

    /*Çpublic void InitializeAgent(string name, agent_shape shape, Color color, float vision, uint population, float radius, float circ_x, float circ_y, float caps_x, float caps_y) {
        done_initializing = false;
        agent_name = name;
        vision_range = (uint) (int) vision;
        initial_population = population;
        current_shape = shape;
        agent_color = color;
        agent_units = new List<GameObject>();
        CreateBaseBehaviour(selected,agent_name);
        CreateBaseParameters(agent_name);
        Debug.Log("Behaviour and Parameters created for Agent");
        GameObject agent_unit = new GameObject();
        agent_unit.name = agent_name + " Unit";
        agent_unit.layer = LayerMask.GetMask("Agents");
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
        if (!Directory.Exists("Assets/Resources")) AssetDatabase.CreateFolder("Assets", "Recources");
        else if (!Directory.Exists("Assets/Resources/Created Agent Units")) AssetDatabase.CreateFolder("Assets/Resources", "Created Agent Units");
        relative_unit_path = "Created Agent Units/"+agent_name.Replace(" ","")+"Unit";
        PrefabUtility.SaveAsPrefabAsset(agent_unit,"Assets/Resources/"+relative_unit_path+".prefab");
        Object.DestroyImmediate(agent_unit);
        /*for (int i = 0; i < initial_population; ++i) {
            AddAgentUnits(i);
        }*/
        //done_initializing = true;
    //}

    // Start is called before the first frame update
    
    void Awake() {

    }

    void Start()
    {
        unit_positions = new List<Vector2>();
        Debug.Log("Why");
    }

    void OnValidate() {
        Debug.Log(done_initializing);
        Debug.Log(initial_population);
        Debug.Log(this.gameObject.transform.hierarchyCount);
        /*if (done_initializing) {
            if (initial_population <= 0) initial_population = 1;
            if (initial_population < this.gameObject.transform.hierarchyCount-1) {
                Debug.Log(initial_population + " " + this.gameObject.transform.hierarchyCount);
                while (initial_population < this.gameObject.transform.hierarchyCount-1) {
                    DestroyAgentUnits(this.gameObject.transform.hierarchyCount-2);
                }
            }
            else if (initial_population > this.gameObject.transform.hierarchyCount-1) {
                while (initial_population > this.gameObject.transform.hierarchyCount-1) {
                    AddAgentUnits(this.gameObject.transform.hierarchyCount-1);
                }
            }
        }*/
    }

    public string GetAgentName() {
        return agent_name;
    }

    public Environment GetEnvironment() {
        return env;
    }

    public void AddAgentUnits(int i, Vector2 random_point) {
        //Debug.Log(relative_unit_path);
        UnityEngine.Object loaded_unit = Resources.Load(relative_unit_path);
        GameObject unit = (GameObject)Instantiate(loaded_unit,(Vector2)this.transform.position+random_point,Quaternion.identity);
        unit.name = i.ToString();
        unit.transform.parent = this.gameObject.transform;
        agent_units.Add(unit);
    }

    public void DestroyAgentUnits(int i) {
        //PrefabUtility.LoadPrefabContents();
        //Debug.Log(Application.isEditor);
        if (Application.isEditor) StartCoroutine(Destroy(this.gameObject.transform.GetChild(i).gameObject));
        else Object.Destroy(this.gameObject.transform.GetChild(i));
        agent_units.RemoveAt(i);
    }

    public void DistributeAgents(Vector2 collider) {
        if (this.gameObject.transform.childCount > 0) {
            foreach (Transform child in this.gameObject.transform) {
                GameObject.Destroy(child.transform.gameObject);
            }
        }
        env_collider = collider;
        unit_positions.Clear();
        unit_positions = new List<Vector2>();
        for (int i = 0; i < initial_population; ++i) {
            //GameObject child = this.gameObject.transform.GetChild(i).gameObject;
            Collider2D overlap = null;
            Vector2 random_point = new Vector2();
            do {
                float x = Random.Range(-collider.x/2,(collider.x/2)+1);
                float y = Random.Range(-collider.y/2,(collider.y/2)+1);
                random_point = new Vector2(x,y);
                overlap = Physics2D.OverlapCircle(random_point,agent_radius,LayerMask.GetMask("Agents"));
            } while (overlap == true);
            if (overlap == false) {
                unit_positions.Add(random_point);
                AddAgentUnits(i,random_point);
            }
        }
    }

    /*public void BreedAgents() {

    }*/

    public void SpawnMoreAgents(int number) {
        if (number > 0) {
            for (int i = 0; i < number; ++i) {
                //GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                Collider2D overlap = null;
                Vector2 random_point = new Vector2();
                do {
                    float x = Random.Range(-env_collider.x/2,(env_collider.x/2)+1);
                    float y = Random.Range(-env_collider.y/2,(env_collider.y/2)+1);
                    random_point = new Vector2(x,y);
                    overlap = Physics2D.OverlapCircle(random_point,agent_radius,LayerMask.GetMask("Agents"));
                } while (overlap == true);
                if (overlap == false) {
                    unit_positions.Add(random_point);
                    AddAgentUnits(i,random_point);
                }
            }
        }
    }

    public void ResetPositions() {
        for (int i = 0; i < initial_population; ++i) {
            GameObject child = this.gameObject.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.transform.position = unit_positions[i];
        }
    }

    public GameObject GetUnit(int id) {
        return agent_units[id];
    }

    public int GetUnitCount() {
        return agent_units.Count;
    }

    protected void CreateBaseParameters(string name) {
        TextAsset base_file = new TextAsset();
        #if UNITY_EDITOR
        base_file = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Parameter/ParameterTemplate.txt", typeof(TextAsset)) as TextAsset;
        string text = "";
        if (base_file != null) {
            text = base_file.text;
            text = text.Replace("SPECIFIC_PARAMETERS",name.Replace(" ","")+"Parameters");
        }
         using(StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/Scripts/Parameter/{0}.cs", new object[] { agent_name.Replace(" ", "") + "Parameters" }))) {
            sw.Write(text);
        }
        AssetDatabase.Refresh();
        #endif
    }

    private void CreateBaseBehaviour(types_of_agents type, string name) {
        TextAsset base_file = new TextAsset();
        #if UNITY_EDITOR
        switch (type) {
            case types_of_agents.reflex_simple:
                base_file = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Behaviour/BehaviourSimpleTemplate.txt", typeof(TextAsset)) as TextAsset;
                break;
            case types_of_agents.model_based:
                base_file = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Behaviour/BehaviourModelTemplate.txt", typeof(TextAsset)) as TextAsset;
                break;
        }
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

    private IEnumerator Destroy (GameObject destroyee) {
        yield return null;
        DestroyImmediate(destroyee);
    }

    public float GetRadius() {
        return agent_radius;
    }

    public Vector2 GetSize() {
        return agent_size;
    }

    public agent_shape GetShape() {
        return current_shape;
    }

    public List<int> GetPopulation() {
        List<int> ids = new List<int>();
        foreach (Transform child in this.gameObject.transform) {
            if (child.gameObject.activeSelf) {
                ids.Add(int.Parse(child.gameObject.name));
            }
        }
        return ids;
    }
}
