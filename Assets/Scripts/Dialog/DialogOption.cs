using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Scripts.Dialog
{
    public class DialogOption : Selectable
    {
        [SerializeField] private Image dialogSelector;
        private string text;
        public string Text => text;
        protected override void Awake()
        {
            base.Awake();
            dialogSelector.enabled = false;
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            dialogSelector.enabled = true;
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            dialogSelector.enabled = false;
        }
        private void OnSubmit(InputAction.CallbackContext context)
        {
            DialogManager.Instance.Submit(this);
        }
        public void SetText(string text)
        {
            this.text = text;
        }
    }
}
