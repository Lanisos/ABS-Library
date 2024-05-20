using System.Collections;
using UnityEngine;
using MultipleEnums;
using System.Collections.Generic;

public class Date {

    private float amount;
    private time_amount time_type;

    public Date(float amount, time_amount time_type) {
        this.amount = amount;
        this.time_type = time_type;
    }

    public float GetAmount() {
        return amount;
    }

    public time_amount GetTimeType() {
        return time_type;
    }

    public void SetAmount (float amount) {
        this.amount = amount;
    }

    public void SetTimeType(time_amount time_type) {
        this.time_type = time_type;
    }

    public float ConvertToSeconds() {
        float final_amount = amount;
        switch (time_type) {
            case time_amount.millisecond:
                final_amount /= 1000;
                break;
            case time_amount.minute:
                final_amount *= 60;
                break;
            case time_amount.hour:
                final_amount *= 60 * 60;
                break;
            case time_amount.day:
                final_amount *= 60 * 60 * 24;
                break;
            case time_amount.month:
                final_amount *= 60 * 60 * 24 * 30;
                break;
            case time_amount.year:
                final_amount *= 60 * 60 * 24 * 30 * 12;
                break;
        }
        return final_amount;
    }
}
