using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public bool inventoryOpen = false;
   public bool canOpenInventory = true;
   public bool canUseInventory = true;
   
   //reset between open/close or remain consistent?
   public int selectIndex = 0;
   [SerializeField]
   public InventoryItem selectedItem;
   public List<InventoryItem> playerItems = new List<InventoryItem>();
   public List<InventoryItem> usedItems = new List<InventoryItem>();

   public PlayerInputHandler playerInputs;

   public ItemUseCase itemUseCase;

   public event Action<bool> AnnounceOpenCloseInventory;
   public event Action<int> AnnounceSelectIndex;

   public event Action<bool> AnnounceInventoryFullEmpty;

   public GameObject objHold;
   private GameObject heldItem;
   
   public GameObject inventoryItemPrefab;
   public void Awake()
   {
      playerInputs.AnnounceInteract += AttemptUseItem;
      playerInputs.AnnounceInventory += OpenCloseInventory;
      playerInputs.AnnounceMoveVector2 += ScrollInventory;
   }

   private void AttemptUseItem(bool obj)
   {
      if (!canOpenInventory)
         return;
      
      if (!obj)
      {
         if (!inventoryOpen)
            return;

         if (selectedItem.item.useable)
         {
            itemUseCase.Use(selectedItem.item);
            RemoveItem(selectedItem.item);
         }
      }
   }

   //opening inventory stops the player from being able to move
   //this is optional, implementing for now to make it feel more strict/constrained for that horror feeling
   private void OpenCloseInventory(bool input)
   {
      if (!canOpenInventory || !canOpenInventory)
         return;

      if (input)
      {
         if (!inventoryOpen && playerItems.Count > 0)
         {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            inventoryOpen = true;
            SelectItem(selectIndex);
            AnnounceOpenCloseInventory?.Invoke(true);
         }
         else
         {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            inventoryOpen = false;
            AnnounceOpenCloseInventory?.Invoke(false);
         }
      }
   }

   private void SelectItem(int index)
   {
      if (playerItems.Count == 0 || !canOpenInventory)
         return;

      selectIndex = Mathf.Clamp(index, 0, playerItems.Count - 1);
      selectedItem = playerItems[selectIndex];
      AnnounceSelectIndex?.Invoke(selectIndex);
      AnnounceInventoryFullEmpty?.Invoke(true);
      
      if (heldItem != null)
      {
         Destroy(heldItem);
         heldItem = null;
      }
      heldItem = Instantiate(selectedItem.item.model, objHold.transform.position, objHold.transform.rotation, objHold.transform);
   }
   
   private void ScrollInventory(Vector2 input)
   {
      if (!inventoryOpen || playerItems.Count == 0 || !canOpenInventory)
         return;
      
      int direction = 0;

      if (input.x > 0.5f || input.y > 0.5f)
         direction = 1;
      else if (input.x < -0.5f || input.y < -0.5f)
         direction = -1;

      if (direction != 0)
      {
         selectIndex += direction;

         if (selectIndex >= playerItems.Count)
            selectIndex = 0;
         else if (selectIndex < 0)
            selectIndex = playerItems.Count - 1;

         SelectItem(selectIndex);
      }
   }

   public void AddItem(InventoryItem newItem)
   {
      InventoryItem invItem = CreateInventoryItem(newItem.item, newItem.data);
      print("added item with data: " + invItem.data);
      playerItems.Add(invItem);
      AnnounceInventoryFullEmpty?.Invoke(true);
   }

   public void LeftButton()
   {
      if (!canOpenInventory)
         return;
      int newIndex = selectIndex - 1;
      if (newIndex < 0)
         newIndex = playerItems.Count - 1;

      SelectItem(newIndex);
   }

   public void RightButton()
   {
      if (!canOpenInventory)
         return;
      int newIndex = selectIndex + 1;
      if (newIndex >= playerItems.Count)
         newIndex = 0;

      SelectItem(newIndex);
   }


   public void RemoveItem(ItemSO itemToRemove)
   {
      /*
      if (playerItems.Contains(itemToRemove))
      {
         Destroy(heldItem);
         heldItem = null;
         playerItems.Remove(itemToRemove);
         usedItems.Add(itemToRemove);
      }*/

      foreach (InventoryItem item in playerItems)
      {
         if (item.item == itemToRemove)
         {
            Destroy(heldItem);
            heldItem = null;
            playerItems.Remove(item);
            usedItems.Add(item);
            break;
         }
      }
      

      if (playerItems.Count == 0)
      {
         selectedItem = null;
         selectIndex = 0;
         inventoryOpen = false;
         AnnounceInventoryFullEmpty?.Invoke(false);
         AnnounceOpenCloseInventory?.Invoke(false);
         return;
      }

      selectIndex = Mathf.Clamp(selectIndex, 0, playerItems.Count - 1);
      SelectItem(selectIndex);
   }

   public void Reset()
   {
      List<InventoryItem> itemsToRemove = new List<InventoryItem>();
      itemsToRemove.AddRange(playerItems);
      itemsToRemove.AddRange(usedItems);
      foreach (var item in itemsToRemove)
      {
         item.item.Reset();
         RemoveItem(item.item);
      }
      playerItems.Clear();
   }

   void OnDisable()
   {
      playerInputs.AnnounceInteract -= AttemptUseItem;
      playerInputs.AnnounceInventory -= OpenCloseInventory;
      playerInputs.AnnounceMoveVector2 -= ScrollInventory;
   }

   public InventoryItem CreateInventoryItem(ItemSO newItemISO, string newItemData = "NULL")
   {
      GameObject it = Instantiate(inventoryItemPrefab, objHold.transform.position, objHold.transform.rotation, objHold.transform);
      InventoryItem invit = it.GetComponent<InventoryItem>();
      invit.item = newItemISO;
      invit.data = newItemData;
      return invit;
   }

   public bool FindSpecificItemInInventory(ItemSO itemSO)
   {
      foreach (var item in playerItems)
      {
         if (item.item == itemSO)
         {
            return true;
         }
      }
      return false;
   }
}
