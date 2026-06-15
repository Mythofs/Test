using Scripts.Dialog;
using Scripts.Interactables;
using Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private Vector2 offset = new(0f, -0.3f);
        private PlayerControl control;
        private LayerMask interactableObjectsLayer;
        private SpriteRenderer sr;
        public IInteractable InteractScript { get; private set; }
        public static PlayerInteract Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            interactableObjectsLayer = LayerMask.GetMask("InteractableObjects");
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
            control.Player.Interact.performed += OnInteract;
        }
        private void OnDisable()
        {
            if (control == null)
                return;
            control.Player.Interact.performed -= OnInteract;
        }
        private void OnInteract(InputAction.CallbackContext context)
        {
            if(GameManager.Instance.State == GameManager.GameState.Interact)
            {
                if (InteractScript.CanInteract())
                {
                    if (InteractScript.RepeatedInteract())
                        InteractScript.Interact();
                    else
                    {
                        InteractScript.Close();
                        GameManager.Instance.SetState(GameManager.GameState.Overworld);
                    }
                }
                return;
            }
            Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + PlayerMovement.Instance.Facing + offset, 0.2f, interactableObjectsLayer);
            if (col != null)
            {
                sr = col.GetComponent<SpriteRenderer>();
                InteractScript = sr.GetComponent<IInteractable>();
                if (InteractScript.CanInteract())
                {
                    GameManager.Instance.SetState(GameManager.GameState.Interact);
                    InteractScript.Interact();
                }
            }
            else
                GameManager.Instance.SetState(GameManager.GameState.Overworld);
        }
    }
}