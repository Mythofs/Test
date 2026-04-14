using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryMovement : MonoBehaviour
{
    private PlayerControl control;
    private Vector2 input;
    private Action<InputAction.CallbackContext> onCancelInput;
    private int index;
    private List<Image> inventorySlots;
    private float delay = 0.2f;
    private float lastMove = 0;
    [SerializeField] private Image mainInventory;
    [SerializeField] private TextMeshProUGUI sideItemName;
    [SerializeField] private Image sideItemSprite;
    [SerializeField] private TextMeshProUGUI sideItemDescription;
    [SerializeField] private int elementsPerRow;
    private void Awake()
    {
        control = new PlayerControl();
        inventorySlots = new();
        foreach (Transform child in mainInventory.transform)
        {
            inventorySlots.Add(child.GetComponent<Image>());
        }
        onCancelInput = ctx =>
        {
            input = Vector2.zero;
        };
        index = inventorySlots.Count - 1;
        transform.position = inventorySlots[index].rectTransform.position;
        SetSideBar(index);
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
        if(input != Vector2.zero && lastMove + delay < Time.time)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;
            Move();
            lastMove = Time.time;
        }
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    private void Move()
    {
        Debug.Log("Inventory input: " + input.x + " " + input.y);
        if(input.x < 0 && index + 1 < inventorySlots.Count)
            index++;
        else if(input.x > 0 && index != 0)
            index--;
        else if(input.y > 0)
        {
            index += elementsPerRow;
            index = Math.Max(index, inventorySlots.Count - 1);
        }
        else
        {
            index -= elementsPerRow;
            index = Math.Min(index, 0);
        }
        transform.position = inventorySlots[index].rectTransform.position;
        if (index < InventoryManager.Instance.Inventory.Count())
        {
            Item item = InventoryManager.Instance.Inventory.GetItem(index);
            sideItemDescription.SetText(item.ItemBase.Desc);
            sideItemName.SetText(item.ItemBase.ItemName);
            sideItemSprite.sprite = item.ItemBase.ItemSprite;
            sideItemSprite.enabled = true;
        }
        else
        {
            sideItemDescription.SetText("");
            sideItemName.SetText("");
            sideItemSprite.enabled = false;
        }
    }
    private void SetSideBar(int index)
    {
        if (index < InventoryManager.Instance.Inventory.Count())
        {
            Item item = InventoryManager.Instance.Inventory.GetItem(index);
            sideItemDescription.SetText(item.ItemBase.Desc);
            sideItemName.SetText(item.ItemBase.ItemName);
            sideItemSprite.sprite = item.ItemBase.ItemSprite;
            sideItemSprite.enabled = true;
        }
        else
        {
            sideItemDescription.SetText("");
            sideItemName.SetText("");
            sideItemSprite.enabled = false;
        }
    }
}
