using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputManager input;

    private Transform player;
    public static Transform controller;

    private void Awake()
    {
        controller = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        input = InputManager.instance;
        player = PlayerController.location;
    }
    
    

    // Update is called once per frame
    void FixedUpdate()
    {
        player = PlayerController.location;
        transform.position = Vector3.Lerp(transform.position, player.position,.3f);
        Vector3 look = new Vector3(input.mouse.y, input.mouse.x, 0);
        Quaternion rotation = Quaternion.Euler(look);
        transform.rotation = rotation;
    }
}
