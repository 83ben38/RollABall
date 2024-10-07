using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float goalPosition;

    public float maxPush;
    private Rigidbody rb;
    public float force;

    public bool pushed = false;
    public bool locks;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goalPosition - transform.position.y >= maxPush)
        {
            pushed = true;
            if (locks)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x,goalPosition-maxPush,transform.position.z);
        }
        else
        {
            pushed = false;
        }

        if (transform.position.y < goalPosition)
        {
            rb.AddForce(0,force,0);
        }
        if (transform.position.y > goalPosition)
        {
            transform.position = new Vector3(transform.position.x,goalPosition,transform.position.z);
        }
    }
}
