using System;
using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemSO itemSO;

    private void Start()
    {
        if (itemSO.model != null)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GameObject mdl = Instantiate(itemSO.model, transform.position, transform.rotation, this.transform);
            mdl.transform.localScale *= 4;
        }
    }

    public override void Interact()
    {
        if (canInteractWith && iInteractInRangeToInteract)
        {
            Inventory inventory = iInteractTransform.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(itemSO);
                PlayerInteract player = iInteractTransform.GetComponent<PlayerInteract>();
                CloseInteractable(player);
                gameObject.SetActive(false);
                itemSO.AnnounceReset += Reset;
            }
        }
    }

    private void Reset()
    {
        gameObject.SetActive(true);
    }

    public override string InteractString()
    {
        return "E: PICK UP "+itemSO.itemName;
    }
}