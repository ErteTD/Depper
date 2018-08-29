using System.Collections;
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

        ObjectType.text = ItemTName;
        //  Invoke("DisableAgent", 0.1f);

        Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 1;
        Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().obstacleAvoidanceType = 0;
        Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().stoppingDistance = 5;
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        rotation = Quaternion.Euler(0, 0, 0);
        

    }

    public void DisableAgent()
    {
        Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }

    private void LateUpdate()
    {
        Parent.transform.rotation = rotation;
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }
    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);

        float dist = Vector3.Distance(Player_.transform.position, transform.position);



        if ((Vector3.Distance(Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().destination, transform.position) > 0.5f))
        {
            Parent.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = Player_.transform.position;
            //Vector3 dir = new Vector3(Player_.transform.position.x, 1.5f, Player_.transform.position.z) - this.transform.position;
            //float distThisFrame = 0.5f * Time.deltaTime;
            //transform.parent.transform.Translate(dir.normalized * distThisFrame, Space.World);
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

        GameManager manager = GameManager.FindObjectOfType<GameManager>();

        switch (ItemType)
        {
            case 1:
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
                }
                break;
            case 4:
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
                }
                break;


        }
        Destroy(Parent);
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0f);
        ShowName.SetActive(true);
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButtonDown(1)))
        {
            clicked = true;
        }
    }

    private void OnMouseExit()
    {
        ShowName.SetActive(false);
        GetComponent<Renderer>().material.SetFloat("_Metallic", 1f);
    }

}
