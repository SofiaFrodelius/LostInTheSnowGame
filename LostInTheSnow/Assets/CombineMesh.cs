using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMesh : MonoBehaviour
{

    public List<GameObject> meshObjectList;

    public void Start()
    {
        CombineMeshes();
    }

    public void CombineMeshes()
    {

        // combine meshes
        CombineInstance[] combine = new CombineInstance[meshObjectList.Count];
        int i = 0;
        while (i < meshObjectList.Count)
        {
            MeshFilter meshFilter = meshObjectList[i].gameObject.GetComponent<MeshFilter>();
            combine[i].mesh = meshFilter.sharedMesh;
            combine[i].transform = meshFilter.transform.localToWorldMatrix;
            i++;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
    }
}
