using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGrid : MonoBehaviour
{

    public GameObject Shop;
    public List<Material> FloorType;
    public GameObject GiveStuff;
    private int TotalRooms;
    public int TotalRoomsForSave;
    public List<GameObject> RoomTypes;
    public GameObject Door;
    public GameObject BossDoor;
    public int RoomMapLocationX;
    public int RoomMapLocationY;
    [HideInInspector] public int XGrid;
    [HideInInspector] public int YGrid;
    private int MiniMapCur;
    private int Counter;
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
    private GameObject LastRoom;
    public GameObject HardCodeRing1;
    public GameObject HardCodeRing2;
    public bool LevelHasHadEvent;
    public bool LevelHasHadMiniBoss;

    [Header("LevelStuff")]
    public int CurrentLevel;
    public int minRoom;
    public int maxRoom;
    public List<int> LevelCR;
    private int NumberOfMiniBosses;
    private int NumberOfEvents;
    [Header("Loot")]
    public List<GameObject> Tokens;
    public List<GameObject> Items;
    public List<GameObject> Healing;
    public GameObject Gold;
    private int AmountOfLoot;
    private int AmountOfPotions;
    [Header("MonsterStuff")]
    public List<GameObject> MonsterType;
    public List<GameObject> MiniBosses;

    [Header("RoomStuff")]
    public List<GameObject> Obstacle;
    public List<GameObject> Boss;
    public List<GameObject> BossesLevel1;
    public List<GameObject> BossesLevel2;
    public List<GameObject> BossesLevel3;
    public List<GameObject> BossesLevel4;
    public List<GameObject> BossesLevel5;
    public List<GameObject> BossesLevel6;

    List<Vector2Int> GridList = new List<Vector2Int>();
    List<Vector2Int> MiniMapList = new List<Vector2Int>();
    List<GameObject> RoomList = new List<GameObject>();
    List<Monster> MonstersThatCanDropLoot = new List<Monster>();
    List<GameObject> MonsterType_;
    private int iterations;

    [Header("MiniMap")]
    public GameObject RoomBlock;
    private int MiniMapX, MiniMapZ;
    public GameObject MiniMapDoor;
    public GameObject MiniMapShop;
    public GameObject MiniMapEvent;

    void Start()
    {
        if (GameManager.StartLevel_)
        {
            minRoom = GameManager.minRoom_;
            maxRoom = GameManager.maxRoom_;
            CurrentLevel = GameManager.CurrentLevel_;
            LevelHasHadEvent = GameManager.LevelHasHadEvent_;
            LevelHasHadMiniBoss = GameManager.LevelHasHadMiniBoss_;

            if (GameManager.GiveLoot_)
            {
                GiveStuff.GetComponent<GameManager>().SpellsAndItems();
            }
        }
        GenerateFirstRoom();
    }

    public void GenerateFirstRoom()
    {
        GiveStuff.GetComponent<GameManager>().CurrentLevel(CurrentLevel);
        TotalRooms = Random.Range(minRoom, maxRoom + 1);
        TotalRoomsForSave = TotalRooms+1; // +1 to account for startroom which will be deducted right away.
        AmountOfLoot = (int)Mathf.Floor(TotalRooms / 5);
        AmountOfPotions = (int)Mathf.Floor(TotalRooms / 4);

        if (!LevelHasHadMiniBoss)
        {
            NumberOfMiniBosses = 1;
        }
        if (!LevelHasHadEvent)
        {
            NumberOfEvents = 1;
        }

        GameObject Base = Instantiate(RoomTypes[0], transform.position, transform.rotation, transform);
        Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material = FloorType[CurrentLevel];
        Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(Base.GetComponent<Room>().Floor.transform.localScale.x, Base.GetComponent<Room>().Floor.transform.localScale.z);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentRoom = Base;

        var RandomObstacle = Random.Range(0, Obstacle.Count);
        Instantiate(Obstacle[RandomObstacle], Base.transform);
        Base.GetComponent<Room>().BuildRoomNavMesh();

        GameObject Shop_ = Instantiate(Shop, Base.transform);
        Shop_.transform.localPosition = new Vector3(25.5f, 2.1f, 1.5f);
        Base.GetComponent<Room>().ThisRoomHasShop = true;

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
                MonsterType_.Remove(monster);
            }
            else if (monster.GetComponent<Monster>().IntendedLevel < CurrentLevel)
            {
                var RemoveMonster = Random.Range(monster.GetComponent<Monster>().IntendedLevel, CurrentLevel + 1);
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
        if (MonsterType_.Count > 5)
        {
            int RemoveMonst = Random.Range(0, MonsterType_.Count);
            MonsterType_.RemoveAt(RemoveMonst);
            TrimList();
        }
        else if (MonsterType_.Count < 2)
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

                int RRoom = 0;
                if (XGrid != 1 && XGrid != -1 && YGrid != 1 && YGrid != -1)
                {
                    RRoom = Random.Range(0, RoomTypes.Count);
                }
                else
                {
                    RRoom = Random.Range(0, RoomTypes.Count - 1);
                }

                //hardcode so miniboss & eventroom always spawn.
                if (TotalRooms <= 2 && NumberOfMiniBosses > 0)
                {
                    RRoom = 3;
                }
                if (TotalRooms == 1 && NumberOfEvents > 0)
                {
                    RRoom = 5;
                }
           
                GameObject Base = Instantiate(RoomTypes[RRoom], transform.position, transform.rotation, transform);
                Room Base_ = Base.GetComponent<Room>();
                Base.SetActive(false);
                Base.transform.localPosition = new Vector3(XGrid * RoomMapLocationX, 0, YGrid * RoomMapLocationY);


                if (Base_.MiniBoss && NumberOfMiniBosses > 0)
                {
                    var RandomMonster = Random.Range(0, MiniBosses.Count);
                    var RandomRot = Random.Range(0, 366);
                    GameObject Monst = Instantiate(MiniBosses[RandomMonster], transform.position, Quaternion.Euler(0f, RandomRot, 0f), Base.transform);
                    Monst.transform.localPosition = Base_.MiniBossLocation;
                    if (MiniBosses[1]) { Monst.transform.localPosition = new Vector3(Base_.MiniBossLocation.x, 3, Base_.MiniBossLocation.z); } // Move Blob up from beneth the ground
                    Monst.GetComponent<Monster>().RoomIAmIn = Base;
                    Monst.GetComponent<Monster>().health = 5 + (CurrentLevel * 5);
                    Monst.GetComponent<Monster>().health2 = 5 + (CurrentLevel * 5);
                    Monst.GetComponent<Monster>().HBtext.color = Color.green;
                    Monst.SetActive(false);
                    Base_.AddMonster(Monst);
                    MiniBossLoot(Monst.GetComponent<Monster>());
                    NumberOfMiniBosses--;
                    MIN += CurrentLevel * 2;
                }


                if (!Base_.MonsterEvent || NumberOfEvents == 0)
                {
                    while (MIN < (LevelCR[CurrentLevel] * MenuScript.MonsterDensity))
                    {
                        var RandomMonster = Random.Range(0, MonsterType_.Count);
                        int SpawnMulti = 1;
                        if (MonsterType_[RandomMonster].GetComponent<Monster>().SpawnMultiBool)
                        {
                            SpawnMulti = MonsterType_[RandomMonster].GetComponent<Monster>().SpawnMultiNumber;
                        }

                        for (int i = 0; i < SpawnMulti; i++)
                        {
                            var RandomPos = Random.Range(0, Base_.MonsterPos.Count);
                            var RandomRot = Random.Range(0, 366);
                            GameObject Monst = Instantiate(MonsterType_[RandomMonster], transform.position, Quaternion.Euler(0f, RandomRot, 0f), Base.transform);
                            Monst.transform.localPosition = Base_.MonsterPos[RandomPos];
                            Base_.MonsterPos.RemoveAt(RandomPos);
                            MonstersThatCanDropLoot.Add(Monst.GetComponent<Monster>());
                            Monst.GetComponent<Monster>().RoomIAmIn = Base;
                            Monst.SetActive(false);
                            Base_.AddMonster(Monst);
                            if (Monst.GetComponent<Monster>().MonsterType == 5)
                            {
                                Monst.transform.position = new Vector3(Monst.transform.position.x, 3f, Monst.transform.position.z);
                            }
                        }
                        MIN += MonsterType_[RandomMonster].GetComponent<Monster>().MonsterCR;
                    }
                }
                else {
                    Base_.CurrentLevel = CurrentLevel;
                    Base_.EventInThisRoom();
                    Base_.ThisRoomHasEvent = true;
                    RoomEventLoot(Base_);
                    NumberOfEvents--;
                }

                var RandomObstacle = Random.Range(0, Base_.RoomObstacles.Count);
                Instantiate(Base_.RoomObstacles[RandomObstacle], Base.transform);
                Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material = FloorType[CurrentLevel];
                Base.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(Base.GetComponent<Room>().Floor.transform.localScale.x, Base.GetComponent<Room>().Floor.transform.localScale.z);

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

            int Indeex = GridList.IndexOf(FinalFinalX);

            GameObject BRdoor = BossDoor;

            if (RoomList[Indeex].GetComponent<Room>().NormalRoom == false)
            {
                BRdoor = Door;
            }


            GameObject Doors = Instantiate(BRdoor, RoomList[Indeex].transform);

            Doors.transform.localPosition = RoomList[Indeex].GetComponent<Room>().DoorLocations[4];
            Doors.transform.localRotation = Quaternion.Euler(RoomList[Indeex].GetComponent<Room>().DoorRotation[4]);
            Doors.transform.Find("LightAndTrigger").GetComponent<OneWayDoor>().BossRoom();
            GameObject GenerateBoss;
            switch (CurrentLevel){
                case 0:
                    GenerateBoss = BossesLevel1[Random.Range(0, (BossesLevel1.Count))];
                    break;
                case 1:
                    GenerateBoss = BossesLevel2[Random.Range(0, (BossesLevel2.Count))];
                    break;
                case 2:
                    GenerateBoss = BossesLevel3[Random.Range(0, (BossesLevel3.Count))];
                    break;
                case 3:
                    GenerateBoss = BossesLevel4[Random.Range(0, (BossesLevel4.Count))];
                    break;
                case 4:
                    GenerateBoss = BossesLevel5[Random.Range(0, (BossesLevel5.Count))];
                    break;
                case 5:
                    GenerateBoss = BossesLevel6[Random.Range(0, (BossesLevel6.Count))];
                    break;
                // for testing.
                default:
                    GenerateBoss = Boss[CurrentLevel+1];
                    break;
            }

            GameObject BossRoom = Instantiate(GenerateBoss, transform); // 
            Room BRoom = BossRoom.GetComponent<Room>();

            BossRoom.transform.position = new Vector3(0f, 0f, (FinalY + 2) * RoomMapLocationY);

            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().ConRoom = BossRoom;
            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().StartBossMusic = true;

            BRoom.Floor.GetComponent<MeshRenderer>().material = FloorType[CurrentLevel];
            BRoom.GetComponent<Room>().Floor.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(BRoom.GetComponent<Room>().Floor.transform.localScale.x * 0.75f, BRoom.GetComponent<Room>().Floor.transform.localScale.z * 0.75f);

            Doors.transform.Find("LightAndTrigger").GetComponent<OneWayDoor>().DoorPortal = new Vector3(BossRoom.transform.position.x + BRoom.StartLocation.x, BossRoom.transform.position.y + +BRoom.StartLocation.y, BossRoom.transform.position.z + +BRoom.StartLocation.z);

            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight += ((FinalY + 2) * RoomMapLocationY) + BRoom.CamTop;
            Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight2 += ((FinalY + 2) * RoomMapLocationY) + BRoom.CamBot;
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
                var RandomObstacle = Random.Range(0, BRoom.RoomObstacles.Count);
                GameObject Rubble = Instantiate(BRoom.RoomObstacles[RandomObstacle], BossRoom.transform);
                Rubble.transform.position = new Vector3(BossRoom.transform.position.x, BossRoom.transform.position.y, BossRoom.transform.position.z - 2f);
            }

            RoomList[Indeex].GetComponent<Room>().DoorList.Add(Doors);
            BossRoom.GetComponent<Room>().BuildRoomNavMesh(); // For some reason navmesh for bossroom has to be built now or things fuck up. 

            BossRoom.SetActive(false);
            BRoom.HasLoot = true;
            //End of Boss Script.

            //Calling Loot Script.
            GiveMonstersLoot();

            foreach (var Mini in MiniMapList)//creates Minimap.
            {
                GameObject MiniMapBlock = Instantiate(RoomBlock, new Vector3((Mini.x * 3.5f) - 1000, 0, Mini.y * 3.5f), transform.rotation, transform); // the -1000 is kinda hacky, just to move it out of the scene to ignore lights. Could do with script. RenderWithShader but MEH.
                RoomList[Counter].GetComponent<Room>().MimiMapBlock = MiniMapBlock;
                var MMIconLoc = RoomList[Counter].GetComponent<Room>().RoomBeforeBossRoomMiniMapIconLocation;

                foreach (var item in RoomList[Counter].GetComponent<Room>().MiniMapDoors)
                {
                    GameObject MMD = Instantiate(MiniMapDoor, MiniMapBlock.transform.position, MiniMapBlock.transform.rotation, MiniMapBlock.transform);
                    switch (item)
                    {
                        case 1:
                            MMD.transform.localPosition = new Vector3(-0.35f, 1, 0.35f); // top left 
                            break;
                        case 2:
                            MMD.transform.localPosition = new Vector3(0.35f, 1, -0.35f); // bottom right 
                            break;
                        case 3:
                            MMD.transform.localPosition = new Vector3(-0.35f, 1, -0.35f); // bottom left 
                            break;
                        case 4:
                            MMD.transform.localPosition = new Vector3(0.35f, 1, 0.35f); // top right
                            break;
                        case 5:
                            MMD.transform.localPosition = MMIconLoc;
                            break;
                        default:
                            break;
                    }
                    foreach (var door in RoomList[Counter].GetComponent<Room>().DoorList)
                    {
                        if (door.transform.GetChild(0).GetComponent<OneWayDoor>().MoveMMCam == item)
                        {
                            door.transform.GetChild(0).GetComponent<OneWayDoor>().ThisDoorsMiniMapBlock = MMD;
                        }
                    }
                }

                if (RoomList[Counter].GetComponent<Room>().ThisRoomHasShop)
                {
                    GameObject MMD = Instantiate(MiniMapShop, MiniMapBlock.transform.position, MiniMapBlock.transform.rotation, MiniMapBlock.transform);
                    MMD.transform.localPosition = new Vector3(0, 1, 0);
                }
                if( RoomList[Counter].GetComponent<Room>().ThisRoomHasEvent){
                    GameObject MMD = Instantiate(MiniMapEvent, MiniMapBlock.transform.position, MiniMapBlock.transform.rotation, MiniMapBlock.transform);
                    MMD.transform.localPosition = new Vector3(0, 1, 0);
                }

                LastRoom = MiniMapBlock;

                if (Counter > 0) // so wont deactive first room
                {
                    MiniMapBlock.SetActive(false);
                }
                else
                {
                    RoomList[Counter].GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = Color.blue;
                }

                Counter++;
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
                DoorPortal1 = new Vector3(-5, 0, 5);
                DoorPortal2 = new Vector3(5, 0, -5);
                MiniMapCur = 2;
                break;
            case 2: //Down
                DoorPortal1 = new Vector3(5, 0, -5);
                DoorPortal2 = new Vector3(-5, 0, 5);
                MiniMapCur = 1;
                break;
            case 3: //Left
                DoorPortal1 = new Vector3(-5, 0, -5);
                DoorPortal2 = new Vector3(5, 0, 5);
                MiniMapCur = 4;
                break;
            case 4: // Right
                DoorPortal1 = new Vector3(5, 0, 5);
                DoorPortal2 = new Vector3(-5, 0, -5);
                MiniMapCur = 3;
                break;
        }

        GameObject Doors = Instantiate(Door, CurrentRoom.transform);
        GameObject Doors2 = Instantiate(Door, Base.transform);
        Room RoomCurrent = CurrentRoom.GetComponent<Room>();
        Room RoomConnecting = Base.GetComponent<Room>();

        Doors.transform.localPosition = RoomCurrent.DoorLocations[RPos - 1];
        Doors2.transform.localPosition = RoomConnecting.DoorLocations[MiniMapCur - 1];
        Doors.transform.localRotation = Quaternion.Euler(RoomCurrent.DoorRotation[RPos - 1]);
        Doors2.transform.localRotation = Quaternion.Euler(RoomConnecting.DoorRotation[MiniMapCur - 1]);

        DoorPortal1 += Doors2.transform.position;
        DoorPortal2 += Doors.transform.position;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().DoorPortal = DoorPortal1;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().DoorPortal = DoorPortal2;

        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight += (YPos * RoomMapLocationY) + RoomCurrent.CamTop;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight2 += (YPos * RoomMapLocationY) + RoomCurrent.CamBot;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().leftEdge += (XPos * RoomMapLocationX) + RoomCurrent.CamLeft;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().rightEdge += (XPos * RoomMapLocationX) + RoomCurrent.CamRight;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().CamCenter = RoomCurrent.CamCenter;

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight += (YGrid * RoomMapLocationY) + RoomConnecting.CamTop;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().doorHeight2 += (YGrid * RoomMapLocationY) + RoomConnecting.CamBot;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().leftEdge += (XGrid * RoomMapLocationX) + RoomConnecting.CamLeft;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().rightEdge += (XGrid * RoomMapLocationX) + RoomConnecting.CamRight;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().CamCenter =  RoomConnecting.CamCenter;

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().ConRoom = Base;
        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().CurRoom = CurrentRoom;

        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().ConRoom = CurrentRoom;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().CurRoom = Base;

        Doors.transform.GetChild(0).GetComponent<OneWayDoor>().ConnectingDoor = Doors2;
        Doors2.transform.GetChild(0).GetComponent<OneWayDoor>().ConnectingDoor = Doors;

        CurrentRoom.GetComponent<Room>().DoorList.Add(Doors);
        Base.GetComponent<Room>().DoorList.Add(Doors2);

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
            //  GameObject.Destroy(child.gameObject); // This was crashing the game.
            child.gameObject.SetActive(false);
            float DestroyTimer = Random.Range(3f, 4);
            Destroy(child.gameObject, DestroyTimer);

        }
        CurrentLevel++;
        maxRoom = Random.Range(5+CurrentLevel, 7+ CurrentLevel);
        minRoom = Random.Range(5+ CurrentLevel, 7+ CurrentLevel);

        LevelHasHadEvent = false;
        LevelHasHadMiniBoss = false;

        MonstersThatCanDropLoot.Clear();
        GridList.Clear();
        MiniMapList.Clear();
        RoomList.Clear();
        MonsterType_.Clear();
        Counter = 0;
        MiniMapX = 0;

        if (HardCodeRing1 != null)
        {
            Destroy(HardCodeRing1);
            Destroy(HardCodeRing2);
        }

        MiniMapZ = 0;
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
        LastRoom = null;
        GenerateFirstRoom();
    }

    private void GiveMonstersLoot()
    {
        var RandomLoot = Random.Range(0, MonstersThatCanDropLoot.Count);
        var RandomPotion = Random.Range(0, MonstersThatCanDropLoot.Count);
        if (AmountOfLoot > 0)
        {
            var LootBag = Random.Range(0, Tokens.Count);
            MonstersThatCanDropLoot[RandomLoot].MonsterHasLoot = true;
            MonstersThatCanDropLoot[RandomLoot].MonsterLoot = Tokens[LootBag];
            MonstersThatCanDropLoot.RemoveAt(RandomLoot);
            AmountOfLoot--;
            GiveMonstersLoot();
        }
        if (AmountOfPotions > 0)
        {
            MonstersThatCanDropLoot[RandomPotion].MonsterHasLoot = true;
            MonstersThatCanDropLoot[RandomPotion].MonsterLoot = Healing[0];
            MonstersThatCanDropLoot.RemoveAt(RandomPotion);

            AmountOfPotions--;
            GiveMonstersLoot();
        }

        if (AmountOfPotions == 0 && AmountOfLoot == 0)
        {
            foreach (var Monster in MonstersThatCanDropLoot)
            {
                Monster.MonsterCanDropGold = true;
                Monster.MonsterGold = Gold;
                Monster.GoldAmount = Mathf.RoundToInt((Monster.MonsterCR/(Monster.SpawnMultiNumber+1))+((CurrentLevel+2)*1.25f));
                Monster.GoldDropChance = 30 + MenuScript.GoldDropChance;
            }
        }
    }

    private void MiniBossLoot(Monster Mboss)
    {
        Mboss.MonsterCanDropGold = true;
        Mboss.MonsterGold = Gold;
        Mboss.MiniBossMonster = true;
        Mboss.GoldAmount = (CurrentLevel*4)+25;
        Mboss.GoldDropChance = 100;
    }
    private void RoomEventLoot(Room ERoom)
    {
        var EventLootRanom = Random.Range(0, 100);
        if (EventLootRanom < 75)
        {
            var LootBag = Random.Range(0, Tokens.Count);
            ERoom.EventLoot = Tokens[LootBag];
        //}else if(EventLootRanom < 85){
        //    ERoom.EventLoot = Gold;
        //    ERoom.EventGold = (CurrentLevel * 5) + 35;         
        }else
        {
            var LootBag = Random.Range(0, Items.Count);
            ERoom.EventLoot = Items[LootBag];
        }
    }
}
