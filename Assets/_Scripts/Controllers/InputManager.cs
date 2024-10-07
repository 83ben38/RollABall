using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;
    public Controls controls;
    public Vector2 move;
    public Vector2 scroll;
    public Vector2 mouse;
    private float minScroll = -1000;
    private float maxScroll = -100;
    public float minMouse = 30;
    public float maxMouse = 60;


    public bool locked = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        controls = new Controls();
        controls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        scroll = new Vector2(0, -250);
        mouse = new Vector2(0, 45);
        controls.Game.Skip.performed += (c) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        controls.Game.Restart.performed += (c) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        controls.Camera.Cycle.performed += (c) => PlayerController.Cycle();
    }

    // Update is called once per frame
    void Update()
    {
        move = controls.Locomotion.Roll.ReadValue<Vector2>();
        scroll += controls.Camera.Scroll.ReadValue<Vector2>();
        scroll.y = Math.Min(scroll.y, maxScroll);
        scroll.y = Math.Max(scroll.y, minScroll);
        mouse += controls.Camera.Movement.ReadValue<Vector2>();
        mouse.y = Math.Min(mouse.y, maxMouse);
        mouse.y = Math.Max(mouse.y, minMouse);
    }
}
