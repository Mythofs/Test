using System;
using System.Collections;
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
    }
    private void Start()
    {
        index = 0;
        StartCoroutine(SetPosition());
        SetSideBar();
    }
    private IEnumerator SetPosition()
    {
        yield return null;
        transform.position = inventorySlots[index].rectTransform.position;
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
        if(input != Vector2.zero && lastMove + delay < Time.time && PlayerInventory.InInventory)
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
        if(input.x > 0 && index + 1 < inventorySlots.Count)
            index++;
        else if(input.x < 0 && index != 0)
            index--;
        else if(input.y < 0)
        {
            index += elementsPerRow;
            index = Math.Min(index, inventorySlots.Count - 1);
        }
        else if(input.y > 0)
        {
            index -= elementsPerRow;
            index = Math.Max(index, 0);
        }
        transform.position = inventorySlots[index].rectTransform.position;
        transform.position = inventorySlots[index].rectTransform.position;
        SetSideBar();
    }
    private void SetSideBar()
    {
        Item item = null;
        if (InventoryManager.Instance.Inventory.Count() >= index)
            item = InventoryManager.Instance.Inventory.GetItem(index);
        if (item != null)
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
