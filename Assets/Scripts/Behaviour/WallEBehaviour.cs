using UnityEngine;
using System.Collections;
using MultipleEnums;

public class WallEBehaviour : Behaviour {
    
    //Specify all agent states here
    protected new enum agent_states {wander, target, carry};

    //The initial state corresponds to this variable, which then becomes the current state of the agent
    [SerializeField] protected new agent_states initial_state = agent_states.wander;

    protected new agent_states current_state;

    protected new Environment env;

    protected new Rigidbody2D rb;

    private WallEParameters parameters;

    protected new SimulationTime timer;

    protected new Agent definition;

    private GameObject spawn_reference;

    void Start() {
        definition = this.gameObject.GetComponentInParent<Agent>();
        env = definition.GetEnvironment();
        parameters = this.gameObject.GetComponent<WallEParameters>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        timer = GameObject.Find("Simulation Time").GetComponent<SimulationTime>();
        current_state = initial_state;
        spawn_reference = new GameObject();
        spawn_reference.transform.position = this.gameObject.transform.position;
        SpriteRenderer spawn_sprite = spawn_reference.AddComponent<SpriteRenderer>();
        Debug.Log(this.gameObject.GetComponent<SpriteRenderer>().size);
        //spawn_sprite = this.gameObject.GetComponent<SpriteRenderer>();
        spawn_sprite.drawMode = SpriteDrawMode.Sliced;
        spawn_sprite.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        spawn_sprite.size = this.gameObject.GetComponent<SpriteRenderer>().size * 0.5f;
        spawn_sprite.color = Color.grey;
    }

    void FixedUpdate() {
        if (!env.IsPaused() && env.IsRunning()) {
            NormalBehaviour();
        }
        else rb.velocity = Vector2.zero;
        if (!env.IsRunning()) {
            current_state = initial_state;
            parameters.ResetCapacity();
            parameters.ResetRandCicles();
            if (!env.NeedsReset()) parameters.ResetStatistics();
        }
         if (env.NeedsReset()) {
            parameters.ExportStatistics();
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (!env.IsPaused() && env.IsRunning()) {
            if (collision.collider.gameObject.tag == "Agent") {
                OnAgentCollision(collision);
            }
            else if (collision.collider.gameObject.tag == "Item") {
                OnItemCollision(collision);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (!env.IsPaused() && env.IsRunning()) {
            if (collision.gameObject.tag == "Agent") {
                OnAgentVisionRange(collision);
            }
            else if (collision.gameObject.tag == "Item") {
                OnItemVisionRange(collision);
            }
        }
    }

    //To be used when there is no collision with anything or nothing is on sight. The bulk of an agent's behaviour.
    private void NormalBehaviour() {
        int turn_counter = 0;
        Debug.Log(current_state);
        switch (current_state) {
            case agent_states.wander:
                Debug.Log(timer.GetAmount());
                Debug.Log(parameters.GetRandDirection());
                if (parameters.GetRandDirection() == Vector2.zero || parameters.GoToRandDirection(timer.GetAmount())) {
                    Vector2 random_dir = new Vector2();
                    Debug.Log("I'm at least computing a random direction");
                    do {
                        random_dir = RandomDirection(-parameters.GetStep(),parameters.GetStep());
                        ++turn_counter;
                    }
                    while (!IsDirectionValid(random_dir) && turn_counter < 5);
                    parameters.SetRandDirection(random_dir);
                    parameters.ResetRandCicles();
                }
                rb.velocity = parameters.GetRandDirection() * parameters.GetSpeed() * timer.GetTimeAmount();
                break;
            case agent_states.target:
                Vector2 dir = parameters.GetTargetPoint() - (Vector2)transform.position;
                rb.velocity = dir * parameters.GetSpeed() * timer.GetTimeAmount();
                
                //rb.velocity = new Vector2(parameters.GetStep() ,rb.velocity.y);
                break;
            case agent_states.carry:
                if (transform.position.x >= parameters.GetReturnPoint().x-0.5 && transform.position.x <= parameters.GetReturnPoint().x+0.5 && transform.position.y >= parameters.GetReturnPoint().y-0.5 && transform.position.y <= parameters.GetReturnPoint().y+0.5) {
                    current_state = agent_states.wander;
                    parameters.ResetCapacity();
                }
                else {
                    Vector2 target_dir = parameters.GetReturnPoint() - (Vector2)transform.position;
                    if (IsDirectionValid(target_dir)) rb.velocity = target_dir * parameters.GetSpeed() * timer.GetTimeAmount();
                    else {
                        if (parameters.GetRandDirection() == Vector2.zero || parameters.GoToRandDirection(timer.GetAmount())) {
                            Vector2 alternate_dir = new Vector2();
                            do {
                                alternate_dir = RandomDirection(-parameters.GetStep(),parameters.GetStep());
                                ++turn_counter;
                            }
                            while (!IsDirectionValid(alternate_dir) && turn_counter < 5);
                            parameters.SetRandDirection(alternate_dir);
                            parameters.ResetRandCicles();
                        }
                        rb.velocity = parameters.GetRandDirection() * parameters.GetSpeed() * timer.GetTimeAmount();
                    }
                }
                break;
        }
        parameters.CheckStatistics(timer.GetCurrentTime());
    }
    
    //To be used when the current agent unit has collisioned with an item.
    private void OnItemCollision(Collision2D collision) {

    }
    
    //To be used when the current agent unit has collisioned with another agent, whether it's the same type or another one.
    private void OnAgentCollision(Collision2D collision) {
        if (current_state == agent_states.target) {
            bool max_reached = parameters.PickRock();
            collision.collider.gameObject.SetActive(false);
            if (max_reached) current_state = agent_states.carry;
            else current_state = agent_states.wander;
        }
    }
    
    //To be used when the agent unit has seen an item of interest inside its vision range.
    private void OnItemVisionRange(Collider2D collision) {

    } 

    //To be used when the agent unit has seen another agent inside its vision range.
    private void OnAgentVisionRange(Collider2D collision) {
        if (current_state == agent_states.wander) {
            current_state = agent_states.target;
            parameters.SetTargetPoint(collision.gameObject.transform.position);
        }
    } 

    private bool IsDirectionValid(Vector2 destiny) {
        //Debug.Log("Do I get here?");
        agent_shape shape = definition.GetShape();
        RaycastHit2D hit = new RaycastHit2D();
        if (shape == agent_shape.circle) hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(definition.GetRadius()/2,definition.GetRadius()/2),destiny,definition.GetRadius(),LayerMask.GetMask("Agents"));
        else {
            Vector2 size = definition.GetSize();
            float max_size = Mathf.Max(size.x,size.y);
            hit = Physics2D.Raycast(transform.position,destiny,max_size,LayerMask.GetMask("Agents"));
        }
        //Debug.Log("Do I get to this second part?");
        //Debug.Log(hit.collider != null);
        if (hit.collider != null && hit.collider.gameObject.activeSelf && hit.collider.gameObject != this.gameObject) return false;
        else return true;
    }

    private Vector2 RandomDirection(float left, float right) {
        float x = Random.Range(left,right);
        float y = Random.Range(left,right);
        return new Vector2(x,y);
    }

    private void OnDestroy() {
        env.ResetCamera();
        Object.Destroy(spawn_reference);
    }
}