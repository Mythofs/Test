using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Crafting
{
    public class CraftingScroll : MonoBehaviour
    {
        [SerializeField] private ScrollRect scroll;
        private void Update()
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;
            if (current != null && current.transform.IsChildOf(scroll.content))
                UpdateCraftingMenu(current);
        }
        private void UpdateCraftingMenu(GameObject current)
        {
            RectTransform currentTransform = current.GetComponent<RectTransform>();
            Vector3 currentPos = scroll.content.InverseTransformPoint(currentTransform.position);
            if(currentPos.y < scroll.content.anchoredPosition.y)
            {
                scroll.verticalNormalizedPosition += 1 / Inventory.Inventory.Instance.CraftableItems.Count;
            }
            else if(currentPos.y > scroll.content.anchoredPosition.y + scroll.viewport.rect.height)
            {
                scroll.verticalNormalizedPosition -= 1 / Inventory.Inventory.Instance.CraftableItems.Count;

            }

        }
    }
}
