using System.Collections;
using UnityEngine;

public class JaiKeyPad : MonoBehaviour
{
    public string code;
    private string recievedCode;

    public Light lightWrong;
    public Light lightRight;

    public JaiDoor myDoor;

    private bool unlocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!unlocked)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject player = FindFirstObjectByType<JaisPlayerScript>().gameObject;
                if (Vector3.Distance(player.transform.position, transform.position) < 5)
                {
                    player.GetComponent<JaisPlayerScript>().PauseInputs();
                }
            }

            if (recievedCode == code)
            {
                lightRight.enabled = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                FindFirstObjectByType<JaiCamera>().inInteraction = false;
                myDoor.Open();
                unlocked = true;
            }

            if (recievedCode != null)
            {
                if (recievedCode != code && recievedCode.Length >= 4)
                {
                    StartCoroutine(FlashWrong());
                    recievedCode = "";
                }
            }
        }

    }

    public void InputKey(string key)
    {
        recievedCode += key;
    }
    
    IEnumerator FlashWrong()
    {
        lightWrong.enabled = true;
        yield return new WaitForSeconds(1);
        lightWrong.enabled = false;
    }
}
