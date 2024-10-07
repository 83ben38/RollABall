using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jump;
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
        Rigidbody r = other.GetComponent<Rigidbody>();
            
        if (p != null)
        {
            r.AddForce(0,((p.jump*24f*jump)+9.8f)*r.mass,0);
        }
    }
}
