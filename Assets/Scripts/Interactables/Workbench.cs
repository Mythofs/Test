using Scripts.Player;
using UnityEngine;

namespace Scripts.Interactables
{
	public class Workbench: Interactable
	{
        private readonly bool inCrafting = false;
        [SerializeField] private Camera craftingCamera;
		[SerializeField] private Camera overworldCamera;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerInventory playerInventory;
        public override void Interact()
        {
            if (inCrafting)
            {
                craftingCamera.depth = -1;
                overworldCamera.depth = 0;
                playerMovement.enabled = true;
                playerInventory.enabled = true;
            }
            else
            {
                craftingCamera.depth = 0;
                overworldCamera.depth = -1;
                playerMovement.enabled = false;
                playerInventory.enabled = false;
            }
        }
        public override bool CanInteract() => true;
	}
}