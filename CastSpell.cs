using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSpell : MonoBehaviour
{


    public float baseCasttimeCD;
    public float spellSlotCD;
    public string spellname;
    public float projectilespeed;
    public float damage;
    public float aoeSizeMeteor;
    public bool ghostCast;
    public bool cone;
    public bool doubleCast;
    public int currentSlot;
    private float CD1;
    private float CD1_;
    private float CD2;
    private float CD2_;
    private float CD3;
    private float CD3_;
    private bool spellSlot1rdy;
    private bool spellSlot2rdy;
    private bool spellSlot3rdy;
    private bool curSlotcd;
    public float chanDur;
    public float chanDur1;
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Text coolDownTextDisplay1;
    public Text coolDownTextDisplay2;
    public Text coolDownTextDisplay3;
    public Text coolDownTextDisplay1_;
    public Text coolDownTextDisplay2_;
    public Text coolDownTextDisplay3_;
    public GameObject HastenVisual;
    public GameObject CritVisual;
    public GameObject CompOrbObject;
    public GameObject ChaosOrbObject;
    public GameObject currentspellObject;
    public GameObject currentspellObjectCompOrbReserve;
    public GameObject currentspellObjectChaosOrbReserve;
    public GameObject currentspellObjectChaosOrbReserve1;
    public GameObject currentConeObject;
    public GameObject currentChannel;
    public GameObject currentChannelCone;
    public GameObject currentMeteor;
    public GameObject currentChanMet;
    [HideInInspector]
    public Vector3 spellCastLocation;
    public GameObject currentspellObject1;
    public Vector3 spellCastLocation1;
    public string spellname1;
    public float projectilespeed1;
    public float damage1;
    public float aoeSizeMeteor1;
    public bool ghostCast1;
    public bool cone1;
    public bool channel1;
    public bool channel;
    public bool splitCast;
    public bool FrostBoltSlow;
    public float SlowPercent, SlowDuration;
    public bool FireBallBurn;
    public float BurnPercent, BurnDuration;
    public bool LBBounce;
    public int LBBounceAmount;

    public bool FrostBoltSlow1;
    public float SlowPercent1, SlowDuration1;
    public bool FireBallBurn1;
    public float BurnPercent1, BurnDuration1;
    public bool LBBounce1;
    public int LBBounceAmount1;

    public bool BoostCrit;
    public float CritChance, CritDamage;

    public bool BoostCrit1;
    public float CritChance1, CritDamage1;

    public bool HastenBool;
    public float HastenChance;

    //public float CDmodifier;
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

    public float BHDuration1;
    public Vector3 BHSize1;
    public float BHRadius1;
    public float BHStrenght1;
    public bool BHBool1;
    public bool Push1;
    public bool pool1;
    public float Poolduration1;
    public float PoolDamage1;
    public GameObject PoolInst1;

    public bool BlessedAim;
    public bool BlessedAim1;
    public bool CompOrb;
    public float CompOrbDur;
    public float CompOrbCD;
    public bool ChaosOrb_;
    public bool ChaosOrb1;
    public float ChaosOrbAttackCD;
    public float ChaosOrbDuration;
    public float ChaosOrbDuration1;

    public float damage1Pure = 0f;
    public float damage2Pure = 0f;
    public float damage3Pure = 0f;
    public float damage1Per = 1f;
    public float damage2Per = 1f;
    public float damage3Per = 1f;
    public float damage4Per = 1f;

    public float cd1Pure = 0f;
    public float cd2Pure = 0f;
    public float cd3Pure = 0f;
    public float cd4Pure = 0f;
    public float cd1Per = 1f;
    public float cd2Per = 1f;
    public float cd3Per = 1f;
    public float cd4Per = 1f;

    private float ChaosOrbCD1;

    public bool SplitChanLeft;
    public bool SplitChanRight;

    private bool MultiChan1;
    private bool MultiChan2;
    private bool MultiChan3;
    private int MultiChanCounter;
    public Player player_;

    public void CastCurrentSpell()
    {
        if (currentSlot == 1 && spellSlot1rdy == true)
        {
            curSlotcd = true;
        }
        if (currentSlot == 2 && spellSlot2rdy == true)
        {
            curSlotcd = true;

        }
        if (currentSlot == 3 && spellSlot3rdy == true)
        {
            curSlotcd = true;

        }

        if (curSlotcd)
        {
            player_.AttackAnim();
            curSlotcd = false;

            if (cone || channel || aoeSizeMeteor > 0)
            {
                if (cone)
                {
                    currentspellObject = currentConeObject;
                }
                if (channel)
                {
                    currentspellObject = currentChannel;
                }
                if (cone && channel)
                {
                    currentspellObject = currentChannelCone;
                }
                if (aoeSizeMeteor > 0 && !channel)
                {
                    currentspellObject = currentMeteor;
                }
                if (aoeSizeMeteor > 0 && spellname != "Lightningbolt" && channel)
                {
                    currentspellObject = currentChanMet;
                }
            }

            if (ChaosOrb_)
            {
                if (currentspellObject != currentspellObjectCompOrbReserve)
                {
                    currentspellObjectChaosOrbReserve = currentspellObject;
                    currentspellObjectChaosOrbReserve1 = currentspellObject;
                }
                currentspellObject = ChaosOrbObject;
            }

            if (CompOrb) // CompOrb Testing.
            {
                currentspellObjectCompOrbReserve = currentspellObject;
                currentspellObject = CompOrbObject;
            }

            damage = (damage1Pure + damage2Pure + damage3Pure) * damage1Per * damage2Per * damage3Per * damage4Per; // fixes the damage calculation order.
            spellSlotCD = (cd1Pure + cd2Pure + cd3Pure + cd4Pure) * cd1Per * cd2Per * cd3Per * cd4Per;


            GameObject test123 = Instantiate(currentspellObject, this.transform);
            SpellProjectile spell = test123.GetComponent<SpellProjectile>();


            spell.BlessedAim = BlessedAim;
            spell.ChaosOrbDuration = ChaosOrbDuration;
            spell.CompOrb = CompOrb;
            spell.CompOrbDur = CompOrbDur;
            spell.HastenBool = HastenBool;
            spell.HastenChance = HastenChance;
            spell.HastenVis = HastenVisual;
            spell.ChaosOrb_ = ChaosOrb_;
            spell.projectilespeed = projectilespeed;
            spell.damage = damage;
            spell.spellCastLocation = spellCastLocation;
            spell.aoeSizeMeteor = aoeSizeMeteor;
            spell.ghostCast = ghostCast;
            spell.cone = cone;
            spell.spellName = spellname;
            spell.channeling = channel;
            // Frostbolt slow effects
            spell.FrostBoltSlow = FrostBoltSlow;
            spell.SlowDuration = SlowDuration;
            spell.SlowPercent = SlowPercent;
            // Fireball burn effects
            spell.FireBallBurn = FireBallBurn;
            spell.BurnDuration = BurnDuration;
            spell.BurnPercent = BurnPercent;
            // LB Bounce effect
            spell.LBBounce = LBBounce;
            spell.LBBounceAmount = LBBounceAmount;
            //Boost
            spell.BoostCrit = BoostCrit;
            spell.CritChance = CritChance;
            spell.CritDamage = CritDamage;
            spell.CritVis = CritVisual;
            //BlackHole
            spell.BHBool = BHBool;
            spell.BHSize = BHSize;
            spell.BHRadius = BHRadius;
            spell.BHDuration = spellSlotCD * BHDuration;
            spell.BHStrenght = BHStrenght;
            //Push
            spell.Push = Push;
            //Pool
            spell.pool = pool;
            spell.PoolInst = PoolInst;
            //spell.Poolduration = Poolduration;
            spell.PoolDamage = damage * PoolDamage;
            spell.Poolduration = spellSlotCD * Poolduration;




            if (channel == true)
            {
                spell.chanDur = chanDur;
            }
            if (CD1 <= baseCasttimeCD)
            {
                CD1 = baseCasttimeCD;
                CD1_ = baseCasttimeCD;
            }
            if (CD2 <= baseCasttimeCD)
            {
                CD2 = baseCasttimeCD;
                CD2_ = baseCasttimeCD;
            }
            if (CD3 <= baseCasttimeCD)
            {
                CD3 = baseCasttimeCD;
                CD3_ = baseCasttimeCD;
            }

            if (player_.BlobWeaponEquppied)
            {
                player_.CastExtraBlob(player_.transform.rotation);
            }

            if (doubleCast)
            {
                currentspellObject1 = currentspellObject;
                projectilespeed1 = projectilespeed;
                damage1 = damage;
                spellCastLocation1 = spellCastLocation;
                aoeSizeMeteor1 = aoeSizeMeteor;
                ghostCast1 = ghostCast;
                cone1 = cone;
                spellname1 = spellname;
                channel1 = channel;
                FrostBoltSlow1 = FrostBoltSlow;
                SlowPercent1 = SlowPercent;
                SlowDuration1 = SlowDuration;
                FireBallBurn1 = FireBallBurn;
                LBBounce1 = LBBounce;
                LBBounceAmount1 = LBBounceAmount;
                BurnPercent1 = BurnPercent;
                BurnDuration1 = BurnDuration;
                BoostCrit1 = BoostCrit;
                CritChance1 = CritChance;
                CritDamage1 = CritDamage;
                chanDur1 = chanDur;
                ChaosOrbDuration1 = ChaosOrbDuration;

                //BlackHole
                BHBool1 = BHBool;
                BHSize1 = BHSize;
                BHRadius1 = BHRadius;
                BHDuration1 = spellSlotCD * BHDuration;
                BHStrenght1 = BHStrenght;
                //Push
                Push1 = Push;
                //Pool
                pool1 = pool;
                PoolInst1 = PoolInst;
                //spell.Poolduration = Poolduration;
                PoolDamage1 = damage * PoolDamage;
                Poolduration1 = spellSlotCD * Poolduration;
                // Blessed aim
                BlessedAim1 = BlessedAim;

                // Chaos
                ChaosOrb1 = ChaosOrb_;

                MultiChanCounter = 0;

                var MultiCast = Random.Range(1, 101);
                if (MultiCast > 50)
                {

                    Invoke("DoubleCastSpell1", 0.15f);
            //        player_.MultiVis(0);

                   StartCoroutine(MultiVisIE(0, 0.15f));
                }
                if (MultiCast > 75)
                {

                    Invoke("DoubleCastSpell1", 0.3f);
                    //  player_.MultiVis(1);
                    StartCoroutine(MultiVisIE(1, 0.3f));
                }
                if (MultiCast > 90)
                {

                    Invoke("DoubleCastSpell1", 0.45f);
                    //          player_.MultiVis(-1);
                    StartCoroutine(MultiVisIE(-1, 0.45f));
                }




            }
            if (splitCast)
            {
                float minC = 1f;
                float maxC = 25f;
                float spreadC = 36;

                if (cone) { maxC = 1f; minC = 1f; spreadC = 60f; }

                if (channel) { maxC = 1f; minC = 1f; spreadC = 30; };

                Vector3 dist = spellCastLocation - this.transform.position;
                float spread = Mathf.Clamp(dist.magnitude, minC, maxC);
                spread = -1 * (spread - spreadC);

                Quaternion dir = transform.rotation;

                //Quaternion dir = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
                Vector3 rot = dir.eulerAngles;
                rot = new Vector3(rot.x, rot.y + spread, rot.z);
                Vector3 rot2 = dir.eulerAngles;
                rot2 = new Vector3(rot2.x, rot2.y - spread, rot2.z);

                Quaternion targetRotation1 = Quaternion.Euler(rot);
                Quaternion targetRotation2 = Quaternion.Euler(rot2);

                Vector3 spellLoc1 = spellCastLocation + (test123.transform.right * 4f);
                Vector3 spellLoc2 = spellCastLocation + (test123.transform.right * -4f);

                SplitCastSpell(spellLoc1, targetRotation1, 2);
                SplitCastSpell(spellLoc2, targetRotation2, 1);

            }

            if (ChaosOrb_) // Testing.
            {
                spell.ChaosOrbReseveObject = currentspellObjectChaosOrbReserve;
                spell.ChaosOrbCD = spellSlotCD * ChaosOrbAttackCD; //WILL BE LONGER, takes extra cd from Chaos ability itself.
                ChaosOrbCD1 = spellSlotCD * ChaosOrbAttackCD;
                currentspellObject = currentspellObjectChaosOrbReserve;
            }

            if (CompOrb) // Testing.
            {
                spell.CompOrbReseveObject = currentspellObjectCompOrbReserve;
                spell.CompOrbCD = spellSlotCD;
                currentspellObject = currentspellObjectCompOrbReserve;
            }

            if (CompOrb) // teesting.
            {
                spellSlotCD = CompOrbCD;
            }     
            SetSlotCD(currentSlot);    // CompanionOrb, fixed CD. fix
        }else if(!player_.channelingNow && !player_.attackingRightNow)
        {
            player_.PlayerIsIdle();
        }
    }

    IEnumerator MultiVisIE(float Position, float delay)
    {
        yield return new WaitForSeconds(delay);
        player_.MultiVis(Position);
    }




    public void DoubleCastSpell1()
    {

        GameObject test1234 = Instantiate(currentspellObject1, this.transform);
        //test1234.GetComponentInChildren<GameObject>().transform.Find("SubFire").gameObject.SetActive(false);
        SpellProjectile spell2 = test1234.GetComponent<SpellProjectile>();

        MultiChanCounter++;

        if (player_.BlobWeaponEquppied)
        {
            player_.CastExtraBlob(player_.transform.rotation);
        }

        switch (MultiChanCounter)
        {
            case 1:
                MultiChan1 = true;
                MultiChan2 = false;
                MultiChan3 = false;
                break;
            case 2:
                MultiChan1 = false;
                MultiChan2 = true;
                MultiChan3 = false;
                break;
            case 3:
                MultiChan1 = false;
                MultiChan2 = false;
                MultiChan3 = true;
                break;

        }


        spell2.MultiChan1 = MultiChan1;
        spell2.MultiChan2 = MultiChan2;
        spell2.MultiChan3 = MultiChan3;
        spell2.projectilespeed = projectilespeed1;
        spell2.damage = damage1;
        spell2.spellCastLocation = spellCastLocation1;
        spell2.aoeSizeMeteor = aoeSizeMeteor1;
        spell2.ghostCast = ghostCast1;
        spell2.cone = cone1;
        spell2.channeling = channel1;
        spell2.chanDur = chanDur1;
        spell2.spellName = spellname1;
        spell2.FrostBoltSlow = FrostBoltSlow1;
        spell2.SlowPercent = SlowPercent1;
        spell2.SlowDuration = SlowDuration1;
        spell2.FireBallBurn = FireBallBurn1;
        spell2.LBBounce = LBBounce1;
        spell2.LBBounceAmount = LBBounceAmount1;
        spell2.BurnPercent = BurnPercent1;
        spell2.BurnDuration = BurnDuration1;
        spell2.BoostCrit = BoostCrit1;
        spell2.CritChance = CritChance1;
        spell2.CritDamage = CritDamage1;

        //BlackHole
        spell2.BHBool = BHBool1;
        spell2.BHSize = BHSize1;
        spell2.BHRadius = BHRadius1;
        spell2.BHDuration = BHDuration1;
        spell2.BHStrenght = BHStrenght1;
        //Push
        spell2.Push = Push1;
        //Pool
        spell2.pool = pool1;
        //spell.Poolduration = Poolduration;
        spell2.PoolDamage = PoolDamage1;
        spell2.Poolduration = Poolduration1;
        spell2.PoolInst = PoolInst1;
        //Blessed aim
        spell2.BlessedAim = BlessedAim1;

        spell2.ChaosOrb_ = ChaosOrb1;
        spell2.ChaosOrbReseveObject = currentspellObjectChaosOrbReserve1;
        spell2.ChaosOrbCD = ChaosOrbCD1;
        spell2.ChaosOrbDuration = ChaosOrbDuration1;
    }


    public void SplitCastSpell(Vector3 splitLoc, Quaternion splitRot, int LeftOrRight)
    {

        GameObject test123 = Instantiate(currentspellObject, this.transform.position, splitRot);
        SpellProjectile spell = test123.GetComponent<SpellProjectile>();

        if (player_.BlobWeaponEquppied)
        {
            player_.CastExtraBlob(splitRot);
        }

        if (LeftOrRight == 1)
        {
            spell.SplitChanLeft = true;

        }
        else
        {
            spell.SplitChanRight = true;
        }


        spell.channeling = channel;
        spell.chanDur = chanDur;
        spell.projectilespeed = projectilespeed;
        spell.damage = damage;
        spell.spellCastLocation = splitLoc;
        spell.aoeSizeMeteor = aoeSizeMeteor;
        spell.ghostCast = ghostCast;
        spell.cone = cone;
        spell.spellName = spellname;
        spell.FrostBoltSlow = FrostBoltSlow;
        spell.SlowPercent = SlowPercent;
        spell.SlowDuration = SlowDuration;
        spell.FireBallBurn = FireBallBurn;
        spell.LBBounce = LBBounce;
        spell.BurnPercent = BurnPercent;
        spell.BurnDuration = BurnDuration;
        spell.BoostCrit = BoostCrit;
        spell.CritChance = CritChance;
        spell.CritDamage = CritDamage;
        //  spell.ConeRote = splitRot;
        //BlackHole
        spell.BHBool = BHBool;
        spell.BHSize = BHSize;
        spell.BHRadius = BHRadius;
        spell.BHDuration = spellSlotCD * BHDuration;
        spell.BHStrenght = BHStrenght;
        //Push
        spell.Push = Push;
        //Pool
        spell.pool = pool;
        //spell.Poolduration = Poolduration;
        spell.PoolDamage = damage * PoolDamage;
        spell.Poolduration = spellSlotCD * Poolduration;
        spell.PoolInst = PoolInst;

        spell.BlessedAim = BlessedAim;
        spell.ChaosOrb_ = ChaosOrb_;
        spell.ChaosOrbReseveObject = currentspellObjectChaosOrbReserve;
        spell.ChaosOrbCD = (spellSlotCD) * ChaosOrbAttackCD;
        spell.ChaosOrbDuration = ChaosOrbDuration;
    }

    void Update()
    {


        if (CD1 <= 0f)
        {
            spellSlot1rdy = true;
            coolDownTextDisplay1.enabled = false;
            coolDownTextDisplay1_.enabled = false;
        }
        else
        {
            if (CD1 > 0.1f)
            {
                coolDownTextDisplay1.enabled = true;
                coolDownTextDisplay1_.enabled = true;
            }
            else
            {
                coolDownTextDisplay1.enabled = false;
                coolDownTextDisplay1_.enabled = false;
            }
            CD1 -= Time.deltaTime;
            slot1.fillAmount = (CD1 / CD1_);
            coolDownTextDisplay1.text = (Mathf.Ceil(CD1).ToString)("0");
            coolDownTextDisplay1_.text = (Mathf.Ceil(CD1).ToString)("0");
            spellSlot1rdy = false;
        }

        if (CD2 <= 0f)
        {
            spellSlot2rdy = true;
            coolDownTextDisplay2.enabled = false;
            coolDownTextDisplay2_.enabled = false;
        }
        else
        {
            if (CD2 > 0.1f)
            {
                coolDownTextDisplay2.enabled = true;
                coolDownTextDisplay2_.enabled = true;
            }
            else
            {
                coolDownTextDisplay2.enabled = false;
                coolDownTextDisplay2_.enabled = false;
            }
            CD2 -= Time.deltaTime;
            slot2.fillAmount = (CD2 / CD2_);
            coolDownTextDisplay2.text = (Mathf.Ceil(CD2).ToString)("0");
            coolDownTextDisplay2_.text = (Mathf.Ceil(CD2).ToString)("0");
            spellSlot2rdy = false;
        }

        if (CD3 <= 0f)
        {
            spellSlot3rdy = true;
            coolDownTextDisplay3.enabled = false;
            coolDownTextDisplay3_.enabled = false;
        }
        else
        {
            if (CD3 > 0.1f)
            {
                coolDownTextDisplay3.enabled = true;
                coolDownTextDisplay3_.enabled = true;
            }
            else
            {
                coolDownTextDisplay3.enabled = false;
                coolDownTextDisplay3_.enabled = false;
            }


            CD3 -= Time.deltaTime;
            slot3.fillAmount = (CD3 / CD3_);
            coolDownTextDisplay3.text = (Mathf.Ceil(CD3).ToString)("0");
            coolDownTextDisplay3_.text = (Mathf.Ceil(CD3).ToString)("0");
            spellSlot3rdy = false;
        }

    }

    public void SetSlotCD(int slotnumber)
    {
        if (slotnumber == 1)
        {
            CD1 = spellSlotCD;
            CD1_ = spellSlotCD;
            if (HastenBool && !CompOrb)
            {
                var randomInt = Random.Range(0, 100);

                if (randomInt <= HastenChance)
                {
                    CD1 = 0.5f;
                    CD1_ = 0.5f;
                    player_.HastenVis();
                }
            }
        }
        if (slotnumber == 2)
        {
            CD2 = spellSlotCD;
            CD2_ = spellSlotCD;
            if (HastenBool && !CompOrb)
            {
                var randomInt = Random.Range(0, 100);

                if (randomInt <= HastenChance)
                {
                    CD2 = 0.5f;
                    CD2_ = 0.5f;

                    player_.HastenVis();
                }
            }
        }
        if (slotnumber == 3)
        {
            CD3 = spellSlotCD;
            CD3_ = spellSlotCD;
            if (HastenBool && !CompOrb)
            {
                var randomInt = Random.Range(0, 100);

                if (randomInt <= HastenChance)
                {
                    CD3 = 0.5f;
                    CD3_ = 0.5f;
                    player_.HastenVis();
                }
            }

        }
    }

}





