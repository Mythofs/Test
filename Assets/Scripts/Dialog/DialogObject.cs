using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dialog
{
    public class DialogObject
    {
        [SerializeField] private string text;
        [SerializeField] private bool hasDialogOption;
        [SerializeField] private List<string> dialogOptions;
        public string Text => text;
        public bool HasDialogOption => hasDialogOption;
        public List<string> DialogOptions => dialogOptions;
    }
}
