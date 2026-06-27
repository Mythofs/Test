using Scripts.Battle;
using Scripts.Managers;
using Scripts.Units;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private List<Sprite> backgroundSprites;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UnitDatabase unitDatabase;
    public static BattleManager Instance { get; private set; }
    private BattleTile[] battleTiles;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        battleTiles = GetComponentsInChildren<BattleTile>();
    }
    public void Open()
    {
        Debug.Log("Battlemanager opening");
        LoadLevel();
        UIManager.Instance.SetCanvas(canvasGroup);
        EventSystem.current.SetSelectedGameObject(battleTiles[0].gameObject);
    }
    private void LoadLevel()
    {
        Level level = levels[GameManager.Instance.Level];
        string levelstr = level.LevelStr;
        string[] backgrounds = levelstr.Split(",");
        for (int a = 0; a < backgrounds.Length; a++)
            battleTiles[a].SetBackground(backgroundSprites[Convert.ToInt32(backgrounds[a])]);
        string[] enemies = level.LevelStr.Split(",");
        foreach (string enemyStr in enemies)
        {
            string[] sarr = enemyStr.Split(" ");
            UnitBase unitBase = unitDatabase.GetUnitByName(sarr[0]);
            battleTiles[Convert.ToInt32(sarr[1])].SetUnit(new Unit(unitBase, false));
        }
        string[] allies = level.LevelStr.Split(",");
        foreach(string allyStr in allies)
        {
            string[] sarr = allyStr.Split(" ");
            UnitBase unitBase = unitDatabase.GetUnitByName(sarr[0]);
            battleTiles[Convert.ToInt32(sarr[1])].SetUnit(new Unit(unitBase, true));
        }
    }
}
