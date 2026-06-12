using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private RectTransform dialogOptionsContainer;
        [SerializeField] private DialogOption dialogPrefab;
        public static DialogManager Instance { get; private set; }
        private TextMeshProUGUI text;
        private List<DialogOption> dialogOptionList = new();
        private void Awake()
        {
            Instance = this;
            Enable(false);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = "";
        }
        public IEnumerator DisplayText(DialogObject dialog)
        {
            text.text = "";
            Enable(true);
            foreach (char c in dialog.Text)
            {
                text.text += c;
                yield return null;
            }
            if(dialog.HasDialogOption)
            {
                foreach(DialogOption option in dialogOptionList)
                {
                    dialogOptionList.Remove(option);
                    Destroy(option.gameObject);
                }
                foreach(string optionText in dialog.DialogOptions)
                {
                    DialogOption option = Instantiate(dialogPrefab, dialogOptionsContainer);
                    option.SetText(optionText);
                    dialogOptionList.Add(option);
                }
                EventSystem.current.SetSelectedGameObject(dialogOptionList[0].gameObject);
            }
        }
        public void Enable(bool b)
        {
            gameObject.SetActive(b);
        }
        public void Submit(DialogOption option)
        {
            //something ig
        }
    }
}