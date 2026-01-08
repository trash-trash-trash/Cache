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
   public ItemSO selectedItem;
   public List<ItemSO> playerItems = new List<ItemSO>();
   public List<ItemSO> usedItems = new List<ItemSO>();

   public PlayerInputHandler playerInputs;

   public ItemUseCase itemUseCase;

   public event Action<bool> AnnounceOpenCloseInventory;
   public event Action<int> AnnounceSelectIndex;

   public event Action<bool> AnnounceInventoryFullEmpty;

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

         if (selectedItem.useable)
         {
            itemUseCase.Use(selectedItem);
            RemoveItem(selectedItem);
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

   public void AddItem(ItemSO newItem)
   {
      playerItems.Add(newItem);
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
      if (playerItems.Contains(itemToRemove))
      {
         playerItems.Remove(itemToRemove);
         usedItems.Add(itemToRemove);
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
      List<ItemSO> itemsToRemove = new List<ItemSO>();
      itemsToRemove.AddRange(playerItems);
      itemsToRemove.AddRange(usedItems);
      foreach (var item in itemsToRemove)
      {
         item.Reset();
         RemoveItem(item);
      }
      playerItems.Clear();
   }

   void OnDisable()
   {
      playerInputs.AnnounceInteract -= AttemptUseItem;
      playerInputs.AnnounceInventory -= OpenCloseInventory;
      playerInputs.AnnounceMoveVector2 -= ScrollInventory;
   }
}
