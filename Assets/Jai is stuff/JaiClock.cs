using System;
using UnityEngine;

public class JaiClock : MonoBehaviour
{
    public GameObject minuteHand;
    public GameObject hourHand;

    public int handSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject player = FindFirstObjectByType<JaisPlayerScript>().gameObject;

            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                player.GetComponent<JaisPlayerScript>().PauseInputs();
            }
        }

        if (handSelected != 0)
        {
            GameObject hand = minuteHand;
            float offset = 180;
            if (handSelected == 2)
            {
                hand = hourHand;
                offset = 90;
            }
            
            
            Vector3 handScreenPos = Camera.main.WorldToScreenPoint(hand.transform.position);
            Vector2 direction = Input.mousePosition - handScreenPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            hand.transform.rotation = Quaternion.Euler(-angle+offset, 180, -90);
        }

        if (Input.GetMouseButtonUp(0))
        {
            handSelected = 0;
        }
    }
}