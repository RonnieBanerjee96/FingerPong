using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LineControl : MonoBehaviour
{
    AudioSource audio_bounce;
    public GameObject ball;
    Vector3 ScreenSize;


    [SerializeField] Vector3 mousePosStrt;
    [SerializeField] Vector3 mousePosEnd;
    [SerializeField] float ScreenHeightBy2;

    EdgeCollider2D ec;
    LineRenderer lr;
    Camera cam;
    int player;
    Vector2[] colliderPoints = new Vector2[2];
    float distanceBtwn;
    bool drawable;
    Rigidbody2D ballRb;
    Vector2 _contactPoint;
    bool isBallHit;
    
    // Start is called before the first frame update
    void Start()
    {
        audio_bounce = gameObject.GetComponent<AudioSource>();
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        lr.enabled = false;
        ec.enabled = false;
        player = 1;
        ballRb = ball.GetComponent<Rigidbody2D>();
        isBallHit = false;
        lr.startWidth = 0.07f;
        lr.endWidth = 0.09f;

        
        ScreenSize = cam.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0f));

    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time > i)
        //{
        //    i += 2;
        //    Instantiate(ball, new Vector3(0f, 0f, 0f), Quaternion.identity);
        //}

        //Vector2[] colliderPoints;
        
        lr.positionCount = 2;
        mousePosStrt = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosStrt.z = 10;
        mousePosEnd = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosEnd.z = 10;
        drawable = Drawable();


       

        //Debug.Log("Distance Between = " + distanceBtwn);
        //Debug.Log("Player=" + player);


        if (Input.GetMouseButtonDown(0) && drawable)
        {
            CancelInvoke("DisableLine");
            lr.enabled = true;
            ec.enabled = true;
            DrawLineStartPosition();



        }


        if (Input.GetMouseButton(0) && drawable)
        {
            DrawLineEndPos();
            colliderPoints[0] = new Vector2(lr.GetPosition(0).x, lr.GetPosition(0).y);
            colliderPoints[1] = new Vector2(lr.GetPosition(1).x, lr.GetPosition(1).y);
            ec.points = colliderPoints;
            //Debug.Log("Distance Between = " + distanceBtwn);
            Invoke("DisableLine", 1);
        }


        if (Input.GetMouseButtonUp(0) && drawable)
        {
            //Debug.Log("Player" + player);
            DisableLine();
            player = PlayerSwapper(player);
        }  



        if (!drawable)
        {
            DisableLine();
        }



        


    }


    void FixedUpdate()
    {
        if (isBallHit)
        {
            Invoke("hitBall",0f);
            //Debug.Log(isBallHit);
        }
        

        else if(!isBallHit)
        {
            CancelInvoke("hitBall");
        }
    }

    void DisableLine()
    {
        lr.enabled = false;
        ec.enabled = false;
    }

    void DrawLineStartPosition()
    {
        
        lr.SetPosition(0, mousePosStrt);
        
    }

    void DrawLineEndPos()
    {
        Vector3 LineStrt = lr.GetPosition(0);
        distanceBtwn = Vector3.Distance(mousePosEnd, LineStrt);
        if (distanceBtwn <= 2)
        {
            lr.SetPosition(1, mousePosEnd);
            
        }
            

        else if (distanceBtwn >= 2)
        {
            lr.enabled = false;
            ec.enabled = false;
            player = PlayerSwapper(player);
        }

        

    }

    bool Drawable()
    {
       if (player == 1 && mousePosStrt.y <= 0.5f)
        {
            return true;
            

        }

        if (player == 2 && mousePosStrt.y >= -0.5f)
        {
            return true;
            
            
        }

        else
            return false;
    }

    void hitBall()
    {
        if (player ==1)
        ballRb.AddForceAtPosition(new Vector2((Random.Range(1f,-1f)), (Random.Range(0f,1f))),_contactPoint, ForceMode2D.Impulse);

        else if (player ==2)
        ballRb.AddForceAtPosition(new Vector2((Random.Range(1f,-1f)), (Random.Range(0f,-1f))),_contactPoint, ForceMode2D.Impulse);
        
    }

    int PlayerSwapper (int player)
    {
        

        if(player == 1)
        {
            player = 2;
        }

        else if (player == 2)
        {
            player = 1;
        }

        else
        {
            player = 1;
        }

        return player;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            audio_bounce.Play();
            _contactPoint = col.GetContact(0).point;
             isBallHit = true;

        }
    }

     void OnCollisionExit2D(Collision2D col) {

        if (col.gameObject.tag == "Ball")
        {
            
             isBallHit = false;

        }
        
    }

    


}
