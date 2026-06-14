using Scripts.Dialog;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Interactables
{
    public class NPC : IInteractable
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private DialogObject dialog;
        [SerializeField] private Image npcImage;
        [SerializeField] private Sprite dialogSprite;
        private int index = 0;
        private bool repeat = true;
        private bool cancel = false;
        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.sprite = sprite;
            npcImage.enabled = false;
        }
        public override void Interact()
        {
            if (dialog.Text.Length > index)
            {
                index++;
                if (index >= dialog.Text.Length)
                {
                    repeat = false;
                    cancel = true;
                }
                npcImage.sprite = dialogSprite;
                npcImage.enabled = true;
                StartCoroutine(Display());
            }
        }
        private IEnumerator Display()
        {
            Interacting = true;
            yield return StartCoroutine(DialogManager.Instance.DisplayText(dialog, index));
            Interacting = false;
        }
        public override void Close()
        {
            npcImage.enabled = false;
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