using System;
using System.Collections;
using UnityEngine;

public class JaisPlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("CHNG").gameObject.layer = 8;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //hit.collider.gameObject.layer = 8;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeInputs();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            float diffX = other.transform.position.x - other.GetComponent<Teleporter>().exit.transform.position.x;
            float diffY = other.transform.position.y - other.GetComponent<Teleporter>().exit.transform.position.y;
            float diffZ = other.transform.position.z - other.GetComponent<Teleporter>().exit.transform.position.z;

            print(diffX + " " + diffY + " " + diffZ);
            transform.position -= new Vector3(diffX, diffY, diffZ);
            //transform.position = other.GetComponent<Teleporter>().exit.transform.position;
            //transform.rotation = other.GetComponent<Teleporter>().exit.transform.rotation;
            transform.eulerAngles += new Vector3(0, 180, 0);
        }

        if (other.CompareTag("Tape"))
        {
            FindFirstObjectByType<JaiCamera>().charge += 8;
            other.gameObject.SetActive(false);
            StartCoroutine(WakeUp(other.gameObject, 10));
        }
    }

    //move to global timer
    IEnumerator WakeUp(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }

    public void PauseInputs()
    {
        FindFirstObjectByType<JaiCamera>().inInteraction = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //would also stop movement
    }

    public void ResumeInputs()
    {
        FindFirstObjectByType<JaiCamera>().inInteraction = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //would also stop movement
    }
}