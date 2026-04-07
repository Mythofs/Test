using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
	private PlayerControl control;
	private bool inInventory = false;
	private float delay = 0.2f;
	private float last = 0;
    [SerializeField] Camera overworld;
    [SerializeField] Camera inventory;
    private void Awake()
	{
		control = new PlayerControl();
	}
	private void OnEnable()
	{
		control.Enable();
		control.Player.Inventory.performed += OnInventory;
	}
	private void OnDisable()
	{
		control.Player.Inventory.performed -= OnInventory;
	}
	private void OnInventory(InputAction.CallbackContext context)
	{
		if (last + delay < Time.time)
		{
			last = Time.time;
			inInventory = !inInventory;
			if (inInventory)
			{
				overworld.depth = -1;
				inventory.depth = 0;
			}
			else
			{
				overworld.depth = 0;
				inventory.depth = -1;
			}
		}
	}
}