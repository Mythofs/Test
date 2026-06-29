using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Battle
{
    public class BattleMovement : MonoBehaviour
    {
        private Vector2Int selectedPos;
        private Vector2Int input;
        private bool isMoving = false;
        private Dictionary<Vector2Int, BattleTile> battleTileMap;
        private BattleTile selectedTile;
        private float last = 0;
        private float delay = 0.2f;
        public static BattleMovement Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void Start()
        {
            battleTileMap = BattleManager.Instance.BattleTileMap;
        }
        private void OnEnable()
        {
            Player.PlayerInputManager.Instance.Control.Battle.Move.performed += OnMove;
        }
        private void Update()
        {
            if(!isMoving && input != Vector2Int.zero)
            {
                Vector2Int target = selectedPos + input;
                if(battleTileMap.TryGetValue(target, out var value))
                {
                    selectedPos = target;
                    selectedTile = value;
                    transform.position = selectedTile.transform.position;
                    input = Vector2Int.zero;
                }
            }
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            if(!isMoving && last + delay < Time.time)
            {
                last = Time.time;
                Vector2 rawInput = context.ReadValue<Vector2>();
                if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
                    rawInput.y = 0;
                else
                    rawInput.x = 0;
                rawInput = rawInput.normalized;
                input = Vector2Int.RoundToInt(rawInput);
            }
        }
        public void SetSelected(Vector2Int pos)
        {
            selectedPos = pos;
            selectedTile = battleTileMap[pos];
            transform.position = selectedTile.transform.position;
        }
    }
}