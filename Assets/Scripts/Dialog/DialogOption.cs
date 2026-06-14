using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

namespace Scripts.Dialog
{
    public class DialogOption : Selectable
    {
        [SerializeField] private Image dialogSelector;
        [SerializeField] private TextMeshProUGUI box;
        private string text;
        public string Text => text;
        protected override void Awake()
        {
            base.Awake();
            dialogSelector.enabled = false;
            box.SetText(text);
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            Player.PlayerInputManager.Instance.Control.UI.Submit.performed += OnSubmit;
            dialogSelector.enabled = true;
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
            dialogSelector.enabled = false;
        }
        private void OnSubmit(InputAction.CallbackContext context)
        {
            DialogManager.Instance.Submit(text);
        }
        public void SetText(string text)
        {
            this.text = text;
        }
    }
}
