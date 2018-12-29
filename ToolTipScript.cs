using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTipScript : MonoBehaviour
{
  //  public GameObject CurSpellToolTipBox;
    public GameObject CurSpellToolTipBox2;

    public List<GameObject> AllSpells = new List<GameObject>();
    public List<MasterItem> AllWeapons_ = new List<MasterItem>();
    public List<MasterItem> AllArmors_ = new List<MasterItem>();

    public Fireball fire;
    public FrostBolt frost;
    public LightningBolt lightning;
    private Meteor meteor;
    private Cone cone;
    private GhostCast ghostcast;
    private DoubleCast doublecast;
    private SplitCast splitcast;
    private Companion companion;
    private Boost boost;
    private Hasten hasten;
    private Empower empower;
    private BlackHole blackhole;
    private Push push;
    private Pool pool;
    private ChaosOrb chaosorb;
    private Channeling channling;
    private BlessedAim blessedaim;

    public Text Effect0;

    [Header("Spell modifier tooltip")]
    public GameObject SpellModifierPanel;
    public Text Spellname;
    public Text DamagePure;
    public Text DamagePercet;
    public Text CooldownSeconds;
    public Text CooldownPercent;
    public Text Effect;
    public Color PositiveStatColor;
    public Color StartStatColor;
    public Image SpellImage;
    public Image NoSpellImage;

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
    public GameObject ForceCloseWeaponPanels;
    public Text weaponEffect;
    public Text weaponName;

    [Header("Armor")]
    public GameObject ArmorColor;
    public GameObject SelectArmor;
    public GameObject ArmorTipPanel;
    public GameObject ForceCloseArmorPanels;
    public Text armorEffect;
    public Text armorName;

    // for the current spell.
    List<string> curSpellname = new List<string>() {"", "", "", "", "", "" };
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

    private bool toggleArmorTooltip;
    private bool toggleWeaponTooltip;

    private int CurItemID;
    private GameManager gm;
    public bool ToolTipHC1;
    public bool ToolTipHC2;
    public void Start()
    {
        gm = FindObjectOfType<GameManager>();
        fire = AllSpells[0].GetComponent<Fireball>();
        frost = AllSpells[1].GetComponent<FrostBolt>();
        lightning = AllSpells[2].GetComponent<LightningBolt>();
        meteor = AllSpells[5].GetComponent<Meteor>();
        cone = AllSpells[3].GetComponent<Cone>();
        ghostcast = AllSpells[4].GetComponent<GhostCast>();
        doublecast = AllSpells[6].GetComponent<DoubleCast>();
        splitcast = AllSpells[7].GetComponent<SplitCast>();
        companion = AllSpells[8].GetComponent<Companion>();
        boost = AllSpells[9].GetComponent<Boost>();
        hasten = AllSpells[10].GetComponent<Hasten>();
        empower = AllSpells[11].GetComponent<Empower>();
        blackhole = AllSpells[12].GetComponent<BlackHole>();
        push = AllSpells[13].GetComponent<Push>();
        pool = AllSpells[14].GetComponent<Pool>();
        chaosorb = AllSpells[15].GetComponent<ChaosOrb>();
        channling = AllSpells[16].GetComponent<Channeling>();
        blessedaim = AllSpells[17].GetComponent<BlessedAim>();
    }

    public void GetSpellClassFrom(MasterSpell spell)
    {
        SpellModifierPanel.SetActive(true);
        Spellname.text = spell.spellname;
        Effect.text = spell.effect;

        if (spell.damagePure > 0) { DamagePure.color = PositiveStatColor; DamagePure.text = "+" + spell.damagePure.ToString("F1") +" damage"; }
        else if (spell.damagePure < 0) { DamagePure.color = Color.red; DamagePure.text = "-" + spell.damagePure.ToString("F1") + " damage"; }

        if (spell.damagePercent > 1) { DamagePercet.color = PositiveStatColor; DamagePercet.text = ((spell.damagePercent * 100)).ToString("F0") + "%"; }
        else if (spell.damagePercent < 1) { DamagePercet.color = Color.red; DamagePercet.text = ((spell.damagePercent * 100)).ToString("F0") + "%"; }

        if (spell.cooldownSeconds < 0) { CooldownSeconds.color = PositiveStatColor; CooldownSeconds.text = "-" + spell.cooldownSeconds.ToString("F1") + " seconds"; }
        else if (spell.cooldownSeconds > 0) { CooldownSeconds.color = Color.red; CooldownSeconds.text = "+" + spell.cooldownSeconds.ToString("F1") + " seconds"; }

        if (spell.cooldownPercent < 1) { CooldownPercent.color = PositiveStatColor; CooldownPercent.text = ((spell.cooldownPercent * 100)).ToString("F0") + "%"; }
        else if (spell.cooldownPercent > 1) { CooldownPercent.color = Color.red; CooldownPercent.text = ((spell.cooldownPercent * 100)).ToString("F0") + "%"; }

        if (spell.damagePure == 0) { DamagePure.color = StartStatColor; DamagePure.text = "---"; }
        if (spell.damagePercent == 0) { DamagePercet.color = StartStatColor; DamagePercet.text = "---"; }
        if (spell.cooldownSeconds == 0) { CooldownSeconds.color = StartStatColor; CooldownSeconds.text = "---"; }
        if (spell.cooldownPercent == 0) { CooldownPercent.color = StartStatColor; CooldownPercent.text = "---"; }

        SpellImage.sprite = spell.GetComponent<Image>().sprite;
    }


    public void NoToken()
    {
        SpellModifierPanel.SetActive(true);

        Spellname.text = "???";
        DamagePure.color = StartStatColor; DamagePure.text = "???"; 
        DamagePercet.color = StartStatColor; DamagePercet.text = "???"; 
        CooldownSeconds.color = StartStatColor; CooldownSeconds.text = "???"; 
        CooldownPercent.color = StartStatColor; CooldownPercent.text = "???";
        Effect.text = "???";
        SpellImage.sprite = NoSpellImage.sprite;

    }


    public void Slvl2S1(int slvl) // Can get rid of all these, just assign through button click the gameobject, works.
    {
        switch (slvl)
        {
            case 1:
                GetSpellClassFrom(meteor);
                break;
            case 2:
                GetSpellClassFrom(cone);
                break;
            case 3:
                GetSpellClassFrom(ghostcast);
                break;
            default:
                NoToken();
                break;
        }
    }
    public void Slvl3S1(int slvl)
    {
        switch (slvl)
        {
            case 1:
                GetSpellClassFrom(doublecast);
                break;
            case 2:
                GetSpellClassFrom(splitcast);
                break;
            case 3:
                GetSpellClassFrom(companion);
                break;
            default:
                NoToken();
                break;
        }
    }
    public void Slvl4S1(int slvl)
    {
        switch (slvl)
        {
            case 1:
                GetSpellClassFrom(boost);
                break;
            case 2:
                GetSpellClassFrom(hasten);
                break;
            case 3:
                GetSpellClassFrom(empower);
                break;
            default:
                NoToken();
                break;
        }
    }
    public void Slvl5S1(int slvl)
    {
        switch (slvl)
        {
            case 1:
                GetSpellClassFrom(blackhole);
                break;
            case 2:
                GetSpellClassFrom(push);
                break;
            case 3:
                GetSpellClassFrom(pool);
                break;
            default:
                NoToken();
                break;
        }
    }
    public void Slvl6S1(int slvl)
    {
        switch (slvl)
        {
            case 1:
                GetSpellClassFrom(chaosorb);
                break;
            case 2:
                GetSpellClassFrom(channling);
                break;
            case 3:
                GetSpellClassFrom(blessedaim);
                break;
            default:
                NoToken();
                break;
        }
    }

    public void SpellCombTip(int lvlAndnumber)
    {
            CurSpellToolTipBox2.SetActive(true);

        if (spellCombTip1 == true && lvlAndnumber <= 3)
        {
            Empty1();
        }
        if (spellCombTip2 == true && (lvlAndnumber > 3 && lvlAndnumber < 30))
        {
            Empty2();
        }
        if (spellCombTip3 == true && (lvlAndnumber >= 30 && lvlAndnumber < 40))
        {
            Empty3();
        }
        if (spellCombTip4 == true && (lvlAndnumber >= 40 && lvlAndnumber < 50))
        {
            Empty4();
        }
        if (spellCombTip5 == true && (lvlAndnumber >= 50 && lvlAndnumber < 60))
        {
            Empty5();
        }
        if (spellCombTip6 == true && (lvlAndnumber >= 60 && lvlAndnumber < 70))
        {
            Empty6();
        }
        
        switch (lvlAndnumber)
        {
            case 1:
                curSpellname[0] = fire.spellname;
                curDamage = fire.damagePure;
                curCooldown = fire.cooldownSeconds;
                spellCombTip1 = true;

                if (GameManager.FindObjectOfType<GameManager>().empowerToken_)
                {
                    Effect0.text = fire.effect2;
                }
                else
                {
                    Effect0.text = fire.effect;
                }

                SpellImage1.sprite = fire.GetComponent<Image>().sprite;

                break;
            case 2:
                curSpellname[0] = frost.spellname;
                curDamage = frost.damagePure;
                curCooldown = frost.cooldownSeconds;
                spellCombTip1 = true;
                Effect0.text = frost.effect;
                SpellImage1.sprite = frost.GetComponent<Image>().sprite;



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
                curDamage = lightning.damagePure;
                curCooldown = lightning.cooldownSeconds;
                spellCombTip1 = true;
                Effect0.text = lightning.effect;
                SpellImage1.sprite = lightning.GetComponent<Image>().sprite;

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
                curDamageModifierlvl2 = meteor.damagePure;
                curExtraCD = meteor.cooldownSeconds;
                spellCombTip2 = true;
                SpellImage2.sprite = meteor.GetComponent<Image>().sprite;
                break;
            case 22:
                curSpellname[1] = "-" + cone.spellname;
                curExtraCD = cone.cooldownSeconds;
                curDamageModifierlvl2 = cone.damagePure;
                spellCombTip2 = true;
                SpellImage2.sprite = cone.GetComponent<Image>().sprite;
                break;
            case 23:
                curSpellname[1] = "-" + ghostcast.spellname;
                curDamageModifierlvl2 = ghostcast.damagePure;
                curExtraCD = ghostcast.cooldownSeconds;
                spellCombTip2 = true;
                SpellImage2.sprite = ghostcast.GetComponent<Image>().sprite;
                break;
            case 31:
                curSpellname[2] = "-" + doublecast.spellname;
                curExtraCD2 = doublecast.cooldownPercent;
                curDamageModifierlvl3 = doublecast.damagePercent;
                spellCombTip3 = true;
                SpellImage3.sprite = doublecast.GetComponent<Image>().sprite;
                break;
            case 32:
                curSpellname[2] = "-" + splitcast.spellname;
                curExtraCD2 = splitcast.cooldownPercent;
                curDamageModifierlvl3 = splitcast.damagePercent;
                spellCombTip3 = true;
                SpellImage3.sprite = splitcast.GetComponent<Image>().sprite;
                break;
            case 33:
                curSpellname[2] = "-" + companion.spellname;
                curExtraCD2 = companion.cooldownPercent;
                curDamageModifierlvl3 = companion.damagePercent;
                spellCombTip3 = true;
                SpellImage3.sprite = companion.GetComponent<Image>().sprite;
                break;
            case 41:
                curSpellname[3] = "-" + boost.spellname;
                curDamageModifierlvl4 = boost.damagePercent;
                curDamageModifierlvl4_2 = boost.damagePure;
                spellCombTip4 = true;
                SpellImage4.sprite = boost.GetComponent<Image>().sprite;
                break;
            case 42:
                curSpellname[3] = "-" + hasten.spellname;
                curExtraCD3 = hasten.cooldownPercent;
                spellCombTip4 = true;
                SpellImage4.sprite = hasten.GetComponent<Image>().sprite;
                break;
            case 43:
                curSpellname[3] = "-" + empower.spellname;
                spellCombTip4 = true;
                SpellImage4.sprite = empower.GetComponent<Image>().sprite;
                break;
            case 51:
                curSpellname[4] = "-" + blackhole.spellname;
                curExtraCD4 = blackhole.cooldownSeconds;
                spellCombTip5 = true;
                SpellImage5.sprite = blackhole.GetComponent<Image>().sprite;
                break;
            case 52:
                curSpellname[4] = "-" + push.spellname;
                curExtraCD4 = push.cooldownSeconds;
                spellCombTip5 = true;
                SpellImage5.sprite = push.GetComponent<Image>().sprite;
                break;
            case 53:
                curSpellname[4] = "-" + pool.spellname;
                curExtraCD4 = pool.cooldownSeconds;
                spellCombTip5 = true;
                SpellImage5.sprite = pool.GetComponent<Image>().sprite;
                break;
            case 61:
                curSpellname[5] = "-" + chaosorb.spellname;
                curExtraCD5 = chaosorb.cooldownPercent;
                curExtraCD5_2 = chaosorb.cooldownSeconds;
                spellCombTip6 = true;
                SpellImage6.sprite = chaosorb.GetComponent<Image>().sprite;
                break;
            case 62:
                curSpellname[5] = "-" + channling.spellname;
                curExtraCD5 = channling.cooldownPercent;
                curExtraCD5 = channling.cooldownPercent;
                curDamageModifierlvl5 = channling.damagePercent;
                spellCombTip6 = true;
                SpellImage6.sprite = channling.GetComponent<Image>().sprite;
                break;
            case 63:
                curSpellname[5] = "-" + blessedaim.spellname;
                curExtraCD5 = blessedaim.cooldownPercent;
                curDamageModifierlvl5 = blessedaim.damagePercent;
                spellCombTip6 = true;
                SpellImage6.sprite = blessedaim.GetComponent<Image>().sprite;
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

    public void GetWeaponClassFrom(MasterItem item, bool preview)
    {
        weaponName.text = item.itemname;
        weaponEffect.text = item.effect;
        if (!preview)
        {
            WeaponColor.GetComponent<Image>().color = item.ItemColor;
        }
    }
    public void GetArmorClassFrom(MasterItem item, bool preview)
    {
        armorName.text = item.itemname;
        armorEffect.text = item.effect;
        if (!preview)
        {
            ArmorColor.GetComponent<Image>().color = item.ItemColor;
        }
    }
    public void WeaponTip(bool preview)
    {
        ToolTipHC1 = false;
        toggleWeaponTooltip = !toggleWeaponTooltip;
        WeaponTipPanel.SetActive(toggleWeaponTooltip);
        int CurID = CastSpell.FindObjectOfType<CastWeapon>().CurrentWeapon;
        if (preview)
        {
            CurID = CurItemID;
            ToolTipHC1 = true;
        }
        GetWeaponClassFrom(AllWeapons_[CurID], preview);
        
    }
    public void ArmorTip(bool preview)
    {
        ToolTipHC2 = false;
        toggleArmorTooltip = !toggleArmorTooltip;
        ArmorTipPanel.SetActive(toggleArmorTooltip);
        int CurID = CastSpell.FindObjectOfType<CastWeapon>().CurrentArmor;
        if (preview)
        {
            CurID = CurItemID;
            ToolTipHC2 = true;
        }
        GetArmorClassFrom(AllArmors_[CurID], preview);
    }
    public void CloseWeapon()
    {
        toggleWeaponTooltip = !toggleWeaponTooltip;
        WeaponTipPanel.SetActive(toggleWeaponTooltip);
    }
    public void CloseArmor()
    {
        toggleArmorTooltip = !toggleArmorTooltip;
        ArmorTipPanel.SetActive(toggleArmorTooltip);      
    }
    public void CLOSEALLITEMTIPS()
    {
        toggleWeaponTooltip = false;
        toggleArmorTooltip = false;
        WeaponTipPanel.SetActive(toggleWeaponTooltip);
        ArmorTipPanel.SetActive(toggleArmorTooltip);
    }

    public void CurrentItemID(int ID)
    {
        CurItemID = ID;
    }
    public void OpenWeaponSelect()
    {
        gm.EnableItem(true, false);
        SelectArmor.SetActive(false);
        showArmorSelect = false;
        ForceCloseArmorPanels.SetActive(false);
        showWeaponSelect = showWeaponSelect ? false : true;
        SelectWeapon.SetActive(showWeaponSelect);
        ForceCloseWeaponPanels.SetActive(showWeaponSelect);
        gm.PickedUpItem();
        if (showWeaponSelect == false)
        {
            GetComponent<Spellbook>().SoundFiles[1].Play();
        }
    }
    public void OpenArmorSelect()
    {
        gm.EnableItem(false, false);
        SelectWeapon.SetActive(false);
        showWeaponSelect = false;
        ForceCloseWeaponPanels.SetActive(false);
        showArmorSelect = showArmorSelect ? false : true;
        SelectArmor.SetActive(showArmorSelect);
        ForceCloseArmorPanels.SetActive(showArmorSelect);
        gm.PickedUpItem();
        if (showArmorSelect == false)
        {
            GetComponent<Spellbook>().SoundFiles[1].Play();
        }
    }


    public void CloseAllItemPanels()
    {
        if (SelectWeapon.activeSelf == true)
        {
            showWeaponSelect = showWeaponSelect ? false : true;
            SelectWeapon.SetActive(showWeaponSelect);
        }
        if (SelectArmor.activeSelf == true)
        {
            showArmorSelect = showArmorSelect ? false : true;
            SelectArmor.SetActive(showArmorSelect);
        }
    }
}



