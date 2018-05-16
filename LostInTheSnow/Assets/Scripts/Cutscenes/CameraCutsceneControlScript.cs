using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraCutsceneControlScript : MonoBehaviour
{
    private GameObject cameraTarget;
    private float smoothSpeed = 0.0f;
    private float t = 0.0f;
    private float xLock = 0, yLock = 0;
    private Vector3 lockPosition;
    private Quaternion lockRotation;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void LockCameraToObject(string tagToTarget)
    {
        cameraTarget = GameObject.FindGameObjectWithTag(tagToTarget);
    }

    public void UnlockCamera()
    {
        cameraTarget = null;
    }

    public void SetSmoothSpeed(float smoothingSpeed)
    {
        smoothSpeed = smoothingSpeed;
    }

    public void SetDeadZoneX(float deadX)
    {
        xLock = deadX;
    }

    public void SetDeadZoneY(float deadY)
    {
        yLock = deadY;
    }

    private void OnDrawGizmos()
    {
        if (cameraTarget != null)
        {
            Vector3 targetScreenPos = cam.WorldToScreenPoint(cameraTarget.transform.position);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetScreenPos, 0.1f);
            Debug.Log(targetScreenPos);
        }
    }

    private void LateUpdate()
    {
        //float xDif = 0, yDif = 0, zDif = 0, wDif = 0;
        if (cameraTarget != null)
        {
            Vector3 targetScreenPos = cam.WorldToScreenPoint(cameraTarget.transform.position);
            //cam.transform.LookAt(cameraTarget.transform);
        }
        else
        {

        }
    }
}