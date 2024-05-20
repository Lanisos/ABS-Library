using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;

public abstract class AgentFactory : MonoBehaviour
{
    public abstract Agent CreateAgent(string name, agent_shape shape, Color color, float vision, int population, float radius, float circ_x, float circ_y, float caps_x, float caps_y);

    
}
