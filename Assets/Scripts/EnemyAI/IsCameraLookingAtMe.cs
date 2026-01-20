using System;
using UnityEngine;

public class IsCameraLookingAtMe : MonoBehaviour
{ 
    public event Action<bool> AnnounceInView;
    RaycastHit hit;
    public LayerMask layerMask;
    int finalLayerMask;
    void OnBecameVisible()
    {
        finalLayerMask = ~layerMask;
        
        if (Physics.Raycast(transform.position, (Camera.main.gameObject.transform.position - transform.position), out hit, Mathf.Infinity, finalLayerMask))
        {
            print(hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                AnnounceInView?.Invoke(true);
            }
        }
    }

    private void OnBecameInvisible()
    {
        AnnounceInView?.Invoke(false);
    }
}
