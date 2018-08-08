using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGrid : MonoBehaviour
{

    public List<Material> FloorType;
    public GameObject GiveStuff;
    private int TotalRooms;
    public GameObject BasicRoom;
    public GameObject Door;



    [HideInInspector] public int XGrid;
    [HideInInspector] public int YGrid;

    private int MiniMapCur;

    private Vector3 DoorLoc;
    private Quaternion DoorRot;
    private int Counter;
    private Vector3 DoorLoc2;
    private Vector3 DoorPortal1;
    private Vector3 DoorPortal2;

    private float MIN;

    private int TopY;
    private int FinalY;
    private int TopX;
    private int MinX;
    private int FinalX;
    private int FinalX2;
    private Vector2Int FinalFinalX;
    private Vector2Int LR;
    private Vector2Int RR;
    private GameObject Lastdoor;
    private GameObject LastRoom;

    [Header("LevelStuff")]
    public int CurrentLevel;
    public int minRoom;
    public int maxRoom;

    public List<int> LevelCR;
    [Header("Loot")]
    public List<GameObject> Tokens;
    public List<GameObject> Healing;
    private int AmountOfLoot;
    private int AmountOfPotions;

    private Quaternion DoorRot2;
    [Header("MonsterStuff")]
    public List<GameObject> MonsterType; // Should be its own class? to specify things like CR. Or if lazy put into monster script.
    [Header("RoomStuff")]
    public List<GameObject> Obstacle;
    public List<GameObject> Boss;
    List<Vector2Int> GridList = new List<Vector2Int>();
    List<Vector2Int> MiniMapList = new List<Vector2Int>();
    List<GameObject> RoomList = new List<GameObject>();
    List<Vector3> MonRanDPosList = new List<Vector3>();
    List<GameObject> MonsterType_;

    private int iterations;

    [Header("MiniMap")]
    public GameObject RoomBlock;
    private int MiniMapX, MiniMapZ;
    public GameObject MiniMapDoor;

    void Start()
    {
        if (GameManager.StartLevel_)
        {
            minRoom = GameManager.minRoom_;
            maxRoom = GameManager.maxRoom_;
            CurrentLevel = GameManager.CurrentLevel_;

            if (GameManager.GiveLoot_)
            {
                GiveStuff.GetComponent<GameManager>().SpellsAndItems();
            }
        }
        GenerateFirstRoom();
    }


    public void GenerateFirstRoom()
    {
        TotalRooms = Random.Range(minRoom, maxRoom + 1);
        AmountOfLoot = (int)Mathf.Floor(TotalRooms / 3);
        AmountOfPotions = (int)Mathf.Floor(TotalRooms / 4);
        GameObject Base = Instantiate(BasicRoom, transform.position, transform.rotation, transform);
        // this is a really disgusting hack that prevents this room from getting loot later on.. sorry
        Base.GetComponent<Room>().HasLoot = true;

        Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material = FloorType[CurrentLevel];

        var RandomObstacle = Random.Range(0, Obstacle.Count);
        Instantiate(Obstacle[RandomObstacle], Base.transform);
        GridList.Add(new Vector2Int(0, 0));
        MiniMapList.Add(new Vector2Int(0, 0));
        RoomList.Add(Base);
        SpawnMoreRooms(0);
    }

    public void MonsterList()
    {
        foreach (var monster in MonsterType) // Lowers chance that higher inteded level creatures spawn at current level. Not working for lower i think.
        {
            if (monster.GetComponent<Monster>().IntendedLevel > CurrentLevel)
            {
                var RemoveMonster = Random.Range(CurrentLevel, monster.GetComponent<Monster>().IntendedLevel + 3);
                if (RemoveMonster != CurrentLevel)
                {
                    MonsterType_.Remove(monster);
                }
            }
            else if (monster.GetComponent<Monster>().IntendedLevel < CurrentLevel)
            {
                var RemoveMonster = Random.Range(monster.GetComponent<Monster>().IntendedLevel, CurrentLevel + 3);
                if (RemoveMonster != monster.GetComponent<Monster>().IntendedLevel)
                {
                    MonsterType_.Remove(monster);
                }
            }
        }
        TrimList();

    }

    public void TrimList()
    {
        if (MonsterType_.Count > 3)
        {
            int RemoveMonst = Random.Range(0, MonsterType_.Count);
            MonsterType_.RemoveAt(RemoveMonst);
            TrimList();
        }
        else if (MonsterType_.Count < 3)
        {
            int AddMonst = Random.Range(0, MonsterType.Count);
            MonsterType_.Add(MonsterType[AddMonst]);
            TrimList();
        }
    }

    public void SpawnMoreRooms(int ListKey)
    {
        MIN = 0;
        MonsterType_ = MonsterType.ToList();

        MonsterList();

        Vector2Int CurGrid = GridList[ListKey];
        XGrid = CurGrid.x;
        YGrid = CurGrid.y;

        if (TotalRooms > 0)
        {
            var RPos = Random.Range(1, 5);
            switch (RPos)
            {
                case 1: //Up
                    YGrid++;
                    MiniMapX = -1;
                    MiniMapZ = 1;
                    break;
                case 2: //Down
                    YGrid--;
                    MiniMapX = 1;
                    MiniMapZ = -1;
                    break;
                case 3: //Left
                    XGrid--;
                    MiniMapX = -1;
                    MiniMapZ = -1;
                    break;
                case 4: // Right
                    XGrid++;
                    MiniMapX = 1;
                    MiniMapZ = 1;
                    break;
                default:
                    break;
            }
            if (!GridList.Contains(new Vector2Int(XGrid, YGrid)))
            {
                GridList.Add(new Vector2Int(XGrid, YGrid));

                MiniMapList.Add(new Vector2Int(MiniMapList[ListKey].x + MiniMapX, MiniMapList[ListKey].y + MiniMapZ));

                TotalRooms--;
                GameObject Base = Instantiate(BasicRoom, transform.position, transform.rotation, transform);
                Base.transform.localPosition = new Vector3(XGrid * 80, 0, YGrid * 40);

                for (int i = -6; i < 7; i += 2)
                {
                    for (int i2 = -12; i2 < 13; i2 += 2)
                    {
                        MonRanDPosList.Add(new Vector3(i, 0, i2));
                    }
                }

                while (MIN < LevelCR[CurrentLevel])
                {
                    var RandomMonster = Random.Range(0, MonsterType_.Count);
                    int SpawnMulti = 1;
                    if (MonsterType_[RandomMonster].GetComponent<Monster>().SpawnMultiBool)
                    {
                        SpawnMulti = MonsterType_[RandomMonster].GetComponent<Monster>().SpawnMultiNumber;
                    }

                    var room = Base.GetComponent<Room>();

                    for (int i = 0; i < SpawnMulti; i++)
                    {
                        var RandomPos = Random.Range(0, MonRanDPosList.Count);
                        var RandomRot = Random.Range(0, 366);

                        GameObject Monst = Instantiate(MonsterType_[RandomMonster], transform.position, Quaternion.Euler(0f, RandomRot, 0f), Base.transform);
                        Monst.transform.localPosition = MonRanDPosList[RandomPos];
                        MonRanDPosList.RemoveAt(RandomPos);

                        room.AddMonster(Monst);
                        if (Monst.GetComponent<Monster>().MonsterType == 5)
                        {
                            Monst.transform.position = new Vector3(Monst.transform.position.x, 3f, Monst.transform.position.z);
                        }
                    }
                    MIN += MonsterType_[RandomMonster].GetComponent<Monster>().MonsterCR;
                }

                MonRanDPosList.Clear();

                var RandomObstacle = Random.Range(0, Obstacle.Count);
                Instantiate(Obstacle[RandomObstacle], Base.transform);

                Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material = FloorType[CurrentLevel];
                Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(Base.GetComponent<Room>().Floor.transform.localScale.x, Base.GetComponent<Room>().Floor.transform.localScale.z);

                Base.SetActive(false);
                RoomList.Add(Base);
            }

            var RandomRoom = Random.Range(0, GridList.Count);
            SpawnMoreRooms(RandomRoom);
        }
        else //Boss Room && Doors
        {
            int Counter2 = 0;
            foreach (var Room in GridList)
            {
                TopY = Room.y;
                TopX = Room.x;
                MinX = Room.x;
                if (FinalY < TopY)
                {
                    FinalY = TopY;
                }
                if (FinalX <= TopX)
                {
                    FinalX = TopX;
                    RR = Room;
                }
                if (MinX <= FinalX2)
                {
                    FinalX2 = MinX;
                    LR = Room;
                }

                for (int i = 0; i < GridList.Count; i++) // create all doors.
                {
                    if (TopX + 1 == GridList[i].x && TopY == GridList[i].y)
                    {
                        SpawnRoom(4, TopY, TopX, GridList[i].y, GridList[i].x, RoomList[Counter2], RoomList[i]);
                    }
                    if (TopX == GridList[i].x && TopY + 1 == GridList[i].y)
                    {
                        SpawnRoom(1, TopY, TopX, GridList[i].y, GridList[i].x, RoomList[Counter2], RoomList[i]);
                    }
                }
                Counter2++;

            }

            int Positive = FinalX * -1;
            if (FinalX2 > Positive) // end of left or right
            {
                FinalFinalX = RR;

            }
            else
            {
                FinalFinalX = LR;
            }

            DoorLoc = new Vector3(0, 2, 15);
            DoorRot = Quaternion.Euler(new Vector3(0, 0, 0));

            int Indeex = GridList.IndexOf(FinalFinalX);
            GameObject Doors = Instantiate(Door, RoomList[Indeex].transform);

            Doors.transform.localPosition = DoorLoc;
            Doors.transform.localRotation = DoorRot;
            Doors.transform.Find("LightAndTrigger").GetComponent<OneWayDoor>().BossRoom();

            //   var RandomBoss = Random.Range(0, Boss.Count); // Boss room currently does not have a challenge raiting.
            GameObject BossRoom = Instantiate(Boss[CurrentLevel], transform); // 
            Room BRoom = BossRoom.GetComponent<Room>();
            BossRoom.transform.position = new Vector3(0f, 0f, (FinalY + 2) * 40);

            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().ConRoom = BossRoom;


            //var RandomFloor = Random.Range(0, FloorType.Count);
            BRoom.Floor.GetComponent<MeshRenderer>().material = FloorType[CurrentLevel];
            BRoom.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(BRoom.GetComponent<Room>().Floor.transform.localScale.x, BRoom.GetComponent<Room>().Floor.transform.localScale.z);


            Doors.transform.Find("LightAndTrigger").GetComponent<OneWayDoor>().DoorPortal = new Vector3(BossRoom.transform.position.x + BRoom.StartLocation.x, BossRoom.transform.position.y + +BRoom.StartLocation.y, BossRoom.transform.position.z + +BRoom.StartLocation.z);

            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight += ((FinalY + 2) * 40) + BRoom.CamTop;
            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight2 += ((FinalY + 2) * 40) + BRoom.CamBot;
            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().leftEdge = BRoom.CamLeft;
            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().rightEdge = BRoom.CamRight;
            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().CamCenter = BRoom.CamCenter;

            MiniMapList.Add(new Vector2Int(0, FinalY + 2));
            RoomList[Indeex].GetComponent<Room>().MiniMapDoors.Add(5);
            RoomList[Indeex].GetComponent<Room>().RoomBeforeBoss = true;
            RoomList.Add(BossRoom);

            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().MoveMMCam = 5; // custom switch for bossrooms.
            Doors.transform.GetChild(0).gameObject.SetActive(false);

            if (BRoom.SBHC) // || for all bossrooms that have random obstacles. Alt. Boss[0] etc.
            {
                var RandomObstacle = Random.Range(0, Obstacle.Count);
                GameObject Rubble = Instantiate(Obstacle[RandomObstacle], BossRoom.transform);
                Rubble.transform.position = new Vector3(BossRoom.transform.position.x, BossRoom.transform.position.y, BossRoom.transform.position.z - 2f);
            }

            BossRoom.SetActive(false);
            BRoom.HasLoot = true;
            //End of Boss Script.

            //Calling Loot Script.
            SpawnLoot();

            foreach (var Mini in MiniMapList)//creates Minimap.
            {
                GameObject MiniMapBlock = Instantiate(RoomBlock, new Vector3((Mini.x * 7f) - 1000, 0, Mini.y * 7f), transform.rotation, transform); // the -1000 is kinda hacky, just to move it out of the scene to ignore lights. Could do with script. RenderWithShader but MEH.
                RoomList[Counter].GetComponent<Room>().MimiMapBlock = MiniMapBlock;

                foreach (var item in RoomList[Counter].GetComponent<Room>().MiniMapDoors)
                {
                    GameObject MMD = Instantiate(MiniMapDoor, MiniMapBlock.transform.position, MiniMapBlock.transform.rotation, MiniMapBlock.transform);
                    switch (item)
                    {
                        case 1:
                            MMD.transform.localPosition = new Vector3(-0.35f, 1, 0.35f);
                            break;
                        case 2:
                            MMD.transform.localPosition = new Vector3(0.35f, 1, -0.35f);
                            break;
                        case 3:
                            MMD.transform.localPosition = new Vector3(-0.35f, 1, -0.35f);
                            Lastdoor = MMD;
                            break;
                        case 4:
                            MMD.transform.localPosition = new Vector3(0.35f, 1, 0.35f);
                            Lastdoor = MMD;
                            break;
                        case 5:
                            MMD.transform.localPosition = new Vector3(0, 1, 0.35f);
                            Lastdoor = MMD;
                            break;
                        default:
                            break;
                    }
                }
                LastRoom = MiniMapBlock;

                if (Counter > 0) // so wont deactive first room
                {
                    MiniMapBlock.SetActive(false);
                }
                else
                {
                    RoomList[Counter].GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = Color.green;
                }

                Counter++;
            }
            if (Lastdoor != null)
            { // Purpose: Make the minimap door connecting to the bossroom RED.
              //   Lastdoor.GetComponent<Renderer>().material.color = Color.red; //Not working as the lastdoor is not always the door connecting to boss room.
            }


            LastRoom.transform.position = new Vector3((0) - 1000, 0, 100);
            LastRoom.SetActive(true);
            LastRoom.GetComponent<Renderer>().material.color = Color.red;
            LastRoom.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);


        }
    }

    public void SpawnRoom(int RPos, int YPos, int XPos, int YGrid, int XGrid, GameObject CurrentRoom, GameObject Base)
    {
        switch (RPos)
        {

            case 1: //Up
                DoorLoc = new Vector3(-25, 2, 13);
                DoorRot = Quaternion.Euler(new Vector3(0, -45, 0));
                DoorPortal1 = new Vector3(-5, 0, 5);
                DoorLoc2 = new Vector3(25, 2, -11);
                DoorRot2 = Quaternion.Euler(new Vector3(0, 135, 0));
                DoorPortal2 = new Vector3(5, 0, -5);
                MiniMapCur = 2;
                break;
            case 2: //Down
                DoorLoc = new Vector3(25, 2, -11);
                DoorRot = Quaternion.Euler(new Vector3(0, 135, 0));
                DoorPortal1 = new Vector3(5, 0, -5);
                DoorLoc2 = new Vector3(-25, 2, 13);
                DoorRot2 = Quaternion.Euler(new Vector3(0, -45, 0));
                DoorPortal2 = new Vector3(-5, 0, 5);
                MiniMapCur = 1;
                break;
            case 3: //Left
                DoorLoc = new Vector3(-25, 2, -11);
                DoorRot = Quaternion.Euler(new Vector3(0, -135, 0));
                DoorPortal1 = new Vector3(-5, 0, -5);
                DoorLoc2 = new Vector3(25, 2, 13);
                DoorRot2 = Quaternion.Euler(new Vector3(0, 45, 0));
                DoorPortal2 = new Vector3(5, 0, 5);
                MiniMapCur = 4;
                break;
            case 4: // Right
                DoorLoc = new Vector3(25, 2, 13);
                DoorRot = Quaternion.Euler(new Vector3(0, 45, 0));
                DoorPortal1 = new Vector3(5, 0, 5);
                DoorLoc2 = new Vector3(-25, 2, -11);
                DoorRot2 = Quaternion.Euler(new Vector3(0, -135, 0));
                DoorPortal2 = new Vector3(-5, 0, -5);
                MiniMapCur = 3;
                break;
        }

        GameObject Doors = Instantiate(Door, CurrentRoom.transform);
        Doors.transform.localPosition = DoorLoc;
        Doors.transform.localRotation = DoorRot;

        GameObject Doors2 = Instantiate(Door, Base.transform);
        Doors2.transform.localPosition = DoorLoc2;
        Doors2.transform.localRotation = DoorRot2;

        DoorPortal1 += Doors2.transform.position;
        DoorPortal2 += Doors.transform.position;

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().DoorPortal = DoorPortal1;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().DoorPortal = DoorPortal2;

        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight += (YPos * 40);
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight2 += (YPos * 40);
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().leftEdge += (XPos * 80);
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().rightEdge += (XPos * 80);

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight += (YGrid * 40);
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight2 += (YGrid * 40);
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().leftEdge += (XGrid * 80);
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().rightEdge += (XGrid * 80);

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().ConRoom = Base;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().CurRoom = CurrentRoom;

        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().ConRoom = CurrentRoom;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().CurRoom = Base;

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().ConnectingRoom = Doors2;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().ConnectingRoom = Doors;

        Doors.transform.GetChild(0).gameObject.SetActive(false);
        Doors2.transform.GetChild(0).gameObject.SetActive(false);

        Base.GetComponent<Room>().MiniMapDoors.Add(MiniMapCur);
        CurrentRoom.GetComponent<Room>().MiniMapDoors.Add(RPos);

        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().MoveMMCam = MiniMapCur;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().MoveMMCam = RPos;
    }



    public void NextLevel()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        CurrentLevel++;
        maxRoom++;

        GridList.Clear();
        MiniMapList.Clear();
        RoomList.Clear();
        MonRanDPosList.Clear();
        MonsterType_.Clear();
        Counter = 0;
        MiniMapX = 0;
        MiniMapZ = 0;
        DoorLoc2 = new Vector3(0, 0, 0);
        DoorPortal1 = new Vector3(0, 0, 0);
        DoorPortal2 = new Vector3(0, 0, 0);
        MIN = 0;
        TopY = 0;
        FinalY = 0;
        TopX = 0;
        MinX = 0;
        FinalX = 0;
        FinalX2 = 0;
        FinalFinalX = new Vector2Int(0, 0);
        LR = new Vector2Int(0, 0);
        RR = new Vector2Int(0, 0);
        Lastdoor = null;
        LastRoom = null;
        GenerateFirstRoom();
    }


    public void SpawnLoot()
    {

        var RandLoot = Random.Range(0, RoomList.Count);
        var RandPot = Random.Range(0, RoomList.Count);

        if (RoomList[RandLoot].GetComponent<Room>().HasLoot == false && AmountOfLoot > 0)
        {
            var LootBag = Random.Range(0, 15);
            GameObject Loot = Instantiate(Tokens[LootBag], RoomList[RandLoot].transform);
            Loot.transform.localPosition = new Vector3(0, 3, 0);
            RoomList[RandLoot].GetComponent<Room>().HasLoot = true;
            AmountOfLoot--;
        }
        if (RoomList[RandPot].GetComponent<Room>().HasLoot == false && AmountOfPotions > 0)
        {
            GameObject Loot = Instantiate(Healing[0], RoomList[RandPot].transform);
            Loot.transform.localPosition = new Vector3(0, 3, 0);
            RoomList[RandPot].GetComponent<Room>().HasLoot = true;
            AmountOfPotions--;

        }

        if (AmountOfLoot > 0 || AmountOfPotions > 0)
        {
            SpawnLoot();
        }



        //foreach (var room in RoomList)
        //{

        //    room.GetComponent<Room>().GetDoors();
        //    if ((room.GetComponent<Room>().DoorList.Count == 1) && (room.GetComponent<Room>().HasLoot == false) && (AmountOfLoot >0))
        //    {       

        //        var LootBag = Random.Range(0, 15);
        //        GameObject Loot = Instantiate(Tokens[LootBag], room.transform);
        //        Loot.transform.localPosition = new Vector3(0, 1, 0);
        //        room.GetComponent<Room>().HasLoot = true;
        //        AmountOfLoot--;
        //    }

        //    if ((room.GetComponent<Room>().HasLoot == false) && (AmountOfPotions > 0))
        //    {
        //        var RandomPotion = Random.Range(0, 3);
        //        if (RandomPotion == 0)
        //        {
        //            GameObject Loot = Instantiate(Healing[0], room.transform);
        //            Loot.transform.localPosition = new Vector3(0, 1, 0);
        //            room.GetComponent<Room>().HasLoot = true;
        //            AmountOfPotions--;

        //        }
        //    }
        //}      
    }


    public void MoreRooms(int ListKey) // not used, has the opposite effect as inteded, creates more rooms with 1 door. Tho i know why. :(
    {
        iterations++;

        var RandomRoom = Random.Range(0, GridList.Count);

        if (RoomList[ListKey].GetComponent<Room>().DoorList.Count <= 3) // did this not work because doorlist wasn't being generated???
        {
            var RandomRoom2 = Random.Range(0, 50);
            if (RandomRoom2 < 48)
            {
                SpawnMoreRooms(ListKey);
            }
            else
            {
                SpawnMoreRooms(RandomRoom);
            }

        }
        else
        {
            SpawnMoreRooms(RandomRoom);
        }
    }

}
