using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsWCurrent : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    private readonly float ymin = -0.2f;
    private readonly float ymax = 0.2f;


    private bool collidedWithHaven = false;
    private float timeOfCollision = 0.0f;
    private readonly float timeBetweenCollision = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (collidedWithHaven == true)
        {
            if (rb.position.z > 1.0f)
            {
                Vector3 movement = new Vector3(-1.0f, 0.0f, 1.0f);
                rb.MovePosition(rb.position + (movement * speed));
            }
            else
            {
                Vector3 movement = new Vector3(1.0f, 0.0f, -1.0f);
                rb.MovePosition(rb.position + (movement * speed));
            }

            if ((Time.time - timeOfCollision) > timeBetweenCollision)
            {
                collidedWithHaven = false;
            }
        }
        else 
        {
            
            Vector3 movement = new Vector3(-1.0f, 0.0f, -1.0f);
            rb.MovePosition(transform.position + (movement * speed*Time.deltaTime));
            rb.position = new Vector3(rb.position.x,Mathf.Clamp(rb.position.y, ymin, ymax),rb.position.z);
            rb.freezeRotation = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Haven") 
        {
            collidedWithHaven = true;
            timeOfCollision = Time.time;
        }
    }
}
