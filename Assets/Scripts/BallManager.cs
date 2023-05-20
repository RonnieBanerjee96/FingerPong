using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BallManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject ball;
    Rigidbody2D ball_rb;
    CircleCollider2D ball_cc;
    bool hasCollidedPaddle = false;
    bool hasCollidedWall = false;


    void Start()
    {
        ball_rb = GetComponent<Rigidbody2D>();
        ball_cc = GetComponent<CircleCollider2D>();
        ball = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasCollidedPaddle)
        {
            ball_rb.gravityScale = 0.2f;
        }

        if (hasCollidedWall)
        {
            ball_rb.gravityScale = 1f;
        }
    }

    //Differentiates Collision between Wall and paddle.
    //and sets the gravity accordingly.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Paddle")
        {
            hasCollidedPaddle = true;
            hasCollidedWall = false;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            hasCollidedWall = true;
            hasCollidedPaddle = false;
        }
    }


    //Resets Graviy after ball has jumped again.

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        hasCollidedWall = false;
    //    }
    //}
}
