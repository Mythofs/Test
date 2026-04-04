using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class InventoryMovement : MonoBehaviour
{
    private PlayerControl control;
    private Vector2 input;
    private Action<InputAction.CallbackContext> onCancelInput;
    private Item selectedItem;
    private Image[] inventorySlots;
    private Item[] items;
    [SerializeField] private Image mainInventory;
    [SerializeField] private Image selector;
    [SerializeField] private TextMeshProUGUI sideItemName;
    [SerializeField] private Image sideItemSprite;
    [SerializeField] private TextMeshProUGUI sideItemDescription;
    
    private void Awake()
    {
        control = new PlayerControl();
        inventorySlots = mainInventory.GetComponentsInChildren<Image>();
        items = new Item[inventorySlots.Length];
        for(int a = 0; a < inventorySlots.Length; a++)
        {
            items[a] = new
        }
        onCancelInput = ctx =>
        {
            input = Vector2.zero;
        };
    }
    private void OnEnable()
    {
        control.Enable();
        control.UI.Move.performed += OnMove;
        control.UI.Move.canceled += onCancelInput;
    }
    private void OnDisable()
    {
        control.UI.Move.performed -= OnMove;
        control.UI.Move.canceled -= onCancelInput;
    }
    void Update()
    {
        if(input != Vector2.zero)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;
            Move();
        }
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    private void Move()
    {

    }
}
