using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrap : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    [SerializeField] private int placeTrapSceneId;


    private void OnLevelWasLoaded(int level)
    {
        if (level == placeTrapSceneId)
        {
            trap.SetActive(true);
        }
    }
}
