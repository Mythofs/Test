using Scripts.Crafting;
using UnityEngine;

namespace Scripts.Interactables
{
	public class Workbench: IInteractable
	{
        [SerializeField] private Camera craftingCamera;
		[SerializeField] private Camera overworldCamera;
        public override void Interact()
        {
            interacting = true;
            craftingCamera.depth = 0;
            overworldCamera.depth = -1;
            CraftingManager.Instance.Open();
        }
        public override bool CanInteract() => !interacting;
        public override void Close()
        {
            interacting = false;
            base.Close();
            craftingCamera.depth = -1;
            overworldCamera.depth = 0;
        }
        public override bool CanCancel() => interacting;
	}
}