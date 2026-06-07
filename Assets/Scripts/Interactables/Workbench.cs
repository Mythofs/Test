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
            craftingCamera.depth = 0;
            overworldCamera.depth = -1;
            CraftingManager.Instance.Open();
        }
        public override bool CanInteract() => true;
        public override void Close()
        {
            base.Close();
            craftingCamera.depth = -1;
            overworldCamera.depth = 0;
        }
	}
}