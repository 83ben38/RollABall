using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 move;
    private float amount;
    public float baseAmount;
    public Button button;

    public float delay;

    private float remainingDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        amount = baseAmount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (remainingDelay > 0)
        {
            remainingDelay--;
            return;
        }
        if (button != null)
        {
            if (button.pushed && amount > 0)
            {
                transform.position += move;
                amount--;
            }
            else if (button.pushed)
            {
                remainingDelay = delay;
            }
            else if (!button.pushed && amount < baseAmount)
            {
                transform.position -= move;
                amount++;
            }
        }
    }
}
