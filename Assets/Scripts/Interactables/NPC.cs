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
                npcImage.sprite = dialogSprite;
                npcImage.enabled = true;
                StartCoroutine(Display());
                index++;
                if (index >= dialog.Text.Length)
                    repeat = false;
            }
        }
        private IEnumerator Display()
        {
            interacting = true;
            yield return StartCoroutine(DialogManager.Instance.DisplayText(dialog, index));
            interacting = false;
        }
        public override void Close()
        {
            npcImage.enabled = false;
            repeat = true;
            index = 0;
            base.Close();
        }
        public override bool CanInteract() => !interacting;
        public override bool CanCancel() => !DialogManager.Instance.InDisplay;
        public override bool RepeatedInteract() => repeat;
    }
}