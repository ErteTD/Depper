﻿using System.Collections;
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


    public Text ModeText;

    public List<Monster> Mlist;
    private bool BarBool;
    public GameObject LoadingBar;
    public Image loadingBar;
    public Text textPourcentage;
    [Header ("Settings")]
    public Toggle VSyncToggle;
    public Toggle FSToggle;
    public Dropdown ScreenResolution;
    public Dropdown AASettings;
    //internal static int VsyncIni;
    internal static bool ResChecked;
    internal static int ResCheckInt;

    internal static Resolution[] resolutions;
    List<int> FakeCount = new List<int>();
    private bool DontUpdateRes;
    internal static int AASetting;


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

    public void SetModeText(int mode)
    {
        switch (mode)
        {
            case 1:
                ModeText.text = "- Infinite lives \n- Lower monster density \n- Monsters deal less damage \n- Bosses have less health";
                break;
            case 2:
                ModeText.text = "- 3 extra lives \n- Normal monster density \n- Monsters deal normal damage \n- Bosses have normal health";
                break;
            case 3:
                ModeText.text = "- 0 extra lives \n- Higher monster density \n- Monsters deal more damage \n- Bosses have more health";
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
                GameDifficulty = "Casual";
                break;
            case 2:
                Lives = 3;
                InfiniteLives = false;
                MonsterDensity = 1;
                PlayerModeArmor = 1f;
                GoldDropChance = 0;
                GameDifficulty = "Normal";
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
