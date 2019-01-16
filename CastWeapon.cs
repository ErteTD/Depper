using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastWeapon : MonoBehaviour
{
    public int CurrentArmor;
    public int CurrentWeapon;

    public ToolTipScript tooltip;
    private float CD1;
    private float CD1_;
    private bool spellSlot1rdy;
    public Text coolDownTextDisplay1;
    public Image slot1;
    public GameObject Player1;
    public ParticleSystem TelePortEffect;
    [HideInInspector] public float spellSlotCD;
    public List<GameObject> Weapons;
    [HideInInspector] Vector3 TeleLoc;
    [HideInInspector] ParticleSystem Tele1Effect;
    [HideInInspector] public bool TelePortDoor;

    private float CD2;
    private float CD2_;
    [HideInInspector] public bool spellSlot2rdy;
    public Text coolDownTextDisplay2;
    public Image slot2;
    [HideInInspector] public float spellSlotCD2;
    public List<GameObject> Armor;

    public void SelectWeapon(int ID)
    {
        CurrentWeapon = ID;
        tooltip.OpenWeaponSelect();

        if (ID == 3)
        {
            Player1.GetComponent<Player>().BigBoyGlow.SetActive(true);
        }
        else if (Player1.GetComponent<Player>().BigBoyGlow.activeSelf == true)
        {
            Player1.GetComponent<Player>().BigBoyGlow.SetActive(false);
        }
        if (ID == 5 && Player1.GetComponent<Player>().BlobWeaponEquppied == false)
        {
            WeaponAttack();
        }
        if (ID != 5 && Player1.GetComponent<Player>().BlobWeaponEquppied == true)
        {
            Player1.GetComponent<Player>().BlobWeaponEquppied = false;
        }
    }
    public void SelectArmor(int ID)
    {
        CurrentArmor = ID;
        tooltip.OpenArmorSelect();
        if (ID == 3 || ID == 5)
        {
            ArmorTrigger();
            if (!spellSlot2rdy)
            {
                Invoke("ArmorTrigger", CD2 + 0.1f);
            }
        }
        else if (Player1.GetComponent<Player>().ChangeColor.GetComponent<Renderer>().material.color == Color.red)
        {
            Player1.GetComponent<Player>().ChangeColor.GetComponent<Renderer>().material.color = Color.white;
            Player1.GetComponent<Player>().MovementSpeed = 9;
            Player1.GetComponent<Player>().MovementSpeed_ = 9;
            Player1.GetComponent<Player>().agent.speed = 9;
            Player1.GetComponent<Player>().CantBeSlowed = false;
        }
        if (ID != 5)
        {
            Player1.GetComponent<Player>().BlobArmorStatus(false); // Add here code to diable potential particle effect that is active when slippers are used.
        }

        if (ID != 4 && Player1.GetComponent<Player>().fullhealth == 15) // armor 4 non frost explosion stuff.
        {
            Player1.GetComponent<Player>().fullhealth = 10;
            if (Player1.GetComponent<Player>().health > 10)
            {
                Player1.GetComponent<Player>().health = 10;
            }

            Player1.GetComponent<Player>().HealthText.text = Player1.GetComponent<Player>().health.ToString("F1");
            Player1.GetComponent<Player>().HealthBar.fillAmount = Player1.GetComponent<Player>().health / Player1.GetComponent<Player>().fullhealth;

        }
        else if (ID == 4)
        {
            Player1.GetComponent<Player>().fullhealth = 15;
            Player1.GetComponent<Player>().HealthText.text = Player1.GetComponent<Player>().health.ToString("F1");
            Player1.GetComponent<Player>().HealthBar.fillAmount = Player1.GetComponent<Player>().health / Player1.GetComponent<Player>().fullhealth;
        }
    }


    public void WeaponAttack()
    {
        if (spellSlot1rdy == true && Player1.GetComponent<Player>().DieOnce == false)
        {
            switch (CurrentWeapon)
            {
                case 0:
                    spellSlotCD = 0.5f;
                    break;


                case 1:
                    WeaponSpider spell = Weapons[CurrentWeapon].GetComponent<WeaponSpider>();
                    spellSlotCD = spell.cooldown;
                    GameObject spider1 = Instantiate(spell.ItemObject, transform.position, transform.rotation, transform);
                    GameObject spider2 = Instantiate(spell.ItemObject, transform.position, transform.rotation, transform);

                    spider1.transform.position += spider1.transform.right * 2;
                    spider2.transform.position += spider2.transform.right * -2;

                    spider1.transform.parent = null;
                    spider2.transform.parent = null;

                    break;
                case 2:
                    Blink spell2 = Weapons[CurrentWeapon].GetComponent<Blink>();
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Floor" || hit.collider.gameObject.tag == "Monster" || hit.collider.gameObject.tag == "Token" || hit.collider.gameObject.tag == "Door")
                    {
                        TelePortDoor = false;
                        Tele1Effect = Instantiate(TelePortEffect, Player1.transform.position, Player1.transform.rotation);
                        Tele1Effect.transform.parent = Player1.transform;

                        ParticleSystem Tele1Effect2 = Instantiate(TelePortEffect, hit.point, Player1.transform.rotation);
                        TeleLoc = hit.point;
                        Destroy(Tele1Effect.transform.gameObject, 3.1f);
                        Destroy(Tele1Effect2.transform.gameObject, 3.1f);

                        Invoke("TelePortPlayer", 1f);
                        spellSlotCD = spell2.cooldown;
                    }
                    else
                    {
                        spellSlotCD = 0.5f;
                    }
                    break;
                case 3:
                    Blink spell3 = Weapons[CurrentWeapon].GetComponent<Blink>();
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit2;
                    if (Physics.Raycast(ray2, out hit2))
                    {
                        // Code Here.
                        Vector3 Pos = new Vector3(Player1.transform.position.x, 1.5f, Player1.transform.position.z);
                        Player1.transform.LookAt(hit2.point);
                        GameObject PoolObj = Instantiate(spell3.ItemObject, Pos + Player1.transform.forward * 4, transform.rotation, transform);
                        PoolObj.transform.parent = null;
                        PoolObj.transform.localScale = new Vector3(1, 1, 1);
                        PoolObj.GetComponent<BigBoyFire>().PoolNumb = 10;
                        PoolObj.GetComponent<BigBoyFire>().PlayerCasting = true;
                        PoolObj.GetComponent<BigBoyFire>().duration = 3f;
                        PoolObj.GetComponent<BigBoyFire>().damage = 1f;

                        spellSlotCD = spell3.cooldown;
                    }
                    else
                    {
                        spellSlotCD = 0.5f;
                    }
                    break;
                case 4:
                    Blink spell4 = Weapons[CurrentWeapon].GetComponent<Blink>();
                    // Code Here.
                    float OrbFacing = transform.rotation.eulerAngles.x;
                    float attackTimer = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject P1 = Instantiate(spell4.ItemObject, transform);
                        Player1.GetComponent<Player>().SpellsCastInThisRoom.Add(P1);
                        P1.transform.Rotate(0, OrbFacing, 0);
                        P1.transform.position = new Vector3(transform.position.x, 3, transform.position.z) + P1.transform.forward * 1.5f;
                        P1.GetComponent<OldKingAttack3>().FloatUpTimer += attackTimer;
                        P1.GetComponent<OldKingAttack3>().damage = 1;
                        P1.GetComponent<OldKingAttack3>().PlayerCast = true;
                        OrbFacing += 72; //360/5
                        attackTimer += 0.2f;
                    }
                    spellSlotCD = spell4.cooldown;
                    break;
                case 5:
                    Blink spell5 = Weapons[CurrentWeapon].GetComponent<Blink>();
                    Player1.GetComponent<Player>().BlobWeaponEquppied = true;
                    Player1.GetComponent<Player>().BlobWeaponObject = spell5.ItemObject;
                    spellSlotCD = spell5.cooldown;
                    break;
            }
            CD1 = spellSlotCD;
            CD1_ = spellSlotCD;
        }
    }

    public void TelePortPlayer()
    {
        if (TelePortDoor == false)
        {
            Tele1Effect.transform.parent = null;
            Player1.GetComponent<Player>().agent.Warp(TeleLoc);
            Player1.GetComponent<Player>().targetPosition = Player1.transform.position;
            Player1.GetComponent<Player>().agent.destination = Player1.transform.position;
        }
    }

    public void ArmorTrigger()
    {
        if (spellSlot2rdy == true && Player1.GetComponent<Player>().DieOnce == false)
        {
            switch (CurrentArmor)
            {
                case 0:
                    spellSlotCD2 = 0f;
                    break;

                case 1:
                    ArmorSpider spell = Armor[CurrentArmor].GetComponent<ArmorSpider>();
                    spellSlotCD2 = spell.cooldown;
                    GameObject spider1 = Instantiate(spell.ItemObject, transform.position, transform.rotation, transform);

                    spider1.transform.position += spider1.transform.forward * 2;

                    spider1.transform.parent = null;

                    break;
                case 2:
                    ArmorIllusion spell2 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell2.cooldown;
                    GameObject Illu = Instantiate(spell2.ItemObject, transform.position, transform.rotation, transform);
                    Illu.transform.parent = null;
                    Illu.transform.localPosition = new Vector3(Illu.transform.position.x, 3, Illu.transform.position.z);
                    break;

                case 3:
                    ArmorIllusion spell3 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell3.cooldown;
                    Player1.GetComponent<Player>().ChangeColor.GetComponent<Renderer>().material.color = Color.red;
                    Player1.GetComponent<Player>().MovementSpeed = 10.8f;
                    Player1.GetComponent<Player>().MovementSpeed_ = 10.8f;
                    Player1.GetComponent<Player>().agent.speed = 10.8f;
                    Player1.GetComponent<Player>().slowedDur = 0;
                    Player1.GetComponent<Player>().CantBeSlowed = true;

                    break;
                case 4:
                    ArmorIllusion spell4 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell4.cooldown;

                    GameObject Exp = Instantiate(spell4.ItemObject, transform.position, transform.rotation, transform);
                    Exp.transform.parent = null;
                    Exp.GetComponent<ExplodeScript>().BoostBurnDur = 0;
                    Exp.GetComponent<ExplodeScript>().BoostBurnPer = 0;
                    Exp.GetComponent<ExplodeScript>().ArmorProc = true;
                    Exp.GetComponent<ExplodeScript>().FireTrueFrostFalse = false;
                    break;

                case 5:
                    ArmorIllusion spell5 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell5.cooldown;
                    Player1.GetComponent<Player>().BlobArmorStatus(true);

                    break;

            }
            CD2 = spellSlotCD2;
            CD2_ = spellSlotCD2;
        }
    }

    public void ResetCDonDeath()
    {
        CD1 = 0;
        CD2 = 0;
        CD1_ = 1;
        CD2_ = 1;
        slot1.fillAmount = (CD1 / CD1_);
        slot2.fillAmount = (CD2 / CD2_);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            WeaponAttack();
        }


        if (CD1 <= 0f)
        {
            spellSlot1rdy = true;
            coolDownTextDisplay1.enabled = false;
        }
        else
        {
            if (CD1 > 0.1f)
            {
                coolDownTextDisplay1.enabled = true;
            }
            else
            {
                coolDownTextDisplay1.enabled = false;
            }
            CD1 -= Time.deltaTime;
            slot1.fillAmount = (CD1 / CD1_);
            coolDownTextDisplay1.text = (Mathf.Ceil(CD1).ToString)("0");
            spellSlot1rdy = false;
        }

        if (CD2 <= 0f)
        {
            spellSlot2rdy = true;
            coolDownTextDisplay2.enabled = false;
        }
        else
        {
            if (CD2 > 0.1f)
            {
                coolDownTextDisplay2.enabled = true;
            }
            else
            {
                coolDownTextDisplay2.enabled = false;
            }
            CD2 -= Time.deltaTime;
            slot2.fillAmount = (CD2 / CD2_);
            coolDownTextDisplay2.text = (Mathf.Ceil(CD2).ToString)("0");
            spellSlot2rdy = false;
        }

    }
}
