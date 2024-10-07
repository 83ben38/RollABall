using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    private InputManager input;

    private Transform player;

    private void Awake()
    {
        CameraController.controller = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        input = InputManager.instance;
        player = PlayerController.location;
        input.minMouse = 0;
    }
    
    

    // Update is called once per frame
    void FixedUpdate()
    {
        player = PlayerController.location;
        transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0,2,0),1);
        Vector3 look = new Vector3(-input.mouse.y, input.mouse.x, 0);
        Quaternion rotation = Quaternion.Euler(look);
        transform.rotation = rotation;
    }
}
