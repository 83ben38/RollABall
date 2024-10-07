using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManipulator : MonoBehaviour
{
    public float size = (float)Math.Cbrt(2);
    private int cooldown = 0;
    public float moveOnTrigger = 0;
    public int trueCooldown = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown--;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        PlayerController p = other.GetComponent<PlayerController>();
            
        if (p != null && cooldown < 1)
        {
            other.transform.localScale*=size;
            other.GetComponent<Rigidbody>().mass *= size * size * size;
            cooldown = trueCooldown;
            transform.position += new Vector3(0,moveOnTrigger,0);
        }
    }
}
