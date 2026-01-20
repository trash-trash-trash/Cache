using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("Saved") != 0)
        {
            float sensitivity = Load("Sensitivity");
            FindFirstObjectByType<PlayerMovementHandler>().mouseSensitivity = sensitivity/100;
            
            float vol =  Load("Volume");
            AudioListener.volume = vol/100;
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
