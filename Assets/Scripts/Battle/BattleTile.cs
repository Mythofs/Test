using Scripts.Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Battle
{
    public class BattleTile : Selectable
    {
        [SerializeField] private Image unitSprite;
        [SerializeField] private Image background;
        [SerializeField] private Unit unit = null;
        [SerializeField] private Image selector;
        [SerializeField] private bool travelable;
        [SerializeField] private bool canPlaceUnit;
        public bool Travelable => travelable;
        public bool CanPlaceUnit => canPlaceUnit;
        protected override void Awake()
        {
            base.Awake();
            selector.enabled = false;
            unitSprite.enabled = false;
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            selector.enabled = true;
            if (unit != null)
                unitSprite.sprite = unit.Base.Selected;
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            selector.enabled = false;
            if (unit != null)
                unitSprite.sprite = unit.Base.UnitSprite;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            selector.enabled = false;
            if (unit != null)
                unitSprite.sprite = unit.Base.UnitSprite;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            selector.enabled = false;
            if (unit != null)
                unitSprite.sprite = unit.Base.UnitSprite;
        }
        public void SetUnit(Unit unit)
        {
            this.unit = unit;
            unitSprite.sprite = unit.Base.UnitSprite;
        }
        public void SetBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }
    }
}