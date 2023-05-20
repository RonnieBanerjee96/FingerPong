using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour
{

    public delegate void GameOver();
    public static event GameOver gameOver;
    bool IsGameOver;
    public static int hitCounter=1;

    public static LineRenderer WallLR;
    Camera cam;
    GameObject wall;
    GameObject ball;
    AudioSource audio_wallHit;
    EdgeCollider2D ec2dWall;
    Rigidbody2D ballRb;



    Color randomColor;
    Scene currentScene;
    Vector2 contactPoint;
    Vector3 screenSize;
    Vector3 screenSizeO;
    

    Vector2 bottomLeft, topLeft, topRight, bottomRight;
    Vector2[] pointsArray = new Vector2[5];

    
    

    
    private void Awake()
    {
        ball = GameObject.Find("Ball");
        currentScene = SceneManager.GetActiveScene();
        cam = Camera.main;
        wall = GameObject.Find("Wall");
        ec2dWall = wall.GetComponent<EdgeCollider2D>();
        audio_wallHit = GetComponent<AudioSource>();
        WallLR = GetComponent<LineRenderer>();
        ballRb = ball.GetComponent<Rigidbody2D>();

        if (ec2dWall == null)
        {
            gameObject.AddComponent<EdgeCollider2D>();
        }


        CalculateScreenSize(cam); //Calculates Screen Size
        EdgeColliderWallMaker(ec2dWall,pointsArray); //Wraps E2CD Wall Around Screen
        RenderOutline(pointsArray); //Renders Outline



    }


    void Update()
    {
        if (GameOverChecker(hitCounter))
        {
            gameOver?.Invoke();
            
        }

        randomColor = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0.7f,1f));
        //Debug.Log("" + IsGameOver);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentScene.name == "GameScene" && collision.gameObject.tag == "Ball")
        {
            audio_wallHit.Play();
            WallLR.startColor = Color.red;
            hitCounter++;
        }

        if (currentScene.name == "Main Menu" && collision.gameObject.tag == "Ball")
        {
            
            audio_wallHit.Play();
            WallLR.startColor = randomColor;
            WallLR.endColor = randomColor;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Ball")
        {
            contactPoint = collision.GetContact(0).point;
            ballRb.AddForceAtPosition(new Vector2((Random.Range(1f, 2.5f)), (Random.Range(0f, 0f))), contactPoint, ForceMode2D.Impulse);
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentScene.name == "GameScene" && collision.gameObject.tag == "Ball")
            WallLR.startColor = Color.white;
    }

    
    
    bool GameOverChecker(int hitCount)
    {
        if (hitCount > 3) 
            IsGameOver = true;


        else
            IsGameOver=false;


        return IsGameOver;
    }

    //Renders Outline
    void RenderOutline(Vector2[] points)
    {
        WallLR.startWidth = 0.2f;
        WallLR.positionCount = 5;
        WallLR.material.color = Color.white;

        WallLR.SetPosition(0, points[0]);
        WallLR.SetPosition(1, points[1]);
        WallLR.SetPosition(2, points[2]);
        WallLR.SetPosition(3, points[3]);
        WallLR.SetPosition(4, points[4]);
    }

    //Wraps E2CD Wall Around Screen
    void EdgeColliderWallMaker(EdgeCollider2D ec, Vector2[] points)
    {
        ec.points = points;
    }


    //Calculates Screen Size
    void CalculateScreenSize(Camera camera)
    {
        screenSize =  camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f));
        screenSizeO = camera.ScreenToWorldPoint(new Vector3(0, 0, 10f));

        bottomLeft = new Vector2(screenSizeO.x, screenSizeO.y+ 0.8f);

        topLeft = new Vector2(screenSizeO.x, screenSize.y- 0.8f);

        topRight = new Vector2(screenSize.x, screenSize.y- 0.8f);

        bottomRight = new Vector2(screenSize.x, screenSizeO.y+0.8f);

        pointsArray[0] = bottomLeft;
        pointsArray[1] = topLeft;
        pointsArray[2] = topRight;
        pointsArray[3] = bottomRight;
        pointsArray[4] = bottomLeft;
    } 



}
