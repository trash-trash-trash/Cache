using UnityEngine;

public class JaiCinematicTrigger : MonoBehaviour
{
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
            
            if (Vector3.Distance(player.transform.position, transform.position) < 6)
            {
                //player.GetComponent<JaisPlayerScript>().PauseInputs();
                GetComponent<Animator>().enabled = true;
                GetComponent<Animator>().Play(0);
            }
        }
    }
}
