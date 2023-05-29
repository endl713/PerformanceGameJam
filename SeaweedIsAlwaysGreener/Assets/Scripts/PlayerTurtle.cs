using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public readonly float xmin = -7.0f;
    public readonly float xmax = 7.0f;
    public readonly float ymin = -1.0f;
    public readonly float ymax = 1.0f;
    public readonly float zmin = -7.0f;
    public readonly float zmax = 7.0f;

}

public class FoodForPoints
{

    private int FoodPoints;
    public void AddFood()
    {
        FoodPoints++;
    }
    public void SetFoodPoints(int HowManyPoints)
    {
        FoodPoints = HowManyPoints;
    }
    public int GetFoodPoints()
    {
        return FoodPoints;
    }

}
public class PlayerTurtle : MonoBehaviour
{
    public Rigidbody playerRB;
    public float speed;
    public Boundary viewScreen = new Boundary();
    public FoodForPoints myFood = new FoodForPoints();

    private bool setMove = false;
    private bool tookHaven = false;
    private float timeInHaven = 0.0f;
    private readonly float timeMinInHaven = 0.5f;

    private Animator turtleAnimator;

    public int PointTotalToWin;
    private bool GameOverStatus = false;
    private bool GameWinLoseStatus = false;

    public AudioSource addSound;

    public AudioClip gameIsOn;
    public AudioClip gotFood;
    public AudioClip onIsland;
    public AudioClip gameOver;
    public AudioClip gameWon;

    public void TurtleBite()
    {
        GameOverStatus = true;
        GameWinLoseStatus = false;
        Destroy(gameObject);
    }
    public bool GetGameOver()
    {
        return GameOverStatus;
    }
    public bool GetWinLose()
    {
        return GameWinLoseStatus;
    }
    public int GetPointCount()
    {
        return myFood.GetFoodPoints();
    }
    public int GetPointTotalToWin()
    {
        return PointTotalToWin;
    }

    void Start()
    {
        turtleAnimator = GetComponent<Animator>();
        turtleAnimator.SetBool("Move", setMove);
        playerRB = GetComponent<Rigidbody>();
        myFood.SetFoodPoints(0);
        tookHaven = true;
        setMove = false;
        playerRB.position = new Vector3(0.0f, 0.4f, 0.0f);
        timeInHaven = Time.time;
        addSound = GetComponent<AudioSource>();
        addSound.PlayOneShot(gameIsOn);
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (tookHaven == true)
        {
            if ((Time.time - timeInHaven) > timeMinInHaven) //Time wait before turtle can leave island
            {
                if (movement.x != 0.0f)
                {
                    tookHaven = false;
                    playerRB.position = new Vector3(2.0f * Mathf.Sign(movement.x), 0.05f, 0.0f);
                    print(playerRB.position);
                }
                else if (movement.z != 0.0f)
                {
                    tookHaven = false;
                    playerRB.position = new Vector3(0.0f, 0.05f, 2.0f * Mathf.Sign(movement.z));
                    print(playerRB.position);
                }
            }
        }
        else 
        {
            if (movement.x != 0.0f)
            {
                setMove = true;
                playerRB.rotation = Quaternion.Euler(0.0f, -90*Mathf.Sign(movement.x), 0.0f);
            }
            else if (movement.z != 0.0f)
            {
                setMove = true;
                playerRB.rotation = Quaternion.Euler(0.0f, 90+90*Mathf.Sign(movement.z), 0.0f);
            }
            else { setMove = false; }

            playerRB.velocity = movement * speed;
            playerRB.position = new Vector3(Mathf.Clamp(playerRB.position.x, viewScreen.xmin, viewScreen.xmax),
                                            Mathf.Clamp(playerRB.position.y, viewScreen.ymin, viewScreen.ymax),
                                            Mathf.Clamp(playerRB.position.z, viewScreen.zmin, viewScreen.zmax));
        }
        turtleAnimator.SetBool("Move", setMove);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reward")
        {
            addSound.PlayOneShot(gotFood);
            turtleAnimator.SetTrigger("Bite");
            myFood.AddFood();
            Destroy(other.gameObject);
            if (myFood.GetFoodPoints() == PointTotalToWin)
            {
                addSound.PlayOneShot(gameWon);
                GameOverStatus = true;
                GameWinLoseStatus = true;
                tookHaven = true;
                setMove = false;
                playerRB.position = new Vector3(0.0f, 0.4f, 0.0f);
                playerRB.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                timeInHaven = Time.time;
            }
            return;
        }
        /* TODO: add damage from ocean trash
        else if (other.tag == "WeakHazard"){}
        */
        else if (other.tag == "Haven") 
        {
            tookHaven = true;
            setMove = false;
            playerRB.position = new Vector3(0.0f, 0.4f, 0.0f);
            timeInHaven = Time.time;
            addSound.PlayOneShot(onIsland);
        }
    }

}
