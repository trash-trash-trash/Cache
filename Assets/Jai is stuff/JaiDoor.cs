using System.Collections;
using UnityEngine;

public class JaiDoor : MonoBehaviour
{
    public bool locked;
    public bool requireKey;
    public bool requireCode;
    public JaiKeyPad keypad;

    public GameObject door;

    public float doorHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keypad.myDoor = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        while (door.transform.position.y < doorHeight)
        {
            yield return new WaitForSeconds(0.01f);
            door.transform.position += door.transform.up * 0.01f;
        }
    }
}
