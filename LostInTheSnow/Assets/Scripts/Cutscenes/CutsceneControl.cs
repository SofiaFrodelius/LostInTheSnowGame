using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneControl : MonoBehaviour
{
    public Transform cameraTarget;
    public Camera cam;
    public bool resetRotMode = false, resetPosMode = false, smoothMode = false, camLock = false, relativeUp = true;
    public float smoothSpeed = 0.01f;
    [Range(0.0f, 0.5f)]
    public float deadZoneX = 0.3f, deadZoneY = 0.3f;
    [Tooltip("This transformation's Up vector is used for Relative Up.")]
    public Transform baseTransform = null;
    
    private bool lockAquired = false, lockToggle = false, smoothing = false;
    private Vector3 startPos,  startDir, lastDir, upDir;
    private Quaternion startRot;

    private void ToggleLock()
    {
        startPos = cam.transform.position;
        startDir = cam.transform.forward;
        lastDir = startDir;
        startRot = cam.transform.rotation;
        smoothing = false;
        if (baseTransform != null)
            upDir = baseTransform.up;
        //Debug.Log("Toggle Lock");
    }

    private void ToggleUnlock()
    {
        lockAquired = false;
        if (resetRotMode)
        {
            cam.transform.forward = startDir;
            cam.transform.rotation = startRot;
        }
        if (resetPosMode)
        {
            cam.transform.position = startPos;
        }
        if (smoothMode)
            smoothing = true;
        //Debug.Log("Toggle Unlock");
    }

    public void LateUpdate()
    {
        if (camLock && !lockToggle)
        {
            lockToggle = true;
            ToggleLock();
        }
        if (!camLock && lockToggle)
        {
            lockToggle = false;
            ToggleUnlock();
        }

        if (!smoothMode)
            smoothing = false;

        Vector2 targetScreenPosition = cam.WorldToScreenPoint(cameraTarget.position);
        Vector3 deltaVector = cameraTarget.position - cam.transform.position;
        if (camLock)
        {
            if (!lockAquired)
            {
                if (targetScreenPosition.x < Screen.width * deadZoneX || targetScreenPosition.x > Screen.width * (1 - deadZoneX) ||
                    targetScreenPosition.y < Screen.height * deadZoneY || targetScreenPosition.y > Screen.height * (1 - deadZoneY))
                {
                    cam.transform.forward = lastDir;
                    targetScreenPosition = cam.WorldToScreenPoint(cameraTarget.position);
                    float step = smoothSpeed * Time.deltaTime;
                    //cam.transform.forward = Vector3.RotateTowards(cam.transform.forward, deltaVector, step, 0.0f);
                    Vector3 newDir = Vector3.RotateTowards(cam.transform.forward, deltaVector, step, 0.0f);
                    if (relativeUp && baseTransform != null)
                        cam.transform.rotation = Quaternion.LookRotation(newDir, upDir);
                    else
                        cam.transform.rotation = Quaternion.LookRotation(newDir);
                }
                if (targetScreenPosition.x >= Screen.width * deadZoneX && targetScreenPosition.x <= Screen.width * (1 - deadZoneX) &&
                    targetScreenPosition.y >= Screen.height * deadZoneY && targetScreenPosition.y <= Screen.height * (1 - deadZoneY))
                {
                    lockAquired = true;
                }
                lastDir = cam.transform.forward;
            }
            else
            {
                cam.transform.forward = lastDir;
                targetScreenPosition = cam.WorldToScreenPoint(cameraTarget.position);
                int safetyIterator = 0;
                while (targetScreenPosition.x < Screen.width * deadZoneX || targetScreenPosition.x > Screen.width * (1 - deadZoneX) ||
                    targetScreenPosition.y < Screen.height * deadZoneY || targetScreenPosition.y > Screen.height * (1 - deadZoneY))
                {
                    //cam.transform.forward = Vector3.RotateTowards(cam.transform.forward, deltaVector, 0.01f, 0.0f);
                    Vector3 newDir = Vector3.RotateTowards(cam.transform.forward, deltaVector, 0.01f, 0.0f);
                    if (relativeUp && baseTransform != null)
                        cam.transform.rotation = Quaternion.LookRotation(newDir, upDir);
                    else
                        cam.transform.rotation = Quaternion.LookRotation(newDir);
                    targetScreenPosition = cam.WorldToScreenPoint(cameraTarget.position);
                    safetyIterator++;
                    if (safetyIterator >= 200)
                    {
                        Debug.LogWarning("safety break was reached");
                        break;
                    }
                }
                lastDir = cam.transform.forward;
            }
        }
        else
        {
            if (smoothing)
            {
                Vector3 targetDirection = cam.transform.forward, targetUp = cam.transform.up;
                float step = smoothSpeed * Time.deltaTime;
                //cam.transform.forward = Vector3.RotateTowards(lastDir, targetDirection, step, 0.0f);
                Vector3 newDir = Vector3.RotateTowards(lastDir, targetDirection, step, 0.0f);
                if (baseTransform != null)
                    upDir = Vector3.RotateTowards(upDir, targetUp, step, 0.0f);
                if (relativeUp && baseTransform != null)
                    cam.transform.rotation = Quaternion.LookRotation(newDir, upDir);
                else
                    cam.transform.rotation = Quaternion.LookRotation(newDir);
                if (cam.transform.forward == targetDirection.normalized)
                {
                    smoothing = false;
                }
                lastDir = cam.transform.forward;
            }
        }
    }
}