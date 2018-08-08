using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        Invoke("DieNow", 2f);
    }

    public void DieNow()
    {
        Destroy(gameObject);
    }
}
