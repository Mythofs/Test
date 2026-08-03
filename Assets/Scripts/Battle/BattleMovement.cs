using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Battle
{
    public class BattleMovement : MonoBehaviour
    {
        [SerializeField] private Camera battleCamera;
        private Vector2Int cameraPos;
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
                isMoving = true;
                Vector2Int target = selectedPos + input;
                if(battleTileMap.TryGetValue(target, out var value))
                {
                    SetSelected(target);
                    input = Vector2Int.zero;
                }
                isMoving = false;
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
        public void SetCameraPos(Vector2Int size) //maxwidth, maxheight
        {
            Vector2Int pos = new Vector2Int(size.x / 2, size.y / 2);
            if (size.x * 16 > battleCamera.orthographicSize * 2)
                pos.x = Convert.ToInt32(battleCamera.orthographicSize / 8);
            if (size.y * 16 > battleCamera.orthographicSize * 2 * battleCamera.aspect)
                pos.y = Convert.ToInt32(battleCamera.orthographicSize * battleCamera.aspect / 8);
            cameraPos = pos;
            Transform target = battleTileMap[pos].transform;
            battleCamera.transform.position = new Vector3(target.position.x, target.position.y, battleCamera.transform.position.z);
        }
    }
}