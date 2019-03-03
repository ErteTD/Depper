using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class MenuScript : MonoBehaviour {
    AsyncOperation async;
    [Header ("Mode settings")]
    public static int Lives;
    public static bool InfiniteLives;
    public static float MonsterDensity = 1;
    public static float GoldDropChance = 0;
    public static float PlayerModeArmor = 1;
    public static float BossHealthModifier = 1;
    public static string GameDifficulty = "Normal";
    public static bool FirstLaunch;
    public static int MouseMovement;
    public static int MouseFire;

    public static string SpellSlot1Key = "Alpha1";
    public static string SpellSlot2Key = "Alpha2";
    public static string SpellSlot3Key = "Alpha3";
    public static string ItemKey = "R";
    public static string MoveUpKey = "W";
    public static string MoveDownKey = "S";
    public static string MoveLeftKey = "A";
    public static string MoveRightKey = "D";

    public static string MoveLoc = "Mouse0";
    public static string CastSpellLoc = "Mouse1";

    public Button ContinueFromLoadbutton;
    public Text ContinueFromLoadbuttonText;
    public GameObject KeyBinder;
    private Color AlphaFade;
    [Header ("KeyBindings")]
    //public InputField SpellSlot1Key_;
    //public InputField SpellSlot2Key_;
    //public InputField SpellSlot3Key_;
    //public InputField ItemKey_;
    //public InputField MoveUpKey_;
    //public InputField MoveDownKey_;
    //public InputField MoveLeftKey_;
    //public InputField MoveRightKey_;
    public Text InputText1;
    public Text InputText2;
    public Text InputText3;
    public Text InputText4;
    public Text InputText5;
    public Text InputText6;
    public Text InputText7;
    public Text InputText8;
    public Text InputText9;
    public Text InputText10;

    public Button InputButton1;
    public Button InputButton2;
    public Button InputButton3;
    public Button InputButton4;
    public Button InputButton5;
    public Button InputButton6;
    public Button InputButton7;
    public Button InputButton8;
    public Button InputButton9;
    public Button InputButton10;

    private Text ActiveInputTextField;
    private Button ActiveInputButtonField;

    internal static int InverseMouseButtons;


    public Text ModeText;

    public List<Monster> Mlist;
    private bool BarBool;
    public GameObject LoadingBar;
    public Image loadingBar;
    public Text textPourcentage;
    [Header ("Settings")]
    public Toggle VSyncToggle;
    public Toggle FSToggle;
    public Toggle InverseMouseToggle;
    public Dropdown ScreenResolution;
    public Dropdown AASettings;
    //internal static int VsyncIni;
    internal static bool ResChecked;
    internal static int ResCheckInt;

    internal static Resolution[] resolutions;
    List<int> FakeCount = new List<int>();
    private bool DontUpdateRes;
    internal static int AASetting;

    private float LimitInputTimer;
    private bool BindAButton;

    public void Start()
    {
        DontUpdateRes = true;
        resolutions = Screen.resolutions;
        ScreenResolution.ClearOptions();
       
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        bool checkDuplicate;
        FakeCount.Clear();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;           
            checkDuplicate = false;
            foreach (var item in options)
            {
                if (item == option)
                {
                    checkDuplicate = true;
                }
            }
            if (checkDuplicate == false)
            {
                options.Add(option);
                FakeCount.Add(i);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }
        ScreenResolution.AddOptions(options);
        if (ResChecked)
        {
            ScreenResolution.value = ResCheckInt;
        }
        else
        {
            ScreenResolution.value = currentResolutionIndex;
        }
        ScreenResolution.RefreshShownValue();
        SetModeText(0);

        AASetting = PlayerPrefs.GetInt("PP_AASettings", 1);
        AASettings.value = AASetting;

        ChooseAA(AASetting);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("PP_VsyncSettings", 0);

        SetKeyBinding();
    }

    void SetKeyBinding()
    {
       // InverseMouseButtons = PlayerPrefs.GetInt("InvMouse", 0);
        MoveLoc = PlayerPrefs.GetString("MoveLoc", "Mouse0");
        CastSpellLoc = PlayerPrefs.GetString("CastSpellLoc", "Mouse1");
        SpellSlot1Key = PlayerPrefs.GetString("SpellSlot1Key", "Alpha1");
        SpellSlot2Key = PlayerPrefs.GetString("SpellSlot2Key", "Alpha2");
        SpellSlot3Key = PlayerPrefs.GetString("SpellSlot3Key", "Alpha3");
        ItemKey = PlayerPrefs.GetString("ItemKey", "R");
        MoveUpKey = PlayerPrefs.GetString("MoveUpKey", "W");
        MoveDownKey = PlayerPrefs.GetString("MoveDownKey", "S");
        MoveLeftKey = PlayerPrefs.GetString("MoveLeftKey", "A");
        MoveRightKey = PlayerPrefs.GetString("MoveRightKey", "D");

        string MoveLoc_2 = MoveLoc;
        string CastSpellLoc_2 = CastSpellLoc;
        string SpellSlot1Key_2 = SpellSlot1Key;
        string SpellSlot2Key_2 = SpellSlot2Key;
        string SpellSlot3Key_2 = SpellSlot3Key;
        string ItemKey_2 = ItemKey;
        string MoveUpKey_2 = MoveUpKey;
        string MoveDownKey_2 = MoveDownKey;
        string MoveLeftKey_2 = MoveLeftKey;
        string MoveRightKey_2 = MoveRightKey;

        MoveLoc_2 = CheckIfStringIsNumber(MoveLoc_2, false);
        CastSpellLoc_2 = CheckIfStringIsNumber(CastSpellLoc_2, false);
        SpellSlot1Key_2 = CheckIfStringIsNumber(SpellSlot1Key_2, false);
        SpellSlot2Key_2 = CheckIfStringIsNumber(SpellSlot2Key_2, false);
        SpellSlot3Key_2 = CheckIfStringIsNumber(SpellSlot3Key_2, false);
        ItemKey_2 = CheckIfStringIsNumber(ItemKey_2, false);
        MoveUpKey_2 = CheckIfStringIsNumber(MoveUpKey_2, false);
        MoveDownKey_2 = CheckIfStringIsNumber(MoveDownKey_2, false);
        MoveLeftKey_2 = CheckIfStringIsNumber(MoveLeftKey_2, false);
        MoveRightKey_2 = CheckIfStringIsNumber(MoveRightKey_2, false);

        InputText9.text = MoveLoc_2;
        InputText10.text = CastSpellLoc_2;
        InputText1.text = SpellSlot1Key_2;
        InputText2.text = SpellSlot2Key_2;
        InputText3.text = SpellSlot3Key_2;
        InputText4.text = ItemKey_2;
        InputText5.text = MoveUpKey_2;
        InputText6.text = MoveDownKey_2;
        InputText7.text = MoveLeftKey_2;
        InputText8.text = MoveRightKey_2;

    }


    string CheckIfStringIsNumber(string Key, bool Save)
    {
        char c = Key[0];
        if (Save && System.Char.IsDigit(c))
        {
            Key = "Alpha" + Key;
            return Key;
        }
        else if (Save && Key.Contains("Mouse"))
        {
            return Key;
        }
        else if (Save)
        {
            return Key.ToUpper();
        }

        if (!Save && Key.Contains("Alpha"))
        {
            return Key = Key.Substring(Key.Length - 1);
        }
        else if (!Save && Key.Contains("Mouse"))
        {
            return Key;
        }
        else
        {
            return Key.ToUpper();
        }      
    }

    public void ChangeKeyBind()
    {
        if (KeyBinder.activeSelf == true)
        {

            string MoveLoc_2 = InputText9.text;
            string CastSpellLoc_2 = InputText10.text;
            string SpellSlot1Key_2 = InputText1.text;
            string SpellSlot2Key_2 = InputText2.text;
            string SpellSlot3Key_2 = InputText3.text;
            string ItemKey_2 = InputText4.text;
            string MoveUpKey_2 = InputText5.text;
            string MoveDownKey_2 = InputText6.text;
            string MoveLeftKey_2 = InputText7.text;
            string MoveRightKey_2 = InputText8.text;

            MoveLoc_2 = CheckIfStringIsNumber(MoveLoc_2, true);
            CastSpellLoc_2 = CheckIfStringIsNumber(CastSpellLoc_2, true);
            SpellSlot1Key_2 = CheckIfStringIsNumber(SpellSlot1Key_2, true);
            SpellSlot2Key_2 = CheckIfStringIsNumber(SpellSlot2Key_2, true);
            SpellSlot3Key_2 = CheckIfStringIsNumber(SpellSlot3Key_2, true);
            ItemKey_2 = CheckIfStringIsNumber(ItemKey_2, true);
            MoveUpKey_2 = CheckIfStringIsNumber(MoveUpKey_2, true);
            MoveDownKey_2 = CheckIfStringIsNumber(MoveDownKey_2, true);
            MoveLeftKey_2 = CheckIfStringIsNumber(MoveLeftKey_2, true);
            MoveRightKey_2 = CheckIfStringIsNumber(MoveRightKey_2, true);


            PlayerPrefs.SetString("MoveLoc", MoveLoc_2);
            PlayerPrefs.SetString("CastSpellLoc", CastSpellLoc_2);
            PlayerPrefs.SetString("SpellSlot1Key", SpellSlot1Key_2);
            PlayerPrefs.SetString("SpellSlot2Key", SpellSlot2Key_2);
            PlayerPrefs.SetString("SpellSlot3Key", SpellSlot3Key_2);
            PlayerPrefs.SetString("ItemKey", ItemKey_2);
            PlayerPrefs.SetString("MoveUpKey", MoveUpKey_2);
            PlayerPrefs.SetString("MoveDownKey", MoveDownKey_2);
            PlayerPrefs.SetString("MoveLeftKey", MoveLeftKey_2);
            PlayerPrefs.SetString("MoveRightKey", MoveRightKey_2);

            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        if (BarBool)
        {
            StartCoroutine(LevelCoroutine());
        }

        LimitInputTimer -= Time.deltaTime;

        if (Input.anyKeyDown && BindAButton && LimitInputTimer < 0)
        {
            LimitInputTimer = 0.3f;

            int count = 0;
            bool tooManyKeys = false;
            foreach (KeyCode kc in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kc))
                    count++;
                if (count > 1)
                {
                    tooManyKeys = true;
                    Debug.Log("Too many keys pressed");
                    break;
                }
            }


            if (!tooManyKeys)
            {

                for (int ButtonNumber = 0; ButtonNumber < 6; ButtonNumber++)
                {
                    if (Input.GetMouseButtonDown(ButtonNumber))
                    {
                        ActiveInputTextField.text = ("Mouse" + ButtonNumber);
                        EndKeyInput();
                        ChangeKeyBind();
                        return;
                    }
                }

                if (Input.inputString.Length > 0)
                {

                    char c = Input.inputString[0];
                    string c_ = c.ToString();
                    if (Regex.IsMatch(c_, @"[a-zA-Z]") || System.Char.IsDigit(c))
                    {
                        ActiveInputTextField.text = Input.inputString;
                        EndKeyInput();
                        ChangeKeyBind();
                        return;
                    }
                }

                if (Input.GetKey(KeyCode.Escape))
                {
                    EndKeyInput();
                    ChangeKeyBind();
                }
            }
        }
    }

    public void ResetKeyBinds()
    {
        //InverseMouseButtons = PlayerPrefs.GetInt("InvMouse", 0);
        // PlayerPrefs.SetInt("InvMouse", 0);

        PlayerPrefs.SetString("MoveLoc", "Mouse0");
        PlayerPrefs.SetString("CastSpellLoc", "Mouse1");
        PlayerPrefs.SetString("SpellSlot1Key", "Alpha1");
        PlayerPrefs.SetString("SpellSlot2Key", "Alpha2");
        PlayerPrefs.SetString("SpellSlot3Key", "Alpha3");
        PlayerPrefs.SetString("ItemKey", "R");
        PlayerPrefs.SetString("MoveUpKey", "W");
        PlayerPrefs.SetString("MoveDownKey", "S");
        PlayerPrefs.SetString("MoveLeftKey", "A");
        PlayerPrefs.SetString("MoveRightKey", "D");
        PlayerPrefs.Save();
        InverseMouseToggle.isOn = false;
        SetKeyBinding();

    }

    public void InverseMouseButtonMethod()
    {
        bool testB = InverseMouseToggle.isOn;
        int asd = 0;
        if (testB)
        {
            asd = 1;
        }
        PlayerPrefs.SetInt("InvMouse", asd);
        PlayerPrefs.Save();
        SetKeyBinding();
    }



    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Getsettings()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            VSyncToggle.isOn = false;
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            VSyncToggle.isOn = true;
        }
        if (Screen.fullScreen == true)
        {
            FSToggle.isOn = true;
        }
        else
        {
            FSToggle.isOn = false;
        }
        DontUpdateRes = false;
        AASettings.value = AASetting;
        ChooseAA(AASetting);
    }

    public void GetKeySettings()
    {
        if (InverseMouseButtons == 1)
        {
            InverseMouseToggle.isOn = true;
        }
        SetKeyBinding();
    }

    public void SetModeText(int mode)
    {
        switch (mode)
        {
            case 1:
                ModeText.text = "- 5 extra lives \n- Lower monster density \n- Monsters deal less damage \n- Bosses have less health";
                break;
            case 2:
                ModeText.text = "- 3 extra lives \n- Normal monster density \n- Monsters deal Normal damage \n- Bosses have Normal health";
                break;
            case 3:
                ModeText.text = "- 0 extra lives \n- Higher monster density \n- Monsters deal more damage \n- Bosses have more health";
                break;
            case 4:

                if (PlayerPrefs.GetInt("SaveFile") == 1)
                {
                    int mode123 = PlayerPrefs.GetInt("CurrentMode_");
                    string CurMode = "";
                    switch (mode123)
                    {
                        case 1:
                            CurMode = "Easy";
                            break;
                        case 2:
                            CurMode = "Normal";
                            break;
                        case 3:
                            CurMode = "Challenge";
                            break;
                    }

                    ModeText.text = string.Format("- Save file found\n-Mode: {0}\n-Level: {1}\n-Lives: {2}", CurMode, PlayerPrefs.GetInt("MG.CurrentLevel")+1, PlayerPrefs.GetInt("Lives"));
                }
                else
                {
                    ModeText.text = "No save file found";
                }
                break;
            case 0:
                ModeText.text = "";
                break;
        }
    }

    public void TurnOnVSync()
    {
        bool testB = VSyncToggle.isOn;
        QualitySettings.vSyncCount = testB ? 1 : 0;
        PlayerPrefs.SetInt("PP_VsyncSettings", QualitySettings.vSyncCount);
        PlayerPrefs.Save();
    }


    public void ChooseResolution(int resolutionIndex)
    {
        if (!DontUpdateRes)
        {
            Resolution resolution = resolutions[FakeCount[resolutionIndex]];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            ResChecked = true;
            ResCheckInt = resolutionIndex;
        }
    }

    public void ChooseAA(int aaIndex)
    {
        AASetting = aaIndex;
        int CurAA = 0;
        switch (aaIndex)
        {
            case 0:
                CurAA = 0;
                break;
                case 1:
                CurAA = 2;
                break;
            case 2:
                CurAA = 4;
                break;
            case 3:
                CurAA = 8;
                break;
        }
        QualitySettings.antiAliasing = CurAA;
        PlayerPrefs.SetInt("PP_AASettings", aaIndex);
        PlayerPrefs.Save();
    }

    void LoadingBarFunc()
    {
        BarBool = true;
        LoadingBar.SetActive(true);
        async = SceneManager.LoadSceneAsync("Level One 1", LoadSceneMode.Single);
        async.allowSceneActivation = false;
    }



    void EndKeyInput()
    {
        BindAButton = false;
        var colors_ = ActiveInputButtonField.colors;
        colors_.normalColor = Color.white;
        ActiveInputButtonField.colors = colors_;
        colors_.highlightedColor = Color.gray;
        ActiveInputButtonField.colors = colors_;
    }

    public void BindButtonTest(int Button)
    {
        BindAButton = true;
        switch (Button)
        {
            case 1:
                ActiveInputTextField = InputText1;
                ActiveInputButtonField = InputButton1;
                break;
            case 2:
                ActiveInputTextField = InputText2;
                ActiveInputButtonField = InputButton2;
                break;
            case 3:
                ActiveInputTextField = InputText3;
                ActiveInputButtonField = InputButton3;
                break;
            case 4:
                ActiveInputTextField = InputText4;
                ActiveInputButtonField = InputButton4;
                break;
            case 5:
                ActiveInputTextField = InputText5;
                ActiveInputButtonField = InputButton5;
                break;
            case 6:
                ActiveInputTextField = InputText6;
                ActiveInputButtonField = InputButton6;
                break;
            case 7:
                ActiveInputTextField = InputText7;
                ActiveInputButtonField = InputButton7;
                break;
            case 8:
                ActiveInputTextField = InputText8;
                ActiveInputButtonField = InputButton8;
                break;
            case 9:
                ActiveInputTextField = InputText9;
                ActiveInputButtonField = InputButton9;
                break;
            case 10:
                ActiveInputTextField = InputText10;
                ActiveInputButtonField = InputButton10;
                break;
        }

        var colors = ActiveInputButtonField.colors;
        colors.normalColor = Color.red;
        ActiveInputButtonField.colors = colors;
        colors.highlightedColor = Color.red;
        ActiveInputButtonField.colors = colors;
    }


    IEnumerator LevelCoroutine()
    {
        float pourcentage = 0;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                loadingBar.fillAmount = async.progress / 0.9f;
                pourcentage = async.progress * 100;
                textPourcentage.text = (int)pourcentage + "%";
            }
            else
            {
                loadingBar.fillAmount = async.progress / 0.9f;
                pourcentage = (async.progress / 0.9f) * 100;
                textPourcentage.text = (int)pourcentage + "%";
                async.allowSceneActivation = true;
            }
            yield return null;
        }
       yield return async;
    }


    public void StartAttacking(bool SoS)
    {
        foreach (var item in Mlist)
        {
            if (SoS)
            {
                item.StartFake();
            }
            else
            {
                item.StopFake();
            }
        }
    }

    bool GetBool(string name)  //For retriving value.
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }



    public void ShowContinueFromLoadButton()
    {
        if (PlayerPrefs.GetInt("SaveFile") == 1)
        {

            var colors = ContinueFromLoadbutton.colors;
            colors.normalColor = Color.white;
            ContinueFromLoadbutton.colors = colors;

            AlphaFade = ContinueFromLoadbuttonText.color;
            AlphaFade.a = 1;
            ContinueFromLoadbuttonText.color = AlphaFade;
            ContinueFromLoadbutton.interactable = true;
        }
        else
        {
            var colors = ContinueFromLoadbutton.colors;
            colors.normalColor = Color.gray;
            ContinueFromLoadbutton.colors = colors;

            AlphaFade = ContinueFromLoadbuttonText.color;
            AlphaFade.a = 0.5f;
            ContinueFromLoadbuttonText.color = AlphaFade;
            ContinueFromLoadbutton.interactable = false;
        }
    }

    public void ContinueFromLoad()
    {
       int mode = PlayerPrefs.GetInt("CurrentMode_");
        switch (mode)
        {
            case 1:
                InfiniteLives = false;
                MonsterDensity = 0.70f;
                GoldDropChance = 15;
                PlayerModeArmor = 0.7f;
                BossHealthModifier = 0.7f;
                GameDifficulty = "Easy";
                GameManager.CurrentMode_ = 1;
                break;
            case 2:
                InfiniteLives = false;
                MonsterDensity = 1;
                PlayerModeArmor = 1f;
                GoldDropChance = 0;
                BossHealthModifier = 1f;
                GameDifficulty = "Normal";
                GameManager.CurrentMode_ = 2;
                break;
            case 3:
                InfiniteLives = false;
                MonsterDensity = 1.35f;
                GoldDropChance = -15;
                PlayerModeArmor = 1.2f;
                BossHealthModifier = 1.3f;
                GameDifficulty = "Challenge";
                GameManager.CurrentMode_ = 3;
                break;
        }

        Lives = PlayerPrefs.GetInt("Lives");
        GameManager.minRoom_ = PlayerPrefs.GetInt("MG.minRoom");
        GameManager.maxRoom_ = PlayerPrefs.GetInt("MG.maxRoom");
        GameManager.CurrentLevel_ = PlayerPrefs.GetInt("MG.CurrentLevel");
        GameManager.LevelHasHadEvent_ = GetBool("MG.LevelHasHadEvent");
        GameManager.LevelHasHadMiniBoss_ = GetBool("MG.LevelHasHadMiniBoss");
        GameManager.GenerateFromLoad_ = true;
        GameManager.GiveLoot_ = false;
        GameManager.StartLevel_ = true;

        FirstLaunch = true;
        LoadingBarFunc();
    }

    public void ChooseMode(int mode)
    {
        switch (mode)
        {
            case 1:
                Lives = 5;
                InfiniteLives = false;
                MonsterDensity = 0.80f;
                GoldDropChance = 15;
                PlayerModeArmor = 0.7f;
                BossHealthModifier = 0.7f;
                GameDifficulty = "Easy";
                GameManager.CurrentMode_ = 1;
                break;
            case 2:
                Lives = 3;
                InfiniteLives = false;
                MonsterDensity = 1;
                PlayerModeArmor = 1f;
                GoldDropChance = 0;
                BossHealthModifier = 1f;
                GameDifficulty = "Normal";
                GameManager.CurrentMode_ = 2;
                break;
            case 3:
                Lives = 0;
                InfiniteLives = false;
                MonsterDensity = 1.35f;
                GoldDropChance = -15;
                PlayerModeArmor = 1.2f;
                BossHealthModifier = 1.3f;
                GameDifficulty = "Challenge";
                GameManager.CurrentMode_ = 3;
                break;
        }

        
        GameManager.minRoom_ = 5;
        GameManager.maxRoom_ = 7;
        GameManager.CurrentLevel_ = 0;
        GameManager.GiveLoot_ = false;

        FirstLaunch = true;
        LoadingBarFunc();
    }

    public void EscButton()
    {
        Application.Quit();
    }

}
