using Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Battle
{
    public class BattleTile : MonoBehaviour
    {
        [SerializeField] private Image unitSprite;
        [SerializeField] private Image background;
        [SerializeField] private Unit unit = null;
        private bool canPlaceUnit;
        public bool CanPlaceUnit => canPlaceUnit;
        private TileConfig Config;
        public bool Travelable => Config.Travelable;
        private void Awake()
        {
            unitSprite.enabled = false;
            canPlaceUnit = false;
        }
        public void Select()
        {
            if (unit != null)
                unitSprite.sprite = unit.Base.Selected;
        }
        public void Deselect()
        {
            if (unit != null)
                unitSprite.sprite = unit.Base.UnitSprite;
        }
        public void SetUnit(Unit unit)
        {
            this.unit = unit;
            unitSprite.sprite = unit.Base.UnitSprite;
        }
        public void SetTileConfig(TileConfig config)
        {
            Config = config;
            background.sprite = config.Background;
        }
        public void SetSpawnArea(bool b)
        {
            canPlaceUnit = b;
        }
    }
}