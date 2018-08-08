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
    public GameObject ConnectingRoom;
    private GameObject[] Monsters;
    private bool clicked;
    public float leftEdge;
    public float rightEdge;
    private Color roomCol;

    public Vector3 DoorPortal;
    public Vector3 BossRoomLoc; // is this used?
    public bool BossNextLevel;
    public float CamCenter;

    public void Start()
    {
        Player_ = GameObject.Find("Player");
        MainCamera = GameObject.Find("MainCamera");
        MiniCamera = GameObject.Find("MiniMapCamera");
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
        if (dist < 6 && clicked == true)
        {
            clicked = false;



            if (ConRoom != null && CurRoom != null)
            {
                ConRoom.SetActive(true);
                CurRoom.SetActive(false);
                GetComponent<Light>().color = Color.green;
                ConnectingRoom.transform.GetChild(0).GetComponent<Light>().color = Color.green;

                //MiniMapStuff
                ConRoom.GetComponent<Room>().MimiMapBlock.SetActive(true);
                CurRoom.GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = roomCol;
                ConRoom.GetComponent<Room>().MimiMapBlock.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (ConRoom != null) // bossroom stuff.
            {
                ConRoom.SetActive(true);
            }



            ResetCamera(); // resets any movement
            switch (MoveMMCam)
            {
                case 1:
                    MiniCamera.transform.position += new Vector3(-7f, 0, 7f);
                    break;
                case 2:
                    MiniCamera.transform.position += new Vector3(7f, 0, -7f);
                    break;
                case 3:
                    MiniCamera.transform.position += new Vector3(-7f, 0, -7f);
                    break;
                case 4:
                    MiniCamera.transform.position += new Vector3(7f, 0, 7f);
                    break;
                case 5:
                    MiniCamera.transform.position = new Vector3(-1000, 100, 100);
                    break;
                default: // if 0, move it back. So after boss level.
                    MiniCamera.transform.position = new Vector3(-1000, 100, 0);
                    break;
            }
            EnterRoom();
        }
    }

    public void ResetCamera()
    {
        MiniCamera.transform.position -= new Vector3(GameManager.FindObjectOfType<GameManager>().changeInX, 0, GameManager.FindObjectOfType<GameManager>().changeInZ);
        GameManager.FindObjectOfType<GameManager>().changeInX = 0;
        GameManager.FindObjectOfType<GameManager>().changeInZ = 0;
    }

    public void EnterRoom()
    {
        FindObjectOfType<CastWeapon>().TelePortDoor = true;
        // MainCamera.GetComponent<CamController>().CameraRoom(doorHeight, doorHeight2);
        // ResetMonsters();

        Player_.GetComponent<Player>().agent.Warp(DoorPortal);

        MainCamera.GetComponent<CamController>().zLevel = doorHeight;
        MainCamera.GetComponent<CamController>().zLevel2 = doorHeight2;

        MainCamera.GetComponent<CamController>().xLimit1 = leftEdge;
        MainCamera.GetComponent<CamController>().xLimit2 = rightEdge;

        MainCamera.GetComponent<CamController>().zTest = CamCenter;
        Player_.GetComponent<Player>().targetPosition = Player_.transform.position;
        Invoke("TrustMe", 1f);


        if (BossNextLevel) // starts the next level.
        {
            FindObjectOfType<MapGrid>().NextLevel();
        }

    }

    private void TrustMe()
    {
        clicked = false;
    }

    //private void ResetMonsters() //useless currently.
    //{

    //    foreach (GameObject monster in Monsters)
    //    {
    //        if (monster != null && monster.GetComponent<Monster>())
    //        {
    //            monster.GetComponent<Monster>().agent.destination = monster.GetComponent<Monster>().startPosition;
    //            monster.GetComponent<Monster>().AggroRange = monster.GetComponent<Monster>().AggroRange_;
    //        }
    //    }
    //}

    public void BossRoom()
    {
        GetComponent<Light>().color = Color.red;
    }


    private void OnMouseOver()
    {
        GetComponent<Light>().intensity = 1.5f;
        if (Input.GetMouseButton(0))
        {
            clicked = true;
        }
    }

    public void ClickedElsewhere()
    {
        clicked = false;
    }

    private void OnMouseExit()
    {
        GetComponent<Light>().intensity = 1f;
    }


}
