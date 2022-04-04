using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField]
    Slider autoTimeSlider;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { MusicSliderValueChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { SFXSliderValueChanged(); });
        autoTimeSlider.onValueChanged.AddListener(delegate { AutoTimeSlidervalueChanged(); });

        musicSlider.value = DataManager.Instance.GetFloatSetting("Music_Volume");
        sfxSlider.value = DataManager.Instance.GetFloatSetting("SFX_Volume");
        autoTimeSlider.value = DataManager.Instance.GetFloatSetting("Auto_Time");
    }

    void MusicSliderValueChanged()
    {
        MusicManager.Instance.SetVolume(musicSlider.value);
        DataManager.Instance.SetFloatSetting("Music_Volume", musicSlider.value);
    }

    void SFXSliderValueChanged()
    {
        SFXManager.Instance.SetVolume(sfxSlider.value);
        DataManager.Instance.SetFloatSetting("SFX_Volume", sfxSlider.value);
    }

    void AutoTimeSlidervalueChanged()
    {
        DataManager.Instance.SetAutoTime(autoTimeSlider.value);
        DataManager.Instance.SetFloatSetting("Auto_Time", autoTimeSlider.value);
    }
}
