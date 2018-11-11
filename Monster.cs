using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Monster : MonoBehaviour, IDamageable {

    public float health;
    public float damage;
    public float meleeRange;
    public bool StopMovingAfterAttacking;
    public float DisengageDistance = 1f;
    private float meleeRange_;
    private float CastingSpellTimer;
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
    public bool InCombat;
    [HideInInspector] public bool AttackFriend;
    private Vector3 destination;
    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    private GameObject PC;
    private GameObject PC2;
    private GameObject PC_;
    private GameObject Illu;
    [HideInInspector] public Vector3 startPosition;
    private bool canAttack;
    private float attackCountdown;
    public GameObject RoomIAmIn;
    private float callForHelpCD;
    private float RefreshNavMeshTargetPosition;
    private float hardCodeDansGame = 0;
    public float attackAnimCD; // how prevents other animations from overriding attack animation.

    [Header("HealthbarStuff")]
    public Image Healthbar;
    public GameObject HealthBarCanvas;
    public Text HBtext;
    private Quaternion HBtrack;
    [HideInInspector] public float health2;

    private Rigidbody rb;

    public GameObject animChild;
    private MonsterAnim anim;
    public int MonsterType;
    public int MonsterTypeSubLayer;
    public float PushResistance;
    public bool CurrentlyInBlackHole;
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
    [Header("Event Specific Interaction")]
    public bool EventSkeleton;
    public float EventSkeletonCD;
    public float EventSkeletonCD_;
    [Header("Loot")]
    public bool MonsterHasLoot;
    public bool MonsterCanDropGold;
    [HideInInspector] public GameObject MonsterLoot;
    [HideInInspector] public GameObject MonsterGold;
    public float GoldDropChance;
    public int GoldAmount;

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

    public GameObject Chest;
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
    private bool DisengageDistanceRemoveAfterAttack;
    public bool SineWaveAttack;
    public bool Cone;
    public GameObject FireTrail;
    public bool LeaveFireTrail;
    public float LeaveFireTrailCD;
    private float LeaveFireTrailCD_;
    public bool StartMenuFakeScene;

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
    public bool FrostTrail;
    public bool TimeKeeper;
    public bool OldKing;
    public bool TheBlob;
    public bool Order;
    public Vector3 LootLoc;
    public GameObject BossHealthAct;
    public Room BossRoom;

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
    private float BigBoyStepSoundDelay;

    [Header("Frosttrail")]
    public float FrostTrailAttack1;
    private float FrostTrailAttack1_;

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
    public float StartSceneAttackRange;
    public GameObject StartOpponent;
    public Monster SkeletonKing;
    public bool Defiling;
    public GameObject CurDefile;
    public List<GameObject> SkeletonList = new List<GameObject>();
    public List<GameObject> SkeletonListAlive = new List<GameObject>();
    public List<GameObject> AllDefiles = new List<GameObject>();

    [Header("Eye of Chaos || Eye of Order")]
    public GameObject BlobAttack1Object;
    public GameObject BlobAttack2Object;
    public float BlobAttackCD;
    public float BlobAttackCD_;
    public float BlobAttackCD2;
    public float BlobAttackCD_2;
    public float BlobAttackCD3;
    public float BlobAttackCD_3;
    public float BlobAttack1Duration;
    public bool BlobAttackNoTarget;
    public Light BossLight;
    public ParticleSystem BlobPS;
    public ParticleSystem BlobPS2;
    private float timeLeft;
    private Color targetColor;
    private int HealTimeCounter;
    public bool BlobAttack2Bool;
    [HideInInspector] public float Blob4RotSpeed;
    [HideInInspector] public bool Healboss;
    public List<GameObject> BlobCornerList;
    [HideInInspector] public bool BlobAttack4;
    [HideInInspector] public bool BlobDie;
    [HideInInspector] public float BlobDieTimer;
    public bool BlobWeapon;
    private bool BlobAttack2Phase2;
    private bool OrderPhase2Orb;
    List<GameObject> Orbs = new List<GameObject>();

    [Header("Dragon && StoneGolems")]
    public GameObject DeadGolemStonen;
    public bool StoneGolem;

    [Header("Sounds")]
    public AudioSource MeleeAttackStart;
    public float AttacStartDelay;
    public AudioSource MeleeAttackLand;

    public AudioSource BossSound1;
    public AudioSource BossSound2;
    public AudioSource BossSound3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (HealthBarCanvas != null)
        {
            HBtrack = Quaternion.Euler(-90, 180, 0);
        }

        if (animChild != null)
        {
            anim = animChild.GetComponent<MonsterAnim>();
        }
      
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
        BlobAttackCD_ = BlobAttackCD/2;
        BlobAttackCD_2 = 1; // change to 15

        if (Order)
        {
            BlobAttackCD_ = 1;
        }


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
            attackCountdown = AttackSpeed/2;
            if (StartScene)
            {
                attackCountdown = StartSceneAS;
            }
        }
        if (!IamIllu && !Brother)
        {
            if (Boss)
            {
                health *= MenuScript.BossHealthModifier;
            }

            health2 = health;
        }

        if (HealthBarCanvas != null)
        {
            if (health2 < 1)
            {
                HBtext.text = "(" + health.ToString("F1") + " / " + health2.ToString("F1") + ")";
            }
            else
            {
                HBtext.text = "(" + health.ToString("F0") + " / " + health2.ToString("F0") + ")";
            }
        }

        if (OldKing)
        {
            OldKingSpecialAttack_1 = OldKingSpecialAttack;
            health -= (health2/2);
            Healthbar.fillAmount = health / health2;
            StartOpponent.GetComponent<Monster>().health -= 11;
            StartOpponent.GetComponent<Monster>().Healthbar.fillAmount = StartOpponent.GetComponent<Monster>().health / StartOpponent.GetComponent<Monster>().health2;
            StartOpponent.GetComponent<Monster>().HBtext.text = "(" + StartOpponent.GetComponent<Monster>().health.ToString("F1") + " / " + StartOpponent.GetComponent<Monster>().health2.ToString("F0") + ")";
        }

        if (Boss) //Boss health bar
        {
            AddToRoomMonsterList(gameObject);

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
            agent.isStopped = true;
            AggroRange = 999f;
            attackCountdown = 9;
            tag = "Illusion";
            Invoke("TKFight", 8.5f);
            Invoke("TimeKeeperAlive", 2f);
            Invoke("TimeKeeperLaugh", 5f);

            anim.SpawnTK(-0.01f);
            MirrorImageCD_ += 6;
            Ratatatata_ += 6;
            TKSpawning = true;
        }

        if (BigBoy) // Starting anim for BigBoyBoss.
        {
            anim.Spawn();
            BossSound2.PlayDelayed(0.5f);
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
        if (MonsterType == 8)
        {
            Invoke("StoneGolemRandomHardCode", 0.1f);
        }
    }
    public void StoneGolemRandomHardCode()
    {
        animChild.SetActive(true);
    }

    public void StartFake()
    {
        AttackFriend = true;
        attackCountdown = Random.Range(1f, 2f);
        InvokeRepeating("AttackFriendFunc", 0.1f, 1);
    }

    public void StopFake()
    {
        AttackFriend = false;
        CancelInvoke("AttackFriendFunc");
        CancelInvoke("Attack");
        InCombat = false;
        agent.destination = transform.position;
        if (MonsterType == 1) { anim.IdleAnim(); }
        if (MonsterType == 2) { anim.IdleAnimation(); }
        if (MonsterType == 3) { anim.IdleAnimation2(); }
        if (MonsterType == 4) { anim.PlayerIdle(); } // Timekeeper
        if (MonsterType == 6) { anim.IdleAnimation3(); }
        if (MonsterType == 7) { anim.IdleAnimation3(); }
        if (MonsterType == 8) { anim.IdleAnimation4(); }
    }

  public  void AddToRoomMonsterList(GameObject Monster_)
    {
        if (BossRoom != null)
        {
            BossRoom.AddMonster(Monster_);
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

    void OrderAttack()
    {
        BlobAttackCD_ -= Time.deltaTime;
        BlobAttackCD_2 -= Time.deltaTime;
        BlobAttackCD_3 -= Time.deltaTime;
        if (BlobAttackCD_ < 0)
        {
            var RandAttack = Random.Range(0, 3);
            switch (RandAttack)
            {
                case 0:
                    OrderOne();
                    break;
                case 1:
                    OrderTwo();
                    break;
                case 2:
                    OrderThree();
                    break;
            }
            BlobAttackCD_2 += 6;
            BlobAttackCD_ = BlobAttackCD;
        }
        if (BlobAttackCD_3 < 0 && health <= health2 * 0.7f)
        {
            OrderPhase3();
            BlobAttackCD_3 = BlobAttackCD3;

        }

         if (BlobAttackCD_2 < 0 && health <= health2 * 0.35f)
        {
            OrderPhase2();
            BlobAttackCD_2 = BlobAttackCD2;
            BlobAttackCD_ += 8;
        }
    }

    void OrderPhase3()
    {
        float AttackDelay_ = 0;
        int BlobDirMod = 0;
        for (int i = 0; i < 18; i++)
        {
            StartCoroutine(Order1(gameObject, 5, new Vector3(0, 0, 0), BlobDirMod, 0.7f, 0.5f, AttackDelay_, Color.white));
            AttackDelay_ += 0.01f;
            BlobDirMod += 18;
        }
    }

    void OrderPhase2()
    {
        float AttackDelay_ = 0;
        int BlobDirMod = 90;
        bool side = true;
        for (int i = 0; i < 6; i++)
        { 
            StartCoroutine(Order1(gameObject, 4, new Vector3(0, 0, 0), BlobDirMod, 0.7f, 12, AttackDelay_, Color.black));
            AttackDelay_ += 0.8f;

        if (side)
        {
            BlobDirMod = -90;
                side = false;
        }
        else
        {
            BlobDirMod = 90;
                side = true;
        }
        }
    }



    void OrderOne()
    {
        float AttackDelay_ = 0;
        int BlobDirMod = 0;
        for (int i = 0; i < 12; i++)
        {
            StartCoroutine(Order1(gameObject, 1, new Vector3(0, 0, 0), BlobDirMod, 0.7f, 12, AttackDelay_, Color.yellow));
            AttackDelay_ += 0.2f;
        }
    }
    void OrderTwo()
    {
        float AttackDelay_ = 0;
        var RandAttack = Random.Range(0, 2);
        int BlobDirMod;
        bool side;
        if (RandAttack == 0)
        {
            BlobDirMod = -50;
            side = true;
        }
        else
        {
            BlobDirMod = 50;
            side = false;
        }

        for (int i = 0; i < 11; i++)
        {
            StartCoroutine(Order1(gameObject, 2, new Vector3(0, 0, 0), BlobDirMod, 0.7f, 9, AttackDelay_,Color.red));
            AttackDelay_ += 0.1f;
            if (side)
            {
                BlobDirMod += 10;
            }
            else
            {
                BlobDirMod -= 10;
            }
        }
    }
    void OrderThree()
    {
        float AttackDelay_ = 0;
        var RandAttack = Random.Range(0, 2);
        int BlobDirMod;
        bool side;
        if (RandAttack == 0)
        {
            side = false;
            BlobDirMod = -25;
        }
        else
        {
            side = true;
            BlobDirMod = 25;
        }
        for (int i = 0; i < 15; i++)
        {
            StartCoroutine(Order1(gameObject, 3, new Vector3(0, 0, 0), BlobDirMod, 0.7f, 9, AttackDelay_, Color.green));
            AttackDelay_ += 0.2f;

            if (BlobDirMod <= -25 && side == false)
            {
                side = true;
            }else if (BlobDirMod >= 25 && side == true) {
                side = false;
            }

            if (side == true)
            {
                BlobDirMod += 15;
            }
            else
            {
                BlobDirMod -= 15;
            }

        }
    }


    IEnumerator Order1(GameObject BlobDad, int AttackNumb, Vector3 BlobRot, int DirMod, float BHealth, float Bspeed, float delay, Color Bcolor)
    {
        yield return new WaitForSeconds(delay);
        OrderAttackFunction(BlobDad, AttackNumb, BlobRot, DirMod, BHealth, Bspeed, Bcolor);
    }

    void OrderAttackFunction(GameObject BlobDad, int AttackNumb, Vector3 BlobRot, int DirMod, float BHealth, float Bspeed, Color Bcolor)
    {
        GameObject CurBlob = Instantiate(BlobAttack1Object, transform.position, transform.rotation, transform);
        AddToRoomMonsterList(CurBlob);
        Monster CurBlob_ = CurBlob.GetComponent<Monster>();

        if (AttackNumb == 1 || AttackNumb == 2 || AttackNumb == 3 || AttackNumb == 4 || AttackNumb == 5)
        {
            CurBlob.transform.LookAt(PC.transform);
            CurBlob.transform.Rotate(Vector3.up * DirMod);
        }

            
        if (AttackNumb == 4) // phase 2 seeking blob.
        {
            CurBlob_.OrderPhase2Orb = true;
        }

        if (AttackNumb == 5)
        {
            var PushDistance = Random.Range(0, 5);
            CurBlob_.PushResistance = PushDistance;
            CurBlob_.pushDir = CurBlob_.transform.forward;
            CurBlob_.pushed = true;
        }

        Orbs.Add(CurBlob);

        CurBlob.transform.position += CurBlob.transform.forward * 3;
        CurBlob.transform.parent = transform.parent;

        ParticleSystem ps = CurBlob_.BlobPS;
        var main = ps.main;
        ParticleSystem ps2 = CurBlob_.BlobPS2;
        var main2 = ps2.main;

        main.startColor = Bcolor;
        main2.startColor = Bcolor;
        CurBlob_.health = BHealth;
        CurBlob_.health2 = BHealth;
        CurBlob_.MovementSpeed = Bspeed;
        CurBlob_.MovementSpeed_ = Bspeed;
        CurBlob_.BlobAttackNoTarget = true;
        CurBlob_.BlobDie = false;
    }

    public void OrbDieAfterBossIsDead()
    {
        var DieTimer = Random.Range(1f, 3f);
        Invoke("KillMonster", DieTimer);
    }

    void TheBlobAttack()
    {
        BlobAttackCD_ -= Time.deltaTime;
        BlobAttackCD_2 -= Time.deltaTime;
        if (BlobAttackCD_ < 0)
        {
            if (HealTimeCounter < 6)
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

        if (BlobAttackCD_2 < 0 && (health/health2) <= 0.7f)
        {
            GameObject CurBlob = Instantiate(BlobAttack2Object, transform.position, transform.rotation, transform);
            CurBlob.transform.position += CurBlob.transform.forward * 6;
            CurBlob.transform.parent = transform.parent;
            Monster CurBlob_ = CurBlob.GetComponent<Monster>();
            CurBlob_.BlobAttack2Bool = true;
            CurBlob_.BossRoom = BossRoom;
            ParticleSystem ps = CurBlob_.BlobPS;
            var main = ps.main;
            ParticleSystem ps2 = CurBlob_.BlobPS2;
            var main2 = ps2.main;
            main.startColor = BossLight.color;
            main2.startColor = BossLight.color;
            BlobAttackCD_2 = BlobAttackCD2;

            if (health/health2 <= 0.3f)
            {
                CurBlob_.BlobAttack2Phase2 = true;
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
        Blob1AttackFunction(BlobDad, heal, AttackNumb,BlobRot,DirMod, BHealth,Bspeed,lifeTime);
    }

    private void Blob1AttackFunction(GameObject BlobDad, bool heal, int AttackNumb, Vector3 BlobRot, int DirMod, float BHealth, float Bspeed, float lifeTime)
    {
        GameObject CurBlob = Instantiate(BlobAttack1Object, transform.position, transform.rotation, transform);
        AddToRoomMonsterList(CurBlob);
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
            CurBlob.transform.parent = transform.parent;
        }
        ParticleSystem ps2 = CurBlob_.BlobPS2;
        var main2 = ps2.main;
        main.startColor = BossLight.color;
        main2.startColor = BossLight.color;
        CurBlob_.health = BHealth;
        CurBlob_.health2 = BHealth;
        CurBlob_.Healboss = heal;
        CurBlob_.MovementSpeed = Bspeed;
        CurBlob_.MovementSpeed_ = Bspeed;
        CurBlob_.BlobAttackNoTarget = true;
        CurBlob_.BlobDie = false;
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
        anim.SpawnTK(-0.75f);
        BossSound1.Play();
    }

    void TimeKeeperLaugh()
    {
        BossSound2.Play();
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


    void FrostTrailAttack()
    {
        FrostTrailAttack1_ -= Time.deltaTime;
        if (FrostTrailAttack1_ < 0)
        {
            int AttackCount = 0;

            if (health <= (health2 / (4f/3f))){
                AttackCount = 1;
            }
            if (health <= (health2 / 2))
            {
                AttackCount = 2;
            }
            if (health <= (health2 / 4))
            {
                AttackCount = 3;
            }

            if (AttackCount > 0)
            {
                FrostTrailFrostMeter(AttackCount);
            }
            FrostTrailAttack1_ = FrostTrailAttack1;
        }
    }

    void FrostTrailFrostMeter(int Count)
    {
        GameObject test123 = Instantiate(currentspellObject, castPoint.transform.position, castPoint.transform.rotation, castPoint.transform);
        SpellProjectile spell = test123.GetComponent<SpellProjectile>();
        spell.projectilespeed = currentSpell.GetComponent<FrostBolt>().projectilespeed;
        spell.damage = currentSpell.GetComponent<FrostBolt>().damagePure;
        spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
        spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
        spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent;
        spell.aoeSizeMeteor = 3f;
        spell.BigBoyFrost = FrostAttack;
        float RandomSpot = Random.Range(-5f*Count, 5f * Count);
        float RandomSpot2 = Random.Range(-5 * Count, 5f * Count);
        //  spell.transform.localScale = new Vector3(2f, 1.5f, 1.5f);
        spell.spellCastLocation = new Vector3(PC.transform.position.x + RandomSpot, 1, PC.transform.position.z + RandomSpot2);
        spell.transform.position = new Vector3(PC.transform.position.x + RandomSpot, 1, PC.transform.position.z + RandomSpot2);
        spell.enemyCastingspell = true;
        Count--;

        PC_.GetComponent<Player>().SpellsCastInThisRoom.Add(test123);

        if (Count > 0)
        {
            FrostTrailFrostMeter(Count);
        }
        
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
        if (Order && InCombat)
        {
            OrderAttack();
        }

        if (FrostTrail && InCombat)
        {
            FrostTrailAttack();
        }

        //Checks if slowed | burning.
        AmISlowed();
        AmIBurning();

        if (!BlobAttackNoTarget)
        {
            if (StartScene)
            {
                PC = StartOpponent;
                meleeRange = StartSceneAttackRange;
            }
            else if (AttackFriend)
            {
                AttackFriendFunc();
            }
            else if (GameObject.FindWithTag("Illusion") != null)
            {
                PC2 = GameObject.FindWithTag("Illusion");
                float dist123a = Vector3.Distance(transform.position, PC_.transform.position);
                float dist123b = Vector3.Distance(transform.position, PC2.transform.position);

                if (dist123a < dist123b)
                {
                    PC = PC_;
                }
                else
                {
                    PC = PC2;
                }
            }
            else // not startscene and no illus
            {
                PC = PC_;
            }
        }
        
        if (LeaveFireTrail)
        {
            LeaveFireTrailFunc();
        }


        if (!CurrentlyRessing && !BlobAttackNoTarget && !TheBlob && PC != null)
        {
            float dist = Vector3.Distance(transform.position, PC.transform.position);
            if ((dist < AggroRange) && !SpiderBoss && (CastingSpellTimer < 0) && !canAttack)
            {
                InCombat = true;

                if (MonsterType != 5)
                {
                    if (agent.isOnNavMesh && ((Vector3.Distance(destination, PC.transform.position) > 1) || CurrentlyInBlackHole || RefreshNavMeshTargetPosition <0))
                    {
                        destination = PC.transform.position;
                        agent.destination = destination;
                        RefreshNavMeshTargetPosition = 0.25f;
                    }
                }

                if (MonsterType == 5)
                {
                    Vector3 dir = new Vector3(PC.transform.position.x, 3f, PC.transform.position.z) - this.transform.position;
                        float distThisFrame = MovementSpeed * Time.deltaTime;
                        transform.Translate(dir.normalized * distThisFrame, Space.World);
                        Quaternion targetRotation = Quaternion.LookRotation(dir);
                        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * MovementSpeed);

                    if (!CurrentlyFrozen && InCombat && !Boss)
                    {
                        MovementSpeed += Time.deltaTime / 6;
                        MovementSpeed_ += Time.deltaTime / 6;
                    }

                }
                if (MonsterType != 5)
                {
                    if (hardCodeDansGame <= 0 && agent.velocity.magnitude > 0.5f) // makes so the attack animation does not get canceled by run animation. && makes sure if they are not currently in attack animation and they are moving the running animation is active.
                    {
                        if (MonsterType == 1) { anim.RunAnim(); if (BigBoy && BigBoyStepSoundDelay < 0) { BossSound3.Play(); BigBoyStepSoundDelay = 3.1f / MovementSpeed; } }
                        if (MonsterType == 2) { anim.RunAnimation(); }
                        if (MonsterType == 3) { anim.RunAnimation2(); }
                        if (MonsterType == 4) { anim.PlayerIdle(); } // Timekeeper
                        if (MonsterType == 6) { anim.RunAnimation3(MonsterTypeSubLayer); }
                        if (MonsterType == 7) { anim.WalkAnim(); }
                        if (MonsterType == 8) { anim.RunAnim4(); }
                        BigBoyStepSoundDelay -= Time.deltaTime;
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

                if (Vector3.Distance(transform.position, agent.destination) < 2f)
                {
                    if (MonsterType == 1) { anim.IdleAnim(); }
                    if (MonsterType == 2) { anim.IdleAnimation(); }
                    if (MonsterType == 3) { anim.IdleAnimation2(); }
                    if (MonsterType == 4) { anim.PlayerIdle(); } // Timekeeper
                    if (MonsterType == 6) { anim.IdleAnimation3(); }
                    if (MonsterType == 7) { anim.IdleAnimation3(); }
                    if (MonsterType == 8) { anim.IdleAnimation4(); }
                }
            }
            if (SpiderBoss)
            {

              //  agent.destination = startPosition;
                if (!Swarm)
                {
                    anim.IdleAnimation2();
                }
                else
                {
                    anim.RunAnimation2();
                }
            }
            if (TimeKSpin) // Timekeeper special attack, chaning destination.
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 130f, Space.World);
                agent.destination = transform.position + (transform.forward * 4);
            }

            //Attack code
            if (((dist <= meleeRange + 0.5f) || (CastingSpellTimer > 0)) && dist <= AggroRange) { canAttack = true; }
            else if (dist > meleeRange + DisengageDistance) { canAttack = false; }

           if (dist <= meleeRange+0.5f && DisengageDistanceRemoveAfterAttack) { DisengageDistanceRemoveAfterAttack = false; }
           else if (DisengageDistanceRemoveAfterAttack) { canAttack = false; }

            if (canAttack && !BBStill && !DisengageDistanceRemoveAfterAttack)
            {
                if (!SpiderBoss && !TimeKSpin && !TKSpawning)
                {
                    RotateTowards(PC.transform); // if in melee range, rotate towards player }
                }
                //  agent.speed = 1.5f; // Have to change this or slow will be tricky. 
                if (attackCountdown <= 0f)
                {

                    if (MonsterType == 1) { anim.AttackAnim(); }
                    if (MonsterType == 2) { anim.AttackAnimation(); }
                    if (MonsterType == 3 && !SpiderBoss) { anim.AttackAnimation2(); }
                    if (MonsterType == 4) { anim.TimeKeeperAttack(); }
                    if (MonsterType == 6) { anim.AttackAnimation3(); }
                    if (MonsterType == 7) { anim.CastSpell(); }
                    if (MonsterType == 8) { anim.AttackAnimation4(); }

                    Invoke("Attack", AttackDelay);

                    if ((MonsterType == 1 || MonsterType == 3 || MonsterType == 6 || MonsterType == 8) && !SpiderBoss)
                    {
                        MeleeAttackStart.PlayDelayed(AttacStartDelay);
                    }

                    if (MonsterType == 1 || MonsterType == 3 || MonsterType == 5 || MonsterType == 6 || SpiderBoss || MonsterType == 8) // non casters
                    {
                        attackCountdown = AttackSpeed;
                    }
                    else
                    {
                        var RandomAS = Random.Range(AttackSpeed, CasterVariationAS);
                        attackCountdown = RandomAS;
                    }
                    hardCodeDansGame = attackAnimCD; //smthing with animation
                    if (StopMovingAfterAttacking)
                    {
                        CastingSpellTimer = attackAnimCD;
                    }
                } else if (MonsterType == 7 && hardCodeDansGame < 0)
                {
                    anim.WaitBetweenAttacks();
                }
                else if (MonsterType == 2 && hardCodeDansGame < 0)
                {
                    anim.IdleAnimation();
                }
            }
            else
            {
                if (MonsterType != 5)
                {
                    agent.speed = MovementSpeed;
                }
            }


            if (BlobAttack2Phase2 && attackCountdown < 0)
            {
                BlobBossAttack2Phase2Attack();
            }


            CastingSpellTimer -= Time.deltaTime;
            attackCountdown -= Time.deltaTime;
            hardCodeDansGame -= Time.deltaTime;
            MirrorImageCD_ -= Time.deltaTime;
            Ratatatata_ -= Time.deltaTime;
            BigBoySpecial1_ -= Time.deltaTime;
            SwarmCD_ -= Time.deltaTime;
            RefreshNavMeshTargetPosition -= Time.deltaTime;
            LeaveFireTrailCD_ -= Time.deltaTime;
            EventSkeletonCD_ -= Time.deltaTime;

            if (IlluHit)
            {
                TakeDamage(Time.deltaTime / 2);
            }

            if (StopMovingAfterAttacking)
            {
                if (canAttack && agent.isOnNavMesh)
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
                if (health >= 0)
                {
                    HBtext.text = "(" + health.ToString("F0") + " / " + health2.ToString("F0") + ")";
                }
            }
        }
    }

    void LeaveFireTrailFunc()
    {
        if (agent.velocity.magnitude > 0f && LeaveFireTrailCD_ < 0)
        {
            GameObject FireT = Instantiate(FireTrail);
            FireT.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            FireT.transform.parent = null;
            FireT.transform.rotation = FireTrail.transform.rotation;
            LeaveFireTrailCD_ = LeaveFireTrailCD;
            PC_.GetComponent<Player>().SpellsCastInThisRoom.Add(FireT);

        }   else if (agent.velocity.magnitude == 0f && LeaveFireTrailCD_ <0)
        {
            GameObject FireT = Instantiate(FireTrail);
            FireT.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            FireT.transform.parent = null;
            FireT.transform.rotation = FireTrail.transform.rotation;
            LeaveFireTrailCD_ = 4;
            PC_.GetComponent<Player>().SpellsCastInThisRoom.Add(FireT);
        }


        if (LeaveFireTrailCD_ > LeaveFireTrailCD && agent.velocity.magnitude > 0f)
        {
            LeaveFireTrailCD_ = 0f;
        }

    }

    public void NoLongerInBlackHole(float duration)
    {
        Invoke("BlackHoleBooltoFalse", duration);
    }

    void BlackHoleBooltoFalse()
    {
        CurrentlyInBlackHole = false;
    }

    public void ResetMonsterPosition()
    {
        agent.destination = startPosition;

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
            if (other.tag == "Player" && !BlobWeapon)
            {
                OnlyOnce = true;
                other.GetComponent<Player>().TakeDamage(damage);
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject, 0.99f);
                transform.GetChild(0).gameObject.transform.parent = null;

                Destroy(gameObject);
            }

            if (other.tag == "Illusion" && !BlobWeapon)
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
                    other.GetComponent<Monster>().TakeDamage(-health/2); // heals the remaning amount of health this mob has.
                }
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(transform.GetChild(0).gameObject, 0.99f);
                transform.GetChild(0).gameObject.transform.parent = null;
                Destroy(gameObject);
            }
            if (other.tag =="Monster" && BlobWeapon && !OnlyOnce)
            {
                OnlyOnce = true;
                other.GetComponent<Monster>().TakeDamage(damage);
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
        if (HealthBarCanvas != null)
        {
            HealthBarCanvas.transform.rotation = HBtrack;
        }
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

    void StartMovingAfterAttackLands()
    {
        StopMovingAfterAttacking = false;
        if (!CurrentlyFrozen && agent.isOnNavMesh && !CurrentlyRessing)
        {
            agent.isStopped = false;
        }
    }

    public void Attack()
    {
        if (MonsterType == 1 || MonsterType == 6 || MonsterType == 8 || (MonsterType == 3 && !SpiderBoss) && !CurrentlyRessing)
        {
            StopMovingAfterAttacking = true;
            CastingSpellTimer = hardCodeDansGame;
            Invoke("StartMovingAfterAttackLands", hardCodeDansGame);
            float dist = Vector3.Distance(transform.position, PC.transform.position);
            if (PC == PC2 && !StartScene)
            {
                PC.GetComponent<IlluScript>().TakeDamage(damage);
                MeleeAttackLand.Play();
            }
            else if (!StartScene && dist < meleeRange + 6 && tag != "Ressing" && !AttackFriend)
            {
                PC.GetComponent<Player>().TakeDamage(damage);
                MeleeAttackLand.Play();
            }
            else if (StartScene)
            {
                PC.GetComponent<Monster>().FakeFight(damage);
                MeleeAttackLand.Play();
            }
            else if (AttackFriend)
            {
                PC.GetComponent<Monster>().TakeDamage(damage);
                MeleeAttackLand.Play();
            }
        }

        if (MonsterType == 5 && !Boss)
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

            if (BlobAttack2Bool)
            {
                BlobExplodeOnDeath();
            }

            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(transform.GetChild(0).gameObject, 0.99f);
            transform.GetChild(0).gameObject.transform.parent = null;

            transform.GetChild(0).gameObject.SetActive(true); // twice cuz second child is now first.
            Destroy(transform.GetChild(0).gameObject, 0.99f);
            transform.GetChild(0).gameObject.transform.parent = null;

            Destroy(gameObject);
        }



        if (MonsterType == 2 || MonsterType == 4 || MonsterType == 7)
        {
            if (MonsterType == 4)
            {
                Invoke("TimeKeeperAttacks", 0.1f);
                var RandomSpell = Random.Range(0, 7);
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
                    case 3:
                        currentspellObject = currentspellObject3;
                        currentSpell = currentSpell3;
                        MonsterTypeSubLayer = 2;
                        break;
                    case 4:
                        currentspellObject = currentspellObject2;
                        currentSpell = currentSpell2;
                        MonsterTypeSubLayer = 1;
                        break;
                    case 5:
                        currentspellObject = currentspellObject3;
                        currentSpell = currentSpell3;
                        MonsterTypeSubLayer = 2;
                        break;
                    case 6:
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

                // makes it so the monster spellprojectiles vary a bit and don't always go in a predicatble pattern staright towards the player.
                if (PC == PC_) // If still, hits player.
                {
                    if (PC.GetComponent<Player>().agent.velocity.magnitude > 1)
                    {

                        float RandomDirection = Random.Range(-20, 20);
                        test123.transform.localRotation = Quaternion.Euler(test123.transform.localRotation.x, test123.transform.localRotation.y + RandomDirection, test123.transform.localRotation.z);
                    }
                }

                switch (MonsterTypeSubLayer)
                {
                    case 1:
                        spell.projectilespeed = currentSpell.GetComponent<FrostBolt>().projectilespeed;
                        spell.damage = currentSpell.GetComponent<FrostBolt>().damagePure;
                        spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
                        spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
                        spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent + 0.1f;
                        spell.SineWaveAttack = SineWaveAttack;

                        break;
                    case 2:
                        spell.projectilespeed = currentSpell.GetComponent<Fireball>().projectilespeed-2f;
                        spell.damage = currentSpell.GetComponent<Fireball>().damagePure - 0.5f;
                        spell.FireBallBurn = currentSpell.GetComponent<Fireball>().FireBallBurn;
                        spell.BurnDuration = currentSpell.GetComponent<Fireball>().BurnDuration;
                        spell.BurnPercent = currentSpell.GetComponent<Fireball>().BurnPercent;
                        spell.SineWaveAttack = SineWaveAttack;
                        spell.cone = Cone;
                        break;
                    case 3:
                        spell.projectilespeed = currentSpell.GetComponent<LightningBolt>().projectilespeed;
                        spell.damage = currentSpell.GetComponent<LightningBolt>().damagePure;
                        spell.LBBounce = currentSpell.GetComponent<LightningBolt>().LBBounce;
                        spell.LBBounceAmount = currentSpell.GetComponent<LightningBolt>().LBBounceAmount = 1;
                        break;
                }

                if (MonsterType == 2)
                {
                    DisengageDistanceRemoveAfterAttack = true;
                }

                if (MonsterType == 4)
                {
                    float RandomSpeed = Random.Range(15, 20);

                    if (MonsterTypeSubLayer == 3)
                    {
                        RandomSpeed = RandomSpeed / 1.5f;
                    }
                    spell.projectilespeed = RandomSpeed;
                }


                    spell.spellCastLocation = agent.destination;

              //  spell.spellCastLocation = PC.transform.forward * 30;

                if (!AttackFriend)
                {
                    spell.enemyCastingspell = true;
                }
            }           
        }
        if (SpiderBoss)
        {
            if (SwarmCD_ <= 0 && !Swarm)
            {
                Swarm = true;
                attackCountdown = 8f;
                SwarmCD_ = SwarmCD;

                Invoke("StartSwarm", 1.5f);
                Invoke("StopSwarm", 5f);
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

        AddToRoomMonsterList(Spider);
        Spider.GetComponent<Monster>().AggroRange = 50;
        Spider.GetComponent<Monster>().MovementSpeed = 3f;
        Spider.GetComponent<Monster>().damage = 0.5f;
        Spider.GetComponent<Monster>().health = 0.7f;
        Spider.GetComponent<Monster>().health2 = 0.7f;
        Spider.GetComponent<Monster>().MonsterTypeSubLayer = 2;
        Spider.GetComponent<Monster>().meleeRange = 2;
        Spider.transform.parent = GameObject.FindGameObjectWithTag("SpiderBossRoom").transform;
        Spider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Spider.GetComponent<Collider>().GetComponent<CapsuleCollider>().height = 10;
        Spider.GetComponent<UnityEngine.AI.NavMeshAgent>().radius = 1f;
        Spider.GetComponent<Monster>().HBtext.gameObject.SetActive(false);

        var RandomSpot = Random.Range(0, 3);
        var RandomSpot2 = Random.Range(0, 3);

        Spider.transform.localPosition = new Vector3(transform.localPosition.x + RandomSpot, transform.localPosition.y, transform.localPosition.z + RandomSpot2);

        if (count > 0)
        {
            //  SummonSwarm(count - 1);
            StartCoroutine(NextSpiderInSwarm(count -1, 0.2f));
        }
    }

    IEnumerator NextSpiderInSwarm(int spidercount, float delay)
    {
        yield return new WaitForSeconds(delay);
        SummonSwarm(spidercount);
    }

        public void IlluMaxTime()
    {
        TakeDamage(0.1f);
    }

    public void TimeKeeperAttacks()
    {
        SineWaveAttack = false;
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

                var RandSind = Random.Range(0, 2);
                if (RandSind == 0)
                {
                    SineWaveAttack = true;
                }
            }


            if (MirrorImageCD_ <= 0 && manag.Illus.Count == 0 && !TimeKSpin)
            {
                //float randomAS2 = Random.Range(3, 6);

                TimeKeeperLaugh();
                for (int i = 0; i < 5; i++)
                {
                    if (i != TeleLoc)
                    {
                        Vector3 Pos = TimeKeeperPoints[i].transform.position;
                        GameObject MMI = Instantiate(gameObject, Pos, transform.rotation);
                        AddToRoomMonsterList(MMI);
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
        if (float.IsNaN(vel))
        {
            vel = 1;
        }

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
        else if (Brother == null && !IamIllu && HBtext != null)
        {
            HBtext.text = "(" + health.ToString("F1") + " / " + health2.ToString("F0") + ")";
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
            if (!Boss)
            {
                Brother.GetComponent<Monster>().BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
            }
        }
        Healthbar.fillAmount = health / health2;

        if (Boss)
        {
            if (health > 0.1f)
            {
                BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
            } else if (health > 0)
            {
                BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = "0.1 / " + health2;
            }
        }
        else if (Brother == null && !IamIllu && tag == "Monster")
        {
            if (health > 0.1f)
            {
                HBtext.text = "(" + health.ToString("F1") + " / " + health2.ToString("F0") + ")";
            }
            else if (health > 0)
            {
                HBtext.text = "(0.1 / " + health2.ToString("F0") + ")";
            }
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

        if (Boss && (FireTrail || FrostTrail))
        {
            PC_.GetComponent<Player>().StartCoroutine(PC_.GetComponent<Player>().TeleportPlayerToStartAreaOnTrailBosses(1f, LootLoc, RoomIAmIn));

            PC_.GetComponent<Player>().RoomChangeDestroyPreviousRoomSpells();

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
        }

        if (slowedDur > 0 && CheckIfFrostBoosted)
        {
            GameObject Exp = Instantiate(FrostExplosion, transform.position, transform.rotation, transform);
            Exp.transform.parent = null;
            Exp.GetComponent<ExplodeScript>().BoostBurnDur = BoostSlowDur;
            Exp.GetComponent<ExplodeScript>().BoostBurnPer = BoostSlowPer;
            Exp.GetComponent<ExplodeScript>().FireTrueFrostFalse = false;
        }
        if (MonsterType == 1) { anim.DieAnim(); }
        if (MonsterType == 2) { anim.DieAnimation(); }

        if (MonsterType == 4) { anim.DieTK(); }
        if (MonsterType == 3)
        {
            anim.DieAnimation2();
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

            if (BlobAttack2Bool)
            {
                BlobExplodeOnDeath();               
            }
        }
        if (MonsterType == 6 && Resurrect && !Immortal) { anim.DieAnimation3(); }
        if (MonsterType == 6 && !Resurrect && !Immortal) { anim.DieAnimation4(); }
        if (MonsterType == 7) { anim.DieAnimation6(); }
        if (MonsterType == 8 && !StoneGolem) {
            anim.DieAnimation7();
            GameObject FireAt = Instantiate(FireAttack, transform.position, transform.rotation);
            FireAt.transform.parent = transform.parent;
            FireAt.transform.localPosition = transform.localPosition + transform.forward * 3;
            FireAt.transform.localRotation = transform.localRotation;
            FireAt.GetComponent<BigBoyFire>().duration = 1;
            FireAt.GetComponent<BigBoyFire>().StoneGolemFire = true;
            FireAt.GetComponent<BigBoyFire>().PoolNumb = 6;
            animChild.transform.parent = FireAt.transform;
        }
        if (StoneGolem) { 
            anim.StoneGolemTurnToStone();
            GameObject Rock = Instantiate(DeadGolemStonen, transform.position, DeadGolemStonen.transform.rotation);
            Rock.transform.parent = transform.parent;
            Rock.transform.localPosition = transform.localPosition + transform.forward * 1;
            Rock.transform.localRotation = transform.localRotation;
            animChild.transform.parent = Rock.transform;
        }

        if (IamIllu)
        {
            GameManager manag = GameObject.FindObjectOfType<GameManager>();
            manag.Illus.Remove(gameObject);
        }

        if (animChild != null && !Resurrect && !StoneGolem)
        {
            animChild.transform.parent = null;
        }

        SpiderBoss = false;
        if (!Resurrect)
        {
            Loot();

            if (Order)
            {
                foreach (var orb in Orbs)
                {
                    if (orb != null)
                    {
                        orb.GetComponent<Monster>().OrbDieAfterBossIsDead();
                    }
                }
            }
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
            if (!OldKing && SkeletonKing != null)
            {
                SkeletonKing.SkeletonList.Add(gameObject);
                SkeletonKing.SkeletonListAlive.Remove(gameObject);
            }
            //    Invoke("SkeletonStartDead", 0.05f);
            SkeletonStartDead();
        }

        Collider[] cols = Physics.OverlapSphere(transform.position, 17f); 
        foreach (Collider c in cols)
        {
            Monster e = c.GetComponent<Monster>();
            if (e != null && c.gameObject != gameObject)
            {
                e.FriendAttacked();
            }
        }
    }

    void BlobBossAttack2Phase2Attack()
    {
        GameObject CurBlob = Instantiate(BlobAttack1Object, transform.position, transform.rotation, transform);
        AddToRoomMonsterList(CurBlob);
        Monster CurBlob_ = CurBlob.GetComponent<Monster>();
        ParticleSystem ps = BlobPS;
        var main = ps.main;
        ParticleSystem ps3 = CurBlob_.BlobPS;
        var main3 = ps3.main;
        ParticleSystem ps4 = CurBlob_.BlobPS2;
        var main4 = ps4.main;
        main3.startColor = main.startColor;
        main4.startColor = main.startColor;
        CurBlob_.health = 2;
        CurBlob_.health2 = 2;
        CurBlob_.Healboss = false;
        CurBlob_.MovementSpeed = 8;
        CurBlob_.MovementSpeed_ = 8;
        CurBlob_.BlobAttackNoTarget = true;
        CurBlob_.BlobDie = false;
        CurBlob.transform.parent = null;
        attackCountdown = AttackSpeed;
    }

    void BlobExplodeOnDeath()
    {
        Vector3 RotateDir = transform.rotation.eulerAngles;
        float yAxis = RotateDir.y;
        for (int i = 0; i < 10; i++)
        {
            RotateDir = new Vector3(0, yAxis, 0);
            GameObject CurBlob = Instantiate(BlobAttack1Object, transform.position, transform.rotation, transform);
            AddToRoomMonsterList(CurBlob);
            Monster CurBlob_ = CurBlob.GetComponent<Monster>();
            CurBlob.transform.rotation = Quaternion.Euler(RotateDir);
            CurBlob.transform.position += CurBlob.transform.forward * 2;
            ParticleSystem ps = BlobPS;
            var main = ps.main;
            yAxis += 36;
            ParticleSystem ps3 = CurBlob_.BlobPS;
            var main3 = ps3.main;
            ParticleSystem ps4 = CurBlob_.BlobPS2;
            var main4 = ps4.main;
            main3.startColor = main.startColor;
            main4.startColor = main.startColor;
            CurBlob_.health = 2;
            CurBlob_.health2 = 2;
            CurBlob_.Healboss = false;
            CurBlob_.MovementSpeed = 8;
            CurBlob_.MovementSpeed_ = 8;
            CurBlob_.BlobAttackNoTarget = true;
            CurBlob_.BlobDie = false;
            CurBlob.transform.parent = null;
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
            Invoke("OldKingAttackEnd", ResTimer);
        }
        else
        {
            Invoke("KingResAnim", 2);
            Invoke("Resurected", ResTimer + 2);
            Invoke("OldKingAttackEnd", ResTimer+2);
        }
    }
    void KingResAnim()
    {
        anim.SkeletonRise();
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


        if (BlobAttackNoTarget && !OrderPhase2Orb)
        {
            float distThisFrame = MovementSpeed * Time.deltaTime;
            if (BlobAttack4)
            {
               // var rotation_ = (Vector3.up * Time.deltaTime * Blob4RotSpeed);
                 transform.Rotate(Vector3.up * Time.deltaTime * Blob4RotSpeed);
               // rb.rotation = Quaternion.Euler(rotation_); // currently not working, but would be better to use rigidbody for transform.
            }
            rb.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            rb.position += transform.forward * distThisFrame;
        }
        if (OrderPhase2Orb)
        {
            Vector3 dir = PC.transform.position - this.transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * MovementSpeed / 8);

            rb.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            rb.position += transform.forward * Time.deltaTime * MovementSpeed;

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
          //  meleeRange = 0f;
            if (MonsterType != 5 && agent.isOnNavMesh)
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                }
            }
            CancelInvoke("StartAgent");
            Invoke("StartAgent", 2f); // HARDCODED

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
           // meleeRange = meleeRange_;
            if (MonsterType != 5)
            {
                agent.isStopped = false;
            }
        }
    }

    public void FriendAttacked()
    {
        if (!StoneGolem)
        {
            AggroRange = 999;
        }
    }

    public void Slow(bool slow, float dur, float str) //Could slow attackspeed aswell, though might take a bit of work to slow down animations aswell.
    {
        CurrentSlowSTR = str;

        if (slow && MovementSpeed >= MovementSpeed_ && !CurrentlyRessing) //slow is true and currently not slowed. How to work if enemy has a sprint effect? to be seen.
        {


            MovementSpeed /= str;

            if (MonsterType != 5 && agent != null)
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
                spell.pool = false;
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
        if (Boss) // or any other boss
        {
            var RandomLoot = Random.Range(0, 2);
            switch (RandomLoot)
            {
                case 0:
                        GameObject BossLoot = Instantiate(Chest, transform.position, Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z), BossRoom.transform);
                        BossLoot.transform.localPosition = LootLoc;
                        BossLoot.GetComponent<AmazingChestHead>().CurrentLoot = BossWeapon;
                    break;
                case 1:
                        GameObject BossLoot2 = Instantiate(Chest, transform.position, Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z), BossRoom.transform);
                        BossLoot2.transform.localPosition = LootLoc;
                        BossLoot2.GetComponent<AmazingChestHead>().CurrentLoot = BossArmor;
                    break;
            }
        }
        else if (MonsterHasLoot)
        {
           GameObject CurLoot = Instantiate(MonsterLoot, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = RoomIAmIn.transform; 
        }
        if (MonsterCanDropGold)
        {
            var DropChance = Random.Range(0, 100);
            if (DropChance < GoldDropChance)
            {
                GameObject CurGold = Instantiate(MonsterGold, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
                CurGold.transform.parent = RoomIAmIn.transform;
                CurGold.GetComponent<GoldPickUpScript>().GoldAmount = GoldAmount;
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
            anim.StartDeadAnim();
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if (!OldKing)
        {
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            anim.DieAnimation5();
        }
        else if (OldKing)
        {
            anim.DieAnimation5();
            OldKingSpecialAttack_1 = 6f;
            Invoke("LongLiveTheKing", 2.5f);
            BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
        }

        if (EventSkeleton) //SKELETONEVENT
        {
            Invoke("StartEventRes", 2f);
        }
    }
    void StartEventRes()
    {
        RoomIAmIn.GetComponent<Room>().SkeletonSeeIfFriendAlive(this);
    }
    

    public void ResEventSkeleton(Monster ResTarget)
    {

        transform.LookAt(ResTarget.gameObject.transform);
        StartCoroutine(SpawnMinion(ResTarget.gameObject, 1f, 0));
        GameObject Visual = Instantiate(Skill1Projectile, transform);
        Visual.transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        Visual.GetComponent<OldKingProjectile>().Skeleton = ResTarget.gameObject;
        float dist = Vector3.Distance(transform.position, ResTarget.gameObject.transform.position);
        Visual.GetComponent<OldKingProjectile>().distance = dist;
        Visual.transform.parent = null;
        OldKingAttackEnd();

        agent.isStopped = true;
        BBStill = true; // lets see.. Remember to turn off.
        CancelInvoke("StartMovingAfterAttackLands");
        CancelInvoke("OldKingAttackEnd");
        attackCountdown = AttackSpeed;
        hardCodeDansGame = attackAnimCD;
        anim.OldKingSpecialAttack1();
        Invoke("OldKingAttackEnd", 3.89f);
        EventSkeletonCD_ = EventSkeletonCD;

    }


    public void LongLiveTheKing()
    {
        if (SkeletonListAlive.Count > 0)
        {
            StartRessing();
            hardCodeDansGame = 0;

            for (int i = SkeletonListAlive.Count-1; i >= 0; i--)
            {
                var Skelly = SkeletonListAlive[i];
                GameObject Heal = Instantiate(Skill1Projectile, Skelly.transform);
                Heal.transform.position = new Vector3(Skelly.transform.position.x, 3, Skelly.transform.position.z);
                Heal.GetComponent<OldKingProjectile>().Skeleton = Skelly.GetComponent<Monster>().StartOpponent;
                Heal.GetComponent<OldKingProjectile>().CurHealth = Skelly.GetComponent<Monster>().health;
                Heal.GetComponent<OldKingProjectile>().distance = 10f;
                Heal.GetComponent<OldKingProjectile>().HealKing = true;
                Skelly.GetComponent<Monster>().RegenLife = false;
                Skelly.GetComponent<Monster>().health = 0;
                Skelly.GetComponent<Monster>().CancelInvoke("Resurected");
                Skelly.GetComponent<Monster>().KillMonster();
            }
        }
        else
        {
            BossHealthAct.transform.GetChild(0).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(1).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(2).gameObject.SetActive(false);
            BossHealthAct.transform.GetChild(3).gameObject.SetActive(false);

            for (int i = AllDefiles.Count - 1; i >= 0; i--)
            {
                Destroy(AllDefiles[i].gameObject);
            }

            Loot();
            RoomIAmIn.GetComponent<Room>().RemoveMonster(gameObject);
        //    transform.parent.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); // hard code to open door after skellyboss
        }
    }

    void OldKingAttack()
    {
        OldKingSpecialAttack_1 -= Time.deltaTime;
        if (OldKingSpecialAttack_1 <= 0)
        {
            agent.isStopped = true;
            BBStill = true; // lets see.. Remember to turn off.
            CancelInvoke("StartMovingAfterAttackLands");
            var RandomSpecialAttack = Random.Range(0, 5);
            switch (RandomSpecialAttack)
            {
                case 0:
                    if (SkeletonList.Count > 0)
                    {
                        OldKingAttack1(1, 0);
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
                        OldKingAttack1(3, -2 / 3f);
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
            BossSound1.PlayDelayed(0.8f);
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

        anim.OldKingSpecialAttack1();
        Invoke("OldKingAttackEnd", 3.89f);
    }
    void OldKingAttack2()
    {
        var RandomMinion = Random.Range(0, SkeletonListAlive.Count);
        transform.LookAt(SkeletonListAlive[RandomMinion].gameObject.transform);
        StartCoroutine(SacrificeMinion(SkeletonListAlive[RandomMinion], 1f));
        anim.OldKingSpecialAttack1();
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
        anim.OldKingSpecialAttack1();
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
        Skel.anim.SkeletonRise();
        Skel.health = ((Skel.health2 * HP));

        Skel.Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
        Skel.HBtext.text = "(0 / " + Skel.health2.ToString("F0") + ")";
        if (!EventSkeleton)
        {
            Skel.SkeletonKing = gameObject.GetComponent<Monster>();
        }

        Skel.agent.radius = 1f;

        if (Skel.Defiling)
        {
            AllDefiles.Remove(Skel.CurDefile);
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
        Defi.transform.parent = RoomIAmIn.transform;
        Skel.Defiling = true;
        Skel.CurDefile = Defi;
        AllDefiles.Add(Defi);
    }

    void OldKingAttackEnd()
    {
        BBStill = false;
        if (!CurrentlyFrozen && agent.isOnNavMesh && !CurrentlyRessing)
        {
            agent.isStopped = false;
        }
    }

    // Most bigboy code below
    void BigBoyAggro() // start/after special attack resetting to normal mode.
    {
        if (!SummonHelp && (health <= (health2/2)))
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
            Monster SmallHelp_ = SmallHelp.GetComponent<Monster>();
            AddToRoomMonsterList(SmallHelp);
            SmallHelp_.AggroRange = 50;
            SmallHelp_.MovementSpeed = 5f;
            SmallHelp_.damage = 1f;
            SmallHelp_.health = 3;
            SmallHelp_.health2 = 3;

            float RandomSpot = Random.Range(0, 5);
            float RandomSpot2 = Random.Range(0, 5);
            SmallHelp.transform.parent = transform.parent;
            SmallHelp.transform.position = new Vector3(SmallHelp.transform.position.x + RandomSpot, SmallHelp.transform.position.y, SmallHelp.transform.position.z + RandomSpot2);
            SmallHelp.transform.localScale = new Vector3(1, 1, 1);
            SmallHelp_.animChild.GetComponent<MonsterAnim>().Spawn(); // FIX ME!
            SmallHelp.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            SmallHelp_.BBStill = true;
            SmallHelp_.Invoke("SmallSpawnAnimStop", 3f);
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
            spell.damage = currentSpell.GetComponent<FrostBolt>().damagePure;
            spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
            spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
            spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent + 0.1f;
            spell.aoeSizeMeteor = 3f;
            spell.ActualPlayer = PC_.GetComponent<Player>();
            spell.BigBoyFrost = FrostAttack;
            float RandomSpot = Random.Range(-36f, 36f);
            float RandomSpot2 = Random.Range(-22, 18.5f);
            //  spell.transform.localScale = new Vector3(2f, 1.5f, 1.5f);
            spell.spellCastLocation = new Vector3(transform.parent.transform.position.x + RandomSpot, 1, transform.parent.transform.position.z + RandomSpot2);
            spell.transform.position = new Vector3(transform.parent.transform.position.x + RandomSpot, 1, transform.parent.transform.position.z + RandomSpot2);
            spell.enemyCastingspell = true;

            float RandomTime = Random.Range(0.1f, 0.2f);
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
        hardCodeDansGame = 0f;
    }
    void SlowDown()
    {
        flag = true;
        MovementSpeed = 6;
        MovementSpeed_ = MovementSpeed;
        agent.speed = MovementSpeed;
        AttackDelay = 0.75f;
        anim.anim.speed = 0.75f;
    }
    void SlowDown2()
    {
        MovementSpeed = 3;
        MovementSpeed_ = MovementSpeed;
        agent.speed = MovementSpeed;
        AttackDelay = AttackDelay_;
        anim.anim.speed = 0.5f;
        ChangeColor = false; // Why was this commented out?
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
        BiGHelp.GetComponent<Monster>().ChangeColor = false;
        BiGHelp.GetComponent<Monster>().health = health;
        BiGHelp.GetComponent<Monster>().health2 = health2;
        SummonHelp = true;
        BossHealthAct.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Big Boy & Big Boy";
        BiGHelp.GetComponent<Monster>().Brother = gameObject;
        Brother = BiGHelp;
    }

    void BBRoar()
    {
        anim.RoarAnim();
        BossSound1.Play();
    }
    void BBRoar2()
    {
        anim.RoarAnim2();
        BossSound1.Play();
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
            anim.AttackAnim();
            BigBoySpecial1_ = BigBoySpecial1;
            CancelInvoke("StartMovingAfterAttackLands");
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
            MeleeAttackLand.PlayDelayed(AttackDelay-0.2f);
            attackCountdown = AttackSpeed;
            BigBoyStepSoundDelay = 0;
            hardCodeDansGame = attackAnimCD;
        }
    }
}
