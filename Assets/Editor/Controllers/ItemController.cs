using System.Collections;
using System.Collections.Generic;
using MultipleEnums;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemController 
{
    private static ItemController instance;

    private ItemController() {

    }

    public static ItemController GetInstance() {
        if (instance == null) instance = new ItemController();
        return instance;
    }

    public int CreateItem(types_of_items type, string name, agent_shape shape, float radius, float length, float height) {
        if (name.Length == 0 || name.Length > 50) return 1; //1 = Item name is either empty or too long
        Regex checker = new Regex("^[a-zA-Z0-9]*$");
        if (!checker.IsMatch(name)) return 2; //2 = Item name is not alphanumerical
        if (File.Exists(Application.dataPath+"/Prefabs/Created Items/"+name+".prefab")) return 3; //3 = Item already exists
        //GameObject loade_item = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Item_base.prefab");
        ItemFactory factory = null;
        switch (type) {
            case types_of_items.decorative:
                factory = new DecorativeFactory();
                break;
            case types_of_items.static_item:
                factory = new StaticFactory();
                break;
            case types_of_items.interactable:
                factory = new InteractableFactory();
                break;
        }
        factory.CreateItem(name, shape, radius, length, height);
        return 0;
    }

    public int DestroyItem(string name) {
        if (name.Length == 0 || name.Length >= 50) return 1; //1 = Item name is either empty or too long
        Regex checker = new Regex("^[a-zA-Z0-9]*$");
        if (!checker.IsMatch(name)) return 2; //2 = Item name is not alphanumerical
        if (File.Exists(Application.dataPath+"/Prefabs/Created Items/"+name+".prefab")) {
            List<string> failed = new List<string>();
            AssetDatabase.DeleteAssets(new string [] {"Assets/Prefabs/Created Items/"+name+".prefab","Assets/Scripts/Parameter/"+name+"ItemParameters.cs"},failed);
            return 0;
        }
        else return 3; //3 = Item does not exist
    }
}
