using System;
using UnityEngine;

[Serializable]
public class Door : Interactable
{
    public bool locked = false;
    public bool open = false;
    public bool canUnlock = false;
    private bool hasKey = false;

    private bool originalLocked;
    private bool originalCanUnlock;

    public ItemSO key;

    private Inventory inventory;

    public bool foreverLocked = false;
    
    void OnEnable()
    {
        if (foreverLocked)
        {
            originalLocked = true;
            originalCanUnlock = false;
        }
        originalLocked = locked;
        originalCanUnlock = canUnlock;
    }
    
    public override void Interact()
    {
    }
    
    protected override void OnIInteractEntered(Transform other)
    {
        base.OnIInteractEntered(other);

        if (iInteractTransform != null && inventory == null)
        {
            inventory = iInteractTransform.GetComponent<Inventory>();
        }
    }
    
    public override string InteractString()
    {
        if (foreverLocked) return "";

        if (locked)
        {
            Transform t = iInteractTransform;
            if (t == null) return "";

            Inventory inv = t.GetComponent<Inventory>();
            if (inv == null) return "";

            bool playerHasKey = inv.FindSpecificItemInInventory(key);
            if (!playerHasKey) return "LOCKED. FIND THE KEY";

            bool keyEquipped = inv.selectedItem.item == key;
            if (!keyEquipped) return "KEY NOT EQUIPPED!";

            return "E: UNLOCK";
        }

        return open ? "E: CLOSE DOOR" : "E: OPEN DOOR";
    }



    public virtual void ResetDoor()
    {
        locked = originalLocked;
        canUnlock = originalCanUnlock;
    }

    public virtual void TryUnlock()
    {
        Inventory inventory = iInteractTransform.GetComponent<Inventory>();
        if (inventory.selectedItem.item == key)
        {
            locked = false;
            inventory.RemoveItem(key);
        }
    }

    public virtual void OpenCloseDoor()
    {
    }
}
