using System.Collections;
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
                index++;
                if (index >= dialog.Length)
                {
                    repeat = false;
                    cancel = true;
                }
                StartCoroutine(Display(dialog[index-1]));
            }
        }
        private IEnumerator Display(string text)
        {
            Interacting = true;
            yield return StartCoroutine(DialogBox.Instance.DisplayText(text));
            Interacting = false;
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
            if (Interacting)
                return false;
            return cancel;
        }
        public override bool RepeatedInteract() => repeat;
    }
}