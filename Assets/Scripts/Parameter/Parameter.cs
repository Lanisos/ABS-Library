using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected float step = 20f;
    [SerializeField] protected int stat_cicles = 300;
    //private bool first_loop = true;
    protected bool export_done = false;
}
