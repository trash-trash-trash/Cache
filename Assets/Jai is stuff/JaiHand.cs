using System;
using UnityEngine;

public class JaiHand : MonoBehaviour
{
    public int hand;
    public int calculatedTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hand == 1)
        {
            calculatedTime = Mathf.RoundToInt(Mathf.Abs((transform.eulerAngles.x) / 6));
        }
        if (hand == 2)
        {
            calculatedTime = Mathf.RoundToInt(((transform.eulerAngles.x) / 30));
        }
    }

    private void OnMouseDown()
    {
        GetComponentInParent<JaiClock>().handSelected = hand;
    }
}