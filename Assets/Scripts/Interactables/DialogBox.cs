using System.Collections;
using TMPro;
using UnityEngine;

namespace Scripts.Interactables
{
    public class DialogBox : MonoBehaviour
    {
        public static DialogBox Instance { get; private set; }
        private TextMeshProUGUI text;
        public bool Displaying;
        private void Awake()
        {
            Instance = this;
            Enable(false);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = "";
        }
        public IEnumerator DisplayText(string str)
        {
            Displaying = true;
            text.text = "";
            Enable(true);
            foreach (char c in str)
            {
                text.text += c;
                yield return null;
            }
            Displaying = false;
        }
        public void Enable(bool b)
        {
            gameObject.SetActive(b);
        }
    }
}