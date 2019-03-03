using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public Button EnableSaveButton;
    public Text EnableSaveButtonText;
    public bool EnableSave;
    private Color AlphaFade;
    public SteamAchievements SteamAch;
    public bool CheatsEnabled;
    public GameObject TestMenu;
    public bool SlotIsInUse1;
    public bool SlotIsInUse2;
    public bool SlotIsInUse3;
    public Texture2D DefCursor;
    public Texture2D HoverOverObjectCursor;
    public GameObject EscMenu;
    public static int minRoom_, maxRoom_, CurrentLevel_, CurrentMode_;
    public static bool StartLevel_, GiveLoot_, LevelHasHadMiniBoss_, LevelHasHadEvent_, GenerateFromLoad_;
    public static string SaveFileValidation;
    public GameObject BossHealth;
    public List<GameObject> SelectSpellsEffect;
    public GameObject SelectWeaponEffect;
    public GameObject SelectArmorEffect;

    public GameObject SpellPanelOpen;
    public GameObject WeaponpanelOpen;
    public GameObject ArmorPanelOpen;
    [Header("GameStats")]
    public float PlayTime;
    public float DeathCount;
    public float DamageReceived;
    public float GoldSpent;
    

    [Header("Menu")]
    public Text PlayTimeText;
    public Text DeathsText;
    public Text DamageReceivedText;
    public Text GoldSpentText;

    [Header("ControlsFromKeybind")]
    public Text CastSpellLocText;
    public Text MoveLocText;
    public Text SpellSlot1Text;
    public Text SpellSlot2Text;
    public Text SpellSlot3Text;
    public Text ItemKeyText;
    public Text MoveUpKeyText;
    public Text MoveDownKeyText;
    public Text MoveLeftKeyText;
    public Text MoveRightKeyText;


    [Header("ShopStuff")]
    public int RandomSpellBuyCost;
    public int RandomItemBuyCost;
    public int PotionBuyCost;
    public int SellSpellGold;
    public int SellItemGold;
    private bool SellTokenSuccess;
    public bool ShowSellWindowBool;
    public bool ShowSellItemWindowBool;
    public AudioSource NotEnoughGoldSound;
    public AudioSource CloseShopWindow;
    public AudioSource SellTokenSound;
    public GameObject ShopWindow;
    public GameObject SellTokenWindow;
    public GameObject SellItemWindow;
    public Vector3 CurrentShopLocation;
    public List<GameObject> Items;
    public bool ForceHideAllOtherWindows;
    public Transform ShopRoom;

    [Header("Music")]
    public AudioSource NormalTheme;
    public AudioSource BossBattle;

    [Header("Currency")]
    public int Money;
    public int Lives;
    public Text CurrentGoldText;
    public Text CurrentLivesText;
    public Text CurrentGameModeText;
    public Text DeathScreenLivesText;
    public Text CurrentLevelText;

    [Header("Inventory Weapon")]
    public GameObject SpiderWeapon;
    public int SpiderWeaponToken;
    public GameObject BlinkWeapon;
    public int BlinkWeaponToken;
    public GameObject FireWeapon;
    public int FireWeaponToken;
    public GameObject IKWeapon;
    public int IKWeaponToken;
    public GameObject BlobWeapon;
    public int BlobWeaponToken;
    public GameObject TimeWeapon;
    public int TimeWeaponToken;
    public GameObject FrostWeapon;
    public int FrostWeaponToken;
    public GameObject StrenghtWeapon;
    public int StrenghtWeaponToken;
    public GameObject MadnessWeapon;
    public int MadnessWeaponToken;
    [Header("Inventory Armor")]
    public GameObject SpiderArmor;
    public int SpiderArmorToken;
    public GameObject IlluArmor;
    public int IlluArmorToken;
    public GameObject RoidArmor;
    public int RoidArmorToken;
    public GameObject IKArmor;
    public int IKArmorToken;
    public GameObject BlobArmor;
    public int BlobArmorToken;
    public GameObject FireArmor;
    public int FireArmorToken;
    public GameObject FrostArmor;
    public int FrostArmorToken;
    public GameObject StoneArmor;
    public int StoneArmorToken;
    public GameObject ThunderArmor;
    public int ThunderArmorToken;

    [Header("ShopItems")]
    public GameObject SpiderWeapon_;
    public GameObject BlinkWeapon_;
    public GameObject FireWeapon_;
    public GameObject IKWeapon_;
    public GameObject BlobWeapon_;
    public GameObject TimeWeapon_;
    public GameObject FrostWeapon_;
    public GameObject StrenghtWeapon_;
    public GameObject MadnessWeapon_;

    public GameObject SpiderArmor_;
    public GameObject IlluArmor_;
    public GameObject RoidArmor_;
    public GameObject IKArmor_;
    public GameObject BlobArmor_;
    public GameObject FireArmor_;
    public GameObject FrostArmor_;
    public GameObject StoneArmor_;
    public GameObject ThunderArmor_;

    [Header("Resources")]
    public int meteorToken;
    public int coneToken;
    public int ghostToken;
    public int doubleToken;
    public int splitToken;
    public int channelingToken;
    public int boostToken;
    public int hastenToken;
    public int empowerToken;
    public int bhToken;
    public int pushToken;
    public int poolToken;
    public int ChaosToken;
    public int CompToken;
    public int AimToken;

    [Header("Resource Images")]
    public GameObject ShowTokensPanel;
    private bool showTP;
    public Text meteorText;
    public Text coneText;
    public Text ghostText;
    public Text doubleText;
    public Text splitText;
    public Text compText;
    public Text boostText;
    public Text hastenText;
    public Text empowerText;
    public Text bhText;
    public Text pushText;
    public Text poolText;
    public Text chaosText;
    public Text channelingText;
    public Text aimText;
    // Forgive me father for I have sinned....
    public Text meteorText_;
    public Text coneText_;
    public Text ghostText_;
    public Text doubleText_;
    public Text splitText_;
    public Text compText_;
    public Text boostText_;
    public Text hastenText_;
    public Text empowerText_;
    public Text bhText_;
    public Text pushText_;
    public Text poolText_;
    public Text chaosText_;
    public Text channelingText_;
    public Text aimText_;

    [Header("Item Count")]
    public Text Item1;
    public Text Item2;
    public Text Item3;
    public Text Item4;
    public Text Item5;
    public Text Item6;
    public Text Item7;
    public Text Item8;
    public Text Item9;
    public Text Item10;
    public Text Item11;
    public Text Item12;
    public Text Item13;
    public Text Item14;
    public Text Item15;
    public Text Item16;
    public Text Item17;
    public Text Item18;


    [Header("Activly used tokens for each slot")]
    public bool meteorToken_;
    public bool coneToken_;
    public bool ghostToken_;
    public bool doubleToken_;
    public bool splitToken_;
    public bool channelingToken_;
    public bool boostToken_;
    public bool hastenToken_;
    public bool empowerToken_;
    public bool bhToken_;
    public bool pushToken_;
    public bool poolToken_;
    public bool ChaosToken_;
    public bool CompToken_;
    public bool AimToken_;

    public bool meteorToken_2;
    public bool coneToken_2;
    public bool ghostToken_2;
    public bool doubleToken_2;
    public bool splitToken_2;
    public bool channelingToken_2;
    public bool boostToken_2;
    public bool hastenToken_2;
    public bool empowerToken_2;
    public bool bhToken_2;
    public bool pushToken_2;
    public bool poolToken_2;
    public bool ChaosToken_2;
    public bool CompToken_2;
    public bool AimToken_2;

    public bool meteorToken_3;
    public bool coneToken_3;
    public bool ghostToken_3;
    public bool doubleToken_3;
    public bool splitToken_3;
    public bool channelingToken_3;
    public bool boostToken_3;
    public bool hastenToken_3;
    public bool empowerToken_3;
    public bool bhToken_3;
    public bool pushToken_3;
    public bool poolToken_3;
    public bool ChaosToken_3;
    public bool CompToken_3;
    public bool AimToken_3;

    public GameObject TokenTT1, TokenTT2, TokenTT3, TokenTT4;
    public GameObject TokenTT1T, TokenTT2T, TokenTT3T, TokenTT4T, TokenTT5T;
    public GameObject ControlMenu;
    private bool ShowHide, ShowHide2;
    public GameObject MiniCamera;
    public float changeInX, changeInZ;
    public List<GameObject> Illus;
    private bool show;
    private float SetTime;
    private GameObject CurrentRoom;

    [Header("MiniMapLarge")]
    public GameObject MiniMapLargeCamera;
    public GameObject MiniMapLargePanel;
    private bool ToggleBoolLargeCamera;

    private CastWeapon cw;
    private ToolTipScript tts;
    private Player Player_;
    private MapGrid MG;
    private GameObject MainCamera;
    public GameObject NextLevelAnim;
    public bool GameOverEsc = false;
    private bool ChangeMouse;
    private bool PreventDoubleClick;
    private float ScrollWheelMinTimer = 0.1f;
    private float ScrollWheelMinTimer_;

    void Start()
    {
        Lives = MenuScript.Lives;
        ChangeMouse = false;
        MainCamera = GameObject.Find("MainCamera");
        MG = FindObjectOfType<MapGrid>();
        Player_ = FindObjectOfType<Player>();
        cw = FindObjectOfType<CastWeapon>();
        tts = FindObjectOfType<ToolTipScript>();
      Vector2 asd = new Vector2(0, 0);
        MiniCamera = GameObject.Find("MiniMapCamera");
        Cursor.SetCursor(DefCursor, asd, CursorMode.Auto);
     //   Time.timeScale = 1.1f;
        SetTime = Time.timeScale;
        CurrentGoldText.text = "Gold: " + Money.ToString();
        CurrentGameModeText.text = MenuScript.GameDifficulty;
        PlayTime = 0;
        GoldSpent = 0;
        DamageReceived = 0;
        DeathCount = 0;

        CastSpellLocText.text = IsKeyBindAMouse(MenuScript.CastSpellLoc);
        MoveLocText.text = IsKeyBindAMouse(MenuScript.MoveLoc);
        SpellSlot1Text.text = IsKeyBindAMouse(MenuScript.SpellSlot1Key);
        SpellSlot2Text.text = IsKeyBindAMouse(MenuScript.SpellSlot2Key);
        SpellSlot3Text.text = IsKeyBindAMouse(MenuScript.SpellSlot3Key);
        ItemKeyText.text = IsKeyBindAMouse(MenuScript.ItemKey);
        MoveUpKeyText.text = IsKeyBindAMouse(MenuScript.MoveUpKey);
        MoveDownKeyText.text = IsKeyBindAMouse(MenuScript.MoveDownKey);
        MoveLeftKeyText.text = IsKeyBindAMouse(MenuScript.MoveLeftKey);
        MoveRightKeyText.text = IsKeyBindAMouse(MenuScript.MoveRightKey);

        if (GenerateFromLoad_)
        {
            LoadGame();
        }

        if (MenuScript.FirstLaunch)
        {
            ToggleControls();
            MenuScript.FirstLaunch = false;
        }

        if (!MenuScript.InfiniteLives)
        {
            CurrentLivesText.text = "Lives: " + Lives.ToString();
            DeathScreenLivesText.text = "Continue (" + Lives.ToString() + ")";
        }
        else
        {
            CurrentLivesText.text = "Lives:  ∞";
            DeathScreenLivesText.text = "Continue ∞";
        }


    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("SaveFile", 0); // Saves can only be loaded once.
        GenerateFromLoad_ = false;

        Money = GetInt("Money");

        //Stats
        PlayTime = GetFloat("PlayTime");
        DeathCount = GetFloat("DeathCount");
        DamageReceived = GetFloat("DamageReceived");
        GoldSpent = GetFloat("GoldSpent");

        Player_.health = GetFloat("Player_.health");
        Player_.fullhealth = GetFloat("Player_.fullhealth");
        Player_.Heal(0);

        //Items
        SpiderWeaponToken = GetInt("SpiderWeaponToken");
        BlinkWeaponToken = GetInt("BlinkWeaponToken");
        FireWeaponToken = GetInt("FireWeaponToken");
        IKWeaponToken = GetInt("IKWeaponToken");
        BlobWeaponToken = GetInt("BlobWeaponToken");
        TimeWeaponToken = GetInt("TimeWeaponToken");
        FrostWeaponToken = GetInt("FrostWeaponToken");
        StrenghtWeaponToken = GetInt("StrenghtWeaponToken");
        MadnessWeaponToken = GetInt("MadnessWeaponToken");
        SpiderArmorToken = GetInt("SpiderArmorToken");
        IlluArmorToken = GetInt("IlluArmorToken");
        RoidArmorToken = GetInt("RoidArmorToken");
        IKArmorToken = GetInt("IKArmorToken");
        BlobArmorToken = GetInt("BlobArmorToken");
        FireArmorToken = GetInt("FireArmorToken");
        FrostArmorToken = GetInt("FrostArmorToken");
        StoneArmorToken = GetInt("StoneArmorToken");
        ThunderArmorToken = GetInt("ThunderArmorToken");

        cw.CurrentWeapon = GetInt("cw.CurrentWeapon");
        cw.CurrentArmor = GetInt("cw.CurrentArmor");
        cw.SelectWeapon(cw.CurrentWeapon);
        cw.SelectArmor(cw.CurrentArmor);

        ToolTipScript tts = ToolTipScript.FindObjectOfType<ToolTipScript>();
        tts.CurrentItemID(GetInt("cw.CurrentWeapon"));
        tts.WeaponTip(false);
        tts.CloseWeapon();

        tts.CurrentItemID(GetInt("cw.CurrentArmor"));
        tts.ArmorTip(false);
        tts.CloseArmor();

        tts.CloseAllItemPanels();
        tts.ForceCloseArmorPanels.SetActive(false);
        tts.ForceCloseWeaponPanels.SetActive(false);
        tts.ItemToolTipPanelhardCode();

        //Spell Selection
        meteorToken_ = GetBool("meteorToken_");
        coneToken_ = GetBool("coneToken_");
        ghostToken_ = GetBool("ghostToken_");
        doubleToken_ = GetBool("doubleToken_");
        splitToken_ = GetBool("splitToken_");
        channelingToken_ = GetBool("channelingToken_");
        boostToken_ = GetBool("boostToken_");
        hastenToken_ = GetBool("hastenToken_");
        empowerToken_ = GetBool("empowerToken_");
        bhToken_ = GetBool("bhToken_");
        pushToken_ = GetBool("pushToken_");
        poolToken_ = GetBool("poolToken_");
        ChaosToken_ = GetBool("ChaosToken_");
        CompToken_ = GetBool("CompToken_");
        AimToken_ = GetBool("AimToken_");
        meteorToken_2 = GetBool("meteorToken_2");
        coneToken_2 = GetBool("coneToken_2");
        ghostToken_2 = GetBool("ghostToken_2");
        doubleToken_2 = GetBool("doubleToken_2");
        splitToken_2 = GetBool("splitToken_2");
        channelingToken_2 = GetBool("channelingToken_2");
        boostToken_2 = GetBool("boostToken_2");
        hastenToken_2 = GetBool("hastenToken_2");
        empowerToken_2 = GetBool("empowerToken_2");
        bhToken_2 = GetBool("bhToken_2");
        pushToken_2 = GetBool("pushToken_2");
        poolToken_2 = GetBool("poolToken_2");
        ChaosToken_2 = GetBool("ChaosToken_2");
        CompToken_2 = GetBool("CompToken_2");
        AimToken_2 = GetBool("AimToken_2");
        meteorToken_3 = GetBool("meteorToken_3");
        coneToken_3 = GetBool("coneToken_3");
        ghostToken_3 = GetBool("ghostToken_3");
        doubleToken_3 = GetBool("doubleToken_3");
        splitToken_3 = GetBool("splitToken_3");
        channelingToken_3 = GetBool("channelingToken_3");
        boostToken_3 = GetBool("boostToken_3");
        hastenToken_3 = GetBool("hastenToken_3");
        empowerToken_3 = GetBool("empowerToken_3");
        bhToken_3 = GetBool("bhToken_3");
        pushToken_3 = GetBool("pushToken_3");
        poolToken_3 = GetBool("poolToken_3");
        ChaosToken_3 = GetBool("ChaosToken_3");
        CompToken_3 = GetBool("CompToken_3");
        AimToken_3 = GetBool("AimToken_3");
        //Tokens
        meteorToken = GetInt("meteorToken");
        coneToken = GetInt("coneToken");
        ghostToken = GetInt("ghostToken");
        doubleToken = GetInt("doubleToken");
        splitToken = GetInt("splitToken");
        channelingToken = GetInt("channelingToken");
        boostToken = GetInt("boostToken");
        hastenToken = GetInt("hastenToken");
        empowerToken = GetInt("empowerToken");
        bhToken = GetInt("bhToken");
        pushToken = GetInt("pushToken");
        poolToken = GetInt("poolToken");
        ChaosToken = GetInt("ChaosToken");
        CompToken = GetInt("CompToken");
        AimToken = GetInt("AimToken");

        Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
        GainGold(0);
        spellb.LoadSpellMems();
        PickedUpItem();      
    }

    public void SaveAndExit()
    {
        ToolTipScript tts = ToolTipScript.FindObjectOfType<ToolTipScript>();

        PlayerPrefs.SetInt("SaveFile", 1);


        SaveFileValidation = "";
        //Map Generation
        SetInt("MG.CurrentLevel", MG.CurrentLevel);
        SetInt("MG.minRoom", MG.TotalRoomsForSave);
        SetInt("MG.maxRoom", MG.TotalRoomsForSave);
        SetBool("MG.LevelHasHadEvent", MG.LevelHasHadEvent);
        SetBool("MG.LevelHasHadMiniBoss", MG.LevelHasHadMiniBoss);
        SetInt("CurrentMode_", CurrentMode_);

        //Stats
        SetFloat("PlayTime", PlayTime);
        SetFloat("DeathCount", DeathCount);
        SetFloat("DamageReceived", DamageReceived);
        SetFloat("GoldSpent", GoldSpent);
        SetInt("Money", Money);
        SetInt("Lives", Lives);
        SetFloat("Player_.health", Player_.health);
        SetFloat("Player_.fullhealth", Player_.fullhealth);

        //Items
        SetInt("SpiderWeaponToken", SpiderWeaponToken);
        SetInt("BlinkWeaponToken", BlinkWeaponToken);
        SetInt("FireWeaponToken", FireWeaponToken);
        SetInt("IKWeaponToken", IKWeaponToken);
        SetInt("BlobWeaponToken", BlobWeaponToken);
        SetInt("TimeWeaponToken", TimeWeaponToken);
        SetInt("FrostWeaponToken", FrostWeaponToken);
        SetInt("StrenghtWeaponToken", StrenghtWeaponToken);
        SetInt("MadnessWeaponToken", MadnessWeaponToken);
        SetInt("SpiderArmorToken", SpiderArmorToken);
        SetInt("IlluArmorToken", IlluArmorToken);
        SetInt("RoidArmorToken", RoidArmorToken);
        SetInt("IKArmorToken", IKArmorToken);
        SetInt("BlobArmorToken", BlobArmorToken);
        SetInt("FireArmorToken", FireArmorToken);
        SetInt("FrostArmorToken", FrostArmorToken);
        SetInt("StoneArmorToken", StoneArmorToken);
        SetInt("ThunderArmorToken", ThunderArmorToken);

        SetInt("cw.CurrentWeapon", cw.CurrentWeapon);
        SetInt("cw.CurrentArmor", cw.CurrentArmor);

        //Spell Selection
        SetBool("meteorToken_", meteorToken_);
        SetBool("coneToken_", coneToken_);
        SetBool("ghostToken_", ghostToken_);
        SetBool("doubleToken_", doubleToken_);
        SetBool("splitToken_", splitToken_);
        SetBool("channelingToken_", channelingToken_);
        SetBool("boostToken_", boostToken_);
        SetBool("hastenToken_", hastenToken_);
        SetBool("empowerToken_", empowerToken_);
        SetBool("bhToken_", bhToken_);
        SetBool("pushToken_", pushToken_);
        SetBool("poolToken_", poolToken_);
        SetBool("ChaosToken_", ChaosToken_);
        SetBool("CompToken_", CompToken_);
        SetBool("AimToken_", AimToken_);
        SetBool("meteorToken_2", meteorToken_2);
        SetBool("coneToken_2", coneToken_2);
        SetBool("ghostToken_2", ghostToken_2);
        SetBool("doubleToken_2", doubleToken_2);
        SetBool("splitToken_2", splitToken_2);
        SetBool("channelingToken_2", channelingToken_2);
        SetBool("boostToken_2", boostToken_2);
        SetBool("hastenToken_2", hastenToken_2);
        SetBool("empowerToken_2", empowerToken_2);
        SetBool("bhToken_2", bhToken_2);
        SetBool("pushToken_2", pushToken_2);
        SetBool("poolToken_2", poolToken_2);
        SetBool("ChaosToken_2", ChaosToken_2);
        SetBool("CompToken_2", CompToken_2);
        SetBool("AimToken_2", AimToken_2);
        SetBool("meteorToken_3", meteorToken_3);
        SetBool("coneToken_3", coneToken_3);
        SetBool("ghostToken_3", ghostToken_3);
        SetBool("doubleToken_3", doubleToken_3);
        SetBool("splitToken_3", splitToken_3);
        SetBool("channelingToken_3", channelingToken_3);
        SetBool("boostToken_3", boostToken_3);
        SetBool("hastenToken_3", hastenToken_3);
        SetBool("empowerToken_3", empowerToken_3);
        SetBool("bhToken_3", bhToken_3);
        SetBool("pushToken_3", pushToken_3);
        SetBool("poolToken_3", poolToken_3);
        SetBool("ChaosToken_3", ChaosToken_3);
        SetBool("CompToken_3", CompToken_3);
        SetBool("AimToken_3", AimToken_3);
        //Tokens
        SetInt("meteorToken", meteorToken);
        SetInt("coneToken", coneToken);
        SetInt("ghostToken", ghostToken);
        SetInt("doubleToken", doubleToken);
        SetInt("splitToken", splitToken);
        SetInt("channelingToken", channelingToken);
        SetInt("boostToken", boostToken);
        SetInt("hastenToken", hastenToken);
        SetInt("empowerToken", empowerToken);
        SetInt("bhToken", bhToken);
        SetInt("pushToken", pushToken);
        SetInt("poolToken", poolToken);
        SetInt("ChaosToken", ChaosToken);
        SetInt("CompToken", CompToken);
        SetInt("AimToken", AimToken);
        Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
        spellb.SaveSpellMems();

        PlayerPrefs.SetString("SaveFileValidation", SaveFileValidation); // Encrypt this string if u want to create validaton for save file.
        
       // new RSACryptoServiceProvider

        //Exits
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }




    public void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
        SaveFileValidation += booleanValue ? 1 : 0;
    }
    public void SetInt(string name, int IntAmount)
    {
        PlayerPrefs.SetInt(name, IntAmount);
        SaveFileValidation += IntAmount;
    }
    public void SetFloat(string name, float FloatAmount)
    {
        PlayerPrefs.SetFloat(name, FloatAmount);
        SaveFileValidation += FloatAmount.ToString("F0");
    }
    bool GetBool(string name)  //For retriving value.
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    public int GetInt(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
    public float GetFloat(string name)
    {
        return PlayerPrefs.GetFloat(name);
    }







    string IsKeyBindAMouse(string Key)
    {

        if (!Key.Contains("Mouse"))
        {
            return Key.Substring(Key.Length - 1);
        }
        else
        {
            return Key;
        }
    }

    void ActivateCheats()
    {
        Player_.health = 13.37f;
        Player_.fullhealth = 13.37f;
        Player_.HealthText.text = Player_.health.ToString("F2");
        SpellsAndItems();
        TestMenu.SetActive(true);
    }


    public void SteamBossAchievement(string ID)
    {
        string genString = " ";
        ID = ID.Replace(genString, "_");
        string ID_ = (ID + "_defeated").ToLower(); // Modify here to include difficulty if u want for each mode

        SteamAch.UnlockSteamAchievement(ID_);
    }
    public void SteamModeAchievement()
    {
        string ID = "game_completed_" + MenuScript.GameDifficulty;
        string ID_ = ID.ToLower(); 
        SteamAch.UnlockSteamAchievement(ID_);

        if (MenuScript.GameDifficulty == "Challenge")
        {
            string ID2 = ("game_completed_Normal").ToLower();
            SteamAch.UnlockSteamAchievement(ID2);
            string ID3 = ("game_completed_Casual").ToLower();
            SteamAch.UnlockSteamAchievement(ID3);
        }
        if (MenuScript.GameDifficulty == "Normal")
        {
            string ID4 = ("game_completed_Casual").ToLower();
            SteamAch.UnlockSteamAchievement(ID4);
        }
    }
    public void SteamOtherAchievemnet()
    {
        if (PlayTime < 3600)
        {
            SteamAch.UnlockSteamAchievement("under_one_hour");
        }
        if (DeathCount == 0)
        {
            SteamAch.UnlockSteamAchievement("0_deaths");
        }
        if (GoldSpent == 0)
        {
            SteamAch.UnlockSteamAchievement("0_gold");
        }
        if (DamageReceived == 0)
        {
            SteamAch.UnlockSteamAchievement("0_damage");
        }
    }


    public void SelectCursor(bool OverObject)
    {
        if (OverObject && !ChangeMouse)
        {
            Cursor.SetCursor(HoverOverObjectCursor, new Vector2(0, 0), CursorMode.Auto);
            ChangeMouse = true;
        }
        else if (!OverObject && ChangeMouse)
        {
            Cursor.SetCursor(DefCursor, new Vector2(0, 0), CursorMode.Auto);
            ChangeMouse = false;
        }
    }

    public void EnableSpellSlotEffect()
    {
        foreach (var item in SelectSpellsEffect)
        {
            item.SetActive(true);
        }
    }
    public void DisableSpellSlotEffect()
    {
        foreach (var item in SelectSpellsEffect)
        {
            item.SetActive(false);
        }
    }

    public void EnableItem(bool ItemType, bool ActiveOrInactive)
    {
        if (ItemType)
        {
            SelectWeaponEffect.SetActive(ActiveOrInactive);
        }
        else
        {
            SelectArmorEffect.SetActive(ActiveOrInactive);
        }

    }

    public void SetCurrentRoom(GameObject room) // not used currently
    {
        CurrentRoom = room;
    }
    public GameObject GetCurrentRoom() // not used currently
    {
        return CurrentRoom;
    }
    public void CurrentLevel(int lvl)
    {


        if (lvl < 5)
        {
            CurrentLevelText.text = "Level " + (lvl + 1).ToString();
            NextLevelAnim.GetComponent<LoadScreen>().NewLevelText("Level " + (lvl + 1).ToString(), lvl);
        }
        else
        {
            CurrentLevelText.text = "Final level";
            NextLevelAnim.GetComponent<LoadScreen>().NewLevelText("Final level", lvl);
        }
    }

    public void GainGold(int amount)
    {
        Money += amount;
        CurrentGoldText.text = "Gold: "+ Money.ToString();
    }

    public void ShopWindowFunc(bool status)
    {
        ShopWindow.SetActive(status);
        if (status == false)
        {
            CloseShopWindow.Play();
        }
    }

    public void BuyRandomToken()
    {
        if (Money >= RandomSpellBuyCost)
        {
            Money -= RandomSpellBuyCost;
            GoldSpent += RandomSpellBuyCost;
            CurrentGoldText.text = "Gold: " + Money.ToString();
            var randomToken = Random.Range(0, MapGrid.FindObjectOfType<MapGrid>().Tokens.Count);

            float random1 = Random.Range(0f,0f);
            float random2 = Random.Range(-5f, -3f);

            var CurLoc = CurrentShopLocation + transform.forward * random1 + transform.right * random2;
            GameObject CurLoot = Instantiate(MapGrid.FindObjectOfType<MapGrid>().Tokens[randomToken], new Vector3(CurLoc.x, 1, CurLoc.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = ShopRoom;
          
        }
        else
        {
            NotEnoughGoldSound.Play();
        }
    }

    public void BuyHealingPotion()
    {
        if (Money >= PotionBuyCost)
        {
            Money -= PotionBuyCost;
            GoldSpent += PotionBuyCost;
            CurrentGoldText.text = "Gold: " + Money.ToString();
            float random1 = Random.Range(0f, 0f);
            float random2 = Random.Range(-5f, -3f);

            var CurLoc = CurrentShopLocation + transform.forward * random1 + transform.right * random2;
            GameObject CurLoot = Instantiate(MapGrid.FindObjectOfType<MapGrid>().Healing[0], new Vector3(CurLoc.x, 1, CurLoc.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = ShopRoom;
        }
        else
        {
            NotEnoughGoldSound.Play();
        }
    }

    public void BuyRandomItem()
    {
        if (Money >= RandomItemBuyCost)
        {
            Money -= RandomItemBuyCost;
            GoldSpent += RandomItemBuyCost;
            CurrentGoldText.text = "Gold: " + Money.ToString();
            var randomToken = Random.Range(0, Items.Count);

            float random1 = Random.Range(0f, 0f);
            float random2 = Random.Range(-5f, -3f);


            var CurLoc = CurrentShopLocation + transform.forward * random1 + transform.right * random2;
            GameObject CurLoot = Instantiate(Items[randomToken], new Vector3(CurLoc.x, 1, CurLoc.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = ShopRoom;
        }
        else
        {
            NotEnoughGoldSound.Play();
        }
    }

    public void ButtonRooms(int Type)
    {
        switch (Type)
        {
            case 0:
                minRoom_ = 5;
                maxRoom_ = 7;
                CurrentLevel_ = 0;
                GiveLoot_ = false;

                break;
            case 1:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 6;
                GiveLoot_ = true;
                break;
            case 2:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 7;
                GiveLoot_ = true;
                break;
            case 3:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 8;
                GiveLoot_ = true;
                break;
            case 4:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 9;
                GiveLoot_ = true;
                break;
            case 5:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 10;
                GiveLoot_ = true;
                break;
            case 6:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 11;
                GiveLoot_ = true;
                break;
            case 7:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 12;
                GiveLoot_ = true;
                break;
            case 8:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 13;
                GiveLoot_ = true;
                break;
            case 9:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 14;
                GiveLoot_ = true;
                break;
            case 10:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 15;
                GiveLoot_ = true;
                break;
            case 11:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 16;
                GiveLoot_ = true;
                break;
            case 20:
                minRoom_ = 20;
                maxRoom_ = 20;
                CurrentLevel_ = 5;
                GiveLoot_ = true;
                break;

        }
        StartLevel_ = true;
        PlayTime = 0;
        GoldSpent = 0;
        DamageReceived = 0;
        DeathCount = 0;
        Time.timeScale = SetTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ControlTime(float time)
    {
        SetTime = time;
    }

    public void ShowAvailableTokens()
    {
        showTP = !showTP;
        ShowTokensPanel.SetActive(showTP);
    }

    void MouseScrollSpells(bool DirectionForward)
    {
        Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
        int NextSpell = 0;
        ScrollWheelMinTimer_ = ScrollWheelMinTimer;

        switch (CastSpell.FindObjectOfType<CastSpell>().currentSlot)
        {
            case 1:
                if (DirectionForward)
                {
                    NextSpell = 2;
                }
                else
                {
                    NextSpell = 3;
                }

                break;
            case 2:
                if (DirectionForward)
                {
                    NextSpell = 3;
                }
                else
                {
                    NextSpell = 1;
                }
                break;
            case 3:
                if (DirectionForward)
                {
                    NextSpell = 1;
                }
                else
                {
                    NextSpell = 2;
                }
                break;
        }
        
        switch (NextSpell)
        {
            case 1:
                spellb.SlotOne();
                break;
            case 2:
                spellb.SlotTwo();
                break;
            case 3:
                spellb.SlotThree();
                break;

        }
    }

    void Update()
    {
        PlayTime += Time.deltaTime;

        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.ItemKey)))
        {
            cw.WeaponAttack();
        }



        if (Input.GetAxis("Mouse ScrollWheel") > 0f && ScrollWheelMinTimer_ <=0)
        {
            MouseScrollSpells(true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && ScrollWheelMinTimer_ <=0)
        {
            MouseScrollSpells(false);
        }

        ScrollWheelMinTimer_ -= Time.deltaTime;

        if (CheatsEnabled)
            {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ActivateCheats();
            }
            if (Input.GetKey(KeyCode.M))
            {
                Time.timeScale += Time.deltaTime*0.5f;
            }
            if (Input.GetKey(KeyCode.N))
            {
                Time.timeScale -= Time.deltaTime*0.5f;
            }

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ToggleBoolLargeCamera)
            {
                ToggleLargeMiniMap();
            }

            if (!show)
            {
                ToggleControls();

                ForceCloseShopWhenOtherButtonsAreClicked();

                if (SpellPanelOpen.activeSelf == true)
                {
                    Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
                    spellb.ForceCloseSpellBookPanel();
                }

                if (WeaponpanelOpen.activeSelf == true || ArmorPanelOpen.activeSelf == true)
                {
                    ToolTipScript tts = ToolTipScript.FindObjectOfType<ToolTipScript>();
                    tts.CloseAllItemPanels();
                    tts.ForceCloseArmorPanels.SetActive(false);
                    tts.ForceCloseWeaponPanels.SetActive(false);
                    tts.ItemToolTipPanelhardCode();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ShowHide2)
            {
                ToggleControls();
            }
            if (!show)
            {
                ToggleLargeMiniMap();
                ForceCloseShopWhenOtherButtonsAreClicked();
                if (SpellPanelOpen.activeSelf == true)
                {
                    Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
                    spellb.ForceCloseSpellBookPanel();
                }

                if (WeaponpanelOpen.activeSelf == true || ArmorPanelOpen.activeSelf == true)
                {
                    ToolTipScript tts = ToolTipScript.FindObjectOfType<ToolTipScript>();
                    tts.CloseAllItemPanels();
                    tts.ForceCloseArmorPanels.SetActive(false);
                    tts.ForceCloseWeaponPanels.SetActive(false);
                    tts.ItemToolTipPanelhardCode();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameOverEsc)
            {
                if (!ShowHide2 && !ToggleBoolLargeCamera)
                {
                    ShowEscMenu();
                }
            }
            else
            {
                EscButton();
            }

            if (ShowHide2)
            {
                ToggleControls();
            }
            if (ToggleBoolLargeCamera)
            {
                ToggleLargeMiniMap();
            }

            if (SpellPanelOpen.activeSelf == true)
            {
                Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
                spellb.ForceCloseSpellBookPanel();
            }
            ForceCloseShopWhenOtherButtonsAreClicked();
            if (WeaponpanelOpen.activeSelf == true || ArmorPanelOpen.activeSelf == true)
            {
                ToolTipScript tts = ToolTipScript.FindObjectOfType<ToolTipScript>();
                tts.CloseAllItemPanels();
                tts.ForceCloseArmorPanels.SetActive(false);
                tts.ForceCloseWeaponPanels.SetActive(false);
                tts.ItemToolTipPanelhardCode();
            }
        }

        if (SpellPanelOpen.activeSelf == true || WeaponpanelOpen.activeSelf == true || ArmorPanelOpen.activeSelf == true || SellTokenWindow.activeSelf == true ||SellItemWindow.activeSelf == true)
        {
            if (ShowHide2)
            {
                ToggleControls();
            }
            if (ToggleBoolLargeCamera)
            {
                ToggleLargeMiniMap();
            }
        }

    }

    public void ToggleControls()
    {
        ShowHide2 = ShowHide2 ? false : true;
        ControlMenu.SetActive(ShowHide2);
    }
    public void PickedUpItem()
    {
        CurrentTokens();
        if (SpiderWeaponToken >= 1)
        {
            SpiderWeapon.SetActive(true);
            if (cw.CurrentWeapon != 1 || SpiderWeaponToken > 1)
            {
                SpiderWeapon_.SetActive(true);
            }
            else
            {
                SpiderWeapon_.SetActive(false);
            }
        }
        else
        {
            SpiderWeapon.SetActive(false);
            SpiderWeapon_.SetActive(false);
        }
        if (BlinkWeaponToken >= 1)
        {
            BlinkWeapon.SetActive(true);
            if (cw.CurrentWeapon != 2 || BlinkWeaponToken > 1)
            {
                BlinkWeapon_.SetActive(true);
            }
            else
            {
                BlinkWeapon_.SetActive(false);
            }
        }
        else
        {
            BlinkWeapon.SetActive(false);
            BlinkWeapon_.SetActive(false);
        }
        if (FireWeaponToken >= 1)
        {
            FireWeapon.SetActive(true);
            if (cw.CurrentWeapon != 3|| FireWeaponToken > 1)
            {
                FireWeapon_.SetActive(true);
            }
            else
            {
                FireWeapon_.SetActive(false);
            }
        }
        else
        {
            FireWeapon.SetActive(false);
            FireWeapon_.SetActive(false);
        }
        if (IKWeaponToken >= 1)
        {
            IKWeapon.SetActive(true);
            if (cw.CurrentWeapon != 4|| IKWeaponToken >1)
            {
                IKWeapon_.SetActive(true);
            }
            else
            {
                IKWeapon_.SetActive(false);
            }
        }
        else
        {
            IKWeapon_.SetActive(false);
            IKWeapon.SetActive(false);
        }
        if (BlobWeaponToken >= 1)
        {
            BlobWeapon.SetActive(true);
            if (cw.CurrentWeapon != 5 ||BlobWeaponToken > 1)
            {
                BlobWeapon_.SetActive(true);
            }
            else
            {
                BlobWeapon_.SetActive(false);
            }
        }
        else
        {
            BlobWeapon_.SetActive(false);
            BlobWeapon.SetActive(false);
        }
        if (TimeWeaponToken >= 1)
        {
            TimeWeapon.SetActive(true);
            if (cw.CurrentWeapon != 6 || TimeWeaponToken > 1)
            {
                TimeWeapon_.SetActive(true);
            }
            else
            {
                TimeWeapon_.SetActive(false);
            }
        }
        else
        {
            TimeWeapon_.SetActive(false);
            TimeWeapon.SetActive(false);
        }

        if (FrostWeaponToken >= 1)
        {
            FrostWeapon.SetActive(true);
            if (cw.CurrentWeapon != 7 || FrostWeaponToken > 1)
            {
               FrostWeapon_.SetActive(true);
            }
            else
            {
                FrostWeapon_.SetActive(false);
            }
        }
        else
        {
            FrostWeapon_.SetActive(false);
            FrostWeapon.SetActive(false);
        }

        if (StrenghtWeaponToken >= 1)
        {
            StrenghtWeapon.SetActive(true);
            if (cw.CurrentWeapon != 8 || StrenghtWeaponToken > 1)
            {
               StrenghtWeapon_.SetActive(true);
            }
            else
            {
                StrenghtWeapon_.SetActive(false);
            }
        }
        else
        {
            StrenghtWeapon_.SetActive(false);
            StrenghtWeapon.SetActive(false);
        }

        if (MadnessWeaponToken >= 1)
        {
            MadnessWeapon.SetActive(true);
            if (cw.CurrentWeapon != 9 || MadnessWeaponToken > 1)
            {
                MadnessWeapon_.SetActive(true);
            }
            else
            {
                MadnessWeapon_.SetActive(false);
            }
        }
        else
        {
            MadnessWeapon_.SetActive(false);
            MadnessWeapon.SetActive(false);
        }
        //Armors
        if (SpiderArmorToken >= 1)
        {
            SpiderArmor.SetActive(true);
            if (cw.CurrentArmor != 1||SpiderArmorToken>1)
            {
                SpiderArmor_.SetActive(true);
            }
            else
            {
                SpiderArmor_.SetActive(false);
            }
        }
        else
        {
            SpiderArmor_.SetActive(false);
            SpiderArmor.SetActive(false);
        }
        if (IlluArmorToken >= 1)
        {
            IlluArmor.SetActive(true);
            if (cw.CurrentArmor != 2|| IlluArmorToken>1)
            {
                IlluArmor_.SetActive(true);
            }
            else
            {
                IlluArmor_.SetActive(false);
            }
        }
        else
        {
            IlluArmor_.SetActive(false);
            IlluArmor.SetActive(false);
        }
        if (RoidArmorToken >= 1)
        {
            RoidArmor.SetActive(true);
            if (cw.CurrentArmor != 3|| RoidArmorToken>1)
            {
                RoidArmor_.SetActive(true);
            }
            else
            {
                RoidArmor_.SetActive(false);
            }
        }
        else
        {
            RoidArmor_.SetActive(false);
            RoidArmor.SetActive(false);
        }
        if (IKArmorToken >= 1)
        {
            IKArmor.SetActive(true);
            if (cw.CurrentArmor != 4|| IKArmorToken>1)
            {
                IKArmor_.SetActive(true);
            }
            else
            {
                IKArmor_.SetActive(false);
            }
        }
        else
        {
            IKArmor_.SetActive(false);
            IKArmor.SetActive(false);
        }
        if (BlobArmorToken >= 1)
        {
            BlobArmor.SetActive(true);
            if (cw.CurrentArmor != 5|| BlobArmorToken>1)
            {
                BlobArmor_.SetActive(true);
            }
            else
            {
                BlobArmor_.SetActive(false);
            }
        }
        else
        {
            BlobArmor_.SetActive(false);
            BlobArmor.SetActive(false);
        }

        if (FireArmorToken >= 1)
        {
            FireArmor.SetActive(true);
            if (cw.CurrentArmor != 6 || FireArmorToken > 1)
            {
                FireArmor_.SetActive(true);
            }
            else
            {
                FireArmor_.SetActive(false);
            }
        }
        else
        {
            FireArmor_.SetActive(false);
            FireArmor.SetActive(false);
        }

        if (FrostArmorToken >= 1)
        {
            FrostArmor.SetActive(true);
            if (cw.CurrentArmor != 7 || FrostArmorToken > 1)
            {
                FrostArmor_.SetActive(true);
            }
            else
            {
                FrostArmor_.SetActive(false);
            }
        }
        else
        {
            FrostArmor_.SetActive(false);
            FrostArmor.SetActive(false);
        }

        if (StoneArmorToken >= 1)
        {
            StoneArmor.SetActive(true);
            if (cw.CurrentArmor != 8 || StoneArmorToken > 1)
            {
                StoneArmor_.SetActive(true);
            }
            else
            {
               StoneArmor_.SetActive(false);
            }
        }
        else
        {
            StoneArmor_.SetActive(false);
            StoneArmor.SetActive(false);
        }

        if (ThunderArmorToken >= 1)
        {
            ThunderArmor.SetActive(true);
            if (cw.CurrentArmor != 9 || ThunderArmorToken > 1)
            {
                ThunderArmor_.SetActive(true);
            }
            else
            {
                ThunderArmor_.SetActive(false);
            }
        }
        else
        {
            ThunderArmor_.SetActive(false);
            ThunderArmor.SetActive(false);
        }

        Item1.text = SpiderArmorToken.ToString();
        Item2.text = IlluArmorToken.ToString();
        Item3.text = RoidArmorToken.ToString();
        Item4.text = IKArmorToken.ToString();
        Item5.text = BlobArmorToken.ToString();
        Item6.text = SpiderWeaponToken.ToString();
        Item7.text = BlinkWeaponToken.ToString();
        Item8.text = FireWeaponToken.ToString();
        Item9.text = IKWeaponToken.ToString();
        Item10.text = BlobWeaponToken.ToString();
        Item11.text = FireArmorToken.ToString();
        Item12.text = FrostArmorToken.ToString();
        Item13.text = StoneArmorToken.ToString();
        Item14.text = ThunderArmorToken.ToString();
        Item15.text = TimeWeaponToken.ToString();
        Item16.text = FrostWeaponToken.ToString();
        Item17.text = StrenghtWeaponToken.ToString();
        Item18.text = MadnessWeaponToken.ToString();

    }

    public void ShowEscMenu()
    {
        EscMenu.SetActive(show = show ? false : true);
        if (show)
        {
            Time.timeScale = 0;
            string hours = Mathf.Floor(PlayTime / 3600).ToString("00");
            string minutes = Mathf.Floor((PlayTime/60) % 60).ToString("00");
            string seconds = (PlayTime % 60).ToString("00");
            PlayTimeText.text = (string.Format("{0}:{1}:{2}",hours, minutes, seconds));

            DeathsText.text = DeathCount.ToString();
            DamageReceivedText.text = DamageReceived.ToString("F1");
            GoldSpentText.text = GoldSpent.ToString();

            if (EnableSave)
            {

                var colors = EnableSaveButton.colors;
                colors.normalColor = Color.white;
                EnableSaveButton.colors = colors;

                AlphaFade = EnableSaveButtonText.color;
                AlphaFade.a = 1;
                EnableSaveButtonText.color = AlphaFade;
                EnableSaveButton.interactable = true;
            }
            else
            {
                var colors = EnableSaveButton.colors;
                colors.normalColor = Color.gray;
                EnableSaveButton.colors = colors;

                AlphaFade = EnableSaveButtonText.color;
                AlphaFade.a = 0.5f;
                EnableSaveButtonText.color = AlphaFade;
                EnableSaveButton.interactable = false;
            }

        }
        else
        {
            Time.timeScale = SetTime;
        }

    }

    public void ToggleLargeMiniMap()
    {
        ToggleBoolLargeCamera = !ToggleBoolLargeCamera;
        MiniMapLargeCamera.SetActive(ToggleBoolLargeCamera);
        MiniMapLargePanel.SetActive(ToggleBoolLargeCamera);
    }


    public void EscButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }


public void RestartButton()
    {
        Time.timeScale = SetTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetDeathText()
    {
        if (Lives == 0)
        {
            DeathScreenLivesText.text = "Game Over";
        }
    }

    public void ContinueAfterDeath()
    {

        //if (Lives == 0)
        //{
        //    DeathScreenLivesText.text = "Game Over";
        //}

        if (Lives > 0)
        {
            if (!PreventDoubleClick)
            {
                PreventDoubleClick = true;
                Invoke("WowWhatALeetCoder", 5);
                MG.CurrentLevel--;
                MG.maxRoom--;
                MG.NextLevel();
                Player_.Continue();
                MainCamera.GetComponent<CamController>().zLevel = -26;
                MainCamera.GetComponent<CamController>().zLevel2 = -26;
                MainCamera.GetComponent<CamController>().xLimit1 = 0;
                MainCamera.GetComponent<CamController>().xLimit2 = 0;
                MainCamera.GetComponent<CamController>().zTest = -22;
                MiniCamera.transform.position = new Vector3(-1000, 100, 0);
                if (!MenuScript.InfiniteLives)
                {
                    Lives--;
                    CurrentLivesText.text = "Lives: " + Lives.ToString();
                    DeathScreenLivesText.text = "Continue (" + Lives.ToString() + ")";
                }
                else
                {
                    CurrentLivesText.text = "Lives:  ∞";
                    DeathScreenLivesText.text = "Continue ∞";
                }
                NormalTheme.Play();
                BossBattle.Stop();

                BossHealth.transform.GetChild(0).gameObject.SetActive(false);
                BossHealth.transform.GetChild(1).gameObject.SetActive(false);
                BossHealth.transform.GetChild(2).gameObject.SetActive(false);
                BossHealth.transform.GetChild(3).gameObject.SetActive(false);
               
            }
        }
        else
        {
            NotEnoughGoldSound.Play();
        }

    }

    void WowWhatALeetCoder()
    {
        PreventDoubleClick = false;
    }

    public void CurrentTokens()
    {
        meteorText.text = meteorToken.ToString();
        coneText.text = coneToken.ToString();
        ghostText.text = ghostToken.ToString();
        doubleText.text = doubleToken.ToString();
        splitText.text = splitToken.ToString();
        channelingText.text = channelingToken.ToString();
        boostText.text = boostToken.ToString();
        hastenText.text = hastenToken.ToString();
        empowerText.text = empowerToken.ToString();
        bhText.text = bhToken.ToString();
        pushText.text = pushToken.ToString();
        poolText.text = poolToken.ToString();
        chaosText.text = ChaosToken.ToString();
        compText.text = CompToken.ToString();
        aimText.text = AimToken.ToString();

        meteorText_.text = meteorToken.ToString();
        coneText_.text = coneToken.ToString();
        ghostText_.text = ghostToken.ToString();
        doubleText_.text = doubleToken.ToString();
        splitText_.text = splitToken.ToString();
        channelingText_.text = channelingToken.ToString();
        boostText_.text = boostToken.ToString();
        hastenText_.text = hastenToken.ToString();
        empowerText_.text = empowerToken.ToString();
        bhText_.text = bhToken.ToString();
        pushText_.text = pushToken.ToString();
        poolText_.text = poolToken.ToString();
        chaosText_.text = ChaosToken.ToString();
        compText_.text = CompToken.ToString();
        aimText_.text = AimToken.ToString();

    }

    public void OpenSellTokenMenu()
    {
        if (!show)
        {
            ShowSellWindowBool = !ShowSellWindowBool;
            SellTokenWindow.SetActive(ShowSellWindowBool);
            //CloseShopWindow.Play();
            ShowSellItemWindowBool = false;
            SellItemWindow.SetActive(ShowSellItemWindowBool);
        }
    }

    public void OpenSellItemMenu()
    {
        if (!show)
        {
            ShowSellItemWindowBool = !ShowSellItemWindowBool;
            SellItemWindow.SetActive(ShowSellItemWindowBool);
            // CloseShopWindow.Play();
            PickedUpItem();
            ShowSellWindowBool = false;
            SellTokenWindow.SetActive(ShowSellWindowBool);
        }
    }

    public void ForceCloseShopWhenOtherButtonsAreClicked()
    {
        SellTokenWindow.SetActive(false);
        SellItemWindow.SetActive(false);
        ShowSellItemWindowBool = false;
        ShowSellWindowBool = false;
    }

    public void SellSpellToken(int spell)
    {
        switch (spell)
        {
            case 1:
                if (meteorToken > 0)
                {
                    meteorToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 2:
                if (coneToken > 0)
                {
                    coneToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 3:
                if (ghostToken > 0)
                {
                    ghostToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;

            case 21:
                if (doubleToken > 0)
                {
                    doubleToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 22:
                if (splitToken > 0)
                {
                    splitToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 23:
                if (CompToken > 0)
                {
                    CompToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;

            case 31:
                if (boostToken > 0)
                {
                    boostToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 32:
                if (hastenToken > 0)
                {
                    hastenToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 33:
                if (empowerToken > 0)
                {
                    empowerToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 41:
                if (bhToken > 0)
                {
                    bhToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 42:
                if (pushToken > 0)
                {
                    pushToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 43:
                if (poolToken > 0)
                {
                    poolToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;

            case 51:
                if (ChaosToken > 0)
                {
                    ChaosToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 52:
                if (channelingToken > 0)
                {
                    channelingToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 53:
                if (AimToken > 0)
                {
                    AimToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
        }

        if (SellTokenSuccess)
        {
            SellTokenSound.Play();
        }
        else
        {
            NotEnoughGoldSound.Play();
        }

        PickedUpItem();
    }

    public void SellItem(int Item)
    {
        switch (Item)
        {
            case 1:
                SpiderArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (SpiderArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 2:
                IlluArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (IlluArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 3:
                RoidArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (RoidArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 4:
                IKArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (IKArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 5:
                BlobArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (BlobArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 6:
                SpiderWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (SpiderWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 7:
                BlinkWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (BlinkWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 8:
                FireWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (FireWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 9:
                IKWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (IKWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 10:
                BlobWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (BlobWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 11:
                FireArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (FireArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 12:
                FrostArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (FrostArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 13:
                StoneArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (StoneArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 14:
                ThunderArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (ThunderArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 15:
                TimeWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (TimeWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 16:
                FrostWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (FrostWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 17:
                StrenghtWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (StrenghtWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 18:
                MadnessWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (MadnessWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
        }
        SellTokenSound.Play();
    }


    public void CheckIfTokenBought(int slot, int tokenlevel)
    {
        SlotIsInUse1 = false;
        SlotIsInUse2 = false;
        SlotIsInUse3 = false;
        switch (slot)
        {
            case 1:
                switch (tokenlevel)
                {
                    case 1:
                        if (meteorToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (coneToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (ghostToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 2:
                        if (doubleToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (splitToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (CompToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 3:
                        if (boostToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (hastenToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (empowerToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 4:
                        if (bhToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (pushToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (poolToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 5:
                        if (ChaosToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (channelingToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (AimToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                }
                break;
            case 2:
                switch (tokenlevel)
                {
                    case 1:
                        if (meteorToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (coneToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (ghostToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 2:
                        if (doubleToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (splitToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (CompToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 3:
                        if (boostToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (hastenToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (empowerToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 4:
                        if (bhToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (pushToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (poolToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 5:
                        if (ChaosToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (channelingToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (AimToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                }
                break;
            case 3:
                switch (tokenlevel)
                {
                    case 1:
                        if (meteorToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (coneToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (ghostToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 2:
                        if (doubleToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (splitToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (CompToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 3:
                        if (boostToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (hastenToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (empowerToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 4:
                        if (bhToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (pushToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (poolToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 5:
                        if (ChaosToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (channelingToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (AimToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                }
                break;
        }
    }


    public void BuyToken(int SpellType)
    {
        Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
        switch (spellb.curSlot)
        {
            case 1:

                switch (SpellType)
                {
                    case 1:

                        if (!meteorToken_)
                        {
                            meteorToken--;
                            meteorToken_ = true;

                            if (coneToken_)
                            {
                                coneToken++;
                                coneToken_ = false;

                            }
                            if (ghostToken_)
                            {
                                ghostToken++;
                                ghostToken_ = false;
                            }
                        }
                        else
                        {
                            meteorToken++;
                            meteorToken_ = false;
                            spellb.MeteorBlocked.enabled = false;
                        }


                        break;
                    case 2:

                        if (!coneToken_)
                        {
                            coneToken--;
                            coneToken_ = true;
                            if (meteorToken_)
                            {
                                meteorToken++;
                                meteorToken_ = false;
                            }
                            if (ghostToken_)
                            {
                                ghostToken++;
                                ghostToken_ = false;
                            }
                        }
                        else
                        {
                            coneToken++;
                            coneToken_ = false;
                            spellb.ConeBlocked.enabled = false;
                        }
                        break;

                    case 3:
                        if (!ghostToken_)
                        {
                            ghostToken--;
                            ghostToken_ = true;
                            if (meteorToken_)
                            {
                                meteorToken++;
                                meteorToken_ = false;
                            }
                            if (coneToken_)
                            {
                                coneToken++;
                                coneToken_ = false;
                            }
                        }
                        else
                        {
                            ghostToken++;
                            ghostToken_ = false;
                            spellb.GhostCastBlocked.enabled = false;
                        }
                        break;
                    case 4:
                        if (!doubleToken_)
                        {
                            doubleToken--;
                            doubleToken_ = true;
                            if (splitToken_)
                            {
                                splitToken++;
                                splitToken_ = false;
                            }
                            if (CompToken_)
                            {
                                CompToken++;
                                CompToken_ = false;
                            }
                        }
                        else
                        {
                            doubleToken++;
                            doubleToken_ = false;
                            spellb.DoubleCastBlocked.enabled = false;
                        }
                        break;
                    case 5:
                        if (!splitToken_)
                        {
                            splitToken--;
                            splitToken_ = true;
                            if (doubleToken_)
                            {
                                doubleToken++;
                                doubleToken_ = false;
                            }
                            if (CompToken_)
                            {
                                CompToken++;
                                CompToken_ = false;
                            }
                        }
                        else
                        {
                            splitToken++;
                            splitToken_ = false;
                            spellb.SplitCastBlocked.enabled = false;
                        }
                        break;
                    case 6:
                        if (!CompToken_)
                        {
                            CompToken--;
                            CompToken_ = true;
                            if (splitToken_)
                            {
                                splitToken++;
                                splitToken_ = false;
                            }
                            if (doubleToken_)
                            {
                                doubleToken++;
                                doubleToken_ = false;
                            }
                        }
                        else
                        {
                            CompToken++;
                            CompToken_ = false;
                            spellb.CompOrbBlocked.enabled = false;
                        }
                        break;
                    case 7:
                        if (!boostToken_)
                        {
                            boostToken--;
                            boostToken_ = true;
                            if (hastenToken_)
                            {
                                hastenToken++;
                                hastenToken_ = false;
                            }
                            if (empowerToken_)
                            {
                                empowerToken++;
                                empowerToken_ = false;
                            }
                        }
                        else
                        {
                            boostToken++;
                            boostToken_ = false;
                            spellb.BoostBlocked.enabled = false;
                        }
                        break;
                    case 8:
                        if (!hastenToken_)
                        {
                            hastenToken--;
                            hastenToken_ = true;
                            if (boostToken_)
                            {
                                boostToken++;
                                boostToken_ = false;
                            }
                            if (empowerToken_)
                            {
                                empowerToken++;
                                empowerToken_ = false;
                            }
                        }
                        else
                        {
                            hastenToken++;
                            hastenToken_ = false;
                            spellb.HastenBlocked.enabled = false;
                        }
                        break;
                    case 9:
                        if (!empowerToken_)
                        {
                            empowerToken--;
                            empowerToken_ = true;
                            if (hastenToken_)
                            {
                                hastenToken++;
                                hastenToken_ = false;
                            }
                            if (boostToken_)
                            {
                                boostToken++;
                                boostToken_ = false;
                            }
                        }
                        else
                        {
                            empowerToken++;
                            empowerToken_ = false;
                            spellb.EmpowerBlocked.enabled = false;
                        }
                        break;
                    case 10: //START
                        if (!bhToken_)
                        {
                            bhToken--;
                            bhToken_ = true;
                            if (pushToken_)
                            {
                                pushToken++;
                                pushToken_ = false;
                            }
                            if (poolToken_)
                            {
                                poolToken++;
                                poolToken_ = false;
                            }
                        }
                        else
                        {
                            bhToken++;
                            bhToken_ = false;
                            spellb.BHBlocked.enabled = false;
                        }
                        break;
                    case 11:
                        if (!pushToken_)
                        {
                            pushToken--;
                            pushToken_ = true;
                            if (bhToken_)
                            {
                                bhToken++;
                                bhToken_ = false;
                            }
                            if (poolToken_)
                            {
                                poolToken++;
                                poolToken_ = false;
                            }
                        }
                        else
                        {
                            pushToken++;
                            pushToken_ = false;
                            spellb.PushBlocked.enabled = false;
                        }
                        break;
                    case 12:
                        if (!poolToken_)
                        {
                            poolToken--;
                            poolToken_ = true;
                            if (pushToken_)
                            {
                                pushToken++;
                                pushToken_ = false;
                            }
                            if (bhToken_)
                            {
                                bhToken++;
                                bhToken_ = false;
                            }
                        }
                        else
                        {
                            poolToken++;
                            poolToken_ = false;
                            spellb.PoolBlocked.enabled = false;
                        }
                        break;
                    // START
                    case 13:
                        if (!ChaosToken_)
                        {
                            ChaosToken--;
                            ChaosToken_ = true;
                            if (channelingToken_)
                            {
                                channelingToken++;
                                channelingToken_ = false;
                            }
                            if (AimToken_)
                            {
                                AimToken++;
                                AimToken_ = false;
                            }
                        }
                        else
                        {
                            ChaosToken++;
                            ChaosToken_ = false;
                            spellb.ChaosOrbBlocked.enabled = false;
                        }
                        break;
                    case 14:
                        if (!channelingToken_)
                        {
                            channelingToken--;
                            channelingToken_ = true;
                            if (ChaosToken_)
                            {
                                ChaosToken++;
                                ChaosToken_ = false;
                            }
                            if (AimToken_)
                            {
                                AimToken++;
                                AimToken_ = false;
                            }
                        }
                        else
                        {
                            channelingToken++;
                            channelingToken_ = false;
                            spellb.ChannelingBlocked.enabled = false;
                        }
                        break;
                    case 15:
                        if (!AimToken_)
                        {
                            AimToken--;
                            AimToken_ = true;
                            if (channelingToken_)
                            {
                                channelingToken++;
                                channelingToken_ = false;
                            }
                            if (ChaosToken_)
                            {
                                ChaosToken++;
                                ChaosToken_ = false;
                            }
                        }
                        else
                        {
                            AimToken++;
                            AimToken_ = false;
                            spellb.BlessedAimBlocked.enabled = false;
                        }
                        break;
                }
                break;

            case 2:

                switch (SpellType)
                {
                    case 1:

                        if (!meteorToken_2)
                        {
                            meteorToken--;
                            meteorToken_2 = true;

                            if (coneToken_2)
                            {
                                coneToken++;
                                coneToken_2 = false;

                            }
                            if (ghostToken_2)
                            {
                                ghostToken++;
                                ghostToken_2 = false;
                            }
                        }
                        else
                        {
                            meteorToken++;
                            meteorToken_2 = false;
                            spellb.MeteorBlocked.enabled = false;
                        }


                        break;
                    case 2:

                        if (!coneToken_2)
                        {
                            coneToken--;
                            coneToken_2 = true;
                            if (meteorToken_2)
                            {
                                meteorToken++;
                                meteorToken_2 = false;
                            }
                            if (ghostToken_2)
                            {
                                ghostToken++;
                                ghostToken_2 = false;
                            }
                        }
                        else
                        {
                            coneToken++;
                            coneToken_2 = false;
                            spellb.ConeBlocked.enabled = false;
                        }
                        break;

                    case 3:
                        if (!ghostToken_2)
                        {
                            ghostToken--;
                            ghostToken_2 = true;
                            if (meteorToken_2)
                            {
                                meteorToken++;
                                meteorToken_2 = false;
                            }
                            if (coneToken_2)
                            {
                                coneToken++;
                                coneToken_2 = false;
                            }
                        }
                        else
                        {
                            ghostToken++;
                            ghostToken_2 = false;
                            spellb.GhostCastBlocked.enabled = false;
                        }
                        break;
                    case 4:
                        if (!doubleToken_2)
                        {
                            doubleToken--;
                            doubleToken_2 = true;
                            if (splitToken_2)
                            {
                                splitToken++;
                                splitToken_2 = false;
                            }
                            if (CompToken_2)
                            {
                                CompToken++;
                                CompToken_2 = false;
                            }
                        }
                        else
                        {
                            doubleToken++;
                            doubleToken_2 = false;
                            spellb.DoubleCastBlocked.enabled = false;
                        }
                        break;
                    case 5:
                        if (!splitToken_2)
                        {
                            splitToken--;
                            splitToken_2 = true;
                            if (doubleToken_2)
                            {
                                doubleToken++;
                                doubleToken_2 = false;
                            }
                            if (CompToken_2)
                            {
                                CompToken++;
                                CompToken_2 = false;
                            }
                        }
                        else
                        {
                            splitToken++;
                            splitToken_2 = false;
                            spellb.SplitCastBlocked.enabled = false;
                        }
                        break;
                    case 6:
                        if (!CompToken_2)
                        {
                            CompToken--;
                            CompToken_2 = true;
                            if (splitToken_2)
                            {
                                splitToken++;
                                splitToken_2 = false;
                            }
                            if (doubleToken_2)
                            {
                                doubleToken++;
                                doubleToken_2 = false;
                            }
                        }
                        else
                        {
                            CompToken++;
                            CompToken_2 = false;
                            spellb.CompOrbBlocked.enabled = false;
                        }
                        break;
                    case 7: //START
                        if (!boostToken_2)
                        {
                            boostToken--;
                            boostToken_2 = true;
                            if (hastenToken_2)
                            {
                                hastenToken++;
                                hastenToken_2 = false;
                            }
                            if (empowerToken_2)
                            {
                                empowerToken++;
                                empowerToken_2 = false;
                            }
                        }
                        else
                        {
                            boostToken++;
                            boostToken_2 = false;
                            spellb.BoostBlocked.enabled = false;
                        }
                        break;
                    case 8:
                        if (!hastenToken_2)
                        {
                            hastenToken--;
                            hastenToken_2 = true;
                            if (boostToken_2)
                            {
                                boostToken++;
                                boostToken_2 = false;
                            }
                            if (empowerToken_2)
                            {
                                empowerToken++;
                                empowerToken_2 = false;
                            }
                        }
                        else
                        {
                            hastenToken++;
                            hastenToken_2 = false;
                            spellb.HastenBlocked.enabled = false;
                        }
                        break;
                    case 9:
                        if (!empowerToken_2)
                        {
                            empowerToken--;
                            empowerToken_2 = true;
                            if (hastenToken_2)
                            {
                                hastenToken++;
                                hastenToken_2 = false;
                            }
                            if (boostToken_2)
                            {
                                boostToken++;
                                boostToken_2 = false;
                            }
                        }
                        else
                        {
                            empowerToken++;
                            empowerToken_2 = false;
                            spellb.EmpowerBlocked.enabled = false;
                        }
                        break;
                    case 10: //START
                        if (!bhToken_2)
                        {
                            bhToken--;
                            bhToken_2 = true;
                            if (pushToken_2)
                            {
                                pushToken++;
                                pushToken_2 = false;
                            }
                            if (poolToken_2)
                            {
                                poolToken++;
                                poolToken_2 = false;
                            }
                        }
                        else
                        {
                            bhToken++;
                            bhToken_2 = false;
                            spellb.BHBlocked.enabled = false;
                        }
                        break;
                    case 11:
                        if (!pushToken_2)
                        {
                            pushToken--;
                            pushToken_2 = true;
                            if (bhToken_2)
                            {
                                bhToken++;
                                bhToken_2 = false;
                            }
                            if (poolToken_2)
                            {
                                poolToken++;
                                poolToken_2 = false;
                            }
                        }
                        else
                        {
                            pushToken++;
                            pushToken_2 = false;
                            spellb.PushBlocked.enabled = false;
                        }
                        break;
                    case 12:
                        if (!poolToken_2)
                        {
                            poolToken--;
                            poolToken_2 = true;
                            if (pushToken_2)
                            {
                                pushToken++;
                                pushToken_2 = false;
                            }
                            if (bhToken_2)
                            {
                                bhToken++;
                                bhToken_2 = false;
                            }
                        }
                        else
                        {
                            poolToken++;
                            poolToken_2 = false;
                            spellb.PoolBlocked.enabled = false;
                        }
                        break;
                    // START
                    case 13:
                        if (!ChaosToken_2)
                        {
                            ChaosToken--;
                            ChaosToken_2 = true;
                            if (channelingToken_2)
                            {
                                channelingToken++;
                                channelingToken_2 = false;
                            }
                            if (AimToken_2)
                            {
                                AimToken++;
                                AimToken_2 = false;
                            }
                        }
                        else
                        {
                            ChaosToken++;
                            ChaosToken_2 = false;
                            spellb.ChaosOrbBlocked.enabled = false;
                        }
                        break;
                    case 14:
                        if (!channelingToken_2)
                        {
                            channelingToken--;
                            channelingToken_2 = true;
                            if (ChaosToken_2)
                            {
                                ChaosToken++;
                                ChaosToken_2 = false;
                            }
                            if (AimToken_2)
                            {
                                AimToken++;
                                AimToken_2 = false;
                            }
                        }
                        else
                        {
                            channelingToken++;
                            channelingToken_2 = false;
                            spellb.ChannelingBlocked.enabled = false;
                        }
                        break;
                    case 15:
                        if (!AimToken_2)
                        {
                            AimToken--;
                            AimToken_2 = true;
                            if (channelingToken_2)
                            {
                                channelingToken++;
                                channelingToken_2 = false;
                            }
                            if (ChaosToken_2)
                            {
                                ChaosToken++;
                                ChaosToken_2 = false;
                            }
                        }
                        else
                        {
                            AimToken++;
                            AimToken_2 = false;
                            spellb.BlessedAimBlocked.enabled = false;
                        }
                        break;
                }
                break;
            case 3:

                switch (SpellType)
                {
                    case 1:

                        if (!meteorToken_3)
                        {
                            meteorToken--;
                            meteorToken_3 = true;

                            if (coneToken_3)
                            {
                                coneToken++;
                                coneToken_3 = false;

                            }
                            if (ghostToken_3)
                            {
                                ghostToken++;
                                ghostToken_3 = false;
                            }
                        }
                        else
                        {
                            meteorToken++;
                            meteorToken_3 = false;
                            spellb.MeteorBlocked.enabled = false;
                        }


                        break;
                    case 2:

                        if (!coneToken_3)
                        {
                            coneToken--;
                            coneToken_3 = true;
                            if (meteorToken_3)
                            {
                                meteorToken++;
                                meteorToken_3 = false;
                            }
                            if (ghostToken_3)
                            {
                                ghostToken++;
                                ghostToken_3 = false;
                            }
                        }
                        else
                        {
                            coneToken++;
                            coneToken_3 = false;
                            spellb.ConeBlocked.enabled = false;
                        }
                        break;

                    case 3:
                        if (!ghostToken_3)
                        {
                            ghostToken--;
                            ghostToken_3 = true;
                            if (meteorToken_3)
                            {
                                meteorToken++;
                                meteorToken_3 = false;
                            }
                            if (coneToken_3)
                            {
                                coneToken++;
                                coneToken_3 = false;
                            }
                        }
                        else
                        {
                            ghostToken++;
                            ghostToken_3 = false;
                            spellb.GhostCastBlocked.enabled = false;
                        }
                        break;
                    case 4:
                        if (!doubleToken_3)
                        {
                            doubleToken--;
                            doubleToken_3 = true;
                            if (splitToken_3)
                            {
                                splitToken++;
                                splitToken_3 = false;
                            }
                            if (CompToken_3)
                            {
                                CompToken++;
                                CompToken_3 = false;
                            }
                        }
                        else
                        {
                            doubleToken++;
                            doubleToken_3 = false;
                            spellb.DoubleCastBlocked.enabled = false;
                        }
                        break;
                    case 5:
                        if (!splitToken_3)
                        {
                            splitToken--;
                            splitToken_3 = true;
                            if (doubleToken_3)
                            {
                                doubleToken++;
                                doubleToken_3 = false;
                            }
                            if (CompToken_3)
                            {
                                CompToken++;
                                CompToken_3 = false;
                            }
                        }
                        else
                        {
                            splitToken++;
                            splitToken_3 = false;
                            spellb.SplitCastBlocked.enabled = false;
                        }
                        break;
                    case 6:
                        if (!CompToken_3)
                        {
                            CompToken--;
                            CompToken_3 = true;
                            if (splitToken_3)
                            {
                                splitToken++;
                                splitToken_3 = false;
                            }
                            if (doubleToken_3)
                            {
                                doubleToken++;
                                doubleToken_3 = false;
                            }
                        }
                        else
                        {
                            CompToken++;
                            CompToken_3 = false;
                            spellb.CompOrbBlocked.enabled = false;
                        }
                        break;
                    case 7: //START
                        if (!boostToken_3)
                        {
                            boostToken--;
                            boostToken_3 = true;
                            if (hastenToken_3)
                            {
                                hastenToken++;
                                hastenToken_3 = false;
                            }
                            if (empowerToken_3)
                            {
                                empowerToken++;
                                empowerToken_3 = false;
                            }
                        }
                        else
                        {
                            boostToken++;
                            boostToken_3 = false;
                            spellb.BoostBlocked.enabled = false;
                        }
                        break;
                    case 8:
                        if (!hastenToken_3)
                        {
                            hastenToken--;
                            hastenToken_3 = true;
                            if (boostToken_3)
                            {
                                boostToken++;
                                boostToken_3 = false;
                            }
                            if (empowerToken_3)
                            {
                                empowerToken++;
                                empowerToken_3 = false;
                            }
                        }
                        else
                        {
                            hastenToken++;
                            hastenToken_3 = false;
                            spellb.HastenBlocked.enabled = false;
                        }
                        break;
                    case 9:
                        if (!empowerToken_3)
                        {
                            empowerToken--;
                            empowerToken_3 = true;
                            if (hastenToken_3)
                            {
                                hastenToken++;
                                hastenToken_3 = false;
                            }
                            if (boostToken_3)
                            {
                                boostToken++;
                                boostToken_3 = false;
                            }
                        }
                        else
                        {
                            empowerToken++;
                            empowerToken_3 = false;
                            spellb.EmpowerBlocked.enabled = false;
                        }
                        break;
                    case 10: //START
                        if (!bhToken_3)
                        {
                            bhToken--;
                            bhToken_3 = true;
                            if (pushToken_3)
                            {
                                pushToken++;
                                pushToken_3 = false;
                            }
                            if (poolToken_3)
                            {
                                poolToken++;
                                poolToken_3 = false;
                            }
                        }
                        else
                        {
                            bhToken++;
                            bhToken_3 = false;
                            spellb.BHBlocked.enabled = false;
                        }
                        break;
                    case 11:
                        if (!pushToken_3)
                        {
                            pushToken--;
                            pushToken_3 = true;
                            if (bhToken_3)
                            {
                                bhToken++;
                                bhToken_3 = false;
                            }
                            if (poolToken_3)
                            {
                                poolToken++;
                                poolToken_3 = false;
                            }
                        }
                        else
                        {
                            pushToken++;
                            pushToken_3 = false;
                            spellb.PushBlocked.enabled = false;
                        }
                        break;
                    case 12:
                        if (!poolToken_3)
                        {
                            poolToken--;
                            poolToken_3 = true;
                            if (pushToken_3)
                            {
                                pushToken++;
                                pushToken_3 = false;
                            }
                            if (bhToken_3)
                            {
                                bhToken++;
                                bhToken_3 = false;
                            }
                        }
                        else
                        {
                            poolToken++;
                            poolToken_3 = false;
                            spellb.PoolBlocked.enabled = false;
                        }
                        break;
                    // START
                    case 13:
                        if (!ChaosToken_3)
                        {
                            ChaosToken--;
                            ChaosToken_3 = true;
                            if (channelingToken_3)
                            {
                                channelingToken++;
                                channelingToken_3 = false;
                            }
                            if (AimToken_3)
                            {
                                AimToken++;
                                AimToken_3 = false;
                            }
                        }
                        else
                        {
                            ChaosToken++;
                            ChaosToken_3 = false;
                            spellb.ChaosOrbBlocked.enabled = false;
                        }
                        break;
                    case 14:
                        if (!channelingToken_3)
                        {
                            channelingToken--;
                            channelingToken_3 = true;
                            if (ChaosToken_3)
                            {
                                ChaosToken++;
                                ChaosToken_3 = false;
                            }
                            if (AimToken_3)
                            {
                                AimToken++;
                                AimToken_3 = false;
                            }
                        }
                        else
                        {
                            channelingToken++;
                            channelingToken_3 = false;
                            spellb.ChannelingBlocked.enabled = false;
                        }
                        break;
                    case 15:
                        if (!AimToken_3)
                        {
                            AimToken--;
                            AimToken_3 = true;
                            if (channelingToken_3)
                            {
                                channelingToken++;
                                channelingToken_3 = false;
                            }
                            if (ChaosToken_3)
                            {
                                ChaosToken++;
                                ChaosToken_3 = false;
                            }
                        }
                        else
                        {
                            AimToken++;
                            AimToken_3 = false;
                            spellb.BlessedAimBlocked.enabled = false;
                        }
                        break;
                }
                break;
        }
    }
    public void SpellsAndItems()
    {
        SpiderWeaponToken = 1;
        BlinkWeaponToken = 1;
        FireWeaponToken = 1;
        IKWeaponToken = 1;
        SpiderArmorToken = 1;
        IlluArmorToken = 1;
        RoidArmorToken = 1;
        IKArmorToken = 1;
        BlobArmorToken = 1;
        BlobWeaponToken = 1;
        FireArmorToken = 1;
        FrostArmorToken = 1;
        StoneArmorToken = 1;
        ThunderArmorToken = 1;
        TimeWeaponToken = 1;
        FrostWeaponToken = 1;
        StrenghtWeaponToken = 1;
        MadnessWeaponToken = 1;

        meteorToken = 3;
        coneToken = 3;
        ghostToken = 3;
        doubleToken = 3;
        splitToken = 3;
        channelingToken = 3;
        boostToken = 3;
        hastenToken = 3;
        empowerToken = 3;
        bhToken = 3;
        pushToken = 3;
        poolToken = 3;
        ChaosToken = 3;
        CompToken = 3;
        AimToken = 3;
    }


}
