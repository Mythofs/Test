using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Managers;
using System.Text;

namespace Scripts.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private RectTransform dialogOptionsContainer;
        [SerializeField] private DialogOption dialogPrefab;
        [SerializeField] private CanvasGroup dialogCanvas;
        public static DialogManager Instance { get; private set; }
        private TextMeshProUGUI text;
        private Dictionary<string, DialogObject> dialogOptionMap = new(); //contains the followup responses to each choice
        private List<DialogOption> dialogOptionElements = new(); //contains references to the actual UI elements
        public bool InDisplay { get; private set; }
        private void Awake()
        {
            Instance = this;
            Enable(false);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = "";
            InDisplay = false;
        }
        public IEnumerator DisplayText(DialogObject dialog, int index)
        {
            InDisplay = true;
            dialogOptionsContainer.gameObject.SetActive(false);
            text.text = "";
            Enable(true);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in dialog.Text[index])
            {
                stringBuilder.Append(c);
                text.SetText(stringBuilder);
                yield return null;
            }
            if (dialog.HasDialogOption && index == dialog.Text.Length - 1)
            {
                Player.PlayerInputManager.Instance.Control.Player.Interact.Disable();
                UIManager.Instance.SetCanvas(dialogCanvas);
                dialogOptionsContainer.gameObject.SetActive(true);
                DialogOption[] children = dialogOptionsContainer.GetComponentsInChildren<DialogOption>();
                foreach (DialogOption option in children)
                    Destroy(option.gameObject);
                dialogOptionElements.Clear();
                dialogOptionMap = dialog.DialogMap();
                foreach (string optionName in dialogOptionMap.Keys)
                {
                    DialogOption option = Instantiate(dialogPrefab, dialogOptionsContainer);
                    option.SetText(optionName);
                    dialogOptionElements.Add(option);
                }
                EventSystem.current.SetSelectedGameObject(dialogOptionElements[0].gameObject);
            }
            else if (index == dialog.Text.Length - 1)
                InDisplay = false;
        }
        public IEnumerator DisplayText(string dialog)
        {
            InDisplay = true;
            dialogOptionsContainer.gameObject.SetActive(false);
            text.text = "";
            Enable(true);
            foreach(char c in dialog)
            {
                text.text += c;
                yield return null;
            }
            InDisplay = false;
        }
        public void Enable(bool b)
        {
            gameObject.SetActive(b);
        }
        public void Submit(string optionName)
        {
            Player.PlayerInputManager.Instance.Control.Player.Interact.Enable();
            StartCoroutine(DisplayText(dialogOptionMap[optionName], 0));
        }
    }
}