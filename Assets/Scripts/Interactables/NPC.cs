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
            StartCoroutine(DialogBox.Instance.DisplayText(dialog[index]));
            index++;
        }
    }
}