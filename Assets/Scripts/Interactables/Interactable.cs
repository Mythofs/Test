using UnityEngine;

namespace Scripts.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = true;
        }
        public abstract void Interact();
        public abstract bool CanInteract();
        public void CloseDialog()
        {
            DialogBox.Instance.Enable(false);
        }
    }
}