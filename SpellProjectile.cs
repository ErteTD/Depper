using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellProjectile : MonoBehaviour {

    public GameObject effect1;
    public GameObject effect2;
    //  public GameObject FireCone;
    private bool ChanMetorCone;
    public LineRenderer lineRenderer;

    private Vector3 pos_;
    private Vector3 Direction;
    private Vector3 cone123;
    private Quaternion coneQuat;

    public bool enemyCastingspell = false;

    [HideInInspector] public Vector3 spellCastLocation;
    [HideInInspector] public float projectilespeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float aoeSizeMeteor;
    [HideInInspector] public bool ghostCast;
    [HideInInspector] public bool cone;
    [HideInInspector] public string spellName;
    [HideInInspector] public bool channeling;
    public float chanDur = 4f;
    private float chanDur_;
    [HideInInspector] public Vector3 chanLoc;
    public float chanRange;

    public bool FrostBoltSlow;
    public float SlowPercent, SlowDuration;

    public bool FireBallBurn;
    public float BurnPercent, BurnDuration;

    public bool LBBounce;
    public int LBBounceAmount;
    public GameObject Unmodified;

    public Quaternion ConeRote;

    [HideInInspector] public bool BoostCrit;
    [HideInInspector] public float CritChance, CritDamage;
    public GameObject CritVis;
    [Header("Channeling effect lightning rays")]
    public GameObject lightChild1;
    public GameObject lightChild2;
    public GameObject lightChild3;
    public GameObject lightChild4;
    public GameObject lightChild5;
    public GameObject lightChild6;

  //  [HideInInspector] public GameObject thePlayer;
    public GameObject BlackHole;
    public bool CompChanCollider;
    public float BHDuration;
    public Vector3 BHSize;
    public float BHRadius;
    public float BHStrenght;
    public bool BHBool;

    public bool Push;

    public bool pool;
    public float Poolduration;
    public float PoolDamage;
    public GameObject PoolInst;
    public GameObject ThePlayer;
    private Vector3 directionF;
    public GameObject BigBoyFrost;
    private GameObject TempTestTarget;
    public bool BlessedAim; // Blessed aim bool.
    public string chainID; // Blessed aim testing ghostcast
    public int listCount; // blessed aim testing ghostchast
    private bool struck; // blessed aim testing
    public bool LBCopy;

    public bool CompOrb; // C-Orb Testing.
    public bool CompOrbPlayer;
    public GameObject CompOrbReseveObject;
    public float CompOrbDur;

    public float CompOrbCD;
    private float CompOrbCD_;

    public bool ChaosOrb_; // Testing
    public GameObject ChaosOrbReseveObject;
    public float ChaosOrbCD;
    private float ChaosOrbCD_;
    public float ChaosOrbDuration;

    public bool SplitChanLeft;
    public bool SplitChanRight;

    public bool MultiChan1;
    public bool MultiChan2;
    public bool MultiChan3;
    private int MultiChanPosX;

    public bool HastenBool;
    public float HastenChance;
    public GameObject HastenVis;
    public GameObject CritVisCompOrb;
    private int ColCounter;
    private SpellProjectile CompColObject;

    private GameObject NearestEnemy_;
    private float Distance_;
    private float NewTargetTimer;

    private void Start()
    {

        if (!CompOrb && !ChaosOrb_) // CompOrb && ChaosOrb Code.
        {
            if (!LBCopy) // for LB bouncing.
            {
                chainID = GetInstanceID().ToString(); // Blessed aim testing
            }


            directionF = transform.forward;

            if (!CompOrbPlayer) // Comporb testing, making sure the orb is the new "player" for channeling scripts.
            {
                ThePlayer = GameObject.FindGameObjectWithTag("Player");
            }
            if (!enemyCastingspell && !CompOrbPlayer)
            {
                ThePlayer.GetComponent<Player>().curSpellProjectile.Add(gameObject);
            }

            Crit(); //calls the crit function.

            if (channeling)
            {
                Invoke("Stop", chanDur);
                if (aoeSizeMeteor == 0)
                {
                    this.transform.parent = ThePlayer.transform;
                    directionF = ThePlayer.transform.forward;

                    if (!cone)
                    {
                        if (MultiChan1)
                        {
                            MultiChanPosX = -1;
                        }
                        if (MultiChan2)
                        {
                            MultiChanPosX = 1;
                        }
                        if (MultiChan3)
                        {
                            MultiChanPosX = 0;
                        }

                    }

                }
                chanLoc = spellCastLocation;
                chanRange = 25; // control channeling distance with this.
                if (!CompOrbPlayer) // Comporb testing, making sure the orb is the new "player" for channeling script
                {
                    ThePlayer.GetComponent<Player>().Channeling(); // call player channeling function to set speed to 0
                }

                if (spellName == "Lightningbolt" && !cone)
                {
                    transform.GetComponent<BoxCollider>().enabled = false;
                }

                if (spellName == "Lightningbolt" && aoeSizeMeteor == 0 && !cone)
                {
                    lightChild1.transform.localScale = new Vector3(lightChild1.transform.localScale.x, 1.5f, lightChild1.transform.localScale.z);
                    lightChild2.transform.localScale = new Vector3(lightChild2.transform.localScale.x, 1.5f, lightChild2.transform.localScale.z);
                    lightChild3.transform.localScale = new Vector3(lightChild3.transform.localScale.x, 1.5f, lightChild3.transform.localScale.z);
                    lightChild4.transform.localScale = new Vector3(lightChild4.transform.localScale.x, 1.5f, lightChild4.transform.localScale.z);
                    lightChild5.transform.localScale = new Vector3(lightChild5.transform.localScale.x, 1.5f, lightChild5.transform.localScale.z);
                    lightChild6.transform.localScale = new Vector3(lightChild6.transform.localScale.x, 1.5f, lightChild6.transform.localScale.z);
                }


                if (spellName != "Lightningbolt")
                {
                   if (SplitChanLeft)
                    {
                        if (aoeSizeMeteor == 0)
                        {
                            Vector3 Rotet = new Vector3(90, -29, 0);
                            transform.localRotation = Quaternion.Euler(Rotet);
                        }
                        else
                        {
                            Vector3 Rotet = new Vector3(180, 0, 0);
                            transform.localRotation = Quaternion.Euler(Rotet);
                        }
                        if (cone)
                        {
                            transform.localPosition = new Vector3(-5, 3, -3f);
                            Vector3 Rotet = new Vector3(90, 0, 120);
                            transform.localRotation = Quaternion.Euler(Rotet);
                        }


                    }
                    if (SplitChanRight)
                    {
                        if (aoeSizeMeteor == 0)
                        {
                            Vector3 Rotet = new Vector3(90, 29, 0);
                            transform.localRotation = Quaternion.Euler(Rotet);
                        }
                        else
                        {
                            Vector3 Rotet = new Vector3(180, 0, 0);
                            transform.localRotation = Quaternion.Euler(Rotet);
                        }
                        if (cone)
                        {
                            transform.localPosition = new Vector3(5, 3, -3f);
                            Vector3 Rotet = new Vector3(90, 0, -120);
                            transform.localRotation = Quaternion.Euler(Rotet);
                        }

                    }
                }

                if (spellName == "Lightningbolt" && cone)
                {
                    if (SplitChanLeft)
                    {
                            transform.localPosition = new Vector3(-4, 2, -2);
                        Vector3 Rotet = new Vector3(0, 0, 90);
                        transform.localRotation = Quaternion.Euler(Rotet);
                    }
                    if (SplitChanRight)
                    {
                        transform.localPosition = new Vector3(4, 2, -2);
                        Vector3 Rotet = new Vector3(0, 0, 90);
                        transform.localRotation = Quaternion.Euler(Rotet);
                    }
                }

            }

            if (!channeling || aoeSizeMeteor > 0) // used to have spellname not LB ||
            {
                transform.parent = null;

            }

            if (aoeSizeMeteor == 0)
            {
                pos_ = transform.position;

                if (channeling)
                {
                    if (cone == false)
                    {
                        pos_ += transform.forward * chanRange; // set to spell range instead of fixed.                  
                    }

                    if (!CompOrbPlayer)
                    {
                        pos_.y = 1.5f;
                    }


                    chanLoc = pos_;

                    spellCastLocation = pos_;
                }

            }
            else
            {


                if (spellName != "Lightningbolt")
                {
                    if (!channeling)
                    {
                        transform.localScale += new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);
                        transform.GetChild(3).gameObject.SetActive(true); // meteor extra effect attached to fire/frost visuals.

                    }
                    //else
                    //{
                    //    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    //}
                }
                else if (channeling) // for lightning bolt, berry confusing.
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    lightChild1.GetComponent<ParticleSystemRenderer>().lengthScale = 5;
                    lightChild2.GetComponent<ParticleSystemRenderer>().lengthScale = 5;
                    lightChild3.GetComponent<ParticleSystemRenderer>().lengthScale = 5;
                    lightChild4.GetComponent<ParticleSystemRenderer>().lengthScale = 5;
                    lightChild5.GetComponent<ParticleSystemRenderer>().lengthScale = 5;
                    lightChild6.GetComponent<ParticleSystemRenderer>().lengthScale = 5;
                    lightChild1.transform.localScale = new Vector3(1, 1, 1);
                    lightChild2.transform.localScale = new Vector3(1, 1, 1);
                    lightChild3.transform.localScale = new Vector3(1, 1, 1);
                    lightChild4.transform.localScale = new Vector3(1, 1, 1);
                    lightChild5.transform.localScale = new Vector3(1, 1, 1);
                    lightChild6.transform.localScale = new Vector3(1, 1, 1);
                    Vector3 Asd = lightChild1.transform.position;

                    lightChild1.transform.position = new Vector3(Asd.x + 1, Asd.y + 1, Asd.z);
                    lightChild2.transform.position = new Vector3(Asd.x - 1, Asd.y - 1, Asd.z);
                    lightChild3.transform.position = new Vector3(Asd.x + 1, Asd.y, Asd.z);
                    lightChild4.transform.position = new Vector3(Asd.x, Asd.y + 1, Asd.z);
                    lightChild5.transform.position = new Vector3(Asd.x - 1, Asd.y, Asd.z);
                    lightChild6.transform.position = new Vector3(Asd.x, Asd.y - 1, Asd.z);

                }
                if (!channeling)
                {
                    if (MultiChan1)
                    {
                        MultiChanPosX = 2;
                    }
                    if (MultiChan2)
                    {
                        MultiChanPosX = 4;
                    }
                    if (MultiChan3)
                    {
                        MultiChanPosX = 6;
                    }
                }

                Vector3 spellCastLocation_ = spellCastLocation;
                if (!channeling || spellName == "Lightningbolt")
                {
                    spellCastLocation_.y += 15f;
                }
                else
                {
                    spellCastLocation_.y = 3;
                }

                if (spellName != "Lightningbolt" && !channeling)
                {
                    this.transform.Find("SubFire").gameObject.SetActive(false);
                    this.transform.Find("SubFire").gameObject.SetActive(true);
                }

                transform.localPosition = spellCastLocation_;


                transform.localPosition += transform.forward * MultiChanPosX;

                if (!channeling || spellName == "Lightningbolt")
                {
                    transform.Rotate(new Vector3(-90f, 0f, 0f));
                }

                if (channeling && spellName == "Lightningbolt")
                {
                    transform.Rotate(new Vector3(180f, 0f, 0f));
                }
                if (!channeling)
                {
                    Invoke("Meteorattack", 3f); // so blessed aim meteors don't chase forever.
                }

            }

            if (cone == true && channeling == false)
            {
                directionF = ThePlayer.transform.forward;
                if (spellName == "Lightningbolt")
                {
                    transform.localScale += new Vector3(transform.localScale.x * 2f, transform.localScale.y * 3f, transform.localScale.z * 4f);
                    Vector3 loc = transform.position;
                    loc += transform.forward * 2.2f;
                    transform.position = loc;

                    lightChild1.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
                    lightChild2.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
                    lightChild3.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
                    lightChild4.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
                    lightChild5.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
                    lightChild6.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);

                }
                else
                {

                        Vector3 loc2 = transform.position;
                        loc2 += transform.forward * 4f;
                        transform.position = loc2;
                }
                Invoke("Stop", 1.5f);

            }

            Invoke("Stop", 5);
        }
        if (CompOrb) // CompOrb testing.
        {
            transform.position = new Vector3(spellCastLocation.x, 1.5f, spellCastLocation.z);
            transform.parent = null;
            Invoke("Stop", CompOrbDur);

            if (channeling && aoeSizeMeteor == 0)
            {
          //   InvokeRepeating("OrbChannelTargetUpdate", 0.01f, 0.1f);
            }

            } else if (ChaosOrb_)
        {
            transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            pos_ = transform.position;
            Direction = transform.forward;

            transform.parent = null;
            Invoke("Stop", ChaosOrbDuration); // Give it a collider so destoryed when hitting a wall? Maaaybe. Lets see.
        }


    }

        public void Crit()
        {
            if (BoostCrit)
            {
                var randomInt = Random.Range(0, 100);

                if (randomInt <= CritChance)
                 {
                    damage *= CritDamage;
                if (!CompOrbPlayer) // Comporb testing, making sure the orb is the new "player" for channeling script. OBS! CHANGE! Make so the text can hover over the orb.
                {
                    ThePlayer.GetComponent<Player>().Crit(damage);
                }
                else
                {

                    GameObject Crit = Instantiate(CritVis, CritVisCompOrb.transform);
                    Text txt = Crit.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
   
                    txt.text = damage.ToString("F1");
                    Destroy(Crit, 1);
                }


            }
            }
        }


    public void ChainTarget() // Blessed aim ghostcast testing.
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                struck = false;
                listCount = enemy.GetComponent<Monster>().chainList.Count;

                for (int a = 0; a < listCount; a++)
                {
                    if (enemy.GetComponent<Monster>().chainList[a] == chainID)
                    {
                        struck = true;
                    }
                }
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if ((distanceToEnemy < shortestDistance) && struck == false)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null && shortestDistance <= 50)
            {
                TempTestTarget = nearestEnemy;
            }
            else
            {
                TempTestTarget = null;
            }    
    }

    void Update()
    {
        if (!CompOrb && !ChaosOrb_)
        { // CompOrb testing && ChaosOrb

            if (aoeSizeMeteor == 0 && cone == false && channeling == false)
            {
                pos_ += transform.forward * Time.deltaTime * projectilespeed;
                transform.position = pos_;

                if (!enemyCastingspell && BlessedAim) // Hello Future Joni. Whole Blessed aim is controllable with a boolean here. || !BlessedAim;
                {                       // Only fixed for normal projectiles. Cone, Meteor Channeling still wutface.
                    ChainTarget(); // From here down Blessed aim testing.
                    if (TempTestTarget != null)
                    {
                        Vector3 dir = TempTestTarget.transform.position - this.transform.localPosition;

                        if (dir.magnitude <= 5)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(dir);
                            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 10);
                        }
                        else
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(dir);
                            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * projectilespeed / 6);
                        }
                    }
                }
                transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            }

            if (aoeSizeMeteor > 0 && !channeling)
            {
                Vector3 dir = spellCastLocation - transform.localPosition;
                if (!enemyCastingspell && BlessedAim)
                {
                    ChainTarget();
                    if (TempTestTarget != null)
                    {
                        dir = (TempTestTarget.transform.position) - transform.localPosition;
                    }
                }

                if (dir.magnitude <= (projectilespeed / 2.5f) * Time.deltaTime) // needed at all?
                {
                    Meteorattack();
                }

                if (dir.magnitude <= 5 && TempTestTarget != null)
                {
                    transform.Translate((dir.normalized) * (projectilespeed / 2f * Time.deltaTime), Space.World);
                }
                else
                {
                    transform.Translate((dir.normalized) * (projectilespeed / 3f * Time.deltaTime), Space.World);
                }
            }

            if (cone && !channeling && BlessedAim)
            {
                pos_ += transform.forward * Time.deltaTime * projectilespeed / 3;

                if (!enemyCastingspell) // Hello Future Joni. Whole Blessed aim is controllable with a boolean here. || !BlessedAim;
                {                       // Only fixed for normal projectiles. Cone, Meteor Channeling still wutface.
                    ChainTarget(); // From here down Blessed aim testing.
                    if (TempTestTarget != null)
                    {
                        Vector3 dir = TempTestTarget.transform.position - this.transform.localPosition;
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(dir);
                            Quaternion targetRotationNotX = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

                            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotationNotX, Time.deltaTime * projectilespeed / 6);

                        }
                        transform.position = new Vector3(pos_.x, pos_.y, pos_.z);
                    }
                }
            }

            if (channeling && !cone && (aoeSizeMeteor == 0 || spellName == "Lightningbolt"))
            {
                if (aoeSizeMeteor == 0)
                {
                    transform.position = ThePlayer.transform.position;
                    pos_ = transform.position;

                        pos_ += ThePlayer.transform.forward * chanRange; // set to spell range instead of fixed.     
                        if (!CompOrbPlayer)
                        {
                            pos_.y = 1.5f;
                        }


                    if (SplitChanRight)
                    {
                        pos_ += (transform.right * 7.5f);                      
                    }
                    if (SplitChanLeft)
                    {
                        pos_ += (transform.right * -7.5f);
                    }
                    
                    chanLoc = pos_;
                    spellCastLocation = pos_;
                    gameObject.transform.position = ThePlayer.transform.position + new Vector3(0, 1.5f, 0f) + ThePlayer.transform.right * MultiChanPosX;                  
                }

                if (ghostCast) // script makes ghostcast work with channeling.
                {
                    RaycastHit[] hits;
                    var heading = chanLoc - transform.position;
                    var distance = heading.magnitude;
                    var direction = heading / distance;
                    hits = Physics.RaycastAll(transform.position, direction);

                    for (int i = 1; i < hits.Length; i++)
                    {
                        RaycastHit hit2 = hits[i];
                        float dist = Vector3.Distance(hit2.point, transform.position);


                        if (dist <= chanRange)
                        {
                            switch (hit2.transform.gameObject.tag)
                            {
                                case "Wall":
                                    chanLoc = hit2.point;

                                    //if (BHBool) //blackholescript TEST not hitting wall
                                    //{
                                    //    GameObject blackH = Instantiate(BlackHole, hit2.point, transform.rotation, transform);
                                    //    blackH.transform.parent = null;
                                    //    blackH.transform.localScale = BHSize;
                                    //    blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                                    //    blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                                    //    blackH.GetComponent<GravityBody>().duration = BHDuration;
                                    //    BHBool = false;
                                    //}
                                    //if (pool) //poolscript
                                    //{
                                    //    GameObject PoolObj = Instantiate(PoolInst, new Vector3(hit2.point.x, 0.5f, hit2.point.z), PoolInst.transform.rotation, transform);
                                    //    PoolObj.transform.parent = null;
                                    //    PoolObj.transform.localScale = new Vector3(1, 1, 1);
                                    //    Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                                    //    curPool.TriggerKillMe(Poolduration);
                                    //    curPool.damage = PoolDamage;
                                    //    curPool.FrostBoltSlow = FrostBoltSlow;
                                    //    curPool.SlowPercent = SlowPercent;
                                    //    curPool.SlowDuration = SlowDuration;
                                    //    curPool.FireBallBurn = FireBallBurn;
                                    //    curPool.BurnPercent = BurnPercent;
                                    //    curPool.BurnDuration = BurnDuration;
                                    //    curPool.LBBounce = LBBounce;
                                    //    curPool.LBBounceAmount = LBBounceAmount;
                                    //    curPool.Unmodified = Unmodified;
                                    //    curPool.projectilespeed = projectilespeed;
                                    //    curPool.ghostCast = ghostCast;
                                    //    curPool.spellName = spellName;
                                    //    pool = false;
                                    //}


                                    break;
                                case "Tree":
                                    chanLoc = hit2.point;
                                    break;
                                case "Monster":
                                    if (!CompChanCollider) // TESTING!!!
                                    {
                                        if (aoeSizeMeteor == 0)
                                        {
                                            Monster enemy = hit2.collider.GetComponent<Monster>();

                                            enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                                            enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
                                            enemy.BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
                                            enemy.TakeDamage(damage * Time.deltaTime);

                                            if (Push)
                                            {
                                                enemy.GetComponent<Monster>().pushDir = directionF;
                                                enemy.GetComponent<Monster>().ChannelPush(4 * Time.deltaTime);

                                            }

                                        }
                                        if (BHBool) //blackholescript
                                        {
                                            GameObject blackH = Instantiate(BlackHole, hit2.point, transform.rotation, transform);
                                            blackH.transform.parent = null;
                                            blackH.transform.localScale = BHSize;
                                            blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                                            blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                                            blackH.GetComponent<GravityBody>().duration = BHDuration;
                                            BHBool = false;
                                        }
                                        if (pool) //poolscript
                                        {
                                            GameObject PoolObj = Instantiate(PoolInst, new Vector3(hit2.point.x, 1, hit2.point.z), PoolInst.transform.rotation, transform);
                                            PoolObj.transform.parent = null;
                                            PoolObj.transform.localScale = new Vector3(1, 2, 2);
                                            Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                                            curPool.TriggerKillMe(Poolduration);
                                            curPool.damage = PoolDamage;
                                            curPool.FrostBoltSlow = FrostBoltSlow;
                                            curPool.SlowPercent = SlowPercent;
                                            curPool.SlowDuration = SlowDuration;
                                            curPool.FireBallBurn = FireBallBurn;
                                            curPool.BurnPercent = BurnPercent;
                                            curPool.BurnDuration = BurnDuration;
                                            curPool.LBBounce = LBBounce;
                                            curPool.LBBounceAmount = LBBounceAmount;
                                            curPool.Unmodified = Unmodified;
                                            curPool.projectilespeed = projectilespeed;
                                            curPool.ghostCast = ghostCast;
                                            curPool.spellName = spellName;
                                            pool = false;
                                        }
                                    }
                                    break;
                                case "Illusion":
                                    break;
                                case "Ressing":
                                    break;
                                case "MirrorWall":
                                    if (ghostCast == false)
                                    {
                                        chanLoc = hit2.point;
                                    }
                                    if (aoeSizeMeteor == 0)
                                    {
                                        MirrorWall enemy = hit2.collider.GetComponent<MirrorWall>();

                                        enemy.BoltBounce(LBBounce, channeling, Unmodified, gameObject, enemyCastingspell);

                                    }
                                    break;

                                default:
                                    chanLoc = spellCastLocation;
                                    break;
                            }
                        }
                    }
                }
                //CompChanCollider Here i guess.
              //  Debug.Log(CompChanCollider);
                if (!CompChanCollider) // HYPERTESTING.
                {
                    RaycastHit hit;

                    var heading = chanLoc - transform.position;
                    var distance = heading.magnitude;
                    var direction = heading / distance;

                    if (Physics.Raycast(transform.position, direction, out hit))
                    {
                        float dist = Vector3.Distance(hit.point, transform.position);
                        //   Debug.Log(hit.transform.name + " " + dist + " " + Time.deltaTime);

                        if (spellName == "Lightningbolt" && aoeSizeMeteor == 0)
                        {
                            lightChild1.GetComponent<ParticleSystemRenderer>().lengthScale = dist * 0.80f;
                            lightChild2.GetComponent<ParticleSystemRenderer>().lengthScale = dist * 0.80f;
                            lightChild3.GetComponent<ParticleSystemRenderer>().lengthScale = dist * 0.80f;
                            lightChild4.GetComponent<ParticleSystemRenderer>().lengthScale = dist * 0.80f;
                            lightChild5.GetComponent<ParticleSystemRenderer>().lengthScale = dist * 0.80f;
                            lightChild6.GetComponent<ParticleSystemRenderer>().lengthScale = dist * 0.80f;
                        }

                        if (aoeSizeMeteor > 0 && spellName == "Lightningbolt")
                        {
                            Collider[] cols = Physics.OverlapSphere(hit.point, aoeSizeMeteor);
                            foreach (Collider c in cols)
                            {
                                Monster e = c.GetComponent<Monster>();
                                if (e != null)
                                {
                                    directionF = (e.transform.position - hit.point).normalized;

                                    e.GetComponent<Monster>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                                    e.GetComponent<Monster>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
                                    e.GetComponent<Monster>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
                                    e.GetComponent<Monster>().TakeDamage(damage * Time.deltaTime);

                                    if (Push)
                                    {
                                        e.GetComponent<Monster>().pushDir = directionF;
                                        e.GetComponent<Monster>().ChannelPush(4 * Time.deltaTime);
                                        // other.GetComponent<Monster>().BaseVel = other.GetComponent<Rigidbody>().velocity;
                                    }

                                    if (BHBool) //blackholescript
                                    {
                                        GameObject blackH = Instantiate(BlackHole, hit.point, transform.rotation, transform);
                                        blackH.transform.parent = null;
                                        blackH.transform.localScale = BHSize;
                                        blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                                        blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                                        blackH.GetComponent<GravityBody>().duration = BHDuration;
                                        BHBool = false;
                                    }

                                    if (pool) //poolscript
                                    {
                                        GameObject PoolObj = Instantiate(PoolInst, new Vector3(hit.point.x, 1, hit.point.z), PoolInst.transform.rotation, transform);
                                        PoolObj.transform.parent = null;
                                        PoolObj.transform.localScale = new Vector3(1, 2, 2);
                                        Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                                        curPool.TriggerKillMe(Poolduration);
                                        curPool.damage = PoolDamage;
                                        curPool.FrostBoltSlow = FrostBoltSlow;
                                        curPool.SlowPercent = SlowPercent;
                                        curPool.SlowDuration = SlowDuration;
                                        curPool.FireBallBurn = FireBallBurn;
                                        curPool.BurnPercent = BurnPercent;
                                        curPool.BurnDuration = BurnDuration;
                                        curPool.LBBounce = LBBounce;
                                        curPool.LBBounceAmount = LBBounceAmount;
                                        curPool.Unmodified = Unmodified;
                                        curPool.projectilespeed = projectilespeed;
                                        curPool.ghostCast = ghostCast;
                                        curPool.spellName = spellName;
                                        pool = false;
                                    }
                                }
                            }
                        }

                        if (dist <= chanRange && !ghostCast)
                        {
                            switch (hit.transform.gameObject.tag)
                            {
                                case "Wall":
                                    chanLoc = hit.point;

                                    //if (BHBool) //blackholescript
                                    //{
                                    //    GameObject blackH = Instantiate(BlackHole, hit.point, transform.rotation, transform);
                                    //    blackH.transform.parent = null;
                                    //    blackH.transform.localScale = BHSize;
                                    //    blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                                    //    blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                                    //    blackH.GetComponent<GravityBody>().duration = BHDuration;
                                    //    BHBool = false;
                                    //}
                                    //if (pool) //poolscript
                                    //{
                                    //    GameObject PoolObj = Instantiate(PoolInst, new Vector3(hit.point.x, 0.5f, hit.point.z), PoolInst.transform.rotation, transform);
                                    //    PoolObj.transform.parent = null;
                                    //    PoolObj.transform.localScale = new Vector3(1, 1, 1);
                                    //    Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                                    //    curPool.TriggerKillMe(Poolduration);
                                    //    curPool.damage = PoolDamage;
                                    //    curPool.FrostBoltSlow = FrostBoltSlow;
                                    //    curPool.SlowPercent = SlowPercent;
                                    //    curPool.SlowDuration = SlowDuration;
                                    //    curPool.FireBallBurn = FireBallBurn;
                                    //    curPool.BurnPercent = BurnPercent;
                                    //    curPool.BurnDuration = BurnDuration;
                                    //    curPool.LBBounce = LBBounce;
                                    //    curPool.LBBounceAmount = LBBounceAmount;
                                    //    curPool.Unmodified = Unmodified;
                                    //    curPool.projectilespeed = projectilespeed;
                                    //    curPool.ghostCast = ghostCast;
                                    //    curPool.spellName = spellName;
                                    //    pool = false;
                                    //}

                                    break;
                                case "Tree":
                                    chanLoc = hit.point;
                                    break;
                                case "Monster":
                                    chanLoc = hit.point;
                                    if (aoeSizeMeteor == 0)
                                    {
                                        Monster enemy = hit.transform.GetComponent<Monster>();

                                        enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                                        enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
                                        enemy.BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
                                        enemy.TakeDamage(damage * Time.deltaTime);


                                        if (Push)
                                        {
                                            enemy.GetComponent<Monster>().pushDir = directionF;
                                            enemy.GetComponent<Monster>().ChannelPush(4 * Time.deltaTime);
                                        }

                                        if (BHBool) //blackholescript
                                        {
                                            GameObject blackH = Instantiate(BlackHole, hit.point, transform.rotation, transform);
                                            blackH.transform.parent = null;
                                            blackH.transform.localScale = BHSize;
                                            blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                                            blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                                            blackH.GetComponent<GravityBody>().duration = BHDuration;
                                            BHBool = false;
                                        }
                                        if (pool) //poolscript
                                        {
                                            GameObject PoolObj = Instantiate(PoolInst, new Vector3(hit.point.x, 1, hit.point.z), PoolInst.transform.rotation, transform);
                                            PoolObj.transform.parent = null;
                                            PoolObj.transform.localScale = new Vector3(1, 2, 2);
                                            Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                                            curPool.TriggerKillMe(Poolduration);
                                            curPool.damage = PoolDamage;
                                            curPool.FrostBoltSlow = FrostBoltSlow;
                                            curPool.SlowPercent = SlowPercent;
                                            curPool.SlowDuration = SlowDuration;
                                            curPool.FireBallBurn = FireBallBurn;
                                            curPool.BurnPercent = BurnPercent;
                                            curPool.BurnDuration = BurnDuration;
                                            curPool.LBBounce = LBBounce;
                                            curPool.LBBounceAmount = LBBounceAmount;
                                            curPool.Unmodified = Unmodified;
                                            curPool.projectilespeed = projectilespeed;
                                            curPool.ghostCast = ghostCast;
                                            curPool.spellName = spellName;
                                            pool = false;
                                        }
                                    }
                                    break;

                                case "Illusion":
                                    break;
                                case "Ressing":
                                    break;
                                case "MirrorWall":
                                    if (ghostCast == false)
                                    {
                                        chanLoc = hit.point;
                                    }
                                    if (aoeSizeMeteor == 0)
                                    {
                                        MirrorWall enemy2 = hit.collider.GetComponent<MirrorWall>();
                                        enemy2.BoltBounce(LBBounce, channeling, Unmodified, gameObject, enemyCastingspell);
                                    }

                                    break;

                                default:
                                    ////chanLoc = spellCastLocation;
                                    break;
                            }
                        }

                        if (spellName != "Lightningbolt")
                        {
                            Transform result = gameObject.transform.Find("waves");
                            ParticleSystem.ShapeModule ps = result.GetComponent<ParticleSystem>().shape;

                            ps.shapeType = ParticleSystemShapeType.Box;


                            ps.scale = new Vector3(1f, 1f, (Vector3.Distance(chanLoc, transform.position + transform.forward * 1)-1.5f));
                            result.transform.localPosition = new Vector3(0, ((Vector3.Distance(chanLoc, transform.position)+3.5f) / 2), 0);

                            //   result.transform.localScale = new Vector3 (transform.position.x, transform.position.y, Vector3.Distance(chanLoc, transform.position) * 0.5f);

                            //lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z));
                            //lineRenderer.SetPosition(1, new Vector3(chanLoc.x, chanLoc.y, chanLoc.z));
                        }
                        else
                        {
                            if (aoeSizeMeteor == 0)
                            {
                                lightChild1.GetComponent<ParticleSystemRenderer>().lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                                lightChild2.GetComponent<ParticleSystemRenderer>().lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                                lightChild3.GetComponent<ParticleSystemRenderer>().lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                                lightChild4.GetComponent<ParticleSystemRenderer>().lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                                lightChild5.GetComponent<ParticleSystemRenderer>().lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                                lightChild6.GetComponent<ParticleSystemRenderer>().lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                            }

                        }


                    }
                }
                else // ChanCompCollider true
                {if (!ghostCast)
                    {
                        if (spellName != "Lightningbolt")
                        {
                            Transform result = gameObject.transform.Find("waves");
                            ParticleSystem.ShapeModule ps = result.GetComponent<ParticleSystem>().shape;
                            ps.shapeType = ParticleSystemShapeType.Box;
                            ps.scale = new Vector3(1f, 1f, 0.1f);
                            result.transform.localPosition = new Vector3(0, 0.1f, 0);
                        }
                        else
                        {
                            if (aoeSizeMeteor == 0)
                            {
                                lightChild1.GetComponent<ParticleSystemRenderer>().lengthScale = 0.1f;
                                lightChild2.GetComponent<ParticleSystemRenderer>().lengthScale = 0.1f;
                                lightChild3.GetComponent<ParticleSystemRenderer>().lengthScale = 0.1f;
                                lightChild4.GetComponent<ParticleSystemRenderer>().lengthScale = 0.1f;
                                lightChild5.GetComponent<ParticleSystemRenderer>().lengthScale = 0.1f;
                                lightChild6.GetComponent<ParticleSystemRenderer>().lengthScale = 0.1f;
                            }
                        }
                    }
                }
            }

        } // CompOrb end.
        if (CompOrb) // Testing
        {
            if (channeling && aoeSizeMeteor == 0)
            {
                OrbChannelTargetUpdate();

                if (CompColObject != null)
                {
                    CompOrbCheck(CompColObject);
                }
            }

            if (CompOrbCD_ <= 0 && chanDur_ <= 0)
            {

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
                float shortestDistance = Mathf.Infinity;
                GameObject nearestEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                }

                if (nearestEnemy != null && shortestDistance <= 50)
                {
                    spellCastLocation = nearestEnemy.transform.position;

                    Vector3 dir = nearestEnemy.transform.position - this.transform.localPosition;
                    transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

                    GameObject test123 = Instantiate(CompOrbReseveObject, this.transform);
                    SpellProjectile spell = test123.GetComponent<SpellProjectile>();

                    spell.chanDur = chanDur;
                    spell.CritVis = CritVis;
                    spell.CritVisCompOrb = gameObject;
                    spell.ChaosOrbReseveObject = ChaosOrbReseveObject;
                    spell.ChaosOrbDuration = ChaosOrbDuration;
                    spell.ChaosOrb_ = ChaosOrb_;
                    spell.ChaosOrbCD = ChaosOrbCD;
                    spell.BlessedAim = BlessedAim;
                    spell.CompOrbPlayer = true;
                    spell.ThePlayer = gameObject;
                    spell.CompOrb = false;
                    spell.projectilespeed = projectilespeed;
                    spell.damage = damage;
                    spell.spellCastLocation = spellCastLocation;
                    spell.aoeSizeMeteor = aoeSizeMeteor;
                    spell.ghostCast = ghostCast;
                    spell.cone = cone;
                    spell.spellName = spellName;
                    spell.channeling = channeling;
                    spell.FrostBoltSlow = FrostBoltSlow;
                    spell.SlowDuration = SlowDuration;
                    spell.SlowPercent = SlowPercent;
                    spell.FireBallBurn = FireBallBurn;
                    spell.BurnDuration = BurnDuration;
                    spell.BurnPercent = BurnPercent;
                    spell.LBBounce = LBBounce;
                    spell.LBBounceAmount = LBBounceAmount;
                    spell.BoostCrit = BoostCrit;
                    spell.CritChance = CritChance;
                    spell.CritDamage = CritDamage;
                    spell.BHBool = BHBool;
                    spell.BHSize = BHSize;
                    spell.BHRadius = BHRadius;
                    spell.BHDuration = BHDuration;
                    spell.BHStrenght = BHStrenght;
                    spell.Push = Push;
                    spell.pool = pool;
                    spell.PoolInst = PoolInst;
                    spell.PoolDamage = damage * PoolDamage;
                    spell.Poolduration = Poolduration;

                    CompColObject = spell;

                    if (channeling)
                    {
                        chanDur_ = chanDur;
                    }
                    CompOrbCD_ = CompOrbCD;
                    if (HastenBool)
                    {
                        var randomInt = Random.Range(0, 100);

                        if (randomInt <= HastenChance)
                        {
                            CompOrbCD_ = 0.15f;
                            GameObject Haste = Instantiate(HastenVis, transform);
                            Destroy(Haste, 2.5f);
                        }
                    }
                    
                }
                else
                {
                    CompOrbCD_ = 0.1f; // so spell wont go on CD if there is no enemy nearby., check again every 0.1 sec.
                }
            }
            chanDur_ -= Time.deltaTime;
            CompOrbCD_ -= Time.deltaTime;

        } else if (ChaosOrb_)
        {
            pos_ += Direction * Time.deltaTime * 7;
            transform.position = pos_;

        //    transform.Rotate(Vector3.up * Time.deltaTime * randomRotSpeed, Space.World);
            if (ChaosOrbCD_ <= 0)
            {

                var randomRotSpeed = Random.Range(0, 360);
                transform.rotation = Quaternion.Euler(0, randomRotSpeed, 0);

                if (aoeSizeMeteor > 0)
                {
                    var randomSpot = Random.Range(0.5f, 10f);
                    spellCastLocation = transform.position + transform.forward * randomSpot;
                }

                GameObject test123 = Instantiate(ChaosOrbReseveObject, this.transform);
                SpellProjectile spell = test123.GetComponent<SpellProjectile>();

                spell.BlessedAim = BlessedAim;
            spell.CompOrbPlayer = true;
            spell.ThePlayer = gameObject;
            spell.ChaosOrb_ = false;
                spell.CritVis = CritVis;
                spell.CritVisCompOrb = gameObject;
                spell.projectilespeed = projectilespeed;
            spell.damage = damage;
            spell.spellCastLocation = spellCastLocation;
            spell.aoeSizeMeteor = aoeSizeMeteor;
            spell.ghostCast = ghostCast;
            spell.cone = cone;
            spell.spellName = spellName;
            spell.channeling = channeling;
            spell.FrostBoltSlow = FrostBoltSlow;
            spell.SlowDuration = SlowDuration;
            spell.SlowPercent = SlowPercent;
            spell.FireBallBurn = FireBallBurn;
            spell.BurnDuration = BurnDuration;
            spell.BurnPercent = BurnPercent;
            spell.LBBounce = LBBounce;
            spell.LBBounceAmount = LBBounceAmount;
            spell.BoostCrit = BoostCrit;
            spell.CritChance = CritChance;
            spell.CritDamage = CritDamage;
            spell.BHBool = BHBool;
            spell.BHSize = BHSize;
            spell.BHRadius = BHRadius;
                spell.BHDuration = BHDuration;
            spell.BHStrenght = BHStrenght;
            spell.Push = Push;
            spell.pool = pool;
            spell.PoolInst = PoolInst;
            spell.PoolDamage = damage * PoolDamage;
            spell.Poolduration = Poolduration;
            ChaosOrbCD_ = ChaosOrbCD;
            }

        ChaosOrbCD_ -= Time.deltaTime;
        }

    }

    private void OrbChannelTargetUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance )
            {
                shortestDistance = distanceToEnemy;               
                nearestEnemy = enemy;                             
            }
        }

        if ((NearestEnemy_ == null || NearestEnemy_.tag != ("Monster") || shortestDistance < Distance_) && NewTargetTimer <= 0)
        {
            Distance_ = shortestDistance;
            NearestEnemy_ = nearestEnemy;
            NewTargetTimer = 0.25f;
        }

        if (NearestEnemy_ != null && Distance_ <= 50)
        {
           // spellCastLocation = nearestEnemy.transform.position;
            Vector3 dir = NearestEnemy_.transform.position - this.transform.localPosition;
            transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        }
        NewTargetTimer -= Time.deltaTime;
    }


    private void CompOrbCheck(SpellProjectile spell) // Checks if CompOrb is inside a collider (Raycast wont work if this is the case).
    {
        ColCounter = 0;
            Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (Collider col in cols)
            {
                if (col.tag == "Monster" && enemyCastingspell == false && aoeSizeMeteor == 0)
                {
                    spell.CompChanCollider = true;
                    ColCounter++;

                    Monster enemy = col.transform.GetComponent<Monster>();
                    enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                    enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, spell.damage * Time.deltaTime);
                    enemy.BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
                    enemy.TakeDamage(spell.damage * Time.deltaTime);
                    if (Push)
                    {
                        enemy.GetComponent<Monster>().pushDir = directionF;
                        enemy.GetComponent<Monster>().ChannelPush(4 * Time.deltaTime);
                    }
                    if (BHBool) //blackholescript
                    {
                        GameObject blackH = Instantiate(BlackHole, col.transform.position, transform.rotation, transform);
                        blackH.transform.parent = null;
                        blackH.transform.localScale = BHSize;
                        blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                        blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                        blackH.GetComponent<GravityBody>().duration = BHDuration;
                        BHBool = false;
                    }
                    if (pool) //poolscript
                    {
                        GameObject PoolObj = Instantiate(PoolInst, new Vector3(col.transform.position.x, 1, col.transform.position.z), PoolInst.transform.rotation, transform);
                        PoolObj.transform.parent = null;
                        PoolObj.transform.localScale = new Vector3(1, 2, 2);
                        Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                        curPool.TriggerKillMe(Poolduration);
                        curPool.damage = PoolDamage;
                        curPool.FrostBoltSlow = FrostBoltSlow;
                        curPool.SlowPercent = SlowPercent;
                        curPool.SlowDuration = SlowDuration;
                        curPool.FireBallBurn = FireBallBurn;
                        curPool.BurnPercent = BurnPercent;
                        curPool.BurnDuration = BurnDuration;
                        curPool.LBBounce = LBBounce;
                        curPool.LBBounceAmount = LBBounceAmount;
                        curPool.Unmodified = Unmodified;
                        curPool.projectilespeed = projectilespeed;
                        curPool.ghostCast = ghostCast;
                        curPool.spellName = spellName;
                        pool = false;
                    }             
                }
            }
        if (ColCounter == 0)
        {
            spell.CompChanCollider = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster" && enemyCastingspell == false && ((channeling && cone || channeling && aoeSizeMeteor > 0)) && tag != "CompOrb")
        {
            other.GetComponent<Monster>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
            other.GetComponent<Monster>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
            other.GetComponent<Monster>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
            other.GetComponent<Monster>().TakeDamage(damage * Time.deltaTime);

            if (BHBool) //blackholescript
            {
                GameObject blackH = Instantiate(BlackHole, other.transform.position, transform.rotation, transform);
                blackH.transform.parent = null;
                blackH.transform.localScale = BHSize;
                blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                blackH.GetComponent<GravityBody>().duration = BHDuration;
                BHBool = false;
            }

            if (pool) //poolscript
            {
                GameObject PoolObj = Instantiate(PoolInst, new Vector3(other.transform.position.x, 1, other.transform.position.z), PoolInst.transform.rotation, transform);
                PoolObj.transform.parent = null;
                PoolObj.transform.localScale = new Vector3(1, 2, 2);
                Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                curPool.TriggerKillMe(Poolduration);
                curPool.damage = PoolDamage;
                curPool.FrostBoltSlow = FrostBoltSlow;
                curPool.SlowPercent = SlowPercent;
                curPool.SlowDuration = SlowDuration;
                curPool.FireBallBurn = FireBallBurn;
                curPool.BurnPercent = BurnPercent;
                curPool.BurnDuration = BurnDuration;
                curPool.LBBounce = LBBounce;
                curPool.LBBounceAmount = LBBounceAmount;
                curPool.Unmodified = Unmodified;
                curPool.projectilespeed = projectilespeed;
                curPool.ghostCast = ghostCast;
                curPool.spellName = spellName;
                pool = false;
            }

            if (Push && aoeSizeMeteor == 0)
            {
                other.GetComponent<Monster>().pushDir = directionF;
                other.GetComponent<Monster>().ChannelPush(4 * Time.deltaTime);
            }
            if (Push && aoeSizeMeteor > 0)
            {
                directionF = (other.transform.position - transform.position).normalized;
                if (Push)
                {
                    other.GetComponent<Monster>().pushDir = directionF;
                    other.GetComponent<Monster>().ChannelPush(4 * Time.deltaTime);
                    // other.GetComponent<Monster>().BaseVel = other.GetComponent<Rigidbody>().velocity;
                }
            }
        }

        if (other.tag == "MirrorWall" && channeling && cone)
        {
            other.GetComponent<MirrorWall>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, enemyCastingspell);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (aoeSizeMeteor >= 1 && !channeling)
        {
            if (other.tag == "Floor" || other.tag == "Wall")
            {
                Meteorattack();

            }
        }
        else
        {
            if (other.tag == "Monster" && enemyCastingspell == false && !channeling && tag != "ChaosOrb" && tag != "CompOrb")
            {

                //  other.GetComponent<Rigidbody>().AddForce(directionF * 1000); // PUSH CODE
                //other.GetComponent<Rigidbody>().velocity = (directionF * 10);
                if (Push)
                {
                    other.GetComponent<Monster>().pushDir = directionF;
                    other.GetComponent<Monster>().pushed = true;
                   // other.GetComponent<Monster>().BaseVel = other.GetComponent<Rigidbody>().velocity;
                }

                other.GetComponent<Monster>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                other.GetComponent<Monster>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage);
                other.GetComponent<Monster>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
                other.GetComponent<Monster>().TakeDamage(damage);

                if (ghostCast) // TEST Ghostcast nerf.
                {
                    damage *= 0.9f;
                }

                if (TempTestTarget !=null) { //Blessed aim testing
                    TempTestTarget.GetComponent<Monster>().chainList.Add(chainID);
                }

                if (BHBool) //blackholescript
                {
                    GameObject blackH = Instantiate(BlackHole, transform.position, transform.rotation, transform);
                    blackH.transform.parent = null;
                    blackH.transform.localScale = BHSize;
                    blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                    blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                    blackH.GetComponent<GravityBody>().duration = BHDuration;
                    BHBool = false;
                }

                if (pool) //poolscript
                {
                    GameObject PoolObj = Instantiate(PoolInst, new Vector3(transform.position.x, 1, transform.position.z), PoolInst.transform.rotation, transform);
                    PoolObj.transform.parent = null;
                    PoolObj.transform.localScale = new Vector3(1, 2, 2);
                    Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                    curPool.TriggerKillMe(Poolduration);
                    curPool.damage = PoolDamage;
                    curPool.FrostBoltSlow = FrostBoltSlow;
                    curPool.SlowPercent = SlowPercent;
                    curPool.SlowDuration = SlowDuration;
                    curPool.FireBallBurn = FireBallBurn;
                    curPool.BurnPercent = BurnPercent;
                    curPool.BurnDuration = BurnDuration;
                    curPool.LBBounce = LBBounce;
                    curPool.LBBounceAmount = LBBounceAmount;
                    curPool.Unmodified = Unmodified;
                    curPool.projectilespeed = projectilespeed;
                    curPool.ghostCast = ghostCast;
                    curPool.spellName = spellName;
                    pool = false;
                }




            }

            if (other.tag == "MirrorWall" && !channeling && tag != "ChaosOrb" && tag !="CompOrb")
            {
                other.GetComponent<MirrorWall>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, enemyCastingspell);
                if (!ghostCast)
                {
                    Stop();
                }
            }
            

                if (other.tag == "Player" && enemyCastingspell == true)
            {
                other.GetComponent<Player>().TakeDamage(damage); // ToDo create player slow aswell.
                other.GetComponent<Player>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage);
                other.GetComponent<Player>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                Stop();
            }
        }

        if (other.tag == "Wall" && tag == "ChaosOrb")
        {
            Stop();
        }

        if ((other.tag != "Monster" && other.tag != "Player" && other.tag != "Illusion" && other.tag != "Ressing" && other.tag != "MirrorWall" && other.tag != "Untagged") && cone == false && !channeling ) 
        {
            Stop();

            //if (BHBool) //blackholescript
            //{
            //    GameObject blackH = Instantiate(BlackHole, transform.position, transform.rotation, transform);
            //    blackH.transform.parent = null;
            //    blackH.transform.localScale = BHSize;
            //    blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
            //    blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
            //    blackH.GetComponent<GravityBody>().duration = BHDuration;
            //    BHBool = false;
            //}

            //if (pool) //poolscript
            //{
            //    GameObject PoolObj = Instantiate(PoolInst, new Vector3(transform.position.x, 0.5f, transform.position.z), PoolInst.transform.rotation, transform);
            //    PoolObj.transform.parent = null;
            //    PoolObj.transform.localScale = new Vector3(1, 1, 1);


            //    Poolscript curPool = PoolObj.GetComponent<Poolscript>();
            //    curPool.TriggerKillMe(Poolduration);
            //    curPool.damage = PoolDamage;
            //    curPool.FrostBoltSlow = FrostBoltSlow;
            //    curPool.SlowPercent = SlowPercent;
            //    curPool.SlowDuration = SlowDuration;
            //    curPool.FireBallBurn = FireBallBurn;
            //    curPool.BurnPercent = BurnPercent;
            //    curPool.BurnDuration = BurnDuration;
            //    curPool.LBBounce = LBBounce;
            //    curPool.LBBounceAmount = LBBounceAmount;
            //    curPool.Unmodified = Unmodified;
            //    curPool.projectilespeed = projectilespeed;
            //    curPool.ghostCast = ghostCast;
            //    curPool.spellName = spellName;
            //    pool = false;
            //}
        }

        if (other.tag == "Monster" && !ghostCast && cone == false && enemyCastingspell == false && aoeSizeMeteor == 0 && tag != "ChaosOrb" && tag != "CompOrb")
        {
            Stop();

            if (BHBool) //blackholescript
            {
                GameObject blackH = Instantiate(BlackHole, transform.position, transform.rotation, transform);
                blackH.transform.parent = null;
                blackH.transform.localScale = BHSize;
                blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                blackH.GetComponent<GravityBody>().duration = BHDuration;
                BHBool = false;
            }
            if (pool) //poolscript
            {
                GameObject PoolObj = Instantiate(PoolInst, new Vector3(transform.position.x, 1, transform.position.z), PoolInst.transform.rotation, transform);
                PoolObj.transform.parent = null;
                PoolObj.transform.localScale = new Vector3(1, 2, 2);
                Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                curPool.TriggerKillMe(Poolduration);
                curPool.damage = PoolDamage;
                curPool.FrostBoltSlow = FrostBoltSlow;
                curPool.SlowPercent = SlowPercent;
                curPool.SlowDuration = SlowDuration;
                curPool.FireBallBurn = FireBallBurn;
                curPool.BurnPercent = BurnPercent;
                curPool.BurnDuration = BurnDuration;
                curPool.LBBounce = LBBounce;
                curPool.LBBounceAmount = LBBounceAmount;
                curPool.Unmodified = Unmodified;
                curPool.projectilespeed = projectilespeed;
                curPool.ghostCast = ghostCast;
                curPool.spellName = spellName;
                pool = false;
            }
        }
        

    }
    public void Meteorattack()
    {
        
            Collider[] cols = Physics.OverlapSphere(transform.position, aoeSizeMeteor);

            foreach (Collider c in cols)
            {
                if (!enemyCastingspell)
                {
                    Monster e = c.GetComponent<Monster>();
                    if (e != null)
                    {

                        directionF = (e.transform.position - transform.position).normalized;

                        e.GetComponent<Monster>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                        e.GetComponent<Monster>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage);
                        e.GetComponent<Monster>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
                        e.GetComponent<Monster>().TakeDamage(damage);

                        if (TempTestTarget != null)
                        { //Blessed aim testing, So blessed aim boltbounce wont attack current target.
                            TempTestTarget.GetComponent<Monster>().chainList.Add(chainID);
                        }


                        if (Push)
                        {
                            e.GetComponent<Monster>().pushDir = directionF;
                            e.GetComponent<Monster>().pushed = true;
                            // other.GetComponent<Monster>().BaseVel = other.GetComponent<Rigidbody>().velocity;
                        }

                        if (BHBool) //blackholescript
                        {
                            GameObject blackH = Instantiate(BlackHole, transform.position, transform.rotation, transform);
                            blackH.transform.parent = null;
                            blackH.transform.localScale = BHSize;
                            blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
                            blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
                            blackH.GetComponent<GravityBody>().duration = BHDuration;
                            BHBool = false;
                        }

                        if (pool) //poolscript
                        {
                            GameObject PoolObj = Instantiate(PoolInst, new Vector3(transform.position.x, 1, transform.position.z), PoolInst.transform.rotation, transform);
                            PoolObj.transform.parent = null;
                            PoolObj.transform.localScale = new Vector3(1, 2, 2);
                            Poolscript curPool = PoolObj.GetComponent<Poolscript>();
                            curPool.TriggerKillMe(Poolduration);
                            curPool.damage = PoolDamage;
                            curPool.FrostBoltSlow = FrostBoltSlow;
                            curPool.SlowPercent = SlowPercent;
                            curPool.SlowDuration = SlowDuration;
                            curPool.FireBallBurn = FireBallBurn;
                            curPool.BurnPercent = BurnPercent;
                            curPool.BurnDuration = BurnDuration;
                            curPool.LBBounce = LBBounce;
                            curPool.LBBounceAmount = LBBounceAmount;
                            curPool.Unmodified = Unmodified;
                            curPool.projectilespeed = projectilespeed;
                            curPool.ghostCast = ghostCast;
                            curPool.spellName = spellName;
                            pool = false;
                        }
                    }
                }
                else
                {
                    Player e = c.GetComponent<Player>();
                    if (e != null)
                    {
                        directionF = (e.transform.position - transform.position).normalized;
                        e.GetComponent<Player>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                        e.GetComponent<Player>().TakeDamage(damage);
                    }
                }
            }

            

            if (enemyCastingspell)//only for bigboy atm
            {
                Instantiate(BigBoyFrost, transform.position, transform.rotation);
            }

            if (spellName != "Lightningbolt")
            {
                if (transform.childCount == 5)
                {
                    GameObject Boom = transform.GetChild(4).gameObject;
                    Boom.SetActive(true);
                    Boom.transform.parent = null;
                    Destroy(Boom, 2);
                }
            }
            else
            {
                GameObject Boom = transform.GetChild(2).gameObject;
                Boom.SetActive(true);
                Boom.transform.parent = null;
                Destroy(Boom, 2);
            }


            Stop();
        

    }


    public void Stop()
    {

        if (ChaosOrb_ && !CompOrb)
        {
            if (transform.childCount == 3)
            {
                GameObject Implode = transform.GetChild(2).gameObject;
                Implode.SetActive(true);
                Implode.transform.parent = null;
                Destroy(Implode, 0.99f);
            }
        }

        if (!cone && !channeling && !CompOrb && !ChaosOrb_)
        {
            GameObject effectOne = Instantiate(effect1, this.transform);
            GameObject effectTwo = Instantiate(effect2, this.transform);

            effectOne.SetActive(true);
            effectTwo.SetActive(true);

            effectOne.transform.parent = null;
            effectTwo.transform.parent = null;
            Destroy(effectOne, 1);
            Destroy(effectTwo, 1);
        }
        if (channeling && !CompOrb && !CompOrbPlayer && !ChaosOrb_)
        {
            ThePlayer.GetComponent<Player>().StopChanneling();
        }
        Destroy(gameObject);
    }
}
