using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    protected enum agent_states { };
    //[SerializeField] private state initial_state;
    protected agent_states current_state;

    protected agent_states initial_state;

    protected Environment env;

    protected Rigidbody2D rb;

    protected SimulationTime timer;

    protected Agent definition;
    
}
