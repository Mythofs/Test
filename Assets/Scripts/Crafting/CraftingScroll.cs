using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Crafting
{
    public class CraftingScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform mask;
        private GameObject lastSelected = null;
        private void Update()
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current != null && current != lastSelected && current.transform.IsChildOf(content))
            {
                RectTransform currentRect = current.GetComponent<RectTransform>();
                float currentPos = currentRect.position.y;
                Vector3[] maskCorners = new Vector3[4];
                mask.GetWorldCorners(maskCorners);
                // 0 - bottom left, 1 - topleft, 2 - top right, 3 - bottom right
                if (currentPos > maskCorners[1].y)
                    content.anchoredPosition -= new Vector2(0, currentRect.rect.height);
                if (currentPos < maskCorners[0].y)
                    content.anchoredPosition += new Vector2(0, currentRect.rect.height);
                Vector3[] contentCorners = new Vector3[4];
                content.GetWorldCorners(contentCorners);
                if (contentCorners[1].y < maskCorners[1].y)
                {
                    Debug.Log("Snapping to top");
                    content.anchoredPosition += new Vector2(0, maskCorners[1].y - contentCorners[1].y);
                }
                lastSelected = current;
            }
        }
    }
}
