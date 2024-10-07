using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMoverController : MonoBehaviour
{
    public Vector3 previousOffset;

    public virtual void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }

    public void EnterPortal()
    {
        
    }

    public void ExitPortal()
    {
        
    }
}
