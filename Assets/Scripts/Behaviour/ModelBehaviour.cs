using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBehaviour : Behaviour
{
    protected enum agent_actions {};

    protected Queue<agent_actions> action_memory;

    protected List<agent_actions> objective_memory;

    protected List<agent_actions> initial_objective_memory;

    protected int memory_size;
}
