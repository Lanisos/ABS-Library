using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedRocksStatistic : MonoBehaviour
{
    private List<float> time_stamps;
    private List<int> amount_stamps;

    private List<int> red_amount;
    private List<int> black_amount;
    private List<int> grey_amount;

    [SerializeField] private int cicle_amount = 100;
    private int current_cicles;

    void Start()
    {
        time_stamps = new List<float>();
        amount_stamps = new List<int>();
        red_amount = new List<int>();
        black_amount = new List<int>();
        grey_amount = new List<int>();
        current_cicles = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
