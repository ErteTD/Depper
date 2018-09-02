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
    [Header("MonsterPositions")]
    public List<Vector3> MonsterPos;
    public bool MiniBoss;
    public Vector3 MiniBossLocation;
    public bool MonsterEvent;
    public List<Vector3> EventSpawnLocations;
    public GameObject TriggerEvent;
    public GameObject SpiderEvent;
    [Header("RoomObstacles")]
    public List<GameObject> RoomObstacles;

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

        //for (int i = -6; i < 6; i++)
        //{
        //    for (int i2 = -9; i2 < 9; i2++)
        //    {
        //        MonsterPos.Add(new Vector3(i, 0, i2));
        //    }
        //}

    }


    public void EventInThisRoom()
    {
        TriggerEvent.SetActive(true);
    }

    public void StartEvent()
    {
        CloseDoors();
        TriggerEvent.SetActive(false);
        StartCoroutine(NextSpiderInSwarm(50 - 1, 2f));
    }

    void SummonSwarm(int count)
    {
        var RandomSpot = Random.Range(0, EventSpawnLocations.Count);
        GameObject Spider = Instantiate(SpiderEvent, transform.position, transform.rotation, transform);
        Spider.transform.localPosition = EventSpawnLocations[RandomSpot];
        AddMonster(Spider);
        Spider.GetComponent<Monster>().AggroRange = 50;
        Spider.GetComponent<Monster>().MovementSpeed = 3f;
        Spider.GetComponent<Monster>().damage = 0.5f;
        Spider.GetComponent<Monster>().health = 1;
        Spider.GetComponent<Monster>().health2 = 1;
        Spider.GetComponent<Monster>().MonsterTypeSubLayer = 2;
        Spider.GetComponent<Monster>().meleeRange = 2;
        //Spider.transform.parent = GameObject.FindGameObjectWithTag("SpiderBossRoom").transform;
        Spider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Spider.GetComponent<Collider>().GetComponent<CapsuleCollider>().height = 10;
        Spider.GetComponent<UnityEngine.AI.NavMeshAgent>().radius = 1f;
    

        if (count > 0)
        {
            //  SummonSwarm(count - 1);
            StartCoroutine(NextSpiderInSwarm(count - 1, 0.4f));
        }
        else
        {
            InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        }
    }

    IEnumerator NextSpiderInSwarm(int spidercount, float delay)
    {
        yield return new WaitForSeconds(delay);
        SummonSwarm(spidercount);
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
          //  CancelInvoke("OpenDoorsIfNoMonsters");
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

    void CloseDoors()
    {
        foreach (var door in DoorList)
        {
            OpenDoor(door, false);
        }
        CancelInvoke("OpenDoorsIfNoMonsters");
    }

    void OpenDoors()
    {
        foreach (var door in DoorList)
        {
            OpenDoor(door, true);
        }
    }

    void OpenDoor(GameObject door, bool status)
    {
        door.transform.GetChild(0).gameObject.SetActive(status);
    }
}
