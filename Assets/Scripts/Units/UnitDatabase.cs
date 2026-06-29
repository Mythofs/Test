using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Units
{
    [CreateAssetMenu(fileName = "UnitDatabase", menuName = "UnitDatabase/Create a new UnitDatabase")]
    public class UnitDatabase : ScriptableObject
    {
        [SerializeField] private List<UnitBase> unitList;
        private Dictionary<int, UnitBase> unitMap;
        private void OnEnable()
        {
            if (unitMap == null)
            {
                unitMap = new();
                foreach (UnitBase unit in unitList)
                    if(unit != null)
                        unitMap.Add(unit.Id, unit);
            }
        }
        public Dictionary<int, UnitBase> UnitMap => unitMap;
        public UnitBase GetUnitById(int id) => unitMap[id];
    }
}