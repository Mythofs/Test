using UnityEngine;

namespace Scripts.Battle
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/Create a new Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] private string levelStr; // 1,1,1,1,1,...
        [SerializeField] private string enemyStr; // 1 1 skeleton, ...
        [SerializeField] private string allyStr; // 1 1 skeleton, ...
        [SerializeField] private string spawnArea; // 1 1, 1 2, ...
        public string LevelStr => levelStr;
        public string EnemyStr => enemyStr;
        public string AllyStr => allyStr;
        public string SpawnArea => spawnArea;
    }
}