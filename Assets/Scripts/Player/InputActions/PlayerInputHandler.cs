using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerActions controls;
    private Vector2 moveInput;

    public event Action<bool> AnnounceInteract;
    public event Action<bool> AnnounceInventory;
    public event Action<Vector2> AnnounceLook;

    public event Action<Vector2> AnnounceScroll;
    
    public event Action<Vector2> AnnounceMoveVector2;

    public event Action<bool> AnnounceQuit;
    
    public event Action<bool> AnnounceSprint;
    

    private void Awake()
    {
        controls = new PlayerActions();


        controls.InGameActions.Interact.performed += OnInteract;
        controls.InGameActions.Interact.canceled += OnInteract;

        controls.InGameActions.Inventory.performed += OnInventory;
        controls.InGameActions.Inventory.canceled += OnInventory;

        controls.InGameActions.Look.performed += OnLook;
        controls.InGameActions.Look.canceled += OnLook;

        controls.InGameActions.MouseScroll.performed += OnScroll;
        
        controls.InGameActions.Move.performed += OnMove;
        controls.InGameActions.Move.canceled += OnMove;
        
        controls.InGameActions.Quit.performed += OnQuit;
        controls.InGameActions.Quit.canceled += OnQuit;

        controls.InGameActions.Sprint.performed += OnSprint;
        controls.InGameActions.Sprint.canceled += OnSprint;
    }

    private void OnEnable()
    {
        controls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnQuit(InputAction.CallbackContext context)
    {
        if (context.performed)
            AnnounceQuit?.Invoke(true);
        else
            AnnounceQuit?.Invoke(false);
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        AnnounceScroll?.Invoke(delta);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.canceled)
            AnnounceInteract?.Invoke(false);
        else
            AnnounceInteract?.Invoke(true);
    }

    private void OnInventory(InputAction.CallbackContext context)
    {
        if (context.canceled)
            AnnounceInventory?.Invoke(false);
        else
            AnnounceInventory?.Invoke(true);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        AnnounceLook?.Invoke(delta);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveInput = context.ReadValue<Vector2>();
        else
            moveInput = Vector2.zero;

        HandleMove();
    }

    private void HandleMove()
    {
        Vector2 normalized = moveInput.normalized;
        Vector2 clampedMove = new Vector2(
            Mathf.RoundToInt(normalized.x),
            Mathf.RoundToInt(normalized.y)
        );

        moveInput = clampedMove;
        AnnounceMoveVector2?.Invoke(moveInput);
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.canceled)
            AnnounceSprint?.Invoke(false);
        else
            AnnounceSprint?.Invoke(true);
    }

    void OnDisable()
    {
        controls.Disable();

        controls.InGameActions.Interact.performed -= OnInteract;
        controls.InGameActions.Interact.canceled -= OnInteract;
        controls.InGameActions.Inventory.performed -= OnInventory;
        controls.InGameActions.Inventory.canceled -= OnInventory;
        controls.InGameActions.Look.performed -= OnLook;
        controls.InGameActions.Look.canceled -= OnLook;
        controls.InGameActions.Move.performed -= OnMove;
        controls.InGameActions.Move.canceled -= OnMove;
        controls.InGameActions.Quit.performed -= OnQuit;
        controls.InGameActions.Quit.canceled -= OnQuit;
        controls.InGameActions.Sprint.performed -= OnSprint;
        controls.InGameActions.Sprint.canceled -= OnSprint;
    }
}