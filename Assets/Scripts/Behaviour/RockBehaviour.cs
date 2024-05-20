using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockBehaviour : Behaviour {
    
    //Specify all agent states here
    protected new enum agent_states {};

    //The initial state corresponds to this variable, which then becomes the current state of the agent
    [SerializeField] protected new agent_states initial_state;

    protected new agent_states current_state;

    protected new Environment env;

    protected new SimulationTime timer;

    protected new Agent definition;

    protected new Rigidbody2D rb;

    private static readonly Color[] rock_colors = {Color.black, Color.white, Color.red, Color.blue, Color.yellow, Color.green, Color.grey};

    void Start() {
        env = this.gameObject.transform.parent.gameObject.GetComponent<Agent>().GetEnvironment();
        definition = this.gameObject.GetComponentInParent<Agent>();
        env = definition.GetEnvironment();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        timer = GameObject.Find("Simulation Time").GetComponent<SimulationTime>();
        current_state = initial_state;
        var rnd = new System.Random();
        int i = rnd.Next(rock_colors.Length);
        gameObject.GetComponent<SpriteRenderer>().color = rock_colors[i];
    }

    void FixedUpdate() {
        if (!env.IsPaused() && env.IsRunning()) {
            NormalBehaviour();
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (!env.IsPaused() && env.IsRunning()) {
            if (collision.collider.gameObject.tag == "Agent") {
                OnAgentCollision();
            }
            else if (collision.collider.gameObject.tag == "Item") {
                OnItemCollision();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (!env.IsPaused() && env.IsRunning()) {
            if (collision.gameObject.tag == "Agent") {
                OnAgentVisionRange();
            }
            else if (collision.gameObject.tag == "Item") {
                OnItemVisionRange();
            }
        }
    }

    //To be used when there is no collision with anything or nothing is on sight. The bulk of an agent's behaviour.
    private void NormalBehaviour() {

    }
    
    //To be used when the current agent unit has collisioned with an item.
    private void OnItemCollision() {

    }
    
    //To be used when the current agent unit has collisioned with another agent, whether it's the same type or another one.
    private void OnAgentCollision() {

    }
    
    //To be used when the agent unit has seen an item of interest inside its vision range.
    private void OnItemVisionRange() {

    } 

    //To be used when the agent unit has seen another agent inside its vision range.
    private void OnAgentVisionRange() {

    } 

    private void OnDestroy() {
        env.ResetCamera();
    }
}