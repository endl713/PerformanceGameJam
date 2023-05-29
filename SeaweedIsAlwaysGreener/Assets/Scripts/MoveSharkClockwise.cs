using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSharkClockwise : MonoBehaviour
{
    //public Rigidbody sharkRB;
    public Transform shark;
    public float speed = 3.0f;
    public Boundary viewScreen = new Boundary();

    public float directionx;
    public float directiony;
    public float directionz;
    private readonly float xmax = 3.5f;
    //private readonly float ymax = 0.5f;
    private readonly float zmax = 3.5f;

    private readonly float startx = 3.4f;
    private readonly float starty = -0.2f;
    private readonly float startz = 3.4f;

    //private bool collidedWithHaven = false;
    //private float timeOfCollision = 0.0f;
    //private readonly float timeBetweenCollision = 1.0f;

    private Animator sharkAnimator;

    public PlayerTurtle attackTurtle;

    void Start()
    {
        sharkAnimator = GetComponent<Animator>();
        shark.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        shark.position = new Vector3(startx, starty, startz);
        directionx = 0.0f;
        directiony = 0.0f;
        directionz = -1.0f;

    }
    private void Update()
    {
        if (Mathf.Abs(directionz + transform.position.z) > zmax)
        {
            directionz = 0.0f;
            directionx = -1.0f * Mathf.Sign(transform.position.x);
            shark.rotation = Quaternion.Euler(0.0f, 90.0f + 90 * Mathf.Sign(directionx), 0.0f);
        }
        else if (Mathf.Abs(directionx + transform.position.x) > xmax)
        {
            directionx = 0.0f;
            directionz = -1.0f * Mathf.Sign(transform.position.z);
            shark.rotation = Quaternion.Euler(0.0f, 90 * Mathf.Sign(directionz), 0.0f);
        }

        Vector3 movement = new Vector3(directionx, directiony, directionz);
        shark.position += movement * speed * Time.deltaTime;
    }
   
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.tag == "Haven") 
        {
            collidedWithHaven = true;
            timeOfCollision = Time.time;
        }
        */
        if (other.tag == "Player")
        {
            sharkAnimator.SetTrigger("Bite");
            attackTurtle.TurtleBite();
        }
    }

}