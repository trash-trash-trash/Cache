using System.Collections;
using TMPro;
using UnityEngine;

public class JaiCamera : MonoBehaviour
{
    public Camera cam;
    public Light light;

    public float charge;
    public TextMeshPro chargeText;

    public bool inInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.Render();
        charge = 16;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inInteraction)
        {
            chargeText.text = charge + "/16 charge";
            if (Input.GetMouseButtonDown(0))
            {
                if (charge > 0)
                {
                    cam.Render();
                    charge--;
                    StartCoroutine(Flash());
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Animator>().SetBool("charging", true);
                StartCoroutine(Charge());
            }
        }
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(4f);
        GetComponent<Animator>().SetBool("charging", false);
        charge = 16;
    }
    
    IEnumerator Flash()
    {
        light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        light.enabled = false;
    }
}
