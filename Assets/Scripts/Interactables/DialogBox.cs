using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class DialogBox : MonoBehaviour
    {
        public static DialogBox Instance { get; private set; }
        private TextMeshProUGUI text;
        private void Awake()
        {
            Instance = this;
            Enable(false);
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = "";
        }
        public IEnumerator DisplayText(string str)
        {
            text.text = "";
            Enable(true);
            foreach (char c in str)
            {
                text.text += c;
                yield return null;
            }
        }
        public  void Enable(bool b)
        {
            gameObject.SetActive(b);
        }
    }
}