using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;

public class PhysicalItem : Item
{
    protected float radius;
    protected Vector2 size;
    protected agent_shape shape;

    public new void Initialize(string name, agent_shape shape, float radius, float length, float height) {
        base.Initialize(name, shape, radius, length, height);
        this.shape = shape;
        switch (shape) {
            case agent_shape.circle:
                CircleCollider2D circle = this.gameObject.AddComponent<CircleCollider2D>();
                circle.radius = radius;
                this.radius = radius;
                break;
            case agent_shape.cube:
                BoxCollider2D cube = this.gameObject.AddComponent<BoxCollider2D>();
                cube.size = new Vector2(length,height);
                this.size = new Vector2(length,height);
                break;
            case agent_shape.person:
                CapsuleCollider2D caps = this.gameObject.AddComponent<CapsuleCollider2D>();
                caps.size = new Vector2(length,height);
                this.size = new Vector2(length,height);
                break;
        }
    }
}
