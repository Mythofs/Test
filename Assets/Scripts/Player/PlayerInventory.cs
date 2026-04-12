using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
	private PlayerControl control;
	public static bool InInventory { get; private set; }
	private float delay = 0.2f;
	private float last = 0;
    [SerializeField] Camera overworld;
    [SerializeField] Camera inventory;
    private void Awake()
	{
		control = new PlayerControl();
		InInventory = false;
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
			InInventory = !InInventory;
			if (InInventory)
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