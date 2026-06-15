using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace Scripts.Dialog
{
    public class DialogOption : Selectable
    {
        [SerializeField] private Image dialogSelector;
        [SerializeField] private TextMeshProUGUI box;
        public string Text => box.text;
        protected override void Awake()
        {
            base.Awake();
            dialogSelector.enabled = false;
            box.SetText("");
            box.enabled = true;
        }
        protected override void Start()
        {
            base.Start();
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
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
            Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
            DialogManager.Instance.Submit(box.text);
        }
        public void SetText(string text)
        {
            box.SetText(text);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if(Player.PlayerInputManager.Instance != null)
                Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            if(Player.PlayerInputManager.Instance != null)
                Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
            dialogSelector.enabled = false;
        }
    }
}
