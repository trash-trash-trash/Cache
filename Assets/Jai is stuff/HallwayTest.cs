using UnityEngine;

public class HallwayTest : MonoBehaviour
{
    //rot around X
    //if before start, -90X, if after end 0X

    private PlayerInputHandler player;
    GameObject playerObj;

    private float rotValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerInputHandler>();
        playerObj = player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.transform.position.z < -5f &&  player.transform.position.z > -180f)
        {
            //rotvalue is between -90 and 0 direct proportion of -5 to -180
            
            
            //0 - -180   === -90 - 0,,    /2 =     0 - -90  ===  -90 - 0
            //rotValue = playerObj.transform.position.z/2;
            rotValue = Mathf.Lerp(-90, 0, playerObj.transform.position.z/-180);
            print(rotValue);
            //rotate
            transform.eulerAngles = new Vector3(rotValue, -90, 90);
        }
        
        
    }
}
