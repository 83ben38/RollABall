using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    private InputManager input;
    // Start is called before the first frame update
    void Start()
    {
        input = InputManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.position += transform.forward * input.scroll.y * 0.05f;
        while (Physics.Raycast(transform.position, transform.forward, Vector3.Distance(transform.localPosition, new Vector3(0, 0, 0))-PlayerController.location.localScale.y))
        {
            transform.position += transform.forward;
        }
    }
}
