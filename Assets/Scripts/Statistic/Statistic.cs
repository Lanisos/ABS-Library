using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Statistic {
    //public abstract Type Type { get;}
    
}

public class Statistic<T> : Statistic {
    //private K name;
    private string stat_name;
    private List<T> values;
    private List<float> time_stamps;
    private int time_step;
    private int cicles;
    private int current_cicles;
    private int registry;
    private Environment env;

    public Statistic(string name, int time_step, int cicles) {
        stat_name = name;
        this.time_step = time_step;
        this.cicles = cicles;
        current_cicles = 0;
        values = new List<T>();
        time_stamps = new List<float>();
        env = GameObject.Find("Environment Space").GetComponent<Environment>();
    } 

    public List<T> GetValues() {
        return values;
    }

    public List<float> GetTimeStamps() {
        return time_stamps;
    }

    public int GetLenght() {
        return values.Count;
    }

    public string GetName() {
        return stat_name;
    }

    public bool IncrementCicles() {
        current_cicles += time_step;
        return current_cicles >= cicles;
    }

    public T GetValueAt(int i) {
        return values[i];
    }

    public float GetStampAt(int i) {
        return time_stamps[i];
    }

    public void ResetValues() {
        values.Clear();
        time_stamps.Clear();
    }

    public void ResetCicles() {
        current_cicles -= cicles;
    }

    public void RegisterValues(float stamp, T value) {
        current_cicles += time_step;
        if (current_cicles >= cicles) {
            ++registry;
            ResetCicles();
            values.Add(value);
            time_stamps.Add(stamp);
        }
    }
}