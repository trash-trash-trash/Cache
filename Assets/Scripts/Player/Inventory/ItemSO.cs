using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
[Serializable]
public class ItemSO : ScriptableObject
{
    public event Action AnnounceReset;
    
    public string itemName;
    public Sprite icon;

    public GameObject model;
    //public GameObject objectRepresentation; //so when held has obj in hand

    public bool useable;
    public string useString;
    
    public void Reset()
    {
        AnnounceReset?.Invoke();
    }
}