using UnityEngine;

namespace Scripts.Battle
{
    [CreateAssetMenu(fileName = "TileConfig", menuName = "TileConfig/Create a new TileConfig")]
    public class TileConfig : ScriptableObject
    {
        [SerializeField] private Sprite background;
        [SerializeField] private bool travelable;
        [SerializeField] private int id;
        public Sprite Background => background;
        public bool Travelable => travelable;
        public int Id => id;
    }
}