using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    private static float MasterSoundLevel = -10;
    private static float MasterMusicLevel = -10;
    private static float MasterSFXLevel = -10;

    public Text DisplayMaster;
    public Text DisplayMusic;
    public Text DisplaySFX;
    public Slider SL;
    public Slider ML;
    public Slider SFXL;

    public AudioMixer masterMixer;
    public float MinLevel;
    public float MaxLevel;

    void Start()
    {
        SetSoundMaster(MasterSoundLevel);
        SetSoundMusic(MasterMusicLevel);
        SetSoundSFX(MasterSFXLevel);
        SL.value = MasterSoundLevel;
        ML.value = MasterMusicLevel;
        SFXL.value = MasterSFXLevel;
    }

    public void SetSoundMaster(float soundLevel)
    {
        masterMixer.SetFloat("Master Volume", soundLevel);
        DisplayMaster.text = (Mathf.InverseLerp(MinLevel, MaxLevel, soundLevel) * 100).ToString("F0") + "%";
        MasterSoundLevel = soundLevel;
    }
    public void SetSoundMusic(float soundLevel)
    {
        masterMixer.SetFloat("Music Volume", soundLevel);
        DisplayMusic.text = (Mathf.InverseLerp(MinLevel, MaxLevel, soundLevel) * 100).ToString("F0") + "%";
        MasterMusicLevel = soundLevel;
    }
    public void SetSoundSFX(float soundLevel)
    {
        masterMixer.SetFloat("SFX Volume", soundLevel);
        DisplaySFX.text = (Mathf.InverseLerp(MinLevel, MaxLevel, soundLevel) * 100).ToString("F0") + "%";
        MasterSFXLevel = soundLevel;
    }
}
