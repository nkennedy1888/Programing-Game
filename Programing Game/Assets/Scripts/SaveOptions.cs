using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer sfxAudioMixer;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider sfxVolumeSlider = null;
    [SerializeField] private Text musicVolumeText = null;
    [SerializeField] private Text sfxVolumeText = null;
    

    public void MusicVolumeSlider (float volume)
    {
        musicVolumeText.text = volume.ToString("0.0");
    }

    public void SFXVolumeSlider(float volume)
    {
        sfxVolumeText.text = volume.ToString("0.0");
    }

    public void SaveButton()
    {
        float musicVolumeValue = musicVolumeSlider.value;
        float sfxVolumeValue = sfxVolumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolumeValue", musicVolumeValue);
        PlayerPrefs.SetFloat("SFXVolumeValue", sfxVolumeValue);
        SetMusicVolume(musicVolumeValue);
        SetSFXVolume(sfxVolumeValue);
    }

    /*void LoadValues()
    {
        float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolumeValue");
        float sfxVolumeValue = PlayerPrefs.GetFloat("SFXVolumeValue");
        musicVolumeSlider.value = musicVolumeValue;
        sfxVolumeSlider.value = sfxVolumeValue;

        myAudioMixer.SetFloat("ambientMusicVolume", Mathf.Log10(musicVolumeValue) * 20);
        myAudioMixer.SetFloat("battleMusicVolume", Mathf.Log10(musicVolumeValue) * 20);

        myAudioMixer.SetFloat("jumpSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("selectSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("projectileSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("healSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("takeDamageSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("damageEnemySFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("walkSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("backSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("navigateSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
    }*/
    public void SetMusicVolume(float musicVolumeValue)
    {
        musicAudioMixer.SetFloat("ambientMusicVolume", Mathf.Log10(musicVolumeValue) * 20);
        musicAudioMixer.SetFloat("battleMusicVolume", Mathf.Log10(musicVolumeValue) * 20);

        /*myAudioMixer.SetFloat("jumpSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("selectSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("projectileSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("healSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("takeDamageSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("damageEnemySFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("walkSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("backSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        myAudioMixer.SetFloat("navigateSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);*/
    }
    public void SetSFXVolume(float sfxVolumeValue)
    {
        sfxAudioMixer.SetFloat("jumpSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("selectSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("projectileSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("healSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("takeDamageSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("damageEnemySFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("walkSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("backSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
        sfxAudioMixer.SetFloat("navigateSFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
    }
}
