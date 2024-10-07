using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Duplicator : MonoBehaviour
{

    public GameObject prefab;

    public float scale =(float)Math.Cbrt(2);
    // Start is called before the first frame update

    private int cooldown = 0;
    public int trueCooldown = 60;
    public float moveOnTrigger = 0;
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
            GameObject d = Instantiate(prefab, other.transform.position, Quaternion.identity);
            other.transform.localScale*=scale;
            other.GetComponent<Rigidbody>().mass *= scale * scale * scale;
            d.transform.localScale = other.transform.localScale;
            d.GetComponent<Rigidbody>().mass = other.GetComponent<Rigidbody>().mass;
            d.GetComponent<PlayerController>().speed = p.speed;
            cooldown = trueCooldown;
            transform.position += new Vector3(0,moveOnTrigger,0);
        }
    }
}
