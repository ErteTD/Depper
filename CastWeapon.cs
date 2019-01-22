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
    [HideInInspector] public bool spellSlot1rdy;
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


        if (ID == 5)
        {
            WeaponAttack();
            if (!spellSlot1rdy)
            {
                Invoke("WeaponAttack", CD1 + 0.1f);
            }
        }

        if (ID == 1)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon1", 0.1f, 0.1f);
        }
        if (ID == 2)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon2", 0.1f, 0.1f);
        }
        if (ID == 3)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon3", 0.1f, 0.1f);
        }
        if (ID == 4)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon4", 0.1f, 0.1f);
        }
        if (ID == 6)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon6", 0.1f, 0.1f);
        }
        if (ID == 7)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon7", 0.1f, 0.1f);
        }
        if (ID == 8)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon8", 0.1f, 0.1f);
        }
        if (ID == 9)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Weapon9", 0.1f, 0.1f);
        }


        if (ID != 1)
        {
            Player1.GetComponent<Player>().W1.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon1");
        }
        if (ID != 2)
        {
            Player1.GetComponent<Player>().W2.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon2");
        }
        if (ID != 3)
        {
            Player1.GetComponent<Player>().BigBoyGlow.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon3");
        }
        if (ID != 4)
        {
            Player1.GetComponent<Player>().W4.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon4");
        }
        if (ID != 6)
        {
            Player1.GetComponent<Player>().W6.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon6");
        }
        if (ID != 7)
        {
            Player1.GetComponent<Player>().W7.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon7");
        }
        if (ID != 8)
        {
            Player1.GetComponent<Player>().W8.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon8");
        }
        if (ID != 9)
        {
            Player1.GetComponent<Player>().W9.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Weapon9");
        }

        if (ID != 5 && Player1.GetComponent<Player>().BlobWeaponEquppied == true)
        {
            Player1.GetComponent<Player>().BlobWeaponEquppied = false;
            Player1.GetComponent<Player>().W5.SetActive(false);
        }
    }
    public void SelectArmor(int ID)
    {
        CurrentArmor = ID;
        tooltip.OpenArmorSelect();
        if (ID == 3 || ID == 5 || ID == 6 || ID == 7 || ID == 8 || ID == 9)
        {
            ArmorTrigger();
            if (!spellSlot2rdy)
            {
                Invoke("ArmorTrigger", CD2 + 0.1f);
            }
        }

        if (ID == 1)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Armor1", 0.1f, 0.1f);
        }
        if (ID == 2)
        {
            Player1.GetComponent<Player>().InvokeRepeating("Armor2", 0.1f, 0.1f);
        }


        if (ID != 1)
        {
            Player1.GetComponent<Player>().SpiderVis.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Armor1");
        }
        if (ID != 2)
        {
            Player1.GetComponent<Player>().IlluVis.SetActive(false);
            Player1.GetComponent<Player>().CancelInvoke("Armor2");
        }

        if ( ID != 3 && Player1.GetComponent<Player>().ChangeColor.GetComponent<Renderer>().material.color == Color.red)
        {
            Player1.GetComponent<Player>().ChangeColor.GetComponent<Renderer>().material.color = Color.white;
            Player1.GetComponent<Player>().MovementSpeed = 9;
            Player1.GetComponent<Player>().MovementSpeed_ = 9;
            Player1.GetComponent<Player>().agent.speed = 9;
            Player1.GetComponent<Player>().CantBeSlowed = false;
        }

        if (ID != 4 && Player1.GetComponent<Player>().fullhealth == 15) // armor 4 non frost explosion stuff.
        {
            Player1.GetComponent<Player>().fullhealth = 10;
            if (Player1.GetComponent<Player>().health > 10)
            {
                Player1.GetComponent<Player>().health = 10;
                Player1.GetComponent<Player>().HealthText.text = Player1.GetComponent<Player>().health.ToString("F0");
            }
            else
            {
                Player1.GetComponent<Player>().HealthText.text = Player1.GetComponent<Player>().health.ToString("F1");
            }
            Player1.GetComponent<Player>().HealthBar.fillAmount = Player1.GetComponent<Player>().health / Player1.GetComponent<Player>().fullhealth;

        }
        else if (ID == 4)
        {
            Player1.GetComponent<Player>().fullhealth = 15;
            Player1.GetComponent<Player>().HealthText.text = Player1.GetComponent<Player>().health.ToString("F1");
            Player1.GetComponent<Player>().HealthBar.fillAmount = Player1.GetComponent<Player>().health / Player1.GetComponent<Player>().fullhealth;
        }

        if (ID != 5 && Player1.GetComponent<Player>().BlobArmorVisual.activeSelf == true)
        {
            Player1.GetComponent<Player>().BlobArmorVisual.SetActive(false);
            Player1.GetComponent<Player>().BlobArmorStatus(false);
        }

        if ((ID != 6 && ID != 7) && Player1.GetComponent<Player>().LeaveFireTrail == true)
        {
            Player1.GetComponent<Player>().LeaveFireTrail = false;
        }

        if (ID != 8 && Player1.GetComponent<Player>().StoneArmor == true)
        {
            Player1.GetComponent<Player>().StoneArmor = false;
            Player1.GetComponent<Player>().StoneArmorVisPassive.SetActive(false);  
        }

        if (ID != 9 && Player1.GetComponent<Player>().LightningArmor.activeSelf == true)
        {
            Player1.GetComponent<Player>().LightningArmor.SetActive(false);
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
                    Player1.GetComponent<Player>().W5.SetActive(true);
                    Player1.GetComponent<Player>().BlobWeaponObject = spell5.ItemObject;
                    spellSlotCD = spell5.cooldown;
                    break;

                case 6:
                    Blink spell6 = Weapons[CurrentWeapon].GetComponent<Blink>();
                    Player1.GetComponent<Player>().TimeStaffCDS();
                    Player1.GetComponent<Player>().ResetCDVis(spell6.ItemObject);
                    spellSlotCD = spell6.cooldown;
                    break;
                case 7:
                    Blink spell7 = Weapons[CurrentWeapon].GetComponent<Blink>();
                    Collider[] cols = Physics.OverlapSphere(transform.position, 35);
                    foreach (Collider c in cols)
                    {
                        Monster e = c.GetComponent<Monster>();
                        if (e != null && e.tag == "Monster")
                        {
                            //Monster enemy = e.GetComponent<Monster>();
                            GameObject test123 = Instantiate(spell7.ItemObject, transform.position, transform.rotation, transform);
                            SpellProjectile Frostmeteor = test123.GetComponent<SpellProjectile>();
                            Frostmeteor.projectilespeed = 20;
                            Frostmeteor.damage = 1;
                            Frostmeteor.FrostBoltSlow = true;
                            Frostmeteor.SlowDuration = 3f;
                            Frostmeteor.SlowPercent = 1.4f;
                            Frostmeteor.aoeSizeMeteor = 3f;
                            Frostmeteor.FrostStaff = true;
                            Frostmeteor.spellCastLocation = e.transform.position;
                            Frostmeteor.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
                            Player1.GetComponent<Player>().SpellsCastInThisRoom.Add(test123);
                        }
                    }
                    spellSlotCD = spell7.cooldown;
                    break;
                case 8:
                    Blink spell8 = Weapons[CurrentWeapon].GetComponent<Blink>();

                    GameObject test1234 = Instantiate(spell8.ItemObject, transform.position, transform.rotation, transform);

                    test1234.transform.parent = null;
                    Destroy(test1234, 1f);

                    Collider[] cols2 = Physics.OverlapSphere(transform.position, 10);
                    foreach (Collider c in cols2)
                    {
                        Monster e = c.GetComponent<Monster>();
                        if (e != null && e.tag == "Monster")
                        {
                            Monster enemy = e.GetComponent<Monster>();
                            enemy.Slow(true, 2, 1.25f);
                            Vector3 directionF = (enemy.transform.position - transform.position).normalized;
                            enemy.CancelInvoke("StopPush");
                            enemy.TakeDamage(1);
                            enemy.pushDir = directionF;
                            enemy.pushed = true;
                        }
                    }
                    spellSlotCD = spell8.cooldown;
                    break;
                case 9:
                   // Blink spell9 = Weapons[CurrentWeapon].GetComponent<Blink>();

                    Spellbook SB_ = FindObjectOfType<Spellbook>();
                    CastSpell CS_ = FindObjectOfType<CastSpell>();

                    int curS = CastSpell.FindObjectOfType<CastSpell>().currentSlot;
                    CS_.MadWeapon = true;
                    SB_.LeveloneSpell(Random.Range(1, 4));
                    SB_.LeveltwoSpell(Random.Range(0, 4));
                    SB_.LevelthreeSpell(Random.Range(0, 5));
                    SB_.LevelfourSpell(Random.Range(0, 4));
                    SB_.LevelfiveSpell(Random.Range(0, 4));
                    SB_.LevelsixSpell(Random.Range(0, 6));
                    Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit3;
                    if (Physics.Raycast(ray3, out hit3))
                    {
                        CS_.spellCastLocation = hit3.point;
                        Vector3 targetPosition = hit3.point;
                        Vector3 direction = (targetPosition - Player1.transform.position).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3             
                        Player1.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 999f);
                    }
                    CS_.CastCurrentSpell();
                    CS_.MadWeapon = false;
                    
                    switch (curS)
                    {
                        case 1:
                            SB_.SlotOne();
                            break;
                        case 2:
                            SB_.SlotTwo();
                            break;
                        case 3:
                            SB_.SlotThree();
                            break;
                    }

                   // spellSlotCD = spell9.cooldown;
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
                    Player1.GetComponent<Player>().SpiderVis.SetActive(false);
                    spider1.transform.parent = null;

                    break;
                case 2:
                    ArmorIllusion spell2 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell2.cooldown;
                    GameObject Illu = Instantiate(spell2.ItemObject, transform.position, transform.rotation, transform);
                    Illu.transform.parent = null;
                    Player1.GetComponent<Player>().IlluVis.SetActive(false);
                    Illu.transform.localPosition = new Vector3(Illu.transform.position.x, 2, Illu.transform.position.z);
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
                    Player1.GetComponent<Player>().BlobArmorVisual.SetActive(true);
                    break;
                case 6:
                    ArmorIllusion spell6 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell6.cooldown;
                    Player1.GetComponent<Player>().BootsOfFire = spell6.ItemObject;
                    Player1.GetComponent<Player>().LeaveFireTrailCD = 0.2f;
                    Player1.GetComponent<Player>().LeaveFireTrail = true;
                    break;
                case 7:
                    ArmorIllusion spell7 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell7.cooldown;
                    Player1.GetComponent<Player>().BootsOfFire = spell7.ItemObject;
                    Player1.GetComponent<Player>().LeaveFireTrailCD = 0.3f;
                    Player1.GetComponent<Player>().LeaveFireTrail = true;
                    break;
                case 8:
                    ArmorIllusion spell8 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell8.cooldown;
                    Player1.GetComponent<Player>().StoneArmor = true;
                    Player1.GetComponent<Player>().StoneArmorVisPassive.SetActive(true);
                    break;
                case 9:
                    ArmorIllusion spell9 = Armor[CurrentArmor].GetComponent<ArmorIllusion>();
                    spellSlotCD2 = spell9.cooldown;
                    Player1.GetComponent<Player>().LightningArmor.SetActive(true);
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
