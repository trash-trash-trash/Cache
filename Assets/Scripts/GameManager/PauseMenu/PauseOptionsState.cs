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

    public void ChangeSensitivity()
    {        
        mouseSensitivityText.text = "Mouse Sensitivity - " + mouseSensitivitySlider.value;

        FindFirstObjectByType<PlayerMovementHandler>().mouseSensitivity = mouseSensitivitySlider.value/100;
    }

    public void ChangeVolume()
    {
        volumeText.text = "Volume - " + volumeSlider.value +"%";
        AudioListener.volume =  volumeSlider.value/100;
    }
}
