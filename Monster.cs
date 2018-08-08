using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Monster : MonoBehaviour
{

    public float health;
    public float damage;
    public float meleeRange;
    private float meleeRange_;
    public float AttackSpeed;
    public float CasterVariationAS;
    private float AttackSpeedRat;
    private float CasterVariationASRat;
    public float MovementSpeed;
    private float MovementSpeed_;
    private float slowedDur;
    public float AggroRange;
    [HideInInspector] public float AggroRange_;
    public float turnRate;
    public float AttackDelay = 0.4f;
    private float AttackDelay_;
    private bool InCombat;
    [HideInInspector] public bool AttackFriend;

    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    private GameObject PC;
    private GameObject PC_;
    private GameObject Illu;
    [HideInInspector] public Vector3 startPosition;
    private bool canAttack;
    private float attackCountdown;

    private float callForHelpCD;

    private float hardCodeDansGame = 0.833f;
    public float attackAnimCD; // how prevents other animations from overriding attack animation.

    public Image Healthbar;
    [HideInInspector] public float health2;

    public GameObject animChild;
    public int MonsterType;
    public int MonsterTypeSubLayer;
    public float PushResistance;

    private float BurnDamage;
    private float BurnDur;
    private float TotalBurnDamage;
    private bool noBounce;
    private float ChannelTimer = 0.5f;
    public float BounceDistance = 1.5f;
    private bool CheckIfBurnBoosted;
    private bool CheckIfFrostBoosted;
    private bool OnlyOnce = false;
    private bool CurrentlyFrozen;

    private float BoostBurnDamage;
    private float BoostBurnDur;
    private float BoostBurnPer;
    private float BoostSlowPer;
    private float BoostSlowDur;

    [HideInInspector] public bool pushed;
    [HideInInspector] public Vector3 pushDir;
    [HideInInspector] public Vector3 BaseVel;
    private float CurrentSlowSTR;
    [HideInInspector] public bool Type5BHSLOW;
    [HideInInspector] public List<string> chainList = new List<string>();
    [Header("Challenge Raiting")]
    public float MonsterCR;
    public bool SpawnMultiBool;
    public int SpawnMultiNumber;
    public int IntendedLevel;

    [Header("Boss Weapon")]
    public GameObject BossWeapon;
    [Header("Boss Armor")]
    public GameObject BossArmor;

    [Header("Monster Type 2 -Cast Frostbost")]
    public GameObject currentspellObject;
    public GameObject currentspellObject2;
    public GameObject currentspellObject3;
    public GameObject currentspellObject4;
    public GameObject currentSpell;
    public GameObject currentSpell2;
    public GameObject currentSpell3;
    public GameObject currentSpell4;
    public GameObject castPoint;

    [Header("Special abilities")]
    public bool Resurrect;
    public float ResTimer;
    private bool CurrentlyRessing;
    private bool RegenLife;

    [Header("VisualEffects")]
    public GameObject FrostSlow;
    public GameObject FireBurn;
    public GameObject CorpseExplosion;
    public GameObject FrostExplosion;
    public GameObject Frozen;
    public GameObject SpiderPoison;
    //public GameObject BlackHole;

    [Header("Bosses")]
    public string BossName;
    public bool Boss;
    public bool SpiderBoss;
    public bool BigBoy;
    public bool TimeKeeper;
    public bool OldKing;
    public bool TheBlob;
    public Vector3 LootLoc;
    public GameObject BossHealthAct;
    //Spidy
    [Header("Spider")]
    public GameObject SBAttack;
    public bool Swarm;
    public GameObject SpiderSwarm;
    public float SwarmCD;
    private float SwarmCD_;
    [Header("TimeKeeper")]
    //TimeKeperstuff.
    public List<GameObject> TimeKeeperPoints;
    public float MirrorImageCD;
    private float MirrorImageCD_;
    public float Ratatatata;
    private float Ratatatata_;
    private bool TimeKSpin;
    private int TeleLoc;
    public bool IamIllu;
    private bool IlluHit;
    public ParticleSystem OuterRing;

    [Header("BigBoy")]
    public GameObject BigBoyGlow;
    public GameObject BigBoyGlow2;
    public GameObject BigBoyGlow3;
    public GameObject BigBoyGlow4;
    public GameObject BigBoyGlow5;
    public GameObject BigBoyGlow5b;
    public GameObject Smallboy;
    public bool SummonHelp;
    private GameObject Brother;
    public float HelpHP;
    public float BigBoySpecial1;
    private float BigBoySpecial1_;
    private int BigBoyCurrentAttack;
    public GameObject FireAttack;
    public GameObject FrostAttack;
    private Vector3 HelpPos;
    private Quaternion HelpRot;
    private Transform CurDir;
    private int FireNumber;
    private int FrostNumber;
    private bool ChangeColor;
    private bool TKSpawning;
    public Color colorIni = Color.white;
    public Color colorFin = Color.red;
    public float duration = 3f;
    Color lerpedColor;
    public GameObject BigBoyColor;
    Renderer _renderer;
    private float t = 0;
    private bool flag;
    private bool BBStill;

    [Header("Old King")]
    public GameObject Skill1Projectile;
    public GameObject Skill2Projectile;
    public GameObject Skill2Defile;
    public GameObject Skill3Projectile;
    public bool StartDead;
    public float OldKingSpecialAttack;
    private float OldKingSpecialAttack_1;
    private int OldKingCurrentAttack;
    public bool Immortal;
    private bool RealDeath;
    public bool StartScene;
    public float StartSceneAS;
    public GameObject StartOpponent;
    public Monster SkeletonKing;
    public bool Defiling;
    public GameObject CurDefile;
    public List<GameObject> SkeletonList = new List<GameObject>();
    public List<GameObject> SkeletonListAlive = new List<GameObject>();

    [Header("The Blob")]
    public GameObject BlobAttack1Object;
    public float BlobAttackCD;
    public float BlobAttackCD_;
    public float BlobAttack1Duration;
    public bool BlobAttackNoTarget;
    public Light BossLight;
    public ParticleSystem BlobPS;
    public ParticleSystem BlobPS2;
    private float timeLeft;
    private Color targetColor;
    private int HealTimeCounter;
    [HideInInspector] public float Blob4RotSpeed;
    [HideInInspector] public bool Healboss;
    public List<GameObject> BlobCornerList;
    [HideInInspector] public bool BlobAttack4;
    [HideInInspector] public bool BlobDie;
    [HideInInspector] public float BlobDieTimer;


    void Start()
    {
        targetColor = Color.green;
        AttackDelay_ = AttackDelay;
        HelpPos = transform.position;
        HelpRot = transform.rotation;
        AttackSpeedRat = AttackSpeed;
        CasterVariationASRat = CasterVariationAS;
        AggroRange_ = AggroRange;
        MovementSpeed_ = MovementSpeed;
        startPosition = transform.position;
        PC = GameObject.Find("Player");
        PC_ = PC;
        SwarmCD_ = SwarmCD / 2;
        meleeRange_ = meleeRange;
        MirrorImageCD_ = MirrorImageCD;
        Ratatatata_ = Ratatatata;
        BlobAttackCD_ = BlobAttackCD;

        if (BigBoyColor != null)
        {
            lerpedColor = colorIni;
            _renderer = BigBoyColor.GetComponent<Renderer>();
        }
        if (MonsterType != 5)
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.angularSpeed = turnRate;
            agent.speed = MovementSpeed;
            agent.stoppingDistance = meleeRange;
        }
        if (!SpiderBoss)
        {
            attackCountdown = AttackSpeed;
            if (StartScene)
            {
                attackCountdown = StartSceneAS;

            }
        }
        if (!IamIllu)
        {
            health2 = health;
        }
        if (OldKing)
        {
            OldKingSpecialAttack_1 = OldKingSpecialAttack;
            health -= 300;
            Healthbar.fillAmount = health / health2;

            StartOpponent.GetComponent<Monster>().health -= 11;
            StartOpponent.GetComponent<Monster>().Healthbar.fillAmount = health / health2;
        }


        if (Boss) //Boss health bar
        {
            BossHealthAct = GameObject.Find("BossHealthActive");
            BossHealthAct.transform.GetChild(0).gameObject.SetActive(true);
            BossHealthAct.transform.GetChild(1).gameObject.SetActive(true);
            BossHealthAct.transform.GetChild(2).gameObject.SetActive(true);
            BossHealthAct.transform.GetChild(3).gameObject.SetActive(true);
            BossHealthAct.transform.GetChild(2).gameObject.GetComponent<Text>().text = BossName;
            BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health + " / " + health2;
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (TimeKeeper && !IamIllu)
        {
            ChangeColor = true;
            agent.isStopped = true;
            AggroRange = 999f;
            attackCountdown = 9;
            tag = "Illusion";
            Invoke("TKFight", 8.5f);
            Invoke("TimeKeeperAlive", 2f);
            animChild.GetComponent<MonsterAnim>().SpawnTK(-0.01f);
            MirrorImageCD_ += 6;
            Ratatatata_ += 6;
            TKSpawning = true;
        }

        if (BigBoy) // Starting anim for BigBoyBoss.
        {
            animChild.GetComponent<MonsterAnim>().Spawn();

            AggroRange = 40f;
            Invoke("BigBoyAggro", 3.6f);
            BigBoySpecial1_ = BigBoySpecial1;
            agent.isStopped = true;
            BBStill = true;
        }

        if (StartDead)
        {
            SkeletonStartDead();
        }

        if (BlobDie)
        {
            Invoke("BlobExpire", BlobDieTimer);
        }

    }

    void ChangeColorBlob()
    {
        if (timeLeft <= 0)
        {

            BossLight.color = targetColor;

            var RandColor = Random.Range(0, 6);
            switch (RandColor)
            {
                case 0:
                    targetColor = new Color(1, Random.value, 0, 1);
                    break;
                case 1:
                    targetColor = new Color(1, 0, Random.value, 1);
                    break;
                case 2:
                    targetColor = new Color(0, 1, Random.value, 1);
                    break;
                case 3:
                    targetColor = new Color(0, Random.value, 1, 1);
                    break;
                case 4:
                    targetColor = new Color(Random.value, 0, 1, 1);
                    break;
                case 5:
                    targetColor = new Color(Random.value, 1, 0, 1);
                    break;
            }
            timeLeft = 2.5f;
        }
        else
        {
            BossLight.color = Color.Lerp(BossLight.color, targetColor, Time.deltaTime);
            timeLeft -= Time.deltaTime;
        }


    }

    void TheBlobAttack()
    {
        BlobAttackCD_ -= Time.deltaTime;



        if (BlobAttackCD_ < 0)
        {
            if (HealTimeCounter < 7)
            {
                var RandAttack = Random.Range(0, 5);
                switch (RandAttack)
                {
                    case 0:
                        BlobOne();
                        break;
                    case 1:
                        BlobTwo();
                        break;
                    case 2:
                        BlobThree();
                        break;
                    case 3:
                        BlobFour();
                        break;
                    case 4:
                        BlobFive();
                        break;
                }
                HealTimeCounter++;
                BlobAttackCD_ = BlobAttackCD;
            }
            else
            {
                BlobHeal();
                HealTimeCounter = 0;
                BlobAttackCD_ = BlobAttackCD * 1.5f;
            }


        }
    }

    void BlobHeal()
    {
        float AttackDelay_ = 0;
        for (int i = 0; i < 20; i++)
        {
            foreach (var BlobSpawn in BlobCornerList)
            {
                Vector3 StartLoc = BlobSpawn.transform.position;
                StartCoroutine(Blob1(gameObject, true, 0, StartLoc, 0, 2f, 5, 5f, AttackDelay_));
            }
            AttackDelay_ += 0.5f;
        }
    }

    void BlobOne()
    {
        float AttackDelay_ = 0;
        int BlobCounter = 0;
        int BlobDirMod = 0;
        for (int i = 0; i < 70; i++)
        {
            BlobCounter++;
            if (BlobCounter == 1)
            {
                BlobDirMod = 0;
            }
            if (BlobCounter == 2)
            {
                BlobDirMod = 30;
            }
            if (BlobCounter == 3)
            {
                BlobDirMod = -30;
                BlobCounter = 0;
            }
            StartCoroutine(Blob1(gameObject, false, 1, new Vector3(0, 0, 0), BlobDirMod, 2, 15, 4f, AttackDelay_));
            AttackDelay_ += 0.1f;
        }
    }

    void BlobTwo()
    {
        float AttackDelay_ = 0;
        transform.LookAt(PC.transform.position);
        Vector3 RotateDir = transform.rotation.eulerAngles;
        float yAxis = RotateDir.y;

        for (int i = 0; i < 100; i++)
        {
            RotateDir = new Vector3(0, yAxis, 0);

            yAxis += 11;

            StartCoroutine(Blob1(gameObject, false, 2, RotateDir, 0, 2, 15, 4, AttackDelay_));
            AttackDelay_ += 0.07f;
        }
    }

    void BlobThree()
    {
        float AttackDelay_ = 0;
        Vector3 RotateDir;
        for (int i = 0; i < 70; i++)
        {
            var yAxis = Random.Range(0, 360);
            RotateDir = new Vector3(0, yAxis, 0);

            StartCoroutine(Blob1(gameObject, false, 3, RotateDir, 0, 2, 15, 4, AttackDelay_));
            AttackDelay_ += 0.1f;
        }
    }

    void BlobFour()
    {
        float AttackDelay_ = 0;
        transform.LookAt(PC.transform.position);
        Vector3 RotateDir = transform.rotation.eulerAngles;
        float yAxis = RotateDir.y;

        for (int i = 0; i < 70; i++)
        {
            RotateDir = new Vector3(0, yAxis, 0);

            yAxis += 22;

            StartCoroutine(Blob1(gameObject, false, 4, RotateDir, 0, 2, 17, 4, AttackDelay_));
            AttackDelay_ += 0.1f;
        }
    }

    void BlobFive() // longer dur.
    {
        float AttackDelay_ = 0;
        transform.LookAt(PC.transform.position);
        Vector3 RotateDir = transform.rotation.eulerAngles;
        float yAxis = RotateDir.y;

        for (int i = 0; i < 50; i++)
        {
            RotateDir = new Vector3(0, yAxis, 0);

            yAxis += 11;

            StartCoroutine(Blob1(gameObject, false, 5, RotateDir, 0, 2, 8, 6, AttackDelay_));
            AttackDelay_ += 0.06f;
        }
    }

    IEnumerator Blob1(GameObject BlobDad, bool heal, int AttackNumb, Vector3 BlobRot, int DirMod, float BHealth, float Bspeed, float lifeTime, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject CurBlob = Instantiate(BlobAttack1Object, transform.position, transform.rotation, transform);
        Monster CurBlob_ = CurBlob.GetComponent<Monster>();
        if (!heal)
        {
            BlobDad.GetComponent<Monster>().TakeDamage(0.5f);
        }
        ParticleSystem ps = CurBlob_.BlobPS;
        var main = ps.main;
        if (AttackNumb == 1)
        {
            CurBlob.transform.LookAt(PC.transform);
            CurBlob.transform.Rotate(Vector3.up * DirMod);
        }
        if (AttackNumb == 2 || AttackNumb == 3 || AttackNumb == 4 || AttackNumb == 5)
        {
            CurBlob.transform.rotation = Quaternion.Euler(BlobRot);
        }
        if (AttackNumb == 4)
        {
            CurBlob_.BlobAttack4 = true;
            CurBlob_.Blob4RotSpeed = 55;
        }
        if (AttackNumb == 5)
        {
            CurBlob_.BlobAttack4 = true;
            CurBlob_.Blob4RotSpeed = 32;
        }
        if (AttackNumb == 0) // heal
        {
            CurBlob.transform.position = BlobRot;
            CurBlob.transform.LookAt(BlobDad.transform);
            CurBlob.transform.parent = null;
        }
        else // so wont trigger on heal but all other attacks.
        {
            CurBlob.transform.position += CurBlob.transform.forward * 4;
        }

        main.startColor = BossLight.color;
        CurBlob_.BlobPS2.startColor = BossLight.color;
        CurBlob_.health = BHealth;
        CurBlob_.health2 = BHealth;
        CurBlob_.Healboss = heal;
        CurBlob_.MovementSpeed = Bspeed;
        CurBlob_.MovementSpeed_ = Bspeed;
        CurBlob_.BlobAttackNoTarget = true;
        CurBlob_.BlobDie = true;
        CurBlob_.BlobDieTimer = lifeTime;

    }

    void BlobExpire()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Destroy(transform.GetChild(0).gameObject, 0.99f);
        transform.GetChild(0).gameObject.transform.parent = null;
        Destroy(gameObject);
    }

    void TimeKeeperAlive()
    {
        animChild.GetComponent<MonsterAnim>().SpawnTK(-0.75f);
    }
    void TKFight()
    {
        tag = "Monster";
        TKSpawning = false;
    }

    public void AttackFriendFunc()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && enemy != gameObject)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= 50)
        {
            PC = nearestEnemy;
        }
        else
        {
            PC = null;
        }
    }
    public void StopFriendAttack()
    {
        Invoke("StopFriendAttack_", 5);
    }
    public void StopFriendAttack_()
    {
        AttackFriend = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Boss)
        {
            BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
        }
        if (BigBoy)
        {
            BigBoyAttack(); // moved all update code here.
        }
        if (OldKing && InCombat && !StartScene && tag != "Ressing")
        {
            OldKingAttack();
        }
        if (TheBlob)
        {
            TheBlobAttack();
            ChangeColorBlob();
        }

        //Checks if slowed | burning.
        AmISlowed();
        AmIBurning();
        if (!BlobAttackNoTarget)
        {
            if (GameObject.FindWithTag("Illusion") != null && !StartScene)
            {
                PC = GameObject.FindWithTag("Illusion");
                AttackDelay = 0;
            }
            else if (!StartScene) // IlluScript.
            {

                if (AttackFriend)
                {
                    AttackFriendFunc();
                    AttackDelay = AttackDelay_;

                }
                else
                {
                    PC = PC_;
                    AttackDelay = AttackDelay_;
                }
            }
            else
            {
                PC = StartOpponent;
                meleeRange = 8;
            }
        }


        if (!CurrentlyRessing && !BlobAttackNoTarget && !TheBlob && PC != null)
        {
            float dist = Vector3.Distance(transform.position, PC.transform.position);
            if (dist < AggroRange && !SpiderBoss)
            {
                InCombat = true;

                if (MonsterType != 5)
                {
                    if (agent.isOnNavMesh)
                    {
                        agent.destination = PC.transform.position;
                    }
                }

                if (MonsterType == 5)
                {
                    Vector3 dir = new Vector3(PC.transform.position.x, 2.5f, PC.transform.position.z) - this.transform.position;

                    float distThisFrame = MovementSpeed * Time.deltaTime;

                    transform.Translate(dir.normalized * distThisFrame, Space.World);
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * MovementSpeed);
                }
                if (MonsterType != 5)
                {
                    if (hardCodeDansGame <= 0 && agent.velocity.magnitude > 0.5f) // makes so the attack animation does not get canceled by run animation. && makes sure if they are not currently in attack animation and they are moving the running animation is active.
                    {
                        if (MonsterType == 1) { animChild.GetComponent<MonsterAnim>().RunAnim(); }
                        if (MonsterType == 2) { animChild.GetComponent<MonsterAnim>().RunAnimation(); }
                        if (MonsterType == 3) { animChild.GetComponent<MonsterAnim>().RunAnimation2(); }
                        if (MonsterType == 4) { animChild.GetComponent<MonsterAnim>().PlayerIdle(); } // Timekeeper
                        if (MonsterType == 6) { animChild.GetComponent<MonsterAnim>().RunAnimation3(MonsterTypeSubLayer); }
                    }
                }
                if (callForHelpCD <= 0f)
                {
                    Collider[] cols = Physics.OverlapSphere(transform.position, 10f);
                    foreach (Collider c in cols)
                    {
                        Monster e = c.GetComponent<Monster>();
                        if (e != null)
                        {
                            e.FriendAttacked();
                        }
                    }
                    callForHelpCD = 0.5f;
                }
                callForHelpCD -= Time.deltaTime;
            }
            else if (dist > AggroRange + 2 && !SpiderBoss && MonsterType != 5)
            {
                agent.destination = startPosition;
                if (Vector3.Distance(transform.position, agent.destination) < 2f)
                {
                    if (MonsterType == 1) { animChild.GetComponent<MonsterAnim>().IdleAnim(); }
                    if (MonsterType == 2) { animChild.GetComponent<MonsterAnim>().IdleAnimation(); }
                    if (MonsterType == 3) { animChild.GetComponent<MonsterAnim>().IdleAnimation2(); }
                    if (MonsterType == 4) { animChild.GetComponent<MonsterAnim>().PlayerIdle(); } // Timekeeper
                    if (MonsterType == 6) { animChild.GetComponent<MonsterAnim>().IdleAnimation3(); }
                }
            }
            if (SpiderBoss)
            {
                agent.destination = startPosition;
                if (!Swarm)
                {
                    animChild.GetComponent<MonsterAnim>().IdleAnimation2();
                }
                else
                {
                    animChild.GetComponent<MonsterAnim>().RunAnimation2();
                }
            }
            if (TimeKSpin) // Timekeeper special attack, chaning destination.
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 130f, Space.World);
                agent.destination = transform.position + (transform.forward * 4);
            }

            //Attack code
            if (dist <= meleeRange + 0.5f && dist <= AggroRange) { canAttack = true; }
            else if (dist > meleeRange + 1f) { canAttack = false; }

            if (canAttack && !BBStill)
            {
                if (!SpiderBoss && !TimeKSpin && !TKSpawning)
                {
                    RotateTowards(PC.transform); // if in melee range, rotate towards player }
                }
                //  agent.speed = 1.5f; // Have to change this or slow will be tricky. 
                if (attackCountdown <= 0f)
                {

                    if (MonsterType == 1) { animChild.GetComponent<MonsterAnim>().AttackAnim(); }
                    if (MonsterType == 2) { animChild.GetComponent<MonsterAnim>().AttackAnimation(); }
                    if (MonsterType == 3 && !SpiderBoss) { animChild.GetComponent<MonsterAnim>().AttackAnimation2(); }
                    if (MonsterType == 4) { animChild.GetComponent<MonsterAnim>().TimeKeeperAttack(); }
                    if (MonsterType == 6) { animChild.GetComponent<MonsterAnim>().AttackAnimation3(); }
                    Invoke("Attack", AttackDelay);

                    if (MonsterType == 1 || MonsterType == 3 || MonsterType == 5 || MonsterType == 6 || SpiderBoss) // non casters
                    {
                        attackCountdown = AttackSpeed;
                    }
                    else
                    {
                        var RandomAS = Random.Range(AttackSpeed, CasterVariationAS);
                        attackCountdown = RandomAS;
                    }
                    hardCodeDansGame = attackAnimCD; //smthing with animation
                }
            }
            else
            {
                if (MonsterType != 5)
                {
                    agent.speed = MovementSpeed;
                }
            }
            attackCountdown -= Time.deltaTime;
            hardCodeDansGame -= Time.deltaTime;
            MirrorImageCD_ -= Time.deltaTime;
            Ratatatata_ -= Time.deltaTime;
            BigBoySpecial1_ -= Time.deltaTime;
            SwarmCD_ -= Time.deltaTime;


            if (IlluHit)
            {
                TakeDamage(Time.deltaTime / 2);
            }

            if (MonsterType != 5 && !BBStill)
            {
                if (Vector3.Distance(PC.transform.position, agent.transform.position) < meleeRange && agent.isOnNavMesh)
                { //Makes enemies stop if they are in attackrange (so they wont try to run closer if a obstacle is in the way).
                    agent.isStopped = true; // Might need to refine if i create walls that block spells, and enemies should be smart enough to avoid them
                }
                else if (!CurrentlyFrozen && agent.isOnNavMesh)
                {
                    agent.isStopped = false;
                }
            }
        }
        else if (RegenLife) // heal if currently ressing.
        {
            if (health <= health2 - 0.01f && !OldKing)
            {
                health += (health2 / ResTimer * Time.deltaTime);
                Healthbar.fillAmount = health / health2;
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (BlobAttackNoTarget && !OnlyOnce)
        {
            if (other.tag == "Wall")
            {
                OnlyOnce = true;
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject, 0.99f);
                transform.GetChild(0).gameObject.transform.parent = null;
                Destroy(gameObject);
            }
            if (other.tag == "Player")
            {
                OnlyOnce = true;
                other.GetComponent<Player>().TakeDamage(damage);
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject, 0.99f);
                transform.GetChild(0).gameObject.transform.parent = null;

                Destroy(gameObject);
            }

            if (other.tag == "Illusion")
            {
                OnlyOnce = true;
                other.GetComponent<IlluScript>().TakeDamage(damage);
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject, 0.99f);
                transform.GetChild(0).gameObject.transform.parent = null;
                Destroy(gameObject);
            }
            if (other.tag == "Monster" && other.GetComponent<Monster>().TheBlob == true && Healboss)
            {
                OnlyOnce = true;
                if (other.GetComponent<Monster>().health + health <= other.GetComponent<Monster>().health2)
                {
                    other.GetComponent<Monster>().TakeDamage(-health); // heals the remaning amount of health this mob has.
                }
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject, 0.99f);
                transform.GetChild(0).gameObject.transform.parent = null;
                Destroy(gameObject);
            }

        }
    }


    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void RotateTowards(Transform target) // if in melee range, rotate towards player
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnRate);
    }

    void StopSpinHC()
    {
        CasterVariationAS = CasterVariationASRat;
        AttackSpeed = AttackSpeedRat;
        attackCountdown = 0.5f;
    }

    public void StopSpin()
    {
        var RandomTime = Random.Range(10, 15);
        MirrorImageCD_ = RandomTime;
        TimeKSpin = false;
        Ratatatata_ = RandomTime * 2.5f;
    }

    public void Attack()
    {
        if (MonsterType == 1 || MonsterType == 6 || (MonsterType == 3 && !SpiderBoss) && !CurrentlyRessing)
        {
            float dist = Vector3.Distance(transform.position, PC.transform.position);
            if (GameObject.FindWithTag("Illusion") != null && !StartScene)
            {
                PC.GetComponent<IlluScript>().TakeDamage(damage);
            }
            else if (!StartScene && dist < meleeRange + 6 && tag != "Ressing" && !AttackFriend)
            {
                PC.GetComponent<Player>().TakeDamage(damage);
            }
            else if (StartScene)
            {
                PC.GetComponent<Monster>().FakeFight(damage);
            }
            else if (AttackFriend)
            {
                PC.GetComponent<Monster>().TakeDamage(damage);
            }
        }

        if (MonsterType == 5)
        {
            if (GameObject.FindWithTag("Illusion") != null)
            {
                PC.GetComponent<IlluScript>().TakeDamage(damage);
            }
            else if (!AttackFriend)
            {
                PC.GetComponent<Player>().TakeDamage(damage);
            }
            else if (AttackFriend)
            {
                PC.GetComponent<Monster>().TakeDamage(damage);
            }

            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(transform.GetChild(0).gameObject, 0.99f);
            transform.GetChild(0).gameObject.transform.parent = null;

            transform.GetChild(0).gameObject.SetActive(true); // twice cuz second child is now first.
            Destroy(transform.GetChild(0).gameObject, 0.99f);
            transform.GetChild(0).gameObject.transform.parent = null;

            Destroy(gameObject);
        }



        if (MonsterType == 2 || MonsterType == 4)
        {
            if (MonsterType == 4)
            {
                var RandomSpell = Random.Range(0, 5);
                switch (RandomSpell)
                {
                    case 0:
                        currentspellObject = currentspellObject3;
                        currentSpell = currentSpell3;
                        MonsterTypeSubLayer = 2;
                        break;
                    case 1:
                        currentspellObject = currentspellObject2;
                        currentSpell = currentSpell2;
                        MonsterTypeSubLayer = 1;
                        break;
                    case 2:
                        currentspellObject = currentspellObject4;
                        currentSpell = currentSpell4;
                        MonsterTypeSubLayer = 3;
                        break;
                    case 3:// extra 2 to make lightningbolt rarer
                        currentspellObject = currentspellObject3;
                        currentSpell = currentSpell3;
                        MonsterTypeSubLayer = 2;
                        break;
                    case 4:
                        currentspellObject = currentspellObject2;
                        currentSpell = currentSpell2;
                        MonsterTypeSubLayer = 1;
                        break;

                }
            }



            if (attackCountdown >= 0.2f || !IamIllu) // so illus wont attack on spawn.
            {

                GameObject test123 = Instantiate(currentspellObject, castPoint.transform.position, castPoint.transform.rotation, castPoint.transform);
                SpellProjectile spell = test123.GetComponent<SpellProjectile>();

                switch (MonsterTypeSubLayer)
                {
                    case 1:
                        spell.projectilespeed = currentSpell.GetComponent<FrostBolt>().projectilespeed;
                        spell.damage = currentSpell.GetComponent<FrostBolt>().damage;
                        spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
                        spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
                        spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent + 0.1f;

                        break;
                    case 2:
                        spell.projectilespeed = currentSpell.GetComponent<Fireball>().projectilespeed;
                        spell.damage = currentSpell.GetComponent<Fireball>().damage - 0.5f;
                        spell.FireBallBurn = currentSpell.GetComponent<Fireball>().FireBallBurn;
                        spell.BurnDuration = currentSpell.GetComponent<Fireball>().BurnDuration;
                        spell.BurnPercent = currentSpell.GetComponent<Fireball>().BurnPercent;
                        break;
                    case 3:
                        spell.projectilespeed = currentSpell.GetComponent<LightningBolt>().projectilespeed;
                        spell.damage = currentSpell.GetComponent<LightningBolt>().damage;
                        spell.LBBounce = currentSpell.GetComponent<LightningBolt>().LBBounce;
                        spell.LBBounceAmount = currentSpell.GetComponent<LightningBolt>().LBBounceAmount = 1;
                        break;
                }

                if (MonsterType == 4)
                {
                    float RandomSpeed = Random.Range(10, 30);


                    if (MonsterTypeSubLayer == 3)
                    {
                        RandomSpeed = RandomSpeed / 1.5f;
                    }
                    spell.projectilespeed = RandomSpeed;
                }
                spell.spellCastLocation = agent.destination;
                if (!AttackFriend)
                {
                    spell.enemyCastingspell = true;
                }
            }


            Invoke("TimeKeeperAttacks", 0.1f);
        }
        if (SpiderBoss)
        {

            if (SwarmCD_ <= 0 && !Swarm)
            {
                Swarm = true;
                attackCountdown = 13f;
                SwarmCD_ = SwarmCD;

                Invoke("StartSwarm", 1.5f);
                Invoke("StopSwarm", 3f);
            }
            else
            {
                GameObject ball = Instantiate(SBAttack, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z - 4f), Quaternion.identity);
                ball.GetComponent<Rigidbody>().velocity = BallisticVel(PC.transform, 30f);
                ball.GetComponent<SpiderBall>().Daddy = gameObject;
            }
        }
    }
    void StartSwarm() // so animation can start before spiders spawn.
    {
        SummonSwarm(20);
    }

    void StopSwarm()
    {
        Swarm = false;
    }
    public void ReturnType5MovementSpeed(float time) // never called?
    {
        Debug.Log("Hello=?");
        Invoke("RT5MS", time);
    }
    private void RT5MS() // never called?
    {
        Type5BHSLOW = false;
        MovementSpeed = MovementSpeed_;
        if (slowedDur > 0)
        {
            Slow(true, slowedDur, CurrentSlowSTR);

        }
    }

    void SummonSwarm(int count)
    {

        GameObject Spider = Instantiate(SpiderSwarm, transform.position - (transform.forward * 3), transform.rotation);
        Spider.GetComponent<Monster>().AggroRange = 50;
        Spider.GetComponent<Monster>().MovementSpeed = 3f;
        Spider.GetComponent<Monster>().damage = 0.5f;
        Spider.GetComponent<Monster>().health = 1;
        Spider.GetComponent<Monster>().health2 = 1;
        Spider.GetComponent<Monster>().MonsterTypeSubLayer = 2;
        Spider.GetComponent<Monster>().meleeRange = 2;
        Spider.transform.parent = GameObject.FindGameObjectWithTag("SpiderBossRoom").transform;
        Spider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Spider.GetComponent<Collider>().GetComponent<CapsuleCollider>().height = 10;
        Spider.GetComponent<UnityEngine.AI.NavMeshAgent>().radius = 1f;

        var RandomSpot = Random.Range(0, 3);
        var RandomSpot2 = Random.Range(0, 3);

        Spider.transform.localPosition = new Vector3(transform.localPosition.x + RandomSpot, transform.localPosition.y, transform.localPosition.z + RandomSpot2);

        if (count > 0)
        {
            SummonSwarm(count - 1);
        }
    }
    public void IlluMaxTime()
    {
        TakeDamage(0.1f);
    }

    public void TimeKeeperAttacks()
    {
        if (MonsterType == 4 && IamIllu == false) // TimeKeeper
        {
            GameManager manag = GameObject.FindObjectOfType<GameManager>();

            if (manag.Illus.Count == 0 && Ratatatata_ <= 0)
            {
                agent.Warp(TimeKeeperPoints[0].transform.position);
                TimeKSpin = true;
                AttackSpeed = 0.1f;
                CasterVariationAS = 0.1f;
                Invoke("StopSpinHC", 7.4f);
                Invoke("StopSpin", 8f);
                Ratatatata_ = Ratatatata;
            }


            if (manag.Illus.Count == 0 && !TimeKSpin)
            {
                TeleLoc = Random.Range(0, TimeKeeperPoints.Count);
                agent.Warp(TimeKeeperPoints[TeleLoc].transform.position);
            }


            if (MirrorImageCD_ <= 0 && manag.Illus.Count == 0 && !TimeKSpin)
            {
                //float randomAS2 = Random.Range(3, 6);
                //attackCountdown = randomAS2;
                for (int i = 0; i < 5; i++)
                {
                    if (i != TeleLoc)
                    {
                        Vector3 Pos = TimeKeeperPoints[i].transform.position;
                        GameObject MMI = Instantiate(gameObject, Pos, transform.rotation);
                        Monster Illu = MMI.GetComponent<Monster>();

                        //   var FakeMaxHealth = health / health2;
                        Illu.health = 5;
                        Illu.health2 = 5;
                        Illu.Healthbar.fillAmount = health / health2;
                        Illu.MirrorImageCD = 100000;
                        Illu.MirrorImageCD_ = 100000;
                        Illu.IamIllu = true;
                        Illu.Boss = false;
                        float randomAS = Random.Range(3, 6);
                        Illu.AttackSpeed = 2;

                        // Illu.attackCountdown = randomAS-3;
                        Illu.CasterVariationAS = randomAS;
                        Illu.Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
                        Illu.Invoke("IlluMaxTime", 15);
                        manag.Illus.Add(MMI);
                    }
                }
                ParticleSystem ps = OuterRing.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.startDelay = 0;
                //  OuterRing.startDelay = 0f;

                OuterRing.Play(true);
                MirrorImageCD_ = MirrorImageCD;


            }

        }
    }


    public Vector3 BallisticVel(Transform target, float angle)
    {//SpiderBossAttack
        var dir = target.position - new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z - 4f);  // get target direction
        var h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        var dist = dir.magnitude;  // get horizontal distance
        var a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
                                   // calculate the velocity magnitude
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }
    void FakeFight(float damage)
    {
        health -= damage;
        Healthbar.fillAmount = health / health2;
        if (Boss)
        {
            BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
        }

        if (health <= 0)
        {
            KillMonster();
            meleeRange = meleeRange_;
            StartScene = false;
            StartOpponent.GetComponent<Monster>().StartScene = false;
            StartOpponent.GetComponent<Monster>().meleeRange = StartOpponent.GetComponent<Monster>().meleeRange_;
            //StartOpponent.GetComponent<Monster>().AggroRange = AggroRange_;
        }
    }

    public void TakeDamage(float damage)
    {
        if (StartScene)
        {
            meleeRange = meleeRange_;
            StartScene = false;
            StartOpponent.GetComponent<Monster>().StartScene = false;
            StartOpponent.GetComponent<Monster>().meleeRange = StartOpponent.GetComponent<Monster>().meleeRange_;
        }
        health -= damage;
        if (Brother != null)
        {
            Brother.GetComponent<Monster>().health -= damage;
            Brother.GetComponent<Monster>().Healthbar.fillAmount = health / health2;
        }
        Healthbar.fillAmount = health / health2;
        if (Boss)
        {
            BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
        }

        if (!TimeKeeper && !StartScene)
        {
            AggroRange = 999;
        }

        if (IamIllu)
        {
            Healthbar.color = Color.blue;
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
            IlluHit = true;
        }

        if (health <= 0 && OnlyOnce == false)
        {
            OnlyOnce = true;
            if (Brother != null)
            {
                Brother.GetComponent<Monster>().Brother = null;
                Brother.GetComponent<Monster>().KillMonster();
            }
            KillMonster();
        }
    }



    void KillMonster()
    {
        if (Immortal)
        {
            Resurrect = true;
        }

        if (Boss && !Resurrect)
        {
            BossHealthAct.transform.GetChild(0).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(1).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(2).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(3).gameObject.SetActive(false);
        }

        // Explode! if boosted and burning.
        if (BurnDur > 0 && CheckIfBurnBoosted)
        {
            GameObject Exp = Instantiate(CorpseExplosion, transform.position, transform.rotation, transform);
            Exp.transform.parent = null;
            Exp.GetComponent<ExplodeScript>().BoostBurnDur = BoostBurnDur;
            Exp.GetComponent<ExplodeScript>().BoostBurnPer = BoostBurnPer;
            Exp.GetComponent<ExplodeScript>().BurnDamage = BurnDamage;
            Exp.GetComponent<ExplodeScript>().BoostTotalBurn = BoostBurnDamage;
            Exp.GetComponent<ExplodeScript>().FireTrueFrostFalse = true;
            //Collider[] cols2 = Physics.OverlapSphere(transform.position, 5f);
            //foreach (Collider c in cols2)
            //{
            //    Monster e = c.GetComponent<Monster>();
            //    if (e != null && e.gameObject != gameObject && !e.CurrentlyRessing && e.OnlyOnce == false)
            //    {        
            //        e.Burn(true, BoostBurnDur, BoostBurnPer, BurnDamage * BoostBurnPer);
            //        e.TakeDamage(BurnDamage * BoostBurnPer);
            //    }
            //}
        }

        if (slowedDur > 0 && CheckIfFrostBoosted)
        {
            GameObject Exp = Instantiate(FrostExplosion, transform.position, transform.rotation, transform);
            Exp.transform.parent = null;
            Exp.GetComponent<ExplodeScript>().BoostBurnDur = BoostSlowDur;
            Exp.GetComponent<ExplodeScript>().BoostBurnPer = BoostSlowPer;
            Exp.GetComponent<ExplodeScript>().FireTrueFrostFalse = false;

            //Collider[] cols2 = Physics.OverlapSphere(transform.position, 5f);
            //foreach (Collider c in cols2)
            //{
            //    Monster e = c.GetComponent<Monster>();
            //    if (e != null && e.gameObject != gameObject && !e.CurrentlyRessing) //&& e.MonsterType != 5)
            //    {
            //        e.Slow(true, 4f, 1.4f); //hardcoded atm. CHANGE!!!!!!!
            //            e.StopAgent();
            //    }
            //    //if (e != null && e.gameObject != gameObject && !e.CurrentlyRessing && e.MonsterType == 5)
            //    //    {  
            //    //        e.Slow(true, 3, 9999);
            //    //        e.StopAgent();
            //    //    }

            //}
        }
        if (MonsterType == 1) { animChild.GetComponent<MonsterAnim>().DieAnim(); }
        if (MonsterType == 2) { animChild.GetComponent<MonsterAnim>().DieAnimation(); }

        if (MonsterType == 4) { animChild.GetComponent<MonsterAnim>().DieTK(); }
        if (MonsterType == 3)
        {
            animChild.GetComponent<MonsterAnim>().DieAnimation2();
            if (!SpiderBoss && MonsterTypeSubLayer != 2)
            {
                Instantiate(SpiderPoison, new Vector3(transform.position.x, 2f, transform.position.z), transform.rotation);
            }
        }
        if (MonsterType == 5)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(transform.GetChild(0).gameObject, 0.99f);
            transform.GetChild(0).gameObject.transform.parent = null;
        }
        if (MonsterType == 6 && Resurrect && !Immortal) { animChild.GetComponent<MonsterAnim>().DieAnimation3(); }
        if (MonsterType == 6 && !Resurrect && !Immortal) { animChild.GetComponent<MonsterAnim>().DieAnimation4(); }

        if (IamIllu)
        {
            GameManager manag = GameObject.FindObjectOfType<GameManager>();
            manag.Illus.Remove(gameObject);
        }

        if (animChild != null && !Resurrect)
        {
            animChild.transform.parent = null;
        }

        SpiderBoss = false;
        if (!Resurrect)
        {
            Loot();
            Destroy(gameObject);
        }
        else if (!Immortal)
        {
            CurrentlyRessing = true;
            agent.isStopped = true;
            if (CurrentlyFrozen)
            {
                CurrentlyFrozen = false;
                Transform result = gameObject.transform.Find("frozen");
                if (result)
                {
                    Destroy(transform.Find("frozen").gameObject);
                }
                meleeRange = meleeRange_;
            }
            Invoke("RessurectMonster", 0.1f);
        }
        if (Immortal)
        {
            RealDeath = true;
            if (CurrentlyFrozen)
            {
                CurrentlyFrozen = false;
                Transform result = gameObject.transform.Find("frozen");
                if (result)
                {
                    Destroy(transform.Find("frozen").gameObject);
                }
                meleeRange = meleeRange_;
            }
            if (!OldKing)
            {
                SkeletonKing.SkeletonList.Add(gameObject);
                SkeletonKing.SkeletonListAlive.Remove(gameObject);
            }
            //    Invoke("SkeletonStartDead", 0.05f);
            SkeletonStartDead();
        }

        Collider[] cols = Physics.OverlapSphere(transform.position, 20f); //aggro range when enemy hit.
        foreach (Collider c in cols)
        {
            Monster e = c.GetComponent<Monster>();
            if (e != null && c.gameObject != gameObject)
            {
                e.FriendAttacked();
            }
        }
    }
    public void RessurectMonster()
    {
        slowedDur = 0;
        BurnDur = 0;
        tag = "Ressing";
        GetComponent<Collider>().GetComponent<CapsuleCollider>().enabled = false;
        Invoke("StartRessing", ResTimer - 0.1f);
    }
    void StartRessing()
    {
        health = 0;
        RegenLife = true;
        if (!OldKing)
        {
            Invoke("Resurected", ResTimer);
        }
        else
        {
            Invoke("KingResAnim", 2);
            Invoke("Resurected", ResTimer + 2);
        }
    }
    void KingResAnim()
    {
        animChild.GetComponent<MonsterAnim>().SkeletonRise();
    }

    public void Resurected()
    {
        tag = "Monster";
        hardCodeDansGame = 0;
        GetComponent<Collider>().GetComponent<CapsuleCollider>().enabled = true;
        CurrentlyRessing = false;
        OnlyOnce = false;
        Resurrect = false;
    }

    public void FixedUpdate()
    {
        if (pushed && !SpiderBoss && !TheBlob)
        {
            PushMonster();
        }


        if (BlobAttackNoTarget)
        {
            //Vector3 dir = transform.forward;
            float distThisFrame = MovementSpeed * Time.deltaTime;

            //transform.Translate(dir.normalized * distThisFrame, Space.World);

            if (BlobAttack4)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * Blob4RotSpeed);
            }

            transform.position += transform.forward * distThisFrame;

        }

    }

    public void PushMonster()
    {
        transform.position = transform.position + ((pushDir * Time.fixedDeltaTime * 10) / (PushResistance + 1));
        Invoke("StopPush", 0.8f);
    }

    public void ChannelPush(float pushStr)
    {

        if (!SpiderBoss && !TheBlob)
        {
            transform.position = transform.position + ((pushDir * pushStr) / (PushResistance + 1));
        }
    }

    public void StopPush()
    {
        pushed = false;
        //   this.GetComponent<Rigidbody>().velocity = BaseVel;
    }

    public void StopAgent()
    {
        if ((agent || MonsterType == 5) && !CurrentlyRessing)
        {
            CurrentlyFrozen = true;
            meleeRange = 0f;
            if (MonsterType != 5)
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                }
            }
            Invoke("StartAgent", 3f); // HARDCODED

            Transform result = gameObject.transform.Find("frozen");
            if (!result)
            {
                GameObject frozen = Instantiate(Frozen, transform);
                frozen.name = "frozen";
            }
        }
    }
    public void StartAgent()
    {
        if ((agent || MonsterType == 5) && !CurrentlyRessing)
        {
            CurrentlyFrozen = false;
            Transform result = gameObject.transform.Find("frozen");
            if (result)
            {
                Destroy(transform.Find("frozen").gameObject);
            }
            meleeRange = meleeRange_;
            if (MonsterType != 5)
            {
                agent.isStopped = false;
            }
        }
    }

    public void FriendAttacked()
    {
        AggroRange = 999;
    }

    public void Slow(bool slow, float dur, float str) //Could slow attackspeed aswell, though might take a bit of work to slow down animations aswell.
    {
        CurrentSlowSTR = str;

        if (slow && MovementSpeed >= MovementSpeed_ && !CurrentlyRessing) //slow is true and currently not slowed. How to work if enemy has a sprint effect? to be seen.
        {


            MovementSpeed /= str;

            if (MonsterType != 5)
            {
                agent.speed = MovementSpeed;
            }


            if (str >= 1.40f)
            {
                CheckIfFrostBoosted = true;
                BoostSlowDur = dur;
                BoostSlowPer = str;
            }
            else
            {
                CheckIfFrostBoosted = false;
            }

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

        if (CurrentlyFrozen && MonsterType == 5)
        {
            MovementSpeed = 0;
        }
        else if (!CurrentlyFrozen && MonsterType == 5 && !TheBlob && MovementSpeed == 0)
        {
            MovementSpeed = MovementSpeed_;
            if (slowedDur > 0)
            {
                Slow(true, slowedDur, CurrentSlowSTR);
            }
        }

        if (slowedDur <= 0)
        {
            if (!Type5BHSLOW) // type5BHSLOW currently always false.. never changed.
            {
                MovementSpeed = MovementSpeed_;
            }
            if (MonsterType != 5)
            {
                agent.speed = MovementSpeed;
            }
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

    public void Burn(bool burn, float dur, float str, float dmg)
    {
        if (burn && !CurrentlyRessing)
        {
            BurnDamage += dmg;
            TotalBurnDamage = BurnDamage * str;
            BurnDur = dur;

            if (str >= 0.30f)
            {
                CheckIfBurnBoosted = true;
                BoostBurnDamage = TotalBurnDamage;
                BoostBurnDur = dur;
                BoostBurnPer = str;
            }

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
        if (BurnDur <= 0)
        {
            BurnDamage = 0;
            Transform result = gameObject.transform.Find("burn");
            if (result)
            {
                CheckIfBurnBoosted = false;
                Destroy(transform.Find("burn").gameObject);
            }
        }
        else
        {
            BurnDur -= Time.deltaTime;
            TakeDamage(TotalBurnDamage * Time.deltaTime);
        }
    }

    public void BoltBounce(bool bounce, bool channel, GameObject bolt, GameObject Current, string chainID)
    {

        if (channel && ChannelTimer > 0)
        {
            noBounce = true;
        }
        else
        {
            noBounce = false;
        }

        if (bounce && noBounce == false && !CurrentlyRessing)
        {
            List<GameObject> MonsterList = new List<GameObject>();

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Monster"))
            { // if not null might need
                float dist = Vector3.Distance(enemy.transform.position, transform.position);
                if (dist < 20 && dist > 1)
                {
                    MonsterList.Add(enemy);
                }
            }
            if (MonsterList.Count >= 1)
            {
                var randomTarget = Random.Range(0, MonsterList.Count);
                var Rot = Quaternion.LookRotation((MonsterList[randomTarget].transform.position - transform.position).normalized);
                var Pos2 = Vector3.MoveTowards(this.transform.position, MonsterList[randomTarget].transform.position, BounceDistance);

                GameObject Bounce = Instantiate(bolt, new Vector3(Pos2.x, 2.6f, Pos2.z), Rot, this.transform);
                SpellProjectile spell = Bounce.GetComponent<SpellProjectile>();

                if (Current.GetComponent<SpellProjectile>() != null)
                {
                    if (!spell.enabled)
                    {
                        spell.enabled = true;
                    }

                    SpellProjectile curr = Current.GetComponent<SpellProjectile>();
                    spell.damage = curr.damage;
                    spell.projectilespeed = curr.projectilespeed;
                    spell.ghostCast = false;
                    spell.spellName = curr.spellName;
                    spell.LBBounce = curr.LBBounce;
                    // spell.Push = curr.Push;
                    // spell.BlessedAim = curr.BlessedAim;
                    spell.LBBounceAmount = curr.LBBounceAmount - 1;
                    spell.chainID = chainID;
                    spell.enemyCastingspell = false;
                    spell.LBCopy = true;
                }
                else
                {
                    Poolscript curr = Current.GetComponent<Poolscript>();
                    spell.damage = curr.damage;
                    spell.projectilespeed = curr.projectilespeed;
                    spell.ghostCast = false;
                    spell.spellName = curr.spellName;
                    spell.LBBounce = curr.LBBounce;
                    spell.chainID = chainID;
                    spell.LBBounceAmount = curr.LBBounceAmount - 1;
                    spell.enemyCastingspell = false;
                    spell.LBCopy = true;
                }



                Bounce.transform.parent = null;
                Bounce.transform.localScale = new Vector3(1, 1, 1);

                spell.spellCastLocation = MonsterList[randomTarget].transform.position;

                spell.channeling = false;
                spell.cone = false;
                spell.aoeSizeMeteor = 0;


                spell.BHBool = false;

                if (spell.LBBounceAmount <= 0)
                {
                    spell.LBBounce = false; // could make it count --;
                }


                if (channel)
                {
                    Bounce.gameObject.GetComponent<Collider>().enabled = true;
                    spell.lightChild1.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild2.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild3.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild4.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild5.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild6.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    //   Invoke("CantBounce", 0.5f); // can't bounce again for 0.5s
                    //    noBounce = true;
                    ChannelTimer = 0.75f;
                }


            }
            MonsterList.Clear();
        }

        ChannelTimer -= Time.deltaTime;
    }


    public void CantBounce()
    {
        noBounce = false;
    }


    public void Loot() // make it better, different monster can drop different loot, different drop rate. 
    {
        if (Boss && !TheBlob) // or any other boss
        {

            var RandomLoot = Random.Range(0, 2);
            switch (RandomLoot)
            {
                case 0:
                    GameObject BossLoot = Instantiate(BossWeapon, transform.position, Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z), GameObject.FindGameObjectWithTag("SpiderBossRoom").transform);
                    BossLoot.transform.localPosition = LootLoc;
                    break;
                case 1:
                    GameObject BossLoot2 = Instantiate(BossArmor, transform.position, Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z), GameObject.FindGameObjectWithTag("SpiderBossRoom").transform);
                    BossLoot2.transform.localPosition = LootLoc;
                    break;
            }
        }
    }

    // Most Immortal king code below
    void SkeletonStartDead()
    {
        agent.radius = 0.1f;
        slowedDur = 0;
        BurnDur = 0;
        Resurrect = true;
        CurrentlyRessing = true;
        agent.isStopped = true;
        OnlyOnce = true;
        tag = "Ressing";
        health = 0;
        GetComponent<Collider>().GetComponent<CapsuleCollider>().enabled = false;

        if (!RealDeath)
        {
            animChild.GetComponent<MonsterAnim>().StartDeadAnim();
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if (!OldKing)
        {
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            animChild.GetComponent<MonsterAnim>().DieAnimation5();
        }
        else if (OldKing)
        {
            animChild.GetComponent<MonsterAnim>().DieAnimation5();
            OldKingSpecialAttack_1 = 6f;
            Invoke("LongLiveTheKing", 2.5f);
            BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
        }
    }

    public void LongLiveTheKing()
    {
        if (SkeletonListAlive.Count > 0)
        {
            StartRessing();
            hardCodeDansGame = 0;
            foreach (var Skelly in SkeletonListAlive)
            {
                GameObject Heal = Instantiate(Skill1Projectile, Skelly.transform);
                Heal.transform.position = new Vector3(Skelly.transform.position.x, 3, Skelly.transform.position.z);
                Heal.GetComponent<OldKingProjectile>().Skeleton = Skelly.GetComponent<Monster>().StartOpponent;
                Heal.GetComponent<OldKingProjectile>().CurHealth = Skelly.GetComponent<Monster>().health;
                Heal.GetComponent<OldKingProjectile>().distance = 10f;
                Heal.GetComponent<OldKingProjectile>().HealKing = true;
                Skelly.GetComponent<Monster>().health = 0;
                Skelly.GetComponent<Monster>().KillMonster();
            }
        }
        else
        {
            BossHealthAct.transform.GetChild(0).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(1).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(2).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(3).gameObject.SetActive(false);
            Loot();
            transform.parent.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); // hard code to open door after skellyboss
        }
    }

    void OldKingAttack()
    {
        OldKingSpecialAttack_1 -= Time.deltaTime;
        if (OldKingSpecialAttack_1 <= 0)
        {
            agent.isStopped = true;
            BBStill = true; // lets see.. Remember to turn off.

            var RandomSpecialAttack = Random.Range(0, 5);
            switch (RandomSpecialAttack)
            {
                case 0:
                    if (SkeletonList.Count > 0)
                    {
                        OldKingAttack1(1, 0); // TEMP, change back to 1.
                    }
                    else
                    {
                        OldKingAttack2();
                    }
                    break;
                case 1:
                    if (SkeletonListAlive.Count > 1)
                    {
                        OldKingAttack2();
                    }
                    else
                    {
                        OldKingAttack1(1, 0);
                    }
                    break;
                case 2:
                    OldKingAttack3();
                    break;
                case 3:
                    if (SkeletonListAlive.Count < 4)
                    {
                        OldKingAttack1(3, -2 / 3f); // TEMP, change back to 1.
                    }
                    else
                    {
                        OldKingAttack2();
                    }
                    break;
                case 4: // lazy double to increase odds of defile
                    if (SkeletonListAlive.Count > 1)
                    {
                        OldKingAttack2();
                    }
                    else
                    {
                        OldKingAttack1(1, 0);
                    }
                    break;
                default:
                    break;
            }
            OldKingSpecialAttack_1 = OldKingSpecialAttack;
            attackCountdown = AttackSpeed;
            hardCodeDansGame = attackAnimCD;
        }
    }

    void OldKingAttack1(int count, float health)
    {
        for (int i = 0; i < count; i++)
        {
            var RandomMinion = Random.Range(0, SkeletonList.Count);

            transform.LookAt(SkeletonList[RandomMinion].gameObject.transform);
            StartCoroutine(SpawnMinion(SkeletonList[RandomMinion], 1f, health));
            GameObject Visual = Instantiate(Skill1Projectile, transform);
            Visual.transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            Visual.GetComponent<OldKingProjectile>().Skeleton = SkeletonList[RandomMinion];
            float dist = Vector3.Distance(transform.position, SkeletonList[RandomMinion].transform.position);
            Visual.GetComponent<OldKingProjectile>().distance = dist;

            GameObject Skelly = SkeletonList[RandomMinion].gameObject;// here already so can't ress same minion twice.
            SkeletonList.Remove(Skelly);
            SkeletonListAlive.Add(Skelly);

        }

        animChild.GetComponent<MonsterAnim>().OldKingSpecialAttack1();
        Invoke("OldKingAttackEnd", 3.89f);
    }
    void OldKingAttack2()
    {
        var RandomMinion = Random.Range(0, SkeletonListAlive.Count);
        transform.LookAt(SkeletonListAlive[RandomMinion].gameObject.transform);
        StartCoroutine(SacrificeMinion(SkeletonListAlive[RandomMinion], 1f));
        animChild.GetComponent<MonsterAnim>().OldKingSpecialAttack1();
        Invoke("OldKingAttackEnd", 3.89f);
        GameObject Visual = Instantiate(Skill2Projectile, transform);
        Visual.transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        Visual.GetComponent<OldKingProjectile>().Skeleton = SkeletonListAlive[RandomMinion];
        float dist = Vector3.Distance(transform.position, SkeletonListAlive[RandomMinion].transform.position);
        Visual.GetComponent<OldKingProjectile>().distance = dist;
        // Probably need code to specify what happens if minion dies before Sacrifice.
        SkeletonListAlive[RandomMinion].gameObject.GetComponent<Monster>().health = 999;
        SkeletonListAlive[RandomMinion].gameObject.GetComponent<Monster>().Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }
    void OldKingAttack3()
    {
        animChild.GetComponent<MonsterAnim>().OldKingSpecialAttack1();
        Invoke("OldKingAttackEnd", 3.89f);

        float OrbFacing = transform.rotation.eulerAngles.x;
        float attackTimer = 0;
        for (int i = 0; i < 5; i++)
        {
            GameObject P1 = Instantiate(Skill3Projectile, transform);
            P1.transform.Rotate(0, OrbFacing, 0);
            P1.transform.position = new Vector3(transform.position.x, 3, transform.position.z) + P1.transform.forward * 3;
            P1.GetComponent<OldKingAttack3>().FloatUpTimer += attackTimer;
            P1.GetComponent<OldKingAttack3>().Target = PC_;
            OrbFacing += 72; //360/5
            attackTimer += 0.6f;
        }
    }

    IEnumerator SpawnMinion(GameObject numb, float delay, float HP)
    {
        yield return new WaitForSeconds(delay);
        GameObject Skelly = numb;
        Monster Skel = Skelly.GetComponent<Monster>();
        Skel.StartRessing();
        Skel.hardCodeDansGame = 0;
        Skel.AggroRange = 99;
        Skel.animChild.GetComponent<MonsterAnim>().SkeletonRise();
        Skel.health = ((Skel.health2 * HP));

        Skel.Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
        Skel.SkeletonKing = gameObject.GetComponent<Monster>();

        Skel.agent.radius = 1f;

        if (Skel.Defiling)
        {
            Destroy(Skel.CurDefile);
        }
    }
    IEnumerator SacrificeMinion(GameObject numb, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject Skelly = numb;
        Monster Skel = Skelly.GetComponent<Monster>();
        Skel.health = 0;
        Skel.KillMonster();
        GameObject Defi = Instantiate(Skill2Defile, Skel.transform.position, Skel.transform.rotation, transform);
        Defi.transform.position = new Vector3(Defi.transform.position.x, 3f, Defi.transform.position.z);
        Defi.transform.parent = null;
        Skel.Defiling = true;
        Skel.CurDefile = Defi;
    }

    void OldKingAttackEnd()
    {
        BBStill = false;
    }

    // Most bigboy code below
    void BigBoyAggro() // start/after special attack resetting to normal mode.
    {
        if (!SummonHelp && health <= HelpHP)
        {
            BigBoyGlow5.SetActive(true);
            BigBoyGlow5b.SetActive(true);
            BigBoyCurrentAttack = 4;
        }
        else
        {
            var RandomSpecialAttack = Random.Range(0, 4);
            BigBoyCurrentAttack = RandomSpecialAttack;
            switch (RandomSpecialAttack)
            {
                case 0:
                    BigBoyGlow.SetActive(true);
                    break;
                case 1:
                    BigBoyGlow2.SetActive(true);
                    break;
                case 2:
                    BigBoyGlow3.SetActive(true);
                    break;
                case 3:
                    BigBoyGlow4.SetActive(true);
                    break;
            }
        }
        BBStill = false;
        if (!CurrentlyFrozen || MovementSpeed > 9) // preventing BigBoy from removing freeze after roar, unless Rage attack.
        {
            agent.isStopped = false;
        }
    }

    void BigBoyFire(Vector3 Pos)
    {
        Pos = new Vector3(Pos.x, 2, Pos.z);
        GameObject PoolObj = Instantiate(FireAttack, Pos + transform.forward * 4, transform.rotation, transform);
        PoolObj.transform.parent = null;
        PoolObj.transform.localScale = new Vector3(1, 1, 1);
        PoolObj.GetComponent<BigBoyFire>().PoolNumb = 20;
    }
    void BigBoySmash1Anim()
    {
        BigBoyGlow.SetActive(false);
        ParticleSystem ps = BigBoyGlow.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSize = 5;
        //  BigBoyGlow.GetComponent<ParticleSystem>().startSize = 5;
        Invoke("BigBoyAggro", 4f);
        Invoke("BBRoar", 0.7f);
        // Reset of attack code here.

        BigBoyFire(transform.position);
    }
    void BigBoySmash2Anim()
    {
        BigBoyGlow2.SetActive(false);
        ParticleSystem ps = BigBoyGlow2.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSize = 0.06f;
        //BigBoyGlow2.GetComponent<ParticleSystem>().startSize = 0.06f;
        Invoke("BigBoyAggro", 4f);
        Invoke("BBRoar", 0.7f);
        // Reset of attack code here.
        for (int i = 0; i < 5; i++)
        {
            GameObject SmallHelp = Instantiate(Smallboy, transform.position + (transform.forward * 3), transform.rotation, transform);
            SmallHelp.GetComponent<Monster>().AggroRange = 50;
            SmallHelp.GetComponent<Monster>().MovementSpeed = 5f;
            SmallHelp.GetComponent<Monster>().damage = 1f;
            SmallHelp.GetComponent<Monster>().health = 3;
            SmallHelp.GetComponent<Monster>().health2 = 3;

            float RandomSpot = Random.Range(0, 5);
            float RandomSpot2 = Random.Range(0, 5);
            SmallHelp.transform.parent = transform.parent;
            SmallHelp.transform.position = new Vector3(SmallHelp.transform.position.x + RandomSpot, SmallHelp.transform.position.y, SmallHelp.transform.position.z + RandomSpot2);
            SmallHelp.transform.localScale = new Vector3(1, 1, 1);
            SmallHelp.GetComponent<Monster>().animChild.GetComponent<MonsterAnim>().Spawn();
            SmallHelp.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            SmallHelp.GetComponent<Monster>().BBStill = true;
            SmallHelp.GetComponent<Monster>().Invoke("SmallSpawnAnimStop", 3f);
        }
    }
    void SmallSpawnAnimStop()
    {
        BBStill = false;
        agent.isStopped = false;
    }
    void BigBoySmash3Anim()
    {
        BigBoyGlow3.SetActive(false);
        ParticleSystem ps = BigBoyGlow3.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSize = 3f;
        //  BigBoyGlow3.GetComponent<ParticleSystem>().startSize = 3;
        Invoke("BigBoyAggro", 4f);
        Invoke("BBRoar", 0.7f);
        // Reset of attack code here.
        FrostNumber = 40;
        Invoke("FrostAttackFunc", 0.01f);

    }
    void FrostAttackFunc()
    {
        if (FrostNumber > 0)
        {
            GameObject test123 = Instantiate(currentspellObject, castPoint.transform.position, castPoint.transform.rotation, castPoint.transform);
            SpellProjectile spell = test123.GetComponent<SpellProjectile>();
            spell.projectilespeed = currentSpell.GetComponent<FrostBolt>().projectilespeed;
            spell.damage = currentSpell.GetComponent<FrostBolt>().damage;
            spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
            spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
            spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent + 0.1f;
            spell.aoeSizeMeteor = 3f;
            spell.BigBoyFrost = FrostAttack;
            float RandomSpot = Random.Range(-36f, 36f);
            float RandomSpot2 = Random.Range(-22, 18.5f);
            //  spell.transform.localScale = new Vector3(2f, 1.5f, 1.5f);
            spell.spellCastLocation = new Vector3(transform.parent.transform.position.x + RandomSpot, 1, transform.parent.transform.position.z + RandomSpot2);
            spell.transform.position = new Vector3(transform.parent.transform.position.x + RandomSpot, 1, transform.parent.transform.position.z + RandomSpot2);
            spell.enemyCastingspell = true;


            float RandomTime = Random.Range(0, 0.2f);
            FrostNumber--;
            Invoke("FrostAttackFunc", RandomTime);
        }
    }
    void BigBoySmash4Anim()
    {
        BigBoyGlow4.SetActive(false);
        ParticleSystem ps = BigBoyGlow4.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSize = 4f;
        Invoke("BigBoyAggro", 2f);
        Invoke("BBRoar2", 0.7f);
        // Reset of attack code here.
        MovementSpeed = 9;
        MovementSpeed_ = MovementSpeed;
        agent.speed = MovementSpeed;
        ChangeColor = true;
        flag = false;
        AttackDelay = 0.5f;
        slowedDur = 0; //removes any slow.
        Invoke("SlowDown", 6f);
        Invoke("SlowDown2", 9f);
    }
    void SlowDown()
    {
        flag = true;
        MovementSpeed = 6;
        MovementSpeed_ = MovementSpeed;
        agent.speed = MovementSpeed;
        AttackDelay = 0.75f;
        animChild.GetComponent<MonsterAnim>().anim.speed = 0.75f;
    }
    void SlowDown2()
    {
        MovementSpeed = 3;
        MovementSpeed_ = MovementSpeed;
        agent.speed = MovementSpeed;
        AttackDelay = AttackDelay_;
        animChild.GetComponent<MonsterAnim>().anim.speed = 0.5f;
        //  ChangeColor = false;
    }

    void BigBoySmash5Anim()
    {
        BigBoyGlow5.SetActive(false);
        ParticleSystem ps = BigBoyGlow5.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSize = 3;
        // BigBoyGlow5.GetComponent<ParticleSystem>().startSize = 3;
        BigBoyGlow5b.SetActive(false);
        ParticleSystem ps2 = BigBoyGlow5b.GetComponent<ParticleSystem>();
        var main2 = ps2.main;
        main.startSize = 3;
        //   BigBoyGlow5b.GetComponent<ParticleSystem>().startSize = 3;
        Invoke("BigBoyAggro", 4f);
        Invoke("BBRoar", 0.7f);
        // Reset of attack code here.
        GameObject BiGHelp = Instantiate(gameObject, HelpPos, HelpRot, gameObject.transform.parent);
        BiGHelp.GetComponent<Monster>().SummonHelp = true;
        BiGHelp.GetComponent<Monster>().Boss = false;
        BiGHelp.GetComponent<Monster>().health = health;
        BiGHelp.GetComponent<Monster>().health2 = health2;
        SummonHelp = true;
        BossHealthAct.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Big Boy & Big Boy";
        BiGHelp.GetComponent<Monster>().Brother = gameObject;
        Brother = BiGHelp;
    }

    void BBRoar()
    {
        animChild.GetComponent<MonsterAnim>().RoarAnim();
    }
    void BBRoar2()
    {
        animChild.GetComponent<MonsterAnim>().RoarAnim2();
    }
    public void BigBoyAttack()
    {
        if (BigBoyGlow.activeSelf == true)
        {
            ParticleSystem ps = BigBoyGlow.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startSizeMultiplier += Time.deltaTime / 2.5f;
        }

        if (BigBoyGlow2.activeSelf == true)
        {
            ParticleSystem ps = BigBoyGlow2.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startSizeMultiplier += Time.deltaTime / 50;
            //  BigBoyGlow2.GetComponent<ParticleSystem>().startSize += Time.deltaTime / 50;
        }
        if (BigBoyGlow3.activeSelf == true)
        {
            ParticleSystem ps = BigBoyGlow3.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startSizeMultiplier += Time.deltaTime / 1.5f;
            // BigBoyGlow3.GetComponent<ParticleSystem>().startSize += Time.deltaTime / 1.5f;
        }
        if (BigBoyGlow4.activeSelf == true)
        {
            ParticleSystem ps = BigBoyGlow4.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startSizeMultiplier += Time.deltaTime / 1.5f;
            // BigBoyGlow3.GetComponent<ParticleSystem>().startSize += Time.deltaTime / 1.5f;
        }
        if (BigBoyGlow5.activeSelf == true)
        {
            ParticleSystem ps = BigBoyGlow5.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startSizeMultiplier += Time.deltaTime / 1.5f;
            //     BigBoyGlow5.GetComponent<ParticleSystem>().startSize += Time.deltaTime / 1.5f;
        }
        if (BigBoyGlow5b.activeSelf == true)
        {
            ParticleSystem ps = BigBoyGlow5b.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startSizeMultiplier += Time.deltaTime / 1.5f;
            //    BigBoyGlow5b.GetComponent<ParticleSystem>().startSize += Time.deltaTime / 1.5f;
        }

        if (ChangeColor)
        {
            lerpedColor = Color.Lerp(colorIni, colorFin, t);
            _renderer.material.color = lerpedColor;

            if (flag == true && t >= 0.01f)
            {
                t -= Time.deltaTime / duration;
            }
            else if (t <= 0.99f)
            {
                t += Time.deltaTime / duration;
            }
        }
        if (BigBoySpecial1_ <= 0) //bigboi special attacks. Shared CD.
        {
            agent.isStopped = true;
            BBStill = true;
            RotateTowards(PC.transform);
            animChild.GetComponent<MonsterAnim>().AttackAnim();
            BigBoySpecial1_ = BigBoySpecial1;
            switch (BigBoyCurrentAttack)
            {
                case 0:
                    Invoke("BigBoySmash1Anim", AttackDelay);
                    break;
                case 1:
                    Invoke("BigBoySmash2Anim", AttackDelay);
                    break;
                case 2:
                    Invoke("BigBoySmash3Anim", AttackDelay);
                    break;
                case 3:
                    Invoke("BigBoySmash4Anim", AttackDelay);
                    break;
                case 4:
                    Invoke("BigBoySmash5Anim", AttackDelay);
                    break;
            }
            attackCountdown = AttackSpeed;
            hardCodeDansGame = attackAnimCD;
        }
    }
}
