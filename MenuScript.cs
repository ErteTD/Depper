using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    AsyncOperation async;
    public static int Lives;
    public static bool InfiniteLives;
    public List<Monster> Mlist;


    private bool BarBool;
    public GameObject LoadingBar;
    public Image loadingBar;
    public Text textPourcentage;
    [Header ("Settings")]
    public Toggle VSyncToggle;
    public Dropdown ScreenResolution;
    internal static int VsyncIni;
    internal static int Screen1;
    internal static int Screen2;

    public void Start()
    {


        if (Screen.width == 1366 & Screen.height == 768)
        {
            ScreenResolution.value = 0;
        }
        else if (Screen.width == 1600 & Screen.height == 900)
        {
            ScreenResolution.value = 1;
        }
        else if (Screen.width == 1920 & Screen.height == 1080)
        {
            ScreenResolution.value = 2;
        }
        else if (Screen.width == 2560 & Screen.height == 1440)
        {
            ScreenResolution.value = 3;
        }
        else
        {
            ChooseResolution();
            ScreenResolution.value = 2;
        }

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
    }

    public void TurnOnVSync()
    {
        bool testB = VSyncToggle.isOn;
        VsyncIni = QualitySettings.vSyncCount;
        QualitySettings.vSyncCount = testB ? 1 : 0;
    }


    public void ChooseResolution()
    {
        switch (ScreenResolution.value)
        {
            case 0:
                Screen.SetResolution(1366, 768, true);
                break;
            case 1:
                Screen.SetResolution(1600, 900, true);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, true);
                break;
            default:
                Screen.SetResolution(1920, 1080, true);
                break;
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
                break;
            case 2:
                Lives = 3;
                InfiniteLives = false;
                break;
            case 3:
                Lives = 0;
                InfiniteLives = false;
                break;
        }
        LoadingBarFunc();
    }

    public void EscButton()
    {
        Application.Quit();
    }

}
