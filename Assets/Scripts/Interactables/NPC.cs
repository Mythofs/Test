using UnityEngine;

namespace Scripts.Interactables
{
    public class NPC : Interactable
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string[] dialog;
        private int index = 0;
        private bool repeat = true;
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
                repeat = false;
        }
        public override void Close()
        {
            repeat = true;
            index = 0;
            base.Close();
        }
        public override bool CanInteract() => true;
        public override bool CanCancel() => false;
        public override bool RepeatedInteract() => repeat;
    }
}