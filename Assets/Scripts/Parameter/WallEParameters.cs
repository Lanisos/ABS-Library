using UnityEngine;
using System.Collections.Generic;

public class WallEParameters : Parameter {

    //Space to specify all needed parameters that affect a particular kind of agents
    [SerializeField] private int max_capacity = 5;
    private int curr_capacity;
    private Vector2 return_point;
    private Vector2 target_point;

    private Vector2 rand_direction;
    [SerializeField] private int rand_dir_cicle = 4;
    private int current_rand_cicle;

    private List<Statistic<int>> stat_list;
    private int total_amount;

    void Start() {
        curr_capacity = 0;
        current_rand_cicle = 0;
        total_amount = 0;
        //rand_dir_cicle = 4;
        return_point = this.gameObject.transform.position;
        stat_list = new List<Statistic<int>>();
        stat_list.Add(new Statistic<int>("Total Rock Amount",5,stat_cicles));
        //Debug.Log(return_point);
    }

    void FixedUpdate() {

    }

    public void CheckStatistics(float time) {
        foreach (Statistic<int> stat in stat_list) {
            //if (stat.IncrementCicles()) {
                switch (stat.GetName()) {
                    case "Total Rock Amount":
                        stat.RegisterValues(time,total_amount);
                        break;
                }
            //}
        }
    }

    public void ExportStatistics() {
        if (!export_done) {
            export_done = true;
            Experiment exp = new Experiment();
            exp.ExportStatisticsCSV<int>("WallESimple",stat_list);
            exp.FinishExporting();
        }
    }

    public void ResetStatistics() {
        foreach (Statistic<int> stat in stat_list) {
            stat.ResetValues();
        }
        total_amount = 0;
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

    public bool PickRock() {
        ++curr_capacity;
        ++total_amount;
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
}