using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float speed = 10f;

    public float moveOnTrigger = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
            
        if (p != null)
        {
            p.speed = speed;
            transform.position += new Vector3(0,moveOnTrigger,0);
        }
    }

    
}
