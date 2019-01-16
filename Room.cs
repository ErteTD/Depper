using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
public class Room : MonoBehaviour
{
    public GameObject Floor;
    public bool NormalRoom;
    public NavMeshSurface NavGen;
    public bool ThisRoomHasShop;
    public bool ThisRoomHasEvent;
    [Header("DoorPositions")]
    public List<Vector3> DoorLocations;
    public List<Vector3> DoorRotation;
    [Header("MonsterPositions")]
    public List<Vector3> MonsterPos;
    public bool MiniBoss;
    public Vector3 MiniBossLocation;
    [Header("Room Events")]
    public GameObject Chest;
    public bool KeepDoorsClosedUntillChestIsOpened;
    public bool MonsterEvent;
    public List<Vector3> EventSpawnLocations;
    public GameObject TriggerEvent;
    public GameObject SpiderEvent;
    public List<GameObject> CasterEvent;
    public List<GameObject> BlobEvent;
    public GameObject SkeletonEvent;
    private int SwarmSize;
    private int CasterSize;
    private int BlobSize;
    private int SkeletonSize;
    public GameObject EventLoot;
    public int EventGold;
    private bool EventStarted;
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
    public Vector3 RoomBeforeBossRoomMiniMapIconLocation;
    [Header("Other stuff")]
    public int Monsters_;
    public GameObject MimiMapBlock;
    public List<GameObject> DoorList = new List<GameObject>();
    public List<int> MiniMapDoors = new List<int>();
    public GameObject Boss;
    public bool HasLoot;
    public int CurrentLevel;
    private List<GameObject> Monsters = new List<GameObject>();
    GameObject[] Rocks;
    private GameManager gm;
    public Player DaPlayer;
    public AudioSource LastBossDeathSound;
    public AudioSource LastBossDeathSound2;
    public ParticleSystem LastBossPart;
    public AudioSource LastBossPartAudio;
    private bool LastBoss;
    public GameObject Credits;
    public bool startmenu;
    public List<GameObject> StartMonsters;

    [Header("GolemBossStuff")]
    public bool GolemBossRoom;
    public GameObject BossWeapon;
    public GameObject BossArmor;
    [HideInInspector]
    public bool FirstRoom;
    [HideInInspector]
    public GameObject ppplayer;

    void Start()
    {
        InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        Invoke("GetDoors", 0.001f);
        Invoke("ColorMiniMapRed", 0.1f);
        if (!BossRoom)
        {
            BuildRoomNavMesh();
            if (!startmenu)
            {
                foreach (var monster in Monsters)
                {
                    monster.SetActive(true);
                }
            }
            else
            {
                foreach (var m in StartMonsters)
                {
                    m.SetActive(true);
                }
            }
        }

        if (TIMEK)
        {
            OuterRing.transform.parent = null;
            InnerRing.transform.parent = null;
        }

        //for (int i = -6; i < 6; i++) // dont delete :) used to generate the monsterspawn locations for new rooms.
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
        KeepDoorsClosedUntillChestIsOpened = true;
        EventStarted = true;
        TriggerEvent.SetActive(false);
        switch (CurrentLevel)
        {
            case 0:
                SwarmSize = 15;
                CasterSize = 4;
                BlobSize = 15;
                SkeletonSize = 2;
                break;
            case 1:
                SwarmSize = 30;
                CasterSize = 6;
                BlobSize = 30;
                SkeletonSize = 4;
                break;
            case 2:
                SwarmSize = 50;
                CasterSize = 5;
                BlobSize = 10;
                SkeletonSize = 6;
                break;
            case 3:
                SwarmSize = 10;
                CasterSize = 8;
                BlobSize = 20;
                SkeletonSize = 8;
                break;
            case 4:
                SwarmSize = 20;
                CasterSize = 4;
                BlobSize = 4;
                SkeletonSize = 10;
                break;
            case 5:
                SwarmSize = 30;
                CasterSize = 6;
                BlobSize = 8;
                SkeletonSize = 12;
                break;
            default:
                SwarmSize = 1;
                CasterSize = 1;
                BlobSize = 1;
                SkeletonSize = 1;
                break;
        }

        var RandomEvent = Random.Range(0, 4);
        
        switch (RandomEvent)
        {
            case 0:
                StartCoroutine(NextSpiderInSwarm(SwarmSize, 2f));
                break;
            case 1:
                StartCoroutine(NextCasterInEvent(CasterSize, 2f));
                break;
            case 2:
                StartCoroutine(NextBlobInEvent(BlobSize, 2f));
                break;
            case 3:
                StartCoroutine(NextSkeletonInEvent(SkeletonSize, 2f));
                break;
        }

    }
    IEnumerator NextSpiderInSwarm(int spidercount, float delay)
    {
        yield return new WaitForSeconds(delay);
        SummonSwarm(spidercount);
    }
    IEnumerator NextCasterInEvent(int Count, float delay)
    {
        yield return new WaitForSeconds(delay);
        SummonCaster(Count);
    }
    IEnumerator NextBlobInEvent(int Count, float delay)
    {
        yield return new WaitForSeconds(delay);
        SummonBlob(Count);
    }

    IEnumerator NextSkeletonInEvent(int Count, float delay)
    {
        yield return new WaitForSeconds(delay);
        SummonSkeleton(Count);
    }

    void SummonSkeleton(int count)
    {
        var RandomSpot = Random.Range(0, EventSpawnLocations.Count);
        GameObject Skeleton = Instantiate(SkeletonEvent, transform.position, transform.rotation, transform);
        Skeleton.transform.localPosition = EventSpawnLocations[RandomSpot];
        AddMonster(Skeleton);
        Monster Skel = Skeleton.GetComponent<Monster>();
        Skel.AggroRange = 50;
        Skel.Immortal = true;
        Skel.EventSkeleton = true;
        Skel.MovementSpeed = 6;
        Skel.RoomIAmIn = gameObject;
        Skel.EventSkeletonCD = 2;
        if (count > 0)
        {
            StartCoroutine(NextSkeletonInEvent(count - 1, 0.25f));
        }
        else
        {
            InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        }
    }

    void SummonSwarm(int count)
    {
        var RandomSpot = Random.Range(0, EventSpawnLocations.Count);
        GameObject Spider = Instantiate(SpiderEvent, transform.position, transform.rotation, transform);
        Spider.transform.localPosition = EventSpawnLocations[RandomSpot];
        AddMonster(Spider);
        Spider.GetComponent<Monster>().AggroRange = 50;
        if (CurrentLevel < 3)
        {
            Spider.GetComponent<Monster>().MovementSpeed = 4f;
            Spider.GetComponent<Monster>().damage = 0.5f;
            Spider.GetComponent<Monster>().health = 0.7f;
            Spider.GetComponent<Monster>().health2 = 0.7f;
            Spider.GetComponent<Monster>().MonsterTypeSubLayer = 2;
            Spider.GetComponent<Monster>().meleeRange = 2;
            Spider.GetComponent<Monster>().HBtext.gameObject.SetActive(false);
            //Spider.transform.parent = GameObject.FindGameObjectWithTag("SpiderBossRoom").transform;
            Spider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Spider.GetComponent<Collider>().GetComponent<CapsuleCollider>().height = 10;
            Spider.GetComponent<UnityEngine.AI.NavMeshAgent>().radius = 1f;
        }

        if (count > 0)
        {
            StartCoroutine(NextSpiderInSwarm(count - 1, 0.25f));
        }
        else
        {
            InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        }
    }

    void SummonCaster(int count)
    {
        var RandomSpot = Random.Range(0, EventSpawnLocations.Count);
        GameObject Caster = Instantiate(CasterEvent[CurrentLevel], transform.position, transform.rotation, transform);
        Caster.transform.localPosition = EventSpawnLocations[RandomSpot];
        AddMonster(Caster);
        Caster.GetComponent<Monster>().AggroRange = 50;
        if (count > 0)
        {
            StartCoroutine(NextCasterInEvent(count - 1, 0.25f));
        }
        else
        {
            InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        }
    }

    void SummonBlob(int count)
    {
        var RandomSpot = Random.Range(0, EventSpawnLocations.Count);
        GameObject Blob = Instantiate(BlobEvent[CurrentLevel], transform.position, transform.rotation, transform);
        Blob.transform.localPosition = EventSpawnLocations[RandomSpot];
        Blob.transform.localPosition = new Vector3(Blob.transform.localPosition.x, 3, Blob.transform.localPosition.z);
        AddMonster(Blob);
        Blob.GetComponent<Monster>().AggroRange = 50;
        if (count > 0)
        {
            StartCoroutine(NextBlobInEvent(count - 1, 0.5f));
        }
        else
        {
            InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
        }
    }

    public void SkeletonSeeIfFriendAlive(Monster deadSkeleton)
    {
        List<GameObject> tempList = new List<GameObject>();
        foreach (var skeleton in Monsters)
        {
            //  private List<GameObject> Monsters = new List<GameObject>();
            if (skeleton != null)
            {
                if (skeleton.GetComponent<Monster>().EventSkeleton && skeleton.tag == "Monster" && skeleton.GetComponent<Monster>().EventSkeletonCD_ <= 0)
                {
                    tempList.Add(skeleton);
                }
            }
        }
        if (tempList.Count > 0)
        {
            var RandomRes = Random.Range(0, tempList.Count);
            tempList[RandomRes].GetComponent<Monster>().ResEventSkeleton(deadSkeleton);
            return;
        }

        Destroy(deadSkeleton.gameObject);
    }



    public void BuildRoomNavMesh()
    {
        NavGen.BuildNavMesh();
    }
    public void AddMonster(GameObject monster)
    {
        Monsters.Add(monster);
    }
    public void RemoveMonster(GameObject monster)
    {
        Monsters.Remove(monster);
    }

    public void Startinvoking()
    {
        InvokeRepeating("OpenDoorsIfNoMonsters", 0.1f, 0.5f);
    }

    void Update()
    {
        if (TIMEK)
        {
            ASD += Time.deltaTime * 5;
            OuterRing.transform.rotation = Quaternion.Euler(new Vector3(90, ASD, 0));
            InnerRing.transform.rotation = Quaternion.Euler(new Vector3(90, -ASD, 0));

        }

        if (GolemBossRoom)
        {
            GolemBossRoomOpenDoorsCheck();
        }
    }

    public void GetDoors()
    {     
        if (BossRoom)
        {
            KeepDoorsClosedUntillChestIsOpened = true;
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

    public void LastBossDoor()
    {
        StartCoroutine(LastBossSkipLoot(6));
        foreach (var monster in Monsters)
        {
            if (monster.GetComponent<Monster>() != null)
            {
                monster.GetComponent<Monster>().TakeDamage(9999);
            }
        }
        DaPlayer.Immortal = true;
        Invoke("LastBossPauseParticleEffectAndStartCredits", 27);
        LastBossDeathSound.Play();
    }

    void LastBossPauseParticleEffectAndStartCredits()
    {
        LastBossPart.Pause();
        GameManager.FindObjectOfType<GameManager>().GameOverEsc = true;
        LastBossPartAudio.Stop();
        LastBossDeathSound2.Play();
        Credits.SetActive(true);
    }

    IEnumerator LastBossSkipLoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        LastBoss = true;

        KeepDoorsClosedUntillChestIsOpened = false;
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

            if (KeepDoorsClosedUntillChestIsOpened == false)
            {
                OpenDoors();


                if (LastBoss)
                {
                    CancelInvoke("OpenDoorsIfNoMonsters");
                }
            }
            else if (EventStarted)
            {
                SpawnChest();
            }
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

    void GolemBossRoomOpenDoorsCheck()
    {
        if (Monsters.Count == 0)
        {
            GolemBossRoom = false;
            Invoke("GBCheck2", 2);
        }
    }

    void GBCheck2()
    {
        if (Monsters.Count == 0)
        {

           Rocks = GameObject.FindGameObjectsWithTag("GolemRock");

            foreach (GameObject rock in Rocks)
            {
                rock.GetComponent<GolemThrownRock>().BossDeadDestroyRocks();
            }

                var RandomLoot = Random.Range(0, 2);
            switch (RandomLoot)
            {
                case 0:
                    GameObject BossLoot = Instantiate(Chest, transform.position, Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z), transform);
                    BossLoot.transform.localPosition = new Vector3(0,0.9f,-10f);
                    BossLoot.GetComponent<AmazingChestHead>().CurrentLoot = BossWeapon;
                    BossLoot.GetComponent<AmazingChestHead>().BossChest = true;
                    break;
                case 1:
                    GameObject BossLoot2 = Instantiate(Chest, transform.position, Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z), transform);
                    BossLoot2.transform.localPosition = new Vector3(0, 0.9f, -10f);
                    BossLoot2.GetComponent<AmazingChestHead>().CurrentLoot = BossArmor;
                    BossLoot2.GetComponent<AmazingChestHead>().BossChest = true;
                    break;
            }
        }
        else
        {
            GolemBossRoom = true;
        }

    }



    void SpawnChest()
    {
            EventStarted = false;
            GameObject CurLoot = Instantiate(Chest, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z));
            CurLoot.GetComponent<AmazingChestHead>().CurrentLoot = EventLoot;
            CurLoot.GetComponent<AmazingChestHead>().GoldAmount = EventGold;
            CurLoot.transform.parent = gameObject.transform;
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
