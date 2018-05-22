using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

class TreeToTree : EditorWindow
{

    static TerrainData td;
    static Vector3 terrainPos;
    List<TreeInstance> ti;
    GameObject forest;
    [MenuItem("Terrain/Bake Forest To Prefab...")]
    static void Init()
    {
        td = null;
        Terrain terrainObject = Selection.activeObject as Terrain;
        if (!terrainObject)
        {
            terrainObject = Terrain.activeTerrain;
        }
        if (terrainObject)
        {
            td = terrainObject.terrainData;
            terrainPos = terrainObject.transform.position;
        }

        EditorWindow.GetWindow<TreeToTree>().Show();
    }
    void OnGUI()
    {
        if (!td)
        {
            GUILayout.Label("No terrain found");
            if (GUILayout.Button("Cancel"))
            {
                EditorWindow.GetWindow<TreeToTree>().Close();
            }
            return;
        }
        if (GUILayout.Button("Bake Forest"))
        {
            Bake();
        }
    }
    void Bake()
    {
        ti = new List<TreeInstance>(td.treeInstances);
        forest = new GameObject();
        forest.name = "forest";
        for (int i = 0; i < ti.Count; i++)
        {
            TreePrototype tp = td.treePrototypes[ti[i].prototypeIndex];
            Vector3 treePos = new Vector3(ti[i].position.x * td.size.x + terrainPos.x, ti[i].position.y * td.size.y + terrainPos.y, ti[i].position.z * td.size.z + + terrainPos.z);
            GameObject tmpTree = Instantiate(tp.prefab, treePos, Quaternion.identity);
            tmpTree.transform.parent = forest.transform;
            Vector3 newRot = new Vector3(tmpTree.transform.eulerAngles.x + Random.Range(-2f, 2f), Random.Range(0f, 360f), tmpTree.transform.eulerAngles.z + Random.Range(-2f, 2f));
            tmpTree.transform.eulerAngles = newRot;
        }
    }

}
