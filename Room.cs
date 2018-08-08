using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Room : MonoBehaviour {
    public GameObject Floor;
    [Header("Boss Stuff")]
    public bool BossRoom;
    public bool RoomBeforeBoss;
    public bool SBHC; //spiderbosshardcode.
    public bool TIMEK; // TimeKeeperBoss
    public GameObject OuterRing;
    public GameObject InnerRing;
    public Vector3 StartLocation;
    public float CamTop, CamBot, CamLeft, CamRight;

    public float CamCenter;
    private float ASD;


    [Header("Other stuff")]
    public int Monsters;
    public GameObject MimiMapBlock;
   public List<GameObject> DoorList = new List<GameObject>();
    public List<int> MiniMapDoors = new List<int>();
    public GameObject Boss;
    public bool HasLoot;
    // Use this for initialization

    void Start() {
        InvokeRepeating("GetChildObject", 0.01f, 0.5f);
        Invoke("GetDoors", 0.001f);
        Invoke("ColorMiniMapRed", 0.1f);
    }

    //void MiniMapRooms(){

    //    foreach (var item in DoorList)
    //    {
    //        if (item.transform.GetChild(0).gameObject.GetComponent<OneWayDoor>().ConRoom.GetComponent<Room>().MimiMapBlock.activeInHierarchy == false)
    //        {
    //            item.transform.GetChild(0).gameObject.GetComponent<OneWayDoor>().ConRoom.GetComponent<Room>().MimiMapBlock.SetActive(true);
    //            item.transform.GetChild(0).gameObject.GetComponent<OneWayDoor>().ConRoom.GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = Color.white;
    //        }
    //    }
    //}


     void Update()
    {

        if (TIMEK)
        {
            ASD += Time.deltaTime * 5;
            OuterRing.transform.rotation = Quaternion.Euler(new Vector3(90, ASD, 0));
            InnerRing.transform.rotation = Quaternion.Euler(new Vector3(90, -ASD, 0));


        }
    }

    public void GetDoors()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Door")
            {
                DoorList.Add(child.gameObject);
            }
        }

        if (BossRoom)
        { // pretty bad code, the Fakedoor of bossroom needs to be the first child in order for it to work. Also dat tag...
            transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<OneWayDoor>().BossNextLevel = true;
            if (SBHC)
            {
                GameObject Spider = Instantiate(Boss, transform);
                Spider.transform.localPosition = new Vector3(0, 1, 13.75f);
                SBHC = false;
            }
            if (TIMEK)
            {
                // Invoke("ActivateBoss", 2f); // no need to activate currently.
            }
        }
    }
    void ColorMiniMapRed()
    {
        if (RoomBeforeBoss)
        {
            MimiMapBlock.transform.GetChild(MiniMapDoors.Count - 1).gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void ActivateBoss()
    {
        if (Boss != null)
        {
            Boss.SetActive(true);
        }
    }


    public void GetChildObject()
    {
        Monsters = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Monster" || child.tag == "Illusion" || child.tag == "Ressing")
            {
                Monsters++;
            }
        }
        if (Monsters == 0)
        {
            foreach (var door in DoorList)
            {
                door.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
