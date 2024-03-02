using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public Sprite rest;
    public Sprite attack;
    public Sprite hurt;
    private Rigidbody2D rb;
    private PolygonCollider2D starCollider;

    private AIState currentState = AIState.StateRest;
    private float restTimestamp;
    private string collidedObjectTag;
    

    public enum AIState
    {
        //TODO: Change these to your own desired states
        StateRest,
        StateAttack,
        StateHurt,
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
        starCollider = GetComponent<PolygonCollider2D>();
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

                print("Color out of space");
                restTimestamp = Time.time;


                break;
            case AIState.StateAttack:
                //your code here
                ChangeSprite(attack);
                print("prey!");
                UpdateColliderPoints();


                break;
            case AIState.StateHurt:
                //your code here
                ChangeSprite(hurt);
                print("hurt!");
                UpdateColliderPoints();
                Invoke("Despawn", 3.0f);
                
                break;
        }
    }
    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.StateRest:
                //your code here
                if (collidedObjectTag == "cthulhu")
                {
                    StartState(AIState.StateHurt);
                }

                else if (Time.time - restTimestamp > 3.0f)
                {
                    StartState(AIState.StateAttack);StartState(AIState.StateAttack);
                }

                break;
            case AIState.StateAttack:
                //your code here

                if (collidedObjectTag == "cthulhu")
                {
                    StartState(AIState.StateHurt);
                }

                else if (collidedObjectTag == "fish")
                {
                    print("Hit fish!");
                    Spawn();
                    StartState(AIState.StateRest);
                }


                break;
            case AIState.StateHurt:
                //your code here
                


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
            case AIState.StateAttack:
                //your code here

                break;
            case AIState.StateHurt:
                //your code here

                break;
        }
    }


    private void ChangeSprite(Sprite newSprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void UpdateColliderPoints()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && starCollider != null)
        {
            Vector2[] spriteBoundsPoints = GetSpriteBoundsPoints(spriteRenderer.sprite.bounds);

            // Set the PolygonCollider2D points
            starCollider.SetPath(0, spriteBoundsPoints);
        }

    }

    private Vector2[] GetSpriteBoundsPoints(Bounds bounds)
    {
        Vector2[] points = new Vector2[4];

        points[0] = new Vector2(bounds.min.x, bounds.min.y);
        points[1] = new Vector2(bounds.min.x, bounds.max.y);
        points[2] = new Vector2(bounds.max.x, bounds.max.y);
        points[3] = new Vector2(bounds.max.x, bounds.min.y);

        return points;
    }

    private void Spawn()
    {
        float spawnY = Random.Range(0.5f, 0.6f);
        float spawnX = Random.Range(0.08f, 0.8f);
        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(spawnX, spawnY));
        Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}