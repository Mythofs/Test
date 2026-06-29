using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            {
                if (canvas == group)
                    group.interactable = true;
                else
                    group.interactable = false;
            }
        }
        public void DisableCanvas()
        {
            foreach (CanvasGroup group in canvases)
                group.interactable = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}