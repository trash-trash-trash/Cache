using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public Inventory inventory;

    public GameObject inventoryParentObj;
    public GameObject gridParentObj;
    public GameObject itemSlotPrefab;

    public GameObject emptyObject;

    public Button leftArrow;
    public Button rightArrow;

    public TMP_Text inventoryText;

    public GameObject openCloseInventoryTextObj;
    public TMP_Text openCloseInventoryText;

    [SerializeField] private bool inventoryHasItems = false;

    void Awake()
    {
        inventory.AnnounceInventoryFullEmpty += ShowOpenCloseInventoryText;
        inventory.AnnounceOpenCloseInventory += OpenCloseInventory;
        inventory.AnnounceSelectIndex += ScrollToItem;
    }

    private void ShowOpenCloseInventoryText(bool newInventoryHasItems)
    {
        inventoryHasItems = newInventoryHasItems;
        
        if (inventoryHasItems && inventory.inventoryOpen)
        {
            openCloseInventoryText.text = "I - CLOSE";
        }
        else if (inventoryHasItems && !inventory.inventoryOpen)
        {
            openCloseInventoryText.text = "I - <size=200%><b>I</b></size>NVENTORY";
        }
        
        if (inventoryHasItems)
        {
            openCloseInventoryTextObj.SetActive(true);
        }
        else
        {
            openCloseInventoryTextObj.SetActive(false);
        }
        
    }

    private void ScrollToItem(int index)
    {
        RefreshUI();
    }

    void OpenCloseInventory(bool input)
    {
        if (input)
        {
            RefreshUI();
            inventoryParentObj.SetActive(true);
        }
        else
        {
            inventoryParentObj.SetActive(false);
        }
        
        ShowOpenCloseInventoryText(inventoryHasItems);
    }

    void RefreshUI()
    {
        // Clear old UI (except arrows and empty object)
        List<Transform> toDestroy = new List<Transform>();

        foreach (Transform child in gridParentObj.transform)
        {
            if (child.gameObject != rightArrow.gameObject &&
                child.gameObject != leftArrow.gameObject &&
                child.gameObject != emptyObject.gameObject)
            {
                toDestroy.Add(child);
            }
        }

        foreach (Transform child in toDestroy)
            Destroy(child.gameObject);

        int count = inventory.playerItems.Count;

        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);
        emptyObject.SetActive(false);

        if (count == 0) return; // No items

        // Determine indices
        int selected = inventory.selectIndex;
        int leftIndex = (selected - 1 + count) % count;
        int rightIndex = (selected + 1) % count;

        // Track slots to insert in order
        List<GameObject> slotObjects = new List<GameObject>();

        if (count == 1)
        {
            // Only one item
            GameObject slot = CreateSlot(inventory.playerItems[selected], true);
            slot.transform.SetParent(gridParentObj.transform, false);

            leftArrow.gameObject.SetActive(false);
            rightArrow.gameObject.SetActive(false);
            emptyObject.SetActive(false);
        }
        else if (count == 2)
        {
            // Two items: selected (x2) + other + empty object
            leftArrow.gameObject.SetActive(true);
            rightArrow.gameObject.SetActive(true);
            emptyObject.SetActive(true);

            leftArrow.transform.SetParent(gridParentObj.transform, false);
            emptyObject.transform.SetParent(gridParentObj.transform, false);

            GameObject leftSlot = CreateSlot(inventory.playerItems[selected], true);
            leftSlot.transform.SetParent(gridParentObj.transform, false);

            GameObject rightSlot = CreateSlot(inventory.playerItems[rightIndex], false);
            rightSlot.transform.SetParent(gridParentObj.transform, false);

            rightArrow.transform.SetParent(gridParentObj.transform, false);
            rightArrow.transform.SetAsLastSibling();
        }
        else
        {
            // Three or more items: left, selected (x2), right
            leftArrow.gameObject.SetActive(true);
            rightArrow.gameObject.SetActive(true);

            leftArrow.transform.SetParent(gridParentObj.transform, false);

            GameObject leftSlot = CreateSlot(inventory.playerItems[leftIndex], false);
            leftSlot.transform.SetParent(gridParentObj.transform, false);

            GameObject centerSlot = CreateSlot(inventory.playerItems[selected], true);
            centerSlot.transform.SetParent(gridParentObj.transform, false);

            GameObject rightSlot = CreateSlot(inventory.playerItems[rightIndex], false);
            rightSlot.transform.SetParent(gridParentObj.transform, false);

            rightArrow.transform.SetParent(gridParentObj.transform, false);
            rightArrow.transform.SetAsLastSibling();
        }

        if (inventory.selectedItem.useable)
        {
            inventoryText.text = inventory.selectedItem.useString;
        }
        else
        {
            inventoryText.text = "";
        }
    }

    private GameObject CreateSlot(ItemSO item, bool isSelected)
    {
        GameObject slot = Instantiate(itemSlotPrefab);
        slot.GetComponentInChildren<Image>().sprite = item.icon;
        slot.GetComponentInChildren<TMP_Text>().text = item.itemName;
        slot.transform.localScale = isSelected ? Vector3.one * 2f : Vector3.one;
        slot.GetComponentInChildren<MenuModel>().model = item.model;
        return slot;
    }


    void OnDisable()
    {
        inventory.AnnounceInventoryFullEmpty -= ShowOpenCloseInventoryText;
        inventory.AnnounceOpenCloseInventory -= OpenCloseInventory;
        inventory.AnnounceSelectIndex -= ScrollToItem;
    }
}