using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;

public class StaticItem : PhysicalItem
{
    
    public new void Initialize(string name, agent_shape shape, float radius, float length, float height) {
        base.Initialize(name, shape, radius, length, height);
    }
}
