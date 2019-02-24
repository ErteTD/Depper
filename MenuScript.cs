using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public static string SpellSlot1Key;
    public static string SpellSlot2Key;
    public static string SpellSlot3Key;
    public static string ItemKey;
    public static string MoveUpKey;
    public static string MoveDownKey;
    public static string MoveLeftKey;
    public static string MoveRightKey;

    public GameObject KeyBinder;

    [Header ("KeyBindings")]
    public InputField SpellSlot1Key_;
    public InputField SpellSlot2Key_;
    public InputField SpellSlot3Key_;
    public InputField ItemKey_;
    public InputField MoveUpKey_;
    public InputField MoveDownKey_;
    public InputField MoveLeftKey_;
    public InputField MoveRightKey_;
    public Text InputText1;
    public Button InputButton1;
    public Text InputText2;
    public Button InputButton2;

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
        InverseMouseButtons = PlayerPrefs.GetInt("InvMouse", 0);
        SpellSlot1Key = PlayerPrefs.GetString("SpellSlot1Key", "Alpha1");
        SpellSlot2Key = PlayerPrefs.GetString("SpellSlot2Key", "Alpha2");
        SpellSlot3Key = PlayerPrefs.GetString("SpellSlot3Key", "Alpha3");
        ItemKey = PlayerPrefs.GetString("ItemKey", "R");
        MoveUpKey = PlayerPrefs.GetString("MoveUpKey", "W");
        MoveDownKey = PlayerPrefs.GetString("MoveDownKey", "S");
        MoveLeftKey = PlayerPrefs.GetString("MoveLeftKey", "A");
        MoveRightKey = PlayerPrefs.GetString("MoveRightKey", "D");

        string SpellSlot1Key_2 = SpellSlot1Key;
        string SpellSlot2Key_2 = SpellSlot2Key;
        string SpellSlot3Key_2 = SpellSlot3Key;
        string ItemKey_2 = ItemKey;
        string MoveUpKey_2 = MoveUpKey;
        string MoveDownKey_2 = MoveDownKey;
        string MoveLeftKey_2 = MoveLeftKey;
        string MoveRightKey_2 = MoveRightKey;


        SpellSlot1Key_2 = CheckIfStringIsNumber(SpellSlot1Key_2, false);
        SpellSlot2Key_2 = CheckIfStringIsNumber(SpellSlot2Key_2, false);
        SpellSlot3Key_2 = CheckIfStringIsNumber(SpellSlot3Key_2, false);
        ItemKey_2 = CheckIfStringIsNumber(ItemKey_2, false);
        MoveUpKey_2 = CheckIfStringIsNumber(MoveUpKey_2, false);
        MoveDownKey_2 = CheckIfStringIsNumber(MoveDownKey_2, false);
        MoveLeftKey_2 = CheckIfStringIsNumber(MoveLeftKey_2, false);
        MoveRightKey_2 = CheckIfStringIsNumber(MoveRightKey_2, false);


        SpellSlot1Key_.text = SpellSlot1Key_2;
        SpellSlot2Key_.text = SpellSlot2Key_2;
        SpellSlot3Key_.text = SpellSlot3Key_2;
        ItemKey_.text = ItemKey_2;
        MoveUpKey_.text = MoveUpKey_2;
        MoveDownKey_.text = MoveDownKey_2;
        MoveLeftKey_.text = MoveLeftKey_2;
        MoveRightKey_.text = MoveRightKey_2;

        if (InverseMouseButtons == 1)
        {
            MouseMovement = 1;
            MouseFire = 0;
        }
        else
        {
            MouseMovement = 0;
            MouseFire = 1;
        }
    }


    string CheckIfStringIsNumber(string Key, bool Save)
    {
        char c = Key[0];
        if (Save && System.Char.IsDigit(c))
        {
            Key = "Alpha" + Key;
            return Key;
        }
        //else if (Save)asd
        //{
        //    Key = "Mouse" + Key;
        //    return Key;
        //}
        else if (Save)
        {
            return Key.ToUpper();
        }

        else if (!Save && Key.Length > 1)
        {
            return Key = Key.Substring(Key.Length - 1);
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

            string SpellSlot1Key_2 = SpellSlot1Key_.text;
            string SpellSlot2Key_2 = SpellSlot2Key_.text;
            string SpellSlot3Key_2 = SpellSlot3Key_.text;
            string ItemKey_2 = ItemKey_.text;
            string MoveUpKey_2 = MoveUpKey_.text;
            string MoveDownKey_2 = MoveDownKey_.text;
            string MoveLeftKey_2 = MoveLeftKey_.text;
            string MoveRightKey_2 = MoveRightKey_.text;


            SpellSlot1Key_2 = CheckIfStringIsNumber(SpellSlot1Key_.text, true);
            SpellSlot2Key_2 = CheckIfStringIsNumber(SpellSlot2Key_.text, true);
            SpellSlot3Key_2 = CheckIfStringIsNumber(SpellSlot3Key_.text, true);
            ItemKey_2 = CheckIfStringIsNumber(ItemKey_.text, true);
            MoveUpKey_2 = CheckIfStringIsNumber(MoveUpKey_.text, true);
            MoveDownKey_2 = CheckIfStringIsNumber(MoveDownKey_.text, true);
            MoveLeftKey_2 = CheckIfStringIsNumber(MoveLeftKey_.text, true);
            MoveRightKey_2 = CheckIfStringIsNumber(MoveRightKey_.text, true);

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


    public void ResetKeyBinds()
    {

        InverseMouseButtons = PlayerPrefs.GetInt("InvMouse", 0);

        PlayerPrefs.SetInt("InvMouse", 0);
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
                ModeText.text = "- Infinite lives \n- Normal monster density \n- Monsters deal normal damage \n- Bosses have normal health";
                break;
            case 2:
                ModeText.text = "- 3 extra lives \n- Higher monster density \n- Monsters deal more damage \n- Bosses have more health";
                break;
            case 3:
                ModeText.text = "- 0 extra lives \n- Highest monster density \n- Monsters deal most damage \n- Bosses have most health";
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

    private void Update()
    {
        if (BarBool)
        {
            StartCoroutine(LevelCoroutine());
        }

        if (Input.anyKeyDown && BindAButton)
        {
            for (int ButtonNumber = 0; ButtonNumber < 6; ButtonNumber++)
            {
                if (Input.GetMouseButtonDown(ButtonNumber))
                {
                    ActiveInputTextField.text = ("Mouse" + ButtonNumber);
                    EndKeyInput();
                }
            }
            if (Input.inputString.Length > 0)
            {
                char c = Input.inputString[0];
                if (System.Char.IsLetter(c) || System.Char.IsDigit(c))
                {
                    ActiveInputTextField.text = Input.inputString;
                    EndKeyInput();
                }
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                EndKeyInput();
            }
        }
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

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

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

    public void ChooseMode(int mode)
    {
        switch (mode)
        {
            case 1:
                Lives = 1;
                InfiniteLives = true;
                MonsterDensity = 0.70f;
                GoldDropChance = 15;
                PlayerModeArmor = 0.7f;
                BossHealthModifier = 0.7f;
                GameDifficulty = "Normal";
                break;
            case 2:
                Lives = 3;
                InfiniteLives = false;
                MonsterDensity = 1;
                PlayerModeArmor = 1f;
                GoldDropChance = 0;
                GameDifficulty = "Hard";
                break;
            case 3:
                Lives = 0;
                InfiniteLives = false;
                MonsterDensity = 1.35f;
                GoldDropChance = -15;
                PlayerModeArmor = 1.2f;
                BossHealthModifier = 1.3f;
                GameDifficulty = "Challenge";
                break;
        }
        FirstLaunch = true;
        LoadingBarFunc();
    }

    public void EscButton()
    {
        Application.Quit();
    }

}
