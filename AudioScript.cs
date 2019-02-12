using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    private static float MasterSoundLevel = -10;
    private static float MasterMusicLevel = -5;
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
    public float DeathMusicSoundLevel;
    public float DeathSFXSoundLevel;

    void Start()
    {
        SetSoundMaster(MasterSoundLevel);
        SetSoundMusic(MasterMusicLevel);
        SetSoundSFX(MasterSFXLevel);
        SL.value = MasterSoundLevel;
        ML.value = MasterMusicLevel;
        SFXL.value = MasterSFXLevel;
    }
    public void ResetVolumeAfterDeath()
    {
        SetSoundMusic(MasterMusicLevel);
        SetSoundSFX(MasterSFXLevel);
    }

    public void SetSoundMaster(float soundLevel)
    {
        float SoundLevel_ = soundLevel;
        MasterSoundLevel = soundLevel;

        if (soundLevel > 0)
        {
            soundLevel = soundLevel / 2;
        }
        masterMixer.SetFloat("Master Volume", soundLevel);
        DisplayMaster.text = (Mathf.InverseLerp(MinLevel, MaxLevel, SoundLevel_) * 100).ToString("F0") + "%";

  

        if (soundLevel == -40)
        {
            masterMixer.SetFloat("Master Volume", -80);
            MasterSoundLevel = -80;
        }
    }
    public void SetSoundMusic(float soundLevel)
    {
        float SoundLevel_ = soundLevel;
        MasterMusicLevel = soundLevel;
        DeathMusicSoundLevel = soundLevel;
        if (soundLevel > 0)
        {
            soundLevel = soundLevel / 2;
        }
        masterMixer.SetFloat("Music Volume", soundLevel);
        DisplayMusic.text = (Mathf.InverseLerp(MinLevel, MaxLevel, SoundLevel_) * 100).ToString("F0") + "%";


        if (soundLevel == -40)
        {
            masterMixer.SetFloat("Music Volume", -80);
            MasterMusicLevel = -80;
            DeathMusicSoundLevel = -80;
        }

    }
    public void SetSoundSFX(float soundLevel)
    {
        float SoundLevel_ = soundLevel;
        MasterSFXLevel = soundLevel;
        DeathSFXSoundLevel = soundLevel;
        if (soundLevel > 0)
        {
            soundLevel = soundLevel / 2;
        }
        masterMixer.SetFloat("SFX Volume", soundLevel);
        masterMixer.SetFloat("Death", soundLevel);
        DisplaySFX.text = (Mathf.InverseLerp(MinLevel, MaxLevel, SoundLevel_) * 100).ToString("F0") + "%";

        if (soundLevel == -40)
        {
            masterMixer.SetFloat("SFX Volume", -80);
            masterMixer.SetFloat("Death", -80);
            MasterSFXLevel = -80;
            DeathSFXSoundLevel = -80;
        }
    }
}
