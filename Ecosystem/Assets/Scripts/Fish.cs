using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Fish : MonoBehaviour
{
    public Sprite rest;
    public Sprite gothit;
    public Sprite scared;
    public float accel;
    public float maxSpeed;
    private Rigidbody2D rb;

    private int facing = -1;
    private AIState currentState = AIState.StateRest;
    private float hitTimestamp;
    private string collidedObjectTag;
    public enum AIState
    {
        //TODO: Change these to your own desired states
        StateRest,
        StateGotHit,
        StateScared,
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collidedObjectTag = collision.gameObject.tag;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collidedObjectTag = null;
    }

    void ChangeGravityScale(float newGravityScale)
    {
        // Set the new gravity scale
        rb.gravityScale = newGravityScale;
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
                //print("fish");
                if (facing != -1) 
                {
                    facing = -(facing);
                }


                break;
            case AIState.StateGotHit:
                //your code here
                ChangeSprite(gothit);
               //print("Got hit£¡");
                if(facing != 1)
                {
                    facing = -(facing);
                }
                currentState = AIState.StateGotHit;
                hitTimestamp = Time.time;


                break;
            case AIState.StateScared:
                //your code here
                ChangeSprite(scared);
                //print("Got kill£¡");
                currentState = AIState.StateScared;
                ChangeGravityScale(1.0f);
                Invoke("Despawn", 5.0f);


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
                if (collidedObjectTag == "wall")
                {
                    StartState(AIState.StateGotHit);
                }

                if (collidedObjectTag == "cthulhu")
                {
                    StartState(AIState.StateScared);
                }


                break;
            case AIState.StateGotHit:
                //your code here
                fly();
                
                if (collidedObjectTag == "cthulhu")
                {
                    StartState(AIState.StateScared);
                }

                if (Time.time - hitTimestamp > 3.0f) 
                {
                    StartState(AIState.StateRest); 
                }
                break;
            case AIState.StateScared:
                //your code here
                ChangeSprite(scared);

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
            case AIState.StateGotHit:
                //your code here

                break;
            case AIState.StateScared:
                //your code here

                break;
        }
    }
    private void fly()
    {
        rb.AddForce(Vector2.right * facing * accel * (maxSpeed - Mathf.Abs(rb.velocity.x)));
        rb.AddForce(Vector2.right * facing * accel * (maxSpeed - Mathf.Abs(rb.velocity.y)));
       
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
