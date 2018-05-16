using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNodeScript : MonoBehaviour
{
    public int movementTime = 0, pauseTime = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue);
    }
}