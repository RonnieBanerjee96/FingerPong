using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMenu : MonoBehaviour
{
    GameObject ball;
    Vector2 ballPos;
    //int ballSpawned =0;
    Rigidbody2D ballRb;
    // Start is called before the first frame update
    void Start()
    {
        //ballSpawned =0;
        ball = gameObject;
        ballPos = new Vector2((Random.Range(-1f, 1f)), (Random.Range(0.5f, 1f)));
        ball.transform.position = ballPos;
        ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2((Random.Range(1f,-1f)), (Random.Range(0.5f, -0.5f))), ForceMode2D.Impulse);


    }

    void FixedUpdate()
    {
        RandomHits();
    }

    void RandomHits()
    {
        if ((Time.time > 3 && Time.time % 3 == 0) || (ballPos.x <= -2f) || (ballPos.x >= 2f))
        {
            ballRb.AddForce(new Vector2((Random.Range(1f, -1f)), (Random.Range(0.5f, -0.5f))), ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
  
}
