using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsState : PauseStateBase
{
    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivityText;
    
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    public override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Saved") != 0)
        {
            mouseSensitivitySlider.value = FindFirstObjectByType<SaveManager>().Load("Sensitivity");
            volumeSlider.value = FindFirstObjectByType<SaveManager>().Load("Volume");
        }
        else
        {
            FindFirstObjectByType<SaveManager>().Save("Sensitivity", Mathf.RoundToInt(mouseSensitivitySlider.value));
            FindFirstObjectByType<SaveManager>().Save("Volume", Mathf.RoundToInt(volumeSlider.value));
        }
    }

    public void ChangeSensitivity()
    {        
        mouseSensitivityText.text = "Mouse Sensitivity - " + mouseSensitivitySlider.value;
        FindFirstObjectByType<PlayerMovementHandler>().mouseSensitivity = mouseSensitivitySlider.value/100;
        
        FindFirstObjectByType<SaveManager>().Save("Sensitivity", Mathf.RoundToInt(mouseSensitivitySlider.value));

    }

    public void ChangeVolume()
    {
        volumeText.text = "Volume - " + volumeSlider.value +"%";
        AudioListener.volume =  volumeSlider.value/100;
            
        FindFirstObjectByType<SaveManager>().Save("Volume", Mathf.RoundToInt(volumeSlider.value));
    }
}
