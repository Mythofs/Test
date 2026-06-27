using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Battle
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/Create a new Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] private string levelStr;
        [SerializeField] private string enemyStr;
        [SerializeField] private string allyStr;
        [SerializeField] private string spawnArea;
        public string LevelStr => levelStr;
        public string EnemyStr => enemyStr;
        public string AllyStr => allyStr;
        public string SpawnArea => spawnArea;
    }
}