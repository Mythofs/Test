using Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Canvas menuCanvas;
        [SerializeField] private CanvasGroup menuCanvasGroup;
        private MenuSlot selectedSlot;
        private MenuSlot[] menuSlots;
        public static MenuManager Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            menuCanvasGroup = GetComponent<CanvasGroup>();
            menuSlots = GetComponentsInChildren<MenuSlot>();
            foreach (MenuSlot slot in menuSlots)
                slot.OnSlotSelected += slot => selectedSlot = slot;
            menuCanvas.enabled = false;
        }
        public void SetText(string text)
        {
            nameText.SetText(text);
        }
        public void Open()
        {
            if (selectedSlot != null)
                EventSystem.current.SetSelectedGameObject(selectedSlot.gameObject);
            else if(menuSlots.Length > 0)
            {
                EventSystem.current.SetSelectedGameObject(menuSlots[0].gameObject);
                selectedSlot = menuSlots[0];
            }
            UIManager.Instance.SetCanvas(menuCanvasGroup);
            menuCanvas.enabled = true;
        }
        public void Close()
        {
            menuCanvas.enabled = false;
        }
    }
}
