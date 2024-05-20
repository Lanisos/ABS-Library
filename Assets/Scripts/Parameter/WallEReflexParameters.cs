using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class WallEReflexParameters : Parameter {

    //Space to specify all needed parameters that affect a particular kind of agents

    [SerializeField] private int max_capacity = 5;
    private int curr_capacity;
    private Vector2 return_point;
    private Vector2 target_point;
    //[SerializeField] private float speed = 4f;
    //[SerializeField] private float step = 20f;
    private Vector2 rand_direction;
    [SerializeField] private int rand_dir_cicle = 4;
    private int current_rand_cicle;
    //private Color[] rock_objectives = {Color.black, Color.grey, Color.red, Color.black};
    //private Color[] obj_copy;
    private List<Statistic<int>> stat_list;
    private int red_amount = 0;
    private int grey_amount = 0;
    private int black_amount = 0;
    //[SerializeField] private int stat_cicles = 300;
    //private bool first_loop = true;
    //private bool export_done = false;

    void Start() {
        curr_capacity = 0;
        current_rand_cicle = 0;
        red_amount = 0;
        grey_amount = 0;
        black_amount = 0;
        //max_capacity = rock_objectives.Length;
        //obj_copy = rock_objectives;
        //rand_dir_cicle = 4;
        return_point = this.gameObject.transform.position;
        //red_amount = new Statistic<int>("Red Rock Amount",5,300);
        //grey_amount = new Statistic<int>("Grey Rock Amount",5,300);
        //black_amount = new Statistic<int>("Black Rock Amount",5,300);
        stat_list = new List<Statistic<int>>();
        stat_list.Add(new Statistic<int>("Red Rock Amount",5,stat_cicles));
        stat_list.Add(new Statistic<int>("Grey Rock Amount",5,stat_cicles));
        stat_list.Add(new Statistic<int>("Black Rock Amount",5,stat_cicles));
        Debug.Log(return_point);
    }

    void FixedUpdate() {
        
    }

    public void CheckStatistics(float time) {
        foreach (Statistic<int> stat in stat_list) {
            //if (stat.IncrementCicles()) {
                switch (stat.GetName()) {
                    case "Red Rock Amount":
                        stat.RegisterValues(time,red_amount);
                        break;
                    case "Grey Rock Amount":
                        stat.RegisterValues(time,grey_amount);
                        break;
                    case "Black Rock Amount":
                        stat.RegisterValues(time,black_amount);
                        break;
                }
            //}
        }
    }

    public void ExportStatistics() {
        if (!export_done) {
            export_done = true;
            Experiment exp = new Experiment();
            exp.ExportStatisticsCSV<int>("WallEReflex",stat_list);
            exp.FinishExporting();
        }
    }

    public void ResetStatistics() {
        foreach (Statistic<int> stat in stat_list) {
            stat.ResetValues();
        }
        red_amount = 0;
        grey_amount = 0;
        black_amount = 0;
        export_done = false;
    }

    public Vector2 GetReturnPoint() {
        return return_point;
    }

    public int GetCurrentCapacity() {
        return curr_capacity;
    }

    public int GetMaxCapacity() {
        return max_capacity;
    }

    public bool PickRock(Color color) {
        ++curr_capacity;
        if (color == Color.red) red_amount++;
        else if (color == Color.black) black_amount++;
        else if (color == Color.grey) grey_amount++;
        /*int x = System.Array.IndexOf(rock_objectives,color);
        rock_objectives = rock_objectives.Where((value, id) => id != x).ToArray();*/
        return max_capacity == curr_capacity;
    }

    public int GetRandDirCicles() {
        return current_rand_cicle;
    }

    public void ResetRandCicles() {
        current_rand_cicle = 0;
    }

    public bool GoToRandDirection(int amount) {
        current_rand_cicle += amount;
        //Debug.Log(rand_dir_cicle);
        //Debug.Log(current_rand_cicle);
        //Debug.Log(current_rand_cicle%(rand_dir_cicle));
        return current_rand_cicle%(rand_dir_cicle) == 0;
    }

    public Vector2 GetRandDirection() {
        return rand_direction;
    }

    public void SetRandDirection(Vector2 dir) {
        rand_direction = dir;
    }

    public void ResetCapacity() {
        curr_capacity = 0;
        //rock_objectives = obj_copy;
    }

    public void SetTargetPoint(Vector2 point) {
        target_point = point;
    }

    public Vector2 GetTargetPoint() {
        return target_point;
    }

    public float GetSpeed() {
        return speed;
    }

    public float GetStep() {
        return step;
    }

    /*public bool RockInObjectives(Color color) {
        return rock_objectives.Contains(color);
    }*/
}