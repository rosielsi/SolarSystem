using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicmanager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void start() {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            load();
        }
        else {
            load();
        }
    }

    public void changeVolume() {
        AudioListener.volume = volumeSlider.value;
        save();
    }

    private void load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

    }
    private void save() {
        PlayerPrefs.SetFloat("musicVolume",volumeSlider.value);
    }
}
