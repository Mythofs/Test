using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Units
{
    [CreateAssetMenu(fileName = "UnitDatabase", menuName = "UnitDatabase/Create a new UnitDatabase")]
    public class UnitDatabase : ScriptableObject
    {
        [SerializeField] private List<UnitBase> unitList;
        private Dictionary<string, UnitBase> unitMap;
        private void OnEnable()
        {
            if (unitMap == null)
            {
                unitMap = new();
                foreach (UnitBase unit in unitList)
                    unitMap.Add(unit.Name, unit);
            }
        }
        public Dictionary<string, UnitBase> UnitMap => unitMap;
        public UnitBase GetUnitByName(string name) => unitMap[name];
    }
}