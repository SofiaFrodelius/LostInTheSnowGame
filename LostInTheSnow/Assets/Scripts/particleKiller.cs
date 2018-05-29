using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleKiller : MonoBehaviour {
    List<ParticleSystem> ps;
    int psDone = 0;

	// Use this for initialization
	void Start () {
        ps = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());

	}
	
	// Update is called once per frame
	void Update () {
        foreach(ParticleSystem p in ps)
        {
            if (p.isStopped)
            {
                psDone++;
            }
            else
            {
                psDone = 0;
            }
        }
        if(psDone == ps.Count)
        {
            Destroy(gameObject);
        }
    }
}
