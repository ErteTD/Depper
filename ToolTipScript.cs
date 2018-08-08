using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTipScript : MonoBehaviour
{
    public GameObject CurSpellToolTipBox;
    public GameObject CurSpellToolTipBox2;

    [Header("Level1")]
    public GameObject Fireball;
    public GameObject FrostBolt;
    public GameObject LightningBolt;
    [Header("Level2")]
    public GameObject Meteor;
    public GameObject Cone;
    public GameObject GhostCast;
    [Header("Level3")]
    public GameObject DoubleCast;
    public GameObject SplitCast;
    public GameObject CompOrb;
    [Header("Level4")]
    public GameObject Boost;
    public GameObject Hasten;
    public GameObject Empower;
    [Header("Level5")]
    public GameObject BlackHole;
    public GameObject Push;
    public GameObject Pool;
    [Header("Level6")]
    public GameObject ChaosOrb;
    public GameObject Channeling;
    public GameObject BlessedAim;



    [Header("Names lvl1")]
    public Text Spellname;
    public Text Damage;
    public Text Manacost;
    public Text Cooldown;
    public Text Effect0;
    [Header("Names lvl2")]
    public Text Spellname2;
    public Text ExtraCD;
    public Text DamageModifierlvl2;
    public Text Effect;
    [Header("Names lvl3")]
    public Text Spellname3;
    public Text DamageModifierlvl3;
    public Text ExtraCD2;
    public Text Effect2;
    [Header("Names lvl4")]
    public Text Spellname4;
    public Text DamageModifierlvl4;
    public Text DamageModifierlvl4_2;
    public Text ExtraCD3;
    public Text Effect3;
    [Header("Names lvl5")]
    public Text Spellname5;
    public Text ExtraCD4;
    public Text Effect4;
    [Header("Names lvl6")]
    public Text Spellname6;
    public Text DamageModifierlvl5;
    public Text ExtraCD5;
    public Text Effect5;

    [Header("SpellCombination")]
    public Text curTSpellname;
    public Text curTDamage;
    public Text curTCooldown;
    public Text curTeffect1;
    public Text curTeffect2;
    public Text curTeffect3;
    [Header("SpellCombination Individual spells per image")]
    public Image SpellImage1;
    public Image SpellImage2;
    public Image SpellImage3;
    public Image SpellImage4;
    public Image SpellImage5;
    public Image SpellImage6;
    public Image EmptyImage;

    [Header("Weapons")]
    public GameObject WeaponColor;
    public GameObject SelectWeapon;
    public GameObject WeaponTipPanel;
    public Text weaponEffect;
    public Text weaponName;
    public GameObject SpiderStaff;
    public GameObject BasicStaff;
    public GameObject BlinkStaff;
    public GameObject FireStaff;
    public GameObject IKStaff;

    [Header("Armor")]
    public GameObject ArmorColor;
    public GameObject SelectArmor;
    public GameObject ArmorTipPanel;
    public Text armorEffect;
    public Text armorName;
    public GameObject SpiderArmor;
    public GameObject BasicArmor;
    public GameObject IlluArmor;
    public GameObject RoidRobe;
    public GameObject IKArmor;

    // for the current spell.
    List<string> curSpellname = new List<string>();
    float curDamage;
    float curCooldown;
    float curDamageModifierlvl2 = 0;
    float curDamageModifierlvl3 = 1;
    float curDamageModifierlvl4 = 1;
    float curDamageModifierlvl4_2 = 0;
    float curDamageModifierlvl5 = 1;

    float curExtraCD;
    float curExtraCD2 = 1;
    float curExtraCD3 = 1;
    float curExtraCD4 = 0;
    float curExtraCD5 = 1;
    float curExtraCD5_2 = 0;

    private bool spellCombTip1;
    private bool spellCombTip2;
    private bool spellCombTip3;
    private bool spellCombTip4;
    private bool spellCombTip5;
    private bool spellCombTip6;

    private bool showWeaponSelect;
    private bool showArmorSelect;

    private int CurItemID;
    public void Start()
    {
        curSpellname.Add(""); // must be here for the spell name combination.
        curSpellname.Add("");
        curSpellname.Add("");
        curSpellname.Add("");
        curSpellname.Add("");
        curSpellname.Add("");
    }

    public void Slvl1S1(int slvl)
    {
        Fireball fire = Fireball.GetComponent<Fireball>();
        FrostBolt frost = FrostBolt.GetComponent<FrostBolt>();
        LightningBolt lightning = LightningBolt.GetComponent<LightningBolt>();

        switch (slvl)
        {
            case 1:
                Spellname.text = fire.spellname;
                Damage.text = "Damage: " + fire.damage.ToString();
                Cooldown.text = "Cooldown: " + fire.cooldown.ToString();

                break;
            case 2:
                Spellname.text = frost.spellname;
                Damage.text = "Damage: " + frost.damage.ToString();
                Cooldown.text = "Cooldown: " + frost.cooldown.ToString();

                break;
            case 3:
                Spellname.text = lightning.spellname;
                Damage.text = "Damage: " + lightning.damage.ToString();
                Cooldown.text = "Cooldown: " + lightning.cooldown.ToString();

                break;
        }
    }
    public void Slvl2S1(int slvl)
    {
        Meteor meteor = Meteor.GetComponent<Meteor>();
        Cone cone = Cone.GetComponent<Cone>();
        GhostCast ghostcast = GhostCast.GetComponent<GhostCast>();

        switch (slvl)
        {
            case 1:
                Spellname2.text = meteor.spellname;
                DamageModifierlvl2.text = "Damage: +" + (meteor.damageReduction).ToString("F1");
                ExtraCD.text = "Cooldown Modifier: +" + meteor.extraCD.ToString("F2") + "s";
                Effect.text = "Effect: " + meteor.effect.ToString();
                break;
            case 2:
                Spellname2.text = cone.spellname;
                DamageModifierlvl2.text = "Damage: +" + (cone.damageModifier).ToString("F1");
                ExtraCD.text = "Cooldown Modifier: +" + cone.extraCD.ToString("F2") + "s";
                Effect.text = "Effect: " + cone.effect.ToString();
                break;
            case 3:
                Spellname2.text = ghostcast.spellname;
                DamageModifierlvl2.text = "";
                ExtraCD.text = "Cooldown Modifier: +" + ghostcast.extraCD.ToString("F2") + "s";
                Effect.text = "Effect: " + ghostcast.effect.ToString();
                break;
        }
    }
    public void Slvl3S1(int slvl)
    {
        DoubleCast doublecast = DoubleCast.GetComponent<DoubleCast>();
        SplitCast splitCast = SplitCast.GetComponent<SplitCast>();
        Companion Corb = CompOrb.GetComponent<Companion>();

        switch (slvl)
        {
            case 1:
                Spellname3.text = doublecast.spellname;
                ExtraCD2.text = "Cooldown Modifier: " + (doublecast.extraCD * 100).ToString("F2") + "%";
                DamageModifierlvl3.text = "Damage: " + ((doublecast.DamageModifier) * 100).ToString("F0") + "%";
                Effect2.text = "Effect: " + doublecast.effect.ToString();
                break;
            case 2:

                Spellname3.text = splitCast.spellname;
                ExtraCD2.text = "Cooldown Modifier: " + (splitCast.extraCD * 100).ToString("F2") + "%";
                DamageModifierlvl3.text = "Damage: " + ((splitCast.DamageModifier) * 100).ToString("F0") + "%";
                Effect2.text = "Effect: " + splitCast.effect.ToString();
                break;
            case 3:
                Spellname3.text = Corb.spellname;
                ExtraCD2.text = "";
                DamageModifierlvl3.text = "";
                Effect2.text = "Effect: " + Corb.effect.ToString();
                break;
        }

    }
    public void Slvl4S1(int slvl)
    {
        Boost boost = Boost.GetComponent<Boost>();
        Hasten hasten = Hasten.GetComponent<Hasten>();
        Empower empower = Empower.GetComponent<Empower>();

        switch (slvl)
        {
            case 1:
                Spellname4.text = boost.spellname;
                //  DamageModifierlvl4.text = "";
                DamageModifierlvl4_2.text = "Damage: +" + (boost.damageModifierPure).ToString("F0") + " and " + ((boost.damageModifierPercent) * 100).ToString("F0") + "%";
                ExtraCD3.text = "";
                Effect3.text = "Effect: " + boost.effect.ToString();

                break;
            case 2:

                Spellname4.text = hasten.spellname;
                //  DamageModifierlvl4.text = "";
                DamageModifierlvl4_2.text = "";
                ExtraCD3.text = "Cooldown Modifier: " + ((hasten.CDModifier) * 100).ToString("F2") + "%";
                Effect3.text = "Effect: " + hasten.effect.ToString();
                break;
            case 3:
                Spellname4.text = empower.spellname;
                //  DamageModifierlvl4.text = "";
                DamageModifierlvl4_2.text = "";
                ExtraCD3.text = "";
                Effect3.text = empower.effect.ToString();
                break;
        }
    }
    public void Slvl5S1(int slvl)
    {
        //Boost boost = Boost.GetComponent<Boost>();
        //Hasten hasten = Hasten.GetComponent<Hasten>();
        //Empower empower = Empower.GetComponent<Empower>();
        BlackHole BH = BlackHole.GetComponent<BlackHole>();
        Push push = Push.GetComponent<Push>();
        Pool pool = Pool.GetComponent<Pool>();


        switch (slvl)
        {
            case 1:
                Spellname5.text = BH.spellname;
                ExtraCD4.text = "Cooldown Modifier: +" + (BH.CDmodifier).ToString("F2") + " seconds";
                Effect4.text = "Effect: " + BH.effect.ToString();

                break;
            case 2:

                Spellname5.text = push.spellname;
                ExtraCD4.text = "Cooldown Modifier: +" + (push.CDmodifier).ToString("F2") + " seconds";
                Effect4.text = "Effect: " + push.effect.ToString();
                break;
            case 3:

                Spellname5.text = pool.spellname;
                ExtraCD4.text = "Cooldown Modifier: +" + (pool.CDmodifier).ToString("F2") + " seconds";
                Effect4.text = "Effect: " + pool.effect.ToString();
                break;
        }
    }

    public void Slvl6S1(int slvl)
    {
        ChaosOrb Chaos = ChaosOrb.GetComponent<ChaosOrb>();
        Channeling chan = Channeling.GetComponent<Channeling>();
        BlessedAim Aim = BlessedAim.GetComponent<BlessedAim>();

        switch (slvl)
        {
            case 1:
                Spellname6.text = Chaos.spellname;
                ExtraCD5.text = "Cooldown Modifier: +" + (Chaos.CoolDownMod*100).ToString("F2") + "% and +" + (Chaos.CoolDownSec).ToString("F0") + " second";
                DamageModifierlvl5.text = "";
                Effect5.text = "Effect: " + Chaos.effect.ToString();

                break;
            case 2:
                Spellname6.text = chan.spellname;
                ExtraCD5.text = "Cooldown Modifier: +" + (chan.extraCD* 100).ToString("F2") + "%";
                DamageModifierlvl5.text = "Damage: " + ((chan.damageModifier) * 100).ToString("F0") + "%";
                Effect5.text = "Effect: " + chan.effect.ToString();
                break;
            case 3:
                Spellname6.text = Aim.spellname;
                ExtraCD5.text = "Cooldown Modifier: +" + (Aim.CoolDownMod* 100).ToString("F2") + "%";
                DamageModifierlvl5.text = "Damage: " + ((Aim.DamageMod) * 100).ToString("F0") + "%";
                Effect5.text = "Effect: " + Aim.effect.ToString();
                break;
        }
    }




    public void SpellCombTip(int lvlAndnumber)
    {
        Fireball fire = Fireball.GetComponent<Fireball>();
        FrostBolt frost = FrostBolt.GetComponent<FrostBolt>();
        LightningBolt lightning = LightningBolt.GetComponent<LightningBolt>();
        Meteor meteor = Meteor.GetComponent<Meteor>();
        Cone cone = Cone.GetComponent<Cone>();
        GhostCast ghostcast = GhostCast.GetComponent<GhostCast>();
        DoubleCast doublecast = DoubleCast.GetComponent<DoubleCast>();
        SplitCast splitCast = SplitCast.GetComponent<SplitCast>();
        Companion Corb = CompOrb.GetComponent<Companion>();
        Boost boost = Boost.GetComponent<Boost>();
        Hasten hasten = Hasten.GetComponent<Hasten>();
        Empower empower = Empower.GetComponent<Empower>();
        BlackHole BH = BlackHole.GetComponent<BlackHole>();
        Push push = Push.GetComponent<Push>();
        Pool pool = Pool.GetComponent<Pool>();
        ChaosOrb Chaos = ChaosOrb.GetComponent<ChaosOrb>();
        Channeling chan = Channeling.GetComponent<Channeling>();
        BlessedAim Aim = BlessedAim.GetComponent<BlessedAim>();

        CurSpellToolTipBox.SetActive(true);
        CurSpellToolTipBox2.SetActive(true);

        if (spellCombTip1 == true && lvlAndnumber <= 3)
        {
            Empty1();
        }
        if (spellCombTip2 == true && (lvlAndnumber > 3 && lvlAndnumber < 30))
        {
            Empty2();
        }
        if (spellCombTip3 == true && (lvlAndnumber > 30 && lvlAndnumber < 40))
        {
            Empty3();
        }
        if (spellCombTip4 == true && (lvlAndnumber > 40 && lvlAndnumber < 50))
        {
            Empty4();
        }
        if (spellCombTip5 == true && (lvlAndnumber > 50 && lvlAndnumber < 60))
        {
            Empty5();
        }
        if (spellCombTip6 == true && (lvlAndnumber >60 && lvlAndnumber < 70))
        {
            Empty6();
        }

        switch (lvlAndnumber)
        {
            case 1:
                curSpellname[0] = fire.spellname;
                curDamage = fire.damage;
                curCooldown = fire.cooldown;
                spellCombTip1 = true;

                if (GameManager.FindObjectOfType<GameManager>().empowerToken_)
                {
                    Effect0.text = fire.effect2;
                }
                else
                {
                    Effect0.text = fire.effect;
                }



                SpellImage1.sprite = Fireball.GetComponent<Image>().sprite;

                break;
            case 2:
                curSpellname[0] = frost.spellname;
                curDamage = frost.damage;
                curCooldown = frost.cooldown;
                spellCombTip1 = true;
                Effect0.text = frost.effect;
                SpellImage1.sprite = FrostBolt.GetComponent<Image>().sprite;

                if (GameManager.FindObjectOfType<GameManager>().empowerToken_2)
                {
                    Effect0.text = frost.effect2;
                }
                else
                {
                    Effect0.text = frost.effect;
                }


                break;
            case 3:
                curSpellname[0] = lightning.spellname;
                curDamage = lightning.damage;
                curCooldown = lightning.cooldown;
                spellCombTip1 = true;
                Effect0.text = lightning.effect;
                SpellImage1.sprite = LightningBolt.GetComponent<Image>().sprite;

                if (GameManager.FindObjectOfType<GameManager>().empowerToken_3)
                {
                    Effect0.text = lightning.effect2;
                }
                else
                {
                    Effect0.text = lightning.effect;
                }


                break;

            case 21:
                curSpellname[1] = "-" + meteor.spellname;
                curDamageModifierlvl2 = meteor.damageReduction;
                curExtraCD = meteor.extraCD;

                spellCombTip2 = true;
                SpellImage2.sprite = Meteor.GetComponent<Image>().sprite;
                break;
            case 22:
                curSpellname[1] = "-" + cone.spellname;
                //  curDamageModifier = cone.damageReduction;
                curExtraCD = cone.extraCD;
                curDamageModifierlvl2 = cone.damageModifier;
                spellCombTip2 = true;
                SpellImage2.sprite = Cone.GetComponent<Image>().sprite;
                break;
            case 23:
                curSpellname[1] = "-" + ghostcast.spellname;
                curDamageModifierlvl2 = ghostcast.damageReduction;
                curExtraCD = ghostcast.extraCD;

                spellCombTip2 = true;
                SpellImage2.sprite = GhostCast.GetComponent<Image>().sprite;
                break;

            case 31:
                curSpellname[2] = "-" + doublecast.spellname;
                curExtraCD2 = doublecast.extraCD;
                curDamageModifierlvl3 = doublecast.DamageModifier;

                spellCombTip3 = true;
                SpellImage3.sprite = DoubleCast.GetComponent<Image>().sprite;
                break;
            case 32:
                curSpellname[2] = "-" + splitCast.spellname;
                curExtraCD2 = splitCast.extraCD;
                curDamageModifierlvl3 = splitCast.DamageModifier;

                spellCombTip3 = true;
                SpellImage3.sprite = SplitCast.GetComponent<Image>().sprite;
                break;
            case 33:
                curSpellname[2] = "-" + Corb.spellname;
                curExtraCD2 = 1;
                curDamageModifierlvl3 = 1;

                spellCombTip3 = true;
                SpellImage3.sprite = Corb.GetComponent<Image>().sprite;
                break;
            //
            //DamageModifierlvl4.text = "Damage: " + ((boost.damageModifierPercent) * 100).ToString("F0") + "%";
            //DamageModifierlvl4_2.text = "Damage: +" + (boost.damageModifierPure).ToString("F0");
            //ExtraCD3.text = "";
            //Effect3.text = "Effect: " + boost.effect.ToString();
            case 41:
                curSpellname[3] = "-" + boost.spellname;
                curDamageModifierlvl4 = boost.damageModifierPercent;
                curDamageModifierlvl4_2 = boost.damageModifierPure;
                spellCombTip4 = true;
                SpellImage4.sprite = Boost.GetComponent<Image>().sprite;
                break;
            case 42:
                curSpellname[3] = "-" + hasten.spellname;
                curExtraCD3 = hasten.CDModifier;
                spellCombTip4 = true;
                SpellImage4.sprite = Hasten.GetComponent<Image>().sprite;
                break;
            case 43:
                curSpellname[3] = "-" + empower.spellname;
                spellCombTip4 = true;
                SpellImage4.sprite = Empower.GetComponent<Image>().sprite;
                break;
            case 51:
                curSpellname[4] = "-" + BH.spellname;
                curExtraCD4 = BH.CDmodifier;
                spellCombTip5 = true;
                SpellImage5.sprite = BH.GetComponent<Image>().sprite;
                break;
            case 52:
                curSpellname[4] = "-" + push.spellname;
                curExtraCD4 = push.CDmodifier;
                spellCombTip5 = true;
                SpellImage5.sprite = push.GetComponent<Image>().sprite;
                break;
            case 53:
                curSpellname[4] = "-" + pool.spellname;
                curExtraCD4 = pool.CDmodifier;
                spellCombTip5 = true;
                SpellImage5.sprite = pool.GetComponent<Image>().sprite;
                break;
                //HERE
            case 61:
                curSpellname[5] = "-" + Chaos.spellname;
                curExtraCD5 = Chaos.CoolDownMod;
                curExtraCD5_2 = Chaos.CoolDownSec;
                spellCombTip6 = true;
                SpellImage6.sprite = Chaos.GetComponent<Image>().sprite;
                break;
            case 62:
                curSpellname[5] = "-" + chan.spellname;
                curExtraCD5 = chan.extraCD;
                curDamageModifierlvl5 = chan.damageModifier;
                spellCombTip6 = true;
                SpellImage6.sprite = chan.GetComponent<Image>().sprite;
                break;
            case 63:
                curSpellname[5] = "-" + Aim.spellname;
                curExtraCD5 = Aim.CoolDownMod;
                curDamageModifierlvl5 = Aim.DamageMod;
                spellCombTip6 = true;
                SpellImage6.sprite = Aim.GetComponent<Image>().sprite;
                break;

            default:
                break;
        }

        curTSpellname.text = curSpellname[0] + curSpellname[1] + curSpellname[2] + curSpellname[3] + curSpellname[4] + curSpellname[5];
        curTDamage.text = "Damage: " + ((curDamage + curDamageModifierlvl2 + curDamageModifierlvl4_2) * curDamageModifierlvl3 * curDamageModifierlvl4 * curDamageModifierlvl5).ToString("F1");
        curTCooldown.text = "Cooldown: " + ((curCooldown + curExtraCD + curExtraCD4 + curExtraCD5_2) * curExtraCD2 * curExtraCD3 * curExtraCD5).ToString("F1");


    }

    public void Empty1()
    {
        curSpellname[0] = "";
        curDamage = 0;
        curCooldown = 0;
        SpellImage1.sprite = EmptyImage.sprite;
        spellCombTip1 = false;
    }
    public void Empty2()
    {
        curSpellname[1] = "";
        curDamageModifierlvl2 = 0;
        curExtraCD = 0;
        SpellImage2.sprite = EmptyImage.sprite;
        spellCombTip2 = false;
    }
    public void Empty3()
    {
        curSpellname[2] = "";
        curExtraCD2 = 1;
        curDamageModifierlvl3 = 1;
        SpellImage3.sprite = EmptyImage.sprite;
        spellCombTip3 = false;
    }
    public void Empty4()
    {
        curSpellname[3] = "";
        curExtraCD3 = 1;
        curDamageModifierlvl4 = 1;
        curDamageModifierlvl4_2 = 0;
        SpellImage4.sprite = EmptyImage.sprite;
        spellCombTip4 = false;
    }
    public void Empty5()
    {
        curSpellname[4] = "";
        curExtraCD4 = 0;
        SpellImage5.sprite = EmptyImage.sprite;
        spellCombTip5 = false;
    }
    public void Empty6() // 23.7 TODO CHANGE THIS
    {
        curSpellname[5] = "";
        curExtraCD5 = 1;
        curExtraCD5_2 = 0;
        curDamageModifierlvl5 = 1;
        SpellImage6.sprite = EmptyImage.sprite;
        spellCombTip6 = false;
    }



    public void WeaponTip(bool preview)
    {
        WeaponSpider spider = SpiderStaff.GetComponent<WeaponSpider>();
        WeaponBasic basic = BasicStaff.GetComponent<WeaponBasic>();
        Blink blink = BlinkStaff.GetComponent<Blink>();
        Blink FireStaff_ = FireStaff.GetComponent<Blink>();
        Blink FireStaff_2 = IKStaff.GetComponent<Blink>();
        CurSpellToolTipBox.SetActive(true);
        WeaponTipPanel.SetActive(true);
        int CurID = CastSpell.FindObjectOfType<CastWeapon>().CurrentWeapon;

        if (preview)
        {
            CurID = CurItemID;
        }

        switch (CurID) //TODO give active item..
        {
            case 0:
                weaponName.text = basic.itemname;
                weaponEffect.text = basic.effect;
                if (!preview)
                {
                    WeaponColor.GetComponent<Image>().color = Color.white;
                }
                break;
            case 1:
                weaponName.text = spider.itemname;
                weaponEffect.text = spider.effect;
                if (!preview)
                {
                    WeaponColor.GetComponent<Image>().color = spider.ItemColor;
                }
                break;
            case 2:
                weaponName.text = blink.itemname;
                weaponEffect.text = blink.effect;
                if (!preview)
                {
                    WeaponColor.GetComponent<Image>().color = blink.ItemColor;
                }
                break;
            case 3:
                weaponName.text = FireStaff_.itemname;
                weaponEffect.text = FireStaff_.effect;
                if (!preview)
                {
                    WeaponColor.GetComponent<Image>().color = FireStaff_.ItemColor;
                }
                break;
            case 4:
                weaponName.text = FireStaff_2.itemname;
                weaponEffect.text = FireStaff_2.effect;
                if (!preview)
                {
                    WeaponColor.GetComponent<Image>().color = FireStaff_2.ItemColor;
                }
                break;


        }
    }

    public void ArmorTip(bool preview)
    {
        ArmorSpider spider = SpiderArmor.GetComponent<ArmorSpider>();
        ArmorBasic basic = BasicArmor.GetComponent<ArmorBasic>();
        ArmorIllusion illu = IlluArmor.GetComponent<ArmorIllusion>();
        ArmorIllusion Rage = RoidRobe.GetComponent<ArmorIllusion>();
        ArmorIllusion Rage_ = IKArmor.GetComponent<ArmorIllusion>();
        CurSpellToolTipBox.SetActive(true);
        ArmorTipPanel.SetActive(true);

        int CurID = CastSpell.FindObjectOfType<CastWeapon>().CurrentArmor;
        
        if (preview)
        {
            CurID = CurItemID;
        }

        switch (CurID) //TODO give active item..
        {
            case 0:
                armorName.text = basic.itemname;
                armorEffect.text = basic.effect;
                if (!preview)
                {
                    ArmorColor.GetComponent<Image>().color = Color.white;
                }

                break;
            case 1:
                armorName.text = spider.itemname;
                armorEffect.text = spider.effect;
                if (!preview)
                {
                    ArmorColor.GetComponent<Image>().color = spider.ItemColor;
                }
                break;
            case 2:
                armorName.text = illu.itemname;
                armorEffect.text = illu.effect;
                if (!preview)
                {
                    ArmorColor.GetComponent<Image>().color = illu.ItemColor;
                }
                break;
            case 3:
                armorName.text = Rage.itemname;
                armorEffect.text = Rage.effect;
                if (!preview)
                {
                    ArmorColor.GetComponent<Image>().color = Rage.ItemColor;
                }
                break;
            case 4:
                armorName.text = Rage_.itemname;
                armorEffect.text = Rage_.effect;
                if (!preview)
                {
                    ArmorColor.GetComponent<Image>().color = Rage_.ItemColor;
                }
                break;
        }
    }



    public void CloseWeapon()
    {
        CurSpellToolTipBox.SetActive(false);
        WeaponTipPanel.SetActive(false);
    }
    public void CloseArmor()
    {
        CurSpellToolTipBox.SetActive(false);
        ArmorTipPanel.SetActive(false);
    }

    public void CurrentItemID(int ID)
    {
        CurItemID = ID;
    }

    public void OpenWeaponSelect()
    {
        SelectArmor.SetActive(false);
        showArmorSelect = false;
        showWeaponSelect = showWeaponSelect ? false : true;
        SelectWeapon.SetActive(showWeaponSelect);
    }

    public void OpenArmorSelect()
    {
        SelectWeapon.SetActive(false);
        showWeaponSelect = false;
        showArmorSelect = showArmorSelect ? false : true;
        SelectArmor.SetActive(showArmorSelect);
    }


}



