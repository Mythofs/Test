using Scripts.Battle;
using Scripts.Managers;
using Scripts.Units;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UnitDatabase unitDatabase;
    [SerializeField] private TileConfigDatabase tileConfigDatabase;
    [SerializeField] private BattleTile battleTilePrefab;
    private RectTransform content;
    public static BattleManager Instance { get; private set; }
    public Dictionary<Vector2Int, BattleTile> BattleTileMap { get; private set; }
    public BattleState State { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        BattleTileMap = new();
        content = GetComponent<RectTransform>();
    }
    public void Open()
    {
        LoadLevel();
        BattleMovement.Instance.SetSelected(new Vector2Int(0, 0));
        UIManager.Instance.SetCanvas(canvasGroup);
        EventSystem.current.SetSelectedGameObject(BattleTileMap[new Vector2Int(0, 0)].gameObject);
    }
    private void LoadLevel()
    {
        foreach(BattleTile tile in BattleTileMap.Values)
            Destroy(tile.gameObject);
        BattleTileMap.Clear();
        Level level = levels[GameManager.Instance.Level];
        string levelstr = level.LevelStr;
        string[] rows = levelstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        int width = rows[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Length;
        content.sizeDelta = new Vector2(width, rows.Length);
        BattleMovement.Instance.SetCameraPos(new Vector2Int(width, rows.Length));
        for (int a = rows.Length - 1; a >= 0; a--)
        {
            string[] columns = rows[a].Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int b = 0; b < columns.Length; b++)
            {
                BattleTile tile = Instantiate(battleTilePrefab, content);
                tile.SetTileConfig(tileConfigDatabase.GetConfigById(Convert.ToInt32(columns[b])));
                BattleTileMap.Add(new Vector2Int(b, a), tile);
            }
        }
        string[] enemies = level.EnemyStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
        foreach (string enemyStr in enemies)
        {
            string[] sarr = enemyStr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            UnitBase unitBase = unitDatabase.GetUnitById(Convert.ToInt32(sarr[2]));
            Vector2Int loc = new(Convert.ToInt32(sarr[0]), Convert.ToInt32(sarr[1]));
            BattleTileMap[loc].SetUnit(new Unit(unitBase, false));
        }
        string[] allies = level.AllyStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
        foreach(string allyStr in allies)
        {
            string[] sarr = allyStr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            UnitBase unitBase = unitDatabase.GetUnitById(Convert.ToInt32(sarr[2]));
            Vector2Int loc = new(Convert.ToInt32(sarr[0]), Convert.ToInt32(sarr[1]));
            BattleTileMap[loc].SetUnit(new Unit(unitBase, true));
        }
        string[] deployLoc = level.SpawnArea.Split(",", StringSplitOptions.RemoveEmptyEntries);
        foreach(string loc in deployLoc)
        {
            string[] sarr = loc.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Vector2Int pos = new(Convert.ToInt32(sarr[0]), Convert.ToInt32(sarr[1]));
            BattleTileMap[pos].SetSpawnArea(true);
        }
    }
    public void SetState(BattleState state)
    {
        State = state;
    }
    public enum BattleState { PlayerMove, PlayerAnimation, EnemyMove, EnemyAnimation }
}
