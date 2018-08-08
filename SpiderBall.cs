using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBall : MonoBehaviour
{

    public GameObject Spooders;
    public GameObject Daddy;
    public GameObject BossRoom;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject Spider = Instantiate(Spooders, transform.position, transform.rotation);
                Spider.GetComponent<Monster>().AggroRange = 25;
                Spider.GetComponent<Monster>().MovementSpeed = 5f;
                Spider.GetComponent<Monster>().health = 3;
                Spider.GetComponent<Monster>().health2 = 3;
                Spider.GetComponent<Monster>().MonsterTypeSubLayer = 2;
                Spider.transform.parent = GameObject.FindGameObjectWithTag("SpiderBossRoom").transform;

            }

            transform.GetChild(0).GetComponent<SpiderPoison>().Daddy = Daddy;
            transform.GetChild(0).GetComponent<SpiderPoison>().NahAH = true;
            transform.GetChild(0).position = new Vector3(transform.position.x, 1, transform.position.z);
            transform.GetChild(0).transform.parent = null;

            Destroy(gameObject);
        }
    }
}
