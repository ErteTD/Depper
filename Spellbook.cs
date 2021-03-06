﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    [Header("Sound files")]
    public List<AudioSource> SoundFiles;
 //   public AudioClip


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
    public GameObject BlessedAim;
    public GameObject Channeling;


    public GameObject GameMang;
    public GameObject ForceClosePanels;
    public GameObject ForceCloseTokenPanel1;
    public GameObject ForceCloseTokenPanel2;

    private GameManager manager;
    [HideInInspector] public GameObject showSpellBook;
    [HideInInspector] public GameObject showSpellBookSpells;

    [Header("Blocked")]
    public Image MeteorBlocked;
    public Image ConeBlocked;
    public Image GhostCastBlocked;
    public Image DoubleCastBlocked;
    public Image SplitCastBlocked;
    public Image ChannelingBlocked;
    public Image BoostBlocked;
    public Image HastenBlocked;
    public Image EmpowerBlocked;
    public Image BHBlocked;
    public Image PushBlocked;
    public Image PoolBlocked;
    public Image BlessedAimBlocked;
    public Image ChaosOrbBlocked;
    public Image CompOrbBlocked;

    public Image Slot1Blocked;
    public Image Slot2Blocked;
    public Image Slot3Blocked;

    [Header("Frames")]
    public Image Frame1;
    public Image Frame2;
    public Image Frame3;
    public Image Frame4;
    public Image Frame5;

    [HideInInspector] public int curSlot;
    private int lvl1choice;
    private int lvl2choice;
    private int lvl3choice;
    public int lvl4choice;
    private int lvl5choice;
    private int lvl6choice;

    private int spellSlot1Choice1;
    private int spellSlot1Choice2;
    private int spellSlot1Choice3;
    private int spellSlot1Choice4;
    private int spellSlot1Choice5;
    private int spellSlot1Choice6;

    private int spellSlot2Choice1;
    private int spellSlot2Choice2;
    private int spellSlot2Choice3;
    private int spellSlot2Choice4;
    private int spellSlot2Choice5;
    private int spellSlot2Choice6;

    private int spellSlot3Choice1;
    private int spellSlot3Choice2;
    private int spellSlot3Choice3;
    private int spellSlot3Choice4;
    private int spellSlot3Choice5;
    private int spellSlot3Choice6;



    // UI stuff
    // SpellBook Spells

    public GameObject SelectedSpell1;
    public GameObject SelectedSpell2;
    public GameObject SelectedSpell3;
    public GameObject SelectedSpell4;
    public GameObject SelectedSpell5;

    public Image fireRing2;
    public Image frostRing2;
    public Image LightningRing2;

    public Image fireRing3; // double
    public Image frostRing3; // split
    public Image LightningRing3; // comp cube

    public Image Boost4;
    public Image Hasten4;
    public Image Empower4;

    public Image BH5;
    public Image Push5;
    public Image Pool5;

    public Image BlessedAim6;
    public Image ChaosOrb6;
    public Image Channeling6;

    // LowerMenu Spellslots
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Color selCol;
    public Color unselCol;
    public Color FrameSelectedCol;

    private int PreviouslySelectedSlot;
    private bool toggleSpells = false;
    private bool finishSpell_;
    private bool DontFinishSpell;
    private bool openBool;

    public bool unselect2;
    public bool unselect3;
    public bool unselect4;
    public bool unselect5;
    public bool unselect6;

  //  public GameObject SpellPanel;

    //Spellslotmemory
    private int slot1Mem1;
    private int slot1Mem2;
    private int slot1Mem3;
    private int slot1Mem4;
    private int slot1Mem5;
    private int slot2Mem1;
    private int slot2Mem2;
    private int slot2Mem3;
    private int slot2Mem4;
    private int slot2Mem5;
    private int slot3Mem1;
    private int slot3Mem2;
    private int slot3Mem3;
    private int slot3Mem4;
    private int slot3Mem5;

    private bool TokenSoundToggle;
    public int CurSlotForScrollWheel;


    public void Start()
    {
        manager = GameMang.GetComponent<GameManager>();
      //  ColorUtility.TryParseHtmlString("#83FF74FF", out selCol);

        spellSlot1Choice1 = 1;
        spellSlot1Choice2 = 0;
        spellSlot1Choice3 = 0;
        spellSlot1Choice4 = 0;
        spellSlot1Choice5 = 0;
        spellSlot1Choice6 = 0;

        spellSlot2Choice1 = 2;
        spellSlot2Choice2 = 0;
        spellSlot2Choice3 = 0;
        spellSlot2Choice4 = 0;
        spellSlot2Choice5 = 0;
        spellSlot2Choice6 = 0;

        spellSlot3Choice1 = 3;
        spellSlot3Choice2 = 0;
        spellSlot3Choice3 = 0;
        spellSlot3Choice4 = 0;
        spellSlot3Choice5 = 0;
        spellSlot3Choice6 = 0;

        SlotOne(); // chose slot 1.
    }

    public void SaveSpellMems()
    {
        manager.SetInt("slot1Mem1", slot1Mem1);
        manager.SetInt("slot1Mem2", slot1Mem2);
        manager.SetInt("slot1Mem3", slot1Mem3);
        manager.SetInt("slot1Mem4", slot1Mem4);
        manager.SetInt("slot1Mem5", slot1Mem5);
        manager.SetInt("slot2Mem1", slot2Mem1);
        manager.SetInt("slot2Mem2", slot2Mem2);
        manager.SetInt("slot2Mem3", slot2Mem3);
        manager.SetInt("slot2Mem4", slot2Mem4);
        manager.SetInt("slot2Mem5", slot2Mem5);
        manager.SetInt("slot3Mem1", slot3Mem1);
        manager.SetInt("slot3Mem2", slot3Mem2);
        manager.SetInt("slot3Mem3", slot3Mem3);
        manager.SetInt("slot3Mem4", slot3Mem4);
        manager.SetInt("slot3Mem5", slot3Mem5);

        manager.SetInt("spellSlot1Choice1", spellSlot1Choice1);
        manager.SetInt("spellSlot1Choice2", spellSlot1Choice2);
        manager.SetInt("spellSlot1Choice3", spellSlot1Choice3);
        manager.SetInt("spellSlot1Choice4", spellSlot1Choice4);
        manager.SetInt("spellSlot1Choice5", spellSlot1Choice5);
        manager.SetInt("spellSlot1Choice6", spellSlot1Choice6);
        manager.SetInt("spellSlot2Choice1", spellSlot2Choice1);
        manager.SetInt("spellSlot2Choice2", spellSlot2Choice2);
        manager.SetInt("spellSlot2Choice3", spellSlot2Choice3);
        manager.SetInt("spellSlot2Choice4", spellSlot2Choice4);
        manager.SetInt("spellSlot2Choice5", spellSlot2Choice5);
        manager.SetInt("spellSlot2Choice6", spellSlot2Choice6);
        manager.SetInt("spellSlot3Choice1", spellSlot3Choice1);
        manager.SetInt("spellSlot3Choice2", spellSlot3Choice2);
        manager.SetInt("spellSlot3Choice3", spellSlot3Choice3);
        manager.SetInt("spellSlot3Choice4", spellSlot3Choice4);
        manager.SetInt("spellSlot3Choice5", spellSlot3Choice5);
        manager.SetInt("spellSlot3Choice6", spellSlot3Choice6);
    }

    public void LoadSpellMems()
    {
        slot1Mem1 = manager.GetInt("slot1Mem1");
        slot1Mem2 = manager.GetInt("slot1Mem2");
        slot1Mem3 = manager.GetInt("slot1Mem3");
        slot1Mem4 = manager.GetInt("slot1Mem4");
        slot1Mem5 = manager.GetInt("slot1Mem5");
        slot2Mem1 = manager.GetInt("slot2Mem1");
        slot2Mem2 = manager.GetInt("slot2Mem2");
        slot2Mem3 = manager.GetInt("slot2Mem3");
        slot2Mem4 = manager.GetInt("slot2Mem4");
        slot2Mem5 = manager.GetInt("slot2Mem5");
        slot3Mem1 = manager.GetInt("slot3Mem1");
        slot3Mem2 = manager.GetInt("slot3Mem2");
        slot3Mem3 = manager.GetInt("slot3Mem3");
        slot3Mem4 = manager.GetInt("slot3Mem4");
        slot3Mem5 = manager.GetInt("slot3Mem5");

        spellSlot1Choice1 = manager.GetInt("spellSlot1Choice1");
        spellSlot1Choice2 = manager.GetInt("spellSlot1Choice2");
        spellSlot1Choice3 = manager.GetInt("spellSlot1Choice3");
        spellSlot1Choice4 = manager.GetInt("spellSlot1Choice4");
        spellSlot1Choice5 = manager.GetInt("spellSlot1Choice5");
        spellSlot1Choice6 = manager.GetInt("spellSlot1Choice6");
        spellSlot2Choice1 = manager.GetInt("spellSlot2Choice1");
        spellSlot2Choice2 = manager.GetInt("spellSlot2Choice2");
        spellSlot2Choice3 = manager.GetInt("spellSlot2Choice3");
        spellSlot2Choice4 = manager.GetInt("spellSlot2Choice4");
        spellSlot2Choice5 = manager.GetInt("spellSlot2Choice5");
        spellSlot2Choice6 = manager.GetInt("spellSlot2Choice6");
        spellSlot3Choice1 = manager.GetInt("spellSlot3Choice1");
        spellSlot3Choice2 = manager.GetInt("spellSlot3Choice2");
        spellSlot3Choice3 = manager.GetInt("spellSlot3Choice3");
        spellSlot3Choice4 = manager.GetInt("spellSlot3Choice4");
        spellSlot3Choice5 = manager.GetInt("spellSlot3Choice5");
        spellSlot3Choice6 = manager.GetInt("spellSlot3Choice6");

        SlotOne();
    }


    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.SpellSlot1Key))) //|| Input.GetKeyDown(KeyCode.Q))
        {
            SlotOne();
        }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.SpellSlot2Key))) //|| Input.GetKeyDown(KeyCode.W))
        {
            SlotTwo();
        }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.SpellSlot3Key))) //|| Input.GetKeyDown(KeyCode.E))
        {
            SlotThree();
        }
    }

    public void OpenSlot(int slot)
    {
    if (!openBool)
        {
             DontFinishSpell = false;
        }
        this.gameObject.GetComponent<ToolTipScript>().CloseAllItemPanels();
        openBool = openBool ? false : true;
    }

    public void LeveloneSpellChoice(int spellNumb)
    {
        lvl1choice = spellNumb;
    }
    public void LeveltwoSpellChoice(int spellNumb)
    {
        Frame1.color = Color.white;
        if (!unselect2 && lvl2choice == spellNumb)
        {
            lvl2choice = 0;
            unselect2 = true;
            SelectedSpell1.SetActive(false);
            if (!DontFinishSpell)
            {
                PlaySpellSound(3);
            }
        }
        else
        {
            lvl2choice = spellNumb;
            unselect2 = false;
            if (spellNumb != 0)
            {
                Frame1.color = FrameSelectedCol;
                SelectedSpell1.SetActive(true);
                if (!DontFinishSpell)
                {
                    PlaySpellSound(2);
                }
            }
        }
        if (!DontFinishSpell)
        {
            FinishSpell();
        }

        switch (lvl2choice)
        {
            case 1:
                fireRing2.color = selCol;
                frostRing2.color = unselCol;
                LightningRing2.color = unselCol;
                SelectedSpell1.transform.localPosition = new Vector3(0, 100, 0);

                break;
            case 2:
                fireRing2.color = unselCol;
                frostRing2.color = selCol;
                LightningRing2.color = unselCol;
                SelectedSpell1.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                fireRing2.color = unselCol;
                frostRing2.color = unselCol;
                LightningRing2.color = selCol;
                SelectedSpell1.transform.localPosition = new Vector3(0, -100, 0);
                break;
            default:
                fireRing2.color = unselCol;
                frostRing2.color = unselCol;
                LightningRing2.color = unselCol;

                CheckCurrency(1);
                break;
        }
    }
    public void LevelthreeSpellChoice(int spellNumb)
    {
        Frame2.color = Color.white;
        if (!unselect3 && lvl3choice == spellNumb)
        {
            lvl3choice = 0;
            unselect3 = true;
            SelectedSpell2.SetActive(false);
            if (!DontFinishSpell)
            {
                PlaySpellSound(3);
            }
        }
        else
        {
            lvl3choice = spellNumb;
            unselect3 = false;
            if (spellNumb != 0)
            {
                Frame2.color = FrameSelectedCol;
                SelectedSpell2.SetActive(true);
                if (!DontFinishSpell)
                {
                    PlaySpellSound(2);
                }
            }
        }
        if (!DontFinishSpell)
        {
            FinishSpell(); // CHANGE MADE HERE.
        }
        switch (lvl3choice)
        {
            case 1:
                fireRing3.color = selCol;
                frostRing3.color = unselCol;
                LightningRing3.color = unselCol;
                SelectedSpell2.transform.localPosition = new Vector3(0, 100, 0);

                break;
            case 2:
                fireRing3.color = unselCol;
                frostRing3.color = selCol;
                LightningRing3.color = unselCol;
                SelectedSpell2.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                fireRing3.color = unselCol;
                frostRing3.color = unselCol;
                LightningRing3.color = selCol;
                SelectedSpell2.transform.localPosition = new Vector3(0, -100, 0);

                break;
            default:
                fireRing3.color = unselCol;
                frostRing3.color = unselCol;
                LightningRing3.color = unselCol;
                CheckCurrency(2);


                break;
        }
    }

    public void LevelfourSpellChoice(int spellNumb)
    {
        Frame3.color = Color.white;
        if (!unselect4 && lvl4choice == spellNumb)
        {
            lvl4choice = 0;
            unselect4 = true;
            SelectedSpell3.SetActive(false);
            if (!DontFinishSpell)
            {
                PlaySpellSound(3);
            }
        }
        else
        {
            lvl4choice = spellNumb;
            unselect4 = false;
            if (spellNumb != 0)
            {
                Frame3.color = FrameSelectedCol;
                SelectedSpell3.SetActive(true);
                if (!DontFinishSpell)
                {
                    PlaySpellSound(2);
                }
            }
        }
        if (!DontFinishSpell)
        {
            FinishSpell(); // CHANGE MADE HERE.
        }

        switch (lvl4choice) //stuff
        {
            case 1:
                Boost4.color = selCol;
                Hasten4.color = unselCol;
                Empower4.color = unselCol;
                SelectedSpell3.transform.localPosition = new Vector3(0, 100, 0);
                break;
            case 2:
                Boost4.color = unselCol;
                Hasten4.color = selCol;
                Empower4.color = unselCol;
                SelectedSpell3.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                Boost4.color = unselCol;
                Hasten4.color = unselCol;
                Empower4.color = selCol;
                SelectedSpell3.transform.localPosition = new Vector3(0, -100, 0);
                break;
            default:
                Boost4.color = unselCol;
                Hasten4.color = unselCol;
                Empower4.color = unselCol;
                CheckCurrency(3);


                break;
        }
    }


    public void LevelfiveSpellChoice(int spellNumb)
    {
        Frame4.color = Color.white;
        if (!unselect5 && lvl5choice == spellNumb)
        {
            lvl5choice = 0;
            unselect5 = true;
            SelectedSpell4.SetActive(false);
            if (!DontFinishSpell)
            {
                PlaySpellSound(3);
            }
        }
        else
        {
            lvl5choice = spellNumb;
            unselect5 = false;
            if (spellNumb != 0)
            {
                Frame4.color = FrameSelectedCol;
                SelectedSpell4.SetActive(true);
                if (!DontFinishSpell)
                {
                    PlaySpellSound(2);
                }
            }
        }
        if (!DontFinishSpell)
        {
            FinishSpell(); // CHANGE MADE HERE.
        }
        switch (lvl5choice) //stuff
        {
            case 1:
                BH5.color = selCol;
                Push5.color = unselCol;
                Pool5.color = unselCol;
                SelectedSpell4.transform.localPosition = new Vector3(0, 100, 0);

                break;
            case 2:
                BH5.color = unselCol;
                Push5.color = selCol;
                Pool5.color = unselCol;
                SelectedSpell4.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                BH5.color = unselCol;
                Push5.color = unselCol;
                Pool5.color = selCol;
                SelectedSpell4.transform.localPosition = new Vector3(0, -100, 0);
                break;
            default:
                BH5.color = unselCol;
                Push5.color = unselCol;
                Pool5.color = unselCol;
                CheckCurrency(4);
                break;
        }
    }

    public void LevelsixSpellChoice(int spellNumb)
    {
        Frame5.color = Color.white;
        if (!unselect6 && lvl6choice == spellNumb)
        {
            lvl6choice = 0;
            unselect6 = true;
            SelectedSpell5.SetActive(false);
            if (!DontFinishSpell)
            {
                PlaySpellSound(3);
            }
        }
        else
        {
            lvl6choice = spellNumb;
            unselect6 = false;
            if (spellNumb != 0)
            {
                Frame5.color = FrameSelectedCol;
                SelectedSpell5.SetActive(true);
                if (!DontFinishSpell)
                {
                    PlaySpellSound(2);
                }
            }
        }
        if (!DontFinishSpell)
        {
            FinishSpell(); // CHANGE MADE HERE.
        }
        switch (lvl6choice) //stuff
        {
            case 1:
                ChaosOrb6.color = selCol;
                Channeling6.color = unselCol;
                BlessedAim6.color = unselCol;
                SelectedSpell5.transform.localPosition = new Vector3(0, 100, 0);
                break;
            case 2:
                ChaosOrb6.color = unselCol;
                Channeling6.color = selCol;
                BlessedAim6.color = unselCol;
                SelectedSpell5.transform.localPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                ChaosOrb6.color = unselCol;
                Channeling6.color = unselCol;
                BlessedAim6.color = selCol;
                SelectedSpell5.transform.localPosition = new Vector3(0, -100, 0);
                break;
            default:
                ChaosOrb6.color = unselCol;
                Channeling6.color = unselCol;
                BlessedAim6.color = unselCol;
                CheckCurrency(5);
                break;
        }
    }

    public void FinishSpell()
    {
        if (curSlot == 1)
        {
            spellSlot1Choice1 = lvl1choice;
            spellSlot1Choice2 = lvl2choice;
            spellSlot1Choice3 = lvl3choice;
            spellSlot1Choice4 = lvl4choice;
            spellSlot1Choice5 = lvl5choice;
            spellSlot1Choice6 = lvl6choice;

            slot1Mem1 = lvl2choice;
            slot1Mem2 = lvl3choice;
            slot1Mem3 = lvl4choice;
            slot1Mem4 = lvl5choice;
            slot1Mem5 = lvl6choice;
            SlotOneTip();
        }
        if (curSlot == 2)
        {
            spellSlot2Choice1 = lvl1choice;
            spellSlot2Choice2 = lvl2choice;
            spellSlot2Choice3 = lvl3choice;
            spellSlot2Choice4 = lvl4choice;
            spellSlot2Choice5 = lvl5choice;
            spellSlot2Choice6 = lvl6choice;
            slot2Mem1 = lvl2choice;
            slot2Mem2 = lvl3choice;
            slot2Mem3 = lvl4choice;
            slot2Mem4 = lvl5choice;
            slot2Mem5 = lvl6choice;
            SlotTwoTip();
        }
        if (curSlot == 3)
        {
            spellSlot3Choice1 = lvl1choice;
            spellSlot3Choice2 = lvl2choice;
            spellSlot3Choice3 = lvl3choice;
            spellSlot3Choice4 = lvl4choice;
            spellSlot3Choice5 = lvl5choice;
            spellSlot3Choice6 = lvl6choice;

            slot3Mem1 = lvl2choice;
            slot3Mem2 = lvl3choice;
            slot3Mem3 = lvl4choice;
            slot3Mem4 = lvl5choice;
            slot3Mem5 = lvl6choice;
            SlotThreeTip();
        }
    }

    public void SlotOne()
    {
        slot1.color = selCol;
        slot2.color = Color.white;
        slot3.color = Color.white;


        if (!finishSpell_)
        {
            LeveloneSpell(spellSlot1Choice1);
            LeveltwoSpell(spellSlot1Choice2);
            LevelthreeSpell(spellSlot1Choice3);
            LevelfourSpell(spellSlot1Choice4);
            LevelfiveSpell(spellSlot1Choice5);
            LevelsixSpell(spellSlot1Choice6);
        }

        CastSpell.FindObjectOfType<CastSpell>().currentSlot = 1;

    }
    public void SlotTwo()
    {
        slot1.color = Color.white;
        slot2.color = selCol;
        slot3.color = Color.white;

        if (!finishSpell_)
        {
            LeveloneSpell(spellSlot2Choice1);
            LeveltwoSpell(spellSlot2Choice2);
            LevelthreeSpell(spellSlot2Choice3);
            LevelfourSpell(spellSlot2Choice4);
            LevelfiveSpell(spellSlot2Choice5);
            LevelsixSpell(spellSlot2Choice6);
        }
        CastSpell.FindObjectOfType<CastSpell>().currentSlot = 2;
    }
    public void SlotThree()
    {
        slot1.color = Color.white;
        slot2.color = Color.white;
        slot3.color = selCol;

        if (!finishSpell_)
        {
            LeveloneSpell(spellSlot3Choice1);
            LeveltwoSpell(spellSlot3Choice2);
            LevelthreeSpell(spellSlot3Choice3);
            LevelfourSpell(spellSlot3Choice4);
            LevelfiveSpell(spellSlot3Choice5);
            LevelsixSpell(spellSlot3Choice6);
        }
        CastSpell.FindObjectOfType<CastSpell>().currentSlot = 3;
    }

    public void SlotOneTip()
    {
        this.GetComponent<ToolTipScript>().SpellCombTip(spellSlot1Choice1);
        this.GetComponent<ToolTipScript>().SpellCombTip(20 + spellSlot1Choice2);
        this.GetComponent<ToolTipScript>().SpellCombTip(30 + spellSlot1Choice3);
        this.GetComponent<ToolTipScript>().SpellCombTip(40 + spellSlot1Choice4);
        this.GetComponent<ToolTipScript>().SpellCombTip(50 + spellSlot1Choice5);
        this.GetComponent<ToolTipScript>().SpellCombTip(60 + spellSlot1Choice6);
    }
    public void SlotTwoTip()
    {
        this.GetComponent<ToolTipScript>().SpellCombTip(spellSlot2Choice1);
        this.GetComponent<ToolTipScript>().SpellCombTip(20 + spellSlot2Choice2);
        this.GetComponent<ToolTipScript>().SpellCombTip(30 + spellSlot2Choice3);
        this.GetComponent<ToolTipScript>().SpellCombTip(40 + spellSlot2Choice4);
        this.GetComponent<ToolTipScript>().SpellCombTip(50 + spellSlot2Choice5);
        this.GetComponent<ToolTipScript>().SpellCombTip(60 + spellSlot2Choice6);
    }
    public void SlotThreeTip()
    {
        this.GetComponent<ToolTipScript>().SpellCombTip(spellSlot3Choice1);
        this.GetComponent<ToolTipScript>().SpellCombTip(20 + spellSlot3Choice2);
        this.GetComponent<ToolTipScript>().SpellCombTip(30 + spellSlot3Choice3);
        this.GetComponent<ToolTipScript>().SpellCombTip(40 + spellSlot3Choice4);
        this.GetComponent<ToolTipScript>().SpellCombTip(50 + spellSlot3Choice5);
        this.GetComponent<ToolTipScript>().SpellCombTip(60 + spellSlot3Choice6);
    }

    public void ExitSlotTip()
    {
        if (!toggleSpells)
        {
            this.GetComponent<ToolTipScript>().Empty1();
            this.GetComponent<ToolTipScript>().Empty2();
            this.GetComponent<ToolTipScript>().Empty3();
            this.GetComponent<ToolTipScript>().Empty4();
            this.GetComponent<ToolTipScript>().Empty5();
            this.GetComponent<ToolTipScript>().Empty6();
            this.GetComponent<ToolTipScript>().SpellCombTip(0);
            this.GetComponent<ToolTipScript>().CurSpellToolTipBox2.SetActive(false);
            manager.PickedUpItem();
        }
    }

    public void CheckCurrency(int level)
    {

        manager.CheckIfTokenBought(curSlot, level);
        switch (level)
        {
            case 1:
                if (manager.meteorToken == 0 && !manager.SlotIsInUse1)
                {
                    MeteorBlocked.enabled = true;
                }
                else MeteorBlocked.enabled = false;
                if (manager.coneToken == 0 && !manager.SlotIsInUse2)
                {
                    ConeBlocked.enabled = true;
                }
                else ConeBlocked.enabled = false;
                if (manager.ghostToken == 0 && !manager.SlotIsInUse3)
                {
                    GhostCastBlocked.enabled = true;
                }
                else GhostCastBlocked.enabled = false;

                break;
            case 2:
                if (manager.doubleToken == 0 && !manager.SlotIsInUse1)
                {
                    DoubleCastBlocked.enabled = true;
                }
                else DoubleCastBlocked.enabled = false;
                if (manager.splitToken == 0 && !manager.SlotIsInUse2)
                {
                    SplitCastBlocked.enabled = true;
                }
                else SplitCastBlocked.enabled = false;
                if (manager.CompToken == 0 && !manager.SlotIsInUse3)
                {
                    CompOrbBlocked.enabled = true;
                }
                else CompOrbBlocked.enabled = false;
                break;
            case 3:
                if (manager.boostToken == 0 && !manager.SlotIsInUse1)
                {
                    BoostBlocked.enabled = true;
                }
                else BoostBlocked.enabled = false;
                if (manager.hastenToken == 0 && !manager.SlotIsInUse2)
                {
                    HastenBlocked.enabled = true;
                }
                else HastenBlocked.enabled = false;
                if (manager.empowerToken == 0 && !manager.SlotIsInUse3)
                {
                    EmpowerBlocked.enabled = true;
                }
                else EmpowerBlocked.enabled = false;
                break;

            case 4:
                if (manager.bhToken == 0 && !manager.SlotIsInUse1)
                {
                    BHBlocked.enabled = true;
                }
                else BHBlocked.enabled = false;
                if (manager.pushToken == 0 && !manager.SlotIsInUse2)
                {
                    PushBlocked.enabled = true;
                }
                else PushBlocked.enabled = false;
                if (manager.poolToken == 0 && !manager.SlotIsInUse3)
                {
                    PoolBlocked.enabled = true;
                }
                else PoolBlocked.enabled = false;
                break;
            case 5:
                if (manager.ChaosToken == 0 && !manager.SlotIsInUse1)
                {
                    ChaosOrbBlocked.enabled = true;
                }
                else ChaosOrbBlocked.enabled = false;
                if (manager.channelingToken == 0 && !manager.SlotIsInUse2)
                {
                    ChannelingBlocked.enabled = true;
                }
                else ChannelingBlocked.enabled = false;
                if (manager.AimToken == 0 && !manager.SlotIsInUse3)
                {
                    BlessedAimBlocked.enabled = true;
                }
                else BlessedAimBlocked.enabled = false;
                break;       
        }
    }

    public void ForceCloseSpellBookPanel()
    {
        if (toggleSpells)
        {
            Spellslot(PreviouslySelectedSlot);
            LeveloneSpellChoice(PreviouslySelectedSlot);
            switch (PreviouslySelectedSlot)
            {
                case 1:
                    SlotOne();
                    break;
                case 2:
                    SlotTwo();
                    break;
                case 3:
                    SlotThree();
                    break;
            }
            OpenSlot(PreviouslySelectedSlot);
            ExitSlotTip();
        }
        else
        {
            PlaySpellSound(1);
        }
    }



    public void Spellslot(int slotNumber)
    {
        Frame1.color = Color.white;
        Frame2.color = Color.white;
        Frame3.color = Color.white;
        Frame4.color = Color.white;
        Frame5.color = Color.white;

        lvl1choice = 0; // CHANGE MADE HERE.
        lvl2choice = 0;
        lvl3choice = 0;
        lvl4choice = 0;
        lvl5choice = 0;
        lvl6choice = 0;
        toggleSpells = toggleSpells ? false : true;
        finishSpell_ = finishSpell_ ? false : true;

           gameObject.GetComponent<ToolTipScript>().CloseAllItemPanels();
            gameObject.GetComponent<ToolTipScript>().ForceCloseArmorPanels.SetActive(false);

            gameObject.GetComponent<ToolTipScript>().ForceCloseWeaponPanels.SetActive(false);

        manager.DisableSpellSlotEffect();
        if (toggleSpells)
        {
            PreviouslySelectedSlot = slotNumber;
            PlaySpellSound(1);
        }
        else if (PreviouslySelectedSlot != slotNumber)
        {
            toggleSpells = true;
            finishSpell_ = true;
            PreviouslySelectedSlot = slotNumber;
            openBool = !openBool;
            PlaySpellSound(0);
        }
        else
        {
            PlaySpellSound(1);
        }

        showSpellBook.SetActive(toggleSpells);
        showSpellBookSpells.SetActive(toggleSpells);
        ForceClosePanels.SetActive(toggleSpells);

        if (!toggleSpells)
        {
            ForceCloseTokenPanel1.SetActive(toggleSpells);
            ForceCloseTokenPanel2.SetActive(toggleSpells);
        }

        curSlot = slotNumber;

        CheckCurrency(1);
        CheckCurrency(2);
        CheckCurrency(3);
        CheckCurrency(4);
        CheckCurrency(5);


        switch (slotNumber)
        {
            case 1:
                if (manager.meteorToken_ == true || manager.coneToken_ == true || manager.ghostToken_ == true)
                {
                    unselect2 = true;
                }
                else
                {
                    unselect2 = false;
                }

                if (manager.doubleToken_ == true || manager.splitToken_ == true || manager.CompToken_ == true)
                {
                    unselect3 = true;
                }
                else
                {
                    unselect3 = false;
                }

                if (manager.boostToken_ == true || manager.hastenToken_ == true || manager.empowerToken_ == true)
                {
                    unselect4 = true;
                }
                else
                {
                    unselect4 = false;
                }

                if (manager.bhToken_ == true || manager.pushToken_ == true || manager.poolToken_ == true)
                {
                    unselect5 = true;
                }
                else
                {
                    unselect5 = false;
                }
                if (manager.ChaosToken_ == true || manager.channelingToken_ == true || manager.AimToken_ == true)
                {
                    unselect6 = true;
                }
                else
                {
                    unselect6 = false;
                }

                lvl1choice = slotNumber; // CHANGE MADE HERE.
                DontFinishSpell = true;
                LeveltwoSpellChoice(slot1Mem1);
                LevelthreeSpellChoice(slot1Mem2);
                LevelfourSpellChoice(slot1Mem3);
                LevelfiveSpellChoice(slot1Mem4);
                LevelsixSpellChoice(slot1Mem5);

                break;
            case 2:

           //     showSpellBook.transform.position = new Vector3(slot2.transform.position.x, showSpellBook.transform.position.y, showSpellBook.transform.position.z);
                //   SpellPanel.transform.position = new Vector3(showSpellBook.transform.position.x + 500, SpellPanel.transform.position.y, SpellPanel.transform.position.z);

                if (manager.meteorToken_2 == true || manager.coneToken_2 == true || manager.ghostToken_2 == true)
                {
                    unselect2 = true;
                }
                else
                {
                    unselect2 = false;
                }

                if (manager.doubleToken_2 == true || manager.splitToken_2 == true || manager.CompToken_2 == true)
                {
                    unselect3 = true;
                }
                else
                {
                    unselect3 = false;
                }

                if (manager.boostToken_2 == true || manager.hastenToken_2 == true || manager.empowerToken_2 == true)
                {
                    unselect4 = true;
                }
                else
                {
                    unselect4 = false;
                }

                if (manager.bhToken_2 == true || manager.pushToken_2 == true || manager.poolToken_2 == true)
                {
                    unselect5 = true;
                }
                else
                {
                    unselect5 = false;
                }
                if (manager.ChaosToken_2 == true || manager.channelingToken_2 == true || manager.AimToken_2 == true)
                {
                    unselect6 = true;
                }
                else
                {
                    unselect6 = false;
                }
                lvl1choice = slotNumber; // change made here.
                DontFinishSpell = true;
                LeveltwoSpellChoice(slot2Mem1);
                LevelthreeSpellChoice(slot2Mem2);
                LevelfourSpellChoice(slot2Mem3);
                LevelfiveSpellChoice(slot2Mem4);
                LevelsixSpellChoice(slot2Mem5);

                break;
            case 3:

            //    showSpellBook.transform.position = new Vector3(slot3.transform.position.x, showSpellBook.transform.position.y, showSpellBook.transform.position.z);
                //  SpellPanel.transform.position = new Vector3(showSpellBook.transform.position.x + 500, SpellPanel.transform.position.y, SpellPanel.transform.position.z);

                if (manager.meteorToken_3 == true || manager.coneToken_3 == true || manager.ghostToken_3 == true)
                {
                    unselect2 = true;
                }
                else
                {
                    unselect2 = false;
                }

                if (manager.doubleToken_3 == true || manager.splitToken_3 == true || manager.CompToken_3 == true)
                {
                    unselect3 = true;
                }
                else
                {
                    unselect3 = false;
                }

                if (manager.boostToken_3 == true || manager.hastenToken_3 == true || manager.empowerToken_3 == true)
                {
                    unselect4 = true;
                }
                else
                {
                    unselect4 = false;
                }

                if (manager.bhToken_3 == true || manager.pushToken_3 == true || manager.poolToken_3 == true)
                {
                    unselect5 = true;
                }
                else
                {
                    unselect5 = false;
                }

                if (manager.ChaosToken_3 == true || manager.channelingToken_3 == true || manager.AimToken_3 == true)
                {
                    unselect6 = true;
                }
                else
                {
                    unselect6 = false;
                }
                lvl1choice = slotNumber; // change made here.
                DontFinishSpell = true;
                LeveltwoSpellChoice(slot3Mem1);
                LevelthreeSpellChoice(slot3Mem2);
                LevelfourSpellChoice(slot3Mem3);
                LevelfiveSpellChoice(slot3Mem4);
                LevelsixSpellChoice(slot3Mem5);
                break;
        }
    }


    public void LeveloneSpell(int lvl1choiceNOW)
    {
        CastSpell cast = CastSpell.FindObjectOfType<CastSpell>();
        Fireball fire = Fireball.GetComponent<Fireball>();
        FrostBolt frost = FrostBolt.GetComponent<FrostBolt>();
        LightningBolt lightning = LightningBolt.GetComponent<LightningBolt>();


        if (lvl1choiceNOW == 1)
        {
            cast.spellname = fire.spellname;
            cast.projectilespeed = fire.projectilespeed;
            cast.damage1Pure = fire.damagePure;
            cast.currentspellObject = fire.projectile;
            cast.currentConeObject = fire.Cone;
            cast.currentChannel = fire.Channel;
            cast.currentChannelCone = fire.ChannelCone;
            cast.currentChanMet = fire.ChannelMeteor;
            cast.currentMeteor = fire.Meteor;
            cast.cd1Pure = fire.cooldownSeconds;
            cast.FireBallBurn = fire.FireBallBurn;
            cast.BurnDuration = fire.BurnDuration;
            cast.BurnPercent = fire.BurnPercent;
            cast.FrostBoltSlow = false;
            cast.LBBounce = false;
            cast.PoolInst = fire.Pool;
            Player.FindObjectOfType<Player>().spellrange = fire.spellrange;
        }

        if (lvl1choiceNOW == 2)
        {
            cast.spellname = frost.spellname;
            cast.projectilespeed = frost.projectilespeed;
            cast.damage1Pure = frost.damagePure;
            cast.currentspellObject = frost.projectile;
            cast.currentConeObject = frost.Cone;
            cast.currentChannel = frost.Channel;
            cast.currentChannelCone = frost.ChannelCone;
            cast.currentChanMet = frost.ChannelMeteor;
            cast.currentMeteor = frost.Meteor;
            cast.cd1Pure = frost.cooldownSeconds;
            cast.FrostBoltSlow = frost.FrostBoltSlow;
            cast.SlowDuration = frost.SlowDuration;
            cast.SlowPercent = frost.SlowPercent;
            cast.FireBallBurn = false;
            cast.LBBounce = false;
            cast.PoolInst = frost.Pool;
            Player.FindObjectOfType<Player>().spellrange = frost.spellrange;
        }

        if (lvl1choiceNOW == 3)
        {
            cast.spellname = lightning.spellname;
            cast.projectilespeed = lightning.projectilespeed;
            cast.damage1Pure = lightning.damagePure;
            cast.currentspellObject = lightning.projectile;
            cast.currentMeteor = lightning.Meteor;
            cast.currentChannelCone = lightning.ChannelCone;
            cast.currentChannel = lightning.Channel;
            cast.currentConeObject = lightning.Cone;
            cast.cd1Pure = lightning.cooldownSeconds;
            cast.FrostBoltSlow = false;
            cast.FireBallBurn = false;
            cast.LBBounce = lightning.LBBounce;
            cast.LBBounceAmount = lightning.LBBounceAmount;
            cast.PoolInst = lightning.Pool;
            Player.FindObjectOfType<Player>().spellrange = lightning.spellrange;
        }
        if (lvl1choiceNOW == 0)
        {
            cast.spellname = "";
            cast.projectilespeed = 0;
            cast.damage1Pure = 0;
            cast.currentspellObject = null;
            cast.cd1Pure = 0;
            cast.FrostBoltSlow = false;
            cast.FireBallBurn = false;
            cast.LBBounce = false;
            cast.LBBounceAmount = 0;
            Player.FindObjectOfType<Player>().spellrange = 0;
        }

    }

    public void LeveltwoSpell(int lvl2choiceNOW)
    {
        CastSpell cast = CastSpell.FindObjectOfType<CastSpell>();
        Meteor meteor = Meteor.GetComponent<Meteor>();
        GhostCast ghost = GhostCast.GetComponent<GhostCast>();
        Cone cone = Cone.GetComponent<Cone>();

        if (lvl2choiceNOW == 1)
        { // Meteor
            cast.damage2Pure = meteor.damagePure;
            cast.aoeSizeMeteor = meteor.aoeSize;
            cast.cd2Pure = meteor.cooldownSeconds;
            cast.ghostCast = false; // needs to be here to remove ghostchast
            cast.cone = false;
        }

        if (lvl2choiceNOW == 2)
        { // Cone - Aoe infront of caster.
            cast.cone = cone.cone;
            cast.damage2Pure = cone.damagePure;
            cast.cd2Pure = cone.cooldownSeconds;
            cast.aoeSizeMeteor = 0f;
            cast.ghostCast = false;
        }

        if (lvl2choiceNOW == 3)
        { //GhostCast (spells pass through enemies).
            cast.damage2Pure = ghost.damagePure;
            cast.ghostCast = ghost.ghostCast;
            cast.cd2Pure = ghost.cooldownSeconds;
            cast.aoeSizeMeteor = 0f; // needs to be here to remove the meteor thingy.. 
            cast.cone = false;
        }
        if (lvl2choiceNOW == 0)
        {
            cast.damage2Pure = 0f;
            cast.aoeSizeMeteor = 0f;
            cast.cd2Pure = 0f;
            cast.cone = false;
            cast.ghostCast = false;
        }
    }

    public void LevelthreeSpell(int lvl3choiceNOW)
    {
        CastSpell cast = CastSpell.FindObjectOfType<CastSpell>();
        DoubleCast dCast = DoubleCast.GetComponent<DoubleCast>();
        SplitCast split = SplitCast.GetComponent<SplitCast>();
        Companion Corb = CompOrb.GetComponent<Companion>();

        if (lvl3choiceNOW == 1)
        { // Multicast

            cast.doubleCast = dCast.doubleCast;
            cast.cd1Per = dCast.cooldownPercent;
            cast.splitCast = false;
            cast.damage1Per = dCast.damagePercent;
            cast.CompOrb = false;
        }

        if (lvl3choiceNOW == 2)
        { // Splitcast
            cast.doubleCast = false;
            cast.splitCast = split.splitCast;
            cast.cd1Per = split.cooldownPercent;
            cast.damage1Per = split.damagePercent;
            cast.CompOrb = false;
        }

        if (lvl3choiceNOW == 3)
        { //CompOrb
            cast.CompOrbCD = Corb.Cooldown;
            cast.CompOrbDur = Corb.duration;
            cast.CompOrb = Corb.CompOrb;
            cast.damage1Per = Corb.damagePercent;
            cast.cd1Per = Corb.cooldownPercent;
            cast.doubleCast = false;
            cast.splitCast = false;
        }
        if (lvl3choiceNOW == 4)
        { // Multicast

            cast.doubleCast = dCast.doubleCast;
            cast.cd1Per = dCast.cooldownPercent * split.cooldownPercent;
            cast.splitCast = true;
            cast.damage1Per = dCast.damagePercent * split.damagePercent;
            cast.CompOrb = false;
        }



        if (lvl3choiceNOW == 0)
        {
            cast.damage1Per = 1f;
            cast.doubleCast = false;
            cast.splitCast = false;
            cast.CompOrb = false;
            cast.cd1Per = 1f;
        }
    }

    public void LevelfourSpell(int lvl4choiceNOW)
    {
        CastSpell cast = CastSpell.FindObjectOfType<CastSpell>();
        Boost boost = Boost.GetComponent<Boost>();
        Hasten hasten = Hasten.GetComponent<Hasten>();
        Empower empower = Empower.GetComponent<Empower>();

        if (lvl4choiceNOW == 1)
        { // Boost
            cast.damage3Pure = boost.damagePure; //TODO ehum order of damage.
            cast.damage2Per = boost.damagePercent;
            cast.BoostCrit = boost.BoostCrit;
            cast.CritChance = boost.CritChance;
            cast.CritDamage = boost.CritDamage;
            cast.cd2Per = 1f;
            cast.HastenBool = false;
        }
        if (lvl4choiceNOW == 2)
        { //Hasten
            cast.cd2Per = hasten.cooldownPercent;
            cast.projectilespeed *= hasten.ProjectileSpeed;
            cast.HastenChance = hasten.HastenChance;
            cast.HastenBool = hasten.HastenBool;
            cast.BoostCrit = false;
            cast.damage3Pure = 0f;
            cast.damage2Per = 1f;
        }
        if (lvl4choiceNOW == 3)
        { //Empower OBS don't need to reset empower or specify for other lvl 4 spells, because always redone when "lvl1" spell in called.
            cast.SlowPercent = empower.SlowPercent;
            cast.SlowDuration = empower.SlowDuration;
            cast.cd2Per = 1f;
            cast.BurnPercent = empower.BurnPercent;
            cast.BurnDuration = empower.BurnDuration;
            cast.LBBounceAmount = empower.LBBounceAmount;
            cast.BoostCrit = false;
            cast.HastenBool = false;
            cast.damage3Pure = 0f;
            cast.damage2Per = 1f;
        }
        if (lvl4choiceNOW == 0)
        {
            cast.damage3Pure = 0f;
            cast.damage2Per = 1f;
            cast.cd2Per = 1f;
            cast.HastenBool = false;
            cast.BoostCrit = false;

        }
    }
    public void LevelfiveSpell(int lvl5choiceNOW)
    {
        CastSpell cast = CastSpell.FindObjectOfType<CastSpell>();
        BlackHole BH = BlackHole.GetComponent<BlackHole>();
        Push push = Push.GetComponent<Push>();
        Pool pool = Pool.GetComponent<Pool>();

        if (lvl5choiceNOW == 1)
        { // BlackHole
            cast.cd3Pure = BH.cooldownSeconds;
            cast.BHDuration = BH.BHDuration;
            cast.BHSize = BH.BHSize;
            cast.BHRadius = BH.BHRadius;
            cast.BHStrenght = BH.BHStrenght;
            cast.BHBool = BH.BHBool;
            cast.Push = false;
            cast.pool = false;
        }

        if (lvl5choiceNOW == 2)
        { //Push

            cast.cd3Pure = push.cooldownSeconds;
            cast.Push = push.push;
            cast.BHBool = false;
            cast.pool = false;
        }

        if (lvl5choiceNOW == 3)
        { //Pool

            cast.cd3Pure = pool.cooldownSeconds;
            cast.pool = pool.pool;
            cast.Poolduration = pool.duration;
            cast.PoolDamage = pool.damageMod;
            cast.BHBool = false;
            cast.Push = false;

        }
        if (lvl5choiceNOW == 0)
        {
            cast.cd3Pure = 0f;
            cast.BHBool = false;
            cast.Push = false;
            cast.pool = false;
            cast.PoolDamage = 0f;

        }
    }

    public void LevelsixSpell(int lvl6choiceNOW)
    {
        CastSpell cast = CastSpell.FindObjectOfType<CastSpell>();
        ChaosOrb Chaos = ChaosOrb.GetComponent<ChaosOrb>();
        Channeling channel = Channeling.GetComponent<Channeling>();
        BlessedAim Aim = BlessedAim.GetComponent<BlessedAim>();

        if (lvl6choiceNOW == 1)
        { // Chaos

            cast.cd4Per = Chaos.cooldownPercent;
            cast.cd4Pure = Chaos.cooldownSeconds;
            cast.ChaosOrb_ = Chaos.ChaosOrbBool;
            cast.damage4Per = 1;
            cast.ChaosOrbAttackCD = Chaos.ChaosOrbAttackCD;
            cast.ChaosOrbDuration = Chaos.ChaosOrbDuration;
            cast.channel = false;
            cast.BlessedAim = false;
        }

        if (lvl6choiceNOW == 2)
        { //Channel

            cast.damage4Per = channel.damagePercent;
            cast.cd4Pure = channel.cooldownSeconds;
            cast.channel = channel.channeling;
            cast.cd4Per = channel.cooldownPercent;
            cast.chanDur = channel.chanDur;
            cast.ChaosOrb_ = false;
            cast.BlessedAim = false;

        }

        if (lvl6choiceNOW == 3)
        { //Blessed
            cast.cd4Per = Aim.cooldownPercent;
            cast.damage4Per = Aim.damagePercent;
            cast.BlessedAim = Aim.BlessedAimBool;
            cast.cd4Pure = 0;
            cast.channel = false;
            cast.ChaosOrb_ = false;
        }
        if (lvl6choiceNOW == 4)
        {
            cast.cd4Per = Aim.cooldownPercent * Chaos.cooldownPercent;
            cast.damage4Per = Aim.damagePercent;
            cast.BlessedAim = Aim.BlessedAimBool;
            cast.cd4Pure = Chaos.cooldownSeconds;
            cast.channel = false;
            cast.ChaosOrb_ = Chaos.ChaosOrbBool;
            cast.ChaosOrbAttackCD = Chaos.ChaosOrbAttackCD;
            cast.ChaosOrbDuration = Chaos.ChaosOrbDuration;
        }
        if (lvl6choiceNOW == 5)
        {
            cast.cd4Per = channel.cooldownPercent * Chaos.cooldownPercent;
            cast.damage4Per = channel.damagePercent;
            cast.BlessedAim = false;
            cast.cd4Pure = Chaos.cooldownSeconds;
            cast.channel = channel.channeling; ;
            cast.chanDur = channel.chanDur;
            cast.ChaosOrb_ = Chaos.ChaosOrbBool;
            cast.ChaosOrbAttackCD = Chaos.ChaosOrbAttackCD;
            cast.ChaosOrbDuration = Chaos.ChaosOrbDuration;
            cast.cone = false;
            cast.aoeSizeMeteor = 5;
        }

        if (lvl6choiceNOW == 0)
        {
            cast.cd4Per = 1;
            cast.damage4Per = 1;
            cast.cd4Pure = 0;
            cast.BlessedAim = false;
            cast.channel = false;
            cast.ChaosOrb_ = false;
        }
    }


    public void PlaySpellSound(int sound)
    {
        SoundFiles[sound].Play();
    }

}
