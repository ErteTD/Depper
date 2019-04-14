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
    public float slowedDur;
    private float StartHeight;
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
    private bool DontAttackJustMove;

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
    public float BurnDur;
    public float TotalBurnDamage;
    private bool noBounce;
    private float ChannelTimer = 0.5f;
    public float BounceDistance = 1.5f;
    private bool CheckIfBurnBoosted;
    private bool CheckIfFrostBoosted;
    private bool OnlyOnce = false;
    public bool CurrentlyFrozen;
    public bool MonsterIsSlowed;
    public bool MonsterIsBurning;
    [Header("Event Specific Interaction")]
    public bool EventSkeleton;
    public float EventSkeletonCD;
    public float EventSkeletonCD_;
    [Header("Loot")]
    public bool MonsterHasLoot;
    public bool MonsterCanDropGold;
    public GameObject MonsterLoot;
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
    [HideInInspector]
    public bool ShowDecimalHealth;
    [HideInInspector]
    public string MaxHealthAsString;
    public bool MiniBossMonster;

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
    public bool FireAndFrostDisabled;
    //public GameObject BlackHole;

    [Header("Bosses")]
    public string BossName;
    public bool Boss;
    public bool SpiderBoss;
    public bool BigBoy;
    public bool FireTrailBoss;
    public bool FrostTrailBoss;
    public bool Illusionist;
    public bool TimeKeeper;
    public bool OldKing;
    public bool TheBlob;
    public bool Order;
    public bool MageBoss;
    public bool Golemboss;
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
    private float IlluDamageAmount;
    public ParticleSystem OuterRing;
    //Lvl2 boss
    public GameObject Platforms;
    private float ASD;
    public Transform ParentPlatform;
    private float ClockAttackCD_;
    private int ClockPlatCount;
    public GameObject TimeOrbFrost;
    public GameObject TimeOrbFire;
    public float TimeOrbCD;
    private float TimeOrbCD_;
    public ParticleSystem WaveColor;
    public Color colorRed;
    public Color colorBlue;
    private bool ChangeCol;
    public float ChangeColorTimer;
    private float ChangeColorTimer_;
    public float TimeRandomOrbAttack;
    private float TimeRandomOrbAttack_;
    private bool TimeRandomOrbBool;
    private float TImeRandomGottaGoFast;
    private bool TimeStartBlastingBool;
    private int TimeKeeperCounter;
    private float MonsterSpellDamageModifier = 1;

    [Header("BigBoy")]
    public GameObject BigBoyGlow;
    public GameObject BigBoyGlow2;
    public GameObject BigBoyGlow3;
    public GameObject BigBoyGlow4;
    public GameObject BigBoyGlow5;
    public GameObject BigBoyGlow5b;
    public GameObject Smallboy;
    public bool SmallBoyBool;
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
    private float BurnTickRate;

    [Header("Frosttrail")]
    public float FrostTrailAttack1;
    private float FrostTrailAttack1_;
    public GameObject Door1;
    public GameObject Door2;

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

    [Header("Mage && StoneGolems")]
    public GameObject ZapAttack;
    public float MageAttackCD;
    private float MageAttackCD_;
    public List<GameObject> Rocks;
    public GameObject NewStoneGolem;
    public GameObject DeadGolemStonen;
    public GameObject ZapExplosion;
    public GameObject FrostBarrage;
    public GameObject BigRedCircle;
    public GameObject BigRedMeteor;
    public GameObject DieNuke;
    public bool GolemOrbAim;
    public Monster MageBossForGolems;
    public bool StoneGolem;
    public Vector3 MageStart;
    private bool BossCastingSpell;
    private float FrostBarrageTurnRateVariation;
    List<int> MageBossSkillOrder = new List<int>();
    private int Zaps;
    public GameObject MageBossFire1;
    public GameObject MageBossFire2;

    [Header("GolemBoss")]
    //Charge attack (small load, then runs in a straight line untill it hits the wall, stunned for a few seconds.
    //Rock Toss, tosses a rock towards player/smthing, aoe and then spawns obstacles
    //Boss can walk over obstacles and destory them.
    public GameObject GolemBRock;
    public GameObject GolemRockTossObject;
    public GameObject BossCopyObj;
    public float GolemRockTossCD;
    private float GolemRockTossCD_;
    private bool TossingRock;
    public bool BossCopy;
    public int NumberOfBlocks;
    public int BossLayer;
    public int NumberOfGolems;
    public Color RockColor;
    //   List<GameObject> Golems = new List<GameObject>();

    [Header("Sounds")]
    public AudioSource MeleeAttackStart;
    public float AttacStartDelay;
    public AudioSource MeleeAttackLand;
    public AudioSource SpawnMinionSound;
    public AudioSource BossSound1;
    public AudioSource BossSound2;
    public AudioSource BossSound3;

    GameManager manag;
    private bool StartAgentBool;

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

        MirrorImageCD_ = 30;
        TimeRandomOrbAttack_ = 45f;
        Ratatatata_ = 15f;

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
        if (MonsterType != 5 && !Boss && !BigBoy && !SmallBoyBool)
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.angularSpeed = turnRate;
            agent.speed = MovementSpeed;
            agent.stoppingDistance = meleeRange;
            StartAgentBool = true;
        }else if(MonsterType != 5 && Boss || BigBoy || SmallBoyBool)
        {
            Invoke("StartAgentMonster", 0.05f);
        }else if (MonsterType == 5)
        {
            StartAgentBool = true;
        }

        if (!SpiderBoss && !IamIllu)
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
            if (health2 < 1 || ShowDecimalHealth)
            {
                MaxHealthAsString = health2.ToString("F1");
                HBtext.text = "(" + health.ToString("F1") + " / " + MaxHealthAsString + ")";
            }
            else
            {
                MaxHealthAsString = health2.ToString("F0");
                HBtext.text = "(" + health.ToString("F0") + " / " + MaxHealthAsString + ")";
            }
        }

        if (OldKing)
        {
            OldKingSpecialAttack_1 = OldKingSpecialAttack;
            health -= (health2/(5.0f/3.0f));
            Healthbar.fillAmount = health / health2;
            StartOpponent.GetComponent<Monster>().health -= 11;
            StartOpponent.GetComponent<Monster>().Healthbar.fillAmount = StartOpponent.GetComponent<Monster>().health / StartOpponent.GetComponent<Monster>().health2;
            StartOpponent.GetComponent<Monster>().HBtext.text = "(" + StartOpponent.GetComponent<Monster>().health.ToString("F1") + " / " + StartOpponent.GetComponent<Monster>().health2.ToString("F0") + ")";

        }

        if (Golemboss)
        {
            if (!BossCopy)
            {
                Invoke("StoneGolemHardCode2", 0.1f);
            }
            BossRoom.AddMonster(gameObject);
            float RandCd = Random.Range(-6, 0);
            GolemRockTossCD_ = GolemRockTossCD + RandCd;
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
            BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
            MageStart = transform.position;
            MageAttackCD_ = 5f;

            if (FrostTrailBoss || FireTrailBoss)
            {
                Invoke("FriendAttacked", 10f);
            }

        }
        if ((Illusionist || TimeKeeper) && !IamIllu)
        {
            TimeKeeperCounter = 2;
            TimeOrbCD_ = TimeOrbCD/2;
            AggroRange = 999f;
            attackCountdown = 9;
            tag = "Illusion";
            Invoke("TKFight", 8.5f);
            Invoke("TimeKeeperAlive", 2f);
            Invoke("TimeKeeperLaugh", 5f);

            anim.SpawnTK(-0.01f);
            MirrorImageCD_ += 6;
            Ratatatata_ += 6;
            TimeRandomOrbAttack_ += 6;
            TKSpawning = true;

            if (TimeKeeper)
            {
                Platforms.transform.parent = null;
                ChangeColorTimer_ = ChangeColorTimer;
            }
        }
        manag = GameObject.FindObjectOfType<GameManager>();

        if (BigBoy) // Starting anim for BigBoyBoss.
        {
            anim.Spawn();
            if (BigBoy)
            {
                BossSound2.PlayDelayed(0.5f);
                AggroRange = 40f;
                Invoke("BigBoyAggro", 3.6f);
                BigBoySpecial1_ = BigBoySpecial1;
            }
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
        if (MageBoss)
        {
            Invoke("StoneGolemHardCode", 0.1f);
        }

        if (MonsterType == 5)
        {
            StartHeight = transform.localPosition.y;
        }
    }
    

    void StartAgentMonster()
    {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.angularSpeed = turnRate;
            agent.speed = MovementSpeed;
            agent.stoppingDistance = meleeRange;
            agent.enabled = true;
            StartAgentBool = true;
        if ((Illusionist || TimeKeeper || BigBoy || SmallBoyBool) && !IamIllu)
        {
            agent.isStopped = true;
            if (SmallBoyBool)
            {
                anim.Spawn();
                BBStill = true;
            }
        }

    }

    void StoneGolemHardCode()
    {
        foreach (var item in Rocks)
        {
            item.SetActive(true);
        }
    }
    void StoneGolemHardCode2()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject GRock = Instantiate(GolemBRock, transform);
            GRock.transform.localPosition = Random.insideUnitSphere * 30;
            GRock.transform.localPosition = new Vector3(GRock.transform.localPosition.x, 0f, GRock.transform.localPosition.z);
            float x = Random.Range(0, 365);
            GRock.transform.localRotation = Quaternion.Euler(new Vector3(0, x, 0));
            GRock.transform.parent = transform.parent;
         //   MonsterAnim asdasd2 = GRock.gameObject.transform.GetChild(0).gameObject.GetComponent<MonsterAnim>();
         //   asdasd2.StoneGolemStartDead();

        }
    }
    public void StoneGolemRandomHardCode()
    {
        animChild.SetActive(true);
        if (StoneGolem)
        {
            agent.isStopped = true;
            BBStill = true;
            Invoke("HD2", 0.1f);
            Invoke("SmallSpawnAnimStop", 4f);
        }
    }
    void HD2()
    {
        anim.SpawnGolem();
        SpawnMinionSound.Play();
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
        if (MonsterType == 8 && animChild.activeSelf) { anim.IdleAnimation4(); }
    }

  public  void AddToRoomMonsterList(GameObject Monster_)
    {
        if (BossRoom != null)
        {
            BossRoom.AddMonster(Monster_);
        }
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

    void BossStopCastingSpellAnim()
    {
        BossCastingSpell = false;
    }



    void MageBossAttack()
    {
        MageAttackCD_ -= Time.deltaTime;
        if (MageAttackCD_ < 0)
        {

            if (MageBossSkillOrder.Count == 0)
            {
                MageBossSkillOrder.Add(0);
                MageBossSkillOrder.Add(1);
                MageBossSkillOrder.Add(2);
            }

            var RandAttack = Random.Range(0, MageBossSkillOrder.Count);

            BossCastingSpell = true;
            switch (MageBossSkillOrder[RandAttack])
            {
                case 0:
                    if (Rocks.Count > 0)
                    {
                        Zaps = 2;
                        MageBossSkill1();                    
                    }
                    else
                    {
                        MageBigBadBomb();
                    }
                    break;
                case 1:
                    MageSplitCastWithAim();
                    break;
                case 2:
                    MageBigBadBomb();
                    break;
            }
            MageBossSkillOrder.RemoveAt(RandAttack);

            MageAttackCD_ = MageAttackCD;
        }
    }

    void MageBossSkill1()
    {
        Invoke("BossStopCastingSpellAnim", 2.5f);
        var RandomRock = Random.Range(0, Rocks.Count);
        RotateTowards(Rocks[RandomRock].transform);
        StartCoroutine(ZapRock(Rocks[RandomRock], 1.5f));
        destination = transform.position;
        BBStill = true;
        agent.isStopped = true;
        anim.AttackAnimation5();
    }

    IEnumerator ZapRock(GameObject Rock, float delay)
    {
        yield return new WaitForSeconds(delay);
        RotateTowards(Rock.transform);
        GameObject Zap = Instantiate(ZapAttack, castPoint.transform.position, transform.rotation);
        Zap.transform.parent = null;
        var ZapDistance = Vector3.Distance(castPoint.transform.position, Rock.transform.position);
        ParticleSystemRenderer ZapParticle = Zap.GetComponent<ParticleSystemRenderer>();
        ZapParticle.lengthScale = (ZapDistance / 5);
        Rocks.Remove(Rock);
        Destroy(Zap, 0.6f);
        Destroy(Rock, 3.5f);
        StartCoroutine(CrumbleRock(Rock, 1f, 2));
        Instantiate(ZapExplosion, new Vector3(Rock.transform.position.x, Rock.transform.position.y + 3, Rock.transform.position.z), ZapExplosion.transform.rotation);

        BBStill = false;
        if (!CurrentlyFrozen)
        {
            agent.isStopped = false;
        }

        if ((health / health2) <= 0.3f && Rocks.Count > 0 && Zaps == 1)
        {
            Zaps--;
            CancelInvoke("BossStopCastingSpellAnim");
            Invoke("MageBossSkill1", 1.5f);
        }
        if ((health / health2) <= 0.7f && Rocks.Count > 0 && Zaps == 2)
        {
            Zaps--;
            CancelInvoke("BossStopCastingSpellAnim");
            Invoke("MageBossSkill1", 1.5f);
        }

    }
    IEnumerator CrumbleRock(GameObject Rock, float delay, int RockNumb)
    {
        yield return new WaitForSeconds(delay);
        Rock.transform.GetChild(0).gameObject.GetComponent<MonsterAnim>().StoneGolemCrumble();
        for (int i = 0; i < RockNumb; i++)
        {
            float RandX = Random.Range(-5f, 6f);
            float RandZ = Random.Range(-5f, 6f);
            GameObject NewGolem = Instantiate(NewStoneGolem, new Vector3(Rock.transform.position.x + RandX, 0, Rock.transform.position.z + RandZ), transform.rotation);
            Monster Golem = NewGolem.GetComponent<Monster>();
            NewGolem.transform.parent = transform.parent;
            Golem.MageBossForGolems = GetComponent<Monster>();
            Golem.MovementSpeed = Random.Range(2f, 4f);
            Golem.MovementSpeed_ = Golem.MovementSpeed;
        }
    }

    void MageSplitCastWithAim()
    {
        BBStill = true;
        agent.isStopped = true;
        anim.AttackAnimation5();

        if ((health / health2) > 0.7f)
        {
            StartCoroutine(MageBarrage(1.55f, 4, 80));
            StartCoroutine(MageBarrage(1.55f, 4, -80));
        }

        if ((health / health2) <= 0.7f && (health / health2) > 0.3f)
        {
            StartCoroutine(MageBarrage(1.55f, 4, 0));
            StartCoroutine(MageBarrage(1.55f, 4, 80));
            StartCoroutine(MageBarrage(1.55f, 4, -80));
        }
        if ((health / health2) <= 0.3f)
        {
            StartCoroutine(MageBarrage(1.55f, 4, -60));
            StartCoroutine(MageBarrage(1.55f, 4, 60));
            StartCoroutine(MageBarrage(1.55f, 4, 0));
            StartCoroutine(MageBarrage(1.55f, 4, 120));
            StartCoroutine(MageBarrage(1.55f, 4, -120));
        }

    }
    IEnumerator MageBarrage(float delay, int AttackCount, int DirMod)
    {
        yield return new WaitForSeconds(delay);
        GameObject CurBlob = Instantiate(FrostBarrage, castPoint.transform.position, transform.rotation, transform);   
        AddToRoomMonsterList(CurBlob);
        Monster CurBlob_ = CurBlob.GetComponent<Monster>();
        CurBlob.transform.LookAt(PC.transform);
        CurBlob.transform.Rotate(Vector3.up * DirMod);
        CurBlob_.OrderPhase2Orb = true;
        CurBlob.transform.position += CurBlob.transform.forward;
        CurBlob.transform.parent = transform.parent;
        CurBlob_.BlobAttackNoTarget = true;
        CurBlob_.BlobDie = false;
        CurBlob_.HealthBarCanvas.SetActive(false);
        CurBlob_.GolemOrbAim = true;
        CurBlob_.FrostBarrageTurnRateVariation = Random.Range(3, 5f);
        AttackCount--;
        CurBlob_.Invoke("BlobExpireTimer", 20f);
        BossSound1.Play();
        if (AttackCount > 0)
        {
            anim.AttackAnimation5();
            StartCoroutine(MageBarrage(1.55f, AttackCount, DirMod));
            RotateTowards(PC.transform);

        }
        else
        {
            BBStill = false;
            BossCastingSpell = false;
            if (!CurrentlyFrozen)
            {
                agent.isStopped = false;
            }
        }
    }

    void EndMageMeteor()
    {
        BBStill = false;
        BossCastingSpell = false;
        if (!CurrentlyFrozen)
        {
            agent.isStopped = false;
        }
        BossSound2.Stop();
    }

    void MageBigBadBomb()   // turns living stones to dead stones. Destroyes dead stones.
    {
        BBStill = true;
        agent.isStopped = true;
        anim.MeteorAttackAnim();
        GameObject RedCircle = Instantiate(BigRedCircle, PC_.transform);
        RedCircle.transform.position = PC_.transform.position;
        //Play Alarm sound.
        BossSound2.Play();
        StartCoroutine(MageBoom(5f, RedCircle, true));
        Invoke("EndMageMeteor", 5f);

        Destroy(RedCircle, 10f);

        if ((health / health2) <= 0.7f)
        {
            StartCoroutine(MageBoom(3f, RedCircle, false));
        }
        if ((health / health2) <= 0.3f)
        {
            StartCoroutine(MageBoom(1f, RedCircle, false));
        }

    }

    IEnumerator MageBoom(float delay, GameObject RedCircle, bool TrueMeteor)
    {

        yield return new WaitForSeconds(delay);
        GameObject BigMeteor = Instantiate(BigRedMeteor, RedCircle.transform);
        SpellProjectile BM = BigMeteor.GetComponent<SpellProjectile>();
        BM.spellCastLocation = RedCircle.transform.position;
        BM.aoeSizeMeteor = 4.5f;
        BM.projectilespeed = 40;
        BM.MageBossMeteor = true;
        BM.damage = 100;
        BM.MageBossCircle = RedCircle;
      //  BM.enemyCastingspell = true;
        BM.MageBoss = this;
        if (TrueMeteor)
        {
            RedCircle.transform.parent = null;
            BM.MageBossMeteorReal = true;
        }

        BigMeteor.transform.position = new Vector3(RedCircle.transform.position.x, RedCircle.transform.position.y + 40, RedCircle.transform.position.z);
    }

    public void MageBossDieFromBigExplosion()
    {
        anim.StoneGolemCrumble();
        animChild.transform.parent = null;
        Destroy(animChild, 2f);
        Destroy(gameObject);
    }

    void ChooseTarget()
    {
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
            else if (!FireTrailBoss) // not startscene and no illus
            {
                PC = PC_;
            }
            else
            {
                float DistToPlayer = Vector3.Distance(transform.position, PC_.transform.position);
                float DistToDoor1 = Vector3.Distance(transform.position, Door1.transform.position);
                float DistToDoor2 = Vector3.Distance(transform.position, Door2.transform.position);

                if (DistToPlayer > 80f)
                {
                    if (DistToDoor1 > DistToDoor2)
                    {
                        PC = Door2;
                    }
                    else
                    {
                        PC = Door1;
                    }
                    DontAttackJustMove = true;
                }
                else
                {
                    PC = PC_;
                    DontAttackJustMove = false;
                }
            }
        }
    }


    private void TimeKeeperOrb()
    {
        TimeOrbCD_ -= Time.deltaTime;

        if (TimeOrbCD_ < 0)
        {
            int OrbCount = 1;
            if (health <= health2 * 0.88f)
            {
                OrbCount++;
            }
            if (health <= health2 * 0.77f)
            {
                OrbCount++;
            }
            if (health <= health2 * 0.66f)
            {
                OrbCount++;
            }
            if (health <= health2 * 0.55f)
            {
                OrbCount++;
            }
            if (health <= health2 * 0.44f)
            {
                OrbCount++;
            }
            if (health <= health2 * 0.33f)
            {
                OrbCount++;
            }
            if (health <= health2 * 0.22f)
            {
                OrbCount++;
            }

            int randPlat = Random.Range(0, TimeKeeperPoints.Count);
            if (ChangeCol)
            {
                StartCoroutine(TimeKeeper2(0f, TimeOrbFrost, randPlat, OrbCount));
            }
            else
            {
                StartCoroutine(TimeKeeper2(0f, TimeOrbFire, randPlat, OrbCount));
            }                 
        TimeOrbCD_ = TimeOrbCD;
        }
    }


    IEnumerator TimeKeeper2(float delay, GameObject FireOrFrost, int randPlat, int count)
    {

        yield return new WaitForSeconds(delay);
        GameObject TimeKOrb = Instantiate(FireOrFrost, TimeKeeperPoints[randPlat].transform);

        TimeKOrb.transform.parent = null;
        TimeKOrb.transform.localScale = new Vector3(1, 1, 1);
        TimeKOrb.transform.position = TimeKeeperPoints[randPlat].transform.position;
        TKOrbScript orb = TimeKOrb.GetComponent<TKOrbScript>();
        orb.TargetBool = true;
        switch (randPlat)
        {
            case 0:
                orb.Target1 = TimeKeeperPoints[0].transform;
                orb.Target2 = TimeKeeperPoints[3].transform;
                break;
            case 1:
                orb.Target1 = TimeKeeperPoints[1].transform;
                orb.Target2 = TimeKeeperPoints[4].transform;
                break;
            case 2:
                orb.Target1 = TimeKeeperPoints[2].transform;
                orb.Target2 = TimeKeeperPoints[5].transform;
                break;
            case 3:
                orb.Target1 = TimeKeeperPoints[3].transform;
                orb.Target2 = TimeKeeperPoints[6].transform;
                break;
            case 4:
                orb.Target1 = TimeKeeperPoints[4].transform;
                orb.Target2 = TimeKeeperPoints[7].transform;
                break;
            case 5:
                orb.Target1 = TimeKeeperPoints[5].transform;
                orb.Target2 = TimeKeeperPoints[0].transform;
                break;
            case 6:
                orb.Target1 = TimeKeeperPoints[6].transform;
                orb.Target2 = TimeKeeperPoints[1].transform;
                break;
            case 7:
                orb.Target1 = TimeKeeperPoints[7].transform;
                orb.Target2 = TimeKeeperPoints[2].transform;
                break;
        }
        count--;
        if (count > 0)
        {
            if (randPlat == 7)
            {
                randPlat = 0;
            }
            else
            {
                randPlat++;
            }
            StartCoroutine(TimeKeeper2(0.3f, FireOrFrost, randPlat, count));
        }
    }

    void TimeKeeperChangeColor()
    {
        if (ChangeColorTimer_ <= 0)
        {
            var main = WaveColor.main;
            if (!ChangeCol)
            {
                main.startColor = colorBlue;
                ChangeCol = true;
                WaveColor.gameObject.GetComponent<TimeKeeperWaveDamage>().DamageType = true;
            }
            else
            {
                main.startColor = colorRed;
                ChangeCol = false;
                WaveColor.gameObject.GetComponent<TimeKeeperWaveDamage>().DamageType = false;
            }
            float randomT = Random.Range(-5, 6);
        ChangeColorTimer_ = ChangeColorTimer+ randomT;
        }
       ChangeColorTimer_ -= Time.deltaTime;
    }



    void GolemBossAttack()
    {
        GolemRockTossCD_ -= Time.deltaTime;
        if (GolemRockTossCD_ < 0)
        {
            GolemBossRockToss();
            float RandCd = Random.Range(-3, 0);
            GolemRockTossCD_ = GolemRockTossCD + RandCd;
        }
    }

    void GolemBossRockToss()
    {
        CancelInvoke("Attack");
        agent.isStopped = true;
        BBStill = true;
        TossingRock = true;
        anim.RockTossAnim();
        StopMovingAfterAttacking = false;
        CancelInvoke("StartMovingAfterAttackLands");
        attackCountdown = AttackSpeed;
        hardCodeDansGame = attackAnimCD;
        GameObject RockInHand = Instantiate(GolemRockTossObject, castPoint.transform.position, castPoint.transform.rotation, castPoint.transform);
        // RockInHand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        BossSound2.PlayDelayed(2f);
        Invoke("OldKingAttackEnd", 4.5f);

        StartCoroutine(RockTossed(2.9f, RockInHand));
    }

    IEnumerator RockTossed(float delay, GameObject Rock)
    {
        yield return new WaitForSeconds(delay);
        Rock.transform.parent = transform.parent;
        Rock.GetComponent<Rigidbody>().isKinematic = false;
        Rock.GetComponent<Rigidbody>().useGravity = true;
        TossingRock = false;
        Rock.GetComponent<GolemThrownRock>().realRock = true;
        Rock.GetComponent<GolemThrownRock>().NumberOfBlocks = NumberOfBlocks;
        Rock.GetComponent<Rigidbody>().velocity = BallisticVel2(PC.transform, 60f, Rock.transform);
        Rock.GetComponent<Rigidbody>().angularVelocity = new Vector3(20, 20, 20);
    }

    void RockTossedOnDeath()
    {
        for (int i = 0; i < NumberOfGolems; i++)
        {
            if (BossLayer != 3)
            {
                GameObject DeathRocks = Instantiate(GolemRockTossObject, castPoint.transform.position, castPoint.transform.rotation, castPoint.transform);
                DeathRocks.transform.position += new Vector3(0, 3, 0);
                DeathRocks.transform.parent = transform.parent;
                DeathRocks.GetComponent<Rigidbody>().isKinematic = false;
                DeathRocks.GetComponent<Rigidbody>().useGravity = true;
                DeathRocks.GetComponent<GolemThrownRock>().realRock = true;
                DeathRocks.GetComponent<GolemThrownRock>().BossDeathRocks = true;
                DeathRocks.GetComponent<GolemThrownRock>().BossCopy = BossCopyObj;
                DeathRocks.GetComponent<Renderer>().material.color = RockColor;
                BossRoom.AddMonster(DeathRocks);
                float xPos = Random.Range(-10, 10);
                float zPos = Random.Range(-10, 10);
                Vector3 asd = new Vector3(DeathRocks.transform.localPosition.x + xPos, 0, DeathRocks.transform.localPosition.z + zPos);
                DeathRocks.GetComponent<Rigidbody>().velocity = BallisticVel3(asd, 80f, DeathRocks.transform);
                int xx = Random.Range(-20, 20);
                int yy = Random.Range(-20, 20);
                int zz = Random.Range(-20, 20);
                DeathRocks.GetComponent<Rigidbody>().angularVelocity = new Vector3(xx, yy, zz);
            }
        }

    }

        // Update is called once per frame
    void Update()
    {
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
        if (FrostTrailBoss && InCombat)
        {
            FrostTrailAttack();
        }
        if (MageBoss && InCombat)
        {
            MageBossAttack();
        }

        if (Boss && TimeKeeper && !TKSpawning && manag.Illus.Count == 0 && !TimeKSpin && !TimeRandomOrbBool)
        {
            TimeKeeperOrb();
        }

        if (Boss && TimeKeeper)
        {
            TimeKeeperChangeColor();
        }

        if (Golemboss && InCombat)
        {
            GolemBossAttack();
        }

        if (TimeKeeper && !IamIllu) //Here is the cause for strange illu behaviour. take away && IAmIllu for it.
        {
            ASD += Time.deltaTime * 4;
            Platforms.transform.rotation = Quaternion.Euler(new Vector3(0, ASD, 0));
        }

        if (!FireAndFrostDisabled)
        {
            AmISlowed();
            AmIBurning();
        }

        ChooseTarget();
        
        if (LeaveFireTrail && InCombat)
        {
            LeaveFireTrailFunc();
        }
        if (!CurrentlyRessing && !BlobAttackNoTarget && !TheBlob && PC != null && StartAgentBool)
        {
            float dist = Vector3.Distance(transform.position, PC.transform.position);
            if ((dist < AggroRange) && !SpiderBoss && (CastingSpellTimer < 0) && !canAttack)
            {
                InCombat = true;

                if (MonsterType != 5)
                {
                    if (agent.isOnNavMesh && ((Vector3.Distance(destination, PC.transform.position) > 1) || CurrentlyInBlackHole || RefreshNavMeshTargetPosition <0))
                    {
                        if (!MageBoss)
                        {
                            destination = PC.transform.position;
                        }
                        else
                        {
                            destination = MageStart;
                        }
                        agent.destination = destination;
                        RefreshNavMeshTargetPosition = 0.25f;
                    }
                }

                if (MonsterType == 5)
                {


                        if (!CurrentlyFrozen && !Type5BHSLOW && MovementSpeed > 1)
                        {
                        Vector3 dir = new Vector3(PC.transform.position.x, 3f, PC.transform.position.z) - this.transform.position;
                        float distThisFrame = MovementSpeed * Time.deltaTime;

                        if (!float.IsNaN(dir.x* distThisFrame) && !float.IsNaN(dir.y* distThisFrame) && !float.IsNaN(dir.z* distThisFrame) && !float.IsNaN(dir.normalized.x * distThisFrame) && !float.IsNaN(dir.normalized.y * distThisFrame) && !float.IsNaN(dir.normalized.z * distThisFrame))
                            {



                            Vector3 ErrorPos = transform.position + (transform.forward * distThisFrame);
                            if (!float.IsNaN(ErrorPos.x) && !float.IsNaN(ErrorPos.y) && !float.IsNaN(ErrorPos.z) && !float.IsInfinity(ErrorPos.x) && !float.IsInfinity(ErrorPos.y) && !float.IsInfinity(ErrorPos.z))
                            {
                                transform.position += transform.forward * distThisFrame;

                                //   transform.Translate(dir.normalized * distThisFrame, Space.World);
                                Quaternion targetRotation = Quaternion.LookRotation(dir);
                                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * MovementSpeed);
                            }
                            else
                            {
                                Debug.Log("Error 2 would occure here");
                            }

                        }
                        else
                        {
                            Debug.Log("Error would occure here");
                        }
                        }

                        if (!CurrentlyFrozen && InCombat && !Boss && MovementSpeed < 9f)
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
                        if (MonsterType == 8 && animChild.activeSelf) { anim.RunAnim4(); }
                        if (MonsterType == 9) { anim.RunAnim5(); }

                        if (BigBoy)
                        {
                            BigBoyStepSoundDelay -= Time.deltaTime;
                        }


                    } else if (MageBoss && hardCodeDansGame < 0 && !BossCastingSpell)
                    {
                        anim.IdleAnimation5();
                        RotateTowards(PC.transform);
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
                    callForHelpCD = 1f;
                }
                    callForHelpCD -= Time.deltaTime;

            }
            else if (dist > AggroRange + 2  && !SpiderBoss && MonsterType != 5 && !BBStill)
            {

                if (Vector3.Distance(transform.position, agent.destination) < 2f)
                {
                    if (MonsterType == 1) { anim.IdleAnim(); }
                    if (MonsterType == 2) { anim.IdleAnimation(); }
                    if (MonsterType == 3) { anim.IdleAnimation2(); }
                    if (MonsterType == 4) { anim.PlayerIdle(); } // Timekeeper
                    if (MonsterType == 6) { anim.IdleAnimation3(); }
                    if (MonsterType == 7) { anim.IdleAnimation3(); }
                    if (MonsterType == 8 && animChild.activeSelf) { anim.IdleAnimation4(); }
                    if (MonsterType == 9) { anim.IdleAnimation5(); }
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
            if (TimeKSpin && Illusionist) // Illusionist special attack, chaning destination.
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 130f, Space.World);
                agent.destination = transform.position + (transform.forward * 4);
            }
            if (TimeKSpin && TimeKeeper) {

                if (ClockAttackCD_ <= 0)
                {
                    if (!CurrentlyInBlackHole)
                    {
                        ClockPlatCount++;
                        agent.Warp(TimeKeeperPoints[ClockPlatCount].transform.position);
                        ParentPlatform = TimeKeeperPoints[ClockPlatCount].transform;
                    }

                    if (ClockPlatCount == TimeKeeperPoints.Count-1)
                    {
                        ClockPlatCount = -1;
                    }

                    ClockAttackCD_ = 0.4f;
                }
                ClockAttackCD_ -= Time.deltaTime;                 
            }

            if (TimeRandomOrbBool && TimeKeeper)
            {

                if (ClockAttackCD_ <= 0)
                {
                    ClockPlatCount++;
                    agent.Warp(TimeKeeperPoints[ClockPlatCount].transform.position);
                    ParentPlatform = TimeKeeperPoints[ClockPlatCount].transform;

                    if (ClockPlatCount == TimeKeeperPoints.Count - 1)
                    {
                        ClockPlatCount = -1;
                    }
                    if (TImeRandomGottaGoFast >= 0.075f)
                    {
                        TImeRandomGottaGoFast -= 0.05f;
                        ClockAttackCD_ = TImeRandomGottaGoFast;
                    }
                    else
                    {
                        ClockAttackCD_ = 0.075f;
                    }

                }
                ClockAttackCD_ -= Time.deltaTime;
            }

            //Attack code
            if (((dist <= meleeRange + 0.5f) || (CastingSpellTimer > 0)) && dist <= AggroRange) { canAttack = true; }
            else if (dist > meleeRange + DisengageDistance) { canAttack = false; }

           if (dist <= meleeRange+0.5f && DisengageDistanceRemoveAfterAttack) { DisengageDistanceRemoveAfterAttack = false; }
           else if (DisengageDistanceRemoveAfterAttack) { canAttack = false; }

            if (canAttack && !BBStill && !DisengageDistanceRemoveAfterAttack && !DontAttackJustMove)
            {
                if (!SpiderBoss && (!TimeKSpin || TimeKeeper) && !TKSpawning)
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
                    if (MonsterType == 9) { anim.AttackAnimation5(); }

                   if (Golemboss)
                    {
                        GolemRockTossCD_ += 2.5f;
                    }
                   if (BigBoy)
                    {
                        BigBoySpecial1_ += 2.5f;
                    }
                   if (OldKing && !AttackFriend)
                    {
                        OldKingSpecialAttack_1 += 3;
                    }

                    Invoke("Attack", AttackDelay);

                    if ((MonsterType == 1 || MonsterType == 3 || MonsterType == 6 || MonsterType == 8 || MonsterType == 9) && !SpiderBoss)
                    {
                        MeleeAttackStart.PlayDelayed(AttacStartDelay);
                    }

                    if (MonsterType == 1 || MonsterType == 3 || MonsterType == 5 || MonsterType == 6 || SpiderBoss || MonsterType == 8 || MonsterType == 9) // non casters
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
            RefreshNavMeshTargetPosition -= Time.deltaTime;

            if (MonsterType != 5)
            {
                hardCodeDansGame -= Time.deltaTime;
            }

            if (Illusionist || TimeKeeper)
            {
                if (manag.Illus.Count <= 0 && !TimeKSpin && !TimeRandomOrbBool)
                {
                    MirrorImageCD_ -= Time.deltaTime;
                    Ratatatata_ -= Time.deltaTime;
                    TimeRandomOrbAttack_ -= Time.deltaTime;
                }
            }
            if (SpiderBoss)
            {
                SwarmCD_ -= Time.deltaTime;
            }
            

            if (EventSkeleton)
            {
                EventSkeletonCD_ -= Time.deltaTime;
            }

            if (IlluHit)
            {
                TakeDamage(Time.deltaTime / IlluDamageAmount);
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
        LeaveFireTrailCD_ -= Time.deltaTime;

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

    void BlobExpireTimer()
    {
        OnlyOnce = true;
        transform.GetChild(0).gameObject.SetActive(true);
        Destroy(transform.GetChild(0).gameObject, 0.99f);
        transform.GetChild(0).gameObject.transform.parent = null;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (BlobAttackNoTarget && !OnlyOnce)
        {
            if (other.tag == "Wall" || other.tag == "MageBossDeadGolem")
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
        if (Golemboss && other.tag == "GolemRock")
        {
            other.GetComponent<GolemThrownRock>().DestoryRock();
            Destroy(other.gameObject);
            BossSound1.Play();
        }
    }


    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        if (HealthBarCanvas != null)
        {
            HealthBarCanvas.transform.rotation = HBtrack;
        }
        if (MonsterType == 5)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, StartHeight, transform.localPosition.z);
        }
    }

    private void RotateTowards(Transform target) // if in melee range, rotate towards player
    {
        Vector3 direction = (new Vector3(target.position.x, 4, target.position.z) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnRate);
    }

    void StopSpinHC()
    {
        CasterVariationAS = CasterVariationASRat;
        AttackSpeed = AttackSpeedRat;
        attackCountdown = 0.5f;
    }

    public void StopRandomOrb()
    {
        TimeRandomOrbBool = false;
        attackCountdown = 3f;
        TimeStartBlastingBool = false;
    }
    public void StopSpin()
    {
     //   var RandomTime = Random.Range(10, 15);
      //  MirrorImageCD_ = RandomTime;
        TimeKSpin = false;
      //  Ratatatata_ = RandomTime * 2.5f;
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
        if (MonsterType == 1 || MonsterType == 6 || MonsterType == 8 || MonsterType == 9 || (MonsterType == 3 && !SpiderBoss) && !CurrentlyRessing)
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

        if (MonsterType == 5 && !Boss && GameObject.FindWithTag("Illusion") == null)
        {
            //if (GameObject.FindWithTag("Illusion") != null)
            //{
            //    PC.GetComponent<IlluScript>().TakeDamage(damage);
            //}
            //else 
            if (!AttackFriend)
            {
                PC.GetComponent<Player>().TakeDamage(damage);
            }
            else if (AttackFriend)
            {
                PC.GetComponent<Monster>().TakeDamage(damage);
            }
            Loot();
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
                var RandomSpell = Random.Range(0, 9);
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
                    case 7:
                        currentspellObject = currentspellObject3;
                        currentSpell = currentSpell3;
                        MonsterTypeSubLayer = 2;
                        break;
                    case 8:
                        currentspellObject = currentspellObject2;
                        currentSpell = currentSpell2;
                        MonsterTypeSubLayer = 1;
                        break;
                }
            }

            if (attackCountdown >= 0.2f || !IamIllu) // so illus wont attack on spawn.
            {

                Quaternion Rotat = castPoint.transform.rotation;
                Vector3 Postat = castPoint.transform.position;

                GameObject test123 = Instantiate(currentspellObject, Postat, Rotat, castPoint.transform);
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
                        float randSpeed = 0;
                        if (MonsterType == 7)
                        {
                            randSpeed = Random.Range(-5, 3);
                        }

                        spell.projectilespeed = currentSpell.GetComponent<FrostBolt>().projectilespeed + randSpeed;
                        spell.damage = currentSpell.GetComponent<FrostBolt>().damagePure * MonsterSpellDamageModifier;
                        spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
                        spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
                        spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent + 0.1f;
                        spell.SineWaveAttack = SineWaveAttack;
                        break;
                    case 2:
                        spell.projectilespeed = currentSpell.GetComponent<Fireball>().projectilespeed-4f;
                        spell.damage = (currentSpell.GetComponent<Fireball>().damagePure * MonsterSpellDamageModifier) - 0.3f;
                        spell.FireBallBurn = currentSpell.GetComponent<Fireball>().FireBallBurn;
                        spell.BurnDuration = currentSpell.GetComponent<Fireball>().BurnDuration+0.5f;
                        spell.BurnPercent = currentSpell.GetComponent<Fireball>().BurnPercent;
                        spell.SineWaveAttack = SineWaveAttack;
                        spell.cone = Cone;
                        if (MonsterType == 2)
                        {
                            ShapeCone(test123);
                        }

                        break;
                    case 3:
                        spell.projectilespeed = currentSpell.GetComponent<LightningBolt>().projectilespeed;
                        spell.damage = currentSpell.GetComponent<LightningBolt>().damagePure * MonsterSpellDamageModifier;
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
                    

                    if (TimeKeeper && (manag.Illus.Count > 0 || TimeKSpin))
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
                BossSound1.Play();
                Invoke("StartSwarm", 1.5f);
                Invoke("StopSwarm", 4f);
            }
            else
            {
                GameObject ball = Instantiate(SBAttack, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z - 4f), Quaternion.identity);
                ball.GetComponent<Rigidbody>().velocity = BallisticVel(PC.transform, 30f);
                ball.GetComponent<SpiderBall>().Daddy = gameObject;
                BossSound2.Play();
            }
        }
    }

    void ShapeCone(GameObject cone)
    {
        cone.GetComponent<BoxCollider>().center = new Vector3(0, 0, -2);
        cone.GetComponent<BoxCollider>().size = new Vector3(3.5f, 3.5f, 0);
        StartCoroutine(ShapeConeNext(0.02f, 10, cone, -2, 0));
    }
    IEnumerator ShapeConeNext(float delay, int iterations, GameObject cone, float x1, float x2)
    {
        yield return new WaitForSeconds(delay);
        if (cone != null)
        {
            Next(cone, iterations, x1, x2);
        }
    }
    void Next(GameObject cone, int iterations, float x1, float x2)
    {
        x1 += 0.4f;
        if (iterations > 5)
        {
            x2 += 0.8f;
        }
        else
        {
            x2 -= 0.8f;
        }
        cone.GetComponent<BoxCollider>().center = new Vector3(0, 0, x1);
        cone.GetComponent<BoxCollider>().size = new Vector3(3.5f, 3.5f, x2);
        if (iterations > 1)
        {
            StartCoroutine(ShapeConeNext(0.11f, iterations - 1, cone, x1, x2));
        }
    }




void StartSwarm() // so animation can start before spiders spawn.
    {
        SummonSwarm(25);

    }

    void StopSwarm()
    {
        Swarm = false;
        BossSound1.Stop();
    }
    public void ReturnType5MovementSpeed(float time) // never called?
    {
        Invoke("RT5MS", time);
    }
    private void RT5MS() // never called?
    {
        Type5BHSLOW = false;
        //MovementSpeed = MovementSpeed_;
        //if (slowedDur > 0)
        //{
        //    Slow(true, slowedDur, CurrentSlowSTR);
        //}
    }

    void SummonSwarm(int count)
    {

        GameObject Spider = Instantiate(SpiderSwarm, transform.position - (transform.forward * 3), transform.rotation);

        AddToRoomMonsterList(Spider);
        Spider.GetComponent<Monster>().AggroRange = 50;
        Spider.GetComponent<Monster>().MovementSpeed = 3f;
        Spider.GetComponent<Monster>().damage = 0.25f;
        Spider.GetComponent<Monster>().health = 0.5f;
        Spider.GetComponent<Monster>().health2 = 0.5f;
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

            if (manag.Illus.Count == 0 && Ratatatata_ <= 0)
            {
                TimeKSpin = true;
                Invoke("StopSpinHC", 7.4f);
                Invoke("StopSpin", 8f);
                Ratatatata_ = Ratatatata;

                agent.Warp(TimeKeeperPoints[0].transform.position);
                ParentPlatform = TimeKeeperPoints[0].transform;
                if (Illusionist)
                {
                    AttackSpeed = 0.1f;
                    CasterVariationAS = 0.1f;
                }
                if (TimeKeeper)
                {
                    AttackSpeed = 0.4f;
                    CasterVariationAS = 0.4f;
                    attackCountdown = 0f;
                    ClockPlatCount = -1;
                }
                TimeKeeperCounter = 4;
            }

            if (manag.Illus.Count == 0 && !TimeKSpin)
            {
                TeleLoc = Random.Range(0, TimeKeeperPoints.Count);
                if (TimeKeeperCounter % 99 == 1 && TimeKeeper)
                {
                    agent.Warp(TimeKeeperPoints[TeleLoc].transform.position);
                    var RandSind = Random.Range(0, 2);
                    if (RandSind == 0)
                    {
                        SineWaveAttack = true;
                    }
                }
                else if (!TimeKeeper || MirrorImageCD_ <=0)
                {
                    agent.Warp(TimeKeeperPoints[TeleLoc].transform.position);
                    var RandSind = Random.Range(0, 2);
                    if (RandSind == 0)
                    {
                        SineWaveAttack = true;
                    }
                }

                ParentPlatform = TimeKeeperPoints[TeleLoc].transform;
            }


            if (MirrorImageCD_ <= 0 && manag.Illus.Count == 0 && !TimeKSpin)
            {
                TimeKeeperLaugh();
                SineWaveAttack = false;
                CurrentlyInBlackHole = false;
                for (int i = 0; i < TimeKeeperPoints.Count; i++)
                {
                    if (i != TeleLoc)
                    {
                        Vector3 Pos = TimeKeeperPoints[i].transform.position;
                        GameObject MMI = Instantiate(gameObject, Pos, transform.rotation);
                        AddToRoomMonsterList(MMI);
                        Monster Illu = MMI.GetComponent<Monster>();

                        if (!TimeKeeper)
                        {
                            Illu.attackCountdown += Random.Range(2, 4f);
                            Illu.health = 5;
                            Illu.health2 = 5;
                            Illu.Invoke("IlluMaxTime", 10);
                            Illu.ParentPlatform = TimeKeeperPoints[i].transform;
                            Illu.IlluDamageAmount = 2;
                            float randomAS = Random.Range(3, 6);
                            Illu.CasterVariationAS = randomAS;
                        }
                        else
                        {
                            Illu.attackCountdown += Random.Range(6, 9f);
                            Illu.health = 50;
                            Illu.health2 = 50;
                            Illu.Invoke("IlluMaxTime", 15);
                            Illu.IlluDamageAmount = 0.5f;
                            float randomAS = Random.Range(4, 8);
                            Illu.CasterVariationAS = randomAS;
                            switch (i)
                            {
                                case 0:
                                    Illu.ParentPlatform = TimeKeeperPoints[3].transform;
                                    break;
                                case 1:
                                    Illu.ParentPlatform = TimeKeeperPoints[4].transform;
                                    break;
                                case 2:
                                    Illu.ParentPlatform = TimeKeeperPoints[5].transform;
                                    break;
                                case 3:
                                    Illu.ParentPlatform = TimeKeeperPoints[6].transform;
                                    break;
                                case 4:
                                    Illu.ParentPlatform = TimeKeeperPoints[7].transform;
                                    break;
                                case 5:
                                    Illu.ParentPlatform = TimeKeeperPoints[0].transform;
                                    break;
                                case 6:
                                    Illu.ParentPlatform = TimeKeeperPoints[1].transform;
                                    break;
                                case 7:
                                    Illu.ParentPlatform = TimeKeeperPoints[2].transform;
                                    break;
                            }


                        }
                        Illu.TotalBurnDamage = 0;
                        Illu.Healthbar.fillAmount = health / health2;
                        Illu.MirrorImageCD = 100000;
                        Illu.MirrorImageCD_ = 100000;
                        Illu.IamIllu = true;
                        Illu.MonsterSpellDamageModifier = 0.5f;
                        Illu.Boss = false;
                        Illu.AttackSpeed = 2;


                        // Illu.attackCountdown = randomAS-3;

                        Illu.Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

                        manag.Illus.Add(MMI);
                        TimeKeeperCounter = 4;
                    }
                    else if (TimeKeeper)
                    {
                        switch (i)
                        {
                            case 0:
                                ParentPlatform = TimeKeeperPoints[3].transform;
                                break;
                            case 1:
                                ParentPlatform = TimeKeeperPoints[4].transform;
                                break;
                            case 2:
                                ParentPlatform = TimeKeeperPoints[5].transform;
                                break;
                            case 3:
                                ParentPlatform = TimeKeeperPoints[6].transform;
                                break;
                            case 4:
                                ParentPlatform = TimeKeeperPoints[7].transform;
                                break;
                            case 5:
                                ParentPlatform = TimeKeeperPoints[0].transform;
                                break;
                            case 6:
                                ParentPlatform = TimeKeeperPoints[1].transform;
                                break;
                            case 7:
                                ParentPlatform = TimeKeeperPoints[2].transform;
                                break;
                        }
                    }
                }
                if (!TimeKeeper)
                {
                    attackCountdown += Random.Range(2, 4f);
                }
                else
                {
                    attackCountdown += Random.Range(5.5f, 8f);
                }
                ParticleSystem ps = OuterRing.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.startDelay = 0;
                OuterRing.Play(true);
                MirrorImageCD_ = MirrorImageCD;
            }

            if (TimeRandomOrbAttack_ <= 0 && manag.Illus.Count == 0 && !TimeKSpin && TimeKeeper)
            {
                TimeRandomOrbBool = true;
                TimeRandomOrbAttack_ = TimeRandomOrbAttack;
                Invoke("StopRandomOrb", 15f);
                TImeRandomGottaGoFast = 0.6f;
                Invoke("TKStartblast", 4f);
                // AttackSpeed = 0.05f;
                // CasterVariationAS = 0.05f;
                attackCountdown = 20f;
                ClockPlatCount = -1;
                TimeKeeperCounter = 4;

            }
            TimeKeeperCounter++;
        }
    }

    void TKStartblast()
    {
        TimeStartBlastingBool = true;
        StartCoroutine(TimeKeeper3(0.1f));

    }

    IEnumerator TimeKeeper3(float delay)
    {

        yield return new WaitForSeconds(delay);
        GameObject FireOrFrost = TimeOrbFire;

        if (ChangeCol)
        {
            FireOrFrost = TimeOrbFrost;
        }

       int randPlat = Random.Range(0, TimeKeeperPoints.Count);
       int randPlat2 = Random.Range(0, TimeKeeperPoints.Count);

        GameObject TimeKOrb = Instantiate(FireOrFrost, TimeKeeperPoints[randPlat].transform);

        TimeKOrb.transform.parent = null;
        TimeKOrb.transform.localScale = new Vector3(1, 1, 1);
        TimeKOrb.transform.position = TimeKeeperPoints[randPlat].transform.position;
        TKOrbScript orb = TimeKOrb.GetComponent<TKOrbScript>();
        orb.TargetBool = true;

        orb.Target1 = TimeKeeperPoints[randPlat].transform;
        
        if (randPlat != randPlat2)
        {
            orb.Target2 = TimeKeeperPoints[randPlat2].transform;
        }
        else if (randPlat < 7)
        {
            orb.Target2 = TimeKeeperPoints[7].transform;
        }
        else
        {
            orb.Target2 = TimeKeeperPoints[0].transform;
        }

        if (TimeStartBlastingBool)
        {
            StartCoroutine(TimeKeeper3(0.5f));
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

    public Vector3 BallisticVel2(Transform target, float angle, Transform Rock)
    {//SpiderBossAttack
        var dir = target.position - Rock.transform.position;  // get target direction
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
    public Vector3 BallisticVel3(Vector3 target, float angle, Transform Rock)
    {//SpiderBossAttack
        var dir = target - Rock.transform.localPosition;  // get target direction
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
            BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
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
                Brother.GetComponent<Monster>().BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
            }
        }
        Healthbar.fillAmount = health / health2;

        if (Boss)
        {
            if (health > 0.1f)
            {
                BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
                BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
            } else if (health > 0)
            {
                BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = "0.1 / " + health2;
                BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
            }
        }
        else if (Brother == null && !IamIllu && tag == "Monster")
        {
            if (health > 0.1f)
            {
                HBtext.text = "(" + health.ToString("F1") + " / " + MaxHealthAsString + ")";
            }
            else if (health > 0)
            {
                HBtext.text = "(0.1 / " + MaxHealthAsString + ")";
            }
        }

        if (!StartScene)
        {
            AggroRange = 999;
        }

        if (IamIllu && damage > 0)
        {
            Healthbar.color = Color.blue;
            Healthbar.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
            IlluHit = true;
        }

        if (health <= 0 && OnlyOnce == false && PC_.GetComponent<Player>().DieOnce == false)
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

        if (Boss && FireTrail)
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
            MageBossForGolems.Rocks.Add(Rock);
            Rock.transform.parent = transform.parent;
            Rock.transform.localPosition = transform.localPosition + transform.forward * 1;
            Rock.transform.localRotation = transform.localRotation;
            animChild.transform.parent = Rock.transform;
        }
        if (MonsterType == 9) {
            anim.DieAnimation8();

            Instantiate(DieNuke, transform.position, transform.rotation);
            PC_.GetComponent<UnityEngine.AI.NavMeshAgent>().areaMask = UnityEngine.AI.NavMesh.AllAreas;
            Destroy(MageBossFire1);
            Destroy(MageBossFire2);
        }

        if (IamIllu)
        {
            manag.Illus.Remove(gameObject);
        }
        if (TimeKeeper && !IamIllu)
        {
            Platforms.transform.parent = transform.parent;
        }

        if (animChild != null && !Resurrect && !StoneGolem)
        {
            animChild.transform.parent = null;
        }

        SpiderBoss = false;
        if (!Resurrect)
        {
            if (Golemboss)
            {
                RockTossedOnDeath();
            }
            else
            {
                Loot();               
            }

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

            if (Boss & !Golemboss && PC_.GetComponent<Player>().DieOnce == false)
            {
                manag.SteamBossAchievement(BossName);
            }

            if (MiniBossMonster)
            {
                MapGrid.FindObjectOfType<MapGrid>().LevelHasHadMiniBoss = true;
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

        if (!BlobAttackNoTarget)
        {
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
            CurBlob_.health = 0.7f;
            CurBlob_.health2 = 0.7f;
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

           if (TossingRock && PC.transform != null)
        {
            RotateTowards(PC.transform);
        }

        if (BlobAttackNoTarget && !OrderPhase2Orb)
        {


            float distThisFrame = (MovementSpeed) * Time.deltaTime;

            Vector3 ErrorPos = transform.position + (transform.forward * distThisFrame);

            if (!float.IsNaN(ErrorPos.x) && !float.IsNaN(ErrorPos.y) && !float.IsNaN(ErrorPos.z) && !float.IsInfinity(ErrorPos.x) && !float.IsInfinity(ErrorPos.y) && !float.IsInfinity(ErrorPos.z))
            {

                if (BlobAttack4)
                {
                    // var rotation_ = (Vector3.up * Time.deltaTime * Blob4RotSpeed);
                    transform.Rotate(Vector3.up * Time.deltaTime * Blob4RotSpeed);
                    // rb.rotation = Quaternion.Euler(rotation_); // currently not working, but would be better to use rigidbody for transform.
                }
                rb.position = new Vector3(transform.position.x, 2.5f, transform.position.z);

                if (!CurrentlyFrozen && !Type5BHSLOW && MovementSpeed > 1)
                {
                    rb.position += transform.forward * distThisFrame;
                }
            }
            else
            {
                Debug.Log("Error 3 would occure here");
            }

        }
        if (OrderPhase2Orb)
        {
            Vector3 dir = PC.transform.position - this.transform.position;

            float distThisFrame = (MovementSpeed) * Time.deltaTime;

            Vector3 ErrorPos = transform.position + (transform.forward * distThisFrame);

            if (!float.IsNaN(ErrorPos.x) && !float.IsNaN(ErrorPos.y) && !float.IsNaN(ErrorPos.z) && !float.IsInfinity(ErrorPos.x) && !float.IsInfinity(ErrorPos.y) && !float.IsInfinity(ErrorPos.z))
            {

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                if (!GolemOrbAim)
                {
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * MovementSpeed / 8);
                }
                else
                {
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * MovementSpeed / FrostBarrageTurnRateVariation);
                    if (MovementSpeed < 9f)
                    {
                        MovementSpeed += Time.deltaTime / 6;
                        MovementSpeed_ += Time.deltaTime / 6;
                    }
                }

                rb.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
                rb.position += transform.forward * distThisFrame;

            }else
            {
                Debug.Log("Error 4 would occure here");
            }
        }

        if (TimeKeeper && ParentPlatform != null && !CurrentlyInBlackHole)
        {

            if (!TKSpawning)
            {
                transform.position = Vector3.MoveTowards(transform.position, ParentPlatform.position, 5 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, ParentPlatform.position, 2.5f * Time.deltaTime);
            }



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
            if (MonsterType == 5)
            {
                MovementSpeed = 0.1f;
            }
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
            if (MonsterType == 5)
            {
                    MovementSpeed = MovementSpeed_;
                if (slowedDur > 0)
                {
                    Slow(true, slowedDur, CurrentSlowSTR);
                }             
            }
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
            AggroRange = 999;
    }

    public void Slow(bool slow, float dur, float str) //Could slow attackspeed aswell, though might take a bit of work to slow down animations aswell.
    {
        CurrentSlowSTR = str;

        if (slow && MovementSpeed >= MovementSpeed_ && !CurrentlyRessing && !FireAndFrostDisabled) //slow is true and currently not slowed. How to work if enemy has a sprint effect? to be seen.
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
            MonsterIsSlowed = true;
            Transform result = gameObject.transform.Find("slow");
            if (!result)
            {
                GameObject sloweffect = Instantiate(FrostSlow, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation, transform);
                sloweffect.name = "slow";
            }
        }
        if (slow)
        {
            slowedDur = dur;
        }
    }

    public void AmISlowed()
    {

        //if (CurrentlyFrozen && MonsterType == 5)
        //{
        //    MovementSpeed = 0.1f;
        //}
        //if (!CurrentlyFrozen && MonsterType == 5 && !TheBlob && MovementSpeed == 0.1f)
        //{
        //    MovementSpeed = MovementSpeed_;
        //    if (slowedDur > 0)
        //    {
        //        Slow(true, slowedDur, CurrentSlowSTR);
        //    }
        //}

        if (slowedDur <= 0 && MonsterIsSlowed)
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
            MonsterIsSlowed = false;
        }
        else if (MonsterIsSlowed)
        {
            slowedDur -= Time.deltaTime;
        }
    }

    public void Burn(bool burn, float dur, float str, float dmg)
    {
        if (burn && !CurrentlyRessing && !FireAndFrostDisabled)
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
            MonsterIsBurning = true;
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
        if (BurnDur <= 0 && MonsterIsBurning)
        {
            BurnDamage = 0;
            BurnTickRate = 0;
            Transform result = gameObject.transform.Find("burn");
            MonsterIsBurning = false;
            if (result)
            {
                CheckIfBurnBoosted = false;
                Destroy(transform.Find("burn").gameObject);
            }
        }
        else if (MonsterIsBurning)
        {
            BurnDur -= Time.deltaTime;

            if (BurnTickRate < 0)
            {
                TakeDamage(TotalBurnDamage * 0.2f);
                if (BurnDur > 0.2f || BurnDur <= 0.01f)
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
        if (bounce && noBounce == false && !CurrentlyRessing && bolt != null)
        {
            List<GameObject> MonsterList = new List<GameObject>();

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Monster"))
            { // if not null might need
                float dist = Vector3.Distance(enemy.transform.position, transform.position);
                if (dist < 30 && dist > 1 && enemy != gameObject)
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
        if (Boss && !TimeKeeper) 
        {
            var RandomLoot = Random.Range(0, 2);
            switch (RandomLoot)
            {
                case 0:
                        GameObject BossLoot = Instantiate(Chest, transform.position, Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z), BossRoom.transform);
                        BossLoot.transform.localPosition = LootLoc;
                        BossLoot.GetComponent<AmazingChestHead>().CurrentLoot = BossWeapon;
                    BossLoot.GetComponent<AmazingChestHead>().BossChest = true;
                    break;
                case 1:
                        GameObject BossLoot2 = Instantiate(Chest, transform.position, Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z), BossRoom.transform);
                        BossLoot2.transform.localPosition = LootLoc;
                        BossLoot2.GetComponent<AmazingChestHead>().CurrentLoot = BossArmor;
                    BossLoot2.GetComponent<AmazingChestHead>().BossChest = true;
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
        if (TimeKeeper && Boss)
        {
            RoomIAmIn.GetComponent<Room>().DaPlayer = PC_.GetComponent<Player>();
            RoomIAmIn.GetComponent<Room>().LastBossDoor();
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
            OldKingSpecialAttack_1 = 12f;
            Invoke("LongLiveTheKing", 2.5f);
            BossHealthAct.transform.GetChild(3).gameObject.GetComponent<Text>().text = health.ToString("F1") + " / " + health2;
            BossHealthAct.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = health / health2;
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

            if (PC_.GetComponent<Player>().DieOnce == false)
            {
                manag.SteamBossAchievement(BossName);
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
    //FrostTrail boss

    void FrostTrailAttack()
    {
        FrostTrailAttack1_ -= Time.deltaTime;
        if (FrostTrailAttack1_ < 0)
        {
            int AttackCount = 0;

            if (health <= (health2 / (4f / 3f)))
            {
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
        spell.damage = currentSpell.GetComponent<FrostBolt>().damagePure - 0.5f;
        spell.FrostBoltSlow = currentSpell.GetComponent<FrostBolt>().FrostBoltSlow;
        spell.SlowDuration = currentSpell.GetComponent<FrostBolt>().SlowDuration;
        spell.SlowPercent = currentSpell.GetComponent<FrostBolt>().SlowPercent;
        spell.aoeSizeMeteor = 3f;
        spell.BigBoyFrost = FrostAttack;
        float RandomSpot = Random.Range(-5f * Count, 5f * Count);
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

    //Most blob boss code
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
            StartCoroutine(Order1(gameObject, 5, new Vector3(0, 0, 0), BlobDirMod, 1f, 0.5f, AttackDelay_, Color.white));
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
            StartCoroutine(Order1(gameObject, 4, new Vector3(0, 0, 0), BlobDirMod, 1f, 12, AttackDelay_, Color.black));
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
            StartCoroutine(Order1(gameObject, 1, new Vector3(0, 0, 0), BlobDirMod, 1, 12, AttackDelay_, Color.yellow));
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
            StartCoroutine(Order1(gameObject, 2, new Vector3(0, 0, 0), BlobDirMod, 1, 9, AttackDelay_, Color.red));
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
            StartCoroutine(Order1(gameObject, 3, new Vector3(0, 0, 0), BlobDirMod, 1f, 9, AttackDelay_, Color.green));
            AttackDelay_ += 0.2f;

            if (BlobDirMod <= -25 && side == false)
            {
                side = true;
            }
            else if (BlobDirMod >= 25 && side == true)
            {
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

        if (BlobAttackCD_2 < 0 && (health / health2) <= 0.7f)
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

            if (health / health2 <= 0.3f)
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
                StartCoroutine(Blob1(gameObject, true, 0, StartLoc, 0, 1f, 5, 5f, AttackDelay_));
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
            StartCoroutine(Blob1(gameObject, false, 1, new Vector3(0, 0, 0), BlobDirMod, 1, 15, 4f, AttackDelay_));
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
            StartCoroutine(Blob1(gameObject, false, 2, RotateDir, 0, 1, 15, 4, AttackDelay_));
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

            StartCoroutine(Blob1(gameObject, false, 3, RotateDir, 0, 1, 15, 4, AttackDelay_));
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

            StartCoroutine(Blob1(gameObject, false, 4, RotateDir, 0, 1, 17, 4, AttackDelay_));
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

            StartCoroutine(Blob1(gameObject, false, 5, RotateDir, 0, 1, 8, 6, AttackDelay_));
            AttackDelay_ += 0.06f;
        }
    }

    IEnumerator Blob1(GameObject BlobDad, bool heal, int AttackNumb, Vector3 BlobRot, int DirMod, float BHealth, float Bspeed, float lifeTime, float delay)
    {
        yield return new WaitForSeconds(delay);
        Blob1AttackFunction(BlobDad, heal, AttackNumb, BlobRot, DirMod, BHealth, Bspeed, lifeTime);
    }

    private void Blob1AttackFunction(GameObject BlobDad, bool heal, int AttackNumb, Vector3 BlobRot, int DirMod, float BHealth, float Bspeed, float lifeTime)
    {
        GameObject CurBlob = Instantiate(BlobAttack1Object, transform.position, transform.rotation, transform);
        AddToRoomMonsterList(CurBlob);
        Monster CurBlob_ = CurBlob.GetComponent<Monster>();

        ParticleSystem ps = CurBlob_.BlobPS;
        var main = ps.main;
        if (AttackNumb == 1)
        {
            CurBlob.transform.LookAt(PC_.transform);
            CurBlob.transform.Rotate(Vector3.up * DirMod);
            CurBlob.transform.rotation = Quaternion.Euler(0, CurBlob.transform.rotation.eulerAngles.y, 0);
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
            CurBlob.transform.rotation = Quaternion.Euler(0, CurBlob.transform.rotation.eulerAngles.y, 0);
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
        CurBlob_.name = AttackNumb.ToString();
        CurBlob_.health2 = BHealth;
        CurBlob_.Healboss = heal;
        CurBlob_.MovementSpeed = Bspeed;
        CurBlob_.MovementSpeed_ = Bspeed;
        CurBlob_.BlobAttackNoTarget = true;
        CurBlob_.BlobDie = false;
        CurBlob_.BlobDieTimer = lifeTime;


        if (!heal)
        {
            TakeDamage(0.65f);
        }

    }

    void BlobExpire()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Destroy(transform.GetChild(0).gameObject, 0.99f);
        transform.GetChild(0).gameObject.transform.parent = null;
        Destroy(gameObject);
    }


    // Most bigboy code below
    void BigBoyAggro() // start/after special attack resetting to normal mode.
    {
        if (!SummonHelp && (health <= (health2/2.5f)))
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
            SmallHelp_.SmallBoyBool = true;
            //SmallHelp.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            //SmallHelp_.BBStill = true;
            //SmallHelp_.animChild.GetComponent<MonsterAnim>().Spawn();
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
        BiGHelp.GetComponent<Monster>().MonsterIsBurning = true;
        BiGHelp.GetComponent<Monster>().MonsterIsSlowed = true;
        BiGHelp.GetComponent<Monster>().slowedDur = 0f;
        BiGHelp.GetComponent<Monster>().BurnDur = 0f;
        BiGHelp.GetComponent<Monster>().health = health;
        BiGHelp.GetComponent<Monster>().health2 = health2;
        SummonHelp = true;
        BossHealthAct.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Big Boy & Big Bro";
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
        BigBoySpecial1_ -= Time.deltaTime;
    }
}
