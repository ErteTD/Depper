using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompOrbAgentHack : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("StopAgent", 0.05f);
	}

    void StopAgent()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }
	

}
