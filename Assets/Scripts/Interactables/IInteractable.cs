using UnityEngine;

namespace Scripts.Interactables
{
    public abstract class IInteractable: MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected bool Interacting;
        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = true;
        }
        public abstract void Interact();
        public abstract bool CanInteract();
        public virtual void Close()
        {
            DialogBox.Instance.Enable(false);
        }
        public virtual bool CanCancel() => true;
        public virtual bool RepeatedInteract() => false;
        public bool GetInteracting() => Interacting;
    }
}