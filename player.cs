﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{

    public GameObject AudioList;
    public GameObject MousePing;
    public GameObject animChild;
    public GameObject TakeDamageText;
    private MonsterAnim anim;
    public GameObject CastSpell;
    public float MoveCD;
    private float MoveCD_;
    private GameManager gm;

    private float BurnTickRate;

    private CastSpell CS;
    private CastWeapon CW;
    //  [HideInInspector] public GameObject curSpellProjectile;
    [HideInInspector] public List<GameObject> curSpellProjectile = new List<GameObject>();
    [HideInInspector] public List<GameObject> curSpellProjectile_ = new List<GameObject>();
    public float health;
    [HideInInspector] public float fullhealth;
    [HideInInspector]
    public Vector3 targetPosition;
    private Vector3 SpellcastPosition;
    [HideInInspector]
    //GameObject trackTarget;
    private bool move;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent agent;
    private bool rightclick;
    // Hide both
    [HideInInspector]
    public float spellrange;
    private float PlayerModeArmor;
    [HideInInspector]
    public bool Immortal;
    public bool channelingNow;
    [Header("Health&Mana")]
    public Text HealthText;
    public Image HealthBar;
    [HideInInspector] public bool CantBeSlowed;
    [HideInInspector] public bool BlobWeaponEquppied;
    private GameObject activeDoor;
    private GameObject activeEvent;
    private GameObject activeToken;
    private GameObject activeShop;
    private GameObject activeChest;
    public bool DieOnce;
    public GameObject CurrentRoom;
    private bool BlobArmorBool;


    [HideInInspector] public bool attackingRightNow;
    private float attackingDuration = 0.25f;

    public float MovementSpeed, MovementSpeed_, slowedDur;
    public GameObject[] Monsters;
    [Header("Player Visuals")]
    public Text CritText;
    public GameObject CritObj;
    public GameObject HastenVisual;
    public GameObject MultiCastVisual;
    public GameObject CritVisual;
    public GameObject ChangeColor;
    public GameObject BlobArmorVisual;
    public GameObject BootsOfFire;
    public GameObject StoneArmorVisPassive;
    public GameObject StoneArmorVisActive;
    public GameObject LightningArmor;
    public GameObject IlluVis;
    public GameObject SpiderVis;

    [Header("Weapon Visuals")]

    public GameObject W1;
    public GameObject W2;
    public GameObject BigBoyGlow;
    public GameObject W4;
    public GameObject W5;
    public GameObject W6;
    public GameObject W7;
    public GameObject W8;
    public GameObject W9;
    public bool StoneArmor;
    public bool LeaveFireTrail;
    public float LeaveFireTrailCD;
    private float LeaveFireTrailCD_;

    [HideInInspector] public GameObject BlobWeaponObject;
    [Header("EnemySpellEffects")]
    public float BurnDamage, TotalBurnDamage, BurnDur;
    public GameObject FireBurn;
    public GameObject FrostSlow;
    private int ChanCount;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    private float InternalSpellCastCD_;

    private float inputX;
    private float inputY;
    private bool BlobArmorAttackOnceBool;
    public List<GameObject> SpellsCastInThisRoom = new List<GameObject>();
    private LoadScreen DeathScreen;
    private AudioScript AS;
    public AudioSource DeathSound;
    public AudioSource DeathSoundLaugh;
    public AudioSource StoneArmorBlock;
    public AudioSource TakeDamageSound;
    [HideInInspector] public int ChannelingCount;
    private bool StartAgentBool = false;
    private bool RotateAfterCasting = false;
    private float RotateAfterCastingTimer;
    private bool RRRRRRR = false;
    private float HorMov;
    private float VerMov;
    private float HorMov2;
    private float VerMov2;

    void Start()
    {
        PlayerModeArmor = MenuScript.PlayerModeArmor;
        AS = FindObjectOfType<AudioScript>();
        DeathScreen = GameObject.Find("DeathScreenTrigger").GetComponent<LoadScreen>();
        gm = GameManager.FindObjectOfType<GameManager>();
        CS = CastSpell.GetComponent<CastSpell>();
        CW = FindObjectOfType<CastWeapon>();
        anim = animChild.GetComponent<MonsterAnim>();
        CritObj.SetActive(false);
        targetPosition = transform.position;
        fullhealth = health;
        HealthText.text = health.ToString("F0");
        MovementSpeed_ = MovementSpeed;

        Immortal = false;
        Invoke("StartAgent", 0.05f);

    }

    void StartAgent()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = true;
        StartAgentBool = true;
    }

    public void RoomChangeDestroyPreviousRoomSpells()
    {
        if (ChannelingCount > 0)
        {
            foreach (var item in curSpellProjectile)
            {
                if (item != null)
                {
                    item.GetComponent<SpellProjectile>().Stop();
                    curSpellProjectile_.Add(item);
                }
            }
            foreach (var item in curSpellProjectile_)
            {
                curSpellProjectile.Remove(item);
            }
        }

        for (int i = SpellsCastInThisRoom.Count - 1; i >= 0; i--)
        {
            Destroy(SpellsCastInThisRoom[i].gameObject);
        }
        SpellsCastInThisRoom.Clear();
    }


    void Update()
    {

        if (!DieOnce && StartAgentBool)
        {
            AmIBurning();
            AmISlowed();
            agent.speed = MovementSpeed;



            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLeftKey)))
            {
                HorMov = -1;
            }else
            {
                HorMov = 0;
            }
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveRightKey)))
            {
                HorMov2 = 1;
            }
            else
            {
                HorMov2 = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveDownKey)))
            {
                VerMov = -1;
            }
            else
            {
                VerMov = 0;
            }
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveUpKey)))
            {
                VerMov2 = 1;
            }
            else
            {
                VerMov2 = 0;
            }

            inputY = VerMov + VerMov2;
            inputX = HorMov + HorMov2;


            MoveCD_ -= Time.deltaTime;

            if (inputX != 0 || inputY != 0)
            {
                Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
                targetPosition = transform.position + moveDir;
                if (BlobArmorBool)
                {
                    agent.destination = targetPosition;
                }
                move = true;
                rightclick = false;
                agent.stoppingDistance = 0f;
            }

            if (LeaveFireTrail)
            {
                LeaveFireTrailFunc();
            }


            if (ChannelingCount > 0)  //channelingNow == true)
            {
                anim.PlayerAttack();
                if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.CastSpellLoc)) && curSpellProjectile.Count > 0 && !BlobArmorBool)
                {
                    foreach (var item in curSpellProjectile)
                    {
                        if (item != null)
                        {
                            item.GetComponent<SpellProjectile>().Stop();
                            curSpellProjectile_.Add(item);
                        }
                    }
                    foreach (var item in curSpellProjectile_)
                    {
                        curSpellProjectile.Remove(item);
                    }
                }
            }
            if (((Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLoc)) && !BlobArmorBool) || ((inputX != 0 || inputY != 0) && !BlobArmorBool)) && ChannelingCount > 0 && curSpellProjectile.Count > 0)
            {
                foreach (var item in curSpellProjectile)
                {
                    if (item != null)
                    {
                        item.GetComponent<SpellProjectile>().Stop();
                        curSpellProjectile_.Add(item);
                    }
                }
                foreach (var item in curSpellProjectile_)
                {
                    curSpellProjectile.Remove(item);
                }
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLoc)) && !EventSystem.current.IsPointerOverGameObject() && (ChannelingCount == 0 || BlobArmorBool))
            {
                rightclick = false;
              //  BlobArmorAttackOnceBool = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast things, checks where mouse clicks
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 HitGroundlevel = new Vector3(hit.point.x, 1, hit.point.z);
                    float dist = Vector3.Distance(HitGroundlevel, transform.position); // distance between click point and PC
                    if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLoc)) && (hit.collider.tag == "Floor" || hit.collider.tag == "Door" || hit.collider.tag == "Wall" || hit.collider.tag == "MageBossDeadGolem" || hit.collider.tag == "Shop" || hit.collider.tag == "Chest"))
                    {
                        Vector3 DaPoint = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                        Instantiate(MousePing, DaPoint, Quaternion.Euler(0, 0, 0));
                    }

                    if (dist > 0.1f && hit.collider.tag != "ObstacleCourseClickLayer")
                    {
                        targetPosition = HitGroundlevel;
                        move = true; // when move true, character moves unless rightclick is true.
                        agent.stoppingDistance = 0f;
                        if (BlobArmorBool)
                        {
                            agent.destination = targetPosition;
                        }
                    }



                    if (hit.collider.gameObject.tag == "Token") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                    {
                        activeToken = hit.collider.gameObject;
                    }
                    else if (activeToken != null)
                    {
                        activeToken.GetComponent<TokenScript>().ClickedElsewhere();
                        activeToken = null;
                    }

                    if (hit.collider.gameObject.tag == "Door") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                    {
                        activeDoor = hit.collider.gameObject;
                    }
                    else if (activeDoor != null)
                    {
                        activeDoor.GetComponent<OneWayDoor>().ClickedElsewhere();
                        activeDoor = null;
                    }
                    if (hit.collider.gameObject.tag == "EventTag") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                    {
                        activeEvent = hit.collider.gameObject;
                    }
                    else if (activeEvent != null)
                    {
                        activeEvent.GetComponent<EventStart>().ClickedElsewhere();
                        activeEvent = null;
                    }
                    if (hit.collider.gameObject.tag == "Shop") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                    {
                        targetPosition = new Vector3(hit.collider.transform.position.x - 4, hit.collider.transform.position.y, hit.collider.transform.position.z);
                        activeShop = hit.collider.gameObject;
                    }
                    else if (activeShop != null)
                    {
                        activeShop.GetComponent<Shop>().ClickedElsewhere();
                        activeShop = null;
                    }
                    if (hit.collider.gameObject.tag == "Chest") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                    {
                        activeChest = hit.collider.gameObject;
                        targetPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z - 3);
                    }
                    else if (activeChest != null)
                    {
                        activeChest.GetComponent<AmazingChestHead>().ClickedElsewhere();
                        activeChest = null;
                    }
                }
            }
            // right click spell cast input

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.CastSpellLoc)) && !EventSystem.current.IsPointerOverGameObject())
            {
              //  BlobArmorAttackOnceBool = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    CS.spellCastLocation = hit.point;
                    SpellcastPosition = hit.point;
                    agent.stoppingDistance = 0f;
                    move = true;
                    if (!rightclick)
                    {
                        RotateAfterCastingTimer = 0.5f;
                    }

                    rightclick = true;

                }
            }

            RotateAfterCastingTimer -= Time.deltaTime;

            if (move)
            {
                if (rightclick == true || RotateAfterCastingTimer >0)
                {
                    bool asd = CS.IsCurrentSpellSlotReady();
                    if (asd || agent.velocity.magnitude == 0f || BlobArmorBool){
                        Vector3 direction = (new Vector3(SpellcastPosition.x, transform.position.y, SpellcastPosition.z) - transform.position).normalized;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 25);
                        float angle = Quaternion.Angle(transform.rotation, rotation);
                        if (ChannelingCount == 0 && angle <= 35f || BlobArmorBool)
                        {
                            RotateAfterCastingTimer = 0;
                            transform.rotation = rotation;
                            SendSpellCast();
                            rightclick = false;
                        }
                    }
                    else
                    {
                        rightclick = false;
                    }
            
                }
                if ((MoveCD_ <= 0 && !attackingRightNow && !rightclick) || BlobArmorBool)
                {
                    agent.isStopped = false;
                    if (Vector3.Distance(agent.destination, targetPosition) > 1 || (inputX != 0 || inputY != 0))
                    {
                        agent.destination = targetPosition;
                    }
                    float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
                    float velocity = agent.velocity.magnitude / agent.speed;
                    if ((distanceToTarget > 0.75f || velocity > 0.9f) && !attackingRightNow)
                    {
                        anim.PlayerMove();
                    }
                }
                else
                {
                    agent.isStopped = true;
                }

            }
            else if (!attackingRightNow)
            {
                PlayerIsIdle();
            }

            if (attackingRightNow)
            {
                if (attackingDuration <= 0f)
                {
                    if (ChannelingCount == 0)
                    {
                        attackingRightNow = false;
                    }
                    attackingDuration = 0.25f;
                }
                attackingDuration -= Time.deltaTime;
            }
            InternalSpellCastCD_ -= Time.deltaTime;
            CheckDestinationReached();
        }
        else
        {
            DieSoundOff(Time.deltaTime * 10);
        }
        if (Immortal)
        {
            DieSoundOff(Time.deltaTime * 4);
        }
    }

    protected void LateUpdate()
    {
        AudioList.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator TeleportPlayerToStartAreaOnTrailBosses(float delay, Vector3 Loc, GameObject BossRoom)
    {
        yield return new WaitForSeconds(delay);

        ParticleSystem TelePortEffect = CastSpell.GetComponent<CastWeapon>().TelePortEffect;
        ParticleSystem Tele1Effect = Instantiate(TelePortEffect, transform.position, transform.rotation);
        Tele1Effect.transform.parent = transform;
        Loc += new Vector3(BossRoom.transform.position.x, BossRoom.transform.position.y, BossRoom.transform.position.z-5f);
        ParticleSystem Tele1Effect2 = Instantiate(TelePortEffect, Loc, transform.rotation);
        Destroy(Tele1Effect.transform.gameObject, 3.1f);
        Destroy(Tele1Effect2.transform.gameObject, 3.1f);
        StartCoroutine(TelePortPlayer(1, Tele1Effect, Loc));
    }

    IEnumerator TelePortPlayer(float delay, ParticleSystem Tele1Effect, Vector3 TeleLoc)
    {
        yield return new WaitForSeconds(delay);
        Tele1Effect.transform.parent = null;
        agent.Warp(TeleLoc);
        targetPosition = transform.position;
        agent.destination = transform.position;
    }

    private void SpellCastLocationAgentLocation()
    {
        agent.destination = this.transform.position;
        targetPosition = agent.destination;
    }

    public void BlobArmorStatus(bool status)
    {
        BlobArmorBool = status;
    }


    public void PlayerIsIdle()
    {
        anim.PlayerIdle();
    }

    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        if (distanceToTarget < 0.75f)
        {
            move = false;
        }
    }

    void IlluArmor()
    {
        bool trigger = false;
        if (CastSpell.GetComponent<CastWeapon>().spellSlot2rdy == true && CastSpell.GetComponent<CastWeapon>().CurrentArmor == 2)
        {
            Monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in Monsters)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) < 7.5f)
                {
                    trigger = true;
                }
            }
            if (trigger)
            {
                CastSpell.GetComponent<CastWeapon>().ArmorTrigger();
            }
        }
    }

    public void Channeling()
    {
        ChannelingCount++;
    }
    public void StopChanneling()
    {
        ChannelingCount--;
    }

    public void TakeDamage(float damage)
    {
        if (!Immortal)
        {
            bool DodgeDamage = false;
            if (StoneArmor)
            {
                var randomInt = Random.Range(0, 100);
                if (randomInt <= 30)
                {
                    DodgeDamage = true;

                    if (damage > 0.2f)
                    {
                        StoneArmorBlock.Play();
                        GameObject StoneSparks = Instantiate(StoneArmorVisActive, transform);
                        StoneSparks.transform.parent = null;
                        Destroy(StoneSparks, 0.6f);
                    }
                }
            }

            if (CastSpell.GetComponent<CastWeapon>().spellSlot2rdy == true && CastSpell.GetComponent<CastWeapon>().CurrentArmor == 1)
            {
                CastSpell.GetComponent<CastWeapon>().ArmorTrigger(); //currentarmor instead of 0.. TODO
                DodgeDamage = true;
            }

            if (!DodgeDamage)
            {           
                health -= damage * PlayerModeArmor;




                if (DieOnce == false)
                {
                    gm.DamageReceived += damage * PlayerModeArmor;
                    HealthText.text = health.ToString("F1");
                    HealthBar.fillAmount = health / fullhealth;

                    if ((damage) >= 0.2f)
                    {
                        GameObject DmgTxt = Instantiate(TakeDamageText, transform);
                        DmgTxt.GetComponent<DamageVisualText>().damageAmount = damage* PlayerModeArmor;
                    }

                }
                if (health <= 0 && DieOnce == false)
                {
                    DieOnce = true;
                    HealthText.text = "0";
                    Die();
                }

                if (CastSpell.GetComponent<CastWeapon>().spellSlot2rdy == true && CastSpell.GetComponent<CastWeapon>().CurrentArmor == 4)
                {
                    CastSpell.GetComponent<CastWeapon>().ArmorTrigger();
                }
            }
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health >= fullhealth)
        {
            health = fullhealth;
            HealthText.text = health.ToString("F0");
        }
        else
        {
            HealthText.text = health.ToString("F1");
        }

        HealthBar.fillAmount = health / fullhealth;
    }

    void LeaveFireTrailFunc()
    {
        if (agent.velocity.magnitude > 0f && LeaveFireTrailCD_ < 0)
        {
            GameObject FireT = Instantiate(BootsOfFire);
            FireT.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            FireT.transform.parent = null;
            FireT.transform.rotation = BootsOfFire.transform.rotation;
            LeaveFireTrailCD_ = LeaveFireTrailCD;
            SpellsCastInThisRoom.Add(FireT);

        }
        else if (agent.velocity.magnitude == 0f && LeaveFireTrailCD_ < 0)
        {
            GameObject FireT = Instantiate(BootsOfFire);
            FireT.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            FireT.transform.parent = null;
            FireT.transform.rotation = BootsOfFire.transform.rotation;
            LeaveFireTrailCD_ = 3;
            SpellsCastInThisRoom.Add(FireT);
        }


        if (LeaveFireTrailCD_ > LeaveFireTrailCD && agent.velocity.magnitude > 0f)
        {
            LeaveFireTrailCD_ = 0f;
        }
        LeaveFireTrailCD_ -= Time.deltaTime;

    }





    public void Crit(float damage)
    {
        CritObj.SetActive(true);
        CritText.text = damage.ToString("F1");
        Invoke("CritHide", 1f);
    }

    public void CritHide()
    {
        CritObj.SetActive(false);
    }

    public void HastenVis()
    {
        GameObject Haste = Instantiate(HastenVisual, transform);
        Destroy(Haste, 2.5f);
    }
    public void ResetCDVis(GameObject Vis)
    {
        GameObject Hasten = Instantiate(Vis, transform);
        Destroy(Hasten, 2.5f);
    }

    public void MultiVis(float Position)
    {
        GameObject Multi = Instantiate(MultiCastVisual, transform);
        Multi.transform.localPosition = new Vector3(Position, 4, 0);
        Destroy(Multi, 0.8f);
    }
    public void CritVis()
    {
        GameObject Crit = Instantiate(CritVisual, transform);
        Crit.transform.localPosition = new Vector3(0, 5, 0);
        Destroy(Crit, 0.8f);
    }

    public void SendSpellCast()
    {
        //if (!BlobArmorAttackOnceBool)
        //{
            if (InternalSpellCastCD_ < 0)
            {
                CS.CastCurrentSpell();
                InternalSpellCastCD_ = 0.2f;

            }
         //   if (BlobArmorBool)
        //    {
        //        BlobArmorAttackOnceBool = true;
        //    }
        //}
    }

    public void StopMoveForSpellCast()
    {
        if (!BlobArmorBool)
        { 
            MoveCD_ = 0.2f;
        SpellCastLocationAgentLocation();

            move = false;
        }

    }



    public void CastExtraBlob(Quaternion rotation)
    {
        GameObject Blob = Instantiate(BlobWeaponObject, new Vector3(transform.position.x, 3, transform.position.z), rotation, transform);
        SpellsCastInThisRoom.Add(Blob);
        Blob.tag = "Untagged";
        Blob.transform.position += transform.forward * 1.5f;
        Blob.transform.parent = null;
        Monster B = Blob.GetComponent<Monster>();
        B.health = 2;
        B.MovementSpeed = 5;
        B.BlobAttackNoTarget = true;
        B.BlobWeapon = true;
        B.BlobDieTimer = 15;
        B.BlobDie = true;
        B.Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Die()
    {
        Invoke("HCDeathAnimDueToFireDamageBug", 0.05f);
        gm.DeathCount++;
        gm.SetDeathText();
        agent.destination = transform.position;
        DeathScreen.FadeToDeath();
        DeathSound.PlayDelayed(1);
        DeathSoundLaugh.PlayDelayed(3);
    }

    void HCDeathAnimDueToFireDamageBug()
    {
        anim.PlayerDie();
    }

    public void Continue()
    {
        agent.Warp(new Vector3(0, 0, 0));
        BurnDur = 0;
        slowedDur = 0;
        health = 10;
        HealthText.text = health.ToString("F0");
        HealthBar.fillAmount = health / fullhealth;
        DieOnce = false;
        DeathScreen.FadeToLife();
        AS.ResetVolumeAfterDeath();
        RoomChangeDestroyPreviousRoomSpells();
        CW.ResetCDonDeath();
        CS.ResetSpellCDOnDeath();
    }

    public void DieSoundOff(float soundLevel)
    {
        AS.DeathMusicSoundLevel -= soundLevel;
        AS.DeathSFXSoundLevel -= soundLevel;
        AS.masterMixer.SetFloat("Music Volume", AS.DeathMusicSoundLevel);
        AS.masterMixer.SetFloat("SFX Volume", AS.DeathSFXSoundLevel);
    }

    public void TimeStaffCDS()
    {
        CS.ResetSpellCDOnDeath();
    }

    public void AttackAnim()
    {
        anim.PlayerAttack();
        attackingRightNow = true;
    }

    public void Burn(bool burn, float dur, float str, float dmg)
    {
        if (burn && !StoneArmor)
        {
            BurnDamage += dmg;
            TotalBurnDamage = BurnDamage * str;
            BurnDur = dur;

            Transform result = gameObject.transform.Find("burn");
            if (!result)
            {
                GameObject burneffect = Instantiate(FireBurn, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation, transform);
                burneffect.name = "burn";
            }
        }
    }

    public void AmIBurning()
    {
        if (BurnDur <= 0 || StoneArmor)
        {
            BurnDamage = 0;
            BurnTickRate = 0;
            Transform result = gameObject.transform.Find("burn");
            if (result)
            {
                Destroy(transform.Find("burn").gameObject);
            }
        }
        else
        {
            BurnDur -= Time.deltaTime;


            if (BurnTickRate < 0)
            {
                TakeDamage(TotalBurnDamage*0.2f);
                if (BurnDur > 0.2f || BurnDur < 0.01f)
                {
                    BurnTickRate = 0.2f;
                }
                else 
                {
                    BurnTickRate = BurnDur - 0.01f;
                }
            }

            BurnTickRate -= Time.deltaTime;
        }
    }

    public void Slow(bool slow, float dur, float str) //Could slow attackspeed aswell, though might take a bit of work to slow down animations aswell.
    {
        if (slow && MovementSpeed >= MovementSpeed_ && !CantBeSlowed) //slow is true and currently not slowed. How to work if enemy has a sprint effect? to be seen.
        {
            MovementSpeed /= str;
            agent.speed = MovementSpeed;


            GameObject sloweffect = Instantiate(FrostSlow, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation, transform);
            sloweffect.name = "slow";
        }
        if (slow)
        {
            slowedDur = dur;
        }
    }

    public void AmISlowed()
    {
        if (slowedDur <= 0)
        {
            MovementSpeed = MovementSpeed_;
            agent.speed = MovementSpeed;
            Transform result = gameObject.transform.Find("slow");
            if (result)
            {
                Destroy(transform.Find("slow").gameObject);
            }
        }
        else
        {
            slowedDur -= Time.deltaTime;
        }
    }

    void Armor1()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentArmor == 1 && CastSpell.GetComponent<CastWeapon>().spellSlot2rdy)
        {
            SpiderVis.SetActive(true);
        }
        else
        {
            SpiderVis.SetActive(false);
        }
    }
    void Armor2()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentArmor == 2 && CastSpell.GetComponent<CastWeapon>().spellSlot2rdy)
        {
            IlluVis.SetActive(true);
        }
        else
        {
            IlluVis.SetActive(false);
        }
    }

    void Weapon1()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 1 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W1.SetActive(true);
        }
        else
        {
            W1.SetActive(false);
        }
    }
    void Weapon2()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 2 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W2.SetActive(true);
        }
        else
        {
            W2.SetActive(false);
        }
    }
    void Weapon3()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 3 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            BigBoyGlow.SetActive(true);
        }
        else
        {
            BigBoyGlow.SetActive(false);
        }
    }
    void Weapon4()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 4 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W4.SetActive(true);
        }
        else
        {
            W4.SetActive(false);
        }
    }
    void Weapon6()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 6 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W6.SetActive(true);
        }
        else
        {
            W6.SetActive(false);
        }
    }
    void Weapon7()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 7 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W7.SetActive(true);
        }
        else
        {
            W7.SetActive(false);
        }
    }
    void Weapon8()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 8 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W8.SetActive(true);
        }
        else
        {
            W8.SetActive(false);
        }
    }
    void Weapon9()
    {
        if (CastSpell.GetComponent<CastWeapon>().CurrentWeapon == 9 && CastSpell.GetComponent<CastWeapon>().spellSlot1rdy)
        {
            W9.SetActive(true);
        }
        else
        {
            W9.SetActive(false);
        }
    }



}
