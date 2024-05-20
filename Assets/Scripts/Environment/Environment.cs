using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;
using Cinemachine;

public class Environment : MonoBehaviour
{

    private bool is_paused;
    private bool running;
    private bool units_set;
    private bool need_to_reset;
    private bool agent_perspective;
    private List<Agent> env_agents;
    private Transform background_trans;

    private Button set_button;
    private Button start_button;
    private Button stop_button;
    private Button reset_button;

    private CinemachineVirtualCamera follow_camera;

    private CinemachineVirtualCamera original_follow_camera;

    private Camera env_camera;

    private Camera original_camera;

    // Start is called before the first frame update
    void Start()
    {
        running = false;
        is_paused = false;
        units_set = false;
        need_to_reset = false;
        agent_perspective = false;
        env_agents = new List<Agent>();
        env_agents.AddRange(this.gameObject.GetComponentsInChildren<Agent>());
        background_trans = this.gameObject.transform.Find("Background"); 
        set_button = GameObject.Find("SetupButton").GetComponent<Button>();
        start_button = GameObject.Find("StartButton").GetComponent<Button>();
        stop_button = GameObject.Find("StopButton").GetComponent<Button>();
        reset_button = GameObject.Find("ResetButton").GetComponent<Button>();
        start_button.interactable = false;
        stop_button.interactable = false;
        reset_button.interactable = false;
        env_camera = this.gameObject.transform.parent.Find("Environment Camera").gameObject.GetComponent<Camera>();
        follow_camera = env_camera.transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>();
        follow_camera.Follow = background_trans;
        set_button.onClick.AddListener(PushSetUnits);
        start_button.onClick.AddListener(PushStartSim);
        stop_button.onClick.AddListener(PushStopSim);
        reset_button.onClick.AddListener(PushResetSim);
        /*agent_type_names = GameObject.Find("Agent Type Name").GetComponent<Dropdown>();
        unit_ids = GameObject.Find("Unit ID").GetComponent<Dropdown>();
        agent_ids = new Dictionary<string, List<int>>();
        foreach (Agent i in env_agents) {
            agent_ids.Add(i.GetAgentName(),i.GetPopulation());
        }
        Debug.Log(agent_ids.Keys.ToList<string>());
        agent_type_names.AddOptions(agent_ids.Keys.ToList<string>());
        List<string> conversion = new List<string>();
        foreach (int i in agent_ids.First().Value) {
            conversion.Add(i.ToString());
        }
        unit_ids.AddOptions(conversion);*/

    }

    public void PushSetUnits() {
        if (!running && !need_to_reset) {
            foreach (Agent i in env_agents) {
                i.DistributeAgents(background_trans.localScale); //.rect.
            }
            units_set = true;
            start_button.interactable = true;
        }
    }

    public void PushStartSim() {
        if (!running && units_set) {
            running = true;
            set_button.interactable = false;
            stop_button.interactable = true;
        }
        else if (running) {
            if (is_paused) {
                is_paused = false;
            }
            else {
                is_paused = true;
            }
        }
    }

    public void PushStopSim() {
        if (running) {
            running = false;
            units_set = false;
            is_paused = false;
            need_to_reset = true;
            stop_button.interactable = false;
            start_button.interactable = false;
            reset_button.interactable = true;
        }
    }

    public void PushResetSim() {
        if (!running && need_to_reset) {
            foreach (Agent i in env_agents) {
                i.ResetPositions();
            }
            need_to_reset = false;
            units_set = true;
            reset_button.interactable = false;
            start_button.interactable = true;
            set_button.interactable = true;
        }
    }

    public bool IsRunning() {
        return running;
    }

    public bool IsPaused() {
        return is_paused;
    }

    public bool NeedsReset() {
        return need_to_reset;
    }

    public void ResetCamera() {
        //follow_camera.gameObject.transform.position = Vector3.zero;
        //env_camera.gameObject.transform.position = new Vector3(0,0,-10);
        if (running) follow_camera.m_Lens.OrthographicSize *= 3;
        follow_camera.Follow = background_trans;
        
    }

    private void FixedUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Click detected");
            Vector2 cursor = (Vector2)env_camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cursor,Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "Agent") {
                Debug.Log("An agent has been hit");
                if (follow_camera.Follow == background_trans) {
                    Debug.Log("Is null");
                    follow_camera.m_Lens.OrthographicSize /= 3;
                    follow_camera.Follow = hit.collider.transform;
                }
                else {
                    Debug.Log("Not null");
                    ResetCamera();
                }
            }
        }
    }

    /*public void FollowAgentUnit(string agent_type, int id) {
        Agent target = GameObject.Find(agent_type).GetComponent<Agent>();
        GameObject followee = target.GetUnit(id);
    }

    public void ResetUnitDropdown() {
        List<int> ids = agent_ids[unit_ids.options[unit_ids.value].text];
        List<string> conversion = new List<string>();
        foreach (int i in ids) {
            conversion.Add(i.ToString());
        }
        unit_ids.ClearOptions();
        unit_ids.AddOptions(conversion);
    }

    public void UpdateUnitDropdown(int val) {
        for (int i = 0; i < unit_ids.options.Count; ++i) {
            if (unit_ids.options[i].text == val.ToString()) {
                unit_ids.options.RemoveAt(i);
                break;
            }
        }
    }*/
}
