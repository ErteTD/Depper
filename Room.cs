using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
public class Room : MonoBehaviour
{
    public GameObject Floor;
    public NavMeshSurface NavGen;
    [Header("DoorPositions")]
    public List<Vector3> DoorLocations;
    public List<Vector3> DoorRotation;


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
    public int Monsters_;
    public GameObject MimiMapBlock;
    public List<GameObject> DoorList = new List<GameObject>();
    public List<int> MiniMapDoors = new List<int>();
    public GameObject Boss;
    public bool HasLoot;
    private List<GameObject> Monsters = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        Invoke("GetDoors", 0.001f);
        Invoke("ColorMiniMapRed", 0.1f);
        if (!BossRoom)
        {
            BuildRoomNavMesh();
        }

        if (TIMEK)
        {
            OuterRing.transform.parent = null;
            InnerRing.transform.parent = null;
        }
    }





    public void BuildRoomNavMesh()
    {
        NavGen.BuildNavMesh();
    }
    public void AddMonster(GameObject monster)
    {
        Monsters.Add(monster);
    }
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
        

        if (BossRoom)
        {
            for (int i = 0; i < transform.childCount; i++) // all other doors connected through MapGrid.
            {
                Transform child = transform.GetChild(i);
                if (child.tag == "Door")
                {
                    DoorList.Add(child.gameObject);
                }
            }

            // pretty bad code, the Fakedoor of bossroom needs to be the first child in order for it to work. Also dat tag...
            transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<OneWayDoor>().BossNextLevel = true;
            if (SBHC)
            {
                GameObject Spider = Instantiate(Boss, transform);
                Spider.GetComponent<Monster>().BossRoom=this;
                Spider.transform.localPosition = new Vector3(0, 1, 13.75f);
                SBHC = false;
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

    void OpenDoorsIfNoMonsters()
    {
        if (Monsters.Count == 0)
        {
            OpenDoors();
            CancelInvoke("OpenDoorsIfNoMonsters");
        }
        else
        {
            for (int i = Monsters.Count - 1; i > -1; i--)
            {
                if (Monsters[i] == null)
                {
                    Monsters.RemoveAt(i);
                }
            }
        }
    }

    void OpenDoors()
    {
        foreach (var door in DoorList)
        {
            OpenDoor(door);
        }
    }

    void OpenDoor(GameObject door)
    {
        door.transform.GetChild(0).gameObject.SetActive(true);
    }
}
