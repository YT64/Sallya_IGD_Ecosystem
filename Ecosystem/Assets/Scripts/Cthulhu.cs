using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cthulhu : MonoBehaviour
{
    public Sprite rest;
    public Sprite sorry;
    public Sprite angry;
    public float accel;
    public float maxSpeed;
    private Rigidbody2D rb;

    private int facing = 1;
    private AIState currentState = AIState.StateRest;
    private float angryTimestamp;
    private string collidedObjectTag;

    public enum AIState
    {
        //TODO: Change these to your own desired states
        StateRest,
        StateSorry,
        StateAngry,
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collidedObjectTag = collision.gameObject.tag;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collidedObjectTag = null;
    }

    private void Start()
    {
        StartState(currentState);
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void StartState(AIState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case AIState.StateRest:
                //your code here
                ChangeSprite(rest);
                //print("Cthulhu");
                if (facing != -1)
                {
                    facing = -(facing);
                }

                break;
            case AIState.StateSorry:
                //your code here
                ChangeSprite(sorry);
                //print("sorry!");
                Invoke("Despawn", 3.0f);


                break;
            case AIState.StateAngry:
                //your code here
                ChangeSprite(angry);
                //print("angry!");
                if (facing != 1)
                {
                    facing = -(facing);
                }
                angryTimestamp = Time.time;
                break;
        }
    }
    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.StateRest:
                //your code here
                fly();
                if (collidedObjectTag == "fish")
                {
                    StartState(AIState.StateSorry);
                }

                if (collidedObjectTag == "star")
                {
                    StartState(AIState.StateAngry);
                }

                break;
            case AIState.StateSorry:
                //your code here
                ChangeSprite(sorry);

                break;
            case AIState.StateAngry:
                //your code here
                accel = 4;
                fly();

                if (collidedObjectTag == "fish")
                {
                    StartState(AIState.StateSorry);
                }

                if (Time.time - angryTimestamp > 4.0f)
                {
                    StartState(AIState.StateRest);
                }

                break;
        }
    }

    private void EndState(AIState oldState)
    {
        switch (oldState)
        {
            case AIState.StateRest:
                //your code here

                break;
            case AIState.StateSorry:
                //your code here

                break;
            case AIState.StateAngry:
                //your code here
                accel = 1;
                break;
        }
    }
    private void fly()
    {
        rb.AddForce(Vector2.right * facing * accel * (maxSpeed - Mathf.Abs(rb.velocity.x)));
    }

    private void ChangeSprite(Sprite newSprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
