using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour
{
    public float DieTime;
    // Use this for initialization
    void Start()
    {

        Invoke("DieNow", DieTime);
    }

    public void DieNow()
    {

        Destroy(gameObject);
    }
}
