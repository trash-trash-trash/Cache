using UnityEngine;

public class WindowCamera : MonoBehaviour
{
    private float xrot;
    private float yrot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yrot += Input.GetAxis("Mouse X") * 1;
        xrot += Input.GetAxis("Mouse Y") * -1;
        transform.eulerAngles = new Vector3(xrot, yrot, 0);
    }
}
