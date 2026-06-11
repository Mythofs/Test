using Scripts.SpecialTiles;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerControl control;
        private Vector2 input;
        private Vector2 buffer;
        private Vector2 offset = new(0f, -0.3f);
        public Vector2 Facing { get; private set; }
        private bool isMoving;
        private bool isRunning;
        private readonly float speed = 3f;
        private readonly float runningSpeed = 4f;
        private Animator animator;
        private LayerMask solidObjectsLayer;
        private LayerMask longGrassLayer;
        private LayerMask interactableObjectsLayer;
        private LayerMask portalsLayer;
        private Action<InputAction.CallbackContext> onCancelInput;
        private readonly float hitboxSize = 0.4f;
        public static PlayerMovement Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            animator = GetComponent<Animator>();
            onCancelInput = ctx =>
            {
                input = Vector2.zero;
                buffer = Vector2.zero;
            };
            solidObjectsLayer = LayerMask.GetMask("SolidObjects");
            longGrassLayer = LayerMask.GetMask("LongGrass");
            interactableObjectsLayer = LayerMask.GetMask("InteractableObjects");
            portalsLayer = LayerMask.GetMask("Portals");
        }
        private void Start()
        {
            control = PlayerInputManager.Instance.Control;
            OnEnable();
        }
        private void OnEnable()
        {
            if (control == null)
                return;
            control.Player.Move.performed += OnMove;
            control.Player.Move.canceled += onCancelInput;
        }
        private void OnDisable()
        {
            if (control == null)
                return;
            control.Player.Move.performed -= OnMove;
            control.Player.Move.canceled -= onCancelInput;
        }
        void Update()
        {
            if (!isMoving && input != Vector2.zero)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                    input.y = 0;
                else
                    input.x = 0;
                Vector3 direction = new Vector3(input.x, input.y, 0);
                if (direction != Vector3.zero)
                {
                    Facing = input;
                    animator.SetFloat("moveX", input.x);
                    animator.SetFloat("moveY", input.y);
                    Vector2 target = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y);
                    if (IsWalkable(target))
                        StartCoroutine(Move(target));
                }
            }
            animator.SetBool("isMoving", isMoving);
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            if (!isMoving)
                input = context.ReadValue<Vector2>();
            else if (context.ReadValue<Vector2>() != Vector2.zero)
            {
                Vector2 raw = context.ReadValue<Vector2>();
                if (Mathf.Abs(raw.x) > Mathf.Abs(raw.y))
                    raw.y = 0;
                else
                    raw.x = 0;
                buffer = raw.normalized;
            }
        }
        private IEnumerator Move(Vector2 target)
        {
            isMoving = true;
            while (Vector2.Distance(target, transform.position) > 0.0001f)
            {
                if (isRunning)
                    transform.position = Vector2.MoveTowards(transform.position, target, runningSpeed * Time.deltaTime);
                else
                    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
            transform.position = new Vector2(SnapX(target.x), SnapY(target.y));
            isMoving = false;
            if (buffer != Vector2.zero)
            {
                input = buffer;
                buffer = Vector2.zero;
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            CheckEncounters();
            CheckPortal();
        }
        private bool IsWalkable(Vector2 target)
        {
            //If it is not null, then there is overlap
            bool blockedBySolid = Physics2D.OverlapCircle(target + offset, hitboxSize, solidObjectsLayer) != null;
            bool blockedByInteractable = Physics2D.OverlapCircle(target + offset, hitboxSize, interactableObjectsLayer) != null;
            return !blockedBySolid && !blockedByInteractable;
        }
        private void CheckEncounters()
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), hitboxSize, longGrassLayer) != null)
            {
                if (UnityEngine.Random.Range(1, 101) <= 10)
                    Debug.Log("Encountered a wild pokemon");
            }
        }
        private void CheckPortal()
        {
            Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), hitboxSize, portalsLayer);
            if (col != null)
            {
                Portal portal = col.GetComponent<Portal>();
                portal.Warp(transform);
            }
        }
        private float SnapY(float y)
        {
            return Mathf.Round(y - 0.8f) + 0.8f;
        }
        private float SnapX(float x)
        {
            return Mathf.Round(x - 0.5f) + 0.5f;
        }
        public void SetFacing(Vector2 direction)
        {
            Facing = direction;
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        }
    }
}