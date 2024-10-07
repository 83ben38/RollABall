using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager input;

    public float speed = 10f;

    public float jump = 10f;

    private Rigidbody rb;
    public static Transform location;
    public bool main = false;
    private Transform cam;
    [SerializeField] private  Material mainMaterialRef;
    [SerializeField] private Material sideMaterialRef;
     private static Material sideMaterial;
     private static Material mainMaterial;
     public static ArrayList players;
    private void Awake()
    {
        if (main)
        {
            location = transform;
            players = new ArrayList();
        }

        if (mainMaterialRef != null)
        {
            sideMaterial = sideMaterialRef;
            mainMaterial = mainMaterialRef;
        }
        players.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        input = InputManager.instance;
        rb = GetComponent<Rigidbody>();
        cam = CameraController.controller;
        input.controls.Locomotion.Jump.performed += Jump;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = input.move.x*rb.mass*cam.transform.right + input.move.y*rb.mass*cam.transform.forward;
        rb.AddForce(movement*speed);
    }

    void Jump(InputAction.CallbackContext x)
    {
        if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.3f))
        {
            rb.AddForce(0, ((jump * 24f) + 9.8f) * rb.mass, 0);
        }
    }

    private void OnDestroy()
    {
        input.controls.Locomotion.Jump.performed -= Jump;
    }

    public static void Cycle()
    {
        players.Add(players[0]);
        ((PlayerController)players[0]).GetComponent<Renderer>().material = sideMaterial;
        ((PlayerController)players[0]).main = false;
        players.RemoveAt(0);
        ((PlayerController)players[0]).GetComponent<Renderer>().material = mainMaterial;
        ((PlayerController)players[0]).main = true;
        location = ((PlayerController)players[0]).transform;
    }
}
