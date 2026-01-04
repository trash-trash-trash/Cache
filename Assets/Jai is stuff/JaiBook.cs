using UnityEngine;

public class JaiBook : MonoBehaviour
{
    public TextAsset book;
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
                FindFirstObjectByType<JaiBookUiManager>().LoadPages(book);
            }
        }
    }
}
