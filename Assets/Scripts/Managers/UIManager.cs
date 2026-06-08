using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<CanvasGroup> canvases;
        public static UIManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        public void SetCanvas(CanvasGroup canvas)
        {
            foreach (CanvasGroup group in canvases)
                if (canvas == group)
                    canvas.interactable = false;
                else
                    canvas.interactable = true;
        }
    }
}