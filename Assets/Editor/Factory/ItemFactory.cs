using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;

public abstract class ItemFactory : MonoBehaviour
{
    
    public abstract void CreateItem(string name, agent_shape shape, float radius, float length, float height);
}
