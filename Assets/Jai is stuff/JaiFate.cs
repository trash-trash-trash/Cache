using UnityEngine;

public class JaiFate : MonoBehaviour
{
    public Animator[] ans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            foreach (var a in ans)
            {
                a.enabled = true;
                a.Play(0);
            }
        }
    }
}
