using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellProjectile : MonoBehaviour
{

    public GameObject HitVisualEffect;
    public GameObject SecondaryVisualEffect;
    public float SecondaryVisualEffectDuration;
    private float VisualEffectDuration = 1;
    //  public GameObject FireCone;
    private bool ChanMetorCone;

    private Vector3 pos_;
    private Vector3 Direction;
    private Vector3 cone123;
    private Quaternion coneQuat;
    private int GhostCastCharges = 10;
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
    [HideInInspector] public bool MageBossMeteor;
    [HideInInspector] public bool MageBossMeteorReal;
    [HideInInspector] public GameObject MageBossCircle;
    [HideInInspector] public Monster MageBoss;
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
    private ParticleSystemRenderer lc1;
    private ParticleSystemRenderer lc2;
    private ParticleSystemRenderer lc3;
    private ParticleSystemRenderer lc4;
    private ParticleSystemRenderer lc5;
    private ParticleSystemRenderer lc6;

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
    public Player ActualPlayer;
    private float GameTime;
    public bool SineWaveAttack;
    private float randomFreq;
    private float randomMag;
    public bool FireTrailCone;
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            ActualPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            ActualPlayer.SpellsCastInThisRoom.Add(gameObject);
        }
        if (!CompOrbPlayer)
        {     
         // Problem with setting all spells to current room. Mostly with comp/channeling/blessed aim.
            ThePlayer = GameObject.FindGameObjectWithTag("Player");

            if (!enemyCastingspell && !ChaosOrb_)
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    ThePlayer.GetComponent<Player>().curSpellProjectile.Add(gameObject);
                }
            }
        }

        randomFreq = Random.Range(13, 17);
        randomMag = Random.Range(1.5f, 2.5f);


        if (CompOrb)
        {
            transform.position = new Vector3(spellCastLocation.x, 1.5f, spellCastLocation.z);
            transform.parent = null;
            Invoke("Stop", CompOrbDur);
            return;
        }
        if (ChaosOrb_)
        {
            transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            pos_ = transform.position;
            Direction = transform.forward;
            transform.parent = null;
            Invoke("Stop", ChaosOrbDuration);
            return;
        }
        if (!LBCopy) // for LB bouncing.
        {
            chainID = GetInstanceID().ToString();
        }

        if (BoostCrit)
        {
            Crit();
        }

        directionF = transform.forward;

        if (aoeSizeMeteor == 0)
        {
            pos_ = transform.position;
        }

        if (channeling)
        {
            SpellIsChanneling();
        }

        if (!channeling || aoeSizeMeteor > 0)
        {
            if (!FireTrailCone)
            {
                transform.parent = null;
            }
        }

        if (aoeSizeMeteor > 0)
        {
            SpellIsMeteor();
        }

        if (cone == true && channeling == false)
        {
            SpellIsCone();
        }

        Invoke("Stop", 5);

    }

    public void ChainTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster"); // Could i pick up active monsters from Room monster list? Wouldn't work with Ressing Or BigBoy Brother.
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            struck = false;
            listCount = enemy.GetComponent<Monster>().chainList.Count; //GETCOMP IN Update

            for (int a = 0; a < listCount; a++)
            {
                if (enemy.GetComponent<Monster>().chainList[a] == chainID) //GETCOMP IN Update
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
        if (CompOrb)
        {
            CompOrbAttack();
            return;
        }
        if (ChaosOrb_)
        {
            ChaosOrbAttack();
            return;
        }
        if (!channeling)
        {
            if (aoeSizeMeteor == 0 && cone == false)
            {
                NormalProjectileAttack();
                return;
            }

            if (aoeSizeMeteor > 0)
            {
                NormalMeteorAttack();
                return;
            }

            if (cone && BlessedAim)
            {
                pos_ += transform.forward * Time.deltaTime * projectilespeed / 3;
                ChainTarget();
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
                return;
            }
        }

        if (channeling && !cone && (aoeSizeMeteor == 0 || spellName == "Lightningbolt"))
        {
            ChannelingAttack();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster" && enemyCastingspell == false && ((channeling && cone || channeling && aoeSizeMeteor > 0)) && tag != "CompOrb")
        {
            Monster enemy = other.GetComponent<Monster>();
            DealDamageOverTime(enemy);     
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
            if (other.tag == "Floor" || other.tag == "Wall" || other.tag == "MageBossDeadGolem")
            {
                Meteorattack();
            }
        }
        else
        {
            if (other.tag == "Monster" && enemyCastingspell == false && !channeling && tag != "ChaosOrb" && tag != "CompOrb")
            {
                Monster enemy = other.GetComponent<Monster>();
                DealDamageOnce(enemy);                
            }
            if (other.tag == "MirrorWall" && !channeling && tag != "ChaosOrb" && tag != "CompOrb")
            {
                other.GetComponent<MirrorWall>().BoltBounce(LBBounce, channeling, Unmodified, gameObject, enemyCastingspell);
                if (!ghostCast)
                {
                    Stop();
                }
            }
            if (other.tag == "Player" && enemyCastingspell == true)
            {
                other.GetComponent<Player>().TakeDamage(damage);
                other.GetComponent<Player>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage);
                other.GetComponent<Player>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                if (!cone)
                {
                    Stop();
                }
            }
        }
        if ((other.tag == "Wall" || other.tag == "MageBossDeadGolem") && tag == "ChaosOrb")
        {
            Stop();
        }
        if ((other.tag != "Monster" && other.tag != "Player" && other.tag != "Illusion" && other.tag != "Ressing" && other.tag != "MirrorWall" && other.tag != "Untagged") && cone == false && !channeling)
        {
          Stop(); // :( apparently needed
        }
    }
    public void Meteorattack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, aoeSizeMeteor);
        foreach (Collider c in cols)
        { 

            if (!enemyCastingspell || MageBossMeteor)
            {
                Monster e = c.GetComponent<Monster>();
                if (e != null && e.tag == "Monster")
                {
                    Monster enemy = e.GetComponent<Monster>();
                    DealDamageOnce(enemy);                 
                }
            }
            if (enemyCastingspell || MageBossMeteor)
            {
                Player e = c.GetComponent<Player>();
                if (e != null && e.tag == "Player")
                {
                    e.GetComponent<Player>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
                    e.GetComponent<Player>().TakeDamage(damage);
                }
            }

            if (MageBossMeteor && c.tag == "MageBossDeadGolem")
            {
                if (c.gameObject.transform.GetChild(0).gameObject.GetComponent<MonsterAnim>() != null)
                {
                    c.gameObject.transform.GetChild(0).gameObject.GetComponent<MonsterAnim>().StoneGolemCrumble();
                }
                MageBoss.Rocks.Remove(c.gameObject);
                Destroy(c.gameObject, 2f);
            }

        }
        if (enemyCastingspell)//only for bigboy atm
        {
        GameObject BigBoyFrostEffect = Instantiate(BigBoyFrost, transform.position, transform.rotation);
        ActualPlayer.SpellsCastInThisRoom.Add(BigBoyFrostEffect);

        }
        Stop();
    }

    void DealDamageOnce(Monster enemy)
    {
        enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
        enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, damage);
        enemy.BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
        enemy.TakeDamage(damage);
        if (ghostCast)
        {
            damage *= 0.9f;

            if (GhostCastCharges <= 0)
            {
                Stop();
            }
            GhostCastCharges--;
        }
        if (TempTestTarget != null)
        {
            TempTestTarget.GetComponent<Monster>().chainList.Add(chainID);
        }
        if (Push)
        {
            if (aoeSizeMeteor > 0)
            {
                directionF = (enemy.transform.position - transform.position).normalized;
            }
            enemy.CancelInvoke("StopPush");
            enemy.pushDir = directionF;
            enemy.pushed = true;
        }
        if (BHBool)
        {
            GameObject blackH = Instantiate(BlackHole, transform.position, transform.rotation, transform);
            ActualPlayer.SpellsCastInThisRoom.Add(blackH);
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
            ActualPlayer.SpellsCastInThisRoom.Add(PoolObj);
            PoolObj.transform.parent = null;
            PoolObj.transform.localScale = new Vector3(2, 2, 2);
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
        if (!ghostCast && aoeSizeMeteor == 0 && !cone)
        {
            Stop();
        }
    }
    
     void DealDamageOverTime(Monster enemy)
     {
        enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
        enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
        enemy.BoltBounce(LBBounce, channeling, Unmodified, gameObject, chainID);
        enemy.TakeDamage(damage * Time.deltaTime);
        if (Push)
        {
            if (aoeSizeMeteor > 0)
            {
                directionF = (enemy.transform.position - transform.position).normalized;
            }
            enemy.pushDir = directionF;
            enemy.ChannelPush(4 * Time.deltaTime);
        }
        if (BHBool)
        {
            GameObject blackH = Instantiate(BlackHole, enemy.transform.position, transform.rotation, transform);
            ActualPlayer.SpellsCastInThisRoom.Add(blackH);
            blackH.transform.parent = null;
            blackH.transform.localScale = BHSize;
            blackH.GetComponent<GravityBody>().pullRadius = BHRadius;
            blackH.GetComponent<GravityBody>().pullForce = BHStrenght;
            blackH.GetComponent<GravityBody>().duration = BHDuration;
            BHBool = false;
        }
        if (pool)
        {
            GameObject PoolObj = Instantiate(PoolInst, new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z), PoolInst.transform.rotation, transform);
            ActualPlayer.SpellsCastInThisRoom.Add(PoolObj);
            PoolObj.transform.parent = null;
            PoolObj.transform.localScale = new Vector3(2, 2, 2);
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

    private void CompOrbAttack()
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
    }

    private void ChaosOrbAttack()
    {
        pos_ += Direction * Time.deltaTime * 7;
        transform.position = pos_;
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

    private void OrbChannelTargetUpdate()
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
                DealDamageOverTime(enemy);
            }
        }
        if (ColCounter == 0)
        {
            spell.CompChanCollider = false;
        }
    }

    void NormalProjectileAttack()
    {
        pos_ += transform.forward * Time.deltaTime * projectilespeed;

        if (SineWaveAttack)
        {
            GameTime += Time.deltaTime;
            Vector3 pos_2 = transform.up * Time.deltaTime * projectilespeed;
            transform.position = pos_ + pos_2 + transform.right * Mathf.Sin(GameTime * randomFreq) * randomMag;
        }
        else
        {
            transform.position = pos_;
        }




        if (!enemyCastingspell && BlessedAim)
        {
            ChainTarget();
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

    void NormalMeteorAttack()
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
        if (dir.magnitude <= (projectilespeed / 2.5f) * Time.deltaTime) 
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

    void ChannelingAttack()
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
            else
            {
                pos_.y = 0;
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
            gameObject.transform.position = ThePlayer.transform.position + new Vector3(0, pos_.y, 0f) + ThePlayer.transform.right * MultiChanPosX;
        }

        if (ghostCast) // script makes ghostcast work with channeling.
        {
            RaycastHit[] hits;
            var heading = chanLoc - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            hits = Physics.RaycastAll(transform.position, direction);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit2 = hits[i];
                float dist = Vector3.Distance(hit2.point, transform.position);
                if (dist <= chanRange)
                {
                    switch (hit2.transform.gameObject.tag)
                    {
                        case "Wall":
                            chanLoc = hit2.point;
                            break;
                        case "MageBossDeadGolem":
                            chanLoc = hit2.point;
                            break;
                        case "Monster":
                            if (!CompChanCollider)
                            {
                                Monster enemy = hit2.collider.GetComponent<Monster>();
                                DealDamageOverTime(enemy);
                            }
                            break;
                        case "Illusion":
                            break;
                        case "Ressing":
                            break;
                        case "MirrorWall":
                            MirrorWall enemy2 = hit2.collider.GetComponent<MirrorWall>();
                            enemy2.BoltBounce(LBBounce, channeling, Unmodified, gameObject, enemyCastingspell);
                            break;
                        default:
                            chanLoc = spellCastLocation;
                            break;
                    }
                }
            }
        }
        if (!CompChanCollider)
        {
            RaycastHit hit;
            var heading = chanLoc - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                float dist = Vector3.Distance(hit.point, transform.position);
                if (spellName == "Lightningbolt" && aoeSizeMeteor == 0)
                {
                    lc1.lengthScale = dist * 0.80f;
                    lc2.lengthScale = dist * 0.80f;
                    lc3.lengthScale = dist * 0.80f;
                    lc4.lengthScale = dist * 0.80f;
                    lc5.lengthScale = dist * 0.80f;
                    lc6.lengthScale = dist * 0.80f;
                }
                if (aoeSizeMeteor > 0 && spellName == "Lightningbolt")
                {
                    Collider[] cols = Physics.OverlapSphere(hit.point, aoeSizeMeteor);
                    foreach (Collider c in cols)
                    {
                        Monster e = c.GetComponent<Monster>();
                        if (e != null)
                        {
                            DealDamageOverTime(e);
                        }
                    }
                }
                if (dist <= chanRange && !ghostCast)
                {
                    switch (hit.transform.gameObject.tag)
                    {
                        case "Wall":
                            chanLoc = hit.point;
                            break;
                        case "MageBossDeadGolem":
                            chanLoc = hit.point;
                            break;
                        case "Monster":
                            chanLoc = hit.point;
                            if (aoeSizeMeteor == 0)
                            {
                                Monster enemy = hit.transform.GetComponent<Monster>();
                                DealDamageOverTime(enemy);
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
                            break;
                    }
                }
                if (spellName != "Lightningbolt")
                {
                    Transform result = gameObject.transform.Find("waves");
                    ParticleSystem.ShapeModule ps = result.GetComponent<ParticleSystem>().shape;
                    ps.shapeType = ParticleSystemShapeType.Box;
                    ps.scale = new Vector3(1f, 1f, (Vector3.Distance(chanLoc, transform.position + transform.forward * 1) - 1.5f));
                    result.transform.localPosition = new Vector3(0, ((Vector3.Distance(chanLoc, transform.position) + 3.5f) / 2), 0);
                }
                else
                {
                    if (aoeSizeMeteor == 0)
                    {
                        lc1.lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                        lc2.lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                        lc3.lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                        lc4.lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                        lc5.lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                        lc6.lengthScale = Vector3.Distance(chanLoc, transform.position) * 0.6f;
                    }
                }
            }
        }
        else // ChanCompCollider true
        {
            if (!ghostCast)
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
                        lc1.lengthScale = 0.1f;
                        lc2.lengthScale = 0.1f;
                        lc3.lengthScale = 0.1f;
                        lc4.lengthScale = 0.1f;
                        lc5.lengthScale = 0.1f;
                        lc6.lengthScale = 0.1f;
                    }
                }
            }
        }
    }

    void SpellIsChanneling()
    {
        Invoke("Stop", chanDur);
        chanLoc = spellCastLocation;
        chanRange = 25;
        if (!CompOrbPlayer)
        {
            ThePlayer.GetComponent<Player>().Channeling();
            pos_.y = 1.5f;
        }
        if (spellName == "Lightningbolt")
        {
            lc1 = lightChild1.GetComponent<ParticleSystemRenderer>();
            lc2 = lightChild2.GetComponent<ParticleSystemRenderer>();
            lc3 = lightChild3.GetComponent<ParticleSystemRenderer>();
            lc4 = lightChild4.GetComponent<ParticleSystemRenderer>();
            lc5 = lightChild5.GetComponent<ParticleSystemRenderer>();
            lc6 = lightChild6.GetComponent<ParticleSystemRenderer>();
        }
        if (aoeSizeMeteor == 0)
        {
            this.transform.parent = ThePlayer.transform;
            directionF = ThePlayer.transform.forward;
            chanLoc = pos_;
            spellCastLocation = pos_;
            if (!cone)
            {
                pos_ += transform.forward * chanRange;
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
                if (spellName == "Lightningbolt")
                {
                   // transform.GetComponent<BoxCollider>().enabled = false;

                    lightChild1.transform.localScale = new Vector3(lightChild1.transform.localScale.x, 1.5f, lightChild1.transform.localScale.z);
                    lightChild2.transform.localScale = new Vector3(lightChild2.transform.localScale.x, 1.5f, lightChild2.transform.localScale.z);
                    lightChild3.transform.localScale = new Vector3(lightChild3.transform.localScale.x, 1.5f, lightChild3.transform.localScale.z); lightChild4.transform.localScale = new Vector3(lightChild4.transform.localScale.x, 1.5f, lightChild4.transform.localScale.z);
                    lightChild5.transform.localScale = new Vector3(lightChild5.transform.localScale.x, 1.5f, lightChild5.transform.localScale.z);
                    lightChild6.transform.localScale = new Vector3(lightChild6.transform.localScale.x, 1.5f, lightChild6.transform.localScale.z);
                }
            }
        }
        else if (spellName == "Lightningbolt")
        {
            transform.localScale = new Vector3(1, 1, 1);
            lc1.lengthScale = 2;
            lc2.lengthScale = 2;
            lc3.lengthScale = 2;
            lc4.lengthScale = 2;
            lc5.lengthScale = 2;
            lc6.lengthScale = 2;
            lightChild1.transform.localScale = new Vector3(2.5f, 1, 1);
            lightChild2.transform.localScale = new Vector3(2.5f, 1, 1);
            lightChild3.transform.localScale = new Vector3(2.5f, 1, 1);
            lightChild4.transform.localScale = new Vector3(2.5f, 1, 1);
            lightChild5.transform.localScale = new Vector3(2.5f, 1, 1);
            lightChild6.transform.localScale = new Vector3(2.5f, 1, 1);
            Vector3 Asd = lightChild1.transform.position;
            lightChild1.transform.position = new Vector3(Asd.x + 1, Asd.y + 1, Asd.z);
            lightChild2.transform.position = new Vector3(Asd.x - 1, Asd.y - 1, Asd.z);
            lightChild3.transform.position = new Vector3(Asd.x + 1, Asd.y, Asd.z);
            lightChild4.transform.position = new Vector3(Asd.x, Asd.y + 1, Asd.z);
            lightChild5.transform.position = new Vector3(Asd.x - 1, Asd.y, Asd.z);
            lightChild6.transform.position = new Vector3(Asd.x, Asd.y - 1, Asd.z);
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

    void SpellIsMeteor()
    {
        if (!channeling)
        {
            //if (spellName != "Lightningbolt")
            //{
            //    transform.localScale += new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);
            //    this.transform.Find("SubFire").gameObject.SetActive(false);
            //    this.transform.Find("SubFire").gameObject.SetActive(true);
            //}
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


            VisualEffectDuration = 1.3f;

        }
        Vector3 spellCastLocation_ = spellCastLocation;
        if (!channeling || spellName == "Lightningbolt")
        {
            if (!MageBossMeteor)
            {
                spellCastLocation_.y += 15f;
            }
            else
            {
                spellCastLocation_.y += 40f;
            }

        }
        else
        {
            spellCastLocation_.y = 3;
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

    void SpellIsCone()
    { //3,4,5. 
        if (!enemyCastingspell)
        {
            directionF = ThePlayer.transform.forward;
        }
        //else
        //{
        //    directionF = transform.forward;
        //}

        if (spellName == "Lightningbolt")
        {
           // transform.localScale += new Vector3(transform.localScale.x * 2f, transform.localScale.y * 3f, transform.localScale.z * 4f);
            Vector3 loc = transform.position;
            loc += transform.forward * 2.2f;
            transform.position = loc;
            //lightChild1.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
            //lightChild2.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
            //lightChild3.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
            //lightChild4.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
            //lightChild5.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
            //lightChild6.transform.localScale = new Vector3(0.8f, 0.6f, 0.4f);
        }
        else
        {
            Vector3 loc2 = transform.position;
            loc2 += transform.forward * 4f;
            transform.position = loc2;
        }
        Invoke("Stop", 1.2f);
    }

    public void Crit()
    {
        var randomInt = Random.Range(0, 100);
        if (randomInt <= CritChance)
        {
            damage *= CritDamage;
            if (!CompOrbPlayer) // Comporb testing, making sure the orb is the new "player" for channeling script. OBS! CHANGE! Make so the text can hover over the orb.
            {
                ThePlayer.GetComponent<Player>().CritVis();
            }
            else
            {
                GameObject Crit = Instantiate(CritVis, CritVisCompOrb.transform);
                Crit.transform.localPosition = new Vector3(0, 3, 0);
                Destroy(Crit, 1);
            }
        }
    }

    public void Stop()
    {
        if (ActualPlayer != null)
        {
            ActualPlayer.SpellsCastInThisRoom.Remove(gameObject);
        }

        if (HitVisualEffect != null)
        {
            GameObject effectOne = Instantiate(HitVisualEffect, this.transform);
           // ActualPlayer.SpellsCastInThisRoom.Add(effectOne);
            effectOne.SetActive(true);
            effectOne.transform.parent = null;
            Destroy(effectOne, VisualEffectDuration);
            if (MageBossMeteor && MageBossMeteorReal)
            {
                Destroy(MageBossCircle, 0.5f);
            }
        }
        if (SecondaryVisualEffect != null) // currently just frostbolt trail
        {
            SecondaryVisualEffect.transform.parent =null;
            Destroy(SecondaryVisualEffect, SecondaryVisualEffectDuration);

        }

        if (channeling && !CompOrb && !CompOrbPlayer && !ChaosOrb_)
        {
            ThePlayer.GetComponent<Player>().StopChanneling();
        }
        Destroy(gameObject);
    }
}

