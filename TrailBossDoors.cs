using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBossDoors : MonoBehaviour
{

    public TrailBossDoors TwinDoor;
    private GameObject Player_;
    public float DoorTriggerCD;
    private float DoorTriggerCD_;
    // Start is called before the first frame update
    void Start()
    {
        Player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DoorTriggerCD_ -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)          
    {
        if (other.tag == "Player" && DoorTriggerCD_ < 0)
        {       
            TwinDoor.DoorTriggerCD_ = TwinDoor.DoorTriggerCD;
            WarpPlayer();
        }
        if (other.tag == "Monster" && DoorTriggerCD_ < 0)
        {
            TwinDoor.DoorTriggerCD_ = TwinDoor.DoorTriggerCD;
            WarpMonster(other.GetComponent<Monster>());
        }
    }


    void WarpPlayer()
    {
        Vector3 Pos = TwinDoor.gameObject.transform.position + TwinDoor.gameObject.transform.forward * 3;
        Player_.GetComponent<Player>().agent.Warp(Pos);
    }
    void WarpMonster(Monster monst)
    {
        Vector3 Pos = TwinDoor.gameObject.transform.position + TwinDoor.gameObject.transform.forward * 3;
        monst.agent.Warp(Pos);
    }

}

