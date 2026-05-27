using UnityEngine;

namespace Scripts.Interactables
{
    public class NPC : Interactable
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string[] dialog;
        private int index = 0;
        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.sprite = sprite;
        }
        public override void Interact()
        {
            if (dialog.Length > index)
            {
                StartCoroutine(DialogBox.Instance.DisplayText(dialog[index]));
                index++;
            }
            else
                index = 0;
        }
        public override bool CanInteract() => true;
        public override bool CanCancel() => false;

    }
}