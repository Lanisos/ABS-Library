using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;

public class Item : MonoBehaviour
{
    protected string item_name;

    public void Initialize(string name, agent_shape shape, float radius, float length, float height) {
        item_name = name;
        SpriteRenderer sp = this.gameObject.AddComponent<SpriteRenderer>();
        sp.drawMode = SpriteDrawMode.Sliced;
        switch (shape) {
            case agent_shape.circle:
                //CircleCollider2D circle = this.gameObject.AddComponent<CircleCollider2D>();
                //circle.radius = radius;
                sp.size = new Vector2(radius*2,radius*2);
                break;
            case agent_shape.cube:
            case agent_shape.person:
                sp.size = new Vector2(length,height);
                break;
        }
    }
}
