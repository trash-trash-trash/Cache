using UnityEngine;
using UnityEngine.UI;

public class MenuModel : MonoBehaviour
{
    public GameObject model;
    public RawImage img;

    private GameObject obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (obj != null)
        {
            Destroy(obj);
        }

        obj = Instantiate(model, transform.position, transform.rotation, transform);
        obj.transform.localScale *= 80;
        RenderTexture rt = new RenderTexture(1024, 1024, 16);
        img.texture = rt;
        GetComponentInParent<Camera>().targetTexture = rt;
        
        Vector3 handScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = Input.mousePosition - handScreenPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle*2, 0);
    }
}