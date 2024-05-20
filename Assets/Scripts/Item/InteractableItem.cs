using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InteractableItem : PhysicalItem
{

    public new void Initialize(string name, agent_shape shape, float radius, float length, float height) {
        base.Initialize(name, shape, radius, length, height);
        CreateBaseParameters(name);
    }

    private void CreateBaseParameters(string name) {
        TextAsset base_file = new TextAsset();
        #if UNITY_EDITOR
        base_file = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Parameter/ParameterTemplate.txt", typeof(TextAsset)) as TextAsset;
        string text = "";
        if (base_file != null) {
            text = base_file.text;
            text = text.Replace("SPECIFIC_PARAMETERS",name.Replace(" ","")+"ItemParameters");
        }
         using(StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/Scripts/Parameter/{0}.cs", new object[] { name.Replace(" ", "") + "ItemParameters" }))) {
            sw.Write(text);
        }
        AssetDatabase.Refresh();
        #endif
    }
}
