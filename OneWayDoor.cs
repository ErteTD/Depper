using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{

    public float doorHeight;
    public float doorHeight2;
    public int MoveMMCam;
    public GameObject Player_;
    public GameObject MainCamera;
    public GameObject MiniCamera;
    public GameObject ConRoom;
    public GameObject CurRoom;
    public GameObject ConnectingDoor;
    public GameObject ThisDoorsMiniMapBlock;
    private GameObject[] Monsters;
    private bool clicked;
    public float leftEdge;
    public float rightEdge;
    private Color roomCol;

    public Vector3 DoorPortal;
    public Vector3 BossRoomLoc; // is this used?
    public bool BossNextLevel;
    public float CamCenter;

    public bool TimekeeperHardCodeDeleteRings;
    public GameObject RingOne;
    public GameObject RingTwo;
    private bool CantClickDoorDuringLoad;
    public GameObject LoadSceneManager;
    public bool StartBossMusic;

    public void Start()
    {
        Player_ = GameObject.Find("Player");
        MainCamera = GameObject.Find("MainCamera");
        MiniCamera = GameObject.Find("MiniMapCamera");
        LoadSceneManager = GameObject.Find("LoadScreenTrigger");
        //   doorHeight = doorHeight2;
        //   Monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (ConRoom != null)
        {
            roomCol = ConRoom.GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color;
        }
    }

    void Update()
    {
            float dist = Vector3.Distance(Player_.transform.position, transform.position);
        
        if (dist < 8 && clicked == true && !CantClickDoorDuringLoad)
        {
            clicked = false;
            CantClickDoorDuringLoad = true;
            LoadSceneManager.GetComponent<LoadScreen>().FadeToLevel(this);

            if (StartBossMusic)
            {
                GameManager.FindObjectOfType<GameManager>().EnableSave = false;
            }
        }
    }

    void ColorMiniMapDoorsGreen()
    {
            ThisDoorsMiniMapBlock.GetComponent<Renderer>().material.color = Color.green;
            ConnectingDoor.transform.GetChild(0).GetComponent<OneWayDoor>().ThisDoorsMiniMapBlock.GetComponent<Renderer>().material.color = Color.green;
    }

    public void ResetCamera()
    {
        MiniCamera.transform.position -= new Vector3(GameManager.FindObjectOfType<GameManager>().changeInX, 0, GameManager.FindObjectOfType<GameManager>().changeInZ);
        GameManager.FindObjectOfType<GameManager>().changeInX = 0;
        GameManager.FindObjectOfType<GameManager>().changeInZ = 0;

        switch (MoveMMCam)
        {
            case 1:
                MiniCamera.transform.position += new Vector3(-3.5f, 0, 3.5f);
                break;
            case 2:
                MiniCamera.transform.position += new Vector3(3.5f, 0, -3.5f);
                break;
            case 3:
                MiniCamera.transform.position += new Vector3(-3.5f, 0, -3.5f);
                break;
            case 4:
                MiniCamera.transform.position += new Vector3(3.5f, 0, 3.5f);
                break;
            case 5:
                MiniCamera.transform.position = new Vector3(-1000, 100, 100);
                break;
            default: // if 0, move it back. So after boss level.
                MiniCamera.transform.position = new Vector3(-1000, 100, 0);
                break;
        }
    }

    public void EnterRoom()
    {

        if (ConRoom != null && CurRoom != null)
        {
            ConRoom.SetActive(true);
            CurRoom.SetActive(false);
            GetComponent<Light>().color = Color.green;
            ConnectingDoor.transform.GetChild(0).GetComponent<Light>().color = Color.green;

            //MiniMapStuff
            ConRoom.GetComponent<Room>().MimiMapBlock.SetActive(true);
            CurRoom.GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = roomCol;
            ConRoom.GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = Color.blue;

            ColorMiniMapDoorsGreen();
        }
        else if (ConRoom != null) // bossroom stuff.
        {
            ConRoom.SetActive(true);
        }
        if (StartBossMusic)
        {
            GameManager.FindObjectOfType<GameManager>().NormalTheme.Stop();
            GameManager.FindObjectOfType<GameManager>().BossBattle.Play();
        }
        GameManager.FindObjectOfType<GameManager>().ShopWindowFunc(false);

        FindObjectOfType<GameManager>().SelectCursor(false);
        ResetCamera();
        FindObjectOfType<CastWeapon>().TelePortDoor = true;
        FindObjectOfType<GameManager>().SetCurrentRoom(ConRoom);




        MainCamera.GetComponent<CamController>().zLevel = doorHeight;
        MainCamera.GetComponent<CamController>().zLevel2 = doorHeight2;
        MainCamera.GetComponent<CamController>().xLimit1 = leftEdge;
        MainCamera.GetComponent<CamController>().xLimit2 = rightEdge;
        MainCamera.GetComponent<CamController>().zTest = CamCenter;
        Invoke("WarpPlayerAfterNavMeshIsCreated", 0.05f);
        Invoke("TrustMe", 1f);

        if (BossNextLevel) // starts the next level.
        {
            if (TimekeeperHardCodeDeleteRings)
            {
                Destroy(RingOne);
                Destroy(RingTwo);
            }
            FindObjectOfType<MapGrid>().NextLevel();
            GameManager.FindObjectOfType<GameManager>().NormalTheme.Play();
            GameManager.FindObjectOfType<GameManager>().BossBattle.Stop();
        }

    }
    void WarpPlayerAfterNavMeshIsCreated()
    {
        Player_.GetComponent<Player>().agent.Warp(DoorPortal);
        Player_.GetComponent<Player>().targetPosition = Player_.transform.position;
        Player_.GetComponent<Player>().CurrentRoom = ConRoom;
        Player_.GetComponent<Player>().RoomChangeDestroyPreviousRoomSpells();
    }

    private void TrustMe()
    {
        clicked = false;
        CantClickDoorDuringLoad = false;
    }
    public void BossRoom()
    {
        GetComponent<Light>().color = Color.red;
    }

    private void OnMouseOver()
    {
        if (GameManager.FindObjectOfType<GameManager>().ShopWindow.activeSelf == false && GameManager.FindObjectOfType<GameManager>().EscMenu.activeSelf == false)
        {
            FindObjectOfType<GameManager>().SelectCursor(true);
            GetComponent<Light>().intensity = 1.5f;
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLoc)))
            {
                clicked = true;
            }
        }
    }

    public void ClickedElsewhere()
    {
        clicked = false;
    }

    private void OnMouseExit()
    {
        FindObjectOfType<GameManager>().SelectCursor(false);
        GetComponent<Light>().intensity = 1f;
    }


}
