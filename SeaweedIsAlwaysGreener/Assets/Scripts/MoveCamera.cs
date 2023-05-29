using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    void Update()
    {
        if (player != null) 
        { 
            transform.position = player.position + offset;
        }
        
    }
}
