using Scripts.Dialog;
using UnityEngine;

namespace Scripts.Interactables
{
    public abstract class IInteractable: MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected bool interacting;
        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = true;
            interacting = false;
        }
        public abstract void Interact();
        public abstract bool CanInteract();
        public virtual void Close()
        {
            DialogManager.Instance.Enable(false);
        }
        public abstract bool CanCancel();
        public virtual bool RepeatedInteract() => false;
    }
}