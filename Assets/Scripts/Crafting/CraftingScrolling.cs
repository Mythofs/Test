using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Crafting
{
    public class CraftingScroll : MonoBehaviour
    {
        [SerializeField] private ScrollRect scroll;
        void Update()
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null)
                return;
            RectTransform selectedTransform = selected.GetComponent<RectTransform>();
            if (selectedTransform == null)
                return;
            Canvas.ForceUpdateCanvases();
            Vector2 position = scroll.viewport.InverseTransformPoint(selectedTransform.position);
            float viewportHeight = scroll.viewport.rect.height;
            if (position.y < 0)
                scroll.content.anchoredPosition -= new Vector2(0, position.y);
            else if (position.y > viewportHeight)
                scroll.content.anchoredPosition -= new Vector2(0, position.y - viewportHeight);
        }
    }
}
