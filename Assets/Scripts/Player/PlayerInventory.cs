using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
	public class PlayerInventory : MonoBehaviour
	{
		private PlayerControl control;
		private Button input;
		private bool inInventory = false;
		private float delay = 0.2f;
		private Action<InputAction.CallbackContext> onCancelInput;
        [SerializeField] Camera overworld;
        [SerializeField] Camera inventory;
        private void Awake()
		{
			control = new PlayerControl();
			onCancelInput = ctx =>
			{
				inInventory = false;
				overworld.depth = 0;
				inventory.depth = -1;
            };
		}
		private void OnEnable()
		{
			control.Enable();
			control.Player.Inventory.performed += OnInventory;
			control.Player.Inventory.canceled += onCancelInput;
		}
		private void OnDisable()
		{
			control.Player.Inventory.performed -= OnInventory;
			control.Player.Inventory.canceled -= onCancelInput;
		}
		private void OnInventory(InputAction.CallbackContext context)
		{
			if(context.performed)
			{
				inInventory = !inInventory;
				if(inInventory)
				{
					overworld.depth = 0;
					inventory.depth = -1;
				}
				else
				{
					overworld.depth = -1;
					inventory.depth = 0;
				}
			}
		}
	}
}