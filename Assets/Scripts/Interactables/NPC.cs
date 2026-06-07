using UnityEngine;

namespace Scripts.Interactables
{
    public class NPC : IInteractable
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string[] dialog;
        private int index = 0;
        private bool repeat = true;
        private bool cancel = false;
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
                if (index >= dialog.Length)
                {
                    repeat = false;
                    cancel = true;
                }
            }
        }
        public override void Close()
        {
            repeat = true;
            cancel = false;
            index = 0;
            base.Close();
        }
        public override bool CanInteract() => true;
        public override bool CanCancel()
        {
            if (DialogBox.Instance.Displaying)
                return false;
            return cancel;
        }
        public override bool RepeatedInteract() => repeat;
    }
}