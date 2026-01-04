using System;
using System.Collections;
using UnityEngine;

public class JaiKeyPadKey : MonoBehaviour
{
    public string key;
    public Light light;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        print("PRESSED KEY!!!");
        StartCoroutine(Flash());
        GetComponentInParent<JaiKeyPad>().InputKey(key);
    }
    
    IEnumerator Flash()
    {
        light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        light.enabled = false;
    }
}
