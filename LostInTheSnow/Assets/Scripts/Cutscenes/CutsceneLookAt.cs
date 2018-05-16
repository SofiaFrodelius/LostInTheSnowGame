using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLookAt : MonoBehaviour
{
    public GameObject cameraTarget;
    public Camera cam;
    public bool resetMode = false;
    public float smoothSpeed = 2.0f, deadZoneX = 0.3f, deadZoneY = 0.3f;

    //private float fov, hFov;
    private bool camLock = false;
    private Vector3 startPos;
    private Quaternion startRot;

    public void OnEnable()
    {
        camLock = true;
        if (cam != null)
        {
            startPos = cam.transform.position;
            startRot = cam.transform.rotation;
        }
    }

    public void OnDisable()
    {
        camLock = false;
        if (resetMode)
            if (cam != null)
            {
                cam.transform.position = startPos;
                cam.transform.rotation = startRot;
            }
    }

    public void LateUpdate()
    {
        if (camLock)
        {
            Vector3 deltaVector = cameraTarget.transform.position - cam.transform.position;
            Vector3 targetScreenPosition = cam.WorldToScreenPoint(cameraTarget.transform.position);
            if (targetScreenPosition.x < Screen.width * deadZoneX || targetScreenPosition.x > Screen.width * (1 - deadZoneX) ||
                targetScreenPosition.y < Screen.height * deadZoneY || targetScreenPosition.y > Screen.height * (1 - deadZoneY))
            {
                float angleDelta, angleFactor, distanceDelta, distanceFactor = 1.0f;
                angleDelta = Vector3.Angle(cam.transform.forward, deltaVector);
                distanceDelta = Vector3.Distance(cam.transform.position, cameraTarget.transform.position);
                angleFactor = Mathf.Abs(angleDelta) / 90;
                distanceFactor = 40 / distanceDelta;
                cam.transform.forward = Vector3.Slerp(cam.transform.forward, deltaVector, Time.deltaTime * smoothSpeed * angleFactor * distanceFactor);
            }
        }
    }
}