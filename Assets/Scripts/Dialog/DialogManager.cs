using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Managers;

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
        private void Awake()
        {
            Instance = this;
            Enable(false);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = "";
        }
        public IEnumerator DisplayText(DialogObject dialog, int index)
        {
            dialogOptionsContainer.gameObject.SetActive(false);
            UIManager.Instance.SetCanvas(dialogCanvas);
            text.text = "";
            Enable(true);
            foreach (char c in dialog.Text[index])
            {
                text.text += c;
                yield return null;
            }
            if(dialog.HasDialogOption)
            {
                dialogOptionsContainer.gameObject.SetActive(true);
                foreach(DialogOption option in dialogOptionElements)
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
        }
        public IEnumerator DisplayText(string dialog)
        {
            dialogOptionsContainer.gameObject.SetActive(false);
            text.text = "";
            Enable(true);
            foreach(char c in dialog)
            {
                text.text += c;
                yield return null;
            }

        }
        public void Enable(bool b)
        {
            gameObject.SetActive(b);
        }
        public void Submit(string optionName)
        {
            StartCoroutine(DisplayText(dialogOptionMap[optionName], 0));
        }
    }
}