using System.Collections;
using System.Collections.Generic;
using MultipleEnums;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class InteractableFactory : ItemFactory
{
    public override void CreateItem(string name, agent_shape shape, float radius, float length, float height)
    {
        GameObject item = new GameObject();
        item.name = name;
        item.tag = "Item";
        InteractableItem inter = item.AddComponent<InteractableItem>();
        inter.Initialize(name, shape, radius, length, height);
        if (!Directory.Exists("Assets/Prefabs/Created Items")) AssetDatabase.CreateFolder("Assets/Prefabs","Created Items");
        PrefabUtility.SaveAsPrefabAsset(item,"Assets/Prefabs/Created Items/"+name.Replace(" ","")+".prefab");
        Object.DestroyImmediate(item);
    }
}
