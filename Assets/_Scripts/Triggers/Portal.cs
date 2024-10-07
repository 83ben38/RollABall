using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;

    public MeshRenderer screen;

    private Camera playerCam;

    private Camera portalCam;

    private RenderTexture viewTexture;

    public List<PortalMoverController> trackedObjects;

    public static int recursionLimit = 5;
    // Start is called before the first frame update
    void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    private void Update()
    {
        Render();
        
        for (int i = 0; i < trackedObjects.Count; i++)
        {
            PortalMoverController obj3ct = trackedObjects[i];
            Transform objectT = obj3ct.transform;
            Vector3 offsetFromPortal = objectT.position - transform.position;
            int portalSide = Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward));
            int portalSideOld = Math.Sign(Vector3.Dot(obj3ct.previousOffset, transform.forward));
            if (portalSide != portalSideOld)
            {
                var matrix = otherPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix *
                             objectT.localToWorldMatrix;
                obj3ct.Teleport(transform,otherPortal.transform,matrix.GetColumn(3),matrix.rotation);
                otherPortal.OnObjectEnterPortal(obj3ct);
                trackedObjects.RemoveAt(i);
                i--;
            }

            obj3ct.previousOffset = offsetFromPortal;
        }
    }
    
    void OnObjectEnterPortal (PortalMoverController obj3ct)
    {
        if (!trackedObjects.Contains(obj3ct))
        {
            obj3ct.previousOffset = obj3ct.transform.position - transform.position;
            trackedObjects.Add(obj3ct);
            obj3ct.EnterPortal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj3ct = other.GetComponent<PortalMoverController>();
        if (obj3ct != null)
        {
            OnObjectEnterPortal(obj3ct);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj3ct = other.GetComponent<PortalMoverController>();
        if (obj3ct!= null && trackedObjects.Contains(obj3ct))
        {
            trackedObjects.Remove(obj3ct);
            obj3ct.ExitPortal();
        }
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }

            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCam.targetTexture = viewTexture;
            otherPortal.screen.material.SetTexture("_Tex",viewTexture);
            
        }
    }

    public void Render()
    {
        CreateViewTexture();
        screen.enabled = false;
        Matrix4x4 localToWorldMatix = playerCam.transform.localToWorldMatrix;
        Matrix4x4[] matrices = new Matrix4x4[recursionLimit];
        for (int i = 0; i < recursionLimit; i++)
        {
            localToWorldMatix = transform.localToWorldMatrix * otherPortal.transform.worldToLocalMatrix *
                                localToWorldMatix;
            matrices[recursionLimit - i - 1] = localToWorldMatix;
        }

  
        for (int i = 0; i < recursionLimit; i++)
        {
            portalCam.transform.SetPositionAndRotation(matrices[i].GetColumn(3),matrices[i].rotation);
            SetNearClipPlane();
            portalCam.Render();
        }

        screen.enabled = true;
    }

    void SetNearClipPlane()
    {
        Transform clipPlane = transform;
        int dot = Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - portalCam.transform.position));
        Vector3 camSpacePos = portalCam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalCam.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot(camSpacePos, camSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);
        portalCam.projectionMatrix = playerCam.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }
}
