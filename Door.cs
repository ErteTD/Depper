using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float doorHeight;
    public float doorHeight2;
    public float doorHeight3;

    public float bottomCamera1, bottomCamera2;
    public float topCamera1, topCamera2;

    public GameObject Player_;
    public GameObject MainCamera;
    private bool clicked;
    public bool Horizontal;
    private GameObject[] Monsters;

    public void Start()
    {
        Player_ = GameObject.Find("Player");

        //   doorHeight = doorHeight2;
        Monsters = GameObject.FindGameObjectsWithTag("Monster");
    }


    void Update () {

        float dist = Vector3.Distance(Player_.transform.position, transform.position);

        if (dist < 5 && clicked == true)
        {
            EnterRoom();
        }
    }

    public void EnterRoom()
    {
       // MainCamera.GetComponent<CamController>().CameraRoom(doorHeight); // not working currently
        ResetMonsters();
        SwitchHeight();
    }



    private void SwitchHeight() // Some code to set Monster aggros to 0, so they wont run through the dungeon if you go through a door.
    {
        clicked = false;
        if (!Horizontal)
        {
            if (doorHeight == doorHeight2)
            {
                doorHeight = doorHeight3;
                Player_.GetComponent<player>().agent.Warp(new Vector3(transform.position.x, Player_.transform.position.y, transform.position.z + 6f));
                MainCamera.GetComponent<CamController>().xLimit1 = topCamera1;
                MainCamera.GetComponent<CamController>().xLimit2 = topCamera2;

            }
            else
            {
                doorHeight = doorHeight2;
                Player_.GetComponent<player>().agent.Warp(new Vector3(transform.position.x, Player_.transform.position.y, transform.position.z - 6f));

                MainCamera.GetComponent<CamController>().xLimit1 = bottomCamera1;
                MainCamera.GetComponent<CamController>().xLimit2 = bottomCamera2;

            }
        }
        else
        {
            if (doorHeight == doorHeight2)
            {
                doorHeight = doorHeight3;
                Player_.GetComponent<player>().agent.Warp(new Vector3(transform.position.x +6f, Player_.transform.position.y, transform.position.z));
                MainCamera.GetComponent<CamController>().xLimit1 = topCamera1;
                MainCamera.GetComponent<CamController>().xLimit2 = topCamera2;

            }
            else
            {
                doorHeight = doorHeight2;
                Player_.GetComponent<player>().agent.Warp(new Vector3(transform.position.x -6f, Player_.transform.position.y, transform.position.z));

                MainCamera.GetComponent<CamController>().xLimit1 = bottomCamera1;
                MainCamera.GetComponent<CamController>().xLimit2 = bottomCamera2;

            }

        }


        Player_.GetComponent<player>().targetPosition = Player_.transform.position;
    }

    private void ResetMonsters()
    {

        foreach (GameObject monster in Monsters)
        {
            if (monster != null)
            {
                monster.GetComponent<Monster>().agent.destination = monster.GetComponent<Monster>().startPosition;
                monster.GetComponent<Monster>().AggroRange = monster.GetComponent<Monster>().AggroRange_;
            }
        }
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
