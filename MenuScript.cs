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
    public static float BossHealthModifier = 1;
    public static string GameDifficulty = "Normal";




    public List<Monster> Mlist;
    private bool BarBool;
    public GameObject LoadingBar;
    public Image loadingBar;
    public Text textPourcentage;
    [Header ("Settings")]
    public Toggle VSyncToggle;
    public Toggle FSToggle;
    public Dropdown ScreenResolution;
    internal static int VsyncIni;
    internal static bool ResChecked;
    internal static int ResCheckInt;

    internal static Resolution[] resolutions;
    List<int> FakeCount = new List<int>();
    private bool DontUpdateRes;


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

    }

    public void TurnOnVSync()
    {
        bool testB = VSyncToggle.isOn;
        VsyncIni = QualitySettings.vSyncCount;
        QualitySettings.vSyncCount = testB ? 1 : 0;
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

    void LoadingBarFunc()
    {
        BarBool = true;
        LoadingBar.SetActive(true);
        async = SceneManager.LoadSceneAsync("Level One 1");
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
                BossHealthModifier = 0.7f;
                GameDifficulty = "Easy Mode";
                break;
            case 2:
                Lives = 3;
                InfiniteLives = false;
                MonsterDensity = 1;
                GoldDropChance = 0;
                GameDifficulty = "Normal Mode";
                break;
            case 3:
                Lives = 0;
                InfiniteLives = false;
                MonsterDensity = 1.3f;
                GoldDropChance = -15;
                BossHealthModifier = 1.3f;
                GameDifficulty = "Hard Mode";
                break;
        }
        LoadingBarFunc();
    }

    public void EscButton()
    {
        Application.Quit();
    }

}
