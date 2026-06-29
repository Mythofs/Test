using UnityEngine;

namespace Scripts.Units
{
    [CreateAssetMenu(fileName = "Unit", menuName = "UnitBase/Create a unit")]
    public class UnitBase : ScriptableObject
    {
        [SerializeField] private int maxHP;
        [SerializeField] private int damage;
        [SerializeField] private new string name;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite selected;
        [TextArea][SerializeField] private string description;
        [SerializeField] private int moveDistance;
        [SerializeField] private int id;
        public int MaxHP => maxHP;
        public int Damage => damage;
        public string Name => name;
        public Sprite UnitSprite => sprite;
        public Sprite Selected => selected;
        public string Description => description;
        public int Id => id;
    }
}