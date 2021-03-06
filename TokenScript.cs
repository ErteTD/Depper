﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenScript : MonoBehaviour
{

    public int TokenType;
    public int ItemType;
    public int ItemID;
    public string ItemName;
    private string ItemTName;
    public GameObject Player_;
    public GameObject child_;
    public Text ObjectName;
    public Text ObjectType;
    public GameObject ShowName;
    public float speed = 10f;
    public GameObject Parent;
    private bool clicked;
    private UnityEngine.AI.NavMeshAgent agent;
    [Header("Healing Potion")]
    public int healing;
    private Quaternion rotation;
    public void Awake()
    {
        Player_ = GameObject.Find("Player");
        ObjectName.text = ItemName;

        switch (ItemType)
        {
            case 1:
                ItemTName = "Spell";
                ObjectType.color = Color.blue;
                break;
            case 2:
                var RandomHealing = Random.Range(3, 6);
                healing = RandomHealing;

                ItemTName = "Potion";
                ObjectType.color = Color.red;
                ObjectName.text = ItemName + " - " + healing;
                break;
            case 3:
                ItemTName = "Weapon";
                ObjectType.color = Color.yellow;
                break;
            case 4:
                ItemTName = "Armor";
                ObjectType.color = Color.green;
                break;

        }

        agent = Parent.GetComponent<UnityEngine.AI.NavMeshAgent>();


        ObjectType.text = ItemTName;
        //  Invoke("DisableAgent", 0.1f);
        agent.speed = 1;
        //    Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().obstacleAvoidanceType = ;
        agent.stoppingDistance = 5;
       // Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        if (ItemType == 1 || ItemType == 2)
        {
            transform.localPosition = new Vector3(0, 4f, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, 2.5f, 0);
        }


        if (!agent.isOnNavMesh)
        {
            agent.enabled = false;
        }

        rotation = Quaternion.Euler(0, 0, 0);
        
    }

    //public void DisableAgent()
    //{
    //    Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
    // //   transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    //}
    //private void EnableAgent()
    //{
    //    Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    //}

    private void LateUpdate()
    {
        Parent.transform.rotation = rotation;
    }
    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);
        float dist = Vector3.Distance(Player_.transform.position, transform.position);

        if (agent.enabled == true)
        {
            if ((Vector3.Distance(agent.destination, transform.position) > 0.5f))
            {
                agent.destination = Player_.transform.position;
            }
        }
        else
        {
            Vector3 dir = Player_.transform.position - transform.parent.transform.position;
            if (dir.magnitude > 5)
            {
               transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, Player_.transform.position, Time.deltaTime);
            }
        }

        if (dist < 2.6 && clicked == true)
        {
            PickUp();
        }
    }

    public void ClickedElsewhere()
    {
        clicked = false;
    }

    public void PickUp()
    {
        GameObject effect = Instantiate(child_, new Vector3(this.transform.position.x, 2f, this.transform.position.z), this.transform.rotation, this.transform);
        Destroy(effect, 2f);
        effect.transform.parent = null;
        FindObjectOfType<GameManager>().SelectCursor(false);

        GameManager manager = GameManager.FindObjectOfType<GameManager>();

        switch (ItemType)
        {
            case 1:
                manager.EnableSpellSlotEffect();
                switch (TokenType)
                {
                    case 1:
                        manager.meteorToken++;
                        break;
                    case 2:
                        manager.coneToken++;
                        break;
                    case 3:
                        manager.ghostToken++;
                        break;
                    case 4:
                        manager.doubleToken++;
                        break;
                    case 5:
                        manager.splitToken++;
                        break;
                    case 6:
                        manager.channelingToken++;
                        break;
                    case 7://start here
                        manager.empowerToken++;
                        break;
                    case 8:
                        manager.hastenToken++;
                        break;
                    case 9:
                        manager.boostToken++;
                        break;
                    case 10:
                        manager.bhToken++;
                        break;
                    case 11:
                        manager.pushToken++;
                        break;
                    case 12:
                        manager.poolToken++;
                        break;
                    case 13:
                        manager.ChaosToken++;
                        break;
                    case 14:
                        manager.CompToken++;
                        break;
                    case 15:
                        manager.AimToken++;
                        break;
                }
                break;

            case 2:
                switch (ItemID)
                {
                    case 1:
                        Player_.GetComponent<Player>().Heal(healing);
                        break;
                }
                break;
            case 3:
                manager.EnableItem(true, true);
                switch (ItemID)
                {
                    case 1:
                        manager.SpiderWeaponToken++;
                        break;
                    case 2:
                        manager.BlinkWeaponToken++;
                        break;
                    case 3:
                        manager.FireWeaponToken++;
                        break;
                    case 4:
                        manager.IKWeaponToken++;
                        break;
                    case 5:
                        manager.BlobWeaponToken++;
                        break;
                    case 6:
                        manager.TimeWeaponToken++;
                        break;
                    case 7:
                        manager.FrostWeaponToken++;
                        break;
                    case 8:
                        manager.StrenghtWeaponToken++;
                        break;
                    case 9:
                        manager.MadnessWeaponToken++;
                        break;
                }
                break;
            case 4:
                manager.EnableItem(false, true);
                switch (ItemID)
                {

                    case 1:
                        manager.SpiderArmorToken++;
                        break;
                    case 2:
                        manager.IlluArmorToken++;
                        break;
                    case 3:
                        manager.RoidArmorToken++;
                        break;
                    case 4:
                        manager.IKArmorToken++;
                        break;
                    case 5:
                        manager.BlobArmorToken++;
                        break;
                    case 6:
                        manager.FireArmorToken++;
                        break;
                    case 7:
                        manager.FrostArmorToken++;
                        break;
                    case 8:
                        manager.StoneArmorToken++;
                        break;
                    case 9:
                        manager.ThunderArmorToken++;
                        break;
                }
                break;


        }

        manager.PickedUpItem();
        Destroy(Parent);
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0f);
        FindObjectOfType<GameManager>().SelectCursor(true);
        ShowName.SetActive(true);
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLoc)))
        {
            clicked = true;
        }
    }

    private void OnMouseExit()
    {
        ShowName.SetActive(false);
        FindObjectOfType<GameManager>().SelectCursor(false);
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.5f);
    }

}
