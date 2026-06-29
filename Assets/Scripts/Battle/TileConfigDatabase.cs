using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Battle
{
    [CreateAssetMenu(fileName = "TileConfigDatabase", menuName = "TileConfigDatabase/Create a new TileConfigDatabase")]
    public class TileConfigDatabase : ScriptableObject
    {
        [SerializeField] private List<TileConfig> tileConfigList;
        private Dictionary<int, TileConfig> tileConfigMap;
        public Dictionary<int, TileConfig> TileConfigMap => tileConfigMap;
        public void OnEnable()
        {
            if(tileConfigMap == null)
            {
                tileConfigMap = new();
                foreach (TileConfig config in tileConfigList)
                    if(config != null)
                        tileConfigMap.Add(config.Id, config);
            }
        }
        public TileConfig GetConfigById(int id) => tileConfigMap[id];
    }
}