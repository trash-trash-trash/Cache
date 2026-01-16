using System;
using UnityEngine;

public class PlayerMiddleManager : MonoBehaviour, IPlayer
{
    public AAAGameManager gameManager;
    public Inventory inventory;
    public Health health;
    public PlayerInteract playerInteract;
    public PlayerMovementHandler playerMovementHandler;

    public Transform cameraRoot;
    
    void Awake()
    {
        inventory.AnnounceOpenCloseInventory += FlipPlayerLookMovement;
        if (gameManager != null)
        {
            gameManager = AAAGameManager.Instance;
            gameManager.AnnouncePause += StopStartMoveLook;
        }
        health.AnnounceIsAlive += ResetInventory;
    }

    private void ResetInventory(bool input)
    {
        if (!input)
        {
            inventory.Reset();
        }
    }
    
    private void StopStartMoveLook(bool input)
    {
        FlipCanAccessInventory(!input);
        if(!inventory.inventoryOpen)
            FlipPlayerLookMovement(input);
    }

    private void FlipPlayerLookMovement(bool input)
    {
        if (input)
        {
            playerInteract.interactDisabled = true;
            playerMovementHandler.CanLook = false;
            playerMovementHandler.CanMove = false;
            Cursor.visible = true;
        }
        else
        {
            playerInteract.interactDisabled = false;
            playerMovementHandler.CanLook = true;
            playerMovementHandler.CanMove = true;
            Cursor.visible = false;
        }
    }
    
    private void FlipCanAccessInventory(bool paused)
    {
        inventory.canOpenInventory = paused;
        inventory.canUseInventory = paused;
    }

    public Transform ReturnTransform()
    {
        return cameraRoot;
    }
    
    void OnDisable()
    {
        //gameManager.AnnouncePause -= StopStartMoveLook;
        health.AnnounceIsAlive -= ResetInventory;
        inventory.AnnounceOpenCloseInventory -= FlipPlayerLookMovement;
    }
}
