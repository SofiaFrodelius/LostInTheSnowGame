using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    public Camera relevantCamera;
    public List<GameObject> nodeList;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private int nodeIterator = 0;
    private int nodeTime = 0, nodePause = 0;
    private float t = 0.0f;
    private bool paused = true, cutsceneRunning = false;

    private void Start()
    {
        for (int i = 0; i < transform.Find("Nodes").childCount; i++)
        {
            nodeList.Add(transform.Find("Nodes").GetChild(i).gameObject);
        }

        StartCutscene();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        for (int i = 1; i < transform.Find("Nodes").childCount; i++)
        {
            Debug.DrawLine(transform.Find("Nodes").GetChild(i - 1).position, transform.Find("Nodes").GetChild(i).position, Color.red);
        }
    }

    private void LateUpdate()
    {
        if (cutsceneRunning)
        {
            if (!paused)
            {
                relevantCamera.transform.position = new Vector3(Mathf.Lerp(nodeList[nodeIterator - 1].transform.position.x, nodeList[nodeIterator].transform.position.x, t),
                Mathf.Lerp(nodeList[nodeIterator - 1].transform.position.y, nodeList[nodeIterator].transform.position.y, t),
                Mathf.Lerp(nodeList[nodeIterator - 1].transform.position.z, nodeList[nodeIterator].transform.position.z, t));

                relevantCamera.transform.rotation = new Quaternion(Mathf.Lerp(nodeList[nodeIterator - 1].transform.rotation.x, nodeList[nodeIterator].transform.rotation.x, t),
                    Mathf.Lerp(nodeList[nodeIterator - 1].transform.rotation.y, nodeList[nodeIterator].transform.rotation.y, t),
                    Mathf.Lerp(nodeList[nodeIterator - 1].transform.rotation.z, nodeList[nodeIterator].transform.rotation.z, t),
                    Mathf.Lerp(nodeList[nodeIterator - 1].transform.rotation.w, nodeList[nodeIterator].transform.rotation.w, t));

                if (nodeTime == 0)
                    t = 1.0f;
                else
                    t += Time.deltaTime * 1000 / nodeTime;
            }
            else
            {
                if (nodePause == 0)
                    t = 1.0f;
                else
                    t += Time.deltaTime * 1000 / nodePause;
            }

            if (t >= 1.0f)
            {
                t = 0.0f;
                if (paused)
                {
                    nodeIterator++;
                    if (nodeIterator >= nodeList.Count)
                    {
                        EndCutscene();
                    }
                    else
                    {
                        CutsceneNodeScript nodeScript;
                        nodeScript = (CutsceneNodeScript)nodeList[nodeIterator].GetComponent("CutsceneNodeScript");
                        nodeTime = nodeScript.movementTime;
                        nodePause = nodeScript.pauseTime;

                        paused = !paused;
                    }
                }
                else
                    paused = !paused;
            }
        }
    }

    public void StartCutscene()
    {
        cutsceneRunning = true;

        startPosition = relevantCamera.transform.position;
        startRotation = relevantCamera.transform.rotation;

        nodeIterator = 0;
        relevantCamera.transform.position = nodeList[0].transform.position;
        relevantCamera.transform.rotation = nodeList[0].transform.rotation;

        CutsceneNodeScript nodeScript;
        nodeScript = (CutsceneNodeScript)nodeList[0].GetComponent("CutsceneNodeScript");
        nodePause = nodeScript.pauseTime;
        nodeTime = nodeScript.movementTime;

        t = 0.0f;

        paused = true;
    }

    private void EndCutscene()
    {
        relevantCamera.transform.position = startPosition;
        relevantCamera.transform.rotation = startRotation;
        cutsceneRunning = false;
        StartCutscene();
    }

    public bool IsCutsceneRunning()
    {
        return cutsceneRunning;
    }
}