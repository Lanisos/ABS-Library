using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleEnums;
using TMPro;

public class SimulationTime : MonoBehaviour
{

    private const float MILLISECONDS = 0.001f;
    private const int MINUTES = 60;
    private const int HOURS = 3600; //MINUTES * 60
    private const int DAYS = 86400; //HOURS * 24
    private const int MONTHS = 2628000; //DAYS * 30
    private const int YEARS = 31536000; //MONTHS * 12

    private float current_time;
    [SerializeField] private time_amount time_type = time_amount.second;
    [SerializeField] private int amount = 1;

    private TMP_Text year_number;
    private TMP_Text month_number;
    private TMP_Text day_number;
    private TMP_Text hour_number;
    private TMP_Text minute_number;
    private TMP_Text second_number;
    private TMP_Text millisecond_number;

    private double years;
    private double months;
    private double days;
    private double hours;
    private double minutes;
    private double seconds;
    private double milliseconds;

    private Environment env;

    // Start is called before the first frame update
    void Start()
    {
        current_time += amount * Time.fixedDeltaTime;
        env = GameObject.Find("Environment Space").GetComponent<Environment>();
        year_number = GameObject.Find("Years").GetComponent<TMP_Text>();
        month_number = GameObject.Find("Months").GetComponent<TMP_Text>();
        day_number = GameObject.Find("Days").GetComponent<TMP_Text>();
        hour_number = GameObject.Find("Hours").GetComponent<TMP_Text>();
        minute_number = GameObject.Find("Minutes").GetComponent<TMP_Text>();
        second_number = GameObject.Find("Seconds").GetComponent<TMP_Text>();
        millisecond_number = GameObject.Find("Milliseconds").GetComponent<TMP_Text>();
    }

    public void InitializeTimer(time_amount type, int quant) {
        time_type = type;
        amount = quant;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!env.IsPaused() && env.IsRunning()) {
            switch (time_type) {
                case time_amount.millisecond:
                    current_time += MILLISECONDS * amount * Time.fixedDeltaTime;
                    break;
                case time_amount.second:
                    current_time += amount * Time.fixedDeltaTime;
                    break;
                case time_amount.minute:
                    current_time += MINUTES * amount * Time.fixedDeltaTime;
                    break;
                case time_amount.hour:
                    current_time += HOURS * amount * Time.fixedDeltaTime;
                    break;
                case time_amount.day:
                    current_time += DAYS * amount * Time.fixedDeltaTime;
                    break;
                case time_amount.month:
                    current_time += MONTHS * amount * Time.fixedDeltaTime;
                    break;
                case time_amount.year:
                    current_time += YEARS * amount * Time.fixedDeltaTime;
                    break;
            }
            milliseconds = current_time * 1000;
            millisecond_number.text = ((int)milliseconds%(1000)).ToString();
            seconds = current_time;
            second_number.text = ((int)seconds%(60)).ToString();
            minutes = seconds / 60;
            minute_number.text = ((int)minutes%(60)).ToString();
            hours = minutes / 60;
            hour_number.text = ((int)hours%(24)).ToString();
            days = hours / 24;
            day_number.text = ((int)days%(365/12)).ToString();
            months = days / (365/12);
            month_number.text = ((int)months%(12)).ToString();
            years = months / 12; 
            year_number.text = ((int)years).ToString();
        }
        else if (!env.IsRunning()) ResetTime();
    }

    public float GetCurrentTime() {
        return current_time;
    }

    public int GetAmount() {
        return amount;
    }

    public float GetTimeAmount() {
        switch (time_type) {
            case time_amount.millisecond:
                return MILLISECONDS * amount * Time.fixedDeltaTime;
            case time_amount.second:
                return amount * Time.fixedDeltaTime;
            case time_amount.minute:
                return MINUTES * amount * Time.fixedDeltaTime;
            case time_amount.hour:
                return HOURS * amount * Time.fixedDeltaTime;
            case time_amount.day:
                return DAYS * amount * Time.fixedDeltaTime;
            case time_amount.month:
                return MONTHS * amount * Time.fixedDeltaTime;
            case time_amount.year:
                return YEARS * amount * Time.fixedDeltaTime;
            default:
                return amount * Time.fixedDeltaTime;
        }
    }

    public void ResetTime() {
        current_time = 0;
        years = 0;
        months = 0;
        days = 0;
        hours = 0;
        minutes = 0;
        seconds = 0;
        milliseconds = 0;
        millisecond_number.text = milliseconds.ToString();
        second_number.text = seconds.ToString();
        minute_number.text = minutes.ToString();
        hour_number.text = hours.ToString();
        day_number.text = days.ToString();
        month_number.text = months.ToString();
        year_number.text = years.ToString();
    }
}
