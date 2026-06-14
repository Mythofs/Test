using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dialog
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Create a new dialog object")]
    public class DialogObject : ScriptableObject
    {
        [SerializeField] private string[] text;
        [SerializeField] private bool hasDialogOption;
        [SerializeField] private List<DialogObjectEntry> dialogOptions;
        private Dictionary<string, DialogObject> dialogMap = new();
        public string[] Text => text;
        public bool HasDialogOption => hasDialogOption;
        public Dictionary<string, DialogObject> DialogMap()
        {
            if(dialogMap == null)
                foreach (DialogObjectEntry entry in dialogOptions)
                    dialogMap.Add(entry.optionName, entry.dialogOption);
            return dialogMap;
        }
    }
    [System.Serializable]
    public class DialogObjectEntry
    {
        public string optionName;
        public DialogObject dialogOption;
    }
}
