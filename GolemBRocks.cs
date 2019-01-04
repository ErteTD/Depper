using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBRocks : MonoBehaviour
{



    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Hello?");

        if (other.tag == "Monster")
        {

            Destroy(this);
        }

    }


}