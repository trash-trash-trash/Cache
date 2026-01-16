using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("Saved") != 0)
        {
            FindFirstObjectByType<PlayerMovementHandler>().mouseSensitivity = (PlayerPrefs.GetInt("Sensitivity")/100);
            AudioListener.volume = PlayerPrefs.GetFloat("Volume")/100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save(string id, int value)
    {
        if (PlayerPrefs.GetInt("Saved") == 0)
        {
            PlayerPrefs.SetInt("Saved", 1);
        }
        PlayerPrefs.SetInt(id,  value);
    }

    public int Load(string id)
    {
        return PlayerPrefs.GetInt(id);
    }
}
